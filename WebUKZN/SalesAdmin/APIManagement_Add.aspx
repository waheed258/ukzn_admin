<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="APIManagement_Add.aspx.cs" Inherits="SalesAdmin_APIManagement_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>ADD API  CONFIGURATION</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">ADD API  CONFIGURATION</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <asp:HiddenField ID="hfLineSeqid" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">ADD API  CONFIGURATION</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="col-sm-12">
                                    <asp:Label ID="labelError" class="message" runat="server" Text="" EnableViewState="false"></asp:Label>
                                </div>

                               <%-- <div class="title" style="font-size: 120%;">
                                    ADD API  CONFIGURATION
                                </div>--%>


                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            PCC Code(<span class="style1">*</span>)</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPCCCode" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPCCCode" runat="server" ID="rfvtxtPCCCode"
                                            ValidationGroup="fld_req" ErrorMessage="Enter PCC Code." Text="Enter PCC Code."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label" id="LblId" for="APIName">
                                            API Name</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtAPIName" runat="server" class="form-control" ReadOnly="false"
                                            MaxLength="10" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAPIName" runat="server" ID="rfvAPIName"
                                            ValidationGroup="fld_req" ErrorMessage="Enter API Name." Text="Enter API Name."
                                            class="validationred" Display="Dynamic" />

                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            UserName(<span class="style1">*</span>)</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserName" runat="server" ID="rfvtxtUserName"
                                            ValidationGroup="fld_req" ErrorMessage="Enter UserName." Text="Enter UserName."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            PassWord(<span class="style1">*</span>)</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPassWord" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPassWord" runat="server" ID="rfvtxtPassWord"
                                            ValidationGroup="fld_req" ErrorMessage="Enter PassWord." Text="Enter PassWord."
                                            class="validationred" Display="Dynamic" />
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label" for="ProductionURL">
                                            Production URL</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtProductionURL" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtProductionURL" runat="server" ID="rfvtxtProductionURL"
                                            ValidationGroup="fld_req" ErrorMessage="Enter Add." Text="Enter Add."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>

                                    <div class="col-sm-2">
                                        <label class="control-label" for="ExpiryDate">
                                            Test Url</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtTestUrl" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtTestUrl" runat="server" ID="rfvtxtTestUrl"
                                            ValidationGroup="fld_req" ErrorMessage="Enter Test Url." Text="Enter Test Url."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label" for="ValidUntil">
                                            Is Active</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="dropIsActive" runat="server" class="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="dropIsActive" runat="server" ID="rfvdropIsActive"
                                            ValidationGroup="fld_req" ErrorMessage="Select Status." Text="Enter Status." class="validationred"
                                            Display="Dynamic" InitialValue="0" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="fld_req"
                                        ShowSummary="true" Display="Dynamic" CssClass="validationred" />
                                </div>

                                <%--<div class="form-group">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="fld_req"
                                            OnClick="cmdSubmit_Click" Text="Submit" />&nbsp;<asp:Button runat="server" ID="cmdCancel"
                                                class="btn btn-primary red" Text="Cancel" OnClick="cmdCancel_Click" />
                                    </div>
                                </div>--%>
                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-2">

                                        <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary" ValidationGroup="fld_req"
                                            OnClick="cmdSubmit_Click" Text="Submit" />&nbsp;
                                        <asp:Button runat="server" ID="cmdCancel"
                                            class="btn btn-danger" Text="Cancel" OnClick="cmdCancel_Click" />

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
    </div>
</asp:Content>

