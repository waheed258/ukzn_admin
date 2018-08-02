<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="MoveFlightSalesToFin.aspx.cs" Inherits="Admin_MoveFlightSalesToFin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <section class="content-header">
            <h1>Flight Bookings
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Flight Bookings</li>
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
                            <h3 class="box-title">Flight Bookings</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="form-group">

                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="Move To Finance" OnClick="cmdSubmit_Click" />

                                </div>

                            </div>
                            <div class="form-group">
                                <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    DataKeyNames="FlightRequestId"
                                     AllowSorting="true" ShowFooter="true"
                                    AutoGenerateColumns="False"
                                    Width="100%" PageSize="100" usecustompager="true">

                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                <asp:HiddenField ID="hfFlightRequestId" runat="server" Value='<%#Eval("FlightRequestId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SN">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
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

                                        <asp:TemplateField HeaderText="Booking Date">
                                            <ItemTemplate>
                                                <%#Eval("BookingDate")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Journey Date">
                                            <ItemTemplate>
                                                <%#Eval("FlightDate")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <%#Eval("TotalPrice")%>
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

