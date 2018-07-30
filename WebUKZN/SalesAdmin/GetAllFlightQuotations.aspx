<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="GetAllFlightQuotations.aspx.cs" Inherits="SalesAdmin_GetAllFlightQuotations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Quotations
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">GetAllFlightQuotations</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-horizontal">
                            <div class="form-group" style="display: none;">
                                <div class="col-sm-4">
                                    Approval No/ Email / Phone no
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtQuotationRef" runat="server" class="btn btn-info" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtQuotationRef" runat="server" ID="rfvtxtTodate"
                                        ValidationGroup="subbmit" ErrorMessage="Qutation No." Text="EnterQutation No."
                                        class="validationred" Display="Dynamic" />
                                </div>


                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                        Text="Get" OnClick="cmdSubmit_Click" />

                                </div>
                            </div>

                            <div class="form-group">
                                <asp:GridView ID="gdvFlightQuotations" runat="server" AllowPaging="true" EmptyDataText="No Quotations Found" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                    DataKeyNames="QuotationMasterId"
                                    AutoGenerateColumns="False"
                                    Width="100%" PageSize="100" usecustompager="true" OnRowCommand="gdvFlightQuotations_RowCommand">

                                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                    <RowStyle CssClass="gradeA odd" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Request No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlightquootationRef" runat="server" Text='<%#Eval("QuotationRefNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approval send Date" SortExpression="CreatedOn">
                                            <ItemTemplate>
                                                <%#Eval("CreatedOn")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passgener Name" SortExpression="TravellerName">
                                            <ItemTemplate>
                                                <%#Eval("TravellerName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UKZN OrderNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUkznOrderNo" runat="server" Text='<%#Eval("ukzn_orderno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UKZN Apporval Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("status_description")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imgGenerateEticket" runat="server" ImageUrl="~/images/edit.png" Height="25" ToolTip="View Details"
                                                                Width="25" CommandName="ViewDetails" CommandArgument='<%# Eval("QuotationMasterId") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/ApproHistory.png" Height="25" ToolTip="View History"
                                                                Width="25" CommandName="Hisotry" CommandArgument='<%# Eval("QuotationMasterId") %>' />
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
        </section>
   
</asp:Content>

