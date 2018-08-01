using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ApprovalHistoryLog : System.Web.UI.Page
{
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["cqert"]))
            {
                int QuotationId = Convert.ToInt32(Request.QueryString["cqert"]);
                BindQuotations(QuotationId);
            }
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

    }
    #region PrivateMethods
    private void BindQuotations(int QuotationMasterId)
    {
        try
        {
            string ApprovalStaff = string.Empty;
            string StaffNo = string.Empty;
            if (Session["EmpCategory"].ToString() != "REQ")
            {
                ApprovalStaff = Session["ukzn_staff"].ToString();
            }
            else
            {
                StaffNo = Session["ukzn_staff"].ToString();
            }
            DataSet objDs = objBAFlightSearch.GetAllQuotationMasterApprovalStatusToStaffHistory(ApprovalStaff, StaffNo, QuotationMasterId);
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

    #endregion PrivateMEthods
}