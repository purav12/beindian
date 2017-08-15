<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="TopicList.aspx.cs" Inherits="Webgape.Admin.Content.TopicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script language="javascript" type="text/javascript">
        function validation() {
            var a = document.getElementById('<%=txtSearch.ClientID %>').value;
            if (a == "") {
                $(document).ready(function () { jAlert('Please Enter Search Value.', 'Message'); });
                document.getElementById('<%=txtSearch.ClientID %>').focus();
                return false;
            }
            return true;
        }


    </script>
    <script type="text/javascript">
        function changeimage(flag, id) {
            alert(flag);
            alert(id);
            document.getElementById("ContentPlaceHolder1_hdntopicid").value = id;
            document.getElementById("ContentPlaceHolder1_hdnflag").value = flag;
            document.getElementById("ContentPlaceHolder1_btnhdnAppr").click();
        }
    </script>


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
                jAlert('Select at least one Topic!', 'Message');
                return false;
            }
            else {
                return ConfirmDelete();
            }
        }

        function ConfirmDelete() {
            jConfirm('Are you sure want to delete all selected Topic(s) ?', 'Confirmation', function (r) {
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
            <li class="active">Topic List</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Topic List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar">
                        <a href="/Admin/Content/Topic.aspx">
                            <span class="btn btn-small btn-primary"><i class="icon-plus-sign"></i>Add Topic </span></a>
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
                                    <div class="span5">
                                    </div>
                                    <div class="span7">
                                        <div class="row-fluid">
                                            <div class="pagination no-margin right" style="width: 100%">
                                                <span class="star" style="vertical-align: middle; text-align: center;">
                                                    <asp:Label ID="lblmsg" runat="server"></asp:Label></span>
                                                <table style="width: 100%" cellpadding="3" cellspacing="3">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td style="width: 30%" align="right">Search :
                                                        </td>                                                    
                                                        <td style="width: 10%">
                                                            <asp:TextBox ID="txtSearch" runat="server" Width="160px" CssClass="order-textfield" Style="margin-bottom: 0px;"></asp:TextBox>
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
                    <tr class="even-row">
                        <td>
                            <asp:GridView ID="grdTopic" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record(s) Found."
                                AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                ViewStateMode="Enabled" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover dataTable"
                                CellPadding="2" CellSpacing="1" GridLines="None" Width="100%" AllowPaging="True"
                                PageSize="<%$ appSettings:GridPageSize %>" PagerSettings-Mode="NumericFirstLast"
                                OnRowCommand="grdTopic_RowCommand" OnRowDataBound="grdTopic_RowDataBound">
                                <EmptyDataTemplate>
                                    <span style="color: Red; font-size: 12px; text-align: center;">
                                        <center>No Record(s) Found !</center>
                                    </span>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <strong>Select </strong>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" ItemStyle-HorizontalAlign="Center" Style="padding-left: 0px; width: 80px; float: left" CssClass="input-checkbox" ItemStyle-Width="50px" />
                                            <asp:HiddenField ID="hdnTopicID" runat="server" Value='<%#Eval("TopicID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="center" />
                                        <ItemStyle VerticalAlign="Top" CssClass="center" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Topic Name                                                               
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("TopicName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="Store Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <HeaderTemplate>
                                            Store Name
                                                                <asp:ImageButton ID="btnStoreName" runat="server" CommandArgument="DESC" CommandName="StoreName"
                                                                    AlternateText="Ascending Order" OnClick="Sorting" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigStoreName" runat="server" Text='<%# Bind("StoreName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Created On">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblConfigId" runat="server" Text='<%# Bind("CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="16%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display in Sitemap">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnSiteMap" runat="server" Value='<%# (DataBinder.Eval(Container.DataItem,"ShowOnSiteMap")) %>' />
                                            <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" CssClass="center" />
                                        <HeaderStyle CssClass="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>                                            
                                            <asp:LinkButton runat="server" ID="_editLinkButton1" ToolTip="Edit" CommandName="edit"
                                                CommandArgument='<%# Eval("TopicId") %>'><span><i class="icon-edit bigger-160 green"></i></span></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" CssClass="center" />
                                        <HeaderStyle CssClass="center" />
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
                <tr id="trBottom" runat="server">
                    <td style="width: 70%; float: left;">
                        <div class="span6">
                            <div class="row-fluid" style="width: 250px;">
                                <span><a id="lkbAllowAll" href="javascript:selectAll(true);" class="btn btn-mini btn-info mybtn">Check All</a> | <a id="lkbClearAll"
                                    href="javascript:selectAll(false);" class="btn btn-mini btn-info mybtn">Clear All</a> </span>
                            </div>
                        </div>
                    </td>
                    <td style="float: right;"><span style="float: right; padding-right: 10px;">
                        <asp:Button ID="btnDeleteTopic" runat="server" Text="Delete" CssClass="btn btn-mini btn-info btn-padding" OnClientClick="return checkCount();"
                            OnClick="btnDeleteTopic_Click" CommandName="DeleteMultiple" />
                        <div style="display: none">
                            <asp:Button ID="btnDeleteTemp" runat="server" ToolTip="Delete" OnClick="btnDeleteTopic_Click" />
                        </div>
                    </span></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
