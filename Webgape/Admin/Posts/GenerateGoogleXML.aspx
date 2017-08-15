<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GenerateGoogleXML.aspx.cs" Inherits="Webgape.Admin.Posts.GenerateGoogleXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Generate Google XML</li>
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
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Generate Google XML"></asp:Label>
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller" style="margin-top: 5px;">Generate Google XML :</h5>
                     
                    <div class="widget-toolbar">
                        <asp:LinkButton ID="btnGenerate" runat="server" AlternateText="Generate" ToolTip="Generate"
                            OnClick="btnGenerate_Click" CssClass="btn btn-small btn-primary">Generate Sitemap</asp:LinkButton>

                        <asp:LinkButton ID="btndelerrorlog" runat="server" AlternateText="Regenerate" ToolTip="Regenerate"
                            OnClick="btnReGenerate_Click" CssClass="btn btn-small btn-primary">Generate ErrorLog</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
