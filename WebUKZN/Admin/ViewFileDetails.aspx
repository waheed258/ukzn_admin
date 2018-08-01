<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFileDetails.aspx.cs" Inherits="Admin_ViewFileDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>File Details</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <!--Core CSS -->
    <link href="../Admin/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    
  <%--  <link href="../Admin/dist/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/bootstrap-reset.css" rel="stylesheet" />
  
    <!-- Custom styles for this template -->
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/style-responsive.css" rel="stylesheet" />

    <link href="cssMain/notification.css" rel="stylesheet" />
    <script src="../Admin/plugins/jQuery/jquery-2.2.3.min.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
       <div class="panel">
            <div class="panel-body">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <br />

       <section class="content">
              <asp:Label ID="Label1" runat="server"></asp:Label>
              <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-body form-horizontal">
                             <div class="col-sm-12 marginbtm">
                    <div class="col-sm-2">
                        
                            Total Amount
                    </div>
                    <div class="col-sm-3">

                        <asp:TextBox ID="txttotalAmount" Enabled="false" runat="server" class="form-control" />

                    </div>
                    <div class="col-sm-1">
                    </div>
                    <asp:Panel runat="server" ID="pnlresvamount">
                        <div class="col-sm-2">
                            
                                Received Amount
                        </div>

                        <div class="col-sm-3">
                            <asp:TextBox ID="txtReceivedAmount" Enabled="false" runat="server" class="form-control" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-sm-12 marginbtm">
                    Flight Bookings
                    <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="myGridClass" DataKeyNames="FlightRequestId"
                        AutoGenerateColumns="False"
                        Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

                        <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                        <Columns>

                            <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("Booking_Status")%>' />
                                    <asp:HiddenField ID="hfBookingRef" runat="server" Value='<%#Eval("BookingRefNo")%>' />
                                     <asp:HiddenField ID="hfTraceId" runat="server" Value='<%#Eval("TraceId")%>' />
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Agent / Consultant Name" SortExpression="UserName">
                                <ItemTemplate>
                                    <%#Eval("UserName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking Date" SortExpression="BookingDate">
                                <ItemTemplate>
                                    <%#Eval("BookingDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger Name" SortExpression="PaxName">
                                <ItemTemplate>
                                    <%#Eval("PaxName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger Email" SortExpression="PaxEmail">
                                <ItemTemplate>
                                    <%#Eval("PaxEmail")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger Phone" SortExpression="PaxMobile">
                                <ItemTemplate>
                                    <%#Eval("PaxMobile")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fromcode" HeaderText="From">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ToCode" HeaderText="To">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Journey Date" SortExpression="FlightDate">
                                <ItemTemplate>
                                    <%#Eval("FlightDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Price" SortExpression="Amount">
                                <ItemTemplate>
                                    <%#Eval("Amount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="ticketstatus">
                                <ItemTemplate>
                                    <%#Eval("ticketstatus")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlHotelBookings" runat="server" Visible="false">
                    <div class="col-sm-12 marginbtm">
                        <label>Hotel Bookings</label>
                        <asp:GridView ID="gdvHotelBooking" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                            AutoGenerateColumns="False" CssClass="myGridClass"
                            Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gdvHotelBooking_RowCommand" OnRowDataBound="gdvHotelBooking_RowDataBound">

                            <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="4" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="hfStatusId" runat="server" Value='<%#Eval("status_id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File No">
                                    <ItemTemplate>
                                        <%#Eval("FileNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Hotel">
                                    <ItemTemplate>
                                        <%#Eval("SupplierHotel")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hotel Address">
                                    <ItemTemplate>
                                        <%#Eval("HotelAddress")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Guest First Name">
                                    <ItemTemplate>
                                        <%#Eval("GuestFirstName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Guest Phone Number">
                                    <ItemTemplate>
                                        <%#Eval("GuestPhoneNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Guest Email Id">
                                    <ItemTemplate>
                                        <%#Eval("GuestEmail")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CheckIn Date">
                                    <ItemTemplate>
                                        <%#Eval("CheckinDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Price">
                                    <ItemTemplate>
                                        <%#Eval("TotalPrice")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%#Eval("status_desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>


                        <asp:GridView ID="gdvOnlineHotelBookings" runat="server" EmptyDataText="No Bookings Found"
                            AutoGenerateColumns="False"
                            Width="100%" OnRowCommand="gdvOnlineHotelBookings_RowCommand" Visible="false">

                            <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                            <Columns>

                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>

                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Traveller Name" SortExpression="PaxName">
                                    <ItemTemplate>
                                        <%#Eval("PaxName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <%#Eval("Location")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check In" SortExpression="ArrivalDate">
                                    <ItemTemplate>
                                        <%#Eval("ArrivalDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check Out" SortExpression="CheckOutDate">
                                    <ItemTemplate>
                                        <%#Eval("CheckOutDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Nigths" SortExpression="Duration">
                                    <ItemTemplate>
                                        <%#Eval("Duration")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Rooms" SortExpression="NoRooms">
                                    <ItemTemplate>
                                        <%#Eval("NoRooms")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </asp:Panel>
                            </div>
                        </div>
                    </div>
                  
            </div>
           </section>
        </div>
            </div>
            
    </form>
   
<%--    <script src="../Admin/plugins/jQuery/jquery-2.2.3.min.js"></script>

    <script src="../Admin/Js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="../Admin/Js/jquery.nicescroll.js"></script>
    
    <script src="../Admin/Js/jquery.scrollTo.js"></script>
    <script src="../Admin/Js/jquery.scrollTo.min.js"></script>
    <script src="../Admin/plugins/datatables/jquery.dataTables.js"></script>
    <script src="../Admin/plugins/slimScroll/jquery.slimscroll.min.js"></script>

    <script src="../Admin/Js/jquery.nicescroll.js"></script>
    

    <script src="../Admin/Js/scripts.js"></script>

   

    <script type="text/javascript" src="../Admin/Js/DT_bootstrap.js"></script>

    <script type="text/javascript" src="../Admin/Js/dynamic_table_init.js"></script>--%>
</body>
</html>

