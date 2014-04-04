﻿using System;
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
            HyperLink1.NavigateUrl = "Player/PlayerManagerPage.aspx";
            HyperLink2.NavigateUrl = "Question/QuestionHistory.aspx";
        }
    }
}