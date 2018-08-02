<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="LastDateToTicket.aspx.cs" Inherits="Admin_LastDateToTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <!-- Content Header (Page header) -->
     <!-- Content Header (Page header) -->

        <section class="content-header">
           
            <h3>Flight Bookings</h3>

            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Flight Bookings</li>
            </ol>
        </section>

        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <header class="panel-heading">
                                <div class="panel-actions">
                                    <a href="#" class="panel-action panel-action-toggle" data-panel-toggle=""></a>
                                </div>

                                <h2 class="panel-title">Flight Bookings</h2>
                            </header>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="form-group">
                                <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    DataKeyNames="FlightRequestId"
                                    AutoGenerateColumns="False"
                                    Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlightBookings_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

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
                                        <asp:TemplateField HeaderText="Jurney Date" SortExpression="FlightDate">
                                            <ItemTemplate>
                                                <%#Eval("FlightDate")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ticketing Status" SortExpression="ticketstatus">
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
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/Air-tickets-icon.png" Height="25" OnClientClick="return confirm('Are you sure generate E-ticket?');" ToolTip="Generate Eticket"
                                                                Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/icon-print.png" Height="25" ToolTip="Print Eticket"
                                                                Width="25" CommandName="PrintTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnCancel" Visible="false" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel Ticket"
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
                        <!-- /.box-body -->
                        <div class="box-footer clearfix">

                            <%-- <ul class="pagination pagination-sm no-margin pull-right">
                                <li><a href="#">&laquo;</a></li>
                                <li><a href="#">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">&raquo;</a></li>
                            </ul>--%>
                        </div>
                    </div>
                    <!-- /.box -->
                </div>
                
            </div>
            <!-- /.row -->
        </section>
</asp:Content>

