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

        protected void Page_Load(object sender, EventArgs e)
        {
            name = Request.QueryString["name"];
            int level = manager.GetPlayerInfo4PlayerDetailPage(name).level;
            int point = manager.GetPlayerInfo4PlayerDetailPage(name).point;
            int win = manager.GetPlayerInfo4PlayerDetailPage(name).win;
            int lose = manager.GetPlayerInfo4PlayerDetailPage(name).lose;

            lblname.Text = name;
            lbllevel.Text = level.ToString();
            lblpoint.Text = point.ToString();
            lblwin.Text = win.ToString();
            lbllose.Text = lose.ToString();

            int[] yValues = { win, lose };
            string[] xValues = { "Win", "Lose" };
            Chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);

            Chart1.Series["Series1"]["PieLabelStyle"] = "Disabled";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            Chart1.Series["Series1"].Label = "#VAL";
            Chart1.Series["Series1"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";

            if (!IsPostBack)
            {
                BindData(name);
            }
        }

        void BindData(string name)
        {
            GV_games.DataSource = manager.GetGamesByUser(name);
            GV_games.DataBind();
            GVPlayer_Detail.DataSource = manager.GetQuestionsByUser(name);
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
            if (gr.Cells[2].Text == "Find Bugs")
            { Response.Redirect("QuestionDetailFindBugs.aspx?ID=" + gr.Cells[1].Text); }
            else if (gr.Cells[2].Text == "Fill Blanks")
            { Response.Redirect("QuestionDetailFillBlank.aspx?ID=" + gr.Cells[1].Text); }
            else
            { Response.Redirect("QuestionDetailSingleChoice.aspx?ID=" + gr.Cells[1].Text); }
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