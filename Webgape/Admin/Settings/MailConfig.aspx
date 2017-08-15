<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/Admin.Master" AutoEventWireup="true"
    CodeBehind="MailConfig.aspx.cs" Inherits="Webgape.ADMIN.Settings.OnePageMailConfig"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>

    <style type="text/css">
        .divbordernone {
            border-style: none !important;
        }

        .control-width {
            width: 250px !important;
            padding-right: 15px;
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
            <li class="active">Mail Configuration</li>
        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Mail Configuration
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label runat="server" Text="Mail Configuration" ID="Label1"></asp:Label></h5>


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
                                                    <div class="controls">
                                                       
                                                        <asp:Button ID="imgTestMail" runat="server" Text="Test Mail" CssClass="btn btn-mini btn-info btn-padding  "
                                                            CausesValidation="true" OnClick="imgTestMail_Click"
                                                            ToolTip="Test Mail" ValidationGroup="AppConfig" />
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Host:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtHost" runat="server" CssClass="span6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtHost" runat="server" ControlToValidate="txtHost"
                                                            ErrorMessage="Enter Host" SetFocusOnError="True" ValidationGroup="AppConfig"
                                                            Display="Dynamic" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Mail User Name :</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMailUserName" runat="server" CssClass="span6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMailUserName" runat="server" ControlToValidate="txtMailUserName"
                                                            ErrorMessage="Enter Mail User Name" SetFocusOnError="True" ValidationGroup="AppConfig"
                                                            Display="Dynamic" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtMailUserName" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtMailUserName" ErrorMessage="Enter Valid User Name" ForeColor="#FF0000"
                                                            SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="AppConfig"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Mail Password :</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMailPassword" runat="server" CssClass="span6" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMailPassword" runat="server" ControlToValidate="txtMailPassword"
                                                            Display="Dynamic" ErrorMessage="Enter Mail Password" SetFocusOnError="True" ValidationGroup="AppConfig"
                                                            ForeColor=" #FF0000"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Mail From Address :</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMailFrom" runat="server" CssClass="span6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMailFrom" runat="server" ControlToValidate="txtMailFrom"
                                                            Display="Dynamic" ErrorMessage="Enter Mail From Address" SetFocusOnError="True"
                                                            ValidationGroup="AppConfig" ForeColor=" #FF0000"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtMailFrom" runat="server" ControlToValidate="txtMailFrom"
                                                            ErrorMessage="Enter Valid E-Mail" ForeColor=" #FF0000" SetFocusOnError="True"
                                                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="AppConfig"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Mail To Address :</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMailMe_ToAddress" runat="server" CssClass="span6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtMailMe_ToAddress" runat="server" ControlToValidate="txtMailMe_ToAddress"
                                                            ErrorMessage="Enter Mail To Address" SetFocusOnError="True" ValidationGroup="AppConfig"
                                                            Display="Dynamic" ForeColor=" #FF0000"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtMailMe_ToAddress" runat="server" ControlToValidate="txtMailMe_ToAddress"
                                                            ErrorMessage="Enter Valid E-Mail" ForeColor=" #FF0000" SetFocusOnError="True"
                                                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="AppConfig"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width"><span class="star-red">*</span>Contact Mail To Address :</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContactMail_ToAddress" runat="server" CssClass="span6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtContactMail_ToAddress" runat="server" ControlToValidate="txtContactMail_ToAddress"
                                                            ErrorMessage="Enter Contact Mail To Address " SetFocusOnError="True" ValidationGroup="AppConfig"
                                                            Display="Dynamic" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtContactMail_ToAddress" runat="server" ControlToValidate="txtContactMail_ToAddress"
                                                            ErrorMessage="Enter Valid E-Mail" ForeColor="#FF0000" SetFocusOnError="True"
                                                            Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="AppConfig"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label control-width">Send Customer Registration Mail?:</label>
                                                    <div class="controls" style="float: left; margin-left: 0px; padding-top: 6px;">
                                                        <asp:CheckBox ID="chkSendCustomerRegistrationMail" runat="server" CssClass="input-checkbox" />
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
                                                            <asp:Button ID="imgSave" runat="server" Text="Save" ToolTip="Save" CssClass="btn btn-small btn-success btn-padding"
                                                                CausesValidation="true" OnClick="imgSave_Click" ValidationGroup="AppConfig" />
                                                            <asp:Button ID="imgCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="imgCancel_Click" />
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
