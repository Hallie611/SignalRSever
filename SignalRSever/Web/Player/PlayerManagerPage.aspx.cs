﻿using System;
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
            if (!IsPostBack)
            {
                
                GVPlayer.DataSource = manager.Get_allPlayer();
                GVPlayer.DataBind();
            }
        }

        protected void GVPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr= GVPlayer.SelectedRow;
            Response.Redirect("PlayerDetail.aspx?name=" + gr.Cells[1].Text);

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
    }
}