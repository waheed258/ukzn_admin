<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="DestinationReport.aspx.cs" Inherits="SalesAdmin_DestinationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate());

                }
            }).attr('readonly', 'true');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>User Report
       
               
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
                            <h3 class="box-title">User Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    From Date</label>
                            </div>
                            <div class="col-sm-2">

                                <asp:TextBox ID="txtStartDate" runat="server" class="form-control" />
                                <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtStartDate"
                                    ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                                    class="validationred" Display="Dynamic" />
                            </div>

                            <div class="col-sm-2">
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

                            <div class="col-sm-3">
                                <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                    Text="Submit" OnClick="cmdSubmit_Click" />
                                <asp:Button runat="server" ID="btnExportPdf" class="btn btn-primary green pdf"
                                    Text="PDF" OnClick="btnExportPdf_Click" />
                            </div>

                            <div class="form-group">
                                <asp:GridView ID="gvDestination" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
                                    AutoGenerateColumns="False"
                                    Width="100%" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
                                    <FooterStyle BackColor="#08376a" />
                                    <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ToCode" HeaderText="Destination">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GdsCodeDescription" HeaderText="Description">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TotalPrice" HeaderText="Booking Amount">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="topDestination" HeaderText="Booking Count">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
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

