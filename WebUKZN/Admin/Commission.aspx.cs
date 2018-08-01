using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using EntityManager;
using System.Configuration;

public partial class Admin_Commission : System.Web.UI.Page
{
    private BALUser _objBalUser = new BALUser();
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");

        }
        if (!IsPostBack)
        {

            BindProvince();
            txtVatPer.Text = ConfigurationManager.AppSettings["VatPercentage"].ToString();
        }
    }

    private void Calculate()
    {
        try
        {
            decimal vatFee = 0;
            vatFee = (Convert.ToDecimal(txtServiceFee.Text) * Convert.ToDecimal(txtVatPer.Text) / 100);
            txtTotalFee.Text = decimal.Round((Convert.ToDecimal(txtServiceFee.Text) - vatFee), 2).ToString();
            txtVatFee.Text = decimal.Round(vatFee, 2).ToString();
        }
        catch (Exception ex)
        {
            txtTotalFee.Text = "0";
        }
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
            objServiceFeeMaster.BranchId = Convert.ToInt32(ddlBranch.SelectedValue);
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
    private void GetServiceFeeAmount(int BranchId)
    {
        try
        {
            if (BranchId != 0)
            {
                DataSet objDs = _objBALUserManager.GetServiceAmountByBranch(BranchId, Convert.ToInt32(Session["role_id"]), Convert.ToInt32(Session["loginId"]));
                if (objDs.Tables[0].Rows.Count > 0)
                {
                    txtTotalFee.Text = objDs.Tables[0].Rows[0]["ServiceFeeAmount"].ToString();
                    txtVatFee.Text = objDs.Tables[0].Rows[0]["vatAmount"].ToString();
                    txtServiceFee.Text = objDs.Tables[0].Rows[0]["TotalServiceFeeAmount"].ToString();
                }
                else
                {
                    txtTotalFee.Text = "0";
                    txtVatFee.Text = "0";
                    txtServiceFee.Text = "0";
                }
            }
            else
            {
                txtTotalFee.Text = "0";
            }
        }
        catch (Exception ex)
        {
            labelError.Text = ex.Message;
        }
    }

    private void BindProvince()
    {
        try
        {
            DataSet objDs = _objBalUser.GetProvince();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlProvince.DataSource = objDs;
                ddlProvince.DataValueField = "ProvinceId";
                ddlProvince.DataTextField = "ProvinceName";
                ddlProvince.DataBind();
                ddlProvince.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlProvince.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void Bindcities(int ProvinceId)
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlCities.Items.Clear();
            if (ProvinceId == 0)
            {
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
                return;
            }

            DataSet objDs = _objBalUser.GetCities(ProvinceId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlCities.DataSource = objDs;
                ddlCities.DataValueField = "CityId";
                ddlCities.DataTextField = "CityName";
                ddlCities.DataBind();
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void BindBranchs(int ProvinceId, int CitiesId)
    {
        try
        {
            ddlBranch.Items.Clear();
            if (ProvinceId == 0 || CitiesId == 0)
            {
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
                return;
            }

            DataSet objDs = _objBalUser.GetBranchs(ProvinceId, CitiesId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = objDs;
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }


    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {

        int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
        Bindcities(ProvinceId);

    }
    protected void ddlCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
        int CotiesId = Convert.ToInt32(ddlCities.SelectedValue);
        BindBranchs(ProvinceId, CotiesId);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetServiceFeeAmount(Convert.ToInt32(ddlBranch.SelectedValue));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlProvince.SelectedIndex = 0;
            ddlCities.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtTotalFee.Text = "0";
            txtServiceFee.Text = "0";
            txtVatFee.Text = "0";
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
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