<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="APIManagement_List.aspx.cs" Inherits="SalesAdmin_APIManagement_List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="App_Code" TagPrefix="fld_req" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/pagging.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>LIST OF API  CONFIGURATION</h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">LIST OF API  CONFIGURATION</li>
            </ol>
        </section>
        <asp:Label ID="LabelError" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">LIST OF API  CONFIGURATION</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="DropPage" runat="server" class="form-control" size="1" OnSelectedIndexChanged="APIManagement_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                        <asp:ListItem Value="400">400</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Records per page</label>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                            <div class="form-group">

                                <fld_req:GridViewWithPager ID="gdvAPIManagement" runat="server" AllowPaging="true" AllowSorting="true"
                                    AutoGenerateColumns="False" CssClass="display table table-bordered table-striped pull_left"
                                    Width="100%" PageSize="3" UseCustomPager="true" OnRowCommand="gdvAPIManagement_RowCommand"
                                    OnSorting="gdvAPIManagement_Sorting" OnPageIndexChanging="gdvAPIManagement_PageIndexChanging">

                                    <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agent PCC" SortExpression="Agent PCC">
                                            <ItemTemplate>
                                                <%#Eval("PccCode")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="API Name" SortExpression="API Name">
                                            <ItemTemplate>
                                                <%#Eval("APIName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" SortExpression="User Name">
                                            <ItemTemplate>
                                                <%#Eval("UserName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Password" SortExpression="Password">
                                            <ItemTemplate>
                                                <%#Eval("PassWord")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Production URL" SortExpression="Production URL">
                                            <ItemTemplate>
                                                <%#Eval("ProductionURL")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test URL" SortExpression="Test URL">
                                            <ItemTemplate>
                                                <%#Eval("TestURL")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <%#Eval("status_desc")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <li class="editbutton">
                                                    <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="~/images/pencil.png" Height="25"
                                                        Width="25" CommandName="Edit" CommandArgument='<%# Eval("LineSeqid") %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </fld_req:GridViewWithPager>


                            </div>
                        </div>
                        <!-- /.box -->


                    </div>

                </div>
            </div>
            <!-- /.row -->
        </section>
    </div>
</asp:Content>

