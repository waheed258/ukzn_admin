using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.html.simpleparser;

public partial class SalesAdmin_DailyDetailReport : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    BALUserManager _objBALUserManager = new BALUserManager();
    string strCurrencycode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
        }
        strCurrencycode = _objBOUtiltiy.Currencycode();
        if (!IsPostBack)
        {
            BindRolesDropDwon();
            txtStartDate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Users", "0"));
            BindReport(0);

        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport(Convert.ToInt32(ddlUser.SelectedValue));
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
            throw;
        }
    }
    private void BindUser(int RoleId)
    {
        try
        {
            ddlUser.Items.Clear();
            if (RoleId == 0)
            {

                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Users", "0"));
                return;
            }
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBALUserManager.GetUserDetailsByRole(RoleId, CompanyId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlUser.DataSource = objDs;
                ddlUser.DataValueField = "UserLoginId";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Users", "0"));
            }
            else
            {
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Users", "0"));
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindReport(int UserId)
    {
        try
        {


            DataSet objDs = _objBORpeort.GetDailyDetailReport(txtStartDate.Text, txtTodate.Text, UserId, Convert.ToInt32(Session["role_id"]), Convert.ToInt32(Session["CompanyId"]));

            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvDailrReport.DataSource = objDs.Tables[0];
                gvDailrReport.DataBind();

                decimal ExlPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SourceFee"));
                decimal ServiceFee = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ServiceFee"));
                decimal VatFee = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("VatFee"));
                decimal TotalPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Total"));

                gvDailrReport.FooterRow.Cells[5].Text = "Total's";
                gvDailrReport.FooterRow.Cells[5].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;



                gvDailrReport.FooterRow.Cells[6].Text = strCurrencycode + " " + ExlPrice.ToString("N2");
                gvDailrReport.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[6].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[6].ForeColor = System.Drawing.Color.White;


                gvDailrReport.FooterRow.Cells[7].Text = strCurrencycode + " " + ServiceFee.ToString("N2");
                gvDailrReport.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[7].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;

                gvDailrReport.FooterRow.Cells[8].Text = strCurrencycode + " " + VatFee.ToString("N2");
                gvDailrReport.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[8].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[8].ForeColor = System.Drawing.Color.White;

                gvDailrReport.FooterRow.Cells[9].Text = strCurrencycode + " " + TotalPrice.ToString("N2");
                gvDailrReport.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                gvDailrReport.FooterRow.Cells[9].Font.Bold = true;
                gvDailrReport.FooterRow.Cells[9].ForeColor = System.Drawing.Color.White;

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
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser(Convert.ToInt32(ddlRole.SelectedValue));
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindReport(Convert.ToInt32(ddlUser.SelectedValue));
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

                    BindReport(Convert.ToInt32(ddlUser.SelectedValue));
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        string FileNo = (sender as LinkButton).CommandArgument;

        string url = "ViewFileDetails.aspx?FileNo=" + FileNo;
        string s = "window.open('" + url + "', 'popup_window', 'width=900,height=1000,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}