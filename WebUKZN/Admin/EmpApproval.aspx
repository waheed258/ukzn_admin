<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EmpApproval.aspx.cs" Inherits="Admin_EmpApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function PrimaryApprovalCheck(PrimaryCk) {

             debugger;

             var gvEmpApproval = document.getElementById("<%=gvEmpLeveApprovals.ClientID%>");

          var chk = gvEmpApproval.getElementsByTagName("input");

          var row = PrimaryCk.parentNode.parentNode;

          for (var i = 0; i < chk.length; i++) {

              if (chk[i].type == "checkbox") {

                  if (chk[i].checked && chk[i] != PrimaryCk) {

                      chk[i].checked = false;

                      break;
                  }

              }

          }
      }

      function RemoveEmpApprovalCheck() {
          var gvEmpApproval = document.getElementById("<%=gvEmpLeveApprovals.ClientID%>");

      }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Employee Approval
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee Approval</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>


            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Employee Approval</h3>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-1">
                                        <label for="inputEmail3" class="control-label">Employee<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlEmpList" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlEmpList" runat="server" ID="rfvtxtEmpFirstName"
                                            ValidationGroup="Employee" ErrorMessage="Enter First Name." Text="Enter First Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>

<%--                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label">Levels Of Approval<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlEmpTotalApproval" runat="server" class="form-control">
                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlEmpTotalApproval" runat="server" ID="rfvtxtEmpLastName"
                                            ValidationGroup="Employee" ErrorMessage="Enter Last Name." Text="Enter Last Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>--%>
                                    <div class="col-sm-2">
                                        <label for="inputEmail3" class="control-label"> Approval Level<span class="validationred"> *</span></label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlEmpCurrentApproval" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpCurrentApproval_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlEmpCurrentApproval" runat="server" ID="RequiredFieldValidator1"
                                            ValidationGroup="Employee" ErrorMessage="Enter Last Name." Text="Enter Last Name." Display="Dynamic"
                                            class="validationred" />
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6">
                                        
                                        
                                            <asp:Button  Width="80px" runat="server" ID="btnEmpLevelAdd"
                                                class="btn btn-outline-primary" ValidationGroup="" Text="Add" OnClick="btnEmpLevelAdd_Click" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button  Width="80px" runat="server" ID="btnRemove" class=" btn btn-outline-danger" ValidationGroup="Employee"
                                                Text="Remove" OnClick="btnRemove_Click" />&nbsp;
                                        </div>
                                   </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6 GridHeightFixed">
                                       <%-- <asp:ListBox ID="lstEmpList" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
                                         <asp:GridView ID="gvEmpData" runat="server" AllowPaging="true" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="UserMasterId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="40" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>
                                     <asp:TemplateField>
                                <ItemTemplate>
                                <asp:CheckBox ID="chkEmpSelect" runat="server" AutoPostBack="true"   />
                                </ItemTemplate>
                                   </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                          
                                           
                                           <%#Eval("ReceivedTransactionId")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="UserMasterId" HeaderText="SN" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center"  />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="UserName" HeaderText="Employee Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   

                                   

                                </Columns>
                            </asp:GridView>
                                    </div>
                                   <%-- <div class="col-sm-2">
                                         <asp:Button runat="server" ID="btnEmpLevelAdd"
                                            class="btn btn-info" ValidationGroup="" Text="Add"  OnClick="btnEmpLevelAdd_Click"/>&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnRemove" class="btn  btn-danger" ValidationGroup="Employee"
                                           Text="Remove" OnClick="btnRemove_Click" />&nbsp;
                                    </div>--%>
                                    <div class="col-sm-6 GridHeightFixed">
                                       
                                       <asp:GridView ID="gvEmpLeveApprovals" runat="server" AllowPaging="true" EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered table-striped mb-none dataTable no-footer" DataKeyNames="EmpId"
                                AutoGenerateColumns="False"
                                Width="100%" PageSize="40" usecustompager="true">

                                <AlternatingRowStyle CssClass="gradeA even" />
                                <FooterStyle BackColor="#08376a" />
                                <RowStyle CssClass="gradeA odd" />
                                <Columns>
                                     <asp:TemplateField>
                                <ItemTemplate>
                                <asp:CheckBox ID="chkEmpAppovalSelect" runat="server" AutoPostBack="true"   onclick="RemoveEmpApprovalCheck();"  />
                                </ItemTemplate>
                                   </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SN" HeaderStyle-CssClass="panel-heading" ItemStyle-CssClass="gradeC">
                                        <ItemTemplate>
                                          
                                           
                                           <%#Eval("ReceivedTransactionId")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="EmpId" HeaderText="SN" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="Employee Name">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                   
                                     <asp:BoundField DataField="LevelId" HeaderText="Level Id">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Primary Approval">
                                <ItemTemplate >
                                   
                                <asp:CheckBox ID="chkEmpPrimarySelect" runat="server" AutoPostBack="true" Checked='<%# Convert.ToInt32(Eval("PrimaryLevelId")) == Convert.ToInt32(Eval("EmpId")) ? true : false %>'  onclick="PrimaryApprovalCheck(this);"  OnCheckedChanged="chkEmpPrimarySelect_CheckedChanged" />
                                </ItemTemplate>
                                   </asp:TemplateField>
                                   
                                   

                                </Columns>
                            </asp:GridView>
                                        </div>
                                </div>

                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <div class="col-sm-5">
                                            </div>
                                    <div class="col-sm-5">
                                   
                                        <asp:Button runat="server" ID="btnUserCancel"
                                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnUserCancel_Click" />&nbsp; &nbsp;
                                       <asp:Button runat="server" ID="btnEmpApprovalSave" OnClick="btnEmpApprovalSave_Click" class="btn  btn-info" ValidationGroup="Employee"
                                           Text="Submit" />&nbsp;
                   
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <!-- /.box-footer -->

                        </div>
                        <!-- /.box -->

                    </div>
                    <!--/.col (right) -->

                </div>
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>

