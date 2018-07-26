using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.html.simpleparser;

public partial class SalesAdmin_DailyReports : System.Web.UI.Page
{

    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            txtStartDate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            BindReport();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void BindReport()
    {
        try
        {
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBORpeort.GetDailyReports(txtStartDate.Text, txtTodate.Text, Session["loginId"].ToString(), RoleId, CompanyId);
            if (objDs.Tables[1].Rows.Count > 0)
            {
                SetCacheDataset(objDs);
            }
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvDailrReport.DataSource = objDs.Tables[0];
                gvDailrReport.DataBind();


                decimal TotalPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaymentAmount"));

                gvDailrReport.FooterRow.Cells[1].Text = "Total's";
                gvDailrReport.FooterRow.Cells[1].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].Text = "R" + TotalPrice.ToString("N2");
                gvDailrReport.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
                gvDailrReport.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                gvDailrReport.DataSource = null;
                gvDailrReport.DataBind();
            }
            Cache.Remove("DailPaymentReports");
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gvDailrReport.AllowPaging = false;
                    BindReport();

                    gvDailrReport.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SalesReport_" + txtStartDate.Text + "_" + txtTodate.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    protected void gvDailrReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string ApprovedOn = gvDailrReport.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvModeOfPayment = e.Row.FindControl("gvModeOfPayment") as GridView;
            DataTable dt = GetPaymentMode(ApprovedOn);
            if (dt.Rows.Count > 0)
            {
                gvModeOfPayment.DataSource = dt;
                gvModeOfPayment.DataBind();

                decimal TotalPrice = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentAmount"));

                gvModeOfPayment.FooterRow.Cells[0].Text = "Total's";
                gvModeOfPayment.FooterRow.Cells[0].Font.Bold = true;
                gvModeOfPayment.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                //  gvModeOfPayment.FooterRow.Cells[0].ForeColor = System.Drawing.Color.White;
                gvModeOfPayment.FooterRow.Cells[1].Text = "R" + TotalPrice.ToString("N2");
                gvModeOfPayment.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvModeOfPayment.FooterRow.Cells[1].Font.Bold = true;
                // gvModeOfPayment.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                gvModeOfPayment.DataSource = null;
                gvModeOfPayment.DataBind();
            }


        }
    }
    public DataTable GetPaymentMode(string Date)
    {
        DataRow[] foundRows = null;
        DataTable dt = new DataTable();
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("PaymentAmount", typeof(decimal));
        dt.Columns.Add("curPaymentAmount", typeof(string));

        if (Cache["DailPaymentReports"] != null)
        {
            DataSet objds = (DataSet)(Cache["DailPaymentReports"]);
            foundRows = objds.Tables[1].Select("PaymentApprovedOn='" + Date + "'");
            foreach (DataRow dr in foundRows)
            {
                dt.Rows.Add(dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        return dt;
    }
    public void SetCacheDataset(DataSet objDs)
    {


        if (Cache["DailPaymentReports"] == null)
        {
            DataSet result = objDs;
            Cache["DailPaymentReports"] = (result);
        }

    }
}