<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="TaskList.aspx.cs" Inherits="Webgape.Admin.TaskList" %>

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
    <script src="Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="Css/jquery.alerts.css" rel="stylesheet" />
    <script src="Js/jquery-alerts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Task List</li>
        </ul>
        <ul class="breadcrumb" style="float: right">
            <li><i class="icon-book home-icon"></i><a href="Admin/Log.aspx?entity=Message">Log</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
        </ul>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Task List
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <div class="widget-toolbar" style="float: right;">
                        <a href="/Admin/Task.aspx" id="ahmsg" runat="server">
                            <span class="btn btn-small btn-primary" style="width: 105px;">
                                <i class="icon-plus-sign"></i>Add Task</span>

                        </a>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-header" style="display: none;">
                            <h4>Search By</h4>
                        </div>
                        <div class="widget-body">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="row-fluid">
                                            <span>&nbsp;</span> Status :                                      
                                            <asp:DropDownList ID="ddlType" runat="server" Style="width: 200px;" AutoPostBack="true"
                                                CssClass="no-margin" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" onchange="javascript:chkHeight();">
                                                <asp:ListItem Selected="True">Select Type</asp:ListItem>
                                                <asp:ListItem>Working</asp:ListItem>
                                                <asp:ListItem>Pending</asp:ListItem>
                                                <asp:ListItem>Future</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="pagination no-margin right">
                                                <table width="100%" align="right" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td></td>
                                                        <td style="padding-right: 4px;">Search :</td>
                                                        <td style="padding-right: 4px;">
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="input-medium no-margin no-float"
                                                                Style="vertical-align: text-top" ValidationGroup="SearchGroup" onkeyup="Checktxt();"></asp:TextBox>
                                                        </td>
                                                        <td style="padding-right: 0;">
                                                            <asp:Button ID="btnSearch" runat="server" data-toggle="button" CssClass="btn btn-mini btn-info btn-padding"
                                                                OnClientClick="return validation(); javascript:chkHeight(); " Text="Search" OnClick="btnSearch_Click" />
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
                                                            <span style="float: right;">Total Tasks:
                                                                   
                                                                <% =Taskcount%>
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
                                    <asp:GridView ID="grdTask" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        GridLines="None" EmptyDataText="No Record(s) Found." OnRowCommand="grdTask_RowCommand"
                                        AllowSorting="True" EmptyDataRowStyle-ForeColor="Red" ShowHeaderWhenEmpty="true"
                                        EmptyDataRowStyle-HorizontalAlign="Center" CssClass="table table-border-display background-default"
                                        ViewStateMode="Enabled" Width="100%" AllowPaging="True" PageSize="<%$ appSettings:GridPageSize %>"
                                        RowStyle-ForeColor="Black" HeaderStyle-ForeColor="#3c2b1b" PagerSettings-Mode="NumericFirstLast"
                                        OnPageIndexChanging="grd_PageIndexChanging"
                                        CellSpacing="1" BorderStyle="solid" BorderWidth="1" BorderColor="#e7e7e7" OnRowDeleting="grdTask_RowDeleting">
                                        <EmptyDataTemplate>
                                            <span style="color: Red; font-size: 12px; text-align: center;">No Record(s) Found !</span>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="input-checkbox" />
                                                    <asp:HiddenField ID="hdnTskid" runat="server" Value='<%#Eval("Id") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" Width="7%" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="tdpro-checkbox" />
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="20%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Status
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Width="56%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Task
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltrtask" runat="server" Text='<%# SetName(Convert.ToString(Eval("Task")))%>'></asp:Literal>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <HeaderTemplate>
                                                    Task Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcreatedon" runat="server" Text='<%# Bind("TaskDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="View" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <a id="hplView" runat="server" href='<%# "/Admin/Task.aspx?TaskID="+ DataBinder.Eval(Container.DataItem, "ID")%>'><span class="green"><i class="icon-eye-open bigger-160"></i></span></a>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="tdpro-checkbox"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btndelUser" ToolTip="Delete" CommandName="delete"
                                                        AlternateText="Delete Message" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' message="Are you sure want to delete selected Task ?" OnClientClick='return confirm(this.getAttribute("message"))'>
                                                                 <span><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>
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
                <div class="row-fluid">
                    <table style="width: 100%; margin-top: 10px;">
                        <tr id="trBottom" runat="server">
                            <td style="width: 70%; float: left;">
                                <div class="span6">
                                    <div class="row-fluid">

                                        <span><a id="lkbAllowAll" href="javascript:selectAll(true);" class="btn btn-mini btn-info">Check All</a> | <a id="lkbClearAll"
                                            href="javascript:selectAll(false);" class="btn btn-mini btn-info">Clear All</a> </span><span style="float: right;"></span>

                                    </div>
                                </div>
                            </td>
                            <td style="float: right">
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-mini btn-info"
                                    OnClientClick="return checkCount();" OnClick="btnDelete_Click" ToolTip="Delete">Delete</asp:LinkButton>
                                <div style="display: none;">
                                    <asp:Button ID="btnDeleteTemp" runat="server" OnClick="btnDelete_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
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
            jConfirm('Are you sure want to delete all selected Task ?', 'Confirmation', function (r) {
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
