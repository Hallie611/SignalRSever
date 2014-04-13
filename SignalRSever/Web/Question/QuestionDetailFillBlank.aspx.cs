using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using System.Data;
using System.Drawing;

namespace SignalRSever.Web.Question
{
    public partial class QuestionDetailFillBlank : System.Web.UI.Page
    {
        QuestionManagers Qmanager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                GV_QuestionDetail.DataSource = Qmanager.GetQuestionByID(id);
                GV_QuestionDetail.DataBind();

                DataTable dt = Qmanager.GetQuestionByID(id);
                lbQuestionID.Text = dt.Rows[0]["QuestionID"].ToString();
                lbType.Text = dt.Rows[0]["Type"].ToString();
                lbDif.Text = dt.Rows[0]["Difficulty"].ToString();
                lbIndex.Text = dt.Rows[0]["Index"].ToString();
                Image.ImageUrl = "~/" + dt.Rows[0]["SRC"].ToString();

                string[] s1;
                string a1 = dt.Rows[0]["List Answer"].ToString();
                s1 = a1.Split(',');
                lbList1_1.Text = s1[0];
                lbList1_2.Text = s1[1];
                lbList1_3.Text = s1[2];
                if (s1[0].ToLower() == dt.Rows[0]["Answer"].ToString().ToLower())
                { lbList1_1.ForeColor = Color.Red; }
                else if (s1[1].ToLower() == dt.Rows[0]["Answer"].ToString().ToLower())
                { lbList1_2.ForeColor = Color.Red; }
                else
                { lbList1_3.ForeColor = Color.Red; }

                string[] s2;
                string a2 = dt.Rows[1]["List Answer"].ToString();
                s2 = a2.Split(',');
                lbList2_1.Text = s2[0];
                lbList2_2.Text = s2[1];
                lbList2_3.Text = s2[2];
                if (s2[0].ToLower() == dt.Rows[1]["Answer"].ToString().ToLower())
                { lbList2_1.ForeColor = Color.Red; }
                else if (s2[1].ToLower() == dt.Rows[1]["Answer"].ToString().ToLower())
                { lbList2_2.ForeColor = Color.Red; }
                else
                { lbList2_3.ForeColor = Color.Red; }

                string[] s3;
                string a3 = dt.Rows[2]["List Answer"].ToString();
                s3 = a3.Split(',');
                lbList3_1.Text = s3[0];
                lbList3_2.Text = s3[1];
                lbList3_3.Text = s3[2];
                if (s3[0].ToLower() == dt.Rows[2]["Answer"].ToString().ToLower())
                { lbList3_1.ForeColor = Color.Red; }
                else if (s3[1].ToLower() == dt.Rows[2]["Answer"].ToString().ToLower())
                { lbList3_2.ForeColor = Color.Red; }
                else
                { lbList3_3.ForeColor = Color.Red; }
            }
        }
    }
}