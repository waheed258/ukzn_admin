<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="ViewQuotationDetails.aspx.cs" Inherits="SalesAdmin_ViewQuotationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        table {
            color: #333;
            font-family: Helvetica, Arial, sans-serif;
            width: 100%;
        }

        tr.border_bottom td {
            border-top: 1pt solid black;
        }

        td, th {
            height: 30px;
            transition: all 0.3s; /* Simple transition for hover effect */
        }

        th {
            background: #DFDFDF; /* Darken header a bit */
            font-weight: bold;
            text-align: center;
        }

        td {
            background: #FAFAFA;
            text-align: center;
        }

        /* Cells in even rows (2,4,6...) are one color */
        tr:nth-child(even) td {
            background: #F1F1F1;
        }

        /* Cells in odd rows (1,3,5...) are another (excludes header cells)  */
        tr:nth-child(odd) td {
            background: #FEFEFE;
        }

        tr td:hover {
            background: #666;
            color: #FFF;
        }
        /* Hover cell effect! */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Quotation Details
       
               
            </h1>

            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Settings</a></li>
                <li class="active">Quotation Details</li>
            </ol>
        </section>
        <section class="content">
             <asp:Label ID="lblMsg" runat="server"></asp:Label>
             <div class="row">

                 <div class="col-md-12">
                     <div class="box box-info">
                        <div class="box-body form-horizontal">
                            
                             <div class="col-lg-5" style="font-size: 17px; padding: 9px; color: #2196f3; font-weight: bold;">
                 
                    <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
            </div>
                               
                             <div class="form-group">
                                 <div class="col-sm-12">
                                 Flight Details
        <table>
            <tr>
                <th>Status</th>
                <th>Carrier</th>
                <th>Class</th>
                <th>Origin</th>
                <th>Destination</th>
                <th>Departure</th>
                <th>Arrival</th>
                <th>Cost fare rules</th>

            </tr>
            <asp:Repeater ID="rptQuotation" runat="server" OnItemCommand="rptQuotation_ItemCommand">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class='<%# Eval("TrClass") %>'>
                        <td>

                            <asp:LinkButton ID="lnkBook" runat="server" CommandArgument='<%# Eval("OptionKeys") %>' CommandName='<%# Eval("OptionText") %>'>
                                      <%# Eval("OptionText") %>
                            </asp:LinkButton>
                            <asp:HiddenField ID="hfServiceFee" runat="server" Value='<%# Eval("ServiceFee") %>' />
                            <asp:HiddenField ID="hfFlightRequestId" runat="server" Value='<%# Eval("FlightRequestId") %>' />
                            <asp:HiddenField ID="hfTrvellerId" runat="server" Value='<%# Eval("TrvellerId") %>' />
                        </td>
                        <td>

                            <img src='<%# Eval("airlineimgsrc") %>' />
                            <br />
                            <asp:Label runat="server" ID="lblAirLineCode"
                                Text='<%# Eval("AirLineCode") %>' />
                            <asp:Label runat="server" ID="lblFlightNo"
                                Text='<%# Eval("FlightNo") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCabinClass"
                                Text='<%# Eval("CabinClass") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFromCode"
                                Text='<%# Eval("FromCode") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblToCode"
                                Text='<%# Eval("ToCode") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblDeptDateAndTime"
                                Text='<%# Eval("DeptDateAndTime") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblArrivalDateAndtime"
                                Text='<%# Eval("ArrivalDateAndtime") %>' />
                        </td>
                        <td>
                            <%# Eval("CurrencyCode") %>
                            <asp:Label runat="server" ID="lblTotalPrice"
                                Text='<%# Eval("TotalPrice") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
                                      <br />
        <br />
                                     </div>
                                 </div>
                            <div class="form-group">
                                 <div class="col-sm-12">
                                 Hotel Details
        <table>
            <tr class="border_bottom">
                <th style="text-align: center;">Option</th>
                <th style="text-align: center;">Hotel name & Address</th>
                <th style="text-align: center;">No pax</th>
                <th style="text-align: center;">No nights</th>
                <th style="text-align: center;">Room type</th>
                <th style="text-align: center;">Meal Basis</th>
                <th style="text-align: center;">Total rooms</th>
                <th style="text-align: center;">Cost fare</th>

            </tr>
            <asp:Repeater ID="rptHotelQuotation" runat="server" OnItemCommand="rptHotelQuotation_ItemCommand">
                <HeaderTemplate>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr class="border_bottom">
                        <td>
                            <asp:LinkButton ID="lnkBook" runat="server" CommandArgument='<%# Eval("HotelBookingText") %>' CommandName='<%# Eval("HotelOptionNo") %>'>
                                      <%# Eval("HotelOptionNo") %>
                            </asp:LinkButton>
                            <asp:HiddenField ID="hfServiceFee" runat="server" Value='<%# Eval("HotelServiceFee") %>' />
                            <asp:HiddenField ID="hfHotelRequestId" runat="server" Value='<%# Eval("HotelRequestId") %>' />
                            <asp:HiddenField ID="hfTrvellerId" runat="server" Value='<%# Eval("TrvellerId") %>' />

                            <%-- <asp:RadioButton ID="rbHotelQutID" runat="server" Text='<%# Eval("HotelOptionNo") %>' AutoPostBack="true" OnCheckedChanged="rbHotelQutID_CheckedChanged" />--%>
                        </td>
                        <td>


                            <asp:Label runat="server" ID="lblHotelName"
                                Text='<%# Eval("PropertyName") %>' />
                            <br />
                            <asp:Label runat="server" ID="lblHotelAddress"
                                Text='<%# Eval("HotelAddress") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblNoPax"
                                Text='<%# Eval("NoPax") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblNoNights"
                                Text='<%# Eval("HotelNoNights") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblRoomType"
                                Text='<%# Eval("HotelRoomType") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMealDesc"
                                Text='<%# Eval("MealDesc") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalRooms"
                                Text='<%# Eval("HotelTotalRooms") %>' />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCostFare"
                                Text='<%# Eval("HotelCostFare") %>' />
                        </td>

                    </tr>
                </ItemTemplate>




            </asp:Repeater>
        </table>
                                      <br />
        <br />
                             </div>   </div>
                            <div class="form-group">
                                 <div class="col-sm-12">
                                 Car Hire
         <div style="border-color: red; /* background-color: red; */
    /* padding: 4px 25px; */
    border-style: ridge; border-color: black;">
             <table>
                 <tr>
                     <th>Option</th>
                     <th>Car Model</th>
                     <th>Pickup Location</th>
                     <th>PickUpDate & Time</th>
                     <th>Drop Location</th>
                     <th>DropDate & Time</th>

                     <th>Cost fare</th>

                 </tr>
                 <asp:Repeater ID="rptCars" runat="server" OnItemCommand="rptCars_ItemCommand">
                     <HeaderTemplate>
                     </HeaderTemplate>

                     <ItemTemplate>
                         <tr class="border_bottom">
                             <td>
                                 <asp:LinkButton ID="lnkBook" runat="server" CommandArgument='<%# Eval("CarDescId") %>' CommandName='Book'>
                                      Proceed for booking
                                 </asp:LinkButton>
                             </td>
                             <td>


                                 <asp:Label runat="server" ID="lblCarModel"
                                     Text='<%# Eval("CarModel") %>' />
                                 <br />
                                 <asp:Label runat="server" ID="lblpickdateandtime"
                                     Text='<%# Eval("pickdateandtime") %>' />
                             </td>
                             <td>
                                 <asp:Label runat="server" ID="lblpickuplocation"
                                     Text='<%# Eval("pickuplocation") %>' />
                             </td>
                             <td>
                                 <asp:Label runat="server" ID="lbldroptimeanddate"
                                     Text='<%# Eval("droptimeanddate") %>' />
                             </td>
                             <td>
                                 <asp:Label runat="server" ID="lbldroplocation"
                                     Text='<%# Eval("droplocation") %>' />
                             </td>

                             <td>
                                 <asp:Label runat="server" ID="lblTotalPrice"
                                     Text='<%# Eval("TotalPrice") %>' />
                             </td>
                         </tr>
                     </ItemTemplate>




                 </asp:Repeater>

             </table>
         </div>
                                </div>
</div>
                            </div>
                         </div>
                     </div>

                 </div>


            </section>
       

</asp:Content>

