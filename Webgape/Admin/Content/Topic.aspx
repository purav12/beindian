<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="Topic.aspx.cs" Inherits="Webgape.Admin.Content.Topic" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script src="../../ckeditor/_samples/sample.js" type="text/javascript"></script>
    <style type="text/css">
        .tab-content {
            padding-top: 0px;
        }

        .widget-body tab-content {
            padding-top: 0px;
        }

        .form-horizontal .control-group {
            margin-bottom: 10px;
        }
    </style>
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/admin/dashboard.aspx" title="Home">Home</a> <span class="divider"><i
                class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Add Topic</li>
        </ul>
        <div class="nav-search" id="nav-search">
            <div class="form-search" />
            <span class="input-icon">&nbsp;
            </span>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Add Topic"></asp:Label></h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Add Topic</h5>
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
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="widget-box transparent">
                                                    <div class="widget-header widget-header-flat-1" style="display: none;">
                                                        <div class="widget-toolbar"><a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a></div>
                                                    </div>
                                                    <div class="widget-body tab-content">
                                                        <div class="form-horizontal">
                                                            <div class="control-group">
                                                                <div class="controls">
                                                                    <span style="float: right"><span class="star-red">*</span>Required Fields</span>
                                                                </div>
                                                            </div>
                                                            <div class="control-group">
                                                                <label class="control-label" for="form-field-1">
                                                                    <span class="star-red">*</span>Title</label>
                                                                <div class="controls">
                                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="span4"
                                                                        MaxLength="100"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ControlToValidate="txtTitle"
                                                                        ErrorMessage=" Enter Topic Title" SetFocusOnError="True" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>


                                                            <div class="control-group">
                                                                <label class="control-label" for="form-field-1">
                                                                    <span class="star-red">*</span>Topic Name</label>
                                                                <div class="controls">
                                                                    <asp:TextBox runat="server" ID="txtTopicName" CssClass="span4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTopicName"
                                                                        ErrorMessage=" Enter Topic Name" SetFocusOnError="True" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" runat="server" ControlToValidate="txtTopicName"
                                                                        ErrorMessage="Topic Name Must Not Contain Special Characters" ValidationExpression="([a-z]|[A-Z]|[0-9]|[ ]|[-]|[_])*"
                                                                        ForeColor="Red" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                            <div class="control-group">
                                                                <label class="control-label" for="form-field-1">
                                                                    <span class="star-red">&nbsp;</span>Description</label>
                                                                <div class="controls">
                                                                    <asp:TextBox TextMode="multiLine" class="description-textarea" ID="ckeditordescription"
                                                                        TabIndex="27" Rows="10" Columns="80" runat="server" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;"></asp:TextBox>
                                                                    <script type="text/javascript">
                                                                        CKEDITOR.replace('<%= ckeditordescription.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 400, width: 800 });
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
                                                            <div class="control-group">
                                                                <label class="control-label" for="form-field-1">
                                                                    <span class="star-red">&nbsp;</span>Show on SiteMap</label>
                                                                <div class="controls">
                                                                    <asp:CheckBox ID="chkShowOnSiteMap" runat="server" class="input-checkbox" Style="float: left; width: 100px;" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="widget-box transparent">
                                                    <div class="widget-header widget-header-flat-1">
                                                        <h4 class="lighter">
                                                            <i class="icon-code orange"></i>SEO</h4>
                                                        <div class="widget-toolbar">
                                                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                                                        </div>
                                                    </div>
                                                    <div class="widget-body">
                                                        <div class="widget-toolbar no-border">
                                                            <ul class="nav nav-tabs" id="recent-tab" style="top: 7px;">
                                                                <li class="active"><a href="#tab21" data-toggle="tab">Page Title</a> </li>
                                                                <li><a href="#tab22" data-toggle="tab">Keywords</a> </li>
                                                                <li><a href="#tab23" data-toggle="tab">Description</a> </li>

                                                            </ul>
                                                        </div>
                                                        <div class="widget-main no-padding">
                                                            <div class="tab-content no-padding overflow-visible">
                                                                <div class="tab-pane active" id="tab21">
                                                                    <div class="item-list" id="tasks">
                                                                        <div class="widget-body">
                                                                            <asp:TextBox ID="txtSETitle" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                                                                Columns="6" Height="50px" Rows="6" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="tab22">
                                                                    <div class="item-list" id="tasks">
                                                                        <div class="widget-body">
                                                                            <asp:TextBox ID="txtSEKeywords" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                                                                Columns="6" Rows="6" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="tab23">
                                                                    <div class="item-list" id="tasks">
                                                                        <div class="widget-body">
                                                                            <asp:TextBox ID="txtSEDescription" runat="server" CssClass="width-95" Width="98%"
                                                                                Style="float: left; margin: 10px 0 0 0;" Columns="6" Height="50px" Rows="6" TextMode="MultiLine"></asp:TextBox>
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
                                            <div class="form-actions center">

                                                <div style="margin-bottom: 5px; line-height: 9px;">
                                                    <br />
                                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">
                                                        <div style="margin-bottom: 1px; margin-top: 3px;">
                                                            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-small btn-success" Text="Save" ToolTip="Save"
                                                                OnClick="btnSave_Click" ValidationGroup="Submit"></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="btnCancle" runat="server" OnClick="btnCancle_Click" Text="Cancel" CssClass="btn btn-small btn-success"></asp:LinkButton>
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
