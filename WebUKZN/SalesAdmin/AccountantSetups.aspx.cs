using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AccountantSetups : System.Web.UI.Page
{
    BOUKZN _objBOUKZN = new BOUKZN();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");

        }
        if (!IsPostBack)
        {
            GetAccountOverRide();
        }
    }

    #region PrivateMethods
    private void InsertUpdateAccountRejection()
    {
        try
        {
            int Id = Convert.ToInt32(hfaccountoverride.Value);
            decimal Amount = Convert.ToDecimal(txtBudgetOverRide.Text);
            int nResult = _objBOUKZN.InsertUpdateAccountOverRide(Id, Session["ukzn_staff"].ToString(), Convert.ToInt32(Session["loginId"]), Amount);
            if (nResult > 0)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Info ", "Action Completed");
            }
            else
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Info ", "Action failed");
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("success", "Info ", ex.Message);
        }
    }
    private void GetAccountOverRide()
    {
        try
        {
            DataSet objDs = _objBOUKZN.GetAccountOverRide(Session["ukzn_staff"].ToString());
            if (objDs.Tables[0].Rows.Count > 0)
            {
                hfaccountoverride.Value = objDs.Tables[0].Rows[0]["accountoverride"].ToString();
                txtBudgetOverRide.Text = objDs.Tables[0].Rows[0]["accountamount"].ToString();
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("success", "Info ", ex.Message);
        }
    }
    #endregion PrivateMethods
    protected void btnUpdateFlightDoc_Click(object sender, EventArgs e)
    {
        InsertUpdateAccountRejection();
    }
}