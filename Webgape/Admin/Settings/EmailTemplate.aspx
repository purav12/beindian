<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/Admin.Master" AutoEventWireup="true"
    CodeBehind="EmailTemplate.aspx.cs" Inherits="Webgape.Admin.Settings.EmailTemplate"
    ValidateRequest="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script src="../../ckeditor/_samples/sample.js" type="text/javascript"></script>
    <script type="text/javascript" src="/App_Themes/<%=Page.Theme %>/js/jquery-alerts-main.js"></script>
    <script type="text/javascript" src="/App_Themes/<%=Page.Theme %>/js/jquery-alerts.js"></script>
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
        function Checkfields() {

            if (document.getElementById('<%=txtLabel.ClientID %>').value == '') {
                jAlert('Please enter Label.', 'Message', '<%=txtLabel.ClientID %>');
                return false;
            }

            else if (document.getElementById('<%=txtSubject.ClientID %>').value == '') {
                jAlert('Please enter Subject.', 'Message', '<%=txtSubject.ClientID %>');
                return false;
            }
        return true;
    }

    </script>
    <script type="text/javascript">
        var testresults
        function checkemail1(str) {
            var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{1,6}(?:\.[a-z]{1})?)$/i
            if (filter.test(str))
                testresults = true
            else {
                testresults = false
            }
            return (testresults)
        }

    </script>


    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Add Email Template</li>
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
            <h1>Add Email Template
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label ID="lblHeader" runat="server" Text="Add Email Template"></asp:Label></h5>


                </div>
                <div class="row-fluid">
                    <div class="span12">
                        <%--  <div class="widget-header">
                            <h4>Search</h4>
                        </div>--%>
                        <div class="widget-body divbordernone">
                            <div class="widget-main">
                                <div class="row-fluid">

                                    <%--  <div class="span6">
                                    </div>--%>
                                    <div>
                                        <div class="row-fluid">
                                            <div class="form-horizontal">

                                                <div class="control-group">

                                                    <div class="controls" style="float: right; padding-right: 35px;">
                                                        <span class="star-red">*</span>Required Field
                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Label:</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtLabel" CssClass="span4" MaxLength="50"
                                                            Width="350px"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Subject:</label>
                                                    <div class="controls">
                                                        <asp:TextBox runat="server" ID="txtSubject" CssClass="span4" MaxLength="500"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="control-group">
                                                    <label for="form-field-1" class="control-label"><span class="star-red">*</span>Email Body: </label>
                                                    <div class="controls">
                                                        <asp:TextBox TextMode="multiLine" class="description-textarea" ID="txtMailBody" Rows="10"
                                                            Columns="80" runat="server" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;"></asp:TextBox>
                                                        <script type="text/javascript">
                                                            CKEDITOR.replace('<%= txtMailBody.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 400 });
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

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="form-actions center">



                                                <div style="margin-bottom: 5px; line-height: 9px;">
                                                    <br />
                                                    <%--<asp:LinkButton ID="btnSave" runat="server" OnClientClick="return ValidatePage();"
                                                    OnClick="btnSave_Click" class="btn btn-small btn-success" Text="Save" TabIndex="37">Save</asp:LinkButton>--%>

                                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">

                                                        <div style="margin-bottom: 1px; margin-top: 3px;">

                                                            <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" ToolTip="Save" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnSaveTemplate_Click" OnClientClick="return Checkfields();" />
                                                            <asp:Button ID="btnCancelTemplate" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success btn-padding"
                                                                OnClick="btnCancelTemplate_Click" />
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
