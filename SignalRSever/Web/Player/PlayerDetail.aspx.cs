using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using SignalRSever.Entities;
using System.Data;

namespace SignalRSever.Web.Player
{
    public partial class PlayerDetail : System.Web.UI.Page
    {
        PlayerManager manager = new PlayerManager();

        static string name;
        static string level;
        static string point;
        static string win;
        static string lose;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
            name = Request.QueryString["name"];
            level = Session["Level"].ToString();
            point = Session["Point"].ToString();
            win = Session["Win"].ToString();
            lose = Session["Lose"].ToString();

            lblname.Text = name;
            lbllevel.Text = level;
            lblpoint.Text = point;
            lblwin.Text = win;
            lbllose.Text = lose;

            int[] yValues = { Int32.Parse(win), Int32.Parse(lose) };
            string[] xValues = { "Win", "Lose" };
            Chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            Chart1.Series["Series1"].Label = "#VAL";
            Chart1.Series["Series1"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";

            if (!IsPostBack)
            {
                BindData(name);
                GV_games.Columns[1].Visible = false;
            }
        }

        void BindData(string name)
        {
            GV_games.DataSource = manager.GetGamesByUser(name);
            if (GV_games.DataSource == null)
                Response.Redirect("/Web/ErrorPage.html");
            GV_games.DataBind();
            GVPlayer_Detail.DataSource = manager.GetQuestionsByUser(name);
            if (GVPlayer_Detail.DataSource == null)
                Response.Redirect("/Web/ErrorPage.html");
            GVPlayer_Detail.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVPlayer_Detail.PageIndex = e.NewPageIndex;
            BindData(name);
        }

        protected void GVPlayer_Detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GVPlayer_Detail.SelectedRow;
            Session["QType"] = gr.Cells[2].Text;
            Session["Correct"] = gr.Cells[3].Text;
            Session["Wrong"] = gr.Cells[4].Text;
            Response.Redirect("/Web/Question/QuestionDetail.aspx?ID=" + gr.Cells[1].Text);
        }

        protected void GVPlayer_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
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
                  ClientScript.GetPostBackClientHyperlink(this.GVPlayer_Detail, "Select$" + e.Row.RowIndex);
            }
        }
    }
}