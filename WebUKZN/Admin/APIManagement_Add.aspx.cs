using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_APIManagement_Add : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BALAPIManagement _objBALAPIManagement = new BALAPIManagement();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            GetStatus();
            if (!string.IsNullOrEmpty(Request.QueryString["LineSeqid"]))
            {
                int LineSeqid = Convert.ToInt32(Request.QueryString["LineSeqid"]);
                GetAPIManagement(LineSeqid);

            }


        }
    }


    protected void cmdSubmit_Click(object sender, EventArgs e)
    {

        InsertUpdateAPIManagement();
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        Clearcontrols();
    }


    private void GetAPIManagement(int LineSeqid)
    {
        try
        {
            DataSet objds = _objBALAPIManagement.GetAPIManagementDetails(LineSeqid);
            if (objds.Tables[0].Rows.Count > 0)
            {
                hfLineSeqid.Value = objds.Tables[0].Rows[0]["LineSeqid"].ToString();
                txtPCCCode.Text = objds.Tables[0].Rows[0]["PccCode"].ToString();
                txtAPIName.Text = objds.Tables[0].Rows[0]["APIName"].ToString();
                txtUserName.Text = objds.Tables[0].Rows[0]["UserName"].ToString();
                txtPassWord.Text = objds.Tables[0].Rows[0]["PassWord"].ToString();
                txtProductionURL.Text = objds.Tables[0].Rows[0]["ProductionURL"].ToString();
                txtTestUrl.Text = objds.Tables[0].Rows[0]["TestURL"].ToString();
                dropIsActive.SelectedIndex = dropIsActive.Items.IndexOf(dropIsActive.Items.FindByValue(objds.Tables[0].Rows[0]["Status"].ToString()));

            }
        }
        catch (Exception ex)
        {

        }
    }



    private void InsertUpdateAPIManagement()
    {

        try
        {
            API_ManagementMater objAPIManagementMater = new API_ManagementMater();
            objAPIManagementMater.LineSeqid = Convert.ToInt32(hfLineSeqid.Value);
            objAPIManagementMater.PccCode = txtPCCCode.Text;
            objAPIManagementMater.APIName = txtAPIName.Text;
            objAPIManagementMater.UserName = txtUserName.Text;
            objAPIManagementMater.PassWord = txtPassWord.Text;
            objAPIManagementMater.ProductionURL = txtProductionURL.Text;
            objAPIManagementMater.TestURL = txtTestUrl.Text;
            // objAPIManagementMater.Status = dropIsActive.SelectedIndex > 0 ? Convert.ToInt32(dropIsActive.SelectedValue) : 0;
            objAPIManagementMater.Status = Convert.ToInt32(dropIsActive.SelectedValue);

            objAPIManagementMater.CreatedBy = Session["loginId"].ToString();
            int nResult = _objBALAPIManagement.InsertUpdateAPIManagement(objAPIManagementMater);
            if (nResult > 0)
            {

                labelError.Text = _objBOUtility.ShowMessage("success", "Success", "APIManagement add successfully");

            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtility.ShowMessage("error", "Error", ex.Message);
        }
    }


    public void Clearcontrols()
    {

        txtPCCCode.Text = "";
        txtAPIName.Text = "";
        txtUserName.Text = "";
        txtPassWord.Text = "";
        txtProductionURL.Text = "";
        txtTestUrl.Text = "";
        dropIsActive.SelectedValue = "0";
    }


    void GetStatus()
    {
        DataSet ds = new DataSet();

        try
        {
            ds = _objBALAPIManagement.GetStatus(0);

            if (ds.Tables.Count > 0)
            {
                dropIsActive.DataSource = ds.Tables[0];
                dropIsActive.DataTextField = "status_desc";
                dropIsActive.DataValueField = "status_id";
                dropIsActive.DataBind();
            }
            dropIsActive.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {

        }
    }
}