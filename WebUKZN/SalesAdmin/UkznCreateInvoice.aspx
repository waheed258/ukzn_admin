﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="UkznCreateInvoice.aspx.cs" Inherits="SalesAdmin_UkznCreateInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>All bookings</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">All bookings</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <asp:HiddenField ID="hf_UserMasterId" runat="server" Value="0" />

        <div class="row">
            <!-- left column -->
            <div class="col-md-12">

                <!--/.col (left) -->
                <!-- right column -->
                <div class="col-md-12">
                    <!-- Horizontal Form -->
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">All bookings</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">

                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Total Amount</label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txttotalAmount" ReadOnly="true" runat="server" class="form-control" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-3">
                                    <label class="control-label">
                                        Cost Centercode & Name</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtCostCenterCodeName" ReadOnly="true" runat="server" class="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Order No</label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtOrderNo" ReadOnly="true" runat="server" class="form-control" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnCreateInvoice" class="btn btn-primary green pdf"
                                        Text="Create Invoice" OnClick="btnCreateInvoice_Click" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnPrintInvoice" class="btn btn-primary green pdf"
                                        Text="View Invoice" OnClick="btnPrintInvoice_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Flight Bookings</h3>
                                </div>


                                <%--<label>Flight Bookings</label>--%>
                                <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found"
                                    CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="FlightRequestId"
                                    AutoGenerateColumns="False"
                                    Width="100%" PageSize="100" usecustompager="true" OnRowDataBound="gdvFlightBookings_RowDataBound">

                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SN">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("Booking_Status")%>' />
                                                <asp:HiddenField ID="hfBookingRef" runat="server" Value='<%#Eval("BookingRefNo")%>' />
                                                <asp:HiddenField ID="hfTraceId" runat="server" Value='<%#Eval("TraceId")%>' />
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Booking User" SortExpression="UserName">
                                            <ItemTemplate>
                                                <%#Eval("UserName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Booking Date" SortExpression="BookingDate">
                                            <ItemTemplate>
                                                <%#Eval("BookingDate")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PNR" SortExpression="PNR">
                                            <ItemTemplate>
                                                <%#Eval("PNR")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airline RefNo" SortExpression="AirlineRefNo">
                                            <ItemTemplate>
                                                <%#Eval("AirlineRefNo")%>
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

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnStatus" runat="server" Style="height: 18px; width: 25px; border-radius: 6px;" />
                                                        </td>
                                                        <td>
                                                            <%#Eval("ticketstatus")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                            <br />
                            <asp:Panel ID="pnlHotelBookings" runat="server" Visible="false">
                                <div class="form-group">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Hotel Bookings</h3>
                                    </div>
                                    <%--<label>Hotel Bookings</label>--%>
                                    <asp:GridView ID="gdvHotelBooking" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                        Width="100%" PageSize="4" usecustompager="true" OnRowDataBound="gdvHotelBooking_RowDataBound">

                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SN">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hfStatusId" runat="server" Value='<%#Eval("status_id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="File No">
                                <ItemTemplate>
                                    <%#Eval("FileNo")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Supplier Hotel">
                                                <ItemTemplate>
                                                    <%#Eval("SupplierHotel")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Hotel Address">
                                <ItemTemplate>
                                    <%#Eval("HotelAddress")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </td>

                                                        </tr>
                                                    </table>

                                                    </li>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <asp:GridView ID="gdvOnlineHotelBookings" runat="server" EmptyDataText="No Bookings Found"
                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                        Width="100%" Visible="false">

                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
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
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>

                                                    <table>
                                                        <tr>

                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </td>

                                                        </tr>
                                                    </table>



                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </asp:Panel>

                            <br />
                            <br />
                            <br />
                            <asp:Panel ID="pnlCars" runat="server" Visible="false">
                                <div class="form-group">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Car Bookings</h3>
                                    </div>
                                    <%--<label>Car Bookings</label>--%>
                                    <asp:GridView ID="gvCarBookings" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                        Width="100%" PageSize="4" usecustompager="true">

                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SN">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Car Model">
                                                <ItemTemplate>
                                                    <%#Eval("CarModel")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Hotel Address">
                            <ItemTemplate>
                                <%#Eval("HotelAddress")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Pickup Location">
                                                <ItemTemplate>
                                                    <%#Eval("pickuplocation")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PickUp Date & Time">
                                                <ItemTemplate>
                                                    <%#Eval("pickdateandtime")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Drop Location">
                                                <ItemTemplate>
                                                    <%#Eval("droplocation")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DropDate & Time">
                                                <ItemTemplate>
                                                    <%#Eval("droptimeanddate")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Price">
                                                <ItemTemplate>
                                                    <%#Eval("TotalPrice")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>

                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnStatus" runat="server" Style="background-color: Green; height: 18px; width: 25px; border-radius: 6px;" />
                                                            </td>
                                                            <td>Booking Confirmed
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    </li>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </div>
                        <!-- /.box-body -->

                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->

                </div>
                <!--/.col (right) -->

            </div>
        </div>
        <!-- /.row -->
    </section>
    <!-- /.content -->

</asp:Content>

