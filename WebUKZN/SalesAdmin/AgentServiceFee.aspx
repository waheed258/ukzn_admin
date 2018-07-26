<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="AgentServiceFee.aspx.cs" Inherits="SalesAdmin_AgentServiceFee" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Service Fee Details</h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li><a href="#">Settings</a></li>
                        <li class="active">Service Fee Details</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">

                            <!--/.col (left) -->
                            <!-- right column -->
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Service Fee Details</h3>
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body form-horizontal">
                                        <div class="col-sm-12">
                                            <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                                                EnableViewState="false"></asp:Label>
                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            Service Fee Details
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-2">

                                                <label class="control-label">
                                                    Service Tax%</label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtVatPer" runat="server" class="form-control" MaxLength="10" ReadOnly="true" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Service Fee</label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtServiceFee" runat="server" class="form-control" MaxLength="10" OnTextChanged="txtServiceFee_TextChanged" AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtServiceFee" runat="server" ID="rfvPercentage" ValidationGroup="akki"
                                                    ErrorMessage="Enter Service Fee" Text="Enter Service Fee" class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtServiceFee" runat="server" ID="rxAmountToPay" ValidationGroup="akki"
                                                    ErrorMessage="Enter  number only." Text="Enter  number only."
                                                    ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Service Tax Amount</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtVatFee" runat="server" class="form-control" ReadOnly="true" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Total Fee</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtTotalFee" runat="server" class="form-control" ReadOnly="true" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                                    Text="Submit" OnClick="cmdSubmit_Click" />

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

