using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_ViewAllTargets : System.Web.UI.Page
{
    BALUserManager _objBALUserManager = new BALUserManager();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            GetAllTargetByMontAndYear();
        }
    }
    #region PrivateMethods
    private void GetAllTargetByMontAndYear()
    {
        try
        {
            int CurrentMonth = DateTime.Today.Month;
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBALUserManager.GetTargetSetupByMonthAndYear(CurrentMonth, 0, CompanyId);
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
    #endregion PrivateMethods
    protected void gdvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfEticketIssue = e.Row.FindControl("hfTargetPercentage") as HiddenField;
            double Percentage = Convert.ToDouble(hfEticketIssue.Value);
            if (Percentage > 0 && Percentage < 75)
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
            }
            else if (Percentage > 75.01 && Percentage < 99.99)
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
            }
        }
    }
}