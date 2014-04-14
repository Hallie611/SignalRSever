using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using System.Data;

namespace SignalRSever.Web
{
    public partial class UserManagerPage : System.Web.UI.Page
    {
        PlayerManager manager = new PlayerManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
            if (!IsPostBack)
            {
                Session["data"] = manager.Get_allPlayer();
                GVPlayer.DataSource = Session["data"];
                GVPlayer.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVPlayer.DataSource = Session["data"];
            GVPlayer.PageIndex = e.NewPageIndex;
            GVPlayer.DataBind();
        }

        protected void GVPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GVPlayer.SelectedRow;
            Session["Level"] = gr.Cells[2].Text;
            Session["Point"] = gr.Cells[3].Text;
            Session["Win"] = gr.Cells[4].Text;
            Session["Lose"] = gr.Cells[5].Text;
            Response.Redirect("/Web/Player/PlayerDetail.aspx?name=" + gr.Cells[1].Text);
        }

        protected void GVPlayer_RowDataBound(object sender, GridViewRowEventArgs e)
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
                  ClientScript.GetPostBackClientHyperlink(this.GVPlayer, "Select$" + e.Row.RowIndex);
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
            GVPlayer.DataSource = dv;
            GVPlayer.DataBind();
        }
    }
}