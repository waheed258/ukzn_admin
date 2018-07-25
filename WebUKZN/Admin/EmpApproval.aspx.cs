using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmpApproval : System.Web.UI.Page
{
    BALUserUKZN _objBalUser = new BALUserUKZN();
    BOUtiltiyUKZN _objBoUtility = new BOUtiltiyUKZN();
    StringBuilder EmpApprovalIds = new StringBuilder();
    EMUser objEmpAppoval = new EMUser();
    int EmployeeQueryStringId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlEmpCurrentApproval.Enabled = false;
            //ddlEmpCurrentApproval.Attributes.Add("disabled", "disabled");
            BindEmployeeListByApprovals();
            btnEmpLevelAdd.Visible = false;
            btnRemove.Visible = false;
            DeleteStatusZeroRecords();
            Session["EmployeeQueryStringId"] = null;
            if (!string.IsNullOrEmpty(Request.QueryString["EmpId"]))
            {
                EmployeeQueryStringId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
                Session["EmployeeQueryStringId"] = EmployeeQueryStringId;
                BindEmployeeList();
                ddlEmpList.Enabled = false;
                    //DataSet EmpApprovalRecordCheck = _objBalUser.EmpApprovalCheckGet(EmployeeId, 1);
                //if (EmpApprovalRecordCheck.Tables[0].Rows.Count >= 1)
                //{
                //    objEmpAppoval.EmApprovalId = Convert.ToInt32(EmpApprovalRecordCheck.Tables[0].Rows[0]["EmpApproveId"].ToString());
                //    EmpApprovalList.Add(EmpApprovalRecordCheck.Tables[0].Rows[0]["ApprovalIds"].ToString());
                //    EmpApprovalIds = String.Join(",", EmpApprovalList.ToArray());
                //}
                //else
                //{
                //    objEmpAppoval.EmApprovalId = 0;
                //}
                ddlEmpCurrentApproval.Enabled = true;
                btnEmpApprovalSave.Text = "Update";
                BindEmpApprovalRightGrid(EmployeeQueryStringId, 1, 1);
                BindLeftEmpGridRecords(EmployeeQueryStringId, 1, 0);
                ddlEmpList.SelectedIndex = EmployeeQueryStringId;
                ddlEmpCurrentApproval.SelectedIndex = 1;
            }
        }
    }

    #region Events
    // adding empolyees as approvals when click on add button
    protected void btnEmpLevelAdd_Click(object sender, EventArgs e)
    {
        // DataTable LeftGridEmpTotal = (DataTable)ViewState["LeftGridEmpTotalRecords"];
        DataTable rightGridEmpApproval;
        if (ViewState["RightGridEmpApprovalRecords"] != null)
            rightGridEmpApproval = (DataTable)ViewState["RightGridEmpApprovalRecords"];
        else
            rightGridEmpApproval = CreateTable();

        // LeftGridEmpTotal =  gvEmpData
        //foreach (GridViewRow  row in gvEmpData.Rows)
        //{
        //// foreach (GridV col in row)
        ////    {
        for (int i = 0; i < gvEmpData.Rows.Count; i++)
        {
            CheckBox chkEmpSelect = (CheckBox)gvEmpData.Rows[i].Cells[0].FindControl("chkEmpSelect");
            if (chkEmpSelect.Checked)
            {
                rightGridEmpApproval = AddGridRow(gvEmpData.Rows[i], rightGridEmpApproval);
                //EmpApprovalIds.AppendFormat("{0} ", gvEmpData.Rows[i].Cells["UserMasterId"]);

                //EmpApprovalIds.Append(rightGridEmpApproval.ta.Rows[i+1]["UserMasterId"].ToString());
                // LeftGridEmpTotal = RemoveRow(gvEmpData.Rows[i], gvEmpData.Rows[i]);
            }
        }
        ViewState["RightGridEmpApprovalRecords"] = rightGridEmpApproval;
        EmpApprovalInsertUpdate();
        BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);
        BindLeftEmpGridRecords(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);



    }
    // Removing empolyees as approvals when click on Remove button
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        DataTable EmpApprovalRightGrid = CreateTable();
        for (int i = 0; i < gvEmpLeveApprovals.Rows.Count; i++)
        {
            CheckBox chkEmpApproveRemove = (CheckBox)gvEmpLeveApprovals.Rows[i].Cells[0].FindControl("chkEmpAppovalSelect");
            if (!chkEmpApproveRemove.Checked)
            {
                EmpApprovalRightGrid = AddGridRow(gvEmpLeveApprovals.Rows[i], EmpApprovalRightGrid);


            }

        }
        ViewState["RightGridEmpApprovalRecords"] = EmpApprovalRightGrid;
        EmpAppovalRemoveUpdate();
        BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);
        BindLeftEmpGridRecords(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);
    }

    protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ddlEmpCurrentApproval.Attributes.Remove("disabled");
        ddlEmpCurrentApproval.Enabled = true;
        BindBothGridEmpAndLevelBase();

    }
    protected void ddlEmpCurrentApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBothGridEmpAndLevelBase();

    }

    protected void chkEmpPrimarySelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow gvrow in gvEmpLeveApprovals.Rows)
            {
                CheckBox chkEmpSelect = (CheckBox)gvrow.FindControl("chkEmpPrimarySelect");
                if (chkEmpSelect.Checked)
                {
                    objEmpAppoval.PrimaryLevelId = Convert.ToInt32(gvrow.Cells[1].Text);

                    DataSet EmpApprovalRecordCheck = _objBalUser.EmpApprovalCheckGet(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue));
                    objEmpAppoval.ApprovalsId = EmpApprovalRecordCheck.Tables[0].Rows[0]["ApprovalIds"].ToString();
                    objEmpAppoval.ApprovalStatus = 1;
                    objEmpAppoval.EmApprovalId = Convert.ToInt32(EmpApprovalRecordCheck.Tables[0].Rows[0]["EmpApproveId"].ToString());
                    objEmpAppoval.EmpId = Convert.ToInt32(ddlEmpList.SelectedValue);
                    objEmpAppoval.LevelId = Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue);
                    if (Convert.ToInt32(Session["EmployeeQueryStringId"]) > 0)
                    {
                        objEmpAppoval.Operation = "Update";
                    }
                    else
                    {
                        objEmpAppoval.Operation = "";
                    }
                    int EmpPrimaryApproval = _objBalUser.EmpApprovalInsertUpdate(objEmpAppoval);
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    // Final saving of Employee Approvals (status column is updating)
    protected void btnEmpApprovalSave_Click(object sender, EventArgs e)
    {
        //string script = string.Format("alert('Saving Employee Approvals. ');");
        //ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script, false);
        objEmpAppoval.EmpId = Convert.ToInt32(ddlEmpList.SelectedValue);

        objEmpAppoval.ApprovalStatus = 1;
        objEmpAppoval.Operation = "Status Update";
        int EmpApprovalStatusUpdate = _objBalUser.EmpApprovalInsertUpdate(objEmpAppoval);
        if (EmpApprovalStatusUpdate > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saving Employee Approvals.');window.location ='EmpApprovalList.aspx';", true);
        }
        else
        {

        }
    }
    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpApprovalList.aspx");
    }
    #endregion EndEvents

    #region PrivateMethods
    // Binding Employee List in dropdown
    private void BindEmployeeList()
    {
        try
        {
            DataSet ds = _objBalUser.GetUserList(0);
            ddlEmpList.DataTextField = "UserName";
            ddlEmpList.DataValueField = "UserMasterId";
            ddlEmpList.DataSource = ds;
            ddlEmpList.DataBind();
            ddlEmpList.Items.Insert(0, new ListItem("-- Please Select --", "-1"));
           // ViewState["LeftGridEmpTotalRecords"] = ds;
            //gvEmpData.DataSource = ds;
            //gvEmpData.DataBind();
          //  BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 1);
            //BindLeftEmpGridRecords(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 1);
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private DataTable AddGridRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("EmpId = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length <= 0)
        {
            dt.Rows.Add();
            int rowscount = dt.Rows.Count - 1;
            dt.Rows[rowscount]["EmpId"] = gvRow.Cells[1].Text;
            dt.Rows[rowscount]["UserName"] = gvRow.Cells[2].Text;

            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("UserMasterId = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmpId");
        dt.Columns.Add("EmpApproveId");
        dt.Columns.Add("UserName");
        dt.Columns.Add("LevelId");
        dt.Columns.Add("PrimaryLevelId");
        dt.AcceptChanges();
        return dt;
    }
    // inserting the employee approvals records in Database Table
    private void EmpApprovalInsertUpdate()
    {
        try
        {
            string EmpApprovalIds = "";
            DataTable RightEmpApprovalRecords = (DataTable)ViewState["RightGridEmpApprovalRecords"];
            // int rows = leftGirdRecords.Rows.Count;
            if (RightEmpApprovalRecords != null)
            {
                try
                {
                    List<string> EmpApprovalList = new List<string>();

                    objEmpAppoval.EmpId = Convert.ToInt32(ddlEmpList.SelectedValue);
                    // objEmpAppoval.EmpId = Convert.ToInt32(RightEmpApprovalRecords.Rows[i]["UserMasterId"].ToString());
                    objEmpAppoval.LevelId = Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue);

                    DataSet EmpApprovalRecordCheck = _objBalUser.EmpApprovalCheckGet(objEmpAppoval.EmpId, objEmpAppoval.LevelId);
                    if (EmpApprovalRecordCheck.Tables[0].Rows.Count >= 1)
                    {
                        objEmpAppoval.EmApprovalId = Convert.ToInt32(EmpApprovalRecordCheck.Tables[0].Rows[0]["EmpApproveId"].ToString());
                        EmpApprovalList.Add(EmpApprovalRecordCheck.Tables[0].Rows[0]["ApprovalIds"].ToString());
                        EmpApprovalIds = String.Join(",", EmpApprovalList.ToArray());
                        
                    }
                    else
                    {
                        objEmpAppoval.EmApprovalId = 0;
                    }

                    foreach (DataRow rows in RightEmpApprovalRecords.Rows)
                    {


                        EmpApprovalList.Add(Convert.ToString(rows["EmpId"].ToString()));
                        // EmpApprovalIds.Append(string.Join(",", ));
                        // EmpApprovalIds.Append(string.Join(",", rows["UserMasterId"].ToString()));
                        // EmpApprovalIds.Append( rows["UserMasterId"].ToString()).Append(",");


                    }
                    EmpApprovalIds = String.Join(",", EmpApprovalList.ToArray());
                    objEmpAppoval.ApprovalsId = Convert.ToString(EmpApprovalIds);
                    EmpApprovalList.Clear();
                    EmpApprovalIds = "";
                    objEmpAppoval.PrimaryLevelId = 0;
                    objEmpAppoval.ApprovalStatus = 0;
                    //if (objEmpAppoval.ApprovalsId.Count() >= 1)
                    //{
                    //    objEmpAppoval.ApprovalStatus = 1;
                    //}
                  //  objEmpAppoval.Operation = "Insert";

                    if (Convert.ToInt32(Session["EmployeeQueryStringId"]) > 0)
                    {
                        objEmpAppoval.Operation = "Update";
                    }
                    else
                    {
                        objEmpAppoval.Operation = "";
                    }

                    int result = _objBalUser.EmpApprovalInsertUpdate(objEmpAppoval);
                    if (result > 0)
                    {
                        ViewState["RightGridEmpApprovalRecords"] = null;
                        // Response.Redirect("EmpApprovalList.aspx");


                    }
                    else
                    {
                        lblMsg.Text = _objBoUtility.ShowMessage("info", "Info", "Employee  was not Added plase try again");
                    }


                }

                catch (Exception ex)
                {
                    lblMsg.Text = _objBoUtility.ShowMessage("danger", "Danger", ex.Message);

                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Danger", ex.Message);

        }

    }
    // update the record while removing the employee approval
    private void EmpAppovalRemoveUpdate()
    {

        try
        {
            string EmpApprovalIds = "";
            DataTable RightEmpApprovalRecords = (DataTable)ViewState["RightGridEmpApprovalRecords"];
            // int rows = leftGirdRecords.Rows.Count;
            if (RightEmpApprovalRecords != null)
            {
                try
                {
                    List<string> EmpApprovalList = new List<string>();

                    objEmpAppoval.EmpId = Convert.ToInt32(ddlEmpList.SelectedValue);
                    // objEmpAppoval.EmpId = Convert.ToInt32(RightEmpApprovalRecords.Rows[i]["UserMasterId"].ToString());
                    objEmpAppoval.LevelId = Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue);

                    DataSet EmpApprovalRecordCheck = _objBalUser.EmpApprovalCheckGet(objEmpAppoval.EmpId, objEmpAppoval.LevelId);
                    if (EmpApprovalRecordCheck.Tables[0].Rows.Count >= 1)
                    {
                        objEmpAppoval.EmApprovalId = Convert.ToInt32(EmpApprovalRecordCheck.Tables[0].Rows[0]["EmpApproveId"].ToString());
                        // EmpApprovalList.Add(EmpApprovalRecordCheck.Tables[0].Rows[0]["ApprovalIds"].ToString());
                        // = String.Join(",", EmpApprovalList.ToArray());
                    }
                    else
                    {
                        objEmpAppoval.EmApprovalId = 0;
                    }

                    foreach (DataRow rows in RightEmpApprovalRecords.Rows)
                    {


                        EmpApprovalList.Add(Convert.ToString(rows["EmpId"].ToString()));
                        // EmpApprovalIds.Append(string.Join(",", ));
                        // EmpApprovalIds.Append(string.Join(",", rows["UserMasterId"].ToString()));
                        // EmpApprovalIds.Append( rows["UserMasterId"].ToString()).Append(",");


                    }
                    EmpApprovalIds = String.Join(",", EmpApprovalList.ToArray());
                    objEmpAppoval.ApprovalsId = Convert.ToString(EmpApprovalIds);
                    EmpApprovalList.Clear();
                    EmpApprovalIds = "";
                    objEmpAppoval.PrimaryLevelId = 0;
                    if (objEmpAppoval.ApprovalsId.Count() >= 1)
                    {
                        objEmpAppoval.ApprovalStatus = 1;
                    }

                    objEmpAppoval.Operation = "Update";
                    int result = _objBalUser.EmpApprovalInsertUpdate(objEmpAppoval);
                    if (result > 0)
                    {
                        ViewState["RightGridEmpApprovalRecords"] = null;
                        // Response.Redirect("EmpApprovalList.aspx");


                    }
                    else
                    {
                        lblMsg.Text = _objBoUtility.ShowMessage("info", "Info", "Employee  was not Added plase try again");
                    }


                }

                catch (Exception ex)
                {
                    lblMsg.Text = _objBoUtility.ShowMessage("danger", "Danger", ex.Message);

                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Danger", ex.Message);

        }


    }
    //Binding Employees in Left Grid
    private void BindLeftEmpGridRecords(int empId, int LevelId, int status)
    {
        try
        {
            if (Convert.ToInt32(Session["EmployeeQueryStringId"]) > 0)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }

            DataSet LeftEmpRecords = _objBalUser.EmpApprovalGet(empId, LevelId, status);
            if (LeftEmpRecords.Tables[0].Rows.Count > 0)
            {
                gvEmpData.DataSource = LeftEmpRecords.Tables[0];
                gvEmpData.DataBind();
                btnEmpLevelAdd.Visible = true;
                btnRemove.Visible = true;
            }
            else
            {
                gvEmpData.DataSource = null;
                gvEmpData.DataBind();
            }

        }
        catch (Exception ex)
        {


            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    //Binding Employees in Right Grid
    private void BindEmpApprovalRightGrid(int EmpId, int LevelId, int Status)
    {
        try
        {
            if (Convert.ToInt32(Session["EmployeeQueryStringId"]) > 0)
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }
            DataSet RightGrid = _objBalUser.EmpApprovalGet(EmpId, LevelId, Status);
            gvEmpLeveApprovals.DataSource = RightGrid.Tables[1];
            gvEmpLeveApprovals.DataBind();

        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }

     //Binding  Both grids based on employee and level  of approval
    private void BindBothGridEmpAndLevelBase()
    {
        if (ddlEmpList.SelectedIndex > 0 && ddlEmpCurrentApproval.SelectedIndex > 0)
        {
            try
            {
                if (Convert.ToInt32(Session["EmployeeQueryStringId"]) > 0)
                {

                    BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 1);
                }
                else
                {

                    BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);
                }
                BindLeftEmpGridRecords(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 0);
            }
            catch (Exception ex)
            {

                lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
            }
        }
        else
        {
            gvEmpData.DataSource = null;
            gvEmpData.DataBind();
            gvEmpLeveApprovals.DataSource = null;
            gvEmpLeveApprovals.DataBind();
        }
    }

    private void BindEmployeeListByApprovals()
    {
        try
        {
            DataSet ds = _objBalUser.UserGetListByEmpApproval();
            ddlEmpList.DataTextField = "UserName";
            ddlEmpList.DataValueField = "UserMasterId";
            ddlEmpList.DataSource = ds;
            ddlEmpList.DataBind();
            ddlEmpList.Items.Insert(0, new ListItem("-- Please Select --", "-1"));
            // ViewState["LeftGridEmpTotalRecords"] = ds;
            //gvEmpData.DataSource = ds;
            //gvEmpData.DataBind();
            //  BindEmpApprovalRightGrid(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 1);
            //BindLeftEmpGridRecords(Convert.ToInt32(ddlEmpList.SelectedValue), Convert.ToInt32(ddlEmpCurrentApproval.SelectedValue), 1);
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }

    private void DeleteStatusZeroRecords()
    {
        try
        {
            //objEmpAppoval.Operation = "Delete";
            //objEmpAppoval.ApprovalStatus = 0;
            int RemoveReslut = _objBalUser.EmpApprovalStatusZeroDelete(0);
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    // private  DataTable GetEmpApprovalCheckAndGet(int EmpId,int LevelId)
    //{
    //    try
    //    { 
    //        DataSet EmpApprovalRecordCheck = _objBalUser.EmpApprovalCheckGet(objEmpAppoval.EmpId, objEmpAppoval.LevelId);
    //        DataTable EmpApprovalRecord = new DataTable();
    //        EmpApprovalRecord = EmpApprovalRecordCheck.Tables[0];
    //        return EmpApprovalRecord;

    //    }
    //    catch (Exception ex)
    //    {

    //        lblMsg.Text = _objBoUtility.ShowMessage("danger", "Error", ex.Message);
    //    }
    //}
    #endregion Privatemethods
   

    


  
    

  
    
}