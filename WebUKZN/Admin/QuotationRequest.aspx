<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="QuotationRequest.aspx.cs" Inherits="Admin_QuotationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        tr.trLastElement td {
            border-bottom: 1px solid black;
        }

        tr:hover {
            background: #f4f4f4;
        }

        th {
            background: #f4f4f4;
        }

        .table {
            border: 3px solid black;
            border-collapse: collapse;
        }
        label {
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper" style="margin-left: 1px;">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Quotations
       
               
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Quotations</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <asp:HiddenField ID="hf_UserMasterId" runat="server" Value="0" />

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!--/.col (left) -->
                    <!-- right column -->
                    <div class="col-md-12">
                        <!-- Horizontal Form -->
                        <div class="box box-info">
                            <%-- <div class="box-header with-border">
                                
                                </div>--%>
                            <!-- /.box-header -->
                            <!-- form start -->

                            <div class="box-body form-horizontal">
                             

                                <div class="box-body form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <label for="inputRequestName" class="control-label">Requester Name</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblRequesertName" runat="server" class="form-control"  Text="mounika"/>
                                           
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="inputRequestEmail" class="control-label">Requester Email</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblRequesterEmail" runat="server" class="form-control"  Text="mounika@gmail.com"/>
                                           
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="inputRequestermobile" class="control-label">Requester Mobile</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblRequesterMobile" runat="server" class="form-control"  Text="9789786786"/>
                                          
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <label for="inputtravelName" class="control-label">Traveller Name</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblTravellerName" runat="server" class="form-control"  Text="Raju"/>
                                           
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="inputTravellerEmail" class="control-label">Traveller Email</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblTravellerEmail" runat="server" class="form-control" Text="raju@gmail.com" />
                                          
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="inputTraveller" class="control-label">Traveller Mobile</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblTravellerMobile" runat="server" class="form-control"  Text="9348328744"/>
                                          
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <label for="inputApprovalNo" class="control-label">Approval No</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lblApprovalNo" runat="server" class="form-control" Text="645645646" />
                                        
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="inputStatus" class="control-label">Status</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                <asp:ListItem Text="Select" Value="-1">Select</asp:ListItem>
                                                     <asp:ListItem Text="Approve" Value="1">Approve</asp:ListItem>
                                                     <asp:ListItem Text="Cancel" Value="2">Cancel</asp:ListItem>
                                            </asp:DropDownList>
                                         

                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <div class="col-sm-2">
                                            <label for="inputRequestName" class="control-label" style="color:#0094ff">Total Amount for trip:</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbltotalAmount" runat="server" class="form-control" Text="15940"  />
                                        </div>

                                       
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-10"></div>
                                         <div class="col-sm-2">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-info" ValidationGroup="Quotation" style="border-radius:8px;width:100px;font-size:17px;" />

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <div class="col-sm-3">
                                                <h4 style="color:#0094ff"><b>Flight Quotation</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <div class="col-sm-3">
                                                <label>Hyderabad,India(HYD)</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <label>Singapore,Changi,Singapore(SIN)</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <table class="table">
                                                <thead class="thead-dark">
                                                    <tr>
                                                        <th scope="col">Option</th>
                                                        <th scope="col">Carrier</th>
                                                        <th scope="col">Class</th>
                                                        <th scope="col">Origin</th>
                                                        <th scope="col">Destination</th>
                                                        <th scope="col">Departure</th>
                                                        <th scope="col">Arrival</th>
                                                        <th scope="col">Cost Fare Rules</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>

                                                        <td>Option 1</td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80" /><br />
                                                            Air India 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad</td>
                                                        <td>Chennai</td>
                                                        <td>01 sep,21:25</td>
                                                        <td>01 sep,22:25</td>
                                                        <td>R 4370.00</td>
                                                    </tr>
                                                    <tr>

                                                        <td></td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80" /><br />
                                                            Air India 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad</td>
                                                        <td>Chennai</td>
                                                        <td>01 sep,21:25</td>
                                                        <td>01 sep,22:25</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr class="trLastElement">


                                                        <td></td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80" /><br />
                                                            Air India 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad</td>
                                                        <td>Chennai</td>
                                                        <td>01 sep,21:25</td>
                                                        <td>01 sep,22:25</td>
                                                        <td></td>

                                                    </tr>
                                                    <tr>

                                                        <td>Option 2</td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80" /><br />
                                                            Emirates 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad,India</td>
                                                        <td>Dubai,UAE</td>
                                                        <td>01 sep,12:25</td>
                                                        <td>01 sep,15:25</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>

                                                        <td></td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80" /><br />
                                                            Emirates 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad,India</td>
                                                        <td>Dubai,UAE</td>
                                                        <td>01 sep,12:25</td>
                                                        <td>01 sep,15:25</td>
                                                        <td>R 2370.00</td>
                                                    </tr>
                                                    <tr class="trLastElement">


                                                        <td></td>
                                                        <td>
                                                            <asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80" /><br />
                                                            Emirates 546</td>
                                                        <td>Economy</td>
                                                        <td>Hyderabad,India</td>
                                                        <td>Dubai,UAE</td>
                                                        <td>01 sep,12:25</td>
                                                        <td>01 sep,15:25</td>
                                                        <td></td>

                                                    </tr>
                                                </tbody>
                                            </table>

                                            <%-- <table class="table">
                                            <thead class="thead-light">
                                                <tr>
                                                    <th scope="col">#</th>
                                                    <th scope="col">First</th>
                                                    <th scope="col">Last</th>
                                                    <th scope="col">Handle</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <th scope="row">1</th>
                                                    <td>Mark</td>
                                                    <td>Otto</td>
                                                    <td>@mdo</td>
                                                </tr>
                                                <tr>
                                                    <th scope="row">2</th>
                                                    <td>Jacob</td>
                                                    <td>Thornton</td>
                                                    <td>@fat</td>
                                                </tr>
                                                <tr>
                                                    <th scope="row">3</th>
                                                    <td>Larry</td>
                                                    <td>the Bird</td>
                                                    <td>@twitter</td>
                                                </tr>
                                            </tbody>
                                        </table>--%>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <div class="col-sm-3">
                                                <h4 style="color:#0094ff"><b>Hotel Quotation</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <table class="table">
                                                <thead class="thead-dark">
                                                    <tr>
                                                        <th scope="col">Option</th>
                                                        <th scope="col">Hotel Name</th>
                                                        <th scope="col">Class</th>
                                                        <th scope="col">Check In</th>
                                                        <th scope="col">Check Out</th>
                                                        <th scope="col">No Of Nights</th>
                                                        <th scope="col">Cost Fare Rules</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>

                                                        <td>Option 1</td>
                                                        <td>Taj Krishna</td>
                                                        <td>Five Star</td>
                                                        <td>23/07/2018</td>
                                                        <td>27/07/208</td>
                                                        <td>05</td>
                                                        <td>R 5000.00</td>
                                                    </tr>
                                                    <tr>

                                                        <td>Option 1</td>
                                                        <td>Avasa</td>
                                                        <td>Three Star</td>
                                                        <td>23/07/2018</td>
                                                        <td>27/07/208</td>
                                                        <td>05</td>
                                                        <td>R 2500.00</td>
                                                    </tr>

                                                </tbody>
                                            </table>


                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <div class="col-sm-3">
                                                <h4 style="color:#0094ff"><b>Car Quotation</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">

                                            <table class="table">
                                                <thead class="thead-dark">
                                                    <tr>
                                                        <th scope="col">Option</th>
                                                        <th scope="col">Car Name</th>

                                                        <th scope="col">Pick Up</th>
                                                        <th scope="col">Drop In</th>

                                                        <th scope="col">Cost Fare Rules</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>

                                                        <td>Option 1</td>
                                                        <td>Audi</td>

                                                        <td>12:45</td>
                                                        <td>1:15</td>

                                                        <td>R 1000.00</td>
                                                    </tr>
                                                    <tr>

                                                        <td>Option 2</td>
                                                        <td>BMW</td>

                                                        <td>12:45</td>
                                                        <td>1:15</td>

                                                        <td>R 700.00</td>
                                                    </tr>

                                                </tbody>
                                            </table>


                                        </div>
                                    </div>
                                </div>
                            <!-- /.box-body -->


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



