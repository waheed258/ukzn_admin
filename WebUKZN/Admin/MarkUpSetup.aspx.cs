using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using EntityManager;

public partial class Admin_MarkUpSetup : System.Web.UI.Page
{
    private BAFlightSearch objBaFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindAirLineList();
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

            DataSet objDs = objBaFlightSearch.GetAllAirLineListMarkupMaster(CompanyId, BranchId, UserId, 1, "ALL");
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
                objBaFlightSearch.InsertUpdateMarkUpSetup(ojbFlightMarkUpMaster);

            }
            BindAirLineList();
        }
        catch (Exception ex)
        {

        }
    }
    #endregion PrivateMethods
    protected void bntMoveRight_Click(object sender, ImageClickEventArgs e)
    {
        MoveRight();
    }
    protected void imgMoveLeft_Click(object sender, ImageClickEventArgs e)
    {
        MoveLeft();
    }

    protected void btnUpdateFlightDoc_Click(object sender, EventArgs e)
    {
        UpdateMarkUpValues();
    }
}