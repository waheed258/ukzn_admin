using BusinessManager;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AirLineReports : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    string strCurrencycode = "R";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        strCurrencycode = _objBOUtiltiy.Currencycode();
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
                    gvAirLineReport.AllowPaging = false;

                    BindReport();
                    gvAirLineReport.RenderControl(hw);
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
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBORpeort.GetAirlineReport(txtStartDate.Text, txtTodate.Text, Convert.ToInt32(Session["loginId"]), RoleId, CompanyId);

            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvAirLineReport.DataSource = objDs.Tables[0];
                gvAirLineReport.DataBind();
                decimal ExlPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalPrice"));
                gvAirLineReport.FooterRow.Cells[2].Text = "Total's";
                gvAirLineReport.FooterRow.Cells[2].Font.Bold = true;
                gvAirLineReport.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvAirLineReport.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;


                gvAirLineReport.FooterRow.Cells[3].Text = strCurrencycode + " " + ExlPrice.ToString("N2");
                gvAirLineReport.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                gvAirLineReport.FooterRow.Cells[3].Font.Bold = true;
                gvAirLineReport.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;

            }
            else
            {
                gvAirLineReport.DataSource = null;
                gvAirLineReport.DataBind();
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
}