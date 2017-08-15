<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="AppConfigList.aspx.cs" Inherits="Webgape.Admin.Settings.AppConfigList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        @media screen and (-webkit-min-device-pixel-ratio:0) {
            .mybtn {
                line-height: 28px;
                height: 28px;
                line-height: 28px\9;
                height: 28px\9;
                *line-height: 28px;
                *height: 28px;
                _line-height: 28px;
                _height: 28px;
            }
        }
    </style>
    <script language="javascript" type="text/javascript">
        function validation() {
            var a = document.getElementById('<%=txtSearch.ClientID %>').value;
            if (a == "") {
                $(document).ready(function () { jAlert('Please Enter Search Value.', 'Message', '<%=txtSearch.ClientID %>'); });
                return false;
            }
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function selectAll(on) {
            var myform = document.forms[0];
            var len = myform.elements.length;
            for (var i = 0; i < len; i++) {
                if (myform.elements[i].type == 'checkbox') {
                    if (on.toString() == 'false') {

                        if (myform.elements[i].checked) {
                            myform.elements[i].checked = false;
                        }
                    }
                    else {
                        myform.elements[i].checked = true;
                    }
                }
            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        function checkCount() {
            var myform = document.forms[0];
            var len = myform.elements.length;
            var count = 0;
            for (var i = 0; i < len; i++) {
                if (myform.elements[i].type == 'checkbox') {
                    if (myform.elements[i].checked == true) {
                        count += 1;
                    }


                }
            }
            if (count == 0) {
                $(document).ready(function () { jAlert('Check at least one Application Configuration!', 'Message', ''); });
                return false;
            }
            else {
                return DeleteConfirm();
            }
            return false;
        }
        function DeleteConfirm() {
            jConfirm('Are you sure want to delete all selected Application Configuration(s) ?', 'Confirmation', function (r) {
                if (r == true) {

                    document.getElementById('ContentPlaceHolder1_btnDeleteTemp').click();
                    return true;
                }
                else {

                    return false;
                }
            });
            return false;
        }




    </script>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Application Configuration</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Application Configuration
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar">
                        <a href="/Admin/Settings/AppConfiguration.aspx">
                            <span class="btn btn-small btn-primary"><i class="icon-plus-sign"></i>Application Configuration </span></a>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header">
                            <h4>Search By</h4>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row-fluid">

                                    <div class="span6">
                                    </div>
                                    <div class="span6">
                                        <div class="row-fluid">
                                            <div class="pagination no-margin right" style="width: 100%">
                                                <span class="star" style="vertical-align: middle; text-align: center;">
                                                    <asp:Label ID="lblmsg" runat="server"></asp:Label></span>
                                                <asp:ObjectDataSource ID="_gridObjectDataSource" runat="server" EnablePaging="True"
                                                    TypeName="Solution.Bussines.Components.ConfigurationComponent" SelectMethod="GetDataByFilter"
                                                    StartRowIndexParameterName="startIndex" MaximumRowsParameterName="pageSize" SortParameterName="sortBy"
                                                    SelectCountMethod="GetCount">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlStore" DbType="Int32" Name="StoreId" DefaultValue="" />
                                                        <asp:ControlParameter ControlID="ddlSearch" DbType="String" Name="SearchBy" />
                                                        <asp:ControlParameter ControlID="txtSearch" DbType="String" Name="SearchValue" DefaultValue="" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td style="width: 20%" align="right">Search :
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:DropDownList ID="ddlSearch" AutoPostBack="False" Width="175px" runat="server"
                                                                CssClass="order-list no-margin-bottom">
                                                                <asp:ListItem>ConfigName</asp:ListItem>
                                                                <asp:ListItem>ConfigValue</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox ID="txtSearch" runat="server" Width="160px" CssClass="span4" Style="margin-bottom: 0px;"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 5%">
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-mini btn-info btn-padding" Text="Search" OnClientClick="return validation();" />
                                                        </td>
                                                        <td style="width: 5%; margin-right: 10px; padding-right: 0px;">
                                                            <asp:Button ID="btnShowall" runat="server" OnClick="btnShowall_Click" CssClass="btn btn-mini btn-info btn-padding margin-right" Text="Show all" />
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
            </div>
        </div>
        <div class="row-fluid margin-top">
            <div class="span12">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="add-product">
                    <tr>
                        <td>
                            <asp:GridView ID="grdApplicationConfig" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="No Record(s) Found." AllowSorting="True" EmptyDataRowStyle-ForeColor="Red"
                                EmptyDataRowStyle-HorizontalAlign="Center" ViewStateMode="Enabled" CssClass="table table-striped table-bordered table-hover dataTable"
                                CellPadding="2" CellSpacing="1" GridLines="None"
                                Width="100%" AllowPaging="True" PageSize="<%$ appSettings:GridPageSize %>" PagerSettings-Mode="NumericFirstLast"
                                DataSourceID="_gridObjectDataSource" OnRowCommand="grdApplicationConfig_RowCommand"
                                OnRowDataBound="grdApplicationConfig_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            Select
                                        </HeaderTemplate>
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" CssClass="input-checkbox" />
                                            <asp:HiddenField ID="hdnConfigid" runat="server" Value='<%#Eval("AppConfigID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Config Name" SortExpression="ConfigName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Config Name
                                                                <asp:ImageButton ID="btnConfigName" runat="server" CommandArgument="DESC" CommandName="ConfigName"
                                                                    AlternateText="Ascending Order" OnClick="Sorting" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigName" runat="server" Text='<%# Bind("ConfigName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Config Value
                                                                <asp:ImageButton ID="btnConfigValue" runat="server" CommandArgument="DESC" CommandName="ConfigValue"
                                                                    AlternateText="Ascending Order" OnClick="Sorting" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblConfigVal" runat="server" Text='<%# Bind("ConfigValue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigStoreName" runat="server" Text='<%# Bind("StoreName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created On">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigId" runat="server" Text='<%# Bind("CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operations">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="_editLinkButton1" ToolTip="Edit" CommandName="edit"
                                                CommandArgument='<%# Eval("AppConfigID") %>'><span><i class="icon-edit bigger-160 green"></i></span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="_DeleteLinkButton1" ToolTip="Delete" CommandName="DeleteAppConfig"
                                                CommandArgument='<%# Eval("AppConfigID") %>' message="Are you sure want to delete current Application Configuration ?"
                                                OnClientClick='return confirm(this.getAttribute("message"))'><span><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
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
        <div class="row-fluid">
            <table style="width: 100%; margin-top: 10px;">
                <tr id="tr1" runat="server">
                    <td style="width: 70%; float: left;">
                        <div class="span6">
                            <div class="row-fluid" style="width: 250px;">

                                <span><a id="lkbAllowAll" href="javascript:selectAll(true);" class="btn btn-mini btn-info mybtn">Check All</a> | <a id="lkbClearAll"
                                    href="javascript:selectAll(false);" class="btn btn-mini btn-info mybtn">Clear All</a> </span><span style="float: right; padding-right: 0px;"></span>
                            </div>
                        </div>
                    </td>
                    <td style="float: right">
                        <asp:LinkButton ID="btnDeleteConfig" runat="server" OnClientClick="return checkCount();" CssClass="btn btn-mini btn-info mybtn"
                            CommandName="DeleteMultiple" OnClick="btnDeleteConfig_Click">Delete</asp:LinkButton>
                        <div style="display: none">
                            <asp:Button ID="btnDeleteTemp" runat="server" ToolTip="Delete" OnClick="btnDeleteConfig_Click" />
                        </div>
                    </td>

                </tr>
            </table>
        </div>
    </div>

    <div id="content-width" style="display: none;">

        <div class="content-row2">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">

                <tr>
                    <td class="border-td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                            class="content-table">
                            <tr>
                                <td class="border-td-sub">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="add-product">


                                        <tr class="altrow" runat="server" id="trBottom">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
