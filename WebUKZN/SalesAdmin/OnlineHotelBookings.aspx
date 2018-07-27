<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="OnlineHotelBookings.aspx.cs" Inherits="SalesAdmin_OnlineHotelBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>OnLine Hotel Bookings
       
               
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
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">OnLine Hotel Bookings</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <asp:GridView ID="gdvHotelBookings" runat="server" EmptyDataText="No Bookings Found"
                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                    Width="100%" OnRowCommand="gdvHotelBookings_RowCommand">

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
                                            <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="~/images/edit.png" Height="25" ToolTip="Book Hotel"
                                                Width="25" CommandName="BookHotel" CommandArgument='<%# Eval("HotelRequestId") %>' Enabled="true" />
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

                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->

                </div>
                <!--/.col (right) -->

            </div>


        </section>
    </div>
</asp:Content>

