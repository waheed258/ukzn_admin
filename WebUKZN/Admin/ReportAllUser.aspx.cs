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

public partial class Admin_ReportAllUser : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
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
    private void BindReport()
    {
        try
        {

            Cache.Remove("DailRolePaymentReports");
            DataSet objDs = _objBORpeort.GetAllUserWiseReports(txtStartDate.Text, txtTodate.Text);

            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvDailrReport.DataSource = objDs.Tables[0];
                gvDailrReport.DataBind();

                decimal TotalPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaymentAmount"));

                gvDailrReport.FooterRow.Cells[1].Text = "Total's";
                gvDailrReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].Text = TotalPrice.ToString("N2");
            }
            else
            {
                gvDailrReport.DataSource = null;
                gvDailrReport.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    public DataTable GetPaymentMode(string LoginId)
    {
        DataRow[] foundRows = null;
        DataTable dt = new DataTable();
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("PaymentAmount", typeof(decimal));

        if (Cache["DailUserPaymentReports"] != null)
        {
            DataSet objds = (DataSet)(Cache["DailUserPaymentReports"]);
            foundRows = objds.Tables[1].Select("UserLoginId='" + LoginId + "'");
            foreach (DataRow dr in foundRows)
            {
                dt.Rows.Add(dr[3].ToString(), dr[2].ToString());
            }
        }

        return dt;
    }

}