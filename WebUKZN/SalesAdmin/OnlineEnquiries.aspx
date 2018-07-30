<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="OnlineEnquiries.aspx.cs" Inherits="SalesAdmin_OnlineEnquiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <section class="content-header">
            <h1>Online Enquiries
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>
        <section class="content">

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="form-group">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Online Enquiries</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found"
                                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound1">

                                    <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("AirPriceRspStatus")%>' />
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
                                                    <asp:ImageButton ID="imgPriceConfirm" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" ToolTip="Check availability and book"
                                                        Width="25" CommandName="BookFlight" OnClientClick="return confirm('are you sure you want to Booking?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />


                                                </li>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>


                            </div>

                        </div>
                        <!-- /.box-body -->

                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->

                </div>
                <!--/.col (right) -->

            </div>


        </section>
</asp:Content>

