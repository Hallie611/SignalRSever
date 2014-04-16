using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using System.Data;
using System.IO;
using System.Drawing;

namespace SignalRSever.Web.Question
{
    public partial class QuestionManager : System.Web.UI.Page
    {
        QuestionManagers manager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
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
            Session["QType"] = gr.Cells[2].Text;
            Response.Redirect("/Web/Question/QuestionDetail.aspx?ID=" + gr.Cells[1].Text);
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

        protected void btExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=QuestionHistory.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GV_historyQuestion.AllowPaging = false;
                GV_historyQuestion.DataSource = Session["data"];
                GV_historyQuestion.DataBind();

                GV_historyQuestion.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GV_historyQuestion.HeaderRow.Cells)
                {
                    cell.BackColor = GV_historyQuestion.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GV_historyQuestion.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GV_historyQuestion.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GV_historyQuestion.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GV_historyQuestion.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}