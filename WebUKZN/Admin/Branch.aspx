<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Branch.aspx.cs" Inherits="Admin_Branch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Branch</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Branch</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <asp:HiddenField ID="hf_BranchId" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title"></h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label"> Name</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBranchName" runat="server" class="form-control" />
                                         <asp:RequiredFieldValidator ControlToValidate="txtBranchName" runat="server" ID="rfvBranchName"
                                            ValidationGroup="BranchValid" ErrorMessage="Enter Name." Text="Enter Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Email</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBranchEmail" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtBranchEmail" runat="server" ID="rfvBranchEmail"
                                            ValidationGroup="BranchValid" ErrorMessage="Enter Email." Text="Enter Email." Display="Dynamic"
                                            class="validationred" />
                                        <asp:RegularExpressionValidator ControlToValidate="txtBranchEmail" runat="server"
                                            ID="revtxtEmail" ValidationGroup="BranchValid" ErrorMessage="Enter Valid Email Id."
                                            Text="Enter Valid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Mobile No</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBranchMobile" runat="server" class="form-control" />
                                         <asp:RequiredFieldValidator ControlToValidate="txtBranchMobile" runat="server" ID="rfvBranchMobile"
                                            ValidationGroup="BranchValid" ErrorMessage="Enter Mobile." Text="Enter Mobile." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Address</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBranchAddress" runat="server" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Contact Person</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtBranchContactPerson" runat="server" class="form-control" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtBranchContactPerson" runat="server" ID="rfvBranchContactPerson"
                                            ValidationGroup="BranchValid" ErrorMessage="Enter contact person Name." Text="Enter contact person Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                   
                               
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Status</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlBranchStatus" runat="server" class="form-control">
                                            <asp:ListItem Text="Select" Value="-1">Select</asp:ListItem>
                                            
                                            <asp:ListItem Text="In active" Value="0">In active</asp:ListItem>
                                            <asp:ListItem Text="Active" Value="1">Active</asp:ListItem>

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

                                        <asp:Button runat="server" ID="btnBranchCancel"
                                            class="btn btn-danger" ValidationGroup="" Text="Cancel"  OnClick="btnBranchCancel_Click" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnBranchSave" class="btn  btn-info" ValidationGroup="BranchValid"
                                           Text="Submit"  OnClick="btnBranchSave_Click" />&nbsp;
                   

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

