<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="ManageAgentProfile.aspx.cs" Inherits="SalesAdmin_ManageAgentProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #loadingDiv {
            position: absolute;
            top: 0px;
            right: 0px;
            width: 100%;
            height: 1500px;
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
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>AGENT DETAILS
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">AGENT DETAILS</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hfAgentLoginId" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfAgentDetailsId" runat="server" Value="0" />

                                    <div class="box-body form-horizontal">
                                        <div class="title" style="font-size: 150%;">
                                            AGENT COMPANY DETAILS
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Agent Company</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlAgentCompany" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Select Company"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="SERENDIPITY TRAVEL"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="SWG "></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlAgentCompany" runat="server" ID="rfvddlAgentCompany"
                                                    ValidationGroup="submit" ErrorMessage="Select User Company." Text="Select User Company."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Role</label>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="3" Text="Agent"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Agent Company Name</label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtCompanyName" runat="server" ID="rfvtxtCompanyName"
                                                    ValidationGroup="submit" ErrorMessage="Enter Company Name." Text="Enter Company Name."
                                                    class="validationred" Display="Dynamic" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Agent Logo</label>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:FileUpload ID="fuAgentLogo" runat="server" CssClass="form-control" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fuAgentLogo"
                                                    ErrorMessage="Invalid  File" class="validationred" ValidationGroup="submit"
                                                    ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.gif|.GIF|.png|.PNG)$"> 
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Company Mobile No
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNumber" runat="server" ID="rfvtxtMobileNumber"
                                                    ValidationGroup="submit" ErrorMessage="Enter your Mobile number." Text="Enter your Mobile number."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtMobileNumber" runat="server"
                                                    ID="revtxtMobileNumber" ValidationGroup="submit" ErrorMessage="Enter your right mobile number."
                                                    Text="Enter your right mobile number." class="validationred" Display="Dynamic"
                                                    ValidationExpression="^[0-9]{10,15}$" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Company   Email Id
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" MaxLength="255" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtEmailId" runat="server" ID="rfvtxtEmailId"
                                                    ValidationGroup="submit" ErrorMessage="Enter your Email id." Text="Enter your Email id."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ControlToValidate="txtEmailId" runat="server" ID="reftxtEmailId" ValidationGroup="submit"
                                                    ErrorMessage="Enter your right Email id" Text="Enter your right Email id" class="validationred"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Province</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlProvince" runat="server" ID="rfvddlProvince"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Province." Text="Select Province."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Cities</label>
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCities" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCities" runat="server" ID="rfvddlCities"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Cities." Text="Select Cities."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            AGENT LOGIN DETAILS
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Login Id</label>
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtAgentLoginId" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAgentLoginId" runat="server" ID="rfvtxtAgentLoginId"
                                                    ValidationGroup="submit" ErrorMessage="Enter Login Id." Text="Enter Login Id."
                                                    class="validationred" Display="Dynamic" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    PassWord
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtPassWord" runat="server" CssClass="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPassWord" runat="server" ID="rfvtxtPassWord"
                                                    ValidationGroup="submit" ErrorMessage="Enter Password." Text="Enter Password."
                                                    class="validationred" Display="Dynamic" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Is Change Password?
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkChangePassword" runat="server" />

                                            </div>

                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Status
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="In Active"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Fax No
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFaxNo" runat="server" CssClass="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtFaxNo" runat="server" ID="rfvtxtFaxNo"
                                                    ValidationGroup="submit" ErrorMessage="Enter Fax No." Text="Enter Fax No."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtFaxNo" runat="server"
                                                    ID="retxtFaxNo" ValidationGroup="submit" ErrorMessage="Enter your right fax number."
                                                    Text="Enter your right fax number." class="validationred" Display="Dynamic"
                                                    ValidationExpression="^[0-9]{10,15}$" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Address
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" MaxLength="250" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" runat="server" ID="rfvtxtAddress"
                                                    ValidationGroup="submit" ErrorMessage="Enter Address." Text="Enter Address."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Web Site
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtWebSite" runat="server" CssClass="form-control" MaxLength="100" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            CONTACT PERSON  DETAILS
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    First Name
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" ID="rfvtxtFirstName"
                                                    ValidationGroup="submit" ErrorMessage="Enter FirstName." Text="Enter FirstName."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Last Name
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" ID="rfvtxtLastName"
                                                    ValidationGroup="submit" ErrorMessage="Enter Last Name." Text="Enter Last Name."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Mobile No
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtContMobileNo" runat="server" CssClass="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtContMobileNo" runat="server" ID="rfvtxtContMobileNo"
                                                    ValidationGroup="submit" ErrorMessage="Enter  Mobile number." Text="Enter  Mobile number."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtContMobileNo" runat="server"
                                                    ID="RegularExpressionValidator4" ValidationGroup="submit" ErrorMessage="Enter  right mobile number."
                                                    Text="Enter  right mobile number." class="validationred" Display="Dynamic"
                                                    ValidationExpression="^[0-9]{10,15}$" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Email Id
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtContEmailId" runat="server" CssClass="form-control" MaxLength="255" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtContEmailId" runat="server" ID="rfvtxtContEmailId"
                                                    ValidationGroup="submit" ErrorMessage="Enter  Email id." Text="Enter  Email id."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ControlToValidate="txtContEmailId" runat="server" ID="retxtContEmailId" ValidationGroup="submit"
                                                    ErrorMessage="Enter  right Email id" Text="Enter  right Email id" class="validationred"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Designation
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtDesignation" runat="server" ID="rfvtxtDesignation"
                                                    ValidationGroup="submit" ErrorMessage="Enter  Designation." Text="Enter  Designation."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            AGENT OTHER  DETAILS
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    IATA No
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtIataNo" runat="server" CssClass="form-control" MaxLength="15" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Business License
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtBusinessLicense" runat="server" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtBusinessLicense" runat="server" ID="rfvtxtBusinessLicense"
                                                    ValidationGroup="submit" ErrorMessage="Enter  Business License." Text="Enter  Business License."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Pseudo Code
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtPseudoCode" runat="server" CssClass="form-control" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Is Approved
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkIsApprove" runat="server" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Overdraft Y/N?
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkIsOverDraft" runat="server" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div id="overDraftLimi" runat="server">
                                                <div class="col-sm-2">
                                                    <label class="control-label">
                                                        OverdraftLimitAmt
                                                    </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtOverDraftLimt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtOverDraftLimt" runat="server" ID="rfvtxtOverDraftLimt"
                                                        ValidationGroup="submit" ErrorMessage="Enter  Business License." Text="Enter  Business License."
                                                        class="validationred" Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                                                        ControlToValidate="txtOverDraftLimt" runat="server" ID="retxtOverDraftLimt" ValidationGroup="submit"
                                                        ErrorMessage="Enter Correct Amount" Text="Enter Correct Amount" class="validationred"
                                                        Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label" style="text-align: left">
                                                    Domestic Commission Percentage
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtdomcmper" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtdomcmper" runat="server" ID="rfvtxtdomcmper"
                                                    ValidationGroup="submit" ErrorMessage="Domestic Commission Percentage." Text="Domestic Commission Percentage."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                                                    ControlToValidate="txtdomcmper" runat="server" ID="rextxtdomcmper" ValidationGroup="submit"
                                                    ErrorMessage="Correct Domestic Commission Percentage." Text="Correct Domestic Commission Percentage." class="validationred"
                                                    Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div>
                                                <div class="col-sm-2">
                                                    <label class="control-label" style="text-align: left">
                                                        Domestic Commission Rate
                                                    </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtdomcmAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtdomcmAmount" runat="server" ID="rfvtxtdomcmAmount"
                                                        ValidationGroup="submit" ErrorMessage="Domestic Commission Amount" Text="Domestic Commission Amount"
                                                        class="validationred" Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                                                        ControlToValidate="txtdomcmAmount" runat="server" ID="rxtxtdomcmAmount" ValidationGroup="submit"
                                                        ErrorMessage="Correct Domestic Commission Amount" Text="Correct Domestic Commission Amount" class="validationred"
                                                        Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label" style="text-align: left">
                                                    International Commission Percentage
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtintcmper" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtintcmper" runat="server" ID="rfvtxtintcmper"
                                                    ValidationGroup="submit" ErrorMessage="International Commission Percentage." Text="Domestic Commission Percentage."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                                                    ControlToValidate="txtintcmper" runat="server" ID="rxtxtintcmper" ValidationGroup="submit"
                                                    ErrorMessage="Correct International Commission Percentage." Text="Correct International Commission Percentage." class="validationred"
                                                    Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div id="Div2" runat="server">
                                                <div class="col-sm-2">
                                                    <label class="control-label" style="text-align: left">
                                                        International Commission Rate
                                                    </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtintcmamt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtintcmamt" runat="server" ID="rfvtxtintcmamt"
                                                        ValidationGroup="submit" ErrorMessage="International Commission Amount" Text="International Commission Amount"
                                                        class="validationred" Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ValidationExpression="^[0-9]+(\.[0-9]+)?$"
                                                        ControlToValidate="txtintcmamt" runat="server" ID="rxtxtintcmamt" ValidationGroup="submit"
                                                        ErrorMessage="Correct International Commission Amount" Text="Correct International Commission Amount" class="validationred"
                                                        Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label" style="text-align: left">
                                                    Commisssion Active Type
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlComActType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Percentage"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Rate"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                                                EnableViewState="false"></asp:Label>
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

                                                <%--  <asp:Button runat="server" ID="btnUserCancel"
                                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnUserCancel_Click" />&nbsp; &nbsp;--%>
                                                <asp:Button runat="server" ID="cmdSubmit" class="btn  btn-info" ValidationGroup="submit"
                                                    Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                   

                                            </div>
                                        </div>


                                    </div>
                                    <!-- /.box-footer -->
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

