<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintHotel.aspx.cs" Inherits="Admin_PrintHotel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:Panel ID="pnl" runat="server" Visible="false">
        <div class="row booking-detail">
                <div class="container clear-padding">
                    <div class="tab-content">

                        <div id="passenger-info" class="tab-pane fade in active">

                            <div id="review-booking" class="tab-pane fade in active">

                                <div class="col-md-4 col-sm-4 booking-sidebar" style="width: 100%">
                                    <div class="sidebar-item booking-summary">
                                        <h4><i class="fa fa-bookmark"></i>Your Booking Information</h4>
                                        <div class="sidebar-body">
                                            <div class="booking-summary-title">
                                                <div class="col-md-4 col-sm-4 col-xs-4 clear-padding" style="width: 19%">
                                                    <img src="assets/images/offer1.jpg" alt="cruise">
                                                </div>
                                                <div class="col-md-8 col-sm-8 col-xs-8">
                                                    <h5>
                                                        <asp:Label ID="lblPropertyName" runat="server"></asp:Label></h5>
                                                    <%--<h5><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i><i class="fa fa-star"></i></h5>--%>
                                                    <h5 class="loc"><i class="fa fa-map-marker"></i>
                                                        <asp:Label ID="lblPropertyAddress" runat="server" /></h5>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Booking References no</h5>
                                                <div class="text-center" style="text-align: left; font-size: 20px; font-weight: bold; margin-bottom: 30px;">
                                                    <%-- <h2>05</h2>
                                            <h6>AUG</h6>
                                            <h5>FRI</h5>--%>
                                                    <asp:Label ID="lblBookingRefNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Supplier &  SupplierReference no</h5>
                                                <div class="text-center" style="text-align: left; font-size: 20px; font-weight: bold; margin-bottom: 30px;">
                                                    <%-- <h2>05</h2>
                                            <h6>AUG</h6>
                                            <h5>FRI</h5>--%>
                                                    <asp:Label ID="lblSupplierRefNo" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Primary Passenger Name</h5>
                                                <div class="text-center" style="text-align: left; font-size: 20px; font-weight: bold; margin-bottom: 30px;">
                                                    <asp:Label ID="lblGuestName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Primary Passenger Address</h5>
                                                <div class="text-center" style="text-align: left; font-size: 20px; font-weight: bold; margin-bottom: 30px;">
                                                    <asp:Label ID="lblGuestAddress" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Check In</h5>
                                                <div class="date text-center">
                                                    <%-- <h2>05</h2>
                                            <h6>AUG</h6>
                                            <h5>FRI</h5>--%>
                                                    <asp:Label ID="lblCheckinDate" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCheckInDateformat" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-6 clear-padding">
                                                <h5>Check Out</h5>
                                                <div class="date text-center">
                                                    <%-- <h2>09</h2>
                                            <h6>AUG</h6>
                                            <h5>SAT</h5>--%>
                                                    <asp:Label ID="lblCheckoutDate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <table class="table">
                                                <tr>
                                                    <td>No Nights</td>
                                                    <td>
                                                        <asp:Label ID="lblDuration" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Total Rooms</td>
                                                    <td>
                                                        <asp:Label ID="lblTotalRooms" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Room Type</td>
                                                    <td>
                                                        <asp:Label ID="lblRoomtype" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                            <div class="clearfix"></div>
                                            <table class="table">
                                                <tr>
                                                    <td>Room Wise Guest</td>
                                                    <td>Adults</td>
                                                    <td>Child</td>
                                                    <td>Infrants</td>
                                                </tr>
                                                <asp:Repeater runat="server" ID="rptTotalGuest">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>Room  <%# Container.ItemIndex+1 %></td>
                                                            <td><%#DataBinder.Eval(Container.DataItem,"Adults")%></td>
                                                            <td><%#DataBinder.Eval(Container.DataItem,"Children")%></td>
                                                            <td><%#DataBinder.Eval(Container.DataItem,"Infants")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </table>
                                            <table class="table">
                                                <tr>

                                                    <td>Total Pay</td>

                                                </tr>
                                                <tr>

                                                    <td class="total">
                                                        <%=CurrencyCode%>
                                                        <asp:Label ID="lblTotalToPay" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="font-size: 17px; color: red; font-weight: bold;">
                                                Note : 
                                            </div>
                                            <asp:Label ID="lblErrotMsg" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfMealBasisID" runat="server" />

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
