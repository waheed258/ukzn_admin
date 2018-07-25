<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="FindFlightBookings.aspx.cs" Inherits="Admin_FindFlightBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    
 <script type="text/javascript">
     $(function () {

         $("#<%= txtfromDate.ClientID  %>").datepicker({});
             $("#<%= txtTodate.ClientID  %>").datepicker({});

         });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Flight Booking List</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Flight Booking</li>
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
                            <h3 class="box-title"></h3>
                            <div>
                            </div>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                                <%--<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>--%>
                            </div>
                        </div>
                        <%--  --%>
                        <div class="form-group">
                            <div class="col-sm-12">

                                <div class="col-sm-1">
                                    <label class="control-label">From Date </label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtfromDate" runat="server" class="form-control" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-1">
                                    <label class="control-label">To Date</label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtTodate" runat="server" class="form-control" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button ID="Submit" Text="Submit" runat="server" CssClass="btn btn-primary" />
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button ID="PDF" Text="PDF" runat="server" CssClass="btn btn-primary" />
                                </div>
                            </div>

                        </div>
                        <br /><br />
                        <%--  --%>
                      
                        <!-- /.box-header -->
                        <div class="box-body">
                           
                            
                            <div class="card-body">
                                <!-- /.Quotation Master Grid -responsive -->
                                <asp:GridView ID="GvFindFlightBookingList" runat="server" AllowPaging="true" EmptyDataText="No Data Found"
                                    AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    Width="100%"  OnRowDataBound="GvFindFlightBookingList_RowDataBound" >

                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.no">
                                            <ItemTemplate>
                                                <%#Eval("ID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent/Consultant">
                                            <ItemTemplate>
                                                <%#Eval("Agent")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PNR">
                                            <ItemTemplate>
                                                <%#Eval("PNR")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <%#Eval("Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone No">
                                            <ItemTemplate>
                                                <%#Eval("PhoneNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ticketing Status">
                                            <ItemTemplate>
                                                <%#Eval("TicketingStatus")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                   
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

