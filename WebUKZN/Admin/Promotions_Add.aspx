<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Promotions_Add.aspx.cs" Inherits="Admin_Promotions_Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
        .style1 {
            color: #FF0000;
        }

    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=dropPromotionCategory.ClientID %>').change(function () {
                var ddlList = $("#<%=dropPromotionCategory.ClientID %>").val();
                if (ddlList == "Hotels") {
                    $("#LblId").text(ddlList);
                }
                else if (ddlList == "Flights") {
                    $("#LblId").text(ddlList);
                }
                else if (ddlList == "Cabs") {
                    $("#LblId").text(ddlList);
                }
                else if (ddlList == "Cruises") {
                    $("#LblId").text(ddlList);
                }
                else if (ddlList == "Packages") {
                    $("#LblId").text(ddlList);
                }
                else {
                    $("#LblId").text("Relavance");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>ADD PROMOTIONS
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">ADD PROMOTIONS</li>
            </ol>
        </section>


        <!-- Main content -->
        <section class="content">
            <asp:Label ID="labelError" class="message" runat="server" Text="" EnableViewState="false"></asp:Label>
            <asp:HiddenField ID="hfPromotionId" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->

                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">ADD PROMOTIONS</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">Promotion  Category<span class="style1">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="dropPromotionCategory" runat="server" class="form-control">
                                            <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Flights" Value="Flights"></asp:ListItem>
                                            <asp:ListItem Text="Hotels" Value="Hotels"></asp:ListItem>
                                            <asp:ListItem Text="Cabs" Value="Cabs"></asp:ListItem>
                                            <asp:ListItem Text="Cruises" Value="Cruises"></asp:ListItem>
                                            <asp:ListItem Text="Packages" Value="Packages"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="dropPromotionCategory" runat="server" ID="rfvdropPromotionCategory"
                                            ValidationGroup="fld_req" ErrorMessage="Select Promotion  Category." Text="Enter Promotion  Category." class="validationred"
                                            Display="Dynamic" InitialValue="-1" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <div id="did"></div>
                                        <label class="control-label" id="LblId" for="Relavance">
                                            Relavance<span class="style1">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtRelavance" runat="server" class="form-control" ReadOnly="false"
                                            MaxLength="10" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtRelavance" runat="server" ID="rfvtxtRelavance"
                                            ValidationGroup="fld_req" ErrorMessage="Enter Relavance." Text="Enter Relavance."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label">
                                            Promotion Code<span class="style1">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPromoCode" runat="server" class="form-control" MaxLength="50" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPromoCode" runat="server" ID="rfvtxtPromoCode"
                                            ValidationGroup="fld_req" ErrorMessage="Enter Promotion Code." Text="Enter Promotion Code."
                                            class="validationred" Display="Dynamic" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label class="control-label" for="PromotionType">
                                            Promotion Type<span class="style1">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="DropPromotionType" runat="server" class="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="-1" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                                            <asp:ListItem Text="International" Value="International"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="DropPromotionType" runat="server" ID="RfvDropPromotionType"
                                            ValidationGroup="fld_req" ErrorMessage="Select Promotion Type." Text="Enter Promotion Type." class="validationred"
                                            Display="Dynamic" InitialValue="-1" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label" for="DiscountTypes">
                                                    Discount Types<span class="style1">*</span></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:RadioButtonList ID="rboDiscountTypes" runat="server" RepeatDirection="Horizontal"
                                                    class="tablerb2 form-control" CellPadding="100" AutoPostBack="true" OnSelectedIndexChanged="rboDiscountTypes_SelectedIndexChanged">
                                                    <asp:ListItem Text="Percentage" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Amount" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div id="divAmount" runat="server">
                                                
                                                    <div class="col-sm-2">
                                                        <b><asp:Label ID="lblAmountTitel" class="control-label" runat="server" Text="Percentage"></asp:Label></b>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return CheckSpecialChar(event)"
                                                            class="form-control" MaxLength="16" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtAmount" runat="server" ID="RfvAmount"
                                                            ValidationGroup="fld_req" ErrorMessage="Enter Amount/Percentage." Text="Enter Amount/Percentage."
                                                            class="validationred" Display="Dynamic" />
                                                        <asp:RegularExpressionValidator ControlToValidate="txtAmount" runat="server"
                                                            ID="revtxtAmount" ValidationExpression="^-*[0-9,\.]+$" ValidationGroup="fld_req"
                                                            ErrorMessage="Enter  only numbers." Text="Enter number only."
                                                            class="validationred" Display="Dynamic" />
                                                    </div>
                                                </div>
                                        </div>


                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label" for="StartDate">
                                            Start Date</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control upper" MaxLength="10"
                                            placeholder="dd-MM-yyyy" onchange="javascript:buttondisable()" />
                                        <cc1:CalendarExtender id="CalendarExtender2" runat="server" enabled="True"
                                            format="dd-MM-yyyy" targetcontrolid="txtStartDate" popupbuttonid="cmdCalendertxtExpiryDate">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtStartDate"
                                            ValidationGroup="fld_req" ErrorMessage="Enter The Start Date ." Text="Enter The Start Date."
                                            class="validationred" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtxtStartDate" runat="server" ErrorMessage="Enter correct Start Date."
                                            Text="Enter The Start Date." ControlToValidate="txtStartDate" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ValidationGroup="fld_req" Display="Dynamic" class="validationred"></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-sm-1">
                                    </div>

                                    <div class="col-sm-2">
                                        <label class="control-label" for="EndDate">
                                            End Date</label>(<span class="style1">*</span>)
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control upper" MaxLength="10"
                                            placeholder="dd-MM-yyyy" onchange="javascript:buttondisable()" />
                                        <cc1:CalendarExtender id="CalendarExtender1" runat="server" enabled="True"
                                            format="dd-MM-yyyy" targetcontrolid="txtEndDate" popupbuttonid="cmdCalendertxtExpiryDate">
                                        </cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ControlToValidate="txtEndDate" runat="server" ID="rfvtxtEndDate"
                                            ValidationGroup="fld_req" ErrorMessage="Enter The End Date ." Text="Enter The End Date."
                                            class="validationred" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="revtxtEndDate" runat="server" ErrorMessage="Enter correct End Date."
                                            Text="Enter The End Date." ControlToValidate="txtEndDate" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ValidationGroup="fld_req" Display="Dynamic" class="validationred"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="cvtxtEndDate" runat="server" ControlToValidate="txtStartDate"
                                            ControlToCompare="txtEndDate" Operator="GreaterThan" Type="Date" Text="Expiry date must greater than Start date."
                                            ErrorMessage="Expiry date must greater than Start date." class="validationred"
                                            ValidationGroup="fld_req" Display="Dynamic"></asp:CompareValidator>
                                    </div>
                                </div>

                               

                           </div>

                           <div class="col-sm-1">
                                    &nbsp
                           </div>
                                <!-- /.box-body -->
                                <div class="box-footer">
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2">

                                            <asp:Button runat="server" ID="cmdCancel"
                                                class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="cmdCancel_Click" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="cmdSubmit" class="btn  btn-info" ValidationGroup="fld_req"
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

                <!-- /.row -->
        </section>
        <!-- /.content -->
</asp:Content>

