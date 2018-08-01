<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="StaffCreation.aspx.cs" Inherits="Admin_StaffCreation" %>

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
    
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Staff</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Staff</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <section class="content">
                    <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                        EnableViewState="false"></asp:Label>
                    <asp:HiddenField ID="hfStaffLoginId" runat="server" Value="0" />
                    <asp:HiddenField ID="hfStaffDetailsId" runat="server" Value="0" />
                    <div class="row">
                        <div class="col-md-12">

                            <!--/.col (left) -->
                            <!-- right column -->
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Staff</h3>
                                    </div>

                                    <div class="box-body form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label for="inputEmail3" class="control-label">STAFF COMPANY DETAILS</label>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Staff Company
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStaffCompany" runat="server" class="form-control">
                                                    <asp:ListItem Value="0" Text="Select Company"></asp:ListItem>

                                                    <asp:ListItem Value="1" Text="SWG WORLDWIDE GROUP"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlStaffCompany" runat="server" ID="rfvddlStaffCompany"
                                                    ValidationGroup="subbmit" ErrorMessage="Select User Company." Text="Select User Company."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Staff Role
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlRole" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlRole" runat="server" ID="rfvddlRole"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Staff Role." Text="Select Staff Role."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Province
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlProvince" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlProvince" runat="server" ID="rfvddlProvince"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Province." Text="Select Province."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                City
                                            </div>

                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCities" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCities_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlCities" runat="server" ID="rfvddlCities"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Cities." Text="Select Cities."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Branch
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlBranch" runat="server" class="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlBranch" runat="server" ID="rfvddlBranch"
                                                    ValidationGroup="subbmit" ErrorMessage="Select Province." Text="Select Province."
                                                    class="validationred" Display="Dynamic" InitialValue="0" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>



                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label for="inputEmail3" class="control-label">STAFF LOGIN DETAILS</label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Login ID
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtStaffLoginId" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtStaffLoginId" runat="server" ID="rfvtxtStaffLoginId"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Login Id." Text="Enter Login Id."
                                                    class="validationred" Display="Dynamic" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Password
                                               
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtPassWord" runat="server" class="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPassWord" runat="server" ID="rfvtxtPassWord"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Password." Text="Enter Password."
                                                    class="validationred" Display="Dynamic" />
                                            </div>

                                        </div>


                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Confirm Password
                                               
                                            </div>
                                            <div class="col-sm-3">

                                                <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword" runat="server" ID="rfvtxtConfirmPassword"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Confirm Password ." Text="Enter Confirm Password."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:CompareValidator runat="server" ID="rfvComparePassword" ControlToValidate="txtPassWord" ControlToCompare="txtConfirmPassword"
                                                    Operator="Equal" Type="String" ErrorMessage="The password shoudl be match!" ValidationGroup="subbmit" Display="Dynamic" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>


                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                Change Password?
                                               
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkChangePassword" runat="server" />

                                            </div>

                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Status
                                              
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label for="inputEmail3" class="control-label">PERSONAL DETAILS</label>
                                            </div>
                                        </div>

                                        <div class="form-group">



                                            <div class="col-sm-2">
                                                First Name
                          
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" ID="rfvtxtFirstName"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter FirstName." Text="Enter FirstName."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Last Name
                            
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" ID="rfvtxtLastName"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Last Name." Text="Enter Last Name."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ControlToValidate="txtEmailId" runat="server" ID="RegularExpressionValidator2" ValidationGroup="akki"
                                                    ErrorMessage="Enter your right Email id" Text="Enter your right Email id" class="validationred"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>

                                        <div class="form-group">



                                            <div class="col-sm-2">
                                                Mobile Number
                           
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtMobileNumber" runat="server" class="form-control" MaxLength="15" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNumber" runat="server" ID="rfvtxtMobileNumber"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter your Mobile number." Text="Enter your Mobile number."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ControlToValidate="txtMobileNumber" runat="server"
                                                    ID="revtxtMobileNumber" ValidationGroup="subbmit" ErrorMessage="Enter your right mobile number."
                                                    Text="Enter your right mobile number." class="validationred" Display="Dynamic"
                                                    ValidationExpression="^[0-9]{10,15}$" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Email
                           
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtEmailId" runat="server" class="form-control" MaxLength="255" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtEmailId" runat="server" ID="rfvtxtEmailId"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter your Email id." Text="Enter your Email id."
                                                    class="validationred" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ControlToValidate="txtEmailId" runat="server" ID="reftxtEmailId" ValidationGroup="subbmit"
                                                    ErrorMessage="Enter your right Email id" Text="Enter your right Email id" class="validationred"
                                                    Display="Dynamic" />
                                            </div>
                                        </div>



                                        <div class="form-group">



                                            <div class="col-sm-2">
                                                Designation
                          
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDesignation" runat="server" class="form-control" MaxLength="100" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtDesignation" runat="server" ID="rfvtxtDesignation"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Designation." Text="Enter Designation."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                Address
                           
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAddress" runat="server" class="form-control" MaxLength="250" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" runat="server" ID="rfvtxtAddress"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Address." Text="Enter Address."
                                                    class="validationred" Display="Dynamic" />

                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <div class="form-group">
                                                <div class="col-sm-5">
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Button runat="server" ID="btnCancel"
                                                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" /> &nbsp;
                                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                                        Text="Submit" OnClick="cmdSubmit_Click" />

                                                </div>
                                            </div>
                                        </div>

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

