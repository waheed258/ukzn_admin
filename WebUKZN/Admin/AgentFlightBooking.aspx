<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AgentFlightBooking.aspx.cs" Inherits="Admin_AgentFlightBooking" %>

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
      <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>All Flight Bookings</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">All Flight Bookings</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">All Flight Bookings</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-horizontal">
                            <div class="form-group">
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
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="Submit" OnClick="cmdSubmit_Click" />
                                    <asp:Button runat="server" ID="btnExportPdf" class="btn btn-primary green"
                                        Text="PDF" OnClick="btnExportPdf_Click" />
                                </div>
                            </div>

                            <asp:GridView ID="gdvFlightBookings" runat="server" AllowPaging="true" EmptyDataText="No Bookings Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                DataKeyNames="FlightRequestId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlights_RowCommand" OnRowDataBound="gdvFlightBookings_RowDataBound">

                                <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfEticketIssue" runat="server" Value='<%#Eval("Booking_Status")%>' />
                                            <asp:HiddenField ID="hfBookingRef" runat="server" Value='<%#Eval("BookingRefNo")%>' />
                                            <asp:HiddenField ID="hfTraceId" runat="server" Value='<%#Eval("TraceId")%>' />
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agent / Consultant" SortExpression="UserName">
                                        <ItemTemplate>
                                            <%#Eval("UserName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Date" SortExpression="BookingDate">
                                        <ItemTemplate>
                                            <%#Eval("BookingDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PNR" SortExpression="PNR">
                                        <ItemTemplate>
                                            <%#Eval("PNR")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Airline RefNo" SortExpression="AirlineRefNo">
                                        <ItemTemplate>
                                            <%#Eval("AirlineRefNo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="PaxName">
                                        <ItemTemplate>
                                            <%#Eval("PaxName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" SortExpression="PaxEmail">
                                        <ItemTemplate>
                                            <%#Eval("PaxEmail")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone" SortExpression="PaxMobile">
                                        <ItemTemplate>
                                            <%#Eval("PaxMobile")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Fromcode" HeaderText="From">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ToCode" HeaderText="To">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Jurney Date" SortExpression="FlightDate">
                                        <ItemTemplate>
                                            <%#Eval("FlightDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ticketing Status" SortExpression="ticketstatus">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnStatus" runat="server" Style="height: 18px; width: 25px; border-radius: 6px;" />
                                                    </td>
                                                    <td>
                                                        <%#Eval("ticketstatus")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/Air-tickets-icon.png" Height="25" OnClientClick="return confirm('Are you sure generate E-ticket?');" ToolTip="Generate Eticket"
                                                            Width="25" CommandName="GenerateTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="cmdPrintTicket" runat="server" ImageUrl="~/images/icon-print.png" Height="25" ToolTip="Print Eticket"
                                                            Width="25" CommandName="PrintTicket" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnCancel" Visible="false" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel Ticket"
                                                            Width="25" CommandName="CancelTicket" OnClientClick="return confirm('Are you sure cancel E-ticket?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnCancelBooking" Visible="false" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Cancel Booking"
                                                            Width="25" CommandName="BookingCancel" OnClientClick="return confirm('Are you sure cancel Booking?');" CommandArgument='<%# Eval("FlightRequestId") %>' Enabled="false" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnViewDtls" runat="server" ImageUrl="~/images/ViewDtls.png" Height="25" ToolTip="View Details"
                                                            Width="25" CommandName="ViewDetails" CommandArgument='<%# Eval("FlightRequestId") %>' />
                                                    </td>
                                                </tr>
                                            </table>



                                        </ItemTemplate>
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
</asp:Content>

