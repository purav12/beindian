<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="ImageSize.aspx.cs" Inherits="Webgape.Admin.Settings.ImageSize"
    MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <style type="text/css">
        .divbordernone {
            border-style: none !important;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>

            <li>One Page Configuration<span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>

            <li class="active">Image Configuration</li>

        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Image  Configuration
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label runat="server" Text="Image  Configuration" ID="Label1"></asp:Label></h5>


                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget-body divbordernone">
                            <div class="widget-main">
                                <div class="row-fluid">
                                    <div>
                                        <div class="row-fluid">
                                            <div class="form-horizontal">
                                                <div class="control-group">

                                                    <div class="controls">
                                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="control-group">

                                                    <div class="controls" style="float: right; padding-right: 35px;">
                                                        <span class="star-red">*</span>Required Field
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Image Type:</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px; font-weight: bold;">Width (px)
                                                                </td>
                                                                <td style="width: 60px; text-align: center; font-weight: bold;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px; font-weight: bold;">Height (px)
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Product Icon :</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductIconwidth" MaxLength="5" CssClass="order-textfield" runat="server"
                                                                        Style="width: 60px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                        ForeColor="Red" ControlToValidate="txtProductIconwidth" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 60px; text-align: center;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductIconHeight" MaxLength="5" runat="server" CssClass="order-textfield"
                                                                        Style="width: 60px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtProductIconHeight" SetFocusOnError="true" ValidationGroup="Update"
                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Product Medium :</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductMediumWidth" MaxLength="5" CssClass="order-textfield"
                                                                        Width="60px" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtProductMediumWidth"
                                                                        SetFocusOnError="true" ValidationGroup="Update" ForeColor="Red" runat="server"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 60px; text-align: center;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductMediumHeight" MaxLength="5" CssClass="order-textfield"
                                                                        runat="server" onkeypress="return isNumberKey(event)" Width="60px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProductMediumHeight"
                                                                        SetFocusOnError="true" ValidationGroup="Update" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Product Large :</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductLargeWidth" MaxLength="5" CssClass="order-textfield" runat="server"
                                                                        Style="width: 60px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtProductLargeWidth" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 60px; text-align: center;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductLargeHeight" MaxLength="5" runat="server" CssClass="order-textfield"
                                                                        Style="width: 60px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtProductLargeHeight" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Product Micro :</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductMicroWidth" MaxLength="5" CssClass="order-textfield" runat="server"
                                                                        Style="width: 60px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtProductMicroWidth" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 60px; text-align: center;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtProductMicroHeight" MaxLength="5" runat="server" CssClass="order-textfield"
                                                                        Style="width: 60px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtProductMicroHeight" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Category Icon :</label>
                                                    <div class="controls">
                                                        <table width="220px">
                                                            <tr>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtCategoryIconWidth" MaxLength="5" CssClass="order-textfield" runat="server"
                                                                        Style="width: 60px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtCategoryIconWidth" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 60px; text-align: center;">X
                                                                </td>
                                                                <td style="width: 80px; text-align: center; padding: 0px;">
                                                                    <asp:TextBox ID="txtCategoryIconHeight" MaxLength="5" runat="server" CssClass="order-textfield"
                                                                        onkeypress="return isNumberKey(event)" Width="60px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                                                        ControlToValidate="txtCategoryIconHeight" ForeColor="Red" SetFocusOnError="true"
                                                                        ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">

                                            <div class="form-actions center">
                                                <div style="margin-bottom: 5px; line-height: 9px;">
                                                    <br />
                                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">

                                                        <div style="margin-bottom: 1px; margin-top: 3px;">

                                                            <asp:Button ID="btnsave" runat="server" ValidationGroup="Update" Text="Save" CssClass="btn btn-small btn-success btn-padding"
                                                                ToolTip="Save" OnClick="btnsave_Click" />
                                                            &nbsp;<asp:Button ID="btnclose" runat="server" Text="Close" ToolTip="Cancel" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnclose_Click" />

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


</asp:Content>
