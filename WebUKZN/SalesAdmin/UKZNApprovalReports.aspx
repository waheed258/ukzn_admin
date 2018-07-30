<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="UKZNApprovalReports.aspx.cs" Inherits="SalesAdmin_UKZNApprovalReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Approvals Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Approvals Report</li>
        </ol>
    </section>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Approvals Report</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <asp:GridView ID="gvApprovalReports" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
                            AutoGenerateColumns="False"
                            Width="100%" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
                            <AlternatingRowStyle CssClass="gradeA even" />
                            <FooterStyle BackColor="#08376a" />
                            <RowStyle CssClass="gradeA odd" />
                            <Columns>

                                <asp:TemplateField HeaderText="SN">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="approval_costcenter" HeaderText="Cost Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="Requester Name">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />

                                </asp:BoundField>
                                <asp:BoundField DataField="created_date" HeaderText="Create Date & Time">
                                    <ItemStyle HorizontalAlign="Right" />


                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>

                    </div>

                    <!-- /.box-body -->

                </div>
                <!-- /.box -->


            </div>

        </div>
        <!-- /.row -->
    </section>

</asp:Content>

