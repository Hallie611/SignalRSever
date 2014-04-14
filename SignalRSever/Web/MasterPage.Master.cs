using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRSever.Web
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lbtlogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Web/Home.aspx");
        }
    }
}