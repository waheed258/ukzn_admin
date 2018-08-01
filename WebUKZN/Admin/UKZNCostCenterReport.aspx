<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="UKZNCostCenterReport.aspx.cs" Inherits="Admin_UKZNCostCenterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Settings</a></li>
            <li class="active">Report</li>
        </ol>
    </section>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Report</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    Cost CenterCode</label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddlCostCenterCode" runat="server" class="form-control" OnSelectedIndexChanged="ddlCostCenterCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-2">
                                <label class="control-label">
                                    From Date</label>
                            </div>
                            <div class="col-sm-2">

                                <asp:TextBox ID="txtStartDate" runat="server" class="form-control" />
                                <asp:RequiredFieldValidator ControlToValidate="txtStartDate" runat="server" ID="rfvtxtStartDate"
                                    ValidationGroup="subbmit" ErrorMessage="Enter From Date." Text="Enter From Date."
                                    class="validationred" Display="Dynamic" />
                            </div>

                            <div class="col-sm-2">
                                <label class="control-label">
                                    To Date
                                </label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTodate" runat="server" class="form-control" />
                                <asp:RequiredFieldValidator ControlToValidate="txtTodate" runat="server" ID="rfvtxtTodate"
                                    ValidationGroup="subbmit" ErrorMessage="Enter To Date." Text="Enter To Date."
                                    class="validationred" Display="Dynamic" />
                            </div>

                            <div class="col-sm-3">
                                <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary green" ValidationGroup="subbmit"
                                    Text="Submit" OnClick="cmdSubmit_Click" />
                                <asp:Button runat="server" ID="btnExportPdf" class="btn btn-primary green pdf"
                                    Text="PDF" OnClick="btnExportPdf_Click" />
                            </div>

                        </div>

                        <div class="form-group">
                            <asp:GridView ID="gvReport" runat="server" AllowPaging="true" AllowSorting="true" EmptyDataText="No Records Found" ShowFooter="true"
                                AutoGenerateColumns="False"
                                Width="100%" CssClass="table table-bordered table-striped mb-none dataTable no-footer">
                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>

                                    <asp:TemplateField HeaderText="SN">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="costcentrecode" HeaderText="Cost Center Code">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="costcentrename" HeaderText="Cost Center Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PaymentAmount" HeaderText="Paid Amount">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CancelAmount" HeaderText="Canceled Amount">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Returnamount" HeaderText="Return Amount">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalSpendAmount" HeaderText="Spend Amount">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                    <!-- /.box-body -->

                </div>
                <!-- /.box -->


            </div>

        </div>
        <!-- /.row -->
    </section>

</asp:Content>

