using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignalRSever.Business;
using System.Threading;
using SignalRSever.Entities;


namespace SignalRSever
{
    public class gamesHub : Hub
    {
        private static object _syncRoot = new object();
        private static readonly List<Client> listClient = new List<Client>();
        /// <summary>
        /// This list is used to keep track of listMatches and their states
        /// </summary>
        private static readonly List<Match> listMatches = new List<Match>();
        // public LocalDataDataContext data = new LocalDataDataContext();

        public PlayerManager playerManager = new PlayerManager();
        public QuestionManagers questionManager = new QuestionManagers();


        public override Task OnDisconnected()
        {
            var game = listMatches.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            Client player = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);

            if (game != null)
            {
                lock (_syncRoot)
                {
                    Client playerOut = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;
                    playerOut.opponent.lookingForOpponent = false;
                    playerOut.opponent.isReady = false;
                    playerManager.UpdatePoint(playerOut.opponent.name, 5, playerOut.name, 0);
                    listMatches.Remove(game);
                    return Clients.Client(playerOut.opponent.connectionId).OpponentDisconnect();
                }
            }
            listClient.Remove(player);
            return null;
        }

        public void OutOfMath()
        {
            var game = listMatches.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            if (game != null)
            {
                lock (_syncRoot)
                {
                    Client playerOut = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;
                    playerOut.isReady = false;
                    playerOut.lookingForOpponent = false;

                    playerOut.opponent.isReady = false;
                    playerOut.opponent.lookingForOpponent = false;

                    playerManager.UpdatePoint(playerOut.opponent.name, 5, playerOut.name, 5);
                    listMatches.Remove(game);
                    Clients.Client(playerOut.opponent.connectionId).OpponentDisconnect();
                }
            }

        }

        public bool Register(string name)
        {
            lock (_syncRoot)
            {
                if (!playerManager.CheckUserExist(name))
                {
                    playerManager.AddUser(name);
                    //Clients.Client(Context.ConnectionId).TrySave(true);
                    return true;
                }
                else
                {
                    // Clients.Client(Context.ConnectionId).TrySave(false);
                }
                return false;
            }

        }

        public void ConnectSever(string name, int level, int point)
        {
            lock (_syncRoot)
            {
                playerManager.UpdateUser(name, level, point);
                var client = listClient.FirstOrDefault(x => x.name == name);
                if (client == null)
                {
                    client = new Client();
                    client.connectionId = Context.ConnectionId;
                    client.name = name;
                    client.level = level;
                    client.point = point;
                    listClient.Add(client);


                }
                client.isReady = false;
                client.lookingForOpponent = false;
            }

        }


        public bool FindOpponent()
        {

            lock (_syncRoot)
            {
                var player = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);

                //if (player == null) return;

                player.lookingForOpponent = true;
                // Look for a random opponent if there's more than one looking for a game
                var opponent = listClient.Where(x => x.connectionId != Context.ConnectionId && x.lookingForOpponent).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                if (opponent == null)
                {
                    return false;
                }
                else
                {
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
                        Match game = new Match(player, opponent);
                        listMatches.Add(game);


                        //int dif = game.Player1.level < game.Player2.level ? game.Player1.level : game.Player2.level;

                        //game.listQ = questionManager.RandomQuestion(dif);
                        //game.QuestionCorrect = game.listQ.Count;
                        //game.QuestionLeft = game.listQ.Count * 2;

                        //game.score = new int[3, game.listQ.Count + 1];
                        ////Reset game
                        //for (int i = 0; i < game.listQ.Count + 1; i++)
                        //{
                        //    game.score[1, i] = 0;
                        //    game.score[2, i] = 0;

                        //}


                        //if (game.listQ[0].type == "Single Choice")
                        //{
                        //    game.listQ[0].type = "Fill Blanks";
                        //    game.listQ[0].questionId = game.listQ[0].questionId - 1000;
                        //}
                        Clients.Client(game.Player1.connectionId).getQuestionList(game.listQ);
                        Clients.Client(game.Player2.connectionId).getQuestionList(game.listQ);
                        return true;
                    }


                }
            }

        }

        public void foundNoOpponents()
        {
            Client player = listClient.FirstOrDefault(x => x.connectionId == Context.ConnectionId);
            if (player != null)
                player.lookingForOpponent = false;
        }


        public void playerReady()
        {
            var game = listMatches.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);
            //            if (game == null || game.IsGameOver) return;
            var player = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;

            if (game.Player1.connectionId == Context.ConnectionId)
            {
                game.Player1.isReady = true;
            }
            else
            {
                game.Player2.isReady = true;
            }


            if (game.Player1.isReady == true && game.Player2.isReady == true)
            {
                Clients.Client(game.Player1.connectionId).gameReady();
                Clients.Client(game.Player2.connectionId).gameReady();
            }
            else
            {
                Clients.Client(player.opponent.connectionId).oponentReady(player.name);
            }

        }


        /// <summary>
        /// Play a marker at a given positon
        /// </summary>
        /// <param name="position">The position where to place the marker</param>
        public void postAnswer(int position, int mark, bool getMaxPoint)
        {
            // Find the game where there is a player1 and player2 and either of them have the current connection id
            var game = listMatches.FirstOrDefault(x => x.Player1.connectionId == Context.ConnectionId || x.Player2.connectionId == Context.ConnectionId);

            if (game == null || game.IsGameOver)
            {
                listMatches.Remove(game);
                return;
            }


            Client PlayerSummit = game.Player1.connectionId == Context.ConnectionId ? game.Player1 : game.Player2;
            // Place the marker and look for a winner
            if (game.Play(PlayerSummit, position, mark, getMaxPoint))
            {
                game.Player1.lookingForOpponent = false;
                game.Player2.lookingForOpponent = false;
                game.Player1.isReady = false;
                game.Player2.isReady = false;


                Clients.Client(game.Player1.connectionId).updateCorrectedQuestion(new { Name = PlayerSummit.name, index = position, point = mark, isMax = getMaxPoint });
                Clients.Client(game.Player2.connectionId).updateCorrectedQuestion(new { Name = PlayerSummit.name, index = position, point = mark, isMax = getMaxPoint });


                if (game.Winner == null)
                {
                    Clients.Client(game.Player1.connectionId).gameOver(new { Name = "none", Point = "" });
                    Clients.Client(game.Player2.connectionId).gameOver(new { Name = "none", Point = "" });
                }
                else
                {
                    Clients.Client(game.Winner.connectionId).gameOver(new { Name = game.Winner.name, Point = "+10" });
                    Clients.Client(game.Winner.opponent.connectionId).gameOver(new { Name = game.Winner.name, Point = "-5" });
                }
                listMatches.Remove(game);
            }
            else
            {

                Clients.Client(game.Player1.connectionId).updateCorrectedQuestion(new { Name = PlayerSummit.name, index = position, point = mark, isMax = getMaxPoint });
                Clients.Client(game.Player2.connectionId).updateCorrectedQuestion(new { Name = PlayerSummit.name, index = position, point = mark, isMax = getMaxPoint });
            }

        }




    }
}