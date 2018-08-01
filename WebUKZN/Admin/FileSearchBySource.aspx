<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="FileSearchBySource.aspx.cs" Inherits="Admin_FileSearchBySource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">

        <h1>All Files</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">FileSearchBySource</li>
        </ol>
    </section>
    <section class="content">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">All Files</h3>
                    </div>
                    <div class="box-body form-horizontal">


                        <div class="form-group">

                            <div class="col-sm-2">
                                <label class="control-label">
                                    Customer Email/Phone</label>
                            </div>
                            <div class="col-sm-4">

                                <asp:TextBox ID="txtSearch" runat="server" class="form-control" />
                                <asp:RequiredFieldValidator ControlToValidate="txtSearch" runat="server" ID="rfvtxtSearch"
                                    ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                                    class="validationred" Display="Dynamic" />
                            </div>

                            <div class="col-sm-2">
                                <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                    Text="Submit" OnClick="cmdSubmit_Click" />
                            </div>
                        </div>

                        <asp:GridView ID="gdvAllFileNo" runat="server" AllowSorting="true" EmptyDataText="No Files Found"
                            AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed mb-none"
                            Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gdvAllFileNo_RowCommand">
                            <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                            <RowStyle CssClass="gradeA odd" />
                            <AlternatingRowStyle CssClass="gradeA even" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File No">
                                    <ItemTemplate>
                                        <%#Eval("FileNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Role">
                                    <ItemTemplate>
                                        <%#Eval("role_desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <%#Eval("UserName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passenger Name">
                                    <ItemTemplate>
                                        <%#Eval("PassengerName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <%#Eval("TotalAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <a href='AllBookings.aspx?FileNo=<%# Eval("FileNo") %>' class="on-default edit-row" title="View booking details"><i class="fa fa-bars"></i></a>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkPrint" runat="server" CommandName="PrintAll" CommandArgument='<%# Eval("FileNo") %>' CssClass="fa fa-print" ToolTip="Print"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <a href='UploadFileDoc.aspx?FileNo=<%# Eval("FileNo") %>' class="on-default edit-row" title="Upload documents"><i class="fa fa-upload"></i></a>
                                                </td>
                                                <td>
                                                    <a href='InvoicePdf.aspx?FileNo=<%# Eval("FileNo") %>' target="_blank" class="on-default edit-row" title="View invoice"><i class="fa fa-file-text"></i></a>
                                                </td>
                                            </tr>
                                        </table>

                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>


                </div>
            </div>
        </div>
    </section>
</asp:Content>

