using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MoveFlightSalesToFin : System.Web.UI.Page
{
    private BALFinSales objBALFinSales = new BALFinSales();
    BALSalesIntegration objBALSalesIntegration = new BALSalesIntegration();
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindBookings();
        }
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet objds = null;
            if (Cache["NoMvoeSalesToFin"] != null)
            {
                objds = (DataSet)(Cache["NoMvoeSalesToFin"]);
            }

            foreach (GridViewRow row in gdvFlightBookings.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[1] as CheckBox;
                HiddenField hf = row.Cells[0].Controls[3] as HiddenField;
                if (chk != null && chk.Checked)
                {

                    // Use the Select method to find all rows matching the filter.
                    if (objds.Tables[0].Rows.Count > 0)
                    {
                        var strExpr = "FlightRequestId = " + hf.Value;
                        var strSort = "FlightRequestId DESC";
                        var foundRows = objds.Tables[0].Select(strExpr, strSort);

                        if (foundRows.Length > 0)
                        {
                            T5BookingHeader objT5BookingHeader = new T5BookingHeader();
                            objT5BookingHeader.PlatingCarrier = foundRows[0]["PlatingCarrier"].ToString();
                            objT5BookingHeader.airline_name = foundRows[0]["airline_name"].ToString();
                            objT5BookingHeader.PseudoCityCode = foundRows[0]["PseudoCityCode"].ToString();
                            objT5BookingHeader.ProviderLocatorCode = foundRows[0]["ProviderLocatorCode"].ToString();
                            objT5BookingHeader.TotalPriceCurrencyCode = foundRows[0]["TotalPriceCurrencyCode"].ToString();
                            objT5BookingHeader.TotalPrice = foundRows[0]["TotalPrice"].ToString().Replace(".", string.Empty).Trim();
                            objT5BookingHeader.FileNo = foundRows[0]["FileNo"].ToString();
                            objT5BookingHeader.FlightDate = objBOUtiltiy.ReverseConvertDateFormat(foundRows[0]["FlightDate"].ToString(), "ddMMMyy").ToUpper();
                            objT5BookingHeader.FlightReturnDate = objBOUtiltiy.ReverseConvertDateFormat(foundRows[0]["FlightReturnDate"].ToString(), "ddMMMyy").ToUpper();
                            objT5BookingHeader.CntCode = foundRows[0]["UserCode"].ToString() != "" ? foundRows[0]["UserCode"].ToString() : "Mh";
                            objT5BookingHeader.PaxName = foundRows[0]["paxname"].ToString();
                            objT5BookingHeader.ExcluAmount = foundRows[0]["ServiceFee"].ToString();
                            objT5BookingHeader.VatAmount = foundRows[0]["VatFee"].ToString();
                            objT5BookingHeader.ClientTot = foundRows[0]["ChargeServiceFee"].ToString();
                            objT5BookingHeader.VatPer = 0;
                            objT5BookingHeader.PaymentMethod = foundRows[0]["paymenttype"].ToString() != "Cash" ? "1" : "2";
                            int FlightRequestId = Convert.ToInt32(foundRows[0]["FlightRequestId"]);
                            int UerLoginId = Convert.ToInt32(foundRows[0]["UserLoginId"]);
                            int UserRole = Convert.ToInt32(foundRows[0]["UserRole"]);
                            int Eticket = Convert.ToInt32(foundRows[0]["EticketBy"]);


                            string PaxName = foundRows[0]["paxname"].ToString();
                            string PaxEmail = foundRows[0]["PaxEmail"].ToString();
                            string PaxMobile = foundRows[0]["PaxMobile"].ToString();


                            string UserName = foundRows[0]["UserName"].ToString();
                            string UserEmail = foundRows[0]["UserPhone"].ToString();
                            string UserPhone = foundRows[0]["UserPhone"].ToString();
                            string UserCode = foundRows[0]["UserCode"].ToString();


                            int nResult = objBALSalesIntegration.InsertUpdateT5BookingHeader(objT5BookingHeader);
                            if (nResult > 0)
                            {

                                DataSet ObjdsAll = null;
                                if (UserRole != 3)
                                {

                                    ObjdsAll = objBALFinSales.GetAllSalesDetails(FlightRequestId, nResult, UerLoginId, UserRole);

                                    objBALSalesIntegration.BatchBulkCopyPassenger(ObjdsAll.Tables[0], "a02_passenger_det");
                                    objBALSalesIntegration.BatchBulkCopyAirLineDetails(ObjdsAll.Tables[1], "a04_airline_det");
                                    objBALSalesIntegration.BatchBulkCopyFairDetails(ObjdsAll.Tables[2], "a07_fare_value_det");

                                    string EmployeeName = ObjdsAll.Tables[3].Rows[0]["UserName"].ToString();
                                    string EmployeeEmail = ObjdsAll.Tables[3].Rows[0]["UserEmail"].ToString();
                                    string EmployeePhone = ObjdsAll.Tables[3].Rows[0]["UserPhone"].ToString();
                                    string EmployeeCode = ObjdsAll.Tables[3].Rows[0]["UserCode"].ToString();

                                    int nEmpResult = objBALSalesIntegration.InsertUpdateEmployee(EmployeeName, EmployeeEmail, EmployeePhone, EmployeeCode, nResult);

                                    int nCrpResult = objBALSalesIntegration.InsertUpdateClients(3, PaxName, PaxEmail, PaxMobile, nResult);

                                }
                                else
                                {
                                    ObjdsAll = objBALFinSales.GetAllSalesDetails(FlightRequestId, nResult, Eticket, UserRole);


                                    objBALSalesIntegration.BatchBulkCopyPassenger(ObjdsAll.Tables[0], "a02_passenger_det");
                                    objBALSalesIntegration.BatchBulkCopyAirLineDetails(ObjdsAll.Tables[1], "a04_airline_det");
                                    objBALSalesIntegration.BatchBulkCopyFairDetails(ObjdsAll.Tables[2], "a07_fare_value_det");

                                    string EmployeeName = ObjdsAll.Tables[3].Rows[0]["UserName"].ToString();
                                    string EmployeeEmail = ObjdsAll.Tables[3].Rows[0]["UserEmail"].ToString();
                                    string EmployeePhone = ObjdsAll.Tables[3].Rows[0]["UserPhone"].ToString();
                                    string EmployeeCode = ObjdsAll.Tables[3].Rows[0]["UserCode"].ToString();

                                    int nEmpResult = objBALSalesIntegration.InsertUpdateEmployee(EmployeeName, EmployeeEmail, EmployeePhone, EmployeeCode, nResult);

                                    int nCrpResult = objBALSalesIntegration.InsertUpdateClients(3, UserName, UserEmail, UserPhone, nResult);
                                }

                                objBALFinSales.UpdateMoveToFinance(FlightRequestId);
                                BindBookings();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #region Methods
    private void BindBookings()
    {
        try
        {
            DataSet objDs = objBALFinSales.GetNotMovedSales();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvFlightBookings.DataSource = objDs;
                gdvFlightBookings.DataBind();

                Cache.Remove("NoMvoeSalesToFin");
                SetCacheDataset(objDs);
            }
            else
            {
                gdvFlightBookings.DataSource = null;
                gdvFlightBookings.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    public void SetCacheDataset(DataSet objDs)
    {
        if (Cache["NoMvoeSalesToFin"] == null)
        {
            DataSet result = objDs;
            Cache["NoMvoeSalesToFin"] = (result);
        }

    }
    #endregion
}