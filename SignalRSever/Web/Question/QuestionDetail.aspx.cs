using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SignalRSever.Business;

namespace SignalRSever.Web.Question
{
    public partial class QuestionDetail : System.Web.UI.Page
    {
        QuestionManagers Qmanager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
            int id = int.Parse(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                int cor = Int32.Parse(Session["Correct"].ToString());
                int wro = Int32.Parse(Session["Wrong"].ToString());

                if (Session["QType"].ToString() == "Find Bugs")
                {
                    FillBlank.Visible = false;
                    SingleChoice.Visible = false;

                    DataTable dt = Qmanager.GetQuestionByID(id);
                    lbQuestionIDFB.Text = dt.Rows[0]["QuestionID"].ToString();
                    lbTypeFB.Text = dt.Rows[0]["Type"].ToString();
                    lbDifFB.Text = dt.Rows[0]["Difficulty"].ToString();
                    ImageFB.ImageUrl = "~/" + dt.Rows[0]["SRC"].ToString();

                    int[] yValues = { cor, wro };
                    string[] xValues = { "Correct", "Wrong" };
                    ChartFB.Series["Series1"].Points.DataBindXY(xValues, yValues);

                    ChartFB.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    ChartFB.Legends[0].Enabled = true;
                    ChartFB.Series["Series1"].Label = "#VAL";
                    ChartFB.Series["Series1"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";

                    imageCSS.Style.Add("left", dt.Rows[0]["Left"].ToString());
                    imageCSS.Style.Add("top", dt.Rows[0]["Top"].ToString());
                    imageCSS.Style.Add("width", dt.Rows[0]["Width"].ToString());
                    imageCSS.Style.Add("height", dt.Rows[0]["Height"].ToString());
                }
                else if (Session["QType"].ToString() == "Fill Blanks")
                {
                    FindBugs.Visible = false;
                    SingleChoice.Visible = false;

                    DataTable dt = Qmanager.GetQuestionByID(id);
                    lbQuestionIDFK.Text = dt.Rows[0]["QuestionID"].ToString();
                    lbTypeFK.Text = dt.Rows[0]["Type"].ToString();
                    lbDifFK.Text = dt.Rows[0]["Difficulty"].ToString();
                    lbIndex.Text = dt.Rows[0]["Index"].ToString();
                    ImageFK.ImageUrl = "~/" + dt.Rows[0]["SRC"].ToString();

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

                    int[] yValues = { cor, wro };
                    string[] xValues = { "Correct", "Wrong" };
                    ChartFK.Series["Series1"].Points.DataBindXY(xValues, yValues);

                    ChartFK.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    ChartFK.Legends[0].Enabled = true;
                    ChartFK.Series["Series1"].Label = "#VAL";
                    ChartFK.Series["Series1"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                }
                else
                {
                    FindBugs.Visible = false;
                    FillBlank.Visible = false;

                    DataTable dt = Qmanager.GetQuestionByID(id);
                    lbQuestionIDSC.Text = dt.Rows[0]["QuestionID"].ToString();
                    lbTypeSC.Text = dt.Rows[0]["Type"].ToString();
                    lbDifSC.Text = dt.Rows[0]["Difficulty"].ToString();
                    lbAnswerSC.Text = dt.Rows[0]["Answer"].ToString();
                    ImageSC.ImageUrl = "~/" + dt.Rows[0]["SRC"].ToString();

                    int[] yValues = { cor, wro };
                    string[] xValues = { "Correct", "Wrong" };
                    ChartSC.Series["Series1"].Points.DataBindXY(xValues, yValues);

                    ChartSC.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    ChartSC.Legends[0].Enabled = true;
                    ChartSC.Series["Series1"].Label = "#VAL";
                    ChartSC.Series["Series1"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                }

            }
        }
    }
}