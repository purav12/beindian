<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="EmailTemplateList.aspx.cs" Inherits="Webgape.Admin.Settings.EmailTemplateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
        function validation() {
            var a = document.getElementById('<%=txtSearch.ClientID %>').value;
            if (a == "") {
                jAlert('Please Enter Search Value.', 'Message');
                document.getElementById('<%=txtSearch.ClientID %>').focus();
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
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
                jAlert('Check atleast one Email Template!', 'Message');
                return false;
            }
            else {
                return confirm('Are you sure want to delete all selected Vendor(s)?', 'Message');
            }
        }

        function validation() {
            var a = document.getElementById('<%=txtSearch.ClientID %>').value;
            if (a == "") {
                jAlert('Please Enter Search Value.', 'Message', '<%=txtSearch.ClientID %>');
                return false;
            }
            return true;
        }
    </script>


    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Email Template List</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Email Template List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar">
                        <a href="/Admin/Settings/EmailTemplate.aspx">
                            <span class="btn btn-small btn-primary"><i class="icon-plus-sign"></i>Add Email Template </span></a>
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
                                                <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td style="width: 20%" align="right">Search :
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:DropDownList ID="ddlSearch" AutoPostBack="False" Width="100px" CssClass="options1 margin-top-5" runat="server">
                                                                <asp:ListItem>Label</asp:ListItem>
                                                                <asp:ListItem>Subject</asp:ListItem>
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
                            <asp:GridView ID="grdEmailTemplate" runat="server" AutoGenerateColumns="False" DataKeyNames="TemplateID" CssClass="table table-striped table-bordered table-hover dataTable"
                                EmptyDataText="No Record(s) Found." AllowSorting="True" EmptyDataRowStyle-ForeColor="Red"
                                EmptyDataRowStyle-HorizontalAlign="Center" ViewStateMode="Enabled" OnRowCommand="grdEmailTemplate_RowCommand"
                                BorderStyle="Solid" BorderColor="#E7E7E7" BorderWidth="1px" CellPadding="2" CellSpacing="1" OnPageIndexChanging="grdEmailTemplate_PageIndexChanging"
                                GridLines="None" Width="100%" AllowPaging="True" PagerSettings-Mode="NumericFirstLast" OnRowDataBound="grdEmailTemplate_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Label
                                        <asp:ImageButton ID="btnLabel" runat="server" CommandArgument="DESC" CommandName="Label"
                                            AlternateText="Ascending Order" OnClick="Sorting" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLabel" runat="server" Text='<%# Bind("Label") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Subject
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelSubject" runat="server" Text='<%# Bind("Subject") %>' Style="text-align: left;"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="_editLinkButton" ToolTip="Edit" CommandName="edit"
                                                CommandArgument='<%# Eval("TemplateID") %>'><i class="icon-edit bigger-160 green"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tdpro-checkbox" />
                                        <HeaderStyle CssClass="tdpro-checkbox" />
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
</asp:Content>
