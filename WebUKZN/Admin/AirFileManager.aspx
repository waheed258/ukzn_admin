<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AirFileManager.aspx.cs" Inherits="Admin_AirFileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Air Files
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">AirFiles</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                       
                        <!-- /.box-header -->
                        <div class="box-body">
                           
                            <asp:GridView ID="gvAirFilesList" runat="server" AllowPaging="true" EmptyDataText="No Data Found"
                                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                                Width="100%" OnRowCommand="gvAirFilesList_RowCommand" OnPageIndexChanging="gvAirFilesList_PageIndexChanging">
                                <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                <RowStyle CssClass="gradeA odd" />
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <Columns>
                                    <asp:BoundField DataField="Sno" HeaderText="SNO" ItemStyle-Width="30" />
                                    <asp:BoundField DataField="FileNo" HeaderText="File No" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="UKZNOrderNo" HeaderText="UKZN Order No" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="UserRole" HeaderText="User Role" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="PassengerName" HeaderText="Passenger Name" ItemStyle-Width="150" />

                                    <%-- <asp:TemplateField HeaderText="SNo">
                        <ItemTemplate>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File No">
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UKZN Order No">
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="User Role">
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Passenger Name">
                        <ItemTemplate>
                            
                            
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <table>
                                                <tr></tr>
                                                <td>
                                                  
                                                    <%--<button runat="server" id="btnPrint" class="btn btn-mini" title="Search">
                                                        <i class="fa fa-print"></i>
                                                    </button>--%>
                                                    <asp:LinkButton runat="server" ID="btnDetails" Width="20" Height="20"><i class="fa fa-list"></i> </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnPrint" Width="20" Height="20"><i class="fa fa-print"></i> </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnPdf"  Width="20" Height="20"><i class=" fa fa-file-pdf-o"></i> </asp:LinkButton>
                                                     
                                                     <asp:LinkButton runat="server" ID="btnTextFile"  Width="20" Height="20"><i class="fa  fa-file-text-o"></i> </asp:LinkButton>
                                                    
                                    
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
    </div>
</asp:Content>

