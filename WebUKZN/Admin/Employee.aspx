<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Admin_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .validationred {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Employee
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <asp:HiddenField ID="hf_UserMasterId" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Employee</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">First Name<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserFirstName" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserFirstName" runat="server" ID="rfvtxtEmpFirstName"
                                            ValidationGroup="Employee" ErrorMessage="Enter First Name." Text="Enter First Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Last Name<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserLastName" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserLastName" runat="server" ID="rfvtxtEmpLastName"
                                            ValidationGroup="Employee" ErrorMessage="Enter Last Name." Text="Enter Last Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Email <span class="validationred">*</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserMail" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserMail" runat="server" ID="rfvEmpEmail"
                                            ValidationGroup="Employee" ErrorMessage="Enter Email." Text="Enter Email." Display="Dynamic"
                                            class="validationred" />
                                        <asp:RegularExpressionValidator ControlToValidate="txtUserMail" runat="server"
                                            ID="revtxtEmail" ValidationGroup="Employee" ErrorMessage="Enter Valid Email Id."
                                            Text="Enter Valid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Phone<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserPhone" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserPhone" runat="server" ID="rfvEmpPhone"
                                            ValidationGroup="Employee" ErrorMessage="Enter Mobile." Text="Enter Mobile." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Password<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtUserPwd" runat="server" class="form-control" TextMode="Password" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUserPwd" runat="server" ID="rfvEmpPassword"
                                            ValidationGroup="Employee" ErrorMessage="Enter Password." Text="Enter Password." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Role<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlUserRole" runat="server" class="form-control">
                                            <%--<asp:ListItem Text="Select" Value="-1">Select</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlUserRole" runat="server" ID="rfvUserRole"
                                            ValidationGroup="Employee" ErrorMessage="Select Role." Text="Select Role." Display="Dynamic"
                                            class="validationred" InitialValue="-1" />

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Status</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlUserStatus" runat="server" class="form-control">
                                            <asp:ListItem Text="Select" Value="-1">Select</asp:ListItem>

                                            <asp:ListItem Text="In active" Value="0">In active</asp:ListItem>
                                            <asp:ListItem Text="Active" Value="1">Active</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Branch</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlUserBranch" runat="server" class="form-control">
                                            <asp:ListItem Text="Select" Value="-1">Select</asp:ListItem>
                                        </asp:DropDownList>
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

                                        <asp:Button runat="server" ID="btnUserCancel"
                                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnUserCancel_Click" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnUseSave" class="btn  btn-info" ValidationGroup="Employee"
                                           Text="Submit" OnClick="btnUseSave_Click" />&nbsp;
                   

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

