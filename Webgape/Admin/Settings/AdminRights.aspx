<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/Admin.Master" AutoEventWireup="true"
    CodeBehind="AdminRights.aspx.cs" Inherits="Webgape.ADMIN.Settings.AdminRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
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
                $(document).ready(function () { jAlert('Please select at least one Right...', 'Message', ''); });
                return false;
            }
        }

    </script>
    <style type="text/css">
        #ContentPlaceHolder1_chklrights input {
        }
        .chkboxlist tr td input {
            opacity: inherit !important;
            margin: 0px;
        }

        .chkboxlist tr td label {
            padding-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Admin Rights</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Admin Rights
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Admin Rights</h5>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header">
                            <h4>Search By:
                            </h4>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div class="span8">
                                    </div>
                                    <div class="span4">
                                        <table style="width: 100%" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td>Users :   </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAdmins" runat="server" Width="300px" Style="border-color: #BCC0C1; border-width: 1px; border-style: Solid;"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlAdmins_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
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
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border: 1px solid #DADADA; border-collapse: collapse;">
                    <tr style="height: 27px;">
                        <td colspan="2" align="center" style="border: 1px solid #DADADA; background: #e7e7e7; font-weight: bold;">Tab Rights
                        </td>
                        <td colspan="2" align="center" style="border: 1px solid #DADADA; background: #e7e7e7; font-weight: bold;">Page Rights
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: top; padding: 10px; width: 7%;"></td>
                        <td align="left" style="padding: 10px; width: 20%; vertical-align: top; border-right: solid 1px #DADADA;" class="chkboxlist">
                            <asp:CheckBoxList ID="chklrights" runat="server">
                            </asp:CheckBoxList>
                            <br />
                            <asp:Button ID="btnUpdateRights" runat="server" Text="Update Admin Rights" CssClass="btn btn-mini btn-info btn-padding "
                                ToolTip="Update Admin Rights" OnClientClick="return checkCount();" OnClick="btnUpdateRights_Click" />
                        </td>
                        <td align="left" style="vertical-align: top; padding: 10px; width: 7%;"></td>
                        <td align="left" style="vertical-align: top; padding: 10px">
                            <asp:GridView ID="gvAdminPageRights" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found." CssClass="table table-striped table-bordered table-hover dataTable"
                                AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                ViewStateMode="Enabled" Width="70%" BackColor="White" GridLines="None" CellPadding="4"
                                CellSpacing="1" Style="background: none; border: 1px solid #DADADA;" OnPageIndexChanging="gvAdminPageRights_PageIndexChanging"
                                AllowPaging="false" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompareAdminID" runat="server" Text='<%#Eval("AdminTypeID") %>'></asp:Label>
                                            <asp:Label ID="lblInnerRightsID" runat="server" Text='<%#Eval("InnerRightsID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tab Name" ItemStyle-Width="20%">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            &nbsp;<asp:Label runat="server" ID="lblTabname" Text='<%#Eval("TabName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section Name" ItemStyle-Width="60%">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            &nbsp;<asp:Label runat="server" ID="lblsectionname" Text='<%#Eval("PageName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Listed" ItemStyle-Width="10%">
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsListed" runat="server" Checked='<%# Convert.ToBoolean(Eval("isListed"))%>' CssClass="input-checkbox" Style="margin-right: 10px;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Modify" ItemStyle-Width="20%">
                                        <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsModify" runat="server" Checked='<%# Convert.ToBoolean(Eval("isModify"))%>' CssClass="input-checkbox" Style="margin-right: 15px;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="altrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                <HeaderStyle ForeColor="White" Font-Bold="false" />
                                <PagerSettings Mode="NumericFirstLast" Position="Bottom"></PagerSettings>
                                <PagerStyle HorizontalAlign="Right" CssClass="grid_paging" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left" style="border-right: solid 1px #DADADA;"></td>
                        <td></td>
                        <td align="left" style="padding-left: 15px;">
                            <div style="text-align: center;" class="divfloatingcss" id="divfloating">

                                <div style="margin-bottom: 1px; margin-top: 3px;">

                                    <asp:Button ID="btnUpdatePageRight" runat="server" Text="Update" ToolTip="Update" CssClass="btn btn-mini btn-info btn-padding "
                                        OnClientClick="return checkCount();" OnClick="btnUpdatePageRight_Click" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 15px;">
                        <td colspan="4"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row-fluid">
        </div>
    </div>
</asp:Content>
