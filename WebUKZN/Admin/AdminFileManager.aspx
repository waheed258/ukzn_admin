<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminFileManager.aspx.cs" Inherits="Admin_AdminFileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>All Files</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">All Files</li>
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
                    <div class="box-body">

                        <asp:GridView ID="gdvAllFileNo" runat="server" AllowSorting="true" EmptyDataText="No Files Found"
                            AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed mb-none"
                            Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gdvAllFileNo_RowCommand">
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
                                <asp:TemplateField HeaderText="UKZN OrderNo">
                                    <ItemTemplate>
                                        <%#Eval("OrderNo")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Center Code">
                                    <ItemTemplate>
                                        <%#Eval("cost_center")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Center Name">
                                    <ItemTemplate>
                                        <%#Eval("costcentrename")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <%#Eval("TravellerFirstName")%>  <%#Eval("TravellerLastName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <%#Eval("TripAmount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%#Eval("FileStatusDesc")%>
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
                                                    <a href='UkznCreateInvoice.aspx?FileNo=<%# Eval("FileNo") %>' target="_blank" class="on-default edit-row" title="View invoice"><i class="fa fa-file-text"></i></a>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkCancelOrder" runat="server" CommandName="Cancel Order" CommandArgument='<%# Eval("FileNo") %>' CssClass="fa fa-times" ToolTip="Cancel"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- /.box-body -->

                </div>
                <!-- /.box -->


            </div>

        </div>
        <!-- /.row -->
    </section>

</asp:Content>

