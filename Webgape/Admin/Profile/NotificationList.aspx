<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="NotificationList.aspx.cs" Inherits="Webgape.Admin.Profile.NotificationList" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function chkHeight() {
            var windowHeight = 0;
            windowHeight = $(document).height(); //window.innerHeight;

            document.getElementById('prepage').style.height = windowHeight + 'px';
            document.getElementById('prepage').style.display = '';
        }

        function validation() {
            if ((document.getElementById('<%= txtSearch.ClientID %>').value).replace(/^\s*\s*$/g, '') == '') {
                jAlert('Please Enter Search Term', 'Message', '<%= txtSearch.ClientID %>');
                $('html, body').animate({ scrollTop: $('#<%= txtSearch.ClientID %>').offset().top }, 'slow');
                return false;
            }
            else {
                chkHeight();
            }

        }
    </script>
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Notification </li>
        </ul>
        <ul class="breadcrumb" style="float: right">
            <li><i class="icon-book home-icon"></i><a href="Admin/Log.aspx?entity=Message">Log</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
        </ul>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Notification List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header" style="display: none;">
                            <h4>Search By</h4>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div class="span6">
                                    </div>
                                    <div class="span6">
                                        <div class="row-fluid">
                                            <div class="pagination no-margin right">
                                                <table width="100%" align="right" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="padding-right: 4px;">Search :</td>

                                                        <td style="padding-right: 4px;">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlSearch" runat="server" Style="width: 150px;" CssClass="no-margin"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem Value="Type">Type</asp:ListItem>
                                                                        <asp:ListItem Value="Notification">Notification</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="padding-right: 4px;">
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="input-medium no-margin no-float"
                                                                Style="vertical-align: text-top" ValidationGroup="SearchGroup" onkeyup="Checktxt();"></asp:TextBox>
                                                        </td>
                                                        <td style="padding-right: 0;">
                                                            <asp:Button ID="btnSearch" runat="server" data-toggle="button" CssClass="btn btn-mini btn-info btn-padding"
                                                                OnClientClick="return validation();  javascript:chkHeight(); " Text="Search" OnClick="btnSearch_Click" />
                                                            <asp:Button ID="btnShowall" runat="server" data-toggle="button" CssClass="btn btn-mini btn-info btn-padding"
                                                                OnClick="btnShowall_Click" Text="Show All" OnClientClick="javascript:chkHeight();"
                                                                CausesValidation="False" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <span style="float: right;">Total Notification:
                                                                   
                                                                <% =Notificationcount%>
                                                                </span>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row-fluid margin-top">
                    <div class="span12">
                        <table id="sample-table-2" width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="padding-top: 0px; padding-bottom: 0px;">
                                    <asp:GridView ID="grdNotification" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        GridLines="None" EmptyDataText="No Record(s) Found."
                                        AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" ShowHeaderWhenEmpty="true"
                                        EmptyDataRowStyle-HorizontalAlign="Center" CssClass="table table-border-display background-default"
                                        ViewStateMode="Enabled" Width="100%" AllowPaging="True" PageSize="<%$ appSettings:GridPageSize %>"
                                        RowStyle-ForeColor="Black" HeaderStyle-ForeColor="#3c2b1b" PagerSettings-Mode="NumericFirstLast"
                                        OnRowDataBound="grdNotification_RowDataBound" OnPageIndexChanging="grd_PageIndexChanging"
                                        CellSpacing="1" BorderStyle="solid" BorderWidth="1" BorderColor="#e7e7e7">
                                        <EmptyDataTemplate>
                                            <span style="color: Red; font-size: 12px; text-align: center;">No Record(s) Found !</span>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Type
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrfrom" runat="server" Text='<%# SetName(Convert.ToString(Eval("Type")))%>'></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="60%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Notification
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrto" runat="server" Text='<%# SetName(Convert.ToString(Eval("Notification")))%>'></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <a id="hplView" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "Link")%>'><span class="green"><i class="icon-eye-open bigger-160"></i></span></a>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="tdpro-checkbox"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="30"></PagerSettings>
                                        <PagerStyle HorizontalAlign="right" CssClass="numbering" />
                                        <RowStyle HorizontalAlign="left" CssClass="eve-row" ForeColor="#000000" />
                                        <AlternatingRowStyle CssClass="odd-row" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                        <HeaderStyle ForeColor="White" Font-Bold="false" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="prepage" style="position: absolute; font-family: arial; font-size: 16; left: 0px; top: 0px; background-color: #000; opacity: 0.7; filter: alpha(opacity=70); layer-background-color: white; height: 100%; width: 100%; z-index: 1000; display: none;">
                <div style="border: 1px solid #ccc;">
                    <table width="100%" style="position: fixed; top: 50%; left: 50%; margin: -50px 0 0 -100px;">
                        <tr>
                            <td>
                                <div style="background: none repeat scroll 0 0 rgba(0, 0,0, 0.9) !important; border: 1px solid #ccc; width: 10%; height: 3%; padding: 20px; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;">
                                    <center>
                                        <img alt="" src="/images/loding.png" style="text-align: center;" /><br />
                                        <asp:Literal ID="ltadd" runat="server" Text="<b class='loadincolor'>Loading ... ... Please wait!</b>"></asp:Literal>
                                    </center>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
