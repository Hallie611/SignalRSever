using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;

namespace SignalRSever.Web.Question
{
    public partial class QuestionManager : System.Web.UI.Page
    {
        QuestionManagers manager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GV_historyQuestion.DataSource = manager.GetQuestionHistory();
                GV_historyQuestion.DataBind();

            }
        }

        protected void GV_historyQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GV_historyQuestion.SelectedRow;
            Response.Redirect("QuestionDetail.aspx?ID=" + gr.Cells[1].Text);

        }

        protected void GV_historyQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Hiding the Select Button Cell in Header Row.
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Hiding the Select Button Cells showing for each Data Row. 
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

                // Attaching one onclick event for the entire row, so that it will
                // fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(this.GV_historyQuestion, "Select$" + e.Row.RowIndex);
            }
        }
    }
}