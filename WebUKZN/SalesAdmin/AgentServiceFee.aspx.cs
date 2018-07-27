using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AgentServiceFee : System.Web.UI.Page
{
    private BALUser _objBalUser = new BALUser();
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
        if (!IsPostBack)
        {
            GetServiceFeeAmount();
            txtVatPer.Text = ConfigurationManager.AppSettings["VatPercentage"].ToString();
        }
    }
    private void Calculate()
    {
        decimal vatFee = 0;
        vatFee = (Convert.ToDecimal(txtServiceFee.Text) * Convert.ToDecimal(txtVatPer.Text) / 100);
        txtTotalFee.Text = decimal.Round((Convert.ToDecimal(txtServiceFee.Text) - vatFee), 2).ToString();
        txtVatFee.Text = decimal.Round(vatFee, 2).ToString();
    }
    private void InsertUpdateCommission()
    {
        ServiceFeeMaster objServiceFeeMaster = new ServiceFeeMaster();
        try
        {
            objServiceFeeMaster.ServiceFeeId = 0;
            objServiceFeeMaster.Int_Comm_Per = 0;
            objServiceFeeMaster.Int_Comm_Rate = 0;
            objServiceFeeMaster.Dom_Comm_Per = 0;
            objServiceFeeMaster.Dom_Comm_Rate = 0;
            objServiceFeeMaster.BranchId = 0;
            objServiceFeeMaster.Comm_Active_type = 0;

            objServiceFeeMaster.CreatedBy = Convert.ToInt32(Session["loginId"]);
            objServiceFeeMaster.RoleId = Convert.ToInt32(Session["role_id"]);

            objServiceFeeMaster.ServiceFeeAmount = Convert.ToDecimal(txtTotalFee.Text);
            objServiceFeeMaster.vatAmount = Convert.ToDecimal(txtVatFee.Text);
            objServiceFeeMaster.vatper = Convert.ToDecimal(txtVatPer.Text);
            objServiceFeeMaster.TotalServiceFeeAmount = objServiceFeeMaster.vatAmount + objServiceFeeMaster.ServiceFeeAmount;

            int nResult = _objBALUserManager.InsertUpdateCommissionsMaster(objServiceFeeMaster);
            if (nResult > 0)
            {
                Calculate();
                Session["ServiceFee"] = txtTotalFee.Text;
                Session["VatAmount"] = txtVatFee.Text;
                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Action Completed.");

            }
            else
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("Info", "Info", "Pleae try again.");
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void GetServiceFeeAmount()
    {
        try
        {

            DataSet objDs = _objBALUserManager.GetServiceAmountByBranch(0, 3, Convert.ToInt32(Session["loginId"]));
            if (objDs.Tables[0].Rows.Count > 0)
            {
                txtTotalFee.Text = objDs.Tables[0].Rows[0]["TotalServiceFeeAmount"].ToString();
                txtVatFee.Text = objDs.Tables[0].Rows[0]["vatAmount"].ToString();
                txtServiceFee.Text = (Convert.ToDecimal(txtTotalFee.Text) + Convert.ToDecimal(txtVatFee.Text)).ToString();
            }
            else
            {
                txtTotalFee.Text = "0";
                txtVatFee.Text = "0";
                txtServiceFee.Text = "0";
            }

        }
        catch (Exception ex)
        {
            labelError.Text = ex.Message;
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCommission();
    }
    protected void txtServiceFee_TextChanged(object sender, EventArgs e)
    {
        Calculate();
    }
}