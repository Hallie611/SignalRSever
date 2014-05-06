using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRSever.Web
{
    public partial class ChangePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
        }

        protected void btConfirm_Click(object sender, EventArgs e)
        {
            if (txtPassCurrent.Text == Application["Password"].ToString())
            {
                if (txtPassNew.Text == txtPassConfirm.Text)
                {
                    Application["Password"] = txtPassNew.Text;
                    Response.Redirect("/Web/Player/PlayerManagerPage.aspx");
                }
                else lbMess.Text = "New Password doesn't match";
            }
            else lbMess.Text = "Wrong Password!";
        }
    }
}