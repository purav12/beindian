<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/Admin.Master" AutoEventWireup="true"
    CodeBehind="DataBackup.aspx.cs" Inherits="Solution.UI.Web.ADMIN.Settings.DataBackup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
        function validation() {
            jConfirm('Are you sure, Do you want to perform this action ?', 'Confirmation', function (r) {
                if (r == true) {
                    document.getElementById('ContentPlaceHolder1_btnTemp').click();
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
            <li class="active">DataBase Backup</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>DataBase Backup
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label runat="server" Text="DataBase Backup" ID="lblTitle"></asp:Label>
                    </h5>


                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-body divbordernone">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div>
                                        <div class="row-fluid">
                                            <div class="form-horizontal">
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="form-actions center">
                                                <asp:Button ID="btnBackupDatabase" runat="server" Text="Backup Database" ToolTip="Save" CssClass="btn btn-small btn-success btn-padding"
                                                    OnClick="btnBackupDatabase_Click" OnClientClick="return validation();" />
                                                <div style="display: none">
                                                    <asp:ImageButton ID="btnTemp" runat="server" OnClick="btnBackupDatabase_Click" />
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
</asp:Content>
