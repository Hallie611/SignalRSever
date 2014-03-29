using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignalRSever.DAL;


namespace SignalRSever
{
    public class gamesHub : Hub
    {
        private static object _syncRoot = new object();
        private static readonly List<Client> listClient = new List<Client>();
        /// <summary>
        /// This list is used to keep track of games and their states
        /// </summary>
        private static readonly List<Chasing> games = new List<Chasing>();
        public LocalDataDataContext data = new LocalDataDataContext();

        public void postQuestion(List<Question> a)
        {

            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            game.listQ = a;
            Clients.Client(game.Player1.connectionId).getQuestionList(game.listQ);
            Clients.Client(game.Player2.connectionId).getQuestionList(game.listQ);
        }

        public Task SendStatsUpdate()
        {
            return Clients.All.refeshAmountOfPlayer(new { totalClient = listClient.Count });
        }

        public override Task OnDisconnected()
        {
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            if (game != null)
            {
                Client playerOut = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;

                playerOut.opponent.isReady = false;
                playerOut.opponent.lookingForOpponent = false;

                Clients.Client(playerOut.opponent.connectionId).OpponentDisconnect();
                playerOut.opponent.point += 5;
                var opponent = data.tblPlayers.FirstOrDefault(x => x.PlayerName == playerOut.opponent.name);
                opponent.PlayerPoint += 5;
                data.SubmitChanges();
                listClient.Remove(playerOut);
                games.Remove(game);
                SendStatsUpdate();
            }
            return null;
        }

        public void OutOfMath()
        {
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            if (game != null)
            {
                Client playerOut = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;
                playerOut.isReady = false;
                playerOut.lookingForOpponent = false;

                playerOut.point -= 5;
                playerOut.opponent.isReady = false;
                playerOut.opponent.lookingForOpponent = false;
                playerOut.opponent.point += 5;
                var opponent = data.tblPlayers.FirstOrDefault(x => x.PlayerName == playerOut.opponent.name);
                opponent.PlayerPoint += 5;
                data.SubmitChanges();
                var player = data.tblPlayers.FirstOrDefault(x => x.PlayerName == playerOut.name);
                player.PlayerPoint -= 5;
                data.SubmitChanges();
                games.Remove(game);
                Clients.Client(playerOut.opponent.connectionId).OpponentDisconnect();
            }

        }

        public void Register(string name)
        {
            lock (_syncRoot)
            {
                var clientdt = data.tblPlayers.FirstOrDefault(x => x.PlayerName == name);
                if (clientdt == null)
                {
                    data.Insert_Player(name, 1, 100);
                    Clients.Client(Context.ConnectionId).TrySave(true);
                }
                else
                {
                    Clients.Client(Context.ConnectionId).TrySave(false);
                }
            }

        }

        public void ConnectSever(string name, int level, int point)
        {
            lock (_syncRoot)
            {
                var client = listClient.FirstOrDefault(x => x.name == name);
                var player = data.tblPlayers.FirstOrDefault(x => x.PlayerName == name);
                if (player != null)
                {
                    player.PlayerLevel = level;
                    player.PlayerPoint = point;
                    data.SubmitChanges();
                }
                if (client == null)
                {
                    client = new Client { connectionId = Context.ConnectionId, name = name, point = point, level = level };
                    listClient.Add(client);
                }
                client.isReady = false;
                client.lookingForOpponent = false;
            }
            SendStatsUpdate();
        }


        public void FindOpponent()
        {
            var player = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);
            if (player == null) return;
            player.lookingForOpponent = true;
            // Look for a random opponent if there's more than one looking for a game
            var opponent = listClient.Where(x => x.connectionId != Context.ConnectionId && x.lookingForOpponent).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            if (opponent == null)
            {
                Clients.Client(Context.ConnectionId).noOpponents();
                return;
            }
            // Set both players as busy
            player.lookingForOpponent = false;
            opponent.lookingForOpponent = false;
            player.opponent = opponent;
            opponent.opponent = player;

            // Notify both players that a game was found
            Clients.Client(Context.ConnectionId).foundOpponent(new { oName = opponent.name, oLevel = opponent.level, oPoint = opponent.point });
            Clients.Client(opponent.connectionId).foundOpponent(new { oName = player.name, oLevel = player.level, oPoint = player.point });

            lock (_syncRoot)
            {
                games.Add(new Chasing { Player1 = player, Player2 = opponent });

                if (player.level > opponent.level)
                {
                    Clients.Client(Context.ConnectionId).createQuestionList();
                }
                else
                {
                    Clients.Client(opponent.connectionId).createQuestionList();
                }
            }

        }

        public void playerReady()
        {
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            if (game == null || game.IsGameOver) return;


            if (game.Player1.connectionId == Context.ConnectionId)
                game.Player1.isReady = true;
            else
                game.Player2.isReady = true;

            if (game.Player1.isReady == true && game.Player2.isReady == true)
            {
                game.Player1.mathPoint = 0;
                game.Player2.mathPoint = 0;

                Clients.Client(game.Player1.connectionId).gameReady();
                Clients.Client(game.Player2.connectionId).gameReady();
            }
        }


        /// <summary>
        /// Play a marker at a given positon
        /// </summary>
        /// <param name="position">The position where to place the marker</param>
        public void correctQuestion(int position, int mark,bool getMaxPoint)
        {
            // Find the game where there is a player1 and player2 and either of them have the current connection id
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);

            if (game == null || game.IsGameOver)
            {
                games.Remove(game);
                return; 
            }

            Client PlayerSummit = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2 ;

            // Place the marker and look for a winner
            if (game.Play(PlayerSummit, position, mark, getMaxPoint))
            {
                game.Player1.lookingForOpponent = false;
                game.Player2.lookingForOpponent = false;
                game.Player1.isReady = false;
                game.Player2.isReady = false;

                if (game.Winner == null)
                {
                    
                }
                else
                {
                    Clients.Client(game.Winner.connectionId).gameOver(new { Name = game.Winner.name, Point = "+10" });
                    Clients.Client(game.Winner.opponent.connectionId).gameOver(new { Name = game.Winner.name, Point = "-5" });
                }
                games.Remove(game);
            }
            if (position != 2)
            {
                Clients.Client(game.Player1.connectionId).CorrectedQuestion(PlayerSummit.name, position, mark);
                Clients.Client(game.Player2.connectionId).CorrectedQuestion(PlayerSummit.name, position, mark);
            }
        }


        
        public void GameOver()
        {
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);

        }
    }
}