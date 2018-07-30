<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs" Inherits="SalesAdmin_CustomerList" %>
<%@ Register Namespace="App_Code" TagPrefix="fld_req" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
  
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Customer FlightBooking List
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Login History</li>
            </ol>
        </section>
         <asp:Label ID="LabelError" runat="server" class="message"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                        
                            <fld_req:GridViewWithPager ID="gdvCustomer" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                            Width="100%" PageSize="100" UseCustomPager="true" OnRowCommand="gdvCustomer_RowCommand"
                           OnSorting="gdvCustomer_Sorting" OnPageIndexChanging="gdvCustomer_PageIndexChanging">
                                <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                            <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="100" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Type" SortExpression="UserType">
                                    <ItemTemplate>
                                        <%#Eval("role_desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                                    <ItemTemplate>
                                        <%#Eval("FirstName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                                    <ItemTemplate>
                                        <%#Eval("LastName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile Number" SortExpression="MobileNumber">
                                    <ItemTemplate>
                                        <%#Eval("user_master_mobile")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Id" SortExpression="EmailId">
                                    <ItemTemplate>
                                        <%#Eval("user_master_mail")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <%#Eval("status_desc")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <li class="editbutton">
                                            <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="~/images/pencil2.png" Height="25"
                                                Width="25" CommandName="Edit" CommandArgument='<%# Eval("user_master_id") %>' />
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </fld_req:GridViewWithPager>
                                
                        </div>

                    </div>

                </div>
            </div>
            <!-- /.row -->
        </section>
    
                        </ContentTemplate>
           </asp:UpdatePanel>
</asp:Content>

