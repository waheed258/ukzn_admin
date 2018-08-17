<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Policysetup.aspx.cs" Inherits="Admin_Policysetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>All</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Policy</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
      <asp:HiddenField ID="hf_CategoryId" runat="server" Value="0" />
        <div class="row">
            <!-- left column -->
            <div class="col-md-12">

                <!--/.col (left) -->
                <!-- right column -->
                <div class="col-md-12">
                    <!-- Horizontal Form -->
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Policy</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">




                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Rule Value</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtRuleValue" runat="server" class="form-control" />
                                </div>
                                <div class="col-sm-1">
                                </div>

                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Category</label>
                                </div>

                                <div class="col-sm-3"> 
                                    <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control"  
                                        OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">Category Type</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="drpCategoryType" runat="server" CssClass="form-control">
                                      
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                </div>

                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Condation</label>
                                </div>

                                <div class="col-sm-3">
                                    <asp:DropDownList ID="drpCondation" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Avilable</asp:ListItem>
                                        <asp:ListItem Value="2">Not Avilable</asp:ListItem>
                                        <asp:ListItem Value="3">Partially Allowed</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                            </div>

                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <div class="form-group">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-info" ValidationGroup="Policysetup" Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                                    <asp:Button runat="server" ID="btnCancel" class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>


                        </div>
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

