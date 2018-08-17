using BusinessManager;
using DataManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Policysetup : System.Web.UI.Page
{
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    DALPolicyRules objDALPolicyRules = new DALPolicyRules();
    BAPolicyRules objBALPolicyRules = new BAPolicyRules();
    EMPolicyRules objEMPolicyRules = new EMPolicyRules();
    char Operation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();

            if (!string.IsNullOrEmpty(Request.QueryString["PolicyId"]))
            {
                int PlicyId = Convert.ToInt32(_objBOUtiltiy.Decrypt(Request.QueryString["PolicyId"], true));
                GetPolicy(Convert.ToInt32(PlicyId));
                cmdSubmit.Text = "Update";
            }
        }
    }

    #region PrivateMethods
    private void BindCategory()
    {
        int CategoryId = 0;
        DataSet DS_Category = new DataSet();
        DS_Category = objBALPolicyRules.Get_Category(CategoryId);
        try
        {
            drpCategory.DataSource = DS_Category.Tables[0];
            drpCategory.DataTextField = "Category";
            drpCategory.DataValueField = "CategoryID";
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }


    #endregion PrivateMethods

    protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Category_Type();
    }


    private void Get_Category_Type()
    {
        try
        {
            drpCategoryType.DataSource = objBALPolicyRules.Get_Category_type(drpCategory.SelectedIndex);
            drpCategoryType.DataTextField = "TypeName";
            drpCategoryType.DataValueField = "TypeID";
            drpCategoryType.DataBind();
            drpCategoryType.Items.Insert(0, new ListItem("-Select-", "0"));


        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objEMPolicyRules.PolicyID = Convert.ToInt32(hf_CategoryId.Value);
            objEMPolicyRules.Category = Convert.ToInt32(drpCategory.SelectedIndex);
            objEMPolicyRules.CategoryType = Convert.ToInt32(drpCategoryType.SelectedIndex);
            objEMPolicyRules.RuleValue = txtRuleValue.Text;
            objEMPolicyRules.Condition = drpCondation.SelectedItem.Text;


            //   char? Operation = null;

            //  char test= cmdSubmit.Text=="Submit"?'i':'u';

            if (cmdSubmit.Text == "Submit")
            {
                Operation = 'i';
            }
            else
            {
                Operation = 'u';
            }
 
            int Result = objBALPolicyRules.InsUpdateCategoryType(objEMPolicyRules, Operation);

            if (Result > 0)
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Agency Cedit Memo created Successfully");
                Response.Redirect("PolicyList.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("info", "Info", "Agency Cedit Memo Details was not created plase try again");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }



    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PolicyList.aspx");
    }



    private void GetPolicy(int PolicyId)
    {
        try
        {
            DataSet objds = objBALPolicyRules.GetPolicys(PolicyId);
            if (objds.Tables[0].Rows.Count > 0)
            {
                hf_CategoryId.Value = objds.Tables[0].Rows[0]["PolicyID"].ToString();
                drpCategory.SelectedIndex = drpCategory.Items.IndexOf(drpCategory.Items.FindByValue(objds.Tables[0].Rows[0]["Category"].ToString()));
                txtRuleValue.Text = objds.Tables[0].Rows[0]["RuleValue"].ToString();
                Get_Category_Type();
                drpCategoryType.SelectedIndex = drpCategoryType.Items.IndexOf(drpCategoryType.Items.FindByValue(objds.Tables[0].Rows[0]["CategoryType"].ToString()));
                drpCondation.SelectedIndex = drpCondation.Items.IndexOf(drpCondation.Items.FindByText(objds.Tables[0].Rows[0]["Condition"].ToString()));

            }
        }
        catch
        {

        }
    }
}