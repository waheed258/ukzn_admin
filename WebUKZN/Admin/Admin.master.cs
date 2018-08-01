using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    BALUserUKZN objBalUser = new BALUserUKZN();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAFlightSearch _objBAFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        if (Session["loginId"] != null)
        {
              LabelUserName.Text = Session["loginuser"].ToString();
            GetMenu();
        //      GetAlert();
        }


    }

    private void GetMenu()
    {
        string strMenu = string.Empty;
        string strRole = Session["role_id"].ToString().Trim();
        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
        DataSet objdsmenu = _objBOUtiltiy.GetMenus(strRole, CompanyId);
        if (objdsmenu.Tables[0].Rows.Count > 0)
        {
            strMenu = "";

            foreach (DataRow mainItem in objdsmenu.Tables[0].Rows)
            {

                if (mainItem["MenuName"].ToString() == "Dashboard" || mainItem["MenuName"].ToString() == "New Booking" || mainItem["MenuName"].ToString() == "Latest Booking" || mainItem["MenuName"].ToString() == "New Booking for others")
                {
                    //strMenu += "<li class='sub-menu'><a href=" + mainItem["Url"].ToString() + "><i class='fa fa-user'></i>" + mainItem["MenuName"].ToString() + "</a>";

                    strMenu += "<li class='treeview'> <a href='" + mainItem["Url"].ToString() + "'><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a></li>";
                }
                else if (mainItem["MenuName"].ToString() == "Visa Services")
                {
                    strMenu += "<li class='treeview'> <a target='_blank' href='" + mainItem["Url"].ToString() + "'><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a></li>";
                }
                else
                {
                    strMenu += "<li class='treeview'><a><i class='" + mainItem["MenuIcon"].ToString() + "' aria-hidden='true'></i><span>" + mainItem["MenuName"].ToString() + "</span></a>";
                }
                DataRow[] lstSubMenu;
                lstSubMenu = objdsmenu.Tables[1].Select("ParentMenuId='" + mainItem["MenuId"] + "'");


                if (lstSubMenu.Length > 0)
                {

                    strMenu += "<ul class='treeview-menu menu-open'>";
                    foreach (var subMenuItem in lstSubMenu)
                    {
                        //  strMenu += "<li><a href=" + subMenuItem["Url"].ToString() + ">" + subMenuItem["MenuName"].ToString() + "</a></li>";
                        strMenu += "<li><a href='" + subMenuItem["Url"].ToString() + "'><i class='fa fa-circle-o'></i>" + subMenuItem["MenuName"].ToString() + "</a></li>";
                    }
                    strMenu += "</ul>";
                }
                strMenu += "</li>";
            }
            strMenu += "</ul>";
        }
        menu.InnerHtml = strMenu;

        //if (objdsmenu.Tables[2].Rows.Count > 0)
        //{
        //    string topMenu = string.Empty;
        //    topMenu += "<li class='divider'></li>";
        //    foreach (DataRow topmenu in objdsmenu.Tables[2].Rows)
        //    {
        //        //  topMenu += "<li><a href=" + topmenu["Url"].ToString() + ">" + topmenu["MenuName"].ToString() + "</a></li>";

        //        topMenu += "<li><a role='menuitem' tabindex='-1' href='" + topmenu["Url"].ToString() + "'><i class='fa fa-user'></i>" + topmenu["MenuName"].ToString() + "</a></li>";
        //    }

        //    ultopmenu.InnerHtml = topMenu;
        //}

    }
    //private void GetAlert()
    //{
    //    StringBuilder strAlert = new StringBuilder();

    //    try
    //    {
    //        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
    //        int RoleID = Convert.ToInt32(Session["role_id"]);
    //        int UserId = Convert.ToInt32(Session["loginId"]);
    //        DataSet objDs = _objBAFlightSearch.GetTrueLastDateToTicketAlerts(UserId, RoleID, CompanyId);
    //        //       lblAlertCount.Text = objDs.Tables[0].Rows.Count.ToString();
    //        if (objDs.Tables[0].Rows.Count > 0)
    //        {
    //            strAlert.Append("<ul>");
    //            strAlert.Append("<li>");
    //            strAlert.Append("<a href='#' class='clearfix'>");
    //            strAlert.Append("<div class='image'>");
    //            strAlert.Append("<i class='fa fa-clock-o bg-danger'></i>");
    //            strAlert.Append("</div>");
    //            strAlert.Append("<span class='title'>E Ticket Need To Done</span>");
    //            strAlert.Append("<span class='message'>Total Pending" + objDs.Tables[0].Rows.Count + "</span>");
    //            strAlert.Append("</a>");
    //            strAlert.Append("</li>");
    //            strAlert.Append("</ul>");

    //            strAlert.Append("<hr />");

    //            strAlert.Append("<div class='text-right'>");
    //            strAlert.Append("<a href='LastDateToTicket.aspx' class='view-more'>View All</a>");
    //            strAlert.Append("</div>");
    //            topalert.InnerHtml = strAlert.ToString();

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        int LogoutResult = objBalUser.UserLoginHistoryInsertUpdate(Convert.ToInt32(Session["UserMasterId"].ToString()), "", "Update");
        if (LogoutResult >= 1)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
