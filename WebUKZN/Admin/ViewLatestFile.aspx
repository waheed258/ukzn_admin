<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ViewLatestFile.aspx.cs" Inherits="Admin_ViewLatestFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            color: #333;
            font-family: Helvetica, Arial, sans-serif;
            width: 100%;
        }

        tr.border_bottom td {
            border-top: 1pt solid black;
        }

        td, th {
            height: 30px;
            transition: all 0.3s; /* Simple transition for hover effect */
        }

        th {
            background: #DFDFDF; /* Darken header a bit */
            font-weight: bold;
            text-align: center;
        }

        td {
            background: #FAFAFA;
            text-align: center;
        }

        /* Cells in even rows (2,4,6...) are one color */
        tr:nth-child(even) td {
            background: #F1F1F1;
        }

        /* Cells in odd rows (1,3,5...) are another (excludes header cells)  */
        tr:nth-child(odd) td {
            background: #FEFEFE;
        }

        tr td:hover {
            background: #666;
            color: #FFF;
        }
        /* Hover cell effect! */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Target Report
       
               
        </h1>

        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Target Report</li>
        </ol>
    </section>

    <section class="content">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-body form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-1">
                                File No :
                    
                            </div>
                            <div class="col-sm-2">

                                <asp:Label ID="lblLatestFileNo" runat="server" class="control-label"></asp:Label>

                            </div>
                            <div class="col-sm-2" style="color: #0088cc; font-size: 15px;">
                                UKZN  Order No :
                    
                            </div>
                            <div class="col-sm-2" style="color: #0088cc; font-size: 15px;">

                                <asp:Label ID="lblOrderNo" runat="server" class="control-label"></asp:Label>

                            </div>
                            <div class="col-sm-2" style="display: none;">
                                Total Amount
                            </div>
                            <div class="col-sm-2" style="display: none;">

                                <asp:TextBox ID="txttotalAmount" ReadOnly="true" runat="server" class="form-control" />

                            </div>

                            <div class="col-sm-2" style="display: none;">
                                Received Amount
                            </div>

                            <div class="col-sm-2" style="display: none;">
                                <asp:TextBox ID="txtReceivedAmount" ReadOnly="true" runat="server" class="form-control" />
                            </div>

                        </div>
                        <div class="form-group" style="display: none;">

                            <div class="col-sm-3">

                                <asp:HiddenField ID="hfTravellerId" runat="server" Value="0" />
                                <asp:HiddenField ID="hfFileNo" runat="server" Value="0" />
                                <asp:Button ID="btnGenerateOtherBookings" runat="server" class="btn btn-primary green" Text="Add another booking" OnClick="btnGenerateOtherBookings_Click" />
                            </div>
                            <div class="col-sm-2">


                                <asp:Button ID="btnCloseFile" runat="server" class="btn btn-primary green" Text="Close File" OnClick="btnCloseFile_Click" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnInvoice" runat="server" class="btn btn-primary green" Text="Print Invoice" OnClick="btnInvoice_Click" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Label ID="lblAgentMessage" runat="server" Visible="false" ForeColor="Red" Font-Size="Large"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                Flight Bookings
                <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                    DataKeyNames="FlightRequestId"
                    AutoGenerateColumns="False"
                    Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

                    <RowStyle CssClass="gradeA odd" />
                    <AlternatingRowStyle CssClass="gradeA even" />
                    <Columns>

                        <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("Booking_Status")%>' />
                                <asp:HiddenField ID="hfBookingRef" runat="server" Value='<%#Eval("BookingRefNo")%>' />
                                <asp:HiddenField ID="hfTraceId" runat="server" Value='<%#Eval("TraceId")%>' />
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agent / Consultant" SortExpression="UserName">
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
                        <asp:TemplateField HeaderText="Name" SortExpression="PaxName">
                            <ItemTemplate>
                                <%#Eval("PaxName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="PaxEmail">
                            <ItemTemplate>
                                <%#Eval("PaxEmail")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone" SortExpression="PaxMobile">
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
                        <asp:TemplateField HeaderText="Price" SortExpression="Amount">
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
                                        <td>
                                            <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/Air-tickets-icon.png" Height="25" OnClientClick="return confirm('Are you sure generate E-ticket?');" ToolTip="Generate Eticket"
                                                Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/icon-print.png" Height="25" ToolTip="Print E-ticket"
                                                Width="25" CommandName="PrintTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnCancel" Visible="false" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel E-ticket"
                                                Width="25" CommandName="CancelTicket" OnClientClick="return confirm('Are you sure cancel E-ticket?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCancelBooking" Visible="false" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel Booking"
                                                Width="25" CommandName="BookingCancel" OnClientClick="return confirm('Are you sure cancel Booking?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnViewDtls" runat="server" ImageUrl="~/images/ViewDtls.png" Height="25" ToolTip="View Details"
                                                Width="25" CommandName="ViewDetails" CommandArgument='<%# Eval("FlightRequestId") %>' />
                                        </td>
                                    </tr>
                                </table>



                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">
                            <div class="col-sm-12">
                                Hotel Bookings
                <asp:GridView ID="gdvHotelBooking" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                    Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gdvHotelBooking_RowCommand" OnRowDataBound="gdvHotelBooking_RowDataBound">

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
                        <%-- <asp:TemplateField HeaderText="Hotel Address">
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

                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnStatus" runat="server" Style="height: 18px; width: 25px; border-radius: 6px;" />
                                        </td>
                                        <td>
                                            <%#Eval("status_desc")%>
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
                                            <asp:ImageButton ID="cmdPrintInvoice" runat="server" ImageUrl="~/images/icon-print.png" Height="25"
                                                Width="25" CommandName="PrintInVoice" CommandArgument='<%# Eval("HotelRequestId") %>' ToolTip="Print Ticket" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="cmdCancelHotel" runat="server" ImageUrl="~/images/delete.png" Height="25"
                                                Width="25" CommandName="Cancel Hotel" CommandArgument='<%# Eval("HotelRequestId") %>' ToolTip="Cancel Hotel" />
                                        </td>
                                    </tr>
                                </table>

                                </li>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                            </div>
                        </div>

                        <br />
                        <br />
                        <div class="form-group">
                            <div class="col-sm-12">
                                Car Bookings
                <asp:GridView ID="gvCarBookings" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                    Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gvCarBookings_RowCommand">

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
                                            <asp:ImageButton ID="cmdPrintInvoice" runat="server" ImageUrl="~/images/icon-print.png" Height="25"
                                                Width="25" CommandName="PrintInVoice" CommandArgument='<%# Eval("CarDescId") %>' ToolTip="Print Ticket" />
                                        </td>

                                    </tr>
                                </table>

                                </li>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

