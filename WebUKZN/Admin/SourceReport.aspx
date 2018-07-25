<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SourceReport.aspx.cs" Inherits="Admin_SourceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
  
      <script type="text/javascript">
        $(function () {

            $("#<%=txtFromDate.ClientID  %>").datepicker({

                changeMonth: true,
                changeYear: true,
                minDate: new Date(),
                maxDate: '+2y',
                onClose: function (selectedDate) {
                    var myDate = $(this).datepicker('getDate');
                    myDate.setDate(myDate.getDate() + 1);
                    $("#<%= txtToDate.ClientID  %>").datepicker('setDate', myDate);
                }
            });
            $("#<%= txtFromDate.ClientID  %>").datepicker().datepicker('setDate', '0');
            $("#<%= txtToDate.ClientID  %>").datepicker({ changeMonth: true, }).datepicker('setDate', '1');

        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
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
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtFromDate" runat="server" ID="rfvtxtFromDate"
                                        ValidationGroup="Employee" ErrorMessage="Select From Date." Text="Select From Date." Display="Dynamic"
                                        class="validationred" />
                                </div>

                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">To Date<span class="validationred"> *</span></label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtToDate" runat="server" ID="rfvtxtToDate"
                                        ValidationGroup="Employee" ErrorMessage="Select To Date." Text="Select To Date." Display="Dynamic"
                                        class="validationred" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnSourceSubmit"
                                        class="btn btn-info" ValidationGroup="SourceRpt" Text="Submit" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnSourcePdf" class="btn  btn-info"
                                           Text="PDF" />&nbsp;
                                </div>
                                </div>

                                <asp:GridView ID="gvSourceReport" runat="server" AllowPaging="true" EmptyDataText="No Data Found" FooterStyle-BackColor="#367fa9"   
                                    AutoGenerateColumns="False" ShowFooter="true" CssClass="table table-bordered  table-striped mb-none"
                                    Width="100%" OnRowCommand="gvSourceReport_RowCommand" OnPageIndexChanging="gvSourceReport_PageIndexChanging">
                                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                       <%-- <asp:BoundField DataField="Sno" HeaderText="SNO" ItemStyle-Width="30" />--%>

                                        <asp:TemplateField HeaderText="SNO">
                                            <ItemTemplate>
                                                1
                                            </ItemTemplate>
                                            <FooterTemplate>
                                               
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Source" HeaderText="Source" ItemStyle-Width="150" />--%>
                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                Hotel
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblSummary" runat="server" Text="Total's" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" ItemStyle-Width="150" />--%>

                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                R5430.00
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalAmount" runat="server" Text="R5430.00" />
                                            </FooterTemplate>
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

