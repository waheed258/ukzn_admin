<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="FileSearch.aspx.cs" Inherits="SalesAdmin_FileSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    

        <!-- Content Header (Page header) -->
        <section class="content-header">

            <h1>File Search</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">FileSearch</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>

            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <div class="box-body form-horizontal">

                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">File No</label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtSearch" runat="server" ID="rfvtxtSearch"
                                        ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                                        class="validationred" Display="Dynamic" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="Submit" OnClick="cmdSubmit_Click" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnNewBooking" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="New Booking" OnClick="btnNewBooking_Click" />
                                </div>
                            </div>
                          
                                    <label>Flight Bookings</label>
                                    <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="FlightRequestId"
                                        AutoGenerateColumns="False"
                                        Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

                                        <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
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
                                            <asp:TemplateField HeaderText="File No" SortExpression="FileNo">
                                                <ItemTemplate>
                                                    <%#Eval("FileNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Agent/Consultant Name" SortExpression="UserName">
                                                <ItemTemplate>
                                                    <%#Eval("UserName")%>
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
                                            <asp:TemplateField HeaderText="Origin" SortExpression="FlightFrom">
                                                <ItemTemplate>
                                                    <%#Eval("FlightFrom")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Destination" SortExpression="FlightTo">
                                                <ItemTemplate>
                                                    <%#Eval("FlightTo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" OnClientClick="return confirm('Are you sure generate E-ticket?');" ToolTip="Generate Eticket"
                                                                    Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/print.png" Height="25" ToolTip="Print E-ticket"
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
                                                        </tr>
                                                    </table>



                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                
                           
                            <br />
                            <br />
                           
                                    <label>Hotel Bookings</label>
                                    <asp:GridView ID="gdvHotelBooking" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
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
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="cmdPrintInvoice" runat="server" ImageUrl="~/images/print.png" Height="25"
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
                        </div>
                       
                   </div>
             
        </section>
        <!-- /.content -->
    
</asp:Content>

