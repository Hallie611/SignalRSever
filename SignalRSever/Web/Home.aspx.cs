using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;

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
                lbMess.Text = "Wrong password, please try again!";
            }
        }

        protected void lbforgot_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("hhbao92@gmail.com");
            mail.From = new MailAddress("chasinggameapp@gmail.com", "Mistake Chasing Game System", System.Text.Encoding.UTF8);
            mail.Subject = "Remind Password";
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = "This is automatic messages. Please do not reply!<br/>Current password is: " + Application["Password"].ToString();
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("chasinggameapp@gmail.com", "ChasingGameApp2014");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                lbMess.Text = "The password has sent to admin email! Go and check it!";
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                lbMess.Text = "Sending Failed...";
            }
        }
    }
}