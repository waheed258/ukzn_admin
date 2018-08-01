using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UKZNApprovalReports : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    BALUser _objBALUser = new BALUser();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            GetReport(Convert.ToInt32(Session["loginId"]));
        }
    }

    #region PrivateMethod
    private void GetReport(int CreatedBy)
    {
        try
        {
            DataSet objDs = _objBALUser.GetUserLoginDetailsByLoginId(CreatedBy);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                string staffNo = objDs.Tables[0].Rows[0]["staffno"].ToString();
                DataSet objDsrpt = _objBORpeort.UkznApprovalReports(staffNo);
                if (objDsrpt.Tables[0].Rows.Count > 0)
                {
                    gvApprovalReports.DataSource = objDsrpt;
                    gvApprovalReports.DataBind();
                }
                else
                {
                    gvApprovalReports.DataSource = null;
                    gvApprovalReports.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", ex.Message);
        }
    }
    #endregion PrivateMethod
}