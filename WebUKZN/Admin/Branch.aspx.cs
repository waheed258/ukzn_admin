using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Branch : System.Web.UI.Page
{
    EMBranch _ObjEmBranch = new EMBranch();
    BALBranch _objBalBranch = new BALBranch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["BranchId"]))
            {
                int BranchMasterId = Convert.ToInt32(Request.QueryString["BranchId"].ToString());
                GetBranchDetails(BranchMasterId);
                btnBranchSave.Text = "Update";
            }
        }
    }

    protected void btnBranchSave_Click(object sender, EventArgs e)
    {

        try
        {
            _ObjEmBranch.BranchName = string.IsNullOrEmpty(txtBranchName.Text) ? "" : txtBranchName.Text;
            _ObjEmBranch.BranchAddress = string.IsNullOrEmpty(txtBranchAddress.Text) ? "" : txtBranchAddress.Text;
            _ObjEmBranch.BranchEmailId = string.IsNullOrEmpty(txtBranchEmail.Text) ? "" : txtBranchEmail.Text;
            _ObjEmBranch.BranchMobileNo = string.IsNullOrEmpty(txtBranchMobile.Text) ? "" : txtBranchMobile.Text;

            _ObjEmBranch.branchStatus = string.IsNullOrEmpty(ddlBranchStatus.SelectedItem.Value.ToString()) ? 0 : Convert.ToInt32(ddlBranchStatus.SelectedItem.Value);
            // _ObjEmBranch.UserCompanyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
            _ObjEmBranch.BranchContactPerson = string.IsNullOrEmpty(txtBranchContactPerson.Text) ? "" : txtBranchContactPerson.Text;
            // ObjEmUser.CreatedBy = Convert.ToInt32(Session["UserMasterId"].ToString());
            _ObjEmBranch.CreatedBy = Convert.ToInt32(Session["UserMasterId"].ToString());
            _ObjEmBranch.BranchId = Convert.ToInt32(hf_BranchId.Value);

            int branchInsertresult = _objBalBranch.InsertUpdateBranch(_ObjEmBranch);
            if (branchInsertresult > 0)
            {
                Response.Redirect("BranchList.aspx");
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);

        }

    }
    protected void btnBranchCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchList.aspx");
    }
    #region PrivateMethods
    private void GetBranchDetails(int BranchMasterId)
    {
        try
        {
            DataSet BranchList = _objBalBranch.GetBranchList(BranchMasterId);
            if (BranchList.Tables[0].Rows.Count > 0)
            {
                hf_BranchId.Value = BranchList.Tables[0].Rows[0]["BranchId"].ToString();
                txtBranchName.Text = BranchList.Tables[0].Rows[0]["BranchName"].ToString();
                txtBranchEmail.Text = BranchList.Tables[0].Rows[0]["BranchEmailId"].ToString();
                txtBranchMobile.Text = BranchList.Tables[0].Rows[0]["BranchMobileNo"].ToString();
                txtBranchAddress.Text = BranchList.Tables[0].Rows[0]["BranchAddress"].ToString();
                ddlBranchStatus.SelectedIndex = ddlBranchStatus.Items.IndexOf(ddlBranchStatus.Items.FindByValue(BranchList.Tables[0].Rows[0]["BranchStatus"].ToString()));
                txtBranchContactPerson.Text = BranchList.Tables[0].Rows[0]["BranchContactPerson"].ToString();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }
    #endregion
}