using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uAPIClassLib;
using uAPIClassLib.AirReference;
using uAPIClassLib.UniversalReference;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using BusinessManager;
using System.Xml;
using Utilitys;
using System.Data;
using System.Configuration;
using EntityManager;
public partial class Admin_GetQuotationApprovalStatus : System.Web.UI.Page
{
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindQuotations();
        }
    }
    protected void gdvFlightQuotations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string QutRefNo = e.CommandArgument.ToString();
        if (e.CommandName == "ViewDetails")
        {
            GridViewRow gvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            if (gvRow != null)
            {
                string strUrl = string.Empty;
                Label lblUkznOrderNo = (Label)gvRow.FindControl("lblUkznOrderNo");
                HiddenField hfApproval_Level = (HiddenField)gvRow.FindControl("hfApproval_Level");
                if (hfApproval_Level.Value == "1")
                    strUrl = "../CustomerSupport/SecondApproval.aspx?crefdqk=" + QutRefNo;
                else if (hfApproval_Level.Value == "4")
                    strUrl = "../CustomerSupport/LastApproval.aspx?crefdqk=" + QutRefNo;
                else
                    strUrl = "../CustomerSupport/AccApproval.aspx?crefdqk=" + QutRefNo;

                Response.Redirect(strUrl);
            }
        }
        else
        {
            string url = "ApprovalHistoryLog.aspx?cqert=" + QutRefNo;
            //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Response.Redirect(url);
        }
    }
    #region PrivateMethods
    private void BindQuotations()
    {
        try
        {
            DataSet objDs = objBAFlightSearch.GetAllQuotationMasterApprovalStatusToStaff(Session["ukzn_staff"].ToString(), "");
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvFlightQuotations.DataSource = objDs;
                gdvFlightQuotations.DataBind();
            }
            else
            {
                gdvFlightQuotations.DataSource = null;
                gdvFlightQuotations.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    //private void BindQuotationByCustomerDetails()
    //{
    //    try
    //    {
    //        int CreatedBy = Convert.ToInt32(Session["loginId"]);
    //        int RoleId = Convert.ToInt32(Session["role_id"]);
    //        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
    //        DataSet objDs = objBAFlightSearch.GetAllQuotationMasterByQuotationRefNo(RoleId, CompanyId, CreatedBy, 1, txtQuotationRef.Text);
    //        if (objDs.Tables[0].Rows.Count > 0)
    //        {
    //            gdvFlightQuotations.DataSource = objDs;
    //            gdvFlightQuotations.DataBind();
    //        }
    //        else
    //        {
    //            gdvFlightQuotations.DataSource = null;
    //            gdvFlightQuotations.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
    //    }
    //}
    #endregion PrivateMEthods

    //protected void cmdSubmit_Click(object sender, System.EventArgs e)
    //{
    //    BindQuotationByCustomerDetails();
    //}
}