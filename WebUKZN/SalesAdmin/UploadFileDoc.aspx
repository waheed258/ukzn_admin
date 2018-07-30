<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="UploadFileDoc.aspx.cs" Inherits="SalesAdmin_UploadFileDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>File Documents
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">UploadFileDoc</li>
            </ol>
        </section>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        File no</label>
                                </div>
                                <div class="col-sm-2">

                                    <asp:TextBox ID="txtFileNo" runat="server" class="form-control" MaxLength="30" ReadOnly="true" />

                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Passenger Name
                                    </label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPassengerName" runat="server" class="form-control" ReadOnly="true" />
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label class="control-label">
                                        Upload File</label>
                                </div>
                                <div class="col-sm-2">

                                    <asp:FileUpload ID="fileuplaod1" runat="server" AllowMultiple="true" Font-Bold="true" />

                                </div>

                            </div>


                            <div class="form-group">
                                <div class="col-sm-2">
                                    <asp:Label ID="label1" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="labbel2" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Label ID="label3" runat="server" Font-Bold="true" ForeColor="Black" Font-Size="Large"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green"
                                        Text="Upload" OnClick="cmdSubmit_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel"
                                            class="btn btn-primary red" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />

                                </div>
                            </div>
                            <div class="form-group">
                                <asp:GridView ID="gdvUpload" runat="server" AllowPaging="true" EmptyDataText="No Files Found"
                                    CssClass="table table-bordered table-striped mb-none dataTable no-footer" OnRowCommand="gdvUpload_RowCommand"
                                    AutoGenerateColumns="False"
                                    Width="100%" PageSize="100" usecustompager="true">
                                    <PagerStyle BackColor="#efefef" ForeColor="black" HorizontalAlign="Left" CssClass="pagination1" />
                                    <AlternatingRowStyle CssClass="gradeA even" />
                                    <FooterStyle BackColor="#08376a" />
                                    <RowStyle CssClass="gradeA odd" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                            <ItemTemplate>


                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileName" HeaderText="File Name">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <a href='<%# Eval("FullPath") %>' target="_blank" class="on-default edit-row"><i class='<%#Eval("Icon")%>'></i></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
  
</asp:Content>

