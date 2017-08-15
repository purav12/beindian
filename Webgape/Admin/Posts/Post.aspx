<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="Post.aspx.cs" ValidateRequest="false" Inherits="Webgape.Admin.Posts.Post" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function chkHeight() {
            var windowHeight = 0;
            windowHeight = $(document).height(); //window.innerHeight;

            document.getElementById('prepage').style.height = windowHeight + 'px';
            document.getElementById('prepage').style.display = '';
        }
        function demoposts() {
            window.open('/Sample.html', '_blank');
        }
        function openCenteredCrossSaleWindow(x) {
            createCookie('poids', document.getElementById(x).value, 1)
            var width = 1100;
            var height = 600;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var StoreID = '<%=Request.QueryString["StoreID"]%>';
            var ProductID = '<%=Request.QueryString["ID"]%>';
            var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,scrollbars=yes,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open('PostIds.aspx?PostId=' + StoreID + '&clientid=' + x, "subWind", windowFeatures);
        }

        function OpenMoreImagesPopup() {
            var popupurl = "MoreImagesUpload.aspx?ID=<%=Request["postid"] %>";
            window.open(popupurl, "MoreIamgesPopup", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=1100,height=550,left=250,top=80");
        }

        function Tempmsg() {
            alert('You can add more images once you save your post , save post first with one Main image only');
        }

        function createCookie(name, value, days) {
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        }
    </script>
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script src="../../ckeditor/_samples/sample.js" type="text/javascript"></script>
    <script src="../Js/jquery-1.3.2.js"></script>
    <script src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
    <script src="../Js/PostValidation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/admin/Dashboard.aspx">Home</a> <span class="divider"><i
                class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">Add Post</li>
        </ul>
    </div>

    <div class="page-content">
        <div class="page-header position-relative">
            <h1>
                <asp:Label ID="lblHeader" runat="server" Text="Add Post"></asp:Label></h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">
                        <asp:Label ID="lblheader2" runat="server" Text="Add Post"></asp:Label></h5>
                </div>
            </div>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-box transparent">
                    <div class="widget-header widget-header-flat-1" style="margin-top: 10px;">
                        <h4 class="lighter">General </h4>
                        <div class="widget-toolbar" style="padding: 0px 10px;"><a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a></div>
                    </div>
                    <div class="widget-body">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <div class="controls">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                    <span style="float: right; font-size: 13px;"><span class="star-red">* </span>Required Fields</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span>Title</label>
                                <div class="controls">
                                    <asp:TextBox runat="server" Width="60%" ID="txtPostTitle" TabIndex="1" CssClass="span4"></asp:TextBox>
                                </div>
                            </div>

                            <div class="control-group" style="display: none">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span>SKU</label>
                                <div class="controls">
                                    <asp:TextBox runat="server" Width="60%" ID="txtSKU" CssClass="span4"></asp:TextBox>
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span> Post Type</label>
                                <div class="controls">
                                    <asp:DropDownList ID="ddlposttype" runat="server" AutoPostBack="true" class="span4" Width="150px" TabIndex="2" OnSelectedIndexChanged="ddlposttype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp; <span id="Span1" runat="server" visible="true">
                                        <asp:LinkButton ID="lnktypedemo" runat="server" Font-Bold="true" OnClientClick="demoposts(); return false;">See Demo of all type</asp:LinkButton>
                                    </span>
                                </div>
                            </div>

                            <div class="control-group" id="divimage" visible="false" runat="server">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span> Image</label>
                                <div class="controls">
                                    <div class="row-fluid">
                                        <img alt="Upload" id="ImgLarge" src="../images/icon-image.gif"
                                            runat="server" width="150" style="margin-bottom: 5px; border: 1px solid darkgray" /><br />
                                    </div>
                                    <div class="widget-main">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="10%">
                                                    <asp:FileUpload ID="fuPostIcon" runat="server" Width="220px" Style="border: 1px solid #1a1a1a; background: #f5f5f5; color: #000000;"
                                                        TabIndex="25" />
                                                </td>
                                                <td width="9%">

                                                    <asp:Button ID="btnUpload" class="btn btn-mini btn-info btn-padding right margin-right"
                                                        runat="server" Style="margin-left: 10px;" Text="Upload" AlternateText="Upload" OnClick="btnUpload_Click"
                                                        TabIndex="26" />

                                                </td>
                                                <td width="9%">
                                                    <asp:Button ID="btnDelete" runat="server" class="btn btn-mini btn-info btn-padding right margin-right"
                                                        Visible="false" AlternateText="Delete" Text="Delete" OnClick="btnDelete_Click" />
                                                </td>
                                                <td width="55%"></td>
                                            </tr>
                                            <tr id="moreimagestr" runat="server">
                                                <td width="100%" colspan="3" style="float: right;">
                                                    <asp:Button ID="btnupmoretemp" runat="server" class="btn btn-mini btn-info btn-padding right margin-right"
                                                        OnClientClick="JavaScript:Tempmsg();" AlternateText="Upload More Images" Text="Upload More Images" />

                                                    <asp:Button ID="btnupmore" runat="server" class="btn btn-mini btn-info btn-padding right margin-right"
                                                        OnClientClick="JavaScript:OpenMoreImagesPopup();" AlternateText="Upload More Images" Text="Upload More Images" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="control-group" id="divvideo" visible="false" runat="server">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span> Video Link</label>
                                <div class="controls">
                                   <asp:TextBox ID="txtvidlink"  TabIndex="3" CssClass="span4" runat="server"></asp:TextBox>
                                    <br />
                                </div>
                            </div>

                            <div class="control-group" id="divaudio" visible="false" runat="server">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span> Audio Upload</label>
                                <div class="controls">
                                    <asp:FileUpload ID="fileaud" runat="server" Width="220px" Style="border: 1px solid #1a1a1a; background: #f5f5f5; color: #000000;"
                                        TabIndex="25" />
                                    <asp:Label ID="lblaudpath" runat="server" Visible="false" ></asp:Label>
                                    <asp:Label ID="lblsortaudpath" runat="server" ></asp:Label>
                                    <asp:Button ID="btnuploadaud" class="btn btn-mini btn-info btn-padding right margin-right"
                                        runat="server" Style="margin-left: 10px;" Text="Upload" AlternateText="Upload" OnClick="btnUploadAud_Click"
                                        TabIndex="26" />
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">&nbsp;</span>Sort Description</label>
                                <div class="controls">
                                    <asp:TextBox TextMode="multiLine" ID="txtsortdesc"  TabIndex="4" Height="200px" Width="850px" CssClass="span4"
                                        runat="server"></asp:TextBox>
                                    <br />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">*</span>Content Body</label>
                                <div class="controls">
                                    <asp:TextBox TextMode="multiLine" class="description-textarea" ID="txtDescription"
                                        TabIndex="5" Rows="10" Columns="80" runat="server" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;"></asp:TextBox>
                                    <script type="text/javascript">
                                        CKEDITOR.replace('<%= txtDescription.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 300, width: 850 });
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
                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" OnClientClick="demoposts(); return false;">See Demo of all type</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row-fluid">
            <div class="span7">
                <div class="widget-box transparent">
                    <div class="widget-header widget-header-flat-1" style="margin-top: 10px;">
                        <h4 class="lighter">
                            <i class="icon-code orange"></i>SEO</h4>
                        <div class="widget-toolbar" style="padding: 0px 10px;">
                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-body">
                        <div class="widget-toolbar no-border" style="padding-bottom: 6px;">
                            <ul class="nav nav-tabs" id="recent-tab" style="top: 7px;">
                                <li class="active"><a href="#tab1" data-toggle="tab" style="font-size: 14px;">Page Title</a> </li>
                                <li><a href="#tab2" data-toggle="tab" style="font-size: 14px;">Keywords</a> </li>
                                <li><a href="#tab3" data-toggle="tab" style="font-size: 14px;">Description</a> </li>
                            </ul>
                        </div>
                        <div class="widget-main no-padding">
                            <div class="tab-content no-padding overflow-visible">
                                <div class="tab-pane active" id="tab1">
                                    <div class="item-list" id="tasks">
                                        <div class="widget-body">
                                            <asp:TextBox ID="txtSETitle" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                                Columns="6" Height="90px" Rows="6"  TabIndex="6" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab2">
                                    <div class="item-list" id="Div1">
                                        <div class="widget-body">
                                            <asp:TextBox ID="txtSEKeyword" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                                Columns="6" Height="90px" TabIndex="7" Rows="6" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab3">
                                    <div class="item-list" id="Div2">
                                        <div class="widget-body">
                                            <asp:TextBox ID="txtSEDescription" TabIndex="8" runat="server" CssClass="width-95" Width="98%"
                                                Style="float: left; margin: 10px 0 0 0;" Columns="6" Height="90px" Rows="6" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="span5">
                <div class="widget-box transparent">
                    <div class="widget-header widget-header-flat-1" style="margin-top: 10px;">
                        <h4 class="lighter">
                            <i class="icon-stackexchange orange"></i>Categories</h4>
                        <div class="widget-toolbar" style="padding: 0px 10px;">
                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-body">
                        <table>
                            <tr style="display: none;">
                                <td width="18%">Main&nbsp;Category:
                                </td>
                                <td width="82%">
                                    <asp:TextBox ID="txtMaincategory" runat="server" CssClass="add-pro-input" MaxLength="100"
                                        Height="19px" Width="290px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="widget-body">
                        <div class="row-fluid margin-top">
                            <div class="span3">
                                <div class="lighter">
                                    <span class="star-red">*</span><label>Select Category:</label>

                                </div>
                            </div>
                            <div class="span9">
                                <div id="tree1" class="">
                                    <asp:TreeView ID="trvCategories" runat="server" CssClass="input-checkbox-1" ShowCheckBoxes="Leaf"
                                        ForeColor="#212121" Style="float: none;" Font-Size="14px" Width="304px" PopulateNodesFromClient="True"
                                        ShowLines="true" TabIndex="24">
                                    </asp:TreeView>
                                    <input id="treeCatID" runat="server" type="hidden" value="0" />
                                    <script type="text/javascript">
                                        var $assets = "assets";
                                    </script>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row-fluid">
            <div class="span7">
                <div class="widget-box transparent">
                    <div class="widget-header widget-header-flat-1" style="margin-top: 10px;">
                        <h4 class="lighter">
                            <i class="icon-info orange"></i>Additional Info</h4>
                        <div class="widget-toolbar" style="padding: 0px 10px;">
                            <a href="#" data-action="collapse"><i class="icon-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-body" style="margin-top: 10px;">

                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    Related Posts</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtRelPosts" TextMode="MultiLine" TabIndex="10" runat="server" Width="405px" CssClass="span8">
                                    </asp:TextBox>
                                    <a id="aOptAcc" name="aOptAcc" onclick="openCenteredCrossSaleWindow('ContentPlaceHolder1_txtRelPosts');"
                                        style="margin-right: 15px; font-weight: bold; cursor: pointer;" tabindex="34">Select
                                                                                Post(s) </a>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">&nbsp;</span>Post Summary</label>
                                <div class="controls">
                                    <asp:TextBox TextMode="multiLine" ID="txtSummary" TabIndex="11" Height="90px" Width="405px" CssClass="span4"
                                        runat="server"></asp:TextBox>
                                    <br />
                                    <span id="Span2" runat="server" style="color: #0088CC; font-weight: bold;" tabindex="34">Any Message To Admin , Who Gonna Review your post
                                    </span>
                                </div>
                            </div>

                            <div class="control-group" id="adminsummary" runat="server" visible="false">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">&nbsp;</span>Admin Summary</label>
                                <div class="controls">
                                    <asp:TextBox TextMode="multiLine" Enabled="false" ID="txtadminsummary" Height="90px" Width="405px" CssClass="span4"
                                        runat="server"></asp:TextBox>
                                    <br />
                                    <span id="Span3" runat="server" style="color: #0088CC; font-weight: bold;" tabindex="34">Message from admin , who reviewed your Post
                                    </span>
                                </div>
                            </div>

                            <div class="control-group" id="usersummary" runat="server" visible="false">
                                <label class="control-label" for="form-field-1">
                                    <span class="star-red">&nbsp;</span>User Summary</label>
                                <div class="controls">
                                    <asp:TextBox TextMode="multiLine" Enabled="false" ID="txtusersummary" Height="90px" Width="405px" CssClass="span4"
                                        runat="server"></asp:TextBox>
                                    <br />
                                    <span id="Span4" runat="server" style="color: #0088CC; font-weight: bold;" tabindex="34">User complaints against your Post
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row-fluid">
            <div class="form-actions center" style="display: none;">
                <button onclick="return false;">
                    Save</button>
                <button onclick="return false;">
                    Cancel</button>
            </div>
            <div class="form-actions center">
                <div style="margin-bottom: 5px; line-height: 9px;">
                </div>
                <div style="margin-bottom: 5px; line-height: 9px;">
                    <br />
                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">
                        <div style="margin-bottom: 1px; margin-top: 3px;">
                            <asp:LinkButton ID="btnSave" runat="server" AlternateText="Save" ToolTip="Save" CausesValidation="true" CssClass="btn btn-small btn-success" Text="Save"
                                OnClientClick="return ValidatePage();" OnClick="btnSave_Click" ValidationGroup="Posts"></asp:LinkButton>


                            &nbsp;&nbsp;&nbsp;

                                                    <asp:LinkButton ID="btnCancle" runat="server" AlternateText="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success" Text="Cancel"
                                                        OnClick="btnCancle_Click" CausesValidation="false"></asp:LinkButton>

                        </div>

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

        <script src="../Js/jquery-1.3.2.min.js"></script>
        <script src="../Js/jquery.smartTab.js"></script>
        <script type="text/javascript">
        </script>
    </div>
</asp:Content>
