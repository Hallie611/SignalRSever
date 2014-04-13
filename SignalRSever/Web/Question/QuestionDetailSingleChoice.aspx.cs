using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using System.Data;

namespace SignalRSever.Web.Question
{
    public partial class QuestionDetailSingleChoice : System.Web.UI.Page
    {
        QuestionManagers Qmanager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                DataTable dt = Qmanager.GetQuestionByID(id);
                lbQuestionID.Text = dt.Rows[0]["QuestionID"].ToString();
                lbType.Text = dt.Rows[0]["Type"].ToString();
                lbDif.Text = dt.Rows[0]["Difficulty"].ToString();
                lbAnswer.Text = dt.Rows[0]["Answer"].ToString();
                Image.ImageUrl = "~/" + dt.Rows[0]["SRC"].ToString();
            }
        }
    }
}