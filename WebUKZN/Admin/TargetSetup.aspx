<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="TargetSetup.aspx.cs" Inherits="Admin_TargetSetup" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Target Setup
       
               
            </h1>

            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Target Setup</li>
            </ol>
        </section>
        <asp:HiddenField ID="hfTargetId" runat="server" Value="0" />

        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-2">
                                    Month
                                </div>
                                <div class="col-sm-2">

                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                        <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    Year
                    
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    Target Amount
                                </div>
                                <div class="col-sm-2">

                                    <asp:TextBox ID="txtTargetAmount" runat="server" class="form-control" MaxLength="10" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTargetAmount" runat="server" ID="rfvPercentage" ValidationGroup="subbmit"
                                        ErrorMessage="Enter Service Fee" Text="Enter Target Amount" class="validationred" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ControlToValidate="txtTargetAmount" runat="server" ID="rxAmountToPay" ValidationGroup="subbmit"
                                        ErrorMessage="Enter  number only." Text="Enter  number only."
                                        ValidationExpression="^\-?[0-9]+(?:\.[0-9]+)?" class="validationred" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    Completed Amount
                    
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCompletedAmount" runat="server" class="form-control" ReadOnly="true" Text="0" />
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    Pending Amount
                                </div>
                                <div class="col-sm-2">

                                    <asp:TextBox ID="txtPendingAmount" runat="server" class="form-control" ReadOnly="true" Text="0" />

                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                                            class="btn btn-primary red" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gdvData" runat="server" AllowPaging="true" EmptyDataText="No Records Found"
                                        CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="TargetId"
                                        AutoGenerateColumns="False"
                                        Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvData_RowCommand" OnRowDataBound="gdvData_RowDataBound">

                                        <AlternatingRowStyle CssClass="gradeA even" />
                                        <FooterStyle BackColor="#08376a" />
                                        <RowStyle CssClass="gradeA odd" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                                <ItemTemplate>

                                                    <asp:HiddenField ID="hfTargetId" runat="server" Value='<%#Eval("TargetId")%>' />
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="Consultant Name">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TargetMonthYear" HeaderText="Month And Year">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>


                                            <asp:BoundField DataField="TargetAmount" HeaderText="Target Amount">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CompletedAmount" HeaderText="Target Achieved">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>


                                            <asp:BoundField DataField="TargetCompletePer" HeaderText="Achieved %">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="PendingAmount" HeaderText="Target Due">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="TargetPendingPer" HeaderText="Target Due %">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" ToolTip="Edit"
                                                                    Width="25" CommandName="EditTarget" CommandArgument='<%# Eval("TargetId") %>' />
                                                            </td>

                                                            <td>
                                                                <asp:ImageButton ID="imgbtnCancelBooking" runat="server" ImageUrl="~/images/delete.png" Height="25" ToolTip="Delete"
                                                                    Width="25" CommandName="DeleteTarget" OnClientClick="return confirm('Are you sure Delete?');" CommandArgument='<%# Eval("TargetId") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

   
</asp:Content>

