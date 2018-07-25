<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="QuotationMaster.aspx.cs" Inherits="Admin_QuotationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <link href="bootstrap/css/jquery.dataTables.css" rel="stylesheet" />
    <link href="bootstrap/css/dataTables.bootstrap4.css" rel="stylesheet" />
    <script src="bootstrap/js/jquery.dataTables.js"></script>
    <script src="bootstrap/js/dataTables.bootstrap4.js"></script>--%>

    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    <style type="text/css">
        .pagination_grid {
            line-height: 25px;
        }

            .pagination_grid span {
                padding-left: 9px;
                padding-right: 9px;
                padding-top: 6px;
                padding-bottom: 6px;
                border: solid 1px #08c;
                text-decoration: none;
                white-space: nowrap;
                background: #08c;
                border-radius: 6px;
                border: 1px solid #ddd;
            }

            .pagination_grid a,
            .pagination_grid a:visited {
                text-decoration: none;
                padding-left: 9px;
                padding-right: 9px;
                padding-top: 6px;
                padding-bottom: 6px;
                white-space: nowrap;
            }

                .pagination_grid a:hover,
                .pagination_grid a:active {
                    padding-left: 9px;
                    padding-right: 9px;
                    padding-top: 6px;
                    padding-bottom: 6px;
                    text-decoration: none;
                    white-space: nowrap;
                    background: #08c;
                    border-radius: 6px;
                }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#target").keyup(function () {
                if ($("[id *=target]").val() != "") {
                    $("[id *=ContentPlaceHolder1_GvQuotationList]").children
                    ('tbody').children('tr').each(function () {
                        $(this).show();
                    });
                    $("[id *=ContentPlaceHolder1_GvQuotationList]").children
                    ('tbody').children('tr').each(function () {
                        var match = false;
                        $(this).children('td').each(function () {
                            if ($(this).text().toUpperCase().indexOf($("[id *=target]").val().toUpperCase()) > -1) {
                                match = true;
                                return false;
                            }
                        });
                        if (match) {
                            $(this).show();
                            $(this).children('th').show();
                        }
                        else {
                            $(this).hide();
                            $(this).children('th').show();
                        }
                    });


                    $("[id *=ContentPlaceHolder1_GvQuotationList]").children('tbody').
                            children('tr').each(function (index) {
                                if (index == 0)
                                    $(this).show();
                            });
                }
                else {
                    $("[id *=ContentPlaceHolder1_GvQuotationList]").children('tbody').
                            children('tr').each(function () {
                                $(this).show();
                            });
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Quotations List</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Quotations</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- Info boxes -->

            <!-- /.row -->

            <!-- Main row -->
            <div class="row">
                <!-- Left col -->
                <div class="col-md-12">

                    <!-- TABLE: LATEST ORDERS -->
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Quotations</h3>
                            <div>
                            </div>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DropPage" runat="server" OnSelectedIndexChanged="DropPage_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                        <asp:ListItem Value="25">25</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-8">
                                </div>
                                <input id="target" class="form-control" type="text" placeholder="search" style="width: 150px;" />
                            </div>
                            <div>
                                <hr />
                            </div>
                            <div class="card-body">
                                <!-- /.Quotation Master Grid -responsive -->
                                <asp:GridView ID="GvQuotationList" runat="server" AllowPaging="true" EmptyDataText="No Data Found"
                                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    Width="100%" OnPageIndexChanging="GvQuotationList_PageIndexChanging" OnRowDataBound="GvQuotationList_RowDataBound" PageSize="10">

                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="TravellerId">
                                            <ItemTemplate>
                                                <%#Eval("TravellerId")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%#Eval("Status")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QuotationRefNo">
                                            <ItemTemplate>
                                                <%#Eval("QuotationRefNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ApprovalStatus">
                                            <ItemTemplate>
                                                <%#Eval("ApprovalStatus")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TripAmount">
                                            <ItemTemplate>
                                                <%#Eval("TripAmount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BudgetAmount">
                                            <ItemTemplate>
                                                <%#Eval("BudgetAmount")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination_grid" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" Position="Bottom" FirstPageText="First" LastPageText="Last" />

                                </asp:GridView>
                                <!-- /.Quotation Master Grid -responsive -->

                            </div>

                        </div>
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->



            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
</asp:Content>

