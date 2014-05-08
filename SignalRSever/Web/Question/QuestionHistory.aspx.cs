using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRSever.Business;
using System.Data;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;


namespace SignalRSever.Web.Question
{
    public partial class QuestionManager : System.Web.UI.Page
    {
        QuestionManagers manager = new QuestionManagers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CheckLogin"] == null)
            {
                Session["LoginFailed"] = "error";
                Response.Redirect("/Web/Home.aspx");
            }
            if (!IsPostBack)
            {
                lblNoRecords.Visible = false;
                Session["data"] = manager.GetQuestionHistory();
                GV_historyQuestion.DataSource = Session["data"];
                GV_historyQuestion.DataBind();;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_historyQuestion.DataSource = Session["data"];
            GV_historyQuestion.PageIndex = e.NewPageIndex;
            GV_historyQuestion.DataBind();
        }

        protected void GV_historyQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GV_historyQuestion.SelectedRow;
            Session["QType"] = gr.Cells[2].Text;
            Session["Correct"] = gr.Cells[4].Text;
            Session["Wrong"] = gr.Cells[5].Text;
            Response.Redirect("/Web/Question/QuestionDetail.aspx?ID=" + gr.Cells[1].Text);
        }

        protected void GV_historyQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
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
                  ClientScript.GetPostBackClientHyperlink(this.GV_historyQuestion, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            lblNoRecords.Visible = false;
            DataTable dt = new DataTable();
            dt = Session["data"] as DataTable;
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;
            DataTable dt2 = new DataTable();
            dt2 = dv.ToTable();
            Session["data"] = dt2;
            GV_historyQuestion.DataSource = dv;
            GV_historyQuestion.DataBind();
        }

        public static void DumpExcel(DataTable dataTable)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DataTable");

                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                for (int i = 1; i <= dataTable.Columns.Count; i++)
                {
                    worksheet.Column(i).AutoFit();

                    if (dataTable.Columns[i - 1].DataType == System.Type.GetType("System.DateTime"))
                    {
                        worksheet.Column(i).Style.Numberformat.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    }

                    for (int j = 2; j <= dataTable.Rows.Count + 1; j++)
                    {
                        if (j % 2 == 0)
                        {
                            worksheet.Cells[j, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[j, i].Style.Fill.BackgroundColor.SetColor(Color.WhiteSmoke);
                            worksheet.Cells[j, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        }
                        else
                        {
                            worksheet.Cells[j, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[j, i].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            worksheet.Cells[j, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[j, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        }
                    }
                }

                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Font.Color.SetColor(Color.White);
                worksheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.Gray);

                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=QuestionHistory.xlsx");
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                HttpContext.Current.Response.End();
            }
        }

        protected void btExport_Click(object sender, EventArgs e)
        {
            DataTable ttb = (DataTable)Session["data"];
            DumpExcel(ttb);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string str = txtSearch.Text.ToLower();
            int countrows = 0;
            lblNoRecords.Visible = false;
            if (str == "")
            {
                GV_historyQuestion.DataSource = Session["data"];
                GV_historyQuestion.DataBind();
            }
            else
            {
                GV_historyQuestion.AllowPaging = false;
                GV_historyQuestion.DataSource = Session["data"];
                GV_historyQuestion.DataBind();
                for (int i = 0; i < GV_historyQuestion.Rows.Count; i++)
                {
                    if (!GV_historyQuestion.Rows[i].Cells[1].Text.ToLower().Contains(str) && !GV_historyQuestion.Rows[i].Cells[2].Text.ToLower().Contains(str) && !GV_historyQuestion.Rows[i].Cells[3].Text.ToLower().Contains(str) && !GV_historyQuestion.Rows[i].Cells[4].Text.ToLower().Contains(str) && !GV_historyQuestion.Rows[i].Cells[5].Text.ToLower().Contains(str))
                    {
                        GV_historyQuestion.Rows[i].Visible = false;
                        countrows++;
                    }
                }
                if (GV_historyQuestion.Rows.Count == countrows)
                {
                    lblNoRecords.Visible = true;
                }
                GV_historyQuestion.AllowPaging = true;
            }
        }
    }
}