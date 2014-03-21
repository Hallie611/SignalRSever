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
        private static readonly List<Client> listPlay = new List<Client>();
        private static int _gamesPlayed = 0;
        /// <summary>
        /// This list is used to keep track of games and their states
        /// </summary>
        private static readonly List<Chasing> games = new List<Chasing>();
        private static  List<Question> listQ = new List<Question>();


        public void getValue(List<Question> a)
        {
            listQ = a;
           
        }

        public Task SendStatsUpdate()
        {

            return Clients.All.refeshAmountOfPlayer(new { totalClient = listPlay.Count });
        }

        public override Task OnDisconnected()
        {
            var clientWithoutGame = listPlay.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (clientWithoutGame != null)
            {
                listPlay.Remove(clientWithoutGame);

                SendStatsUpdate();
            }
            return null;
        }


        public void Register(string name,int level,int point)
        {
            lock (_syncRoot)
            {
                var client = listPlay.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (client == null)
                {
                    client = new Client { ConnectionId = Context.ConnectionId, Name = name,point=point ,};
                    listPlay.Add(client);
                }

                client.IsPlaying = false;
            }
            SendStatsUpdate();
        }


        public void FindOpponent(int level)
        {
            var player = listPlay.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (player == null) return ;
            player.LookingForOpponent = true;
            player.level = level;
            // Look for a random opponent if there's more than one looking for a game
            var opponent = listPlay.Where(x => x.ConnectionId != Context.ConnectionId && x.LookingForOpponent && !x.IsPlaying).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            if (opponent == null)
            {
                Clients.Client(Context.ConnectionId).noOpponents();
                return ;
            }
            // Set both players as busy
            player.IsPlaying = true;
            player.LookingForOpponent = false;
            opponent.IsPlaying = true;
            opponent.LookingForOpponent = false;
            player.Opponent = opponent;
            opponent.Opponent = player;
            
            // Notify both players that a game was found



            
            Clients.Client(Context.ConnectionId).foundOpponent(new { oName =opponent.Name, oLevel = opponent.level ,oPoint= opponent.point  });
            Clients.Client(opponent.ConnectionId).foundOpponent(new { oName = player.Name, oLevel = player.level, oPoint = player.point });

            lock (_syncRoot)
            {
                games.Add(new Chasing { Player1 = player, Player2 = opponent });

                if (player.level > opponent.level)
                {
                    Clients.Client(Context.ConnectionId).createQuestionList();
                }
                else
                {
                    Clients.Client(opponent.ConnectionId).createQuestionList();
                }

                Clients.Client(Context.ConnectionId).getQuestionList(listQ);
                Clients.Client(opponent.ConnectionId).getQuestionList(listQ);
            }

            SendStatsUpdate();


        }


        /// <summary>
        /// Play a marker at a given positon
        /// </summary>
        /// <param name="position">The position where to place the marker</param>
        public void Play(int position)
        {
            // Find the game where there is a player1 and player2 and either of them have the current connection id
            var game = games.FirstOrDefault(x => x.Player1.ConnectionId == Context.ConnectionId || x.Player2.ConnectionId == Context.ConnectionId);

            if (game == null || game.IsGameOver) return;

            int marker = 0;

            // Detect if the player connected is player 1 or player 2
            if (game.Player2.ConnectionId == Context.ConnectionId)
            {
                marker = 1;
            }
            var player = marker == 0 ? game.Player1 : game.Player2;

            // If the player is waiting for the opponent but still tried to make a move, just return
            if (player.WaitingForMove) return;

            // Notify both players that a marker has been placed
            Clients.Client(game.Player1.ConnectionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });
            Clients.Client(game.Player2.ConnectionId).addMarkerPlacement(new GameInformation { OpponentName = player.Name, MarkerPosition = position });

            // Place the marker and look for a winner
            if (game.Play(marker, position))
            {
                games.Remove(game);
                _gamesPlayed += 1;
                Clients.Client(game.Player1.ConnectionId).gameOver(player.Name);
                Clients.Client(game.Player2.ConnectionId).gameOver(player.Name);
            }

            // If it's a draw notify the players that the game is over
            if (game.IsGameOver && game.IsCorrect)
            {
                games.Remove(game);
                _gamesPlayed += 1;
                Clients.Client(game.Player1.ConnectionId).gameOver("It's a draw!");
                Clients.Client(game.Player2.ConnectionId).gameOver("It's a draw!");
            }

            if (!game.IsGameOver)
            {
                player.WaitingForMove = !player.WaitingForMove;
                player.Opponent.WaitingForMove = !player.Opponent.WaitingForMove;

                Clients.Client(player.Opponent.ConnectionId).waitingForMarkerPlacement(player.Name);
            }

            SendStatsUpdate();
        }
    }
}