<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="Admin_EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Employee List
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            
                            <asp:GridView ID="gvUserList" runat="server" AllowPaging="true" EmptyDataText="No Data Found" 
                                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                Width="100%" OnRowCommand="gvUserList_RowCommand" OnPageIndexChanging="gvUserList_PageIndexChanging" OnSorting="gvUserList_Sorting">
                               
                                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                
                                    <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SNo">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <%#Eval("UserFirstName")%>  <%#Eval("UserLastName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <%#Eval("UserEmail")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>
                                            <%#Eval("UserPhone")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <%#Eval("RoleDescription")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>

                                            <%#Eval("UserStatus").ToString() == "1" ? "Active" : "In Active"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <table>
                                                <tr></tr>
                                                <td>
                                                    <asp:ImageButton ID="imgEdit" ToolTip="Edit" runat="server" ImageUrl="images/icon-edit.png" Height="20" Width="20"
                                                        CommandName="Edit User" CommandArgument='<%#Eval("UserMasterId") %>' />

                                                </td>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                       
                        <!-- /.box-body -->
                        <div class="box-footer clearfix">
                            
                           <%-- <ul class="pagination pagination-sm no-margin pull-right">
                                <li><a href="#">&laquo;</a></li>
                                <li><a href="#">1</a></li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">&raquo;</a></li>
                            </ul>--%>
                        </div>
                    </div>
                    <!-- /.box -->


                </div>

            </div>
            <!-- /.row -->
        </section>
    </div>
</asp:Content>

