﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="TravellerReport.aspx.cs" Inherits="SalesAdmin_TravellerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.js"></script>
   

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
            <h1>Target Report
       
               
            </h1>

            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Target Report</li>
            </ol>
        </section>
          
         <section class="content">
             <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-body form-horizontal">
                             <div class="form-group">
                <div class="col-sm-2">
                   
                        Cost CenterCode
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlTraveller" runat="server" class="form-control" OnSelectedIndexChanged="ddlTraveller_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <div class="col-sm-2">
                   
                        From Date
                </div>
                <div class="col-sm-2">

                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control" />
                    <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtStartDate"
                        ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                        class="validationred" Display="Dynamic" />
                </div>

                <div class="col-sm-2">
                   
                        To Date
                   
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
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                <asp:GridView ID="gvClientReport" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found"
                    AutoGenerateColumns="False"
                    Width="100%" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
                    <AlternatingRowStyle CssClass="gradeA even" />
                    <FooterStyle BackColor="#08376a" />
                    <RowStyle CssClass="gradeA odd" />
                    <Columns>

                        <asp:TemplateField HeaderText="S No">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TravellerName" HeaderText="Client Name">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TravellerMailId" HeaderText="Email">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />

                        </asp:BoundField>
                        <asp:BoundField DataField="TravellerMobile" HeaderText="Mobile">
                            <ItemStyle HorizontalAlign="Center" />

                        </asp:BoundField>
                        <asp:BoundField DataField="TravellerPassPortNo" HeaderText="Passport No">
                            <ItemStyle HorizontalAlign="Right" />

                        </asp:BoundField>
                        <asp:BoundField DataField="totalfiles" HeaderText="No of bookings">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TOTALAMOUNT" HeaderText="Total business amount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
                            </div>
                        </div>
                    </div>
                </div>
             </section>
         </div>
</asp:Content>

