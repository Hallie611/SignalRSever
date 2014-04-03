using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;

namespace SignalRSever.Web.Question
{
    public partial class QuestionDetail : System.Web.UI.Page
    {
        QuestionManagers Qmanager  = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int id = int.Parse(Request.QueryString["ID"]);
            if (!IsPostBack) {
                GV_QuestionDetail.DataSource = Qmanager.GetQuestionByID(id);
                GV_QuestionDetail.DataBind();
            }
        }
    }
}