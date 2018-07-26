<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="AccountantSetups.aspx.cs" Inherits="SalesAdmin_AccountantSetups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .validationred {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Accountant Override</h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li><a href="#">Settings</a></li>
                        <li class="active">Accountant Override</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hfaccountoverride" runat="server" Value="0" />

                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">
                            <!--/.col (left) -->
                            <!-- right column -->
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Accountant Override</h3>
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-3">
                                                <label class="control-label">Account OverRide Amount<span class="validationred"> *</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtBudgetOverRide" runat="server" class="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator ControlToValidate="txtBudgetOverRide" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                                                    ErrorMessage="Enter number only." Text="Enter  number only."
                                                    ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="txtBudgetOverRide" runat="server" ID="rfvPercentage" ValidationGroup="subbmit"
                                                    ErrorMessage="Enter MarkUpValue" Text="Enter MarkUpValue" class="validationred" Display="Dynamic" />
                                                <asp:HiddenField ID="hfAllAirlineMarkupId" runat="server" Value="0" />
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Button runat="server" ID="btnUpdateFlightDoc" class="btn btn-primary green" ValidationGroup="subbmit"
                                                    Text="Submit" OnClick="btnUpdateFlightDoc_Click" />
                                            </div>

                                        </div>


                                    </div>


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

