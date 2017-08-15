<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Visitor.aspx.cs" Inherits="Webgape.Visitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/style/js/jquery.slickforms.js"></script>
    <script src="/style/js/jquery.flexslider-min.js"></script>
    <script src="/style/js/jquery.tools.min.js"></script>
    <script src="/style/js/scriptsLatest.js"></script>
    <link href="/style/css/grid.css" rel="stylesheet" />
    <style type="text/css">
        ul.tabs li a.current,
        .panes {
            background-color: rgba(0, 0, 0, 0.25);
        }

        ul.tabs li a,
        .toggle h4.title.active {
            background-color: rgba(0, 0, 0, 0.40);
        }

        ul.tabs li a {
            font-weight: bold;
        }

        ul.tabs li a {
            font-family: Open Sans Condensed;
        }
    </style>

    <%--Country Chart--%>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">   
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawCountryChart);
        function drawCountryChart() {
            var data = google.visualization.arrayToDataTable([        
          <%=CountryChart %>
            ]);
            var formatter = new google.visualization.NumberFormat({
                prefix: ' '
            });
            formatter.format(data, 1);         
            var options = {
                title: <%=CountryChartTitle %>,
                is3D: true,
                pieSliceText: 'label',
                slices: {  4: {offset: 0.2},
                   
                },
            };

            var chart = new google.visualization.PieChart(document.getElementById('Countrychart_div'));
            chart.draw(data, options);   
        }
    </script>

    <script type="text/javascript">   
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawCityChart);
        function drawCityChart() {
            var data = google.visualization.arrayToDataTable([        
          <%=CityChart %>
            ]);
            var formatter = new google.visualization.NumberFormat({
                prefix: ' '
            });
            formatter.format(data, 1);         
            var options = {
                title: <%=CityChartTitle %>,
                is3D: true,
                pieSliceText: 'label',
                slices: {  4: {offset: 0.2},
                   
                },
            };

            var chart = new google.visualization.PieChart(document.getElementById('Citychart_div'));
            chart.draw(data, options);   
        }
    </script>
    <%--Country Chart--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="wrapper" style="overflow: inherit;">
        <div style="margin-bottom: 5px">
            <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
            <!-- All page main advertise -->
            <ins class="adsbygoogle"
                style="display: block"
                data-ad-client="ca-pub-1373088425496976"
                data-ad-slot="5649438247"
                data-ad-format="auto"></ins>
            <script>
                (adsbygoogle = window.adsbygoogle || []).push({});
            </script>
        </div>

        <div class="content box" style="overflow: inherit;">
            <h1 class="title">Visitor Details
            </h1>

            <h2 class="widget-title" style="font-size: 15px;">
                Visitors Details are unavailable currently. Please check later.
            </h2>
            <h2>
                <asp:Literal ID="ltrentity" runat="server"></asp:Literal>
            </h2>
            <div class="sidebox widget" style="overflow: inherit; display: none;">
                <!-- the tabs -->
                <ul class="tabs">

                    <li><a class="" href="#">Chart</a></li>
                    <li><a class="" href="#">Tabular</a></li>
                </ul>
                <!-- tab "panes" -->
                <div class="panes" style="overflow: inherit;">

                    <div class="pane" style="overflow: inherit;">
                        <div id="Countrychart_div" style="width: 100%; min-width: 470px; height: 400px;">
                        </div>
                        <br />
                        <label for="form-field-1" class="control-label">Select Country:</label>&nbsp;
                        <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                            AutoPostBack="True" Style="width: 30%" CssClass="searchbox"
                            DataTextField="Country" DataValueField="Country">
                        </asp:DropDownList>
                        <br />
                        <div id="Citychart_div" style="width: 100%; min-width: 470px; height: 400px;">
                        </div>
                    </div>
                    <div class="pane">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-container">

                                    <asp:GridView ID="grdCountryVisitor" OnPageIndexChanging="grdCountryVisitor_PageIndexChanging" runat="server" Width="60%" AutoGenerateColumns="False"
                                        CssClass="mGrid" AllowPaging="True" GridLines="None" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt"
                                        PageSize="15">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:BoundField DataField="CountryName" ItemStyle-Width="75%" HeaderText="CountryName" />
                                            <asp:BoundField DataField="TotalCount" ItemStyle-Width="25%" HeaderText="TotalCount" />
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                    <br />
                                    <label for="form-field-1" class="control-label">Select Country:</label>&nbsp;
                            <asp:DropDownList ID="ddlcity" runat="server" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged"
                                AutoPostBack="True" Style="width: 30%" CssClass="searchbox"
                                DataTextField="Country" DataValueField="Country">
                            </asp:DropDownList>
                                    <br />
                                    <asp:GridView ID="grdCityVisitor" runat="server" OnPageIndexChanging="grdCityVisitor_PageIndexChanging" Width="60%" AutoGenerateColumns="False"
                                        CssClass="mGrid" AllowPaging="True" GridLines="None" PagerStyle-CssClass="pgr"
                                        AlternatingRowStyle-CssClass="alt"
                                        PageSize="15">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:BoundField DataField="City" ItemStyle-Width="75%" HeaderText="City" />
                                            <asp:BoundField DataField="TotalCount" ItemStyle-Width="25%" HeaderText="TotalCount" />
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clear"></div>
            </div>

        </div>
        <div class="sidebar box">
            <div class="sidebox widget">
                <h3 class="widget-title">Advertise</h3>
                <div>
                    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                    <!-- Post Page Responsive Side Menu -->
                    <ins class="adsbygoogle"
                        style="display: block"
                        data-ad-client="ca-pub-1373088425496976"
                        data-ad-slot="6569598241"
                        data-ad-format="auto"></ins>
                    <script>
                        (adsbygoogle = window.adsbygoogle || []).push({});
                    </script>
                </div>
            </div>
            <div class="sidebox widget">
                <div class="clear"></div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <div class="wrapper">

        <div style="margin-top: 10px;">
            <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
            <!-- Index Page Bottom Advertise -->
            <ins class="adsbygoogle"
                style="display: block"
                data-ad-client="ca-pub-1373088425496976"
                data-ad-slot="2139398647"
                data-ad-format="auto"></ins>
            <script>
                (adsbygoogle = window.adsbygoogle || []).push({});
            </script>
        </div>
    </div>
    <script src="/style/js/scripts.js"></script>
</asp:Content>
