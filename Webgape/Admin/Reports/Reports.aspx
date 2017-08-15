<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="Webgape.Admin.Reports.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/admin/dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Reports</li>
        </ul>
        <div class="nav-search" id="nav-search">
            <div class="form-search">
                <span class="input-icon"></span>
            </div>
    </div>
    </div>
            <div class="page-content">
                <div class="page-header position-relative">
                    <h1>Reports
                    </h1>
                </div>
                <div class="row-fluid">
                    <div class="span3">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat-1">
                                <h4 class="lighter">
                                    <i class="icon-refresh  orange"></i>Customers
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="widget-body margin-top">
                                <ul class="unstyled spaced">
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrderStatistic.aspx" title="">Order Statistics</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/MaillogList.aspx" title="">Mail Log</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/SearchlogList.aspx" title="">Search Log</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/ContactInquiries.aspx" title="">Inquiries List</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/LowInventory.aspx" title="">Low Inventory</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/CustomerProfitableReport.aspx" title="">Profitable Customers</a></li>
                                    <li style="display: none;"><i class="icon-angle-right"></i><a href="/Admin/Reports/ItemsBySales.aspx" title="">Best Selling Items</a></li>
                                    <li style="display: none;"><i class="icon-angle-right"></i><a href="/Admin/Reports/OrderTaxReport.aspx" title="">Order Tax Report</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/StoreStatistics.aspx" title="">Store Statistics</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat-1">
                                <h4 class="lighter">
                                    <i class="icon-refresh  orange"></i>Orders/Sales
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="widget-body margin-top">
                                <ul class="unstyled spaced">
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdSalesItemByMarket.aspx" title="">Item Sales by market</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdComparativeSalesByTime.aspx" title="">Comparative sales by time period</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/ExportOrdersByDate.aspx" title="">Export Orders By Date</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdPrintPendingPackageSlip.aspx" title="">Print Pending Pick List</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdExportSalesSummarybyItemStore.aspx" title="">Export Sales summary by Item/Store</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrderBeneficialReport.aspx" title="">Order Beneficial Report</a>
                                    </li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrderStateTaxReport.aspx" title="">Order State Tax Report</a>
                                    </li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrderNumberSalesTaxReport.aspx" title="">Order Number Sales Tax Report</a>

                                    </li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/TopSellingProduct.aspx" title="">Top Selling Product</a>

                                    </li>

                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat-1">
                                <h4 class="lighter">
                                    <i class="icon-refresh  orange"></i>Customers
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="widget-body margin-top">
                                <ul class="unstyled spaced">
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/custMyCustomers.aspx" title="">My Customers</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/custTopCustomers.aspx" title="">My Top 50 Repeat Customers</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/custExportOrders.aspx" title="">Export Customers/Order Summary</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/custExportOrdersByStore.aspx" title="">Export Customers/Sales Summary
                                        by Item/Store</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/custReminders.aspx" title="">Reminders/Pending Tasks</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="span3">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat-1">
                                <h4 class="lighter">
                                    <i class="icon-refresh  orange"></i>Business Accounting Reports
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="widget-body margin-top">
                                <ul class="unstyled spaced">
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/fiSearchOrderByCC.aspx" title="">Search Order By CC#</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdSaleSummaryForShippedOrders.aspx" title="">Sales Summary By Store (Shipped
                                        Orders)</a></li>
                                    <li><i class="icon-angle-right"></i><a href="/Admin/Reports/OrdSaleSummaryForReceivedOrders.aspx" title="">Sales summary By Store (Received
                                        Orders)</a></li>
                                    <li><i class="icon-angle-right"></i><a href=" OrdSaleSummaryByStoreShipped.aspx" title="">Sales Summary By Store based
                                        on Shipped Date</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
