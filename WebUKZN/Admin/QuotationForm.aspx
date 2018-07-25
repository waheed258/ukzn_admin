<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="QuotationForm.aspx.cs" Inherits="Admin_QuotationForm" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
      tr.trLastElement  td{ 
    border-bottom: 1px solid black; 
  
}
      tr:hover{
            background:#f4f4f4;
        }
      th{
          background:#f4f4f4;
      }
            .table{
          border :3px solid black; border-collapse:collapse
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
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
                                <div class="form-group">
                                    <div class="col-sm-12">

                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnSendQuotation"
                                                class="btn btn-info" Text="SEND QUOTATION" />&nbsp; &nbsp;
                                        </div>
                                        <div class="col-sm-3">


                                            <asp:Button runat="server" ID="btnAnotherQuotation" class="btn  btn-info"
                                                Text="ADD ANOTHER QUOTATION" />&nbsp;
                   

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">

                                        <div class="col-sm-3">
                                            <h4 class="text-light-blue"><b>Flight Quotation</b></h4>
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
                                                    <td><asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80"/><br />Air India 546</td>
                                                    <td>Economy</td>
                                                    <td>Hyderabad</td>
                                                    <td>Chennai</td>
                                                    <td>01 sep,21:25</td>
                                                    <td>01 sep,22:25</td>
                                                    <td>R 4370.00</td>
                                                </tr>
                                               <tr>
                                                   
                                                     <td></td>
                                                    <td><asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80"/><br />Air India 546</td>
                                                    <td>Economy</td>
                                                    <td>Hyderabad</td>
                                                    <td>Chennai</td>
                                                    <td>01 sep,21:25</td>
                                                    <td>01 sep,22:25</td>
                                                    <td></td>
                                                </tr>
                                                <tr class="trLastElement" >
                                                   
                                                   
                                                     <td></td>
                                                    <td><asp:Image runat="server" ImageUrl="images/air-india-main.png" Height="20" Width="80"/><br />Air India 546</td>
                                                    <td>Economy</td>
                                                    <td>Hyderabad</td>
                                                    <td>Chennai</td>
                                                    <td>01 sep,21:25</td>
                                                    <td>01 sep,22:25</td>
                                                    <td ></td>
                                                
                                                </tr>
                                                 <tr>
                                                   
                                                     <td>Option 2</td>
                                                  <td><asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80"/><br />Emirates 546</td>
                                                    <td>Economy</td>
                                                    <td>Hyderabad,India</td>
                                                    <td>Dubai,UAE</td>
                                                    <td>01 sep,12:25</td>
                                                    <td>01 sep,15:25</td>
                                                    <td></td>
                                                </tr>
                                               <tr>
                                                   
                                                     <td></td>
                                                 <td><asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80"/><br />Emirates 546</td>
                                                    <td>Economy</td>
                                                    <td>Hyderabad,India</td>
                                                    <td>Dubai,UAE</td>
                                                    <td>01 sep,12:25</td>
                                                    <td>01 sep,15:25</td>
                                                    <td>R 2370.00</td>
                                                </tr>
                                                <tr class="trLastElement" >
                                                   
                                                   
                                                     <td></td>
                                                    <td><asp:Image runat="server" ImageUrl="images/flight.png" Height="20" Width="80"/><br />Emirates 546</td>
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
                                            <h4 class="text-light-blue"><b>Hotel Quotation</b></h4>
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
                                            <h4 class="text-light-blue"><b>Car Quotation</b></h4>
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

