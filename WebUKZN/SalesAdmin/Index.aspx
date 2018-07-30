<%@ Page Title="" Language="C#" MasterPageFile="~/SalesAdmin/SalesAdmin.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="SalesAdmin_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
        <!-- Content Header (Page header) -->
        <section class="content-header">
          
            <h2>Welcome UKZN Travel management system       
            </h2>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
              <asp:Label ID="lblMsg" runat="server"></asp:Label>
           <div class="row" style="display: none;">
        <asp:HiddenField ID="hfDayReport" runat="server" />
        <asp:HiddenField ID="hfDayReportValues" runat="server" />
        <div class="col-md-6">
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Daily Report</h2>
                    <p class="panel-subtitle">

                        <asp:Label ID="lblDayTitle" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body">

                    <!-- Flot: Pie -->
                    <div class="ct-chart ct-perfect-fourth ct-golden-section" id="flotPie" style="height: 290px;"></div>


                </div>
            </section>
        </div>

        <div class="col-md-6">
            <asp:HiddenField ID="hfMonthReport" runat="server" />
            <asp:HiddenField ID="hfMonthReportValues" runat="server" />
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Monthly Report</h2>
                    <p class="panel-subtitle">
                        <asp:Label ID="lblMonthTitle" runat="server"></asp:Label>
                    </p>
                </header>
                <div class="panel-body">
                    <div id="ChartistExtremeResponsiveConfiguration" class="ct-chart ct-perfect-fourth ct-golden-section" style="height: 290px;"></div>

                    <!-- See: assets/javascripts/ui-elements/examples.charts.js for the example code. -->
                </div>
            </section>
        </div>

    </div>

    <div class="row" style="display: none;">

        <asp:HiddenField ID="hfYearMonth" runat="server" />
        <asp:HiddenField ID="hfYearValue" runat="server" />


        <div class="col-md-6">

            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Yearly Report (<asp:Label ID="lblYearTitel" runat="server"></asp:Label>)</h2>

                </header>
                <div class="panel-body">

                    <!-- Flot: Bars -->
                    <div class="chart chart-md" id="flotBars"></div>


                </div>
            </section>
        </div>


        <div class="col-md-6">


            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="panel-action panel-action-toggle" data-panel-toggle></a>
                        <a href="#" class="panel-action panel-action-dismiss" data-panel-dismiss></a>
                    </div>

                    <h2 class="panel-title">Target Report</h2>
                </header>

                <div class="panel-body" runat="server" id="divTarget" style="height: 375px;">
                </div>
                <!-- See: assets/javascripts/ui-elements/examples.charts.js for the example code. -->

            </section>



        </div>
    </div>
        </section>
        <!-- /.content -->
    <!-- /.content-wrapper -->
</asp:Content>
