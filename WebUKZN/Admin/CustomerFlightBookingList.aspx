<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CustomerFlightBookingList.aspx.cs" Inherits="Admin_CustomerFlightBookingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <section class="content-header">
            <h1>Customer FlightBooking List
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Login History</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Data Found"
                                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound1">
                                <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("Booking_Status")%>' />
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="PaxName">
                                        <ItemTemplate>
                                            <%#Eval("PaxName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Email" SortExpression="paxEmail">
                                        <ItemTemplate>
                                            <%#Eval("paxEmail")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Mobile" SortExpression="PaxMobile">
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
                                    <asp:TemplateField HeaderText="Jurney Date" SortExpression="FlightDate">
                                        <ItemTemplate>
                                            <%#Eval("FlightDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ticketing Status" SortExpression="ticketstatus">
                                        <ItemTemplate>
                                            <%#Eval("ticketstatus")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <li class="editbutton">
                                                <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" ToolTip="Generate Eticket"
                                                    Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                            </li>
                                            <li class="editbutton">
                                                <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/print.png" Height="25" ToolTip="Print Eticket"
                                                    Width="25" CommandName="PrintTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                            </li>
                                            <li class="editbutton">
                                                <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel"
                                                    Width="25" CommandName="CancelTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                            </li>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>

                </div>
            </div>
            <!-- /.row -->
        </section>  
</asp:Content>

