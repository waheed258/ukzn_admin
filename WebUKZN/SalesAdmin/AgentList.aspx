<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="AgentList.aspx.cs" Inherits="SalesAdmin_AgentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Agent List</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Agent List</li>
        </ol>
    </section>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Agent List</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" />
                            </div>

                        </div>


                        <asp:GridView ID="gvAgent" runat="server" AllowPaging="true" EmptyDataText="No Agents Found"
                            AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                            Width="100%" OnRowCommand="gvStaff_RowCommand">

                            <RowStyle CssClass="gradeA odd" />
                            <AlternatingRowStyle CssClass="gradeA even" />
                            <Columns>

                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agent Name" SortExpression="agentname">
                                    <ItemTemplate>
                                        <%#Eval("agentname")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Assigned Company" SortExpression="CompanyName">
                                    <ItemTemplate>
                                        <%#Eval("CompanyName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Email" SortExpression="UserEmail">
                                    <ItemTemplate>
                                        <%#Eval("UserEmail")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone No" SortExpression="UserPhone">
                                    <ItemTemplate>
                                        <%#Eval("UserPhone")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="status_desc">
                                    <ItemTemplate>
                                        <%#Eval("status_desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="CreateAgent.aspx?agentId=<%# Eval("UserDetailsId") %>" class="on-default edit-row"><i class="fa fa-pencil"></i></a>

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

