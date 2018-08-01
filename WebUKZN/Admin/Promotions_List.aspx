<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Promotions_List.aspx.cs" Inherits="Admin_Promotions_List" %>

<%@ Register Namespace="App_Code" TagPrefix="fld_req" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/pagging.css" rel="stylesheet" type="text/css" />

    
     <style>
    .gvPagerCss span
    {
        background-color:#2295CC;
        font-size:20px;
    }  
    .gvPagerCss td
    {
        padding-left: 5px;   
        padding-right: 5px;  
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>LIST OF PROMOTIONS
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">LIST OF PROMOTIONS</li>
            </ol>
        </section>
        <section class="content">
           <div class="row">
              <div class="col-md-12">
                    <div class="box">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="box-header with-border">
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                            <asp:Label ID="LabelError" runat="server" class="message"></asp:Label>
                              <div class="col-md-12">
                                <div class="col-sm-2" style="float: left; margin-left: -3%;">
                                <asp:DropDownList ID="DropPage" runat="server" class="form-control" size="1" OnSelectedIndexChanged="Promotions_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Records per page</label></div>
                            <div class="col-sm-4">
                            </div>
                            <%-- <div class="col-sm-1">
                                <label class="control-label">
                                    Search</label></div>
                            <div class="col-sm-3">
                                <asp:Panel ID="PanelSearch" runat="server" DefaultButton="cmdSearch">
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>
                                    <asp:Button runat="server" ID="cmdSearch" OnClick="cmdSearch_Click" Text="Search Snack"
                                        Style="display: none" />
                                </asp:Panel>
                            </div>--%>
                            </div>
                            <fld_req:GridViewWithPager ID="gdvPromotions" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="display table table-bordered table-striped pull_left"
                            Width="100%" PageSize="2" UseCustomPager="true" OnRowCommand="gdvPromotions_RowCommand"
                           OnSorting="gdvPromotions_Sorting" OnPageIndexChanging="gdvPromotions_PageIndexChanging" OnRowDataBound="gdvPromotions_RowDataBound">

                            <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="3" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PromotionCategory" SortExpression="PromotionCategory">
                                    <ItemTemplate>
                                        <%#Eval("promotionCategory")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PromotionRelavance" SortExpression="PromotionRelavance">
                                    <ItemTemplate>
                                        <%#Eval("promotionRelavance")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PromotionType" SortExpression="PromotionType">
                                    <ItemTemplate>
                                        <%#Eval("promotionType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PromotionCode" SortExpression="PromotionCode">
                                    <ItemTemplate>
                                        <%#Eval("promotionCode")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DiscountType" SortExpression="DiscountType">
                                    <ItemTemplate>
                                        <%#Eval("DiscountType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Percentage" SortExpression="Percentage">
                                    <ItemTemplate>
                                        <%#Eval("percentage")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                                    <ItemTemplate>
                                        <%#Eval("amount")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                                    <ItemTemplate>
                                        <%#Eval("startDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                                    <ItemTemplate>
                                        <%#Eval("endDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <li class="editbutton">
                                            <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="~/images/pencil.png" Height="25"
                                                Width="25" CommandName="Edit" CommandArgument='<%# Eval("promotionId") %>' />
                                        </li>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </fld_req:GridViewWithPager>
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
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                  <!-- /.box -->
              </div>
           </div>
           <!-- /.row -->
        </section>
</asp:Content>

