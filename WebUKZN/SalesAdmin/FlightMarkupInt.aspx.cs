using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using EntityManager;
public partial class SalesAdmin_FlightMarkupInt : System.Web.UI.Page
{
    private BAFlightSearch objBaFlightSearch = new BAFlightSearch();
    private BOUtiltiy objBoutility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            GetMarkupValues("ALL");
        }
    }
    #region PrivateMethod
    private void InsertInternationalMarkup()
    {
        try
        {
            int UserId = Convert.ToInt32(Session["loginId"]);
            int BranchId = Convert.ToInt32(Session["BranchId"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);


            FlightMarkUpMaster ojbFlightMarkUpMaster = new FlightMarkUpMaster();
            ojbFlightMarkUpMaster.MarkUpId = Convert.ToInt32(hfMarkUpId.Value);
            ojbFlightMarkUpMaster.AirLineCode = "";
            ojbFlightMarkUpMaster.MarkUpType = Convert.ToInt32(ddlMarkUptype.SelectedValue);// Fixed On Percentage
            ojbFlightMarkUpMaster.MarkUpValue = Convert.ToDecimal(txtMarkUpValue.Text);
            ojbFlightMarkUpMaster.MarkUpTripType = 2; // International or Domastic
            ojbFlightMarkUpMaster.MarkUpOn = 2;
            ojbFlightMarkUpMaster.ClassCode = ddlClass.SelectedValue;
            ojbFlightMarkUpMaster.SegmentLevel = chkSegmentLevel.Checked;
            ojbFlightMarkUpMaster.PassengerLevel = chkPassengerLevel.Checked;
            ojbFlightMarkUpMaster.CreatedBy = UserId;
            ojbFlightMarkUpMaster.CompanyId = CompanyId;
            ojbFlightMarkUpMaster.BranchId = BranchId;
            ojbFlightMarkUpMaster.UserId = UserId;
            ojbFlightMarkUpMaster.Status = 1;
            objBaFlightSearch.InsertUpdateMarkUpSetup(ojbFlightMarkUpMaster);
            labelError.Text = objBoutility.ShowMessage("success", "success", "Action Completed");

        }
        catch (Exception ex)
        {
            labelError.Text = objBoutility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void GetMarkupValues(string ClassAirlineCode)
    {
        try
        {
            int UserId = Convert.ToInt32(Session["loginId"]);
            int BranchId = Convert.ToInt32(Session["BranchId"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);

            DataSet objDs = objBaFlightSearch.GetAllAirLineListMarkupMaster(CompanyId, BranchId, UserId, 2, ClassAirlineCode);

            if (objDs.Tables[4].Rows.Count > 0)
            {
                hfMarkUpId.Value = objDs.Tables[4].Rows[0]["MarkUpId"].ToString();
                ddlMarkUptype.SelectedIndex = ddlMarkUptype.Items.IndexOf(ddlMarkUptype.Items.FindByValue(objDs.Tables[4].Rows[0]["MarkUpType"].ToString()));
                ddlClass.SelectedIndex = ddlClass.Items.IndexOf(ddlClass.Items.FindByValue(objDs.Tables[4].Rows[0]["ClassCode"].ToString()));
                txtMarkUpValue.Text = objDs.Tables[4].Rows[0]["MarkUpValue"].ToString();
                chkSegmentLevel.Checked = Convert.ToBoolean(objDs.Tables[4].Rows[0]["SegmentLevel"].ToString());
                chkPassengerLevel.Checked = Convert.ToBoolean(objDs.Tables[4].Rows[0]["PassengerLevel"].ToString());
            }
            else
            {
                hfMarkUpId.Value = "0";
                ddlMarkUptype.SelectedIndex = 0;

                txtMarkUpValue.Text = "0";
                chkSegmentLevel.Checked = false;
                chkPassengerLevel.Checked = false;
            }

        }
        catch (Exception ex)
        {

        }
    }
    #endregion PrivateMethod
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetMarkupValues(ddlClass.SelectedValue);
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertInternationalMarkup();
    }
}