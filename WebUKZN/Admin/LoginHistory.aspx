<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="LoginHistory.aspx.cs" Inherits="Admin_LoginHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Login History
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Login History</li>
            </ol>
        </section>
          <asp:Label ID="lblMsg" runat="server"></asp:Label>
           <section class="content">
                <div class="row">
        <div class="col-md-12">
          <div class="box">
            <div class="box-header with-border">
              <h3 class="box-title"> </h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
             
                <asp:GridView ID="gvLoginEmpList" runat="server" AllowPaging="true" EmptyDataText="No Data Found"
                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                Width="100%"  OnRowCommand="gvLoginEmpList_RowCommand"  OnPageIndexChanging="gvLoginEmpList_PageIndexChanging"  OnSorting="gvLoginEmpList_Sorting">

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
                            <%#Eval("UserName")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ip Address">
                        <ItemTemplate>
                            <%#Eval("IpAddress")%>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Login In Date">
                        <ItemTemplate>
                            <%#Eval("LoginDateTime")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Logout Date">
                        <ItemTemplate>
                            <%#Eval("LogoutDateTime")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    
                </Columns>
            </asp:GridView>
             
            </div>
            <!-- /.box-body -->
          <%--  <div class="box-footer clearfix">
              <ul class="pagination pagination-sm no-margin pull-right">
                <li><a href="#">&laquo;</a></li>
                <li><a href="#">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">&raquo;</a></li>
              </ul>
            </div>--%>
          </div>
          <!-- /.box -->

       
        </div>
       
      </div>
      <!-- /.row -->
           </section>
</div>
</asp:Content>

