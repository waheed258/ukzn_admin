<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="SalesAdmin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #loadingDiv {
            position: absolute;
            top: 0px;
            right: 0px;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.87);
            background-image: url('ajax-loader.gif');
            background-repeat: no-repeat;
            background-position: center;
            z-index: 10000000;
            opacity: 1.4;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }

        .loading {
            position: absolute;
            left: 50%;
            top: 50%;
            margin: -60px 0 0 -60px;
            background: #fff;
            width: 150px;
            height: 150px;
            border-radius: 100%;
            border: 10px solid #19bee1;
        }

            .loading:after {
                content: '';
                background: trasparent;
                width: 140%;
                height: 140%;
                position: absolute;
                border-radius: 100%;
                top: -20%;
                left: -20%;
                opacity: 0.7;
                box-shadow: rgba(255, 255, 255, 0.6) -4px -5px 3px -3px;
                animation: rotate 2s infinite linear;
            }

        @keyframes rotate {
            0% {
                transform: rotateZ(0deg);
            }

            100% {
                transform: rotateZ(360deg);
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

                 <section class="content-header">
            <h1>CHANGE PASSWORD
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>
                 <section class="content">
                      <asp:HiddenField ID="hfStaffLoginId" runat="server" Value="0" />
                      <asp:HiddenField ID="hfStaffDetailsId" runat="server" Value="0" />
                      <asp:HiddenField ID="hfIsChangePassword" runat="server" />

                <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="form-group">
                                <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                            EnableViewState="false"></asp:Label>
                                </div>
                            <div class="box-header with-border">
                                <h3 class="box-title">CHANGE PASSWORD</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Login Id</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtStaffLoginId" runat="server" CssClass="form-control" Enabled="false" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtStaffLoginId" runat="server" ID="rfvtxtStaffLoginId"
                                            ValidationGroup="subbmit" ErrorMessage="Enter Login Id." Text="Enter Login Id." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Old PassWord</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPassWord" runat="server" class="form-control" MaxLength="15"  />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPassWord" runat="server" ID="rfvtxtPassWord"
                                            ValidationGroup="subbmit" ErrorMessage="Enter Old Password." Text="Enter Old Password." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">New Password</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtNewPassword" runat="server" ID="rfvtxtNewPassword"
                                            ValidationGroup="subbmit" ErrorMessage="Enter New Password." Text="Enter New Password." Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Confirm PassWord</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" />
                                       <asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword" runat="server" ID="rfvtxtConfirmPassword"
                                ValidationGroup="subbmit" ErrorMessage="Enter Confirm Password." Text="Enter Confirm Password."
                                class="validationred" Display="Dynamic" />
                            <asp:CompareValidator runat="server" ID="rfvComparePassword" ControlToValidate="txtNewPassword" ControlToCompare="txtConfirmPassword"
                                Operator="Equal" Type="String" ErrorMessage="The password shoudl be match!" ValidationGroup="subbmit" Display="Dynamic" />
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
                                       <asp:Button runat="server" ID="cmdSubmit" class="btn  btn-info" ValidationGroup="subbmit"
                                           Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
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
            
                     </section>
                 
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
        <ProgressTemplate>
            <div id="loadingDiv">
                <div style="margin: 14% 40%;">
                    <div class="ui yellow huge icon header" id="dimmmer">

                        <div class="loading" style="padding: 51px 20px; font-weight: bold; color: rgb(40, 56, 145)">
                            Please Wait..
                        </div>
                    </div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

