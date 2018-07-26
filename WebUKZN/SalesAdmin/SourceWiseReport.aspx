<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="SourceWiseReport.aspx.cs" Inherits="SalesAdmin_SourceWiseReport" %>

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
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Source Wise Report
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">SourcewiseReport</li>
            </ol>
        </section>
        
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Source Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">From Date<span class="validationred"> *</span></label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtFromDate"
                                        ValidationGroup="Employee" ErrorMessage="Select From Date." Text="Select From Date." Display="Dynamic"
                                        class="validationred" />
                                </div>

                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">To Date<span class="validationred"> *</span></label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtTodate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTodate" runat="server" ID="rfvtxtToDate"
                                        ValidationGroup="Employee" ErrorMessage="Select To Date." Text="Select To Date." Display="Dynamic"
                                        class="validationred" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnSourceSubmit"
                                        class="btn btn btn-primary green" ValidationGroup="SourceRpt" Text="Submit" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnSourcePdf" class="btn  btn btn-primary green"
                                           Text="PDF" />&nbsp;
                                </div>
                            </div>

                            <asp:GridView ID="gvDailrReport" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
                                AutoGenerateColumns="False"
                                Width="100%" DataKeyNames="PaymentAmount" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
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
                                     <asp:TemplateField HeaderText="Source">
                                         <ItemStyle HorizontalAlign="Center" />
                                <itemtemplate>
                                                 <%# Eval("BookingSource") %>
                                            </itemtemplate>
                                <footertemplate>
                                                <asp:Label ID="lblSummary" runat="server" Text="Total's" />
                                            </footertemplate>
                            </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Total Amount">
                                         <ItemStyle HorizontalAlign="Right" />
                                <itemtemplate>
                                               <%# Eval("curPaymentAmount") %>
                                            </itemtemplate>
                                <footertemplate>
                                                <asp:Label ID="lblTotalAmount" runat="server" Text="R5430.00" />
                                            </footertemplate>
                            </asp:TemplateField>

                                   

                                </Columns>
                            </asp:GridView>


                        </div>

                    </div>
                    <!-- /.box -->


                </div>

            </div>
            <!-- /.row -->
        </section>
    </div>
</asp:Content>

