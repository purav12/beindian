<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="Webgape.Admin.Posts.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sc1" runat="server">
    </asp:ScriptManager>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Category List</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Category List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar" style="float: right;">
                        <a href="/Admin/Posts/Category.aspx">
                            <span class="btn btn-small btn-primary"><i class="icon-plus-sign"></i>Add Category </span></a>
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
                                            <div class="pagination no-margin right" style="width: 750px;">
                                                Status :
                                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="120px" AutoPostBack="True"
                                                                CssClass="order-list margin-bottom-none" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                <asp:ListItem>Active</asp:ListItem>
                                                                <asp:ListItem>InActive</asp:ListItem>
                                                            </asp:DropDownList>
                                                Search :
                                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="order-list margin-bottom-none" Style="width: 150px;">
                                                                <asp:ListItem Value="Name">Category Name</asp:ListItem>
                                                                <asp:ListItem Value="ParentCatName">Parent Category Name</asp:ListItem>
                                                            </asp:DropDownList>

                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="order-textfield margin-bottom-none" Width="160px"></asp:TextBox>

                                                <asp:LinkButton ID="ibtnsearch" runat="server" Style="padding-right: 5px;" CssClass="btn btn-mini btn-info" OnClick="btnGo_Click"
                                                    OnClientClick="return validation();">Search</asp:LinkButton>
                                                <asp:LinkButton ID="btnShowAll" runat="server" OnClick="btnShowAll_Click" CssClass="btn btn-mini btn-info margin-right">Show All</asp:LinkButton><br />
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div style="float: right; margin-top: 5px; margin-right: 103px">
                                                <asp:Button ID="btnExport" class="btn btn-mini btn-info btn-padding" Visible="false" Text="Export" runat="server" ToolTip="Export" OnClick="btnExport_Click" />
                                                <br />
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
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdnCategory" runat="server" />
                                    <asp:GridView ID="gvCategory" AutoGenerateColumns="false" runat="server" GridLines="None"
                                        Width="100%" CellPadding="2" CellSpacing="1" CssClass="table table-striped table-bordered table-hover dataTable"
                                        RowStyle-ForeColor="Black" HeaderStyle-ForeColor="#3c2b1b" PagerSettings-Mode="NumericFirstLast"
                                        AllowPaging="True" AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                        OnRowDataBound="gvCategory_RowDataBound"
                                        OnPageIndexChanging="gvCategory_PageIndexChanging" PageSize="20">
                                        <EmptyDataTemplate>
                                            <span style="color: Red; font-size: 12px; text-align: center;">No Record(s) Found !</span>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="input-checkbox" />
                                                    <asp:HiddenField ID="hdnCatid" runat="server" Value='<%#Eval("CategoryID") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" Width="7%" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="1" border="0" align="left">
                                                        <tr style="border: 0px;">
                                                            <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                <strong>Name</strong>
                                                                <asp:ImageButton ID="lbName" runat="server" CommandArgument="DESC" CommandName="Name"
                                                                    AlternateText="Ascending Order" OnClick="Sorting" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="text-align: justify; width: 300px;">
                                                        <a href='<%# "Category.aspx?Mode=edit&ID="+ DataBinder.Eval(Container.DataItem, "CategoryID") %>'>
                                                            <%#Eval("Name").ToString().Length>50?Eval("Name").ToString().Substring(0,50):Eval("Name")%>
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parent Category Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParent" runat="server" Text='<%#Eval("ParentName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    Category :
                                                                        <asp:Label ID="lblCategoryCount" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                                    | Posts :
                                                                        <asp:Label ID="lblPostCount" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                                    <headerstyle horizontalalign="Center" />
                                                    <itemstyle horizontalalign="Left" width="25%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hplAddPost" runat="server" Text="Add Post" ToolTip="AddPost"
                                                        NavigateUrl='<%# "Post.aspx?Mode=add&ID="+ DataBinder.Eval(Container.DataItem, "CategoryID") %>'></asp:HyperLink>
                                                    <headerstyle horizontalalign="Left" />
                                                    <itemstyle horizontalalign="Left" width="25%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display&nbsp;Order" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="left">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="1" border="0" align="center">
                                                        <tr style="border: 0px;">
                                                            <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                <strong>Display&nbsp;Order</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("DisplayOrder").ToString().Equals("999")?null:Eval("DisplayOrder")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Updated By" ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%"
                                                HeaderStyle-HorizontalAlign="left">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="1" border="0" align="left">
                                                        <tr style="border: 0px;">
                                                            <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                <strong>Updated By</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("UpdatedBy")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Updated On" ItemStyle-HorizontalAlign="left" ItemStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="left">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="1" border="0" align="left">
                                                        <tr style="border: 0px;">
                                                            <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                <strong>Updated On</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("UpdatedOn")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnActive" runat="server" Value='<%# (DataBinder.Eval(Container.DataItem,"Active")) %>' />
                                                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="29px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a title='' id="hplEdit" runat="server" href='<%# "Category.aspx?Mode=edit&ID="+ DataBinder.Eval(Container.DataItem, "CategoryID")   %>'><span class="green"><i class="icon-edit bigger-160"></i></span></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="29px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="40"></PagerSettings>
                                        <PagerStyle HorizontalAlign="right" CssClass="numbering" />
                                        <RowStyle HorizontalAlign="left" CssClass="eve-row" ForeColor="#000000" />
                                        <AlternatingRowStyle CssClass="odd-row" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                        <HeaderStyle ForeColor="White" Font-Bold="false" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gvCategory" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                            <div class="row-fluid" style="width: 900px;">

                                <span><a id="lkbAllowAll" href="javascript:selectAll(true);" class="btn btn-mini btn-info">Check All</a> | <a id="lkbClearAll"
                                    href="javascript:selectAll(false);" class="btn btn-mini btn-info">Clear All</a> </span><span style="float: right;"></span>

                            </div>
                        </div>
                    </td>
                    <td style="float: right">
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-mini btn-info"
                            OnClientClick="return checkCount();" OnClick="btnDelete_Click" ToolTip="Delete">Delete</asp:LinkButton>
                        <div style="display: none;">
                            <asp:Button ID="btnDeleteTemp" runat="server" OnClick="btnDelete1_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
                jAlert('Check at least One Record!', 'Message');
                return false;
            }
            else {
                return checkaa();
            }
            return true;
        }
        function checkaa() {
            jConfirm('Are you sure want to delete all selected Category ?', 'Confirmation', function (r) {
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
