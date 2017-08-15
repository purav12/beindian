<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Webgape.Admin.Profile.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/App_Themes/<%=Page.Theme %>/js/jquery-alerts-main.js"></script>
    <script type="text/javascript" src="/App_Themes/<%=Page.Theme %>/js/jquery-alerts.js"></script>
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
        function Checkfields() {

            if (document.getElementById('<%=txtfrom.ClientID %>').value == '') {
                jAlert('Something wrong try again later.', 'Message', '<%=txtfrom.ClientID %>');
                return false;
            }

            if (document.getElementById('<%=txtto.ClientID %>').value == '') {
                jAlert('Something wrong try again later.', 'Message', '<%=txtto.ClientID %>');
                return false;
            }

            if (document.getElementById('<%=txtmessage.ClientID %>').value == '') {
                jAlert('Please enter Message.', 'Message', '<%=txtmessage.ClientID %>');
                return false;
            }

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Message</li>
        </ul>
        <!--#nav-search-->
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Message
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label ID="lblHeader" runat="server" Text="Add Message"></asp:Label>
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
                                                <div class="control-group">
                                                    <div class="controls" style="float: right; padding-right: 35px;">
                                                        <span class="star-red">*</span>Required Field
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Message From:</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" Enabled="false" ID="txtfrom" CssClass="span4" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Message To:</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" Enabled="false" ID="txtto" CssClass="span4" MaxLength="500"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Message:</label>
                                                    <div class="controls">
                                                        <asp:TextBox TextMode="multiLine" ID="txtmessage" Height="200px" Width="850px" CssClass="span4"
                                                            runat="server"></asp:TextBox>
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
                                                            <asp:Button ID="btnSaveMessage" runat="server" Text="Send Message" ToolTip="Send Message" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnSendMessage_Click" OnClientClick="return Checkfields();" />
                                                            <asp:Button ID="btnCancelMessage" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnCancelMessage_Click" />
                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" ToolTip="Delete" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnDeleteMessage_Click" />
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

    <div style="display: none;">
        <input type="hidden" id="hdnfromid" value="0" runat="server" />
        <input type="hidden" id="hdntoid" value="0" runat="server" />
    </div>

</asp:Content>
