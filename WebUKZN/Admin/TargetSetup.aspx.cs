using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using EntityManager;
public partial class Admin_TargetSetup : System.Web.UI.Page
{
    BALUserManager _objBALUserManager = new BALUserManager();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        if (Request.QueryString["stfid"] != null)
        {
            UserId = Convert.ToInt32(Request.QueryString["stfid"]);
        }
        if (!IsPostBack)
        {
            ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(DateTime.Today.Month.ToString()));
            BindYears();


            GetTargetDetailsByUserId(UserId);
        }


    }
    #region PrivateMethods
    private void BindYears()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 0; i <= 5; i++)
        {
            ddlYear.Items.Add((currentYear - i).ToString());
        }
    }
    private void GetTargetDetailsByUserId(int UserId)
    {
        try
        {
            DataSet objDs = _objBALUserManager.GetTargetSetup(0, UserId);
            if (objDs.Tables.Count > 0)
            {
                if (objDs.Tables[0].Rows.Count > 0)
                {
                    gdvData.DataSource = objDs.Tables[0];
                    gdvData.DataBind();
                }
                else
                {
                    gdvData.DataSource = null;
                    gdvData.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void GetTargetDetailsByTargetId(int TargetId)
    {
        try
        {
            DataSet objDs = _objBALUserManager.GetTargetSetup(TargetId, 0);
            if (objDs.Tables.Count > 0)
            {
                if (objDs.Tables[0].Rows.Count > 0)
                {
                    txtTargetAmount.Text = objDs.Tables[0].Rows[0]["TargetAmount"].ToString();
                    txtCompletedAmount.Text = objDs.Tables[0].Rows[0]["CompletedAmount"].ToString();
                    ddlMonth.SelectedIndex = ddlMonth.Items.IndexOf(ddlMonth.Items.FindByValue(objDs.Tables[0].Rows[0]["TargetMonth"].ToString()));
                    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(objDs.Tables[0].Rows[0]["TargetYear"].ToString()));
                    txtTargetAmount.Text = objDs.Tables[0].Rows[0]["PendingAmount"].ToString();
                    hfTargetId.Value = objDs.Tables[0].Rows[0]["TargetId"].ToString();
                }
                else
                {
                    txtTargetAmount.Text = "0";
                    txtCompletedAmount.Text = "0";
                    txtPendingAmount.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void ClearControls()
    {
        try
        {
            txtTargetAmount.Text = "0";
            txtCompletedAmount.Text = "0";
            txtPendingAmount.Text = "0";
            ddlYear.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            hfTargetId.Value = "0";
        }
        catch (Exception ex)
        {

        }
    }
    private void InsertUpdateTarget(int UserLoginId)
    {
        try
        {

            TargetSetupMaster objTargetSetupMaster = new TargetSetupMaster();
            objTargetSetupMaster.TargetId = Convert.ToInt32(hfTargetId.Value);
            objTargetSetupMaster.UserLoginId = UserLoginId;
            objTargetSetupMaster.TargetAmount = Convert.ToDecimal(txtTargetAmount.Text);
            objTargetSetupMaster.PendingAmount = Convert.ToDecimal(txtTargetAmount.Text) - Convert.ToDecimal(txtCompletedAmount.Text);
            objTargetSetupMaster.CompletedAmount = Convert.ToDecimal(txtTargetAmount.Text) - objTargetSetupMaster.PendingAmount;

            objTargetSetupMaster.TargetCompletePer = _objBOUtiltiy.ClaulatePercentage(objTargetSetupMaster.CompletedAmount, objTargetSetupMaster.TargetAmount);
            objTargetSetupMaster.TargetPendingPer = _objBOUtiltiy.ClaulatePercentage(objTargetSetupMaster.PendingAmount, objTargetSetupMaster.TargetAmount);
            objTargetSetupMaster.TargetStatus = 1;
            objTargetSetupMaster.TargetMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            objTargetSetupMaster.TargetYear = Convert.ToInt32(ddlYear.SelectedValue);
            int nResult = _objBALUserManager.InsertUpdateTargetSetup(objTargetSetupMaster);
            if (nResult > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Action Completed");
                GetTargetDetailsByUserId(UserLoginId);
                ClearControls();
            }
            else if (nResult == -1)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info", "Sorry This combination alredy have a information.");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info", "Please try again");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    #endregion PrivateMethods
    protected void gdvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gdvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int TargetId = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "EditTarget")
        {
            GetTargetDetailsByTargetId(TargetId);
        }
        else
        {
            _objBALUserManager.UpdateTargetStatus(TargetId, 2);
            GetTargetDetailsByUserId(UserId);
            ClearControls();
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {

        InsertUpdateTarget(UserId);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
}