using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalRSever.DataAccess;
using System.Data;

namespace SignalRSever.Business
{
    public class QuestionManagers
    {
        public SeverDataDataContext severdata = new SeverDataDataContext();

        public DataTable GetQuestionByID(int ID)
        {
            DataTable result = new DataTable();
            var type = severdata.get_question_type(ID).FirstOrDefault();


            if (type.Type != null)
                switch (type.Type)
                {
                    case "Find Bugs":
                        result.Columns.Add(new DataColumn("QuestionID", typeof(int)));
                        result.Columns.Add(new DataColumn("Type", typeof(string)));
                        result.Columns.Add(new DataColumn("Difficulty", typeof(int)));
                        result.Columns.Add(new DataColumn("SRC", typeof(string)));
                        result.Columns.Add(new DataColumn("Width", typeof(string)));
                        result.Columns.Add(new DataColumn("Height", typeof(string)));
                        result.Columns.Add(new DataColumn("Top", typeof(string)));
                        result.Columns.Add(new DataColumn("Left", typeof(string)));

                        var questionFindBug = severdata.get_findbugs_byID(ID);

                        foreach (var q in questionFindBug)
                        {
                            DataRow dr;
                            dr = result.NewRow();
                            dr["QuestionID"] = q.QuestionID;
                            dr["Type"] = q.Type;
                            dr["Difficulty"] = q.QuestionDif;
                            dr["SRC"] = q.SRC;
                            dr["Width"] = q.QuestionID;
                            dr["Height"] = q.Type;
                            dr["Top"] = q.QuestionDif;
                            dr["Left"] = q.SRC;

                            result.Rows.Add(dr);
                        }

                        break;
                    case "Fill Blanks":

                        result.Columns.Add(new DataColumn("QuestionID", typeof(int)));
                        result.Columns.Add(new DataColumn("Type", typeof(string)));
                        result.Columns.Add(new DataColumn("Difficulty", typeof(int)));
                        result.Columns.Add(new DataColumn("SRC", typeof(string)));
                        result.Columns.Add(new DataColumn("Index", typeof(int)));
                        result.Columns.Add(new DataColumn("List Answer", typeof(string)));
                        result.Columns.Add(new DataColumn("Answer", typeof(string)));


                        var questionFillBlank = severdata.get_fillBlank_byID(ID);

                        foreach (var q in questionFillBlank)
                        {
                            DataRow dr;
                            dr = result.NewRow();
                            dr["QuestionID"] = q.QuestionID;
                            dr["Type"] = q.Type;
                            dr["Difficulty"] = q.QuestionDif;
                            dr["SRC"] = q.SRC;
                            dr["Index"] = q.AnswerIndex;
                            dr["List Answer"] = q.ListAnswers;
                            dr["Answer"] = q.CorrectAnswer;
                            result.Rows.Add(dr);
                        }


                        break;
                    case "Multiple Choice":
                        result.Columns.Add(new DataColumn("QuestionID", typeof(int)));
                        result.Columns.Add(new DataColumn("Type", typeof(string)));
                        result.Columns.Add(new DataColumn("Difficulty", typeof(int)));
                        result.Columns.Add(new DataColumn("SRC", typeof(string)));
                        result.Columns.Add(new DataColumn("Answer", typeof(string)));


                        var questionMulti = severdata.get_mulichoice_byID(ID);

                        foreach (var q in questionMulti)
                        {
                            DataRow dr;
                            dr = result.NewRow();
                            dr["QuestionID"] = q.QuestionID;
                            dr["Type"] = q.Type;
                            dr["Difficulty"] = q.QuestionDif;
                            dr["SRC"] = q.SRC;
                            dr["Answer"] = q.Answer;
                            result.Rows.Add(dr);
                        }
                        break;
                }


            return result;
        }


        public DataTable GetQuestionHistory()
        {
            DataTable result = new DataTable();

            var history = severdata.get_all_questions_history();

            result.Columns.Add(new DataColumn("QuestionID", typeof(int)));
            result.Columns.Add(new DataColumn("Type", typeof(string)));
            result.Columns.Add(new DataColumn("Difficulty", typeof(int)));
            result.Columns.Add(new DataColumn("Correct", typeof(int)));
            result.Columns.Add(new DataColumn("Wrong", typeof(int)));

            foreach (var h in history)
            {
                DataRow dr;
                dr = result.NewRow();
                dr["QuestionID"] = h.QuestionID;
                dr["Type"] = h.Type;
                dr["Difficulty"] = h.QuestionDif;
                dr["Correct"] = h.Correct;
                dr["Wrong"] = h.Wrong;
                result.Rows.Add(dr);
            }

            return result;
        }
    }
}