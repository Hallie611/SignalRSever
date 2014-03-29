using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DAL;

namespace SignalRSever
{
    public class Chasing
    {
        public bool IsGameOver { get; private set; }
        
        public Client Player1 { get; set; }
        public Client Player2 { get; set; }
        public Client Winner { get; set; }
        public bool isGameOver { get; set; }

        public List<Question> listQ { get; set; }

       // private LocalDataDataContext data = new LocalDataDataContext();
        private ApphabourSeverDataContext data = new ApphabourSeverDataContext();

        private int QuestionLeft = 3;
        public readonly int[,] score = new int[3,4];

        public Chasing()
        {
            //Reset game
            for (int i = 0; i < 4; i++)
            {
                score[1,i] = 0;
                score[2,i] = 0;
                
            }
        }
        /// <summary>
        /// Insert a correct at a given index for a given player
        /// </summary>
        /// <param name="player">The player number should be 0 or 1</param>
        /// <param name="position">The index where to place the marker, should be between 0 and 5</param>
        /// <returns>True if a winner was found</returns>
        public bool Play(Client player, int posion, int mark, bool getMaxPoint)
        {
            
            if (posion == 0)
            {
                Winner = Winner = score[1, 0] > score[2, 0] ? Player1 : Player2;
                isGameOver = true;
                UpdateHistory();
               return true;
            }

            if (getMaxPoint)
            {
                QuestionLeft--;

            }

            if (player == Player1)
            {
                score[1,posion] = mark;
                score[1, 0] += mark;
            }
            else if(player==Player2)
            {
                score[2,posion] = mark;
                score[2, 0] += mark;
            }

            if (QuestionLeft <= 0)
            {
                Winner = score[1,0] > score[2,0] ? Player1 : Player2;
                isGameOver = true;
                UpdateHistory();
                return true;
            }


            return false;
        }

        public void UpdateHistory()
        {
            for (int i = 1; i < 4; i++)
            {
                if (score[1, i] == 0)
                {
                    data.wrongQuestion(Player1.name, listQ[i - 1].questionId);
                }
                else
                {
                    data.correctQuestion(Player1.name, listQ[i - 1].questionId);
                }
                if (score[2, i] == 0)
                {
                    data.wrongQuestion(Player2.name, listQ[i - 1].questionId);
                }
                else
                {
                    data.correctQuestion(Player2.name, listQ[i - 1].questionId);
                }
            }

        }

    }
}