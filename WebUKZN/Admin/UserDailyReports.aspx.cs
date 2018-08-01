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

public partial class Admin_UserDailyReports : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    BALUserManager _objBALUserManager = new BALUserManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            BindRolesDropDwon();
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
            string UserId = "0";
            string UserRole = ddlRole.SelectedValue;
            Cache.Remove("DailUserPaymentReports");
            DataSet objDs = _objBORpeort.UserSalesReports(txtStartDate.Text, txtTodate.Text, UserId, UserRole);
            if (objDs.Tables[1].Rows.Count > 0)
            {
                SetCacheDataset(objDs);
            }
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvDailrReport.DataSource = objDs.Tables[0];
                gvDailrReport.DataBind();

                decimal TotalPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaymentAmount"));

                gvDailrReport.FooterRow.Cells[2].Text = "Total's";
                gvDailrReport.FooterRow.Cells[2].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                gvDailrReport.FooterRow.Cells[3].Text = "R" + TotalPrice.ToString("N2");
                gvDailrReport.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[3].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;

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
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }
    public DataTable GetPaymentMode(string LoginId)
    {
        DataRow[] foundRows = null;
        DataTable dt = new DataTable();
        dt.Columns.Add("PaymentType", typeof(string));
        dt.Columns.Add("PaymentAmount", typeof(decimal));
        dt.Columns.Add("curPaymentAmount", typeof(string));
        if (Cache["DailUserPaymentReports"] != null)
        {
            DataSet objds = (DataSet)(Cache["DailUserPaymentReports"]);
            foundRows = objds.Tables[1].Select("PaymentApprovedOn='" + LoginId + "'");
            foreach (DataRow dr in foundRows)
            {
                dt.Rows.Add(dr[2].ToString(), dr[1].ToString(), dr[4].ToString());
            }
        }

        return dt;
    }
    private void BindRolesDropDwon()
    {
        try
        {
            ddlRole.Items.Clear();
            DataSet objDs = _objBALUserManager.GetRoleMaster(0);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlRole.DataSource = objDs;
                ddlRole.DataValueField = "role_id";
                ddlRole.DataTextField = "role_desc";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Role", "0"));
            }
            else
            {
                ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Role", "0"));
            }
        }
        catch
        {
            //throw;
        }
    }

    public void SetCacheDataset(DataSet objDs)
    {


        if (Cache["DailUserPaymentReports"] == null)
        {
            DataSet result = objDs;
            Cache["DailUserPaymentReports"] = (result);
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
}