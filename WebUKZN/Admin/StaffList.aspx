<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="StaffList.aspx.cs" Inherits="Admin_StaffList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Redirect() {
            window.location = "StaffCreation.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Staff List
       
               
            </h1>
           
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Staff</li>
            </ol>
        </section>
   
        <section class="content">
              <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>
                        </div>
                          <div class="box-body">
                               <asp:GridView ID="gvStaff" runat="server" AllowPaging="true" EmptyDataText="No Staff Found"
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
                    <asp:TemplateField HeaderText="Company" SortExpression="CompanyName">
                        <ItemTemplate>
                            <%#Eval("CompanyName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation" SortExpression="Designation">
                        <ItemTemplate>
                            <%#Eval("Designation")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Role" SortExpression="role_desc">
                        <ItemTemplate>
                            <%#Eval("role_desc")%>
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

                            <%-- <li class="editbutton">
                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" ToolTip="Edit Staff"
                                    Width="25" CommandName="Edit" CommandArgument='<%# Eval("UserDetailsId") %>' /> 
                                 


                            </li>--%>
                            <a href='StaffCreation.aspx?stfid=<%# Eval("UserDetailsId") %>' class="on-default edit-row"><i class="fa fa-pencil"></i></a>
                            <a href='TargetSetup.aspx?stfid=<%# Eval("UserDetailsId") %>' class="on-default edit-row"><i class="fa fa-line-chart"></i></a>
                            <%--<li class="editbutton">
                                <asp:ImageButton ID="imgTarget" runat="server" ImageUrl="~/images/testi-icon.png" Height="25" ToolTip="Assign Target"
                                    Width="25" CommandName="Target" CommandArgument='<%# Eval("UserDetailsId") %>' />




                            </li>--%>
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

