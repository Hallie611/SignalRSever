using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        private static List<Question> listQ = new List<Question>();
        public MistakeGameDataDataContext data = new MistakeGameDataDataContext();

        public void postQuestion(List<Question> a)
        {
            listQ.Clear();
            listQ = a;
            var client = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);


            Clients.Client(client.connectionId).getQuestionList(listQ);
            Clients.Client(client.opponent.connectionId).getQuestionList(listQ);
        }

        public Task SendStatsUpdate()
        {
            return Clients.All.refeshAmountOfPlayer(new { totalClient = listClient.Count });
        }

        public override Task OnDisconnected()
        {
            var clientWithoutGame = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);
            if (clientWithoutGame != null)
            {
                clientWithoutGame.opponent.isReady = false;
                clientWithoutGame.opponent.lookingForOpponent = false;
                Clients.Client(clientWithoutGame.opponent.connectionId).OpponentDisconnect();
                listClient.Remove(clientWithoutGame);
                SendStatsUpdate();
            }
            return null;
        }

        public void Register(string name)
        {
            lock (_syncRoot)
            {
                var clientdt = data.tblPlayers.FirstOrDefault(x => x.PlayerName == name);
                if (clientdt == null)
                {
                    data.tblPlayers.InsertOnSubmit(new tblPlayer { PlayerName = name, PlayerLevel = 1, PlayerPoint = 100 });
                    data.SubmitChanges();
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
                Clients.Client(game.Player1.connectionId).gameReady();
                Clients.Client(game.Player2.connectionId).gameReady();
            }
        }


        /// <summary>
        /// Play a marker at a given positon
        /// </summary>
        /// <param name="position">The position where to place the marker</param>
        public void correctQuestion(int position)
        {
            // Find the game where there is a player1 and player2 and either of them have the current connection id
            var game = games.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);

            if (game == null || game.IsGameOver) return;

            int marker = 0;

            // Detect if the player connected is player 1 or player 2
            if (game.Player2.connectionId == Context.ConnectionId)
            {
                marker = 1;
            }
            var player = marker == 0 ? game.Player1 : game.Player2;

            // If the player is waiting for the opponent but still tried to make a move, just return
            //if (player.WaitingForMove) return;

            // Notify both players that a marker has been placed
            //Clients.Client(game.Player1.ConnectionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });
            //Clients.Client(game.Player2.ConnectionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });
            Clients.Client(game.Player1.connectionId).CorrectedQuestion(player.connectionId, position);
            Clients.Client(game.Player2.connectionId).CorrectedQuestion(player.connectionId, position);


            // Place the marker and look for a winner
            if (game.Play(marker, position))
            {
                game.Player1.lookingForOpponent = false;
                game.Player2.lookingForOpponent = false;
                game.Player1.isReady = false;
                game.Player2.isReady = false;
                Clients.Client(game.Winner.connectionId).gameOver(new { Name = game.Winner.name , Point = "+100" });
                Clients.Client(game.Winner.opponent.connectionId).gameOver(new { Name = game.Winner.name , Point = "-50"});
                games.Remove(game);
            }
        }
    }
}