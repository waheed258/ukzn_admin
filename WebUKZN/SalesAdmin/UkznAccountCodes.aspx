<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="UkznAccountCodes.aspx.cs" Inherits="SalesAdmin_UkznAccountCodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Account Codes</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Account Codes</li>
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
                            <h3 class="box-title">Account Codes</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvReport" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
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
                                            <asp:BoundField DataField="accountcode" HeaderText="Account Code"></asp:BoundField>
                                            <asp:BoundField DataField="accountcategory" HeaderText="Account Category"></asp:BoundField>
                                            <asp:BoundField DataField="accountname" HeaderText="Account Name"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
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

