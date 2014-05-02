using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DataAccess;
using SignalRSever.Business;

namespace SignalRSever.Entities
{
    public class Match
    {

        public PlayerManager playerManager = new PlayerManager();
        public QuestionManagers questionMN = new QuestionManagers();

        public bool IsGameOver { get; private set; }

        public Client Player1 { get; set; }
        public Client Player2 { get; set; }
        public Client Winner { get; set; }
        private int[,] score { get; set; }
        public List<Question> listQ { get; set; }
        private int QuestionLeft;
        private int QuestionCorrect;


        public Match(Client player, Client opponent)
        {
            Player1 = player;
            Player2 = opponent;
            listQ = questionMN.RandomQuestion(Player1.level < Player2.level ? Player1.level : Player2.level);

            QuestionCorrect = listQ.Count;
            QuestionLeft = listQ.Count * 2;

            score = new int[3, listQ.Count + 1];
            //Reset game
            for (int i = 0; i < listQ.Count + 1; i++)
            {
                score[1, i] = 0;
                score[2, i] = 0;

            }




            if (listQ[0].type == "Single Choice")
            {
                listQ[0].type = "Fill Blanks";
                listQ[0].questionId = listQ[0].questionId - 1000;
            }

            //listQ[0].questionId = 2;
            //listQ[0].type = "Find Bugs";
            //listQ[1].questionId = 1002;
            //listQ[1].type = "Fill Blanks";
            //listQ[2].questionId = 2002;
            //listQ[2].type = "Single Choice";
        }
        /// <summary>
        /// Insert a correct at a given index for a given player
        /// </summary>
        /// <param name="player">The player number should be 0 or 1</param>
        /// <param name="position">The index where to place the marker, should be between 0 and 5</param>
        /// <returns>True if a winner was found</returns>
        public bool Play(Client player, int posion, int mark, bool getMaxPoint)
        {
            QuestionLeft--;
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
                QuestionCorrect--;
                QuestionLeft--;
            }

            if (player == Player1)
            {
                score[1, posion] = mark;
                score[1, 0] += mark;
            }
            else if (player == Player2)
            {
                score[2, posion] = mark;
                score[2, 0] += mark;
            }

            if (QuestionLeft <= 0 || QuestionCorrect <= 0)
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


        public bool UpdateData()
        {
            try
            {
                if (Winner != null)
                    playerManager.UpdatePoint(Winner.name, Winner.opponent.level * 10, Winner.opponent.name, Winner.opponent.level * 10);
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
                return true;
            }
            catch (Exception e) { return false; }


        }

    }
}