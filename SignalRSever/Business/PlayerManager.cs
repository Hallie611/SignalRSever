using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DataAccess;
using System.Web.UI.WebControls;
using System.Data;
using SignalRSever.Entities;

namespace SignalRSever.Business
{
    public class PlayerManager
    {
        public ServerDataDataContext severdata = new ServerDataDataContext();

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

        public DataTable Get_allPlayer()
        {
            DataTable result = new DataTable();


            result.Columns.Add(new DataColumn("Name", typeof(string)));
            result.Columns.Add(new DataColumn("Level", typeof(int)));
            result.Columns.Add(new DataColumn("Point", typeof(int)));
            result.Columns.Add(new DataColumn("Win", typeof(int)));
            result.Columns.Add(new DataColumn("Lose", typeof(int)));


            var players = severdata.get_all_player();

            foreach (var p in players)
            {
                DataRow dr;
                dr = result.NewRow();

                dr["Name"] = p.PlayerName;
                dr["Level"] = p.PlayerLevel;
                dr["Point"] = p.PlayerPoint;
                dr["Win"] = p.Win;
                dr["Lose"] = p.Lose;
                result.Rows.Add(dr);
            }
            return result;
        }

        public DataTable GetQuestionsByUser(string name)
        {
            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn("QuestionID", typeof(int)));
            result.Columns.Add(new DataColumn("Type", typeof(string)));
            result.Columns.Add(new DataColumn("Right", typeof(int)));
            result.Columns.Add(new DataColumn("Wrong", typeof(int)));

            var players = severdata.question_report_by_name(name);

            foreach (var p in players)
            {
                DataRow dr;
                dr = result.NewRow();
                dr["QuestionID"] = p.QuestionID;
                dr["Type"] = p.Type;
                dr["Right"] = p.Rright;
                dr["Wrong"] = p.Wrong;
                result.Rows.Add(dr);
            }

            return result;
        }
    }
}