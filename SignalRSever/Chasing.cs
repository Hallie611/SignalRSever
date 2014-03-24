using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSever
{
    public class Chasing
    {
        public bool IsGameOver { get; private set; }


        public Client Player1 { get; set; }

        public Client Player2 { get; set; }
        public Client Winner { get; set; }

        private readonly int[] score = new int[3];
        private int QuestionLeft = 3;

        public Chasing()
        {
            //Reset game
            for (var i = 0; i < score.Length; i++)
            {
                score[i] = -1;
            }
        }
        /// <summary>
        /// Insert a correct at a given index for a given player
        /// </summary>
        /// <param name="player">The player number should be 0 or 1</param>
        /// <param name="position">The index where to place the marker, should be between 0 and 5</param>
        /// <returns>True if a winner was found</returns>
        public bool Play(int player ,int posion)
        {
            QuestionLeft -= 1;
            score[posion - 1] = player;
            if (QuestionLeft <= 0)
            {
                int player1count=0;
                for (var i = 0; i < score.Length; i++)
                {
                    if (score[i] == 0)
                        player1count++;
                }
                if (player1count >= 2)
                    Winner = Player1;
                else
                    Winner = Player2;
                IsGameOver = true;

                return true;
            }
            return false;
        }
        
    }
}