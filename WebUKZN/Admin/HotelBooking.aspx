<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="HotelBooking.aspx.cs" Inherits="Admin_HotelBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
        
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Hotel Bookings
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Hotel Bookings</li>
            </ol>
        </section>
          <asp:Label ID="lblMsg" runat="server"></asp:Label>
           <section class="content">
                <div class="row">
        <div class="col-md-12">
          <div class="box">
            <div class="box-header with-border">
            
            </div>
            <!-- /.box-header -->
            <div class="box-body">

                       <asp:GridView ID="gdvHotelBooking" runat="server" AllowSorting="true" EmptyDataText="No Bookings Found"
                AutoGenerateColumns="False" CssClass="table table-bordered table-striped mb-none dataTable no-footer"
                Width="100%" PageSize="4" usecustompager="true" OnRowCommand="gdvHotelBooking_RowCommand" OnRowDataBound="gdvHotelBooking_RowDataBound">

                <PagerSettings PreviousPageText="&laquo; previous" NextPageText="next &raquo;" PageButtonCount="4" />
                <Columns>
                    <asp:TemplateField HeaderText="SN">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                            <asp:HiddenField ID="hfStatusId" runat="server" Value='<%#Eval("status_id")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File No">
                        <ItemTemplate>
                            <%#Eval("FileNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier Hotel">
                        <ItemTemplate>
                            <%#Eval("SupplierHotel")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Guest First Name">
                        <ItemTemplate>
                            <%#Eval("GuestFirstName")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Guest Phone Number">
                        <ItemTemplate>
                            <%#Eval("GuestPhoneNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guest Email Id">
                        <ItemTemplate>
                            <%#Eval("GuestEmail")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CheckIn Date">
                        <ItemTemplate>
                            <%#Eval("CheckinDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Price">
                        <ItemTemplate>
                            <%#Eval("TotalPrice")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <%#Eval("status_desc")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="cmdPrintInvoice" runat="server" ImageUrl="~/images/print.png" Height="25"
                                            Width="25" CommandName="PrintInVoice" CommandArgument='<%# Eval("HotelRequestId") %>' ToolTip="Print Ticket" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="cmdCancelHotel" runat="server" ImageUrl="~/images/delete.png" Height="25"
                                            Width="25" CommandName="Cancel Hotel" CommandArgument='<%# Eval("HotelRequestId") %>' ToolTip="Cancel Hotel" />
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


