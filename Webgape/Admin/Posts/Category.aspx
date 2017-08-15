<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="Category.aspx.cs" Inherits="Webgape.Admin.Posts.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script src="../../ckeditor/_samples/sample.js" type="text/javascript"></script>
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <style type="text/css">
        .tab-content {
            padding-top: 0px;
        }

        .widget-body tab-content {
            padding-top: 0px;
        }

        .row-fluid .form-horizontal .control-group {
            margin-bottom: 10px;
        }
    </style>
    <script type="text/javascript">
        function load() {
            var strseacrh = document.getElementById('<%=txtSearch.ClientID%>').value;
            if (strseacrh != '') {
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundImage = 'url(/admin/images/loading.gif)';
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundRepeat = 'no-repeat';
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundPosition = 'right';
            }
            else {
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundImage = 'none';
            }
        }
        function hide() {
            document.getElementById('<%=txtSearch.ClientID%>').style.backgroundImage = 'none';
        }

        function Checktxt() {
            var strseacrh = document.getElementById('<%=txtSearch.ClientID%>').value;
            if (strseacrh == '') {
                document.getElementById('<%=txtSearch.ClientID%>').style.backgroundImage = 'none';
            }
        }
    </script>
    <style type="text/css">
        .autocomplete_completionListElement {
            margin: 0px !important; /*background-color: #507CD1;*/
            background-color: #fff;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: double;
            cursor: 'default';
            overflow: auto; /* height: 200px;*/
            text-align: left;
            list-style-type: none;
            font-size: 11px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }

        .autocomplete_highlightedListItem {
            background-color: #ffff99;
            color: black;
            padding: 1px;
            font-size: 11px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }

        .autocomplete_listItem {
            font-size: 11px;
            font-family: Verdana, Arial, Helvetica, sans-serif; /*background-color: #507CD1;*/
            background-color: #fff;
            color: windowtext;
            padding: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <style type="text/css">
        .MasterTreeView {
            font-family: Arial,Helvetica,sans-serif;
            font-size: 12px;
            color: #727272;
        }

            .MasterTreeView a {
                text-decoration: none;
                color: #9b2414;
                word-wrap: break-word;
            }

                .MasterTreeView a:hover {
                    text-decoration: underline;
                    color: #9b2414;
                }

                .MasterTreeView a.active {
                    text-decoration: underline;
                    color: #000;
                }

        .MasterTreeViewnew td {
            padding: 0 2px;
        }

        #ContentPlaceHolder1_tvMasterCategoryList_0.active {
            color: #000;
        }
    </style>
    <style type="text/css">
        .slidingDiv {
            height: 300px;
            padding: 20px;
            margin-top: 10px;
        }

        .show_hide {
            display: block;
        }
    </style>
    <script type="text/javascript">



        function chkHeight() {
            var windowHeight = 0;
            windowHeight = $(document).height(); //window.innerHeight;

            document.getElementById('prepage').style.height = windowHeight + 'px';
            document.getElementById('prepage').style.display = '';
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
    <script type="text/javascript">
        function ChkRootNode() {
            if (document.getElementById('ContentPlaceHolder1_tvCategoryn0CheckBox') && document.getElementById('ContentPlaceHolder1_tvCategoryn0CheckBox').checked == true) {
                document.getElementById('ContentPlaceHolder1_trShowonHeader').style.display = '';
            }
            else {
                document.getElementById('ContentPlaceHolder1_trShowonHeader').style.display = 'none';
            }
        }
    </script>
    <script type="text/javascript">
        function ShowDiv(imgid, divid) {
            if (document.getElementById(imgid) != null) {
                var src = document.getElementById(imgid).src;
                if (src.indexOf('expand.gif') > -1) {
                    document.getElementById(imgid).src = src.replace('expand.gif', 'minimize.png');
                    document.getElementById(imgid).title = 'Minimize';
                    document.getElementById(imgid).alt = 'Minimize';
                    document.getElementById(imgid).style.marginTop = "4px";
                    document.getElementById(imgid).className = 'minimize';
                    if (document.getElementById(divid)) {
                        document.getElementById(divid).style.display = '';
                    }

                }
                else if (src.indexOf('minimize.png') > -1) {
                    document.getElementById(imgid).src = src.replace('minimize.png', 'expand.gif');
                    document.getElementById(imgid).title = 'Show';
                    document.getElementById(imgid).style.marginTop = "0px";
                    document.getElementById(imgid).alt = 'Show';
                    document.getElementById(imgid).className = 'close';
                    if (document.getElementById(divid)) {
                        document.getElementById(divid).style.display = 'none';
                    }

                }
            }
        }


        function Tabdisplay(id) {
            document.getElementById('ContentPlaceHolder1_hdnTabid').value = id;
            for (var i = 1; i <= 5; i++) {

                var divid = "divtab" + i.toString()
                var liid = "ContentPlaceHolder1_li" + i.toString()
                if (document.getElementById(divid) != null && ('divtab' + id == divid)) {
                    document.getElementById(divid).style.display = '';
                }
                else {
                    if (document.getElementById(divid) != null) {
                        document.getElementById(divid).style.display = 'none';
                    }
                }
                if (document.getElementById(liid) && ('ContentPlaceHolder1_li' + id == liid)) {
                    document.getElementById(liid).className = 'active';
                }
                else {
                    if (document.getElementById(liid) != null) {
                        document.getElementById(liid).className = '';
                    }
                }
            }

        }

        function iframeAutoheight(iframe) {
            var height = iframe.contentWindow.document.body.scrollHeight;
            iframe.height = height + 5;
        }
        function iframereload(iframe) {
            document.getElementById(iframe).src = document.getElementById(iframe).src;

        }

    </script>
    <script type="text/javascript">
        function ChkRootNode() {

            var allElts = document.getElementById('ContentPlaceHolder1_tvCategory').getElementsByTagName('input');

            for (i = 0; i < allElts.length; i++) {
                var elt = allElts[i];

                if (elt.type == "checkbox") {
                    if (document.getElementById('ContentPlaceHolder1_tvCategoryn0CheckBox').checked == true && elt.id != 'ContentPlaceHolder1_tvCategoryn0CheckBox') {
                        elt.checked = false;
                        elt.disabled = true;
                    }
                    else {
                        elt.disabled = false;
                    }
                }
            }
        }

    </script>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/admin/dashboard.aspx" title="Home">Home</a> <span class="divider"><i
                class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Add Category</li>
        </ul>
        <div class="nav-search" id="nav-search">
            <div class="form-search" />
            <span class="input-icon"></span>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>
                <asp:Label runat="server" Text="Add Category" ID="lblTitle"></asp:Label></h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="widget-box transparent">
                <div class="widget-body">
                    <div class="widget-main padding-10">
                        <div class="tabbable">
                            <div class="tab-content no-padding">
                                <div class="tab-pane active" id="tab1">
                                    <div class="tab-content">
                                        <div class="tab-content" id="tablePost" runat="server">
                                            <div class="row-fluid">
                                                <div class="span3" style="float: left">
                                                    <div class="widget-box transparent" style="border: 1px solid #CCCCCC; width: 98%;">
                                                        <div class="widget-header widget-header-flat-1">
                                                            <h4 class="lighter">
                                                                <i class="icon-code orange"></i>Category</h4>
                                                            <div class="widget-toolbar">
                                                                <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body tab-content" style="width: 98%">
                                                            <table cellpadding="0" cellspacing="0" width="98%" class="no-border-left">
                                                                <tr>
                                                                    <td align="left" style="padding-left: 5px" id="tdAddcategory" runat="server">
                                                                        <a href="/Admin/Posts/Category.aspx" onclick="chkHeight();" class="btn btn-mini btn-info" title="Add Category">Add Category
                                                                        </a>
                                                                    </td>

                                                                    <td align="left" style="padding-left: 5px;"
                                                                        id="tdrootcategory" runat="server">
                                                                        <a href="/Admin/Posts/Category.aspx?Root=1" onclick="chkHeight();" class="btn btn-mini btn-info" title="Add Root Category">Add Root Category
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label id="lblsea" runat="server" title="Search By :">Search By :</label>
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="span4" Style="width: 163px; margin-top: 5px;"
                                                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                            <asp:ListItem Value="Name">Category Name</asp:ListItem>
                                                                            <asp:ListItem Value="ParentCatName">Parent Category Name</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" colspan="2">
                                                                        <label id="lblsea2" runat="server" title="Search :">Search :</label>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                                                            CssClass="order-textfield margin-bottom-none" Width="110px" Style="margin-left: 5px;" onkeyup="Checktxt();"></asp:TextBox>
                                                                        <asp:LinkButton ID="ibtngo" OnClientClick="chkHeight();" runat="server" OnClick="ibtngo_Click" CssClass="btn btn-mini btn-info" Text="Go">Go</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:RadioButtonList ID="rdlCategoryStatus" AutoPostBack="true" onchange="chkHeight();"
                                                                            CssClass="input-radio12" Style="border: none; float: left; margin-left: 75px; width: 200px;" RepeatDirection="Horizontal"
                                                                            runat="server" OnSelectedIndexChanged="rdlCategoryStatus_SelectedIndexChanged">
                                                                            <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                            <asp:ListItem Value="InActive">In Active</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trCollexpand" runat="server">
                                                                    <td align="center" colspan="2" style="border-bottom: 1px solid #e7e7e7;">
                                                                        <asp:LinkButton ID="lnkCollapseAll" OnClientClick="chkHeight();" runat="server" OnClick="lnkCollapseAll_Click">
                                                                            <img src="/admin/images/collapsed.png" alt="" />&nbsp;<label id="Label1" runat="server" title="Collapse All">Collapse All</label>
                                                                        </asp:LinkButton>
                                                                        &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnkExpandAll" OnClientClick="chkHeight();" runat="server" OnClick="lnkExpandAll_Click">
                                                                <img src="/admin/images/expanded.png" alt="" />&nbsp;<label id="Label2" runat="server" title="Expand All">Expand All</label>
                                                            </asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:TreeView ID="tvMasterCategoryList" ShowLines="true" CssClass="MasterTreeViewnew" Style="font-size: 14px; line-height: 25px;"
                                                                            runat="server">
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="span9" style="float: right">
                                                    <div class="widget-box transparent" style="border: 1px solid #CCCCCC;">
                                                        <div class="widget-header widget-header-flat-1">
                                                            <h4 class="lighter">General
                                                            </h4>
                                                            <div class="widget-toolbar">
                                                                <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body tab-content">
                                                            <div class="form-horizontal">
                                                                <div class="control-group">

                                                                    <div class="controls">
                                                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">*</span> Name</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtname" CssClass="span4" MaxLength="500" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">*</span> Short Title :</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox CssClass="span4" ID="txtShortTitle" runat="server" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">*</span> Parent :</label>
                                                                    <div class="controls">
                                                                        <asp:TreeView ID="tvCategory" runat="server" CssClass="input-checkbox-1" Style="float: left; font-size: 13px; line-height: 25px;">
                                                                        </asp:TreeView>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span>Display Order :</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox CssClass="span4" onkeypress="return isNumberKey(event)" MaxLength="5"
                                                                            ID="txtdisplayorder" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span> Icon :</label>
                                                                    <div class="controls">
                                                                        <table>
                                                                            <tr>
                                                                                <td valign="bottom">
                                                                                    <img alt="" id="imgIcon" width="241" height="142" runat="server" />
                                                                                </td>
                                                                                <td valign="bottom">
                                                                                    <asp:FileUpload ID="fuIcon" runat="server" />
                                                                                </td>
                                                                                <td valign="bottom">
                                                                                    <asp:LinkButton ID="ibtnUpload" runat="server" CssClass="btn btn-mini btn-info"
                                                                                        OnClick="ibtnUpload_Click" Text="Upload" OnClientClick="chkHeight();">
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span>Header Title :</label>
                                                                    <div class="controls">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td class="ckeditor-table">
                                                                                    <asp:TextBox TextMode="multiLine" class="span4" ID="txtDescription"
                                                                                        Rows="10" Columns="80" runat="server" MaxLength="500" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;">
                                                                                    </asp:TextBox>
                                                                                    <script type="text/javascript">
                                                                                        CKEDITOR.replace('<%= txtDescription.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 150, width: 600 });
                                                                                        CKEDITOR.on('dialogDefinition', function (ev) {
                                                                                            if (ev.data.name == 'image') {
                                                                                                var btn = ev.data.definition.getContents('info').get('browse');
                                                                                                btn.hidden = false;
                                                                                                btn.onClick = function () { window.open(CKEDITOR.basePath + 'ImageBrowser.aspx', 'popuppage', 'scrollbars=no,width=780,height=630,left=' + ((screen.width - 780) / 2) + ',top=' + ((screen.height - 630) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };
                                                                                            }
                                                                                            if (ev.data.name == 'link') {
                                                                                                var btn = ev.data.definition.getContents('info').get('browse');
                                                                                                btn.hidden = false;
                                                                                                btn.onClick = function () { window.open(CKEDITOR.basePath + 'LinkBrowser.aspx', 'popuppage', 'scrollbars=no,width=520,height=580,left=' + ((screen.width - 520) / 2) + ',top=' + ((screen.height - 580) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };
                                                                                            }
                                                                                        });
                                                                                    </script>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span> Header Text :</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox CssClass="span4" ID="txtHeaderText" MaxLength="500" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span>Footer Text :</label>
                                                                    <div class="controls">
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td class="ckeditor-table">
                                                                                    <asp:TextBox TextMode="multiLine" class="description-textarea" ID="txtSummary" Rows="10"
                                                                                        Columns="80" runat="server" MaxLength="500" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;">
                                                                                    </asp:TextBox>
                                                                                    <script type="text/javascript">
                                                                                        CKEDITOR.replace('<%= txtSummary.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 150, width: 600 });
                                                                                        CKEDITOR.on('dialogDefinition', function (ev) {
                                                                                            if (ev.data.name == 'image') {
                                                                                                var btn = ev.data.definition.getContents('info').get('browse');
                                                                                                btn.hidden = false;
                                                                                                btn.onClick = function () { window.open(CKEDITOR.basePath + 'ImageBrowser.aspx', 'popuppage', 'scrollbars=no,width=780,height=630,left=' + ((screen.width - 780) / 2) + ',top=' + ((screen.height - 630) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };
                                                                                            }
                                                                                            if (ev.data.name == 'link') {
                                                                                                var btn = ev.data.definition.getContents('info').get('browse');
                                                                                                btn.hidden = false;
                                                                                                btn.onClick = function () { window.open(CKEDITOR.basePath + 'LinkBrowser.aspx', 'popuppage', 'scrollbars=no,width=520,height=580,left=' + ((screen.width - 520) / 2) + ',top=' + ((screen.height - 580) / 2) + ',resizable=no,toolbar=no,titlebar=no'); };
                                                                                            }
                                                                                        });
                                                                                    </script>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span>Status :</label>
                                                                    <div class="controls">
                                                                        <asp:RadioButtonList ID="rblPublished" runat="server" CssClass="input-radio" Style="float: left; margin-top: -5px;" RepeatColumns="2" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True" Selected="True">Yes&nbsp;&nbsp;</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group">
                                                                    <label class="control-label" for="form-field-1">
                                                                        <span class="star-red">&nbsp;</span>Feature Category :</label>
                                                                    <div class="controls">
                                                                        &nbsp;<asp:RadioButton ID="rblIsfeatured" runat="server" CssClass="input-radio" Style="float: left; margin-top: 6px;" Text="Yes" EnableViewState="true"
                                                                            GroupName="rdoFeature" />&nbsp; &nbsp;
                                                                        <asp:RadioButton ID="rblIsfeaturedNo" runat="server" CssClass="input-radio" Style="float: left; margin-top: 6px; margin-left: 10px;" Text="No" EnableViewState="true"
                                                                            GroupName="rdoFeature" />
                                                                    </div>
                                                                </div>
                                                                <div class="control-group" id="catid" runat="server" visible="false">
                                                                    <asp:Literal ID="ltrcatid" runat="server"></asp:Literal>
                                                                    <div class="controls">
                                                                        <asp:TextBox CssClass="span4" onkeypress="return isNumberKey(event)" MaxLength="5"
                                                                            ID="txtcatidbystore" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="control-group" style="display: none;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <label class="control-label" for="form-field-1">
                                                                                    <span class="star-red">&nbsp;</span>&nbsp;&nbsp;&nbsp;Show&nbsp;on&nbsp;Item&nbsp;Header&nbsp;:</label>
                                                                                <div class="controls">
                                                                                    <asp:CheckBox ID="chkShowOnHeader" runat="server" CssClass="input-checkbox" Style="float: left;" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="span9" style="float: right;">
                                                    <div class="widget-box transparent" style="border: 1px solid #CCCCCC;">
                                                        <div class="widget-header widget-header-flat-1">
                                                            <h4 class="lighter">
                                                                <i class="icon-code orange"></i>SEO</h4>
                                                            <div class="widget-toolbar">
                                                                <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body tab-content">
                                                            <div class="widget-toolbar no-border">
                                                                <ul id="recent-tab" class="nav nav-tabs">
                                                                    <li class="active"><a href="#tab21" data-toggle="tab" id="ordernotes1" runat="server" onclick='jQuery("#ordernotes1").addClass("active");jQuery("#privatenotes1").removeClass("active");jQuery("#giftnotes1").removeClass("active");jQuery("#myaccount1").removeClass("active");jQuery("div.order-notes").fadeIn();jQuery("div.private-notes").css("display", "none");jQuery("div.gift-notes").css("display", "none");jQuery("div.my-account").css("display", "none");'>Page Title</a></li>
                                                                    <li><a href="#tab22" data-toggle="tab" id="privatenotes1" onclick='jQuery("#ordernotes1").removeClass("active");jQuery("#privatenotes1").addClass("active"); jQuery("#giftnotes1").removeClass("active");jQuery("#myaccount1").removeClass("active");jQuery("div.private-notes").fadeIn();jQuery("div.order-notes").css("display", "none");jQuery("div.gift-notes").css("display", "none");jQuery("div.my-account").css("display", "none");'>Keywords</a> </li>
                                                                    <li><a href="#tab23" data-toggle="tab" id="giftnotes1" onclick='jQuery("#giftnotes1").addClass("active");jQuery("#privatenotes1").removeClass("active");jQuery("#ordernotes1").removeClass("active");jQuery("#myaccount1").removeClass("active");jQuery("div.gift-notes").fadeIn();jQuery("div.private-notes").css("display", "none");jQuery("div.order-notes").css("display", "none");jQuery("div.my-account").css("display", "none");'>Description</a> </li>
                                                                </ul>
                                                            </div>
                                                            <div class="widget-main no-padding">
                                                                <div class="tab-content no-padding overflow-visible" style="margin-top: -5px;">
                                                                    <div class="tab-pane active" id="tab21">
                                                                        <div class="item-list" id="tasks">
                                                                            <div class="widget-body">
                                                                                <asp:TextBox ID="txtsetitle" runat="server" CssClass="width-95 margin-top" Width="100%"
                                                                                    Columns="6" MaxLength="500" Rows="6" TextMode="MultiLine">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="tab-pane" id="tab22">
                                                                        <div class="item-list" id="Div1">
                                                                            <div class="widget-body">
                                                                                <asp:TextBox ID="txtsekeyword" runat="server" CssClass="width-95 margin-top" Width="100%"
                                                                                    Columns="6" Rows="6" TextMode="MultiLine">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="tab-pane" id="tab23">
                                                                        <div class="item-list" id="Div2">
                                                                            <div class="widget-body">
                                                                                <asp:TextBox ID="txtseodescription" runat="server" CssClass="width-95 margin-top" Width="100%"
                                                                                    Columns="6" Rows="6" TextMode="MultiLine">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                            </div>
                                        </div>
                                        <div class="row-fluid">

                                            <div class="form-actions center">

                                                <div style="margin-bottom: 5px; line-height: 9px;">
                                                    <br />
                                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">
                                                        <div style="margin-bottom: 1px; margin-top: 3px;">
                                                            <asp:LinkButton ID="imgSave" runat="server" AlternateText="Save" ToolTip="Save" OnClick="imgbtnSave_Click" OnClientClick="return validation();" CssClass="btn btn-small btn-success" Text="Save"></asp:LinkButton>
                                                            &nbsp;&nbsp;
                                                            <asp:LinkButton ID="imgCancle" runat="server" AlternateText="Cancel" ToolTip="Cancel"
                                                                OnClick="imgCancle_Click" class="btn btn-small btn-success"
                                                                TabIndex="38">Cancel</asp:LinkButton>

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
                </div>
            </div>
        </div>
    </div>

    <div id="prepage" style="position: absolute; font-family: arial; font-size: 16; left: 0px; top: 0px; background-color: #000; opacity: 0.7; filter: alpha(opacity=70); layer-background-color: white; height: 100%; width: 100%; z-index: 1000; display: none;">
        <table width="100%" style="position: fixed; top: 40%; left: 50%; margin: -50px 0 0 -100px;">
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


    <script language="javascript" type="text/javascript">
        function validation() {



            var b = document.getElementById('<%=txtname.ClientID %>').value;
            if (b == "") {
                alert('Enter Category Name!');
                document.getElementById('<%=txtname.ClientID %>').focus();
                return false;
            }

            var b = document.getElementById('<%=txtShortTitle.ClientID %>').value;
            if (b == "") {
                alert('Enter Category Title!');
                document.getElementById('<%=txtShortTitle.ClientID %>').focus();
                return false;
            }


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
                jAlert('Check Atleast One Category!', 'Message');
                return false;
            }

            return true;
        }

    </script>
    <script language="javascript" type="text/javascript">
        function vsble() {
            if (document.getElementById('frmPosts').style.display == '')
                document.getElementById('frmPosts').style.display = '';
            else
                document.getElementById('frmPosts').style.display = '';
        }
    </script>
    <script language="javascript" type="text/javascript">
        function vsbleDisplayOrder() {
            if (document.getElementById('frmDisplayOrder').style.display == '')
                document.getElementById('frmDisplayOrder').style.display = '';
            else
                document.getElementById('frmDisplayOrder').style.display = '';
        }
    </script>
    <script language="javascript" type="text/javascript">
        function vsblefrminventory() {
            if (document.getElementById('frminventory').style.display == '')
                document.getElementById('frminventory').style.display = '';
            else
                document.getElementById('frminventory').style.display = '';
        }
    </script>
    <script language="javascript" type="text/javascript">
        function vsblefrmprice() {
            if (document.getElementById('frmprice').style.display == '')
                document.getElementById('frmprice').style.display = '';
            else
                document.getElementById('frmprice').style.display = '';
        }
    </script>
    <div style="display: none;">
        <input type="hidden" id="hdnTabid" runat="server" value="1" />
    </div>
    </div>
</asp:Content>
