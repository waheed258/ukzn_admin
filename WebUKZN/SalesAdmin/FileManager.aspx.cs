using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using EntityManager;
using UKZNInterface;
using System.Configuration;

public partial class SalesAdmin_FileManager : System.Web.UI.Page
{
    BALFileManager _objBALFileManager = new BALFileManager();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    UKZNTravelRequest _objUKZNTravelRequest = new UKZNTravelRequest();
    string _strUKZNsupplierno = ConfigurationManager.AppSettings["UKZNsupplierno"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            GetFileGrid();
        }
    }
    #region PrivateMethods
    private void GetFileGrid()
    {
        try
        {
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBALFileManager.GetAllBookings(CreatedBy, RoleId, CompanyId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvAllFileNo.DataSource = objDs;
                gdvAllFileNo.DataBind();
            }
            else
            {
                gdvAllFileNo.DataSource = null;
                gdvAllFileNo.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #endregion PrivateMethods
    protected void gdvAllFileNo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         string FileNo = e.CommandArgument.ToString();
        string strMessage = string.Empty;
        if (e.CommandName == "PrintAll")
        {
            string url = "PrintFile.aspx?FileNo=" + FileNo;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else
        {
            DataSet objDs = _objBALFileManager.GetQuotationMasterByFileNo(FileNo);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                List<TravelRequestProcess> LstobjTravelRequestProcess = new List<TravelRequestProcess>();
                TravelRequestProcess ItemList = new TravelRequestProcess();
                ItemList.supplierno = _strUKZNsupplierno;
                ItemList.ordernumber = objDs.Tables[0].Rows[0]["ukzn_orderno"].ToString();
                ItemList.travelrequestno = objDs.Tables[0].Rows[0]["QuotationRefNo"].ToString();
                ItemList.totamountincvat = objDs.Tables[0].Rows[0]["TripAmount"].ToString();
                ItemList.cancellationdate = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper();
                LstobjTravelRequestProcess.Add(ItemList);
                strMessage = _objUKZNTravelRequest.validateordcancellation(LstobjTravelRequestProcess);
                if (strMessage == "Success")
                {
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success ", "Order cancelled in Integrator");
                    _objBALFileManager.UpdateFileMaster(FileNo, "Order cancelled in Integrator");
                    GetFileGrid();
                }
                else
                {
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", strMessage);
                }

            }


            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info ", "Sorry no records found with this order");
            }
        }

    }
    
}