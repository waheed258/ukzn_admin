using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;
public partial class SalesAdmin_Promotions_Add : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BALPromotions _objBALPromotions = new BALPromotions(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {

            lblAmountTitel.Visible = true;
            if (!string.IsNullOrEmpty(Request.QueryString["promotionId"]))
            {
                int promotionId = Convert.ToInt32(Request.QueryString["promotionId"]);
                GetPromotions(promotionId);

            }
        }
    }

    
    protected void rboDiscountTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rboDiscountTypes.SelectedValue == "1")
        {
            lblAmountTitel.Text = "Percentage";
        }
        else
        {
            lblAmountTitel.Text = "Amount";
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdatePromotions();
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        Clearcontrols();
    }


    private void GetPromotions(int promotionId)
    {
        try
        {
            DataSet objds = _objBALPromotions.GetPromotionsDetails(promotionId);
            if (objds.Tables[0].Rows.Count > 0)
            {

                hfPromotionId.Value = objds.Tables[0].Rows[0]["promotionId"].ToString();
                dropPromotionCategory.SelectedIndex = dropPromotionCategory.Items.IndexOf(dropPromotionCategory.Items.FindByValue(objds.Tables[0].Rows[0]["promotionCategory"].ToString()));
                txtRelavance.Text = objds.Tables[0].Rows[0]["promotionRelavance"].ToString();
                DropPromotionType.SelectedIndex = DropPromotionType.Items.IndexOf(DropPromotionType.Items.FindByValue(objds.Tables[0].Rows[0]["promotionType"].ToString()));
                txtPromoCode.Text = objds.Tables[0].Rows[0]["promotionCode"].ToString();

                rboDiscountTypes.SelectedIndex = rboDiscountTypes.Items.IndexOf(rboDiscountTypes.Items.FindByText(objds.Tables[0].Rows[0]["DiscountType"].ToString()));
                if (rboDiscountTypes.SelectedIndex == 1)
                {

                    txtAmount.Text = objds.Tables[0].Rows[0]["amount"].ToString();
                    lblAmountTitel.Text = "Amount";

                }
                else
                {

                    txtAmount.Text = objds.Tables[0].Rows[0]["percentage"].ToString();
                    lblAmountTitel.Text = "Percentage";
                }

                txtStartDate.Text = _objBOUtility.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["startDate"].ToString(), "dd-MM-yyyy");
                txtEndDate.Text = _objBOUtility.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["endDate"].ToString(), "dd-MM-yyyy");

            }
        }
        catch (Exception ex)
        {

        }
    }

    private void InsertUpdatePromotions()
    {

        try
        {
            PromotionsMaster objPromotionsMaster = new PromotionsMaster();
            objPromotionsMaster.PromotionId = Convert.ToInt32(hfPromotionId.Value);
            objPromotionsMaster.PromotionCategory = dropPromotionCategory.SelectedValue;
            objPromotionsMaster.PromotionRelavance = txtRelavance.Text;
            objPromotionsMaster.PromotionType = DropPromotionType.SelectedValue;
            objPromotionsMaster.PromotionCode = txtPromoCode.Text;

            objPromotionsMaster.DiscountType = Convert.ToInt32(rboDiscountTypes.SelectedValue);
            if (rboDiscountTypes.SelectedValue == "1")
            {
                objPromotionsMaster.Percentage = Convert.ToDecimal(txtAmount.Text);
            }
            else
            {
                objPromotionsMaster.Amount = Convert.ToDecimal(txtAmount.Text);
            }


            objPromotionsMaster.StartDate = txtStartDate.Text != "" ? Convert.ToDateTime(_objBOUtility.ConvertDateFormat(txtStartDate.Text))
                : Convert.ToDateTime(_objBOUtility.ConvertDateFormat(System.DateTime.Now.ToShortDateString()));

            objPromotionsMaster.EndDate = txtEndDate.Text != "" ? Convert.ToDateTime(_objBOUtility.ConvertDateFormat(txtEndDate.Text))
                : Convert.ToDateTime(_objBOUtility.ConvertDateFormat(System.DateTime.Now.ToShortDateString()));
            objPromotionsMaster.CreatedBy = Session["loginId"].ToString();
            int nResult = _objBALPromotions.InsertUpdatePromotions(objPromotionsMaster);
            if (nResult > 0)
            {

                labelError.Text = _objBOUtility.ShowMessage("success", "Success", "Promaotion add successfully");

            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtility.ShowMessage("error", "Error", ex.Message);
        }
    }
    
    
    
    public void Clearcontrols()
    {
        dropPromotionCategory.SelectedValue = "-1";
        txtRelavance.Text = "";
        DropPromotionType.SelectedValue = "-1";
        txtPromoCode.Text = "";
        txtAmount.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";

    }
}