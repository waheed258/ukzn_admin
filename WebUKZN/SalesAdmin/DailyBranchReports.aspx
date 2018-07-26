<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="DailyBranchReports.aspx.cs" Inherits="SalesAdmin_DailyBranchReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.js"></script>
    <%-- <link href="css/wickedpicker.css" rel="stylesheet" />--%>

    <script src="js/wickedpicker.js"></script>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {


            DatePickerSet();
        });


        function DatePickerSet() {
            $("#ContentPlaceHolder1_txtStartDate").datepicker({

                numberOfMonths: 1,
                dateFormat: 'yy-mm-dd',
                autoclose: true,
                onSelect: function (selected) {
                    var date2 = $('#ContentPlaceHolder1_txtStartDate').datepicker('getDate');
                    date2.setDate(date2.getDate());
                    //  $('#ctl00_ContentPlaceHolder1_txtEventEndDate').datepicker('setDate', date2);
                    $('#ContentPlaceHolder1_txtTodate').datepicker('option', 'minDate', date2);
                }
            }).attr('readonly', 'true');;

            $("#ContentPlaceHolder1_txtTodate").datepicker({

                dateFormat: 'yy-mm-dd',
                numberOfMonths: 1,
                autoclose: true,
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate());

                }
            }).attr('readonly', 'true');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Branch Report
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>
        <section class="content">
           
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="form-group">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Branch Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                              <div class="form-group">
                <div class="col-sm-2" style="width: 11%">
                    <label class="control-label">
                        From Date</label>
                </div>
                <div class="col-sm-2">

                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtStartDate"
                        ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                        class="validationred" Display="Dynamic" />
                </div>

                <div class="col-sm-1">
                    <label class="control-label">
                        To Date
                    </label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtTodate" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtTodate" runat="server" ID="rfvtxtTodate"
                        ValidationGroup="subbmit" ErrorMessage="Enter To Date." Text="Enter To Date."
                        class="validationred" Display="Dynamic" />
                </div>


                <div class="col-sm-2">
                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-1">
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="btnSubmit" class="btn btn-primary green"
                        Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button runat="server" ID="btnExportPdf" class="btn btn-primary green pdf"
                        Text="PDF" OnClick="btnExportPdf_Click" />
                </div>


            </div>

                            <div class="form-group">
                <asp:GridView ID="gvDailrReport" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
                    AutoGenerateColumns="False" OnRowDataBound="gvDailrReport_RowDataBound"
                    Width="100%" DataKeyNames="BranchId" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
                    <FooterStyle BackColor="#08376a" />
                    <RowStyle CssClass="gradeA odd" />
                    <AlternatingRowStyle CssClass="gradeA even" />
                    <Columns>

                        <asp:TemplateField HeaderText="SN">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BranchName" HeaderText="Branch">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="curPaymentAmount" HeaderText="Total Amount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="gvModeOfPayment" runat="server" AutoGenerateColumns="false" 
                                    CssClass="table table-bordered table-striped mb-none dataTable no-footer" ShowFooter="true">
                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="PaymentType" HeaderText="Mode of Payment">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="curPaymentAmount" HeaderText="Amount">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
                        </div>
                        <!-- /.box-body -->
                        
                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->

                </div>
                <!--/.col (right) -->

            </div>


        </section>
    </div>
</asp:Content>

