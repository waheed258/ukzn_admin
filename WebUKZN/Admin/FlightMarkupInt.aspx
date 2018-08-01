<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="FlightMarkupInt.aspx.cs" Inherits="Admin_FlightMarkupInt" %>

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

            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>International Flight Price Markup Settings         
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Settings</a></li>
                    <li class="active">FlightMarkupInt</li>
                </ol>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title"></h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Label ID="labelError" class="message" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Class Code</label>
                                    </div>
                                    <div class="col-sm-3">

                                        <asp:DropDownList ID="ddlClass" runat="server" class="form-control" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                            <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>

                                            <asp:ListItem Value="E" Text="E"></asp:ListItem>
                                            <asp:ListItem Value="F" Text="F"></asp:ListItem>
                                            <asp:ListItem Value="G" Text="G"></asp:ListItem>
                                            <asp:ListItem Value="H" Text="H"></asp:ListItem>

                                            <asp:ListItem Value="I" Text="I"></asp:ListItem>
                                            <asp:ListItem Value="J" Text="J"></asp:ListItem>
                                            <asp:ListItem Value="K" Text="K"></asp:ListItem>
                                            <asp:ListItem Value="L" Text="L"></asp:ListItem>

                                            <asp:ListItem Value="M" Text="M"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                            <asp:ListItem Value="O" Text="O"></asp:ListItem>
                                            <asp:ListItem Value="P" Text="P"></asp:ListItem>
                                            <asp:ListItem Value="Q" Text="Q"></asp:ListItem>

                                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                            <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>

                                            <asp:ListItem Value="R" Text="R"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="S"></asp:ListItem>
                                            <asp:ListItem Value="T" Text="T"></asp:ListItem>
                                            <asp:ListItem Value="U" Text="U"></asp:ListItem>

                                            <asp:ListItem Value="V" Text="V"></asp:ListItem>
                                            <asp:ListItem Value="W" Text="W"></asp:ListItem>
                                            <asp:ListItem Value="X" Text="X"></asp:ListItem>
                                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                                            <asp:ListItem Value="Z" Text="Z"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Markup Type</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:HiddenField ID="hfMarkUpId" runat="server" Value="0" />
                                        <asp:DropDownList ID="ddlMarkUptype" runat="server" class="form-control">
                                            <asp:ListItem Value="1" Text="Fixed"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Percentage"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Markup Value</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtMarkUpValue" runat="server" class="form-control" MaxLength="5" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtMarkUpValue" runat="server" ID="rfvPercentage" ValidationGroup="subbmit"
                                            ErrorMessage="Enter MarkUpValue" Text="Enter MarkUpValue" class="validationred" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ControlToValidate="txtMarkUpValue" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                                            ErrorMessage="Enter  number only." Text="Enter  number only."
                                            ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Segment Level</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:CheckBox ID="chkSegmentLevel" runat="server" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Passenger Level</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:CheckBox ID="chkPassengerLevel" runat="server" />
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
            </section>
            <br />

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

