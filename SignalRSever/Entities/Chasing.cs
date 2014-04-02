using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DataAccess;
using SignalRSever.Business;

namespace SignalRSever
{
    public class Chasing
    {
        public bool IsGameOver { get; private set; }
        
        public Client Player1 { get; set; }
        public Client Player2 { get; set; }
        public Client Winner { get; set; }
        public int[,] score { get; set; }
        public List<Question> listQ { get; set; }
        public PlayerManager playerManager = new PlayerManager();
        private int QuestionLeft = 3;

        public Chasing()
        {
            
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
                if (score[1, 0] > score[2, 0])
                    Winner = Player1;
                else if (score[1, 0] < score[2, 0])
                    Winner = Player2;
                else
                    Winner = null;
                IsGameOver = true;
                UpdateData();
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
                if (score[1, 0] > score[2, 0])
                    Winner = Player1;
                else if (score[1, 0] < score[2, 0])
                    Winner = Player2;
                else
                    Winner = null;

                IsGameOver = true;
                UpdateData();
                return true;
            }


            return false;
        }

        public void UpdateData()
        {
            if(Winner!=null)
            playerManager.UpdatePoint(Winner.name, Winner.level * 10, Winner.opponent.name, Winner.opponent.level * 10);
            for (int i = 1; i < 4; i++)
            {
                if (score[1, i] == 0)
                {
                    playerManager.WrongQuestion(Player1.name, listQ[i - 1].questionId);
                }
                else
                {
                    playerManager.RightQuestion(Player1.name, listQ[i - 1].questionId);
                }
                if (score[2, i] == 0)
                {
                    playerManager.WrongQuestion(Player2.name, listQ[i - 1].questionId);
                }
                else
                {
                    playerManager.RightQuestion(Player2.name, listQ[i - 1].questionId);
                }
            }

        }

    }
}