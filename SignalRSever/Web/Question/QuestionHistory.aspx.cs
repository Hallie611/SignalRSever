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
    public partial class QuestionManager : System.Web.UI.Page
    {
        QuestionManagers manager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["data"] = manager.GetQuestionHistory();
                GV_historyQuestion.DataSource = Session["data"];
                GV_historyQuestion.DataBind();;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_historyQuestion.DataSource = Session["data"];
            GV_historyQuestion.PageIndex = e.NewPageIndex;
            GV_historyQuestion.DataBind();
        }

        protected void GV_historyQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GV_historyQuestion.SelectedRow;
            if (gr.Cells[2].Text == "Find Bugs")
            { Response.Redirect("QuestionDetailFindBugs.aspx?ID=" + gr.Cells[1].Text); }
            else if (gr.Cells[2].Text == "Fill Blanks")
            { Response.Redirect("QuestionDetailFillBlank.aspx?ID=" + gr.Cells[1].Text); }
            else
            { Response.Redirect("QuestionDetailSingleChoice.aspx?ID=" + gr.Cells[1].Text); }
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

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            DataTable dt = new DataTable();
            dt = Session["data"] as DataTable;
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            DataTable dt2 = new DataTable();
            dt2 = dv.ToTable();
            Session["data"] = dt2;
            GV_historyQuestion.DataSource = dv;
            GV_historyQuestion.DataBind();
        }
    }
}