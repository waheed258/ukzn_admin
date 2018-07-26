<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="AgentStaffCreation.aspx.cs" Inherits="SalesAdmin_AgentStaffCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Staff Creation</h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li><a href="#">Settings</a></li>
                        <li class="active">Staff Creation</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfStaffLoginId" runat="server" Value="0" />
                    <asp:HiddenField ID="hfStaffDetailsId" runat="server" Value="0" />

                    <div class="row">
                        <!-- left column -->
                        <div class="col-md-12">

                            <!--/.col (left) -->
                            <!-- right column -->
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Staff Creation</h3>
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body form-horizontal">
                                        <div class="col-sm-12">
                                            <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                                                EnableViewState="false"></asp:Label>
                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            STAFF LOGIN DETAILS
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    Login ID</label>
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
                                                <label class="control-label">
                                                    Password
                                                </label>
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
                                                <label class="control-label">
                                                    Confirm Password
                                                </label>
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
                                                <label class="control-label">
                                                    Change Password?
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
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="title" style="font-size: 150%;">
                                            PERSONAL DETAILS
                                        </div>
                                        <div class="form-group">



                                            <div class="col-sm-2">
                                                <label class="control-label">
                                                    First Name
                                                </label>
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
                                                <label class="control-label">
                                                    Last Name
                                                </label>
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
                                                <label class="control-label">
                                                    Mobile Number
                                                </label>
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
                                                <label class="control-label">
                                                    Email
                                                </label>
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
                                                <label class="control-label">
                                                    Designation
                                                </label>
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
                                                <label class="control-label">
                                                    Address
                                                </label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAddress" runat="server" class="form-control" MaxLength="250" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" runat="server" ID="rfvtxtAddress"
                                                    ValidationGroup="subbmit" ErrorMessage="Enter Address." Text="Enter Address."
                                                    class="validationred" Display="Dynamic" />

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

                                                <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary" ValidationGroup="subbmit"
                                                    Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                                                <asp:Button runat="server" ID="btnCancel"
                                                    class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />


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

