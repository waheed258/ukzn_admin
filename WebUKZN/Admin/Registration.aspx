<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Admin_Registration" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .validationred {
            color: red;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: #00acd6;
            padding-top: 10px;
            padding-left: 10px;
            width: 600px;
            height: 160px;
            border-radius: 6px;
            top: 126px !important;

        }
    </style>
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>

    <script type="text/javascript">
        $(function () {

            $("#<%=txtIssueDate.ClientID  %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%= txtExpiryDate.ClientID  %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $('#<%= myselect.ClientID %>').select2();
        });



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>

            <%-- <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
            --%>
            <!-- ModalPopupExtender -->
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnSearch"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">

                <div class="col-md-12">
                    Search Customer
                    <div>
                        <hr />
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblRegNumber" runat="server" Text="Enter Phone Number"></asp:Label>
                    </div>

                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegNumber"></asp:TextBox>
                    </div>
                    <br />
                    <br />
                    <div class="col-md-12">

                        <asp:Button runat="server" ID="btnSerach" Text="Search" class="btn  btn-info" />
                        &nbsp; &nbsp;
                    
                            <asp:Button ID="btnClose" runat="server" Text="Close" class="btn  btn-warning" />

                    </div>
                </div>



            </asp:Panel>

            <section class="content-header">
                <h1>Registration</h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li><a href="#">Settings</a></li>
                    <li class="active">Registration</li>
                </ol>
            </section>
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                          <div class="box-header with-border">
                            <h3 class="box-title">Registration</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="col-sm-12 margin-bottom">
                            </div>
                            <div class="col-sm-12 margin-bottom">
                                <div class="col-sm-3">
                                    <asp:Button ID="btnSearch" runat="server" class="btn  btn-info" Text="SEARCH CUSTOMER" />
                                </div>
                            </div>

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="FirstName" class="control-label">First Name<span class="validationred">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" ID="rfvtxtFirstName"
                                            ValidationGroup="Reg" ErrorMessage="Enter First Name." Text="Enter First Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="LastName" class="control-label">Last Name<span class="validationred">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" ID="rfvtxtLastName"
                                            ValidationGroup="Reg" ErrorMessage="Enter Last Name." Text="Enter Last Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="Title" class="control-label">Title</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlTitle" runat="server" class="form-control">
                                            <asp:ListItem Text="Mr" Value="1" Selected="True">Mr</asp:ListItem>
                                            <asp:ListItem Text="Ms" Value="2">Ms</asp:ListItem>
                                            <asp:ListItem Text="Mrs" Value="3">Mrs</asp:ListItem>
                                        </asp:DropDownList>




                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="Email" class="control-label">Email<span class="validationred">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ID="rfvtxtEmail"
                                            ValidationGroup="Reg" ErrorMessage="Enter Email." Text="Enter Email." Display="Dynamic"
                                            class="validationred" />
                                        <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server"
                                            ID="revtxtEmail" ValidationGroup="Reg" ErrorMessage="Enter valid Email Id."
                                            Text="Enter valid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="MobileNo" class="control-label">Mobile No<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtMobileNo" runat="server" class="form-control" MaxLength="15" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" runat="server" ID="rfvtxtMobileNo"
                                            ValidationGroup="Reg" ErrorMessage="Enter Mobile No." Text="Enter Mobile No." Display="Dynamic"
                                            class="validationred" />
                                        <asp:RegularExpressionValidator ControlToValidate="txtMobileNo" runat="server"
                                            ID="revtxtMobileNo" ValidationGroup="Reg" ErrorMessage="Enter number only."
                                            Text="Enter number only." ValidationExpression="^[0-9]{10,15}$"
                                            class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="Address" class="control-label">Address</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="PassportNo" class="control-label">Passport No</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtPassportNo" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="IssueDate" class="control-label">Issue Date</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtIssueDate" runat="server" class="form-control" />
                                        <%--    <cc1:CalendarExtender ID="caltxtIssueDate" runat="server" Enabled="True"
                                            Format="dd-MM-yyyy" TargetControlID="txtIssueDate" PopupButtonID="cmdCalender2"></cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="revtxtIssueDate" runat="server" ErrorMessage="Enter date in (dd-mm-yyyy) format."
                                            Text="Enter date in (dd-mm-yyyy) format." ControlToValidate="txtIssueDate"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ValidationGroup="Reg" Display="Dynamic" class="validationred"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="ExpiryDate" class="control-label">Expiry Date</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtExpiryDate" runat="server" class="form-control" />
                                        <%--    <cc1:CalendarExtender ID="CaltxtExpiryDate" runat="server" Enabled="True"
                                            Format="dd-MM-yyyy" TargetControlID="txtExpiryDate" PopupButtonID="cmdCalender2"></cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="revtxtExpiryDate" runat="server" ErrorMessage="Enter date in (dd-mm-yyyy) format."
                                            Text="Enter date in (dd-mm-yyyy) format." ControlToValidate="txtExpiryDate"
                                            ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
                                            ValidationGroup="Reg" Display="Dynamic" class="validationred"></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="cvtxtExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                            ControlToCompare="txtIssueDate" Operator="GreaterThan" Type="Date"
                                            Text="Passport Expiry Date must greater than Passport Issue Date." ErrorMessage="Passport Expiry Date must greater than Passport Issue Date."
                                            class="validationred" Display="Dynamic" ValidationGroup="Reg">
                                        </asp:CompareValidator>--%>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="myselect" runat="server" class="form-control">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>venki</asp:ListItem>
                                        <asp:ListItem>venu</asp:ListItem>
                                        <asp:ListItem>charles ven</asp:ListItem>
                                        <asp:ListItem>venuzila</asp:ListItem>
                                        <asp:ListItem>veron philender</asp:ListItem>
                                        <asp:ListItem>india</asp:ListItem>
                                        <asp:ListItem>indianven</asp:ListItem>
                                        <asp:ListItem>vesta</asp:ListItem>
                                    </asp:DropDownList>
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
                                        <asp:Button runat="server" ID="btnSave" class="btn  btn-info" ValidationGroup="Reg"
                                            Text="SAVE AND CONTINUE" OnClick="btnSave_Click" />&nbsp;
                   

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

