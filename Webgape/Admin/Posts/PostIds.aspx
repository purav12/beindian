<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostIds.aspx.cs" Inherits="Webgape.Admin.Posts.PostIds" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../assets/css/ace-responsive.min.css" rel="stylesheet" />
    <link href="../assets/css/ace.min.css" rel="stylesheet" />


    <link href="/Admin/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Admin/assets/css/bootstrap-responsive.min.css"
        rel="stylesheet" />

    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <link rel="stylesheet" href="/Admin/assets/css/style.min.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" language="javascript">
        function fvalidation() {
            var a = document.getElementById('<%=txtsearch.ClientID %>').value;
            if (a == "") {
                $(document).ready(function () { jAlert('Enter Keyword to Search Post!', 'Message', '<%=txtsearch.ClientID %>'); });
                return false;
            }
            return true;
        }
        function fcheckCount() {
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
                $(document).ready(function () { jAlert('Check at least One Record!', 'Message'); });
                return false;
            }
            else if (count > 6) {
                $(document).ready(function () { jAlert('Check Max. Six Record!', 'Message'); });
                return false;
            }
            return true;
        }
    </script>
</head>
<body style="background: none">

    <form id="form1" runat="server">
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <img src="/style/images/logo.png" style="margin-top: 10px" />
                    <div class="widget-toolbar">
                        <asp:HiddenField ID="hfskus" runat="server" />
                        <a href="javascript:void(0);" onclick="window.close();"><span class="btn btn-mini btn-info">Close </span></a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header">
                    <h4>Post(s)</h4>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <div class="row-fluid">
                            <div class="span10">
                                <div class="row-fluid">
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" class="content-table border-td"
                                        style="padding: 2px;">
                                        <tbody>

                                            <tr>
                                                <td>
                                                    <div class="slidingDivImage" style="padding-top: 8px; padding-bottom: 8px;">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="1" cellspacing="1" width="100%">
                                                                        <tr>
                                                                            <td style="width: 67px; padding-left: 5px;">
                                                                                <span style="font-family: Arial,sans-serif; color: #212121; font-size: 13px">Search</span>:
                                                                            </td>

                                                                            <td style="width: 320px;">
                                                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="span6" Style="margin-top: 10px;"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="ibtnFeaturesystemsearch" runat="server" Text="Search" CssClass="btn btn-mini btn-info btn-padding"
                                                                                    OnClientClick="return fvalidation();" OnClick="ibtnFeaturesystemsearch_Click" />
                                                                                &nbsp;
                                                                                <asp:Button ID="ibtnfeaturesystemshowall" runat="server" Text="Show All" CssClass="btn btn-mini btn-info btn-padding"
                                                                                    OnClick="ibtnfeaturesystemshowall_Click" />
                                                                                &nbsp;&nbsp;
                                                                                <asp:Button ID="ibtnFeaturesystemaddtoselectionlist" Style="float: right" runat="server" Text="Add To Selection List" CssClass="btn btn-mini btn-info btn-padding"
                                                                                    OnClientClick="return fcheckCount();" OnClick="ibtnFeaturesystemaddtoselectionlist_Click" />&nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="padding: 2px">
                                                    <div id="rdolist" style="border: 5px sollid #e7e7e7;">
                                                        <asp:GridView ID="grdFeaturesystem" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-striped table-bordered table-hover dataTable"
                                                            EmptyDataText="No Record Found!" RowStyle-ForeColor="Black" HeaderStyle-ForeColor="#3c2b1b"
                                                            EmptyDataRowStyle-HorizontalAlign="Center" AllowPaging="True" PageSize="20" OnPageIndexChanging="grdFeaturesystem_PageIndexChanging">
                                                            <EmptyDataTemplate>
                                                                <span style="color: Red; font-size: 12px; text-align: center;">
                                                                    <center>No Record(s) Found !</center>
                                                                </span>
                                                            </EmptyDataTemplate>

                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <table cellpadding="0" cellspacing="1" border="0" align="center">
                                                                            <tr style="border: 0px;">
                                                                                <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                                    <strong>Select</strong>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="input-checkbox" />
                                                                        <asp:HiddenField ID="hdnPostid" runat="server" Value='<%#Eval("PostID") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="tdpro-checkbox" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Post Title">
                                                                    <ItemTemplate>
                                                                        <%# SetName(Convert.ToString(Eval("Title")))%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Post Id">
                                                                    <ItemTemplate>
                                                                        <%#Eval("PostID") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="30"></PagerSettings>
                                                            <PagerStyle HorizontalAlign="right" CssClass="numbering" />
                                                            <RowStyle HorizontalAlign="left" CssClass="eve-row" ForeColor="#000000" />
                                                            <AlternatingRowStyle CssClass="odd-row" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                                            <HeaderStyle ForeColor="White" Font-Bold="false" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
