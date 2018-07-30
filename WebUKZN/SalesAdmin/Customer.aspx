<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="SalesAdmin_Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <section class="content-header">
            <h1>CUSTOMER
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Employee</li>
            </ol>
        </section>
        <section class="content">
            <asp:HiddenField ID="hfUserId" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <div class="form-group">
                                <asp:Label ID="labelError" class="message" ForeColor="Red" runat="server" Text=""
                                    EnableViewState="false"></asp:Label>
                            </div>
                        </div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CUSTOMER</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">User Type</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="dropUserType" runat="server" class="form-control">
                                        <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>--%>
                                        <%--<asp:ListItem Text="Super Admin" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                      <asp:ListItem Text="Consultant" Value="3"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="dropUserType" runat="server" ID="rfvdropUserType"
                                        ValidationGroup="akki" ErrorMessage="Select User Type." Text="Select User Type."
                                        class="validationred" Display="Dynamic" InitialValue="0" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Location Id</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="dropLocation" runat="server" class="form-control">
                                        <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Durban" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Johannesburg" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Capton" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="dropLocation" runat="server" ID="rfvdropLocation"
                                        ValidationGroup="akki" ErrorMessage="Select Location Id." Text="Select Location Id."
                                        class="validationred" Display="Dynamic" InitialValue="-1" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Status</label>
                                </div>
                                <div class="col-sm-3">

                                    <asp:DropDownList ID="dropStatus" runat="server" class="form-control">
                                        <%--<asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>
                     <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="In Active" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Hold" Value="3"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="dropStatus" runat="server" ID="rfvdropStatus"
                                        ValidationGroup="akki" ErrorMessage="Select Status." Text="Select Status."
                                        class="validationred" Display="Dynamic" InitialValue="0" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">First Name</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control" MaxLength="50" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" ID="rfvtxtFirstName"
                                        ValidationGroup="akki" ErrorMessage="Enter your First name." Text="Enter your First name."
                                        class="validationred" Display="Dynamic" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Last Name</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtLastName" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" ID="rfvtxtLastName"
                                        ValidationGroup="akki" ErrorMessage="Enter your Last name." Text="Enter your Last name."
                                        class="validationred" Display="Dynamic" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Mobile Number</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtMobileNumber" runat="server" class="form-control" MaxLength="15" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtMobileNumber" runat="server" ID="rfvtxtMobileNumber"
                                        ValidationGroup="akki" ErrorMessage="Enter your Mobile number." Text="Enter your Mobile number."
                                        class="validationred" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ControlToValidate="txtMobileNumber" runat="server"
                                        ID="revtxtMobileNumber" ValidationGroup="akki" ErrorMessage="Enter your right mobile number."
                                        Text="Enter your right mobile number." class="validationred" Display="Dynamic"
                                        ValidationExpression="^[0-9]{10,15}$" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">LandLine Number</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtLandLineNumber" runat="server" class="form-control" MaxLength="20" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtLandLineNumber" runat="server" ID="rfvtxtLandLineNumber"
                                        ValidationGroup="akki" ErrorMessage="Enter your LandLine number." Text="Enter your LandLine number."
                                        class="validationred" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ControlToValidate="txtLandLineNumber" runat="server"
                                        ID="revtxtLandLineNumber" ValidationGroup="akki" ErrorMessage="Enter your right LandLine number."
                                        Text="Enter your right LandLine number." class="validationred" Display="Dynamic"
                                        ValidationExpression="^[0-9]{10,15}$" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Email Id</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtEmailId" runat="server" class="form-control" MaxLength="255" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtEmailId" runat="server" ID="rfvtxtEmailId"
                                        ValidationGroup="akki" ErrorMessage="Enter your Email id." Text="Enter your Email id."
                                        class="validationred" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="txtEmailId" runat="server" ID="reftxtEmailId" ValidationGroup="akki"
                                        ErrorMessage="Enter your right Email id" Text="Enter your right Email id" class="validationred"
                                        Display="Dynamic" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <asp:Image ID="Image1" runat="server" Height="100px" Width="90px" ImageUrl="~/images/no_image.png" />
                                </div>
                                <div class="col-sm-3">
                                    <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" Style="padding: 0px; border: none;"
                                        onchange="return PreviewImage(this);" />
                                </div>
                            </div>
                         <div class="form-group">
                              <asp:Panel ID="PanelPassword" runat="server">
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Password</label>
                                </div>
                                <div class="col-sm-3">
                                     <asp:TextBox ID="txtPassword" runat="server" class="form-control" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" ID="rfvtxtPassword"
                        ValidationGroup="akki" ErrorMessage="Enter your Password." Text="Enter your Password."
                        class="validationred" Display="Dynamic" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-2">
                                    <label for="inputEmail3" class="control-label">Confirm Password</label>
                                </div>
                                <div class="col-sm-3">
                                   <asp:TextBox ID="txtConfirmPawd" runat="server" class="form-control" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtConfirmPawd" runat="server" ID="rfvtxtConfirmPawd"
                        ValidationGroup="akki" ErrorMessage="Enter your Confirm Password." Text="Enter your Confirm Password."
                        class="validationred" Display="Dynamic" />
                     <asp:CompareValidator ID="cvtxtConfirmPawd" runat="server" ControlToValidate="txtConfirmPawd"
                        ControlToCompare="txtPassword" Operator="Equal" Type="String" Text="Password must be equal to Confirm Password."
                        ErrorMessage="Password must be equal to Confirm Password." class="validationred"
                        ValidationGroup="akki" Display="Dynamic"></asp:CompareValidator>
                                </div>
                                  </asp:Panel>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <div class="form-group">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" ID="btnCancel"
                                            class="btn btn-danger" ValidationGroup="" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
                                    <asp:Button runat="server" ID="cmdSubmit" class="btn  btn-info" ValidationGroup="akki"
                                        Text="Submit" OnClick="cmdSubmit_Click" />&nbsp;
                                </div>
                            </div>


                        </div>
                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->

                </div>
                <!--/.col (right) -->

            </div>


        </section>
    
</asp:Content>

