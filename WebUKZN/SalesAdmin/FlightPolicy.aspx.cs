using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using EntityManager;
public partial class SalesAdmin_FlightPolicy : System.Web.UI.Page
{
    private BAFlightSearch objBaFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("~/SalesLogin.aspx");
        }
        if (!IsPostBack)
        {
            BindAirLineList();
            BindCabinClassGrids();
            BindHotelPolicyGrid();
            BindCabPolicyGrid();
        }
    }
    #region PrivateMethods
    private void BindAirLineList()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["loginId"]);
            int BranchId = Convert.ToInt32(Session["BranchId"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);

            DataSet objDs = objBaFlightSearch.GetAllAirLineListMarkupMasterEmpCategory(CompanyId, BranchId, UserId, 1, "ALL", ddlEmployeeCategory.SelectedValue);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvAirLineCodes.DataSource = objDs.Tables[0];
                gdvAirLineCodes.DataBind();
            }
            else
            {
                gdvAirLineCodes.DataSource = null;
                gdvAirLineCodes.DataBind();
            }
            if (objDs.Tables[1].Rows.Count > 0)
            {
                gdvFlightMarkUp.DataSource = objDs.Tables[1];
                gdvFlightMarkUp.DataBind();
            }
            else
            {
                gdvFlightMarkUp.DataSource = null;
                gdvFlightMarkUp.DataBind();
            }
            if (objDs.Tables[3].Rows.Count > 0)
            {
                hfAllAirlineMarkupId.Value = objDs.Tables[3].Rows[0]["MarkUpId"].ToString();
                txtAllAirLineMarkupValue.Text = objDs.Tables[3].Rows[0]["MarkUpValue"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void MoveRight()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["loginId"]);
            int BranchId = Convert.ToInt32(Session["BranchId"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            foreach (GridViewRow row in gdvAirLineCodes.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;
                HiddenField hf = row.Cells[0].Controls[3] as HiddenField;
                if (chk.Checked == true)
                {
                    FlightMarkUpMaster ojbFlightMarkUpMaster = new FlightMarkUpMaster();
                    ojbFlightMarkUpMaster.MarkUpId = 0;
                    ojbFlightMarkUpMaster.AirLineCode = hf.Value;
                    ojbFlightMarkUpMaster.MarkUpType = 1;// Fixed On Percentage
                    ojbFlightMarkUpMaster.MarkUpValue = 0;
                    ojbFlightMarkUpMaster.MarkUpTripType = 1; // International or Domastic
                    ojbFlightMarkUpMaster.MarkUpOn = 1; // Class Or AirLine Code
                    ojbFlightMarkUpMaster.ClassCode = "";
                    ojbFlightMarkUpMaster.SegmentLevel = false;
                    ojbFlightMarkUpMaster.PassengerLevel = false;
                    ojbFlightMarkUpMaster.CreatedBy = UserId;
                    ojbFlightMarkUpMaster.CompanyId = CompanyId;
                    ojbFlightMarkUpMaster.BranchId = BranchId;
                    ojbFlightMarkUpMaster.UserId = UserId;
                    ojbFlightMarkUpMaster.Status = 1;
                    ojbFlightMarkUpMaster.EmployeeCategory = ddlEmployeeCategory.SelectedValue;
                    objBaFlightSearch.InsertUpdateMarkUpSetup(ojbFlightMarkUpMaster);
                }

            }
            BindAirLineList();
        }
        catch (Exception ex)
        {

        }
    }
    private void MoveLeft()
    {
        try
        {

            foreach (GridViewRow row in gdvFlightMarkUp.Rows)
            {
                HiddenField hfMarkUpId = row.Cells[1].Controls[1] as HiddenField;
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;
                if (chk.Checked == true)
                {
                    int MarkUpId = Convert.ToInt32(hfMarkUpId.Value);
                    int Status = 2;
                    objBaFlightSearch.UpdateMarkUpSetupStatus(MarkUpId, Status);
                }
            }
            BindAirLineList();
        }
        catch (Exception ex)
        {

        }
    }
    private void UpdateMarkUpValues()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["loginId"]);
            int BranchId = Convert.ToInt32(Session["BranchId"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);



            FlightMarkUpMaster ojbFlightMarkUpMasterAll = new FlightMarkUpMaster();
            ojbFlightMarkUpMasterAll.MarkUpId = Convert.ToInt32(hfAllAirlineMarkupId.Value);
            ojbFlightMarkUpMasterAll.AirLineCode = "ALL";
            ojbFlightMarkUpMasterAll.MarkUpType = 1;// Fixed On Percentage
            ojbFlightMarkUpMasterAll.MarkUpValue = Convert.ToDecimal(txtAllAirLineMarkupValue.Text);
            ojbFlightMarkUpMasterAll.MarkUpTripType = 1; // International or Domastic
            ojbFlightMarkUpMasterAll.MarkUpOn = 1; // Class Or AirLine Code
            ojbFlightMarkUpMasterAll.ClassCode = "";
            ojbFlightMarkUpMasterAll.SegmentLevel = false;
            ojbFlightMarkUpMasterAll.PassengerLevel = false;
            ojbFlightMarkUpMasterAll.CreatedBy = UserId;
            ojbFlightMarkUpMasterAll.CompanyId = CompanyId;
            ojbFlightMarkUpMasterAll.BranchId = BranchId;
            ojbFlightMarkUpMasterAll.UserId = UserId;
            ojbFlightMarkUpMasterAll.Status = 1;
            ojbFlightMarkUpMasterAll.EmployeeCategory = ddlEmployeeCategory.SelectedValue;
            objBaFlightSearch.InsertUpdateMarkUpSetup(ojbFlightMarkUpMasterAll);


            foreach (GridViewRow row in gdvFlightMarkUp.Rows)
            {
                HiddenField hfMarkUpId = row.Cells[1].Controls[1] as HiddenField;
                HiddenField hfAirLinecode = row.Cells[1].Controls[3] as HiddenField;
                TextBox txtMarkupValue = row.Cells[3].Controls[1] as TextBox;
                DropDownList ddlMarkuptype = row.Cells[2].Controls[1] as DropDownList;

                FlightMarkUpMaster ojbFlightMarkUpMaster = new FlightMarkUpMaster();
                ojbFlightMarkUpMaster.MarkUpId = Convert.ToInt32(hfMarkUpId.Value);
                ojbFlightMarkUpMaster.AirLineCode = hfAirLinecode.Value;
                ojbFlightMarkUpMaster.MarkUpType = Convert.ToInt32(ddlMarkuptype.SelectedValue);// Fixed On Percentage
                ojbFlightMarkUpMaster.MarkUpValue = Convert.ToDecimal(txtMarkupValue.Text);
                ojbFlightMarkUpMaster.MarkUpTripType = 1; // International or Domastic
                ojbFlightMarkUpMaster.MarkUpOn = 1; // Class Or AirLine Code
                ojbFlightMarkUpMaster.ClassCode = "";
                ojbFlightMarkUpMaster.SegmentLevel = false;
                ojbFlightMarkUpMaster.PassengerLevel = false;
                ojbFlightMarkUpMaster.CreatedBy = UserId;
                ojbFlightMarkUpMaster.CompanyId = CompanyId;
                ojbFlightMarkUpMaster.BranchId = BranchId;
                ojbFlightMarkUpMaster.UserId = UserId;
                ojbFlightMarkUpMaster.Status = 1;
                ojbFlightMarkUpMaster.EmployeeCategory = ddlEmployeeCategory.SelectedValue;
                objBaFlightSearch.InsertUpdateMarkUpSetup(ojbFlightMarkUpMaster);

            }
            BindAirLineList();
        }
        catch (Exception ex)
        {

        }
    }
    #endregion PrivateMethods
    protected void ddlEmployeeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAirLineList();
        BindCabinClassGrids();
        BindHotelPolicyGrid();
        BindCabPolicyGrid();
    }
    protected void bntMoveRight_Click(object sender, ImageClickEventArgs e)
    {
        MoveRight();
    }
    protected void imgMoveLeft_Click(object sender, ImageClickEventArgs e)
    {
        MoveLeft();
    }
    #region CabinClassPolicy
    protected void imgPolicyCabinMoveRight_Click(object sender, ImageClickEventArgs e)
    {
        InsertCabinclassPolicy();
    }
    protected void ingPolicyCabinMoveLeft_Click(object sender, ImageClickEventArgs e)
    {
        UpdateCabinPolic();
    }
    private void BindCabinClassGrids()
    {
        try
        {
            DataSet objCabin = objBaFlightSearch.GetAllCabinClassPolicy(ddlEmployeeCategory.SelectedValue);
            if (objCabin.Tables[0].Rows.Count > 0)
            {
                gvCabinclass.DataSource = objCabin.Tables[0];
                gvCabinclass.DataBind();
            }
            else
            {
                gvCabinclass.DataSource = null;
                gvCabinclass.DataBind();
            }
            if (objCabin.Tables[1].Rows.Count > 0)
            {
                gvCabinClassPolicy.DataSource = objCabin.Tables[1];
                gvCabinClassPolicy.DataBind();
            }
            else
            {
                gvCabinClassPolicy.DataSource = null;
                gvCabinClassPolicy.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void UpdateCabinPolic()
    {
        foreach (GridViewRow row in gvCabinClassPolicy.Rows)
        {
            CheckBox chk = row.FindControl("chkMoveLeft") as CheckBox;
            HiddenField hfCabinClassId = row.FindControl("hfCabinPolicySetupId") as HiddenField;
            if (chk.Checked == true)
            {
                int nResult = objBaFlightSearch.UpdateCabinClassPolicy(Convert.ToInt32(hfCabinClassId.Value), 0);
            }
        }
        BindCabinClassGrids();
    }
    private void InsertCabinclassPolicy()
    {
        foreach (GridViewRow row in gvCabinclass.Rows)
        {
            CheckBox chk = row.FindControl("chkMove") as CheckBox;
            HiddenField hfCabinClassId = row.FindControl("hfCabinClassId") as HiddenField;
            if (chk.Checked == true)
            {
                int CabinPolicySetupId = 0; string EmployeeCategory = ddlEmployeeCategory.SelectedValue; int policyStatus = 1;
                objBaFlightSearch.InsertUpdateCabinClassPolicy(CabinPolicySetupId, Convert.ToInt32(hfCabinClassId.Value), EmployeeCategory, policyStatus);
            }
        }
        BindCabinClassGrids();
    }
    #endregion CabinClassPolicy
    #region HotelPolicy
    protected void imgHotePolicyRight_Click(object sender, ImageClickEventArgs e)
    {
        InsertUpdateHotelPolicy();
    }
    protected void imgHotelLeft_Click(object sender, ImageClickEventArgs e)
    {
        UpdateHotelPolicy();
    }
    private void BindHotelPolicyGrid()
    {
        try
        {
            DataSet objDs = objBaFlightSearch.GetHotelCabPolicy(ddlEmployeeCategory.SelectedValue);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvHotelStars.DataSource = objDs.Tables[0];
                gvHotelStars.DataBind();
            }
            else
            {
                gvHotelStars.DataSource = null;
                gvHotelStars.DataBind();
            }
            if (objDs.Tables[1].Rows.Count > 0)
            {
                gvHotelRight.DataSource = objDs.Tables[1];
                gvHotelRight.DataBind();
            }
            else
            {
                gvHotelRight.DataSource = null;
                gvHotelRight.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void UpdateHotelPolicy()
    {
        foreach (GridViewRow row in gvHotelRight.Rows)
        {
            CheckBox chk = row.FindControl("chkMoveLeft") as CheckBox;
            HiddenField hfHotelPolicyId = row.FindControl("hfHotelPolicyId") as HiddenField;
            if (chk.Checked == true)
            {
                int nResult = objBaFlightSearch.DeleteHotelPolicy(Convert.ToInt32(hfHotelPolicyId.Value));
            }
        }
        BindHotelPolicyGrid();
    }
    private void InsertUpdateHotelPolicy()
    {
        foreach (GridViewRow row in gvHotelStars.Rows)
        {
            CheckBox chk = row.FindControl("chkMove") as CheckBox;
            HiddenField hfhotelstarsId = row.FindControl("hfhotelstarsId") as HiddenField;
            if (chk.Checked == true)
            {
                int HotelPolicyId = 0;
                string EmployeeCategory = ddlEmployeeCategory.SelectedValue;
                objBaFlightSearch.HotelPolicyInsert(HotelPolicyId, Convert.ToInt32(hfhotelstarsId.Value), EmployeeCategory);
            }
        }
        BindHotelPolicyGrid();
    }
    #endregion HotelPolicy

    #region CabPolicy
    protected void imgCarRight_Click(object sender, ImageClickEventArgs e)
    {
        InsertUpdateCabPolicy();
    }
    protected void imgCarLeft_Click(object sender, ImageClickEventArgs e)
    {
        UpdateCabPolicy();
    }
    private void BindCabPolicyGrid()
    {
        try
        {
            DataSet objDs = objBaFlightSearch.GetHotelCabPolicy(ddlEmployeeCategory.SelectedValue);

            if (objDs.Tables[2].Rows.Count > 0)
            {
                gvCarPolicy.DataSource = objDs.Tables[2];
                gvCarPolicy.DataBind();
            }
            else
            {
                gvCarPolicy.DataSource = null;
                gvCarPolicy.DataBind();
            }
            if (objDs.Tables[3].Rows.Count > 0)
            {
                gvCadRight.DataSource = objDs.Tables[3];
                gvCadRight.DataBind();
            }
            else
            {
                gvCadRight.DataSource = null;
                gvCadRight.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void UpdateCabPolicy()
    {
        foreach (GridViewRow row in gvCadRight.Rows)
        {
            CheckBox chk = row.FindControl("chkMoveLeft") as CheckBox;
            HiddenField hfcabpolicyId = row.FindControl("hfcabpolicyId") as HiddenField;
            if (chk.Checked == true)
            {
                int nResult = objBaFlightSearch.CabPolicyDelete(Convert.ToInt32(hfcabpolicyId.Value));
            }
        }
        BindCabPolicyGrid();
    }
    private void InsertUpdateCabPolicy()
    {
        foreach (GridViewRow row in gvCarPolicy.Rows)
        {
            CheckBox chk = row.FindControl("chkMove") as CheckBox;
            HiddenField hfcabclassid = row.FindControl("hfcabclassid") as HiddenField;
            if (chk.Checked == true)
            {
                int HotelPolicyId = 0;
                string EmployeeCategory = ddlEmployeeCategory.SelectedValue;
                objBaFlightSearch.CabPolicyInsert(HotelPolicyId, Convert.ToInt32(hfcabclassid.Value), EmployeeCategory);
            }
        }
        BindCabPolicyGrid();
    }
    #endregion CabPolicy
    protected void btnUpdateFlightDoc_Click(object sender, EventArgs e)
    {
        UpdateMarkUpValues();
    }
}