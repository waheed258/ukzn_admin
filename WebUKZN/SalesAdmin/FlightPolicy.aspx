﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="FlightPolicy.aspx.cs" Inherits="SalesAdmin_FlightPolicy" %>

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
                    <h1>AirLine Policy
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li><a href="#">Settings</a></li>
                        <li class="active">FlightPolicy</li>
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
                                        <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            Employee Category
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlEmployeeCategory" runat="server" class="form-control" OnSelectedIndexChanged="ddlEmployeeCategory_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Requestor" Value="REQ"></asp:ListItem>
                                                <asp:ListItem Text="Approver L1" Value="AP1"></asp:ListItem>
                                                <asp:ListItem Text="Approver L2" Value="AP2"></asp:ListItem>
                                                <asp:ListItem Text="Accountant" Value="ACC"></asp:ListItem>
                                                <asp:ListItem Text="Buyer" Value="BYE"></asp:ListItem>
                                                <asp:ListItem Text="Creditors" Value="CRD"></asp:ListItem>
                                                <asp:ListItem Text="Financial Mananger" Value="FM"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtAllAirLineMarkupValue" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                            <asp:RegularExpressionValidator ControlToValidate="txtAllAirLineMarkupValue" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                                                ErrorMessage="Enter number only." Text="Enter  number only."
                                                ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="txtAllAirLineMarkupValue" runat="server" ID="rfvPercentage" ValidationGroup="subbmit"
                                                ErrorMessage="Enter MarkUpValue" Text="Enter MarkUpValue" class="validationred" Display="Dynamic" />
                                            <asp:HiddenField ID="hfAllAirlineMarkupId" runat="server" Value="0" />
                                        </div>
                                     <%--   <div class="col-md-6">
                                            <asp:Button runat="server" ID="btnUpdateFlightDoc" class="btn btn-primary green" ValidationGroup="subbmit"
                                                Text="Update" OnClick="btnUpdateFlightDoc_Click" />
                                        </div>--%>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <div class="col-md-5" style="height: 600px; overflow: scroll;">
                                            <asp:GridView ID="gdvAirLineCodes" runat="server" AutoGenerateColumns="false">
                                                <FooterStyle BackColor="#08376a" />
                                                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                                <RowStyle CssClass="gradeA odd" />
                                                <AlternatingRowStyle CssClass="gradeA even" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMove" runat="server" />
                                                            <asp:HiddenField ID="hfAirLinecode" runat="server" Value='<%#Eval("airline_code")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AirLine Logo">
                                                        <ItemTemplate>
                                                            <img src="../DinoSales/airline_logo/<%#Eval("airline_code")%>.gif" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="airline_code" HeaderText="AirLine Code">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="airline_name" HeaderText="AirLine Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-2" style="height: 600px;">
                                            <table style="height: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="bntMoveRight" runat="server" ImageUrl="~/images/right.png" OnClick="bntMoveRight_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgMoveLeft" runat="server" ImageUrl="~/images/left.png" OnClick="imgMoveLeft_Click" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                        <div class="col-md-5" style="height: 600px; overflow: scroll;">
                                            <asp:GridView ID="gdvFlightMarkUp" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data Found">
                                                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                                <RowStyle CssClass="gradeA odd" />
                                                <AlternatingRowStyle CssClass="gradeA even" />
                                                <FooterStyle BackColor="#08376a" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMoveLeft" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prefered Airlines">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfMarkUpId" runat="server" Value='<%#Eval("MarkUpId")%>' />
                                                            <asp:HiddenField ID="hfAirLineCode" runat="server" Value='<%#Eval("airline_code")%>' />
                                                            <%#Eval("airline_name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="MarkUp Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMarkUptype" runat="server" class="form-control">
                                                        <asp:ListItem Value="1" Text="Fixed"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Percentage"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                    <%--   <asp:TemplateField HeaderText="MarkUp Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMarkupValue" runat="server" Text='<%#Eval("MarkUpValue")%>' class="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ControlToValidate="txtMarkupValue" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                                                        ErrorMessage="Enter number only." Text="Enter  number only."
                                                        ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <br />
                                    <br />
                                    Flight Class Policy
                            <div class="form-group">
                                <div class="col-md-5" style="height: 150px;">
                                    <asp:GridView ID="gvCabinclass" runat="server" AutoGenerateColumns="false">
                                        <FooterStyle BackColor="#08376a" />
                                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMove" runat="server" />
                                                    <asp:HiddenField ID="hfCabinClassId" runat="server" Value='<%#Eval("CabinClassId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CabinClassCode" HeaderText="Cabin Class">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2" style="height: 150px;">
                                    <table style="height: 100%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgPolicyCabinMoveRight" runat="server" ImageUrl="~/images/right.png" OnClick="imgPolicyCabinMoveRight_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ingPolicyCabinMoveLeft" runat="server" ImageUrl="~/images/left.png" OnClick="ingPolicyCabinMoveLeft_Click" />
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="col-md-5" style="height: 150px;">
                                    <asp:GridView ID="gvCabinClassPolicy" runat="server" AutoGenerateColumns="false">
                                        <FooterStyle BackColor="#08376a" />
                                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMoveLeft" runat="server" />
                                                    <asp:HiddenField ID="hfCabinPolicySetupId" runat="server" Value='<%#Eval("CabinPolicySetupId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CabinClassCode" HeaderText="Cabin Class">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>


                                    <br />
                                    <br />
                                    Hotel Policy
                           <div class="form-group">
                               <div class="col-md-5" style="height: 150px;">
                                   <asp:GridView ID="gvHotelStars" runat="server" AutoGenerateColumns="false">
                                       <FooterStyle BackColor="#08376a" />
                                       <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                       <RowStyle CssClass="gradeA odd" />
                                       <AlternatingRowStyle CssClass="gradeA even" />
                                       <Columns>
                                           <asp:TemplateField>
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chkMove" runat="server" />
                                                   <asp:HiddenField ID="hfhotelstarsId" runat="server" Value='<%#Eval("hotelstarsId")%>' />
                                               </ItemTemplate>
                                           </asp:TemplateField>

                                           <asp:BoundField DataField="stardescription" HeaderText="Hotel Ratings">
                                               <ItemStyle HorizontalAlign="Center" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </asp:BoundField>

                                       </Columns>
                                   </asp:GridView>
                               </div>
                               <div class="col-md-2" style="height: 150px;">
                                   <table style="height: 100%">
                                       <tr>
                                           <td>
                                               <asp:ImageButton ID="imgHotePolicyRight" runat="server" ImageUrl="~/images/right.png" OnClick="imgHotePolicyRight_Click" />
                                           </td>
                                           <td>
                                               <asp:ImageButton ID="imgHotelLeft" runat="server" ImageUrl="~/images/left.png" OnClick="imgHotelLeft_Click" />
                                           </td>
                                       </tr>

                                   </table>
                               </div>
                               <div class="col-md-5" style="height: 150px;">
                                   <asp:GridView ID="gvHotelRight" runat="server" AutoGenerateColumns="false">
                                       <FooterStyle BackColor="#08376a" />
                                       <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                       <RowStyle CssClass="gradeA odd" />
                                       <AlternatingRowStyle CssClass="gradeA even" />
                                       <Columns>
                                           <asp:TemplateField>
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chkMoveLeft" runat="server" />
                                                   <asp:HiddenField ID="hfHotelPolicyId" runat="server" Value='<%#Eval("HotelPolicyId")%>' />
                                               </ItemTemplate>
                                           </asp:TemplateField>


                                           <asp:BoundField DataField="stardescription" HeaderText="Hotel Ratings">
                                               <ItemStyle HorizontalAlign="Center" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </asp:BoundField>

                                       </Columns>
                                   </asp:GridView>

                               </div>
                           </div>

                                    <br />
                                    <br />
                                    Car Policy
                            <div class="form-group">
                                <div class="col-md-5" style="height: 150px;">
                                    <asp:GridView ID="gvCarPolicy" runat="server" AutoGenerateColumns="false">
                                        <FooterStyle BackColor="#08376a" />
                                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMove" runat="server" />
                                                    <asp:HiddenField ID="hfcabclassid" runat="server" Value='<%#Eval("cabclassid")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="cabclassdescription" HeaderText="Cab Class">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-2" style="height: 150px;">
                                    <table style="height: 100%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgCarRight" runat="server" ImageUrl="~/images/right.png" OnClick="imgCarRight_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgCarLeft" runat="server" ImageUrl="~/images/left.png" OnClick="imgCarLeft_Click" />
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="col-md-5" style="height: 150px;">
                                    <asp:GridView ID="gvCadRight" runat="server" AutoGenerateColumns="false">
                                        <FooterStyle BackColor="#08376a" />
                                        <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                        <RowStyle CssClass="gradeA odd" />
                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMoveLeft" runat="server" />
                                                    <asp:HiddenField ID="hfcabpolicyId" runat="server" Value='<%#Eval("cabpolicyId")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="cabclassdescription" HeaderText="Cab Class">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                                </div>
                            </div>
                        </div>
                </section>
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

