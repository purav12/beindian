<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Database.aspx.cs" Inherits="Webgape.Admin.Database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Js/jquery-1.3.2.js"></script>
    <script src="Js/jquery-alerts.js"></script>
    <link href="Css/jquery.alerts.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Database </li>
        </ul>
        <!--.breadcrumb-->
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
        <!--#nav-search-->
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Database
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <!--PAGE CONTENT BEGINS-->
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Database </h5>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="widget-box transparent">
                <div class="widget-body">
                    <div class="widget-main no-padding-10">
                        <div class="row-fluid">
                            <div class="span12">
                                <div class="widget-box transparent">
                                    <div class="widget-body tab-content">
                                        <div class="form-horizontal">
                                            <div class="control-group">
                                                <div class="controls">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                                    <span style="float: right; font-size: 13px;"><span class="star-red">* </span>Required Fields</span>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Table Names</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddltablenames" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddltablenames_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Column Name</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtColumnname" runat="server" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Query</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtQuery" runat="server" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>




                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>File Name</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtname" runat="server" Width="50%"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="grdtempgrid" runat="server">
                        </asp:GridView>

                        <div class="row-fluid">
                            <div class="form-actions center">
                                <div style="margin-bottom: 5px; line-height: 9px;">
                                </div>
                                <div style="margin-bottom: 5px; line-height: 9px;">
                                    <br />
                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">
                                        <div style="margin-bottom: 1px; margin-top: 3px;">
                                            <asp:LinkButton ID="btnshow" runat="server" AlternateText="Show" ToolTip="Show" CssClass="btn btn-small btn-success" Text="Show"
                                                OnClick="btnshow_Click"></asp:LinkButton>

                                            <asp:LinkButton ID="btnExport" runat="server" AlternateText="Export" ToolTip="Export" CssClass="btn btn-small btn-success" Text="Export"
                                                OnClick="btnExport_Click"></asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="control-group">
                            <label class="control-label" for="form-field-1">
                                <span class="star-red">*</span>Comment</label>
                            <div class="controls">
                                <asp:TextBox ID="txtcomment" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                    Columns="6" Height="90px" Rows="6" TextMode="MultiLine">

                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
