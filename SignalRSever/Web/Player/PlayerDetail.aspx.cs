﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;

namespace SignalRSever.Web.Player
{
    public partial class PlayerDetail : System.Web.UI.Page
    {
        PlayerManager manager = new PlayerManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string name = Request.QueryString["name"];
            lblname.Text = name;
            if (!IsPostBack)
            {
                GVPlayer_Detail.DataSource = manager.GetQuestionsByUser(name);
                GVPlayer_Detail.DataBind();
            }
        }

        protected void GVPlayer_Detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GVPlayer_Detail.SelectedRow;
            Response.Redirect("../Question/QuestionDetail.aspx?ID=" + gr.Cells[1].Text);
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