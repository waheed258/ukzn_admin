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

public partial class Admin_DailyBranchReports : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    BALUser _objBalUser = new BALUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindBranch();

            txtStartDate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            BindReport();
        }
    }
    private void BindReport()
    {
        try
        {
            string BranchId = ddlBranch.SelectedValue;
            Cache.Remove("DailBranchPaymentReports");
            DataSet objDs = _objBORpeort.BranchDailyReports(txtStartDate.Text, txtTodate.Text, BranchId);
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
                gvDailrReport.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
                gvDailrReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].Text = "R" + TotalPrice.ToString("N2");
                gvDailrReport.FooterRow.Cells[2].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
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
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {

        BindReport();
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
                gvModeOfPayment.FooterRow.Cells[1].Text = "R" + TotalPrice.ToString("N2");
                gvModeOfPayment.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvModeOfPayment.FooterRow.Cells[1].Font.Bold = true;
            }
            else
            {
                gvModeOfPayment.DataSource = null;
                gvModeOfPayment.DataBind();
            }


        }
    }
    public DataTable GetPaymentMode(string BranchId)
    {
        DataRow[] foundRows = null;
        DataTable dt = new DataTable();
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("PaymentAmount", typeof(decimal));
        dt.Columns.Add("curPaymentAmount", typeof(string));

        if (Cache["DailBranchPaymentReports"] != null)
        {
            DataSet objds = (DataSet)(Cache["DailBranchPaymentReports"]);
            foundRows = objds.Tables[1].Select("BranchId='" + BranchId + "'");
            foreach (DataRow dr in foundRows)
            {
                dt.Rows.Add(dr[2].ToString(), dr[1].ToString(), dr[3].ToString());
            }
        }

        return dt;
    }
    private void BindBranch()
    {
        try
        {
            ddlBranch.Items.Clear();
            DataSet objDs = _objBalUser.GetAllBranchs();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = objDs;
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
            else
            {
                ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    public void SetCacheDataset(DataSet objDs)
    {


        if (Cache["DailBranchPaymentReports"] == null)
        {
            DataSet result = objDs;
            Cache["DailBranchPaymentReports"] = (result);
        }

    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindReport();
    }
}