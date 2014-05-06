using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRSever.Web
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginFailed"] != null)
            {
                lbMess.Text = "Please Login first!";
            }
            Master.FindControl("Manage").Visible = false;
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == Application["Password"].ToString())
            {
                Session["CheckLogin"] = "Done";
                Response.Redirect("/Web/Player/PlayerManagerPage.aspx");
            }
            else
            {
                lbMess.Text = "Invalid Login, Please try again!";
            }
        }
    }
}