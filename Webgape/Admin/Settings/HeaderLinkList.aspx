<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="HeaderLinkList.aspx.cs" Inherits="Webgape.Admin.Settings.HeaderLinkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script language="javascript" type="text/javascript">
        function validation() {
            var a = document.getElementById('<%=txtSearch.ClientID %>').value;
            if (a == "") {
                $(document).ready(function () { jAlert('Please Enter Search Value.', 'Messages', '<%=txtSearch.ClientID %>'); });
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
                jAlert('Select at least One Header Link !', 'Message');
                return false;
            }
            else {
                return checkaa();
            }
            return false;
        }
        function checkaa() {
            jConfirm('Are you sure want to delete all selected Header Link ?', 'Confirmation', function (r) {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Header Links </li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Header Links
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar">
                        <a href="/Admin/Settings/HeaderLink.aspx">
                            <span class="btn btn-small btn-primary"><i class="icon-plus-sign"></i>Add Header Link Configuration </span></a>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header">
                            <h4>Search </h4>
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
                                                    TypeName="Solution.Bussines.Components.HeaderlinkComponent" SelectMethod="GetDataByFilter"
                                                    StartRowIndexParameterName="startIndex" MaximumRowsParameterName="pageSize" SortParameterName="sortBy"
                                                    SelectCountMethod="GetCount">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hdnheaderlink" DbType="String" DefaultValue="PageId"
                                                            Name="CName" />
                                                        <asp:ControlParameter ControlID="txtSearch" DbType="String" Name="pSearchValue" DefaultValue="" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:HiddenField ID="hdnheaderlink" runat="server" />
                                                <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td style="width: 20%" align="right">Search :
                                                        </td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%">

                                                            <asp:TextBox ID="txtSearch" runat="server" Width="160px" CssClass="order-textfield" Style="margin-bottom: 0px;"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 5%">
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-mini btn-info btn-padding" Text="Search"
                                                                OnClientClick="return validation();" />
                                                        </td>
                                                        <td style="width: 5%; margin-right: 10px; padding-right: 0px;">
                                                            <asp:Button ID="btnshowall" runat="server" OnClick="btnshowall_Click" CssClass="btn btn-mini btn-info btn-padding margin-right" Text="Show all" />
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
                            <asp:GridView ID="grdheaderlink" runat="server" AutoGenerateColumns="False" DataKeyNames="PageId" CssClass="table table-striped table-bordered table-hover dataTable"
                                CellSpacing="1" BorderStyle="Solid" BorderWidth="1" BorderColor="#E7E7E7" EmptyDataText="No Record(s) Found."
                                AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                ViewStateMode="Enabled" Width="100%" GridLines="None" AllowPaging="True" PageSize="<%$ appSettings:GridPageSize %>"
                                PagerSettings-Mode="NumericFirstLast" CellPadding="2" DataSourceID="_gridObjectDataSource"
                                OnRowDataBound="grdheaderlink_RowDataBound" OnRowCommand="grdheaderlink_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="10%">
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkselect" runat="server" CssClass="input-checkbox" />
                                            <asp:HiddenField ID="hdnheaderlinkid" runat="server" Value='<%#Eval("PageId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Header Name" ItemStyle-Width="20%">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbheadername" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Header Link" ItemStyle-Width="20%">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbheaderlink" runat="server" Text='<%# Bind("PageURL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Order" ItemStyle-Width="15%">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbdisplayorder" runat="server" Text='<%# Bind("DisplayOrder") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name" ItemStyle-Width="25%">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbstorename" runat="server" Text='<%# Bind("StoreName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%">
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemTemplate>

                                            <asp:LinkButton runat="server" ID="lnkedit" ToolTip="Edit" CommandName="edit" CommandArgument='<%# Eval("PageId") %>'>
                                                <span><i class="icon-edit bigger-160 green"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tdpro-checkbox" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom"></PagerSettings>
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
                <tr id="trBottom" runat="server">
                    <td style="width: 70%; float: left;">
                        <div class="span6">
                            <div class="row-fluid" style="width: 250px;">

                                <span><a id="lkbAllowAll" href="javascript:selectAll(true);" class="btn btn-mini btn-info">Check All</a> | <a id="lkbClearAll"
                                    href="javascript:selectAll(false);" class="btn btn-mini btn-info">Clear All</a> </span><span style="float: right; padding-right: 0px;"></span>
                            </div>
                        </div>
                    </td>
                    <td style="float: right">


                        <asp:Button ID="btndelete" runat="server" ToolTip="Delete" Text="Delete" OnClientClick='return checkCount()' CssClass="btn btn-mini btn-info btn-padding"
                            OnClick="btndelete_Click" />
                        <div style="display: none">
                            <asp:Button ID="btnDeleteTemp" runat="server" ToolTip="Delete" OnClick="btndelete1_Click" />
                        </div>
                    </td>

                </tr>
            </table>
        </div>
    </div>

</asp:Content>
