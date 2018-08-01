<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminInvoicePdf.aspx.cs" Inherits="Admin_AdminInvoicePdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Admin Invoice PDF</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Admin Invoice PDF</li>
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
                            <h3 class="box-title">Admin Invoice PDF</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->


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

</asp:Content>

