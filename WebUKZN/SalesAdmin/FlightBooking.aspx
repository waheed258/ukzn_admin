<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="FlightBooking.aspx.cs" Inherits="SalesAdmin_FlightBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
        <section class="content-header">
            <h1>Flight Bookings</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Flight Bookings</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                             <h3 class="box-title"> </h3>
                        </div>
                        <div class="box-body">
                            <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="FlightRequestId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

                               <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
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
                                                        <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/Air-tickets-icon.png" Height="25" OnClientClick="return confirm('Are you sure generate E-ticket?');" ToolTip="Generate Eticket"
                                                            Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/icon-print.png" Height="25" ToolTip="Print E-ticket"
                                                            Width="25" CommandName="PrintTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnCancel" Visible="false" runat="server" ImageUrl="~/images/icon-print-cancel.png" Height="25" ToolTip="Cancel E-ticket"
                                                            Width="25" CommandName="CancelTicket" OnClientClick="return confirm('Are you sure cancel E-ticket?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnCancelBooking" Visible="false" runat="server" ImageUrl="~/images/icon-print-cancel.png" Height="25" ToolTip="Cancel Booking"
                                                            Width="25" CommandName="BookingCancel" OnClientClick="return confirm('Are you sure cancel Booking?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                </tr>
                                            </table>



                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
  
</asp:Content>

