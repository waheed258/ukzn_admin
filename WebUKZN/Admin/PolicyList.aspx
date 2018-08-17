<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="PolicyList.aspx.cs" Inherits="Admin_PolicyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="css/pagging.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Agent List</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Policy List</li>
        </ol>
    </section>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Policy List</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" class="btn btn-primary" />
                            </div>

                        </div>


                        <asp:GridView ID="gvPolicy" runat="server" AllowPaging="true" EmptyDataText="No Staff Found" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                            Width="100%" PageSize="10" OnSorting="gvPolicy_Sorting" OnRowCommand="gvPolicy_RowCommand" OnPageIndexChanging="gvPolicy_PageIndexChanging">

                            <RowStyle CssClass="gradeA odd" />
                            <AlternatingRowStyle CssClass="gradeA even" />
                             <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                            <Columns>

                                  <asp:TemplateField HeaderText="S.No"  HeaderStyle-ForeColor="#3c8dbc">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" SortExpression="Category">
                                    <ItemTemplate>
                                        <%#Eval("Category")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CategoryType" SortExpression="CategoryType">
                                    <ItemTemplate>
                                        <%#Eval("CategoryType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rule" SortExpression="Rule">
                                    <ItemTemplate>
                                        <%#Eval("RuleValue")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Condition" SortExpression="Condition">
                                    <ItemTemplate>
                                        <%#Eval("Condition")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <table>
                                            <tr></tr>
                                            <td>
                                                <asp:ImageButton ID="imgEdit" ToolTip="Edit" runat="server" ImageUrl="../images/icon-edit.png" Height="20" Width="20"
                                                    CommandName="Edit Policy" CommandArgument='<%#Eval("PolicyID") %>' />
                                                <asp:ImageButton ID="imgDelete" ToolTip="Delete" runat="server" ImageUrl="../images/icon-delete.png" Height="20" Width="20"
                                                    CommandName="Delete Policy" CommandArgument='<%#Eval("PolicyID") %>' OnClientClick="javascript:return confirm('Are You Sure To Delete AirSupplier Details')" />

                                            </td>
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

