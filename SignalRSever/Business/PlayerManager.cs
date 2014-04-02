using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DataAccess;

namespace SignalRSever.Business
{
    public class PlayerManager
    {
        public SeverDataDataContext severdata = new SeverDataDataContext();

        public void UpdatePoint(string winner, int winPoint, string loser, int lossPoint)
        {
            severdata.update_point(winner, winPoint, loser, lossPoint);
        }

        public void RightQuestion(string userName, int questionId)
        {
            severdata.correctQuestion(userName, questionId);
        }

        public void WrongQuestion(string userName, int questionId)
        {
            severdata.wrongQuestion(userName, questionId);
        }

        public bool CheckUserExist(string username)
        {
            var clientdt = severdata.get_player_info(username).FirstOrDefault();
            if (clientdt == null)
                return false;

            return true;
        }

        public void AddUser(string username)
        {
            severdata.Insert_Player(username);
        }
        public void UpdateUser(string username, int level, int point)
        {
            severdata.update_player_info(username, level, point);
        }

        public Client GetPlayerInfo(string username)
        {
            var player = severdata.get_player_info(username).FirstOrDefault();
            return new Client { name = player.PlayerName, level = (int)player.PlayerLevel, point = (int)player.PlayerPoint };
        }


    }
}