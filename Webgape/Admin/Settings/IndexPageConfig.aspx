<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="IndexPageConfig.aspx.cs" Inherits="Webgape.Admin.Settings.IndexPageConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
    <script src="../../ckeditor/_samples/sample.js" type="text/javascript"></script>
  <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>
     <style type="text/css">
        .tab-content {
            padding-top:0px;
        }
         .widget-body tab-content {
              padding-top:0px;
         }
        .row-fluid .form-horizontal .control-group {
            margin-bottom: 10px;
        }
    </style>
    <script type="text/javascript">
        function Checkfields() {
            if (document.getElementById("ContentPlaceHolder1_FileUploadBanner").value == '') {

                jAlert('Please Select Banner Image.</br>( Size should be 279 x 260 )', 'Message', 'ContentPlaceHolder1_FileUploadBanner');
                return false;
            }
            return true;
        }
        function keyRestrict(e, validchars) {
            var key = '', keychar = '';
            key = getKeyCode(e);
            if (key == null) return true;
            keychar = String.fromCharCode(key);
            keychar = keychar.toLowerCase();
            validchars = validchars.toLowerCase();
            if (validchars.indexOf(keychar) != -1)
                return true;
            if (key == null || key == 0 || key == 8 || key == 9 || key == 13 || key == 27 || key == 46)
                return true;
            return false;
        }
        function getKeyCode(e) {
            if (window.event)
                return window.event.keyCode;
            else if (e)
                return e.which;
            else
                return null;
        }
    </script>
    <script type="text/javascript">
        function checkondelete(message, id) {
            jConfirm(message, 'Confirmation', function (r) {
                if (r == true) {

                    document.getElementById("ContentPlaceHolder1_hdnDelete").value = id;
                    document.getElementById("ContentPlaceHolder1_btnhdnDelete").click();
                    return true;
                }
                else {
                    return false;
                }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        function checkondeleteFeaturedSystem(message, id) {
            jConfirm(message, 'Confirmation', function (r) {
                if (r == true) {

                    document.getElementById("ContentPlaceHolder1_hdnFeaturedPost").value = id;
                    document.getElementById("ContentPlaceHolder1_btnhdnFeaturedPost").click();
                    return true;
                }
                else {
                    return false;
                }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        function checkondeleteBestSeller(message, id) {
            jConfirm(message, 'Confirmation', function (r) {
                if (r == true) {

                    document.getElementById("ContentPlaceHolder1_hdnBestSeller").value = id;
                    document.getElementById("ContentPlaceHolder1_btnhdnBestSeller").click();
                    return true;
                }
                else {
                    return false;
                }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        function checkondeleteNewArrival(message, id) {
            jConfirm(message, 'Confirmation', function (r) {
                if (r == true) {

                    document.getElementById("ContentPlaceHolder1_hdnNewArrival").value = id;
                    document.getElementById("ContentPlaceHolder1_btnhdnNewArrival").click();
                    return true;
                }
                else {
                    return false;
                }
            });
            return false;
        }
    </script>
    <script src="../js/jquery-onoff.js" type="text/javascript"></script>
    <script src="../js/jquery-switch.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 14px;
        }

        .left {
            float: left;
            width: 120px;
        }
    </style>
    <script type="text/javascript">

        function MakeCheckedall(flag, id) {
            var arrflag = flag.split(',');
            var arrid = id.split(',');
            if (arrflag.length == arrid.length)
                for (var i = 0; i < arrflag.length; i++) {
                    if (arrflag[i].toString() == "true") {
                        document.getElementById(arrid[i].toString()).checked = true;
                    }
                    else {
                        document.getElementById(arrid[i].toString()).checked = false;
                    }
                }
        }

        function CheckState(id) {
            var i = "";
            if (document.getElementById(id).checked) {
                i = "on";
            }
            else {
                i = "off";
            }
            return i;
        }

        function Getstatusall(chkID, DivID, btnID, txtID) {
            var arrchk = chkID.split(',');
            var arrdiv = DivID.split(',');
            var arrtxt = txtID.split(',');
            if (arrchk.length == arrdiv.length)
                for (var i = 0; i < arrdiv.length; i++) {
                    var state = CheckState(arrchk[i]);
                    $('#' + arrdiv[i]).iphoneSwitch(state, { switch_on_container_path: '../images/iphone_switch_container_off.png' }, arrchk[i], btnID, arrtxt[i]);
                }
        }

        function fire() {
            document.getElementById('<%=imgbtntxtsave.ClientID %>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>



    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/Admin/Dashboard.aspx">Home</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>


            <li class="active">Index Page Configuration</li>

        </ul>
        <div class="nav-search" id="nav-search" style="display: none;">
            <form class="form-search">
                <span class="input-icon"></span>
            </form>
        </div>
    </div>
    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Index Page Configuration
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Store :</h5>

                    <asp:DropDownList ID="ddlStore" runat="server" Width="250px" CssClass="option1 margin-top-1"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>

            </div>
        </div>
        <div class="row-fluid margin-top">
            <div class="span12">

                <legend>DEAL OF DAY Post</legend>
                <div class="form-horizontal">

                    <div class="control-group">

                        <div class="controls" style="float: right; padding-right: 35px;">
                            <span class="star-red">*</span>Required Field
                        </div>
                    </div>
                    <div class="control-group">

                        <div class="controls">
                            <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                        </div>
                    </div>

                    <div class="control-group">
                        <label for="form-field-1" class="control-label"><span class="star-red">*</span> Post Name :</label>
                        <div class="controls">
                            <asp:DropDownList ID="ddlPost" runat="server" DataTextField="PostName" DataValueField="PostID"
                                Width="560px">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="control-group">
                        <label for="form-field-1" class="control-label"><span class="star-red">*</span> Post Price :</label>
                        <div class="controls">
                            <asp:TextBox ID="txtHotdealprice" CssClass="span4" Width="100px" onkeypress="return keyRestrict(event,'0123456789.');"
                                runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="control-group">
                        <label for="form-field-1" class="control-label"><span class="star-red">*</span> Banner Image :</label>
                        <div class="controls">
                            <asp:FileUpload ID="FileUploadBanner" runat="server" />
                            <asp:Label ID="lblImgSize" runat="server" Text="Size should be 279 x 260"></asp:Label>
                            <div>
                                <img id="imgBanner" runat="server" visible="false" width="150" height="150" />
                            </div>

                        </div>
                    </div>

                    <div class="control-group">
                        <label for="form-field-1" class="control-label"></label>
                        <div class="controls">

                            <asp:Button ID="imgSave" runat="server" Text="Save" ToolTip="Save" CssClass="btn btn-mini btn-info btn-padding"
                                OnClick="imgSave_Click" OnClientClick="return Checkfields();" />
                        </div>
                    </div>
                </div>

                <table style="width: 100%" cellpadding="2" cellspacing="2">
                    <tr>
                        <td><legend>WELOCOME TEXT CONFIGURATION</legend></td>
                    </tr>
                    <tr align="center" style="text-align: center;">
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 80%">
                                        <table>

                                            <tr>

                                                <td>
                                                    <div class="left" id="divwelcometext" runat="server">
                                                    </div>
                                                </td>

                                            </tr>
                                        </table>
                                        <div style="display: none">
                                            <asp:CheckBox ID="chkwelcome" runat="server" CssClass="input-checkbox" />
                                        </div>
                                    </td>
                                    <td style="width: 20%" align="right"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="even-row">
                        <td>
                            <table style="width: 100%" cellpadding="2" cellspacing="2" class="content-table">
                                <tr valign="middle">
                                    <th style="width: 80%; color: #fff; text-align: left;">
                                        <strong>Welcome Text</strong>
                                    </th>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox TextMode="multiLine" class="description-textarea" ID="txtwelcometext"
                                Rows="10" Columns="80" runat="server" Style="border: solid 1px #e7e7e7; background: none repeat scroll 0 0 #E7E7E7;"></asp:TextBox>
                            <script type="text/javascript">
                                CKEDITOR.replace('<%= txtwelcometext.ClientID %>', { baseHref: '<%= ResolveUrl("~/ckeditor/") %>', height: 150 });
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
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <asp:Button ID="imgwelcomesave" runat="server" Text="Save" ToolTip="Save" CssClass="btn btn-mini btn-info btn-padding"
                                OnClick="imgwelcomesave_Click" />
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td></td>
                    </tr>
                    <tr>
                        <td><legend>FEATURED CATEGORY</legend></td>
                    </tr>
                    <tr align="center" style="text-align: center;">
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 80%">
                                        <table>
                                            <tr>

                                                <td style="padding-left: 10px;">
                                                    <div class="left" id="divfeaturecat" runat="server">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtfeaturecat" runat="server" Width="50px" CssClass="order-textfield"
                                                        onchange="fire();" onkeypress="return keyRestrict(event,'0123456789.');" MaxLength="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="display: none">
                                            <asp:CheckBox ID="chkfeaturecat" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 20%" align="right">
                                        <asp:Button ID="ibtnFeaturecategory" Text="Add Feature Category" runat="server" OnClientClick="return openCenteredCrossSaleWindow1('category');" CssClass="btn btn-mini btn-info btn-padding" />
                                        <div style="display: none;">
                                            <asp:Button ID="btnFeatureCategory" runat="server" Text="bt" OnClick="btnFeatureCategory_Click" CssClass="btn btn-mini btn-info btn-padding" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="even-row">
                        <td>
                            <table style="width: 100%" cellpadding="2" cellspacing="2">
                                <tr valign="middle">
                                    <td style="width: 80%">
                                        <asp:GridView ID="grdFeaturedcategory" GridLines="None" runat="server" ForeColor="White" CssClass="table table-striped table-bordered table-hover dataTable"
                                            AutoGenerateColumns="False" RowStyle-ForeColor="Black" CellPadding="0" CellSpacing="1"
                                            EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                            HeaderStyle-ForeColor="#3c2b1b" Width="100%" OnRowDataBound="grdFeaturedcategory_RowDataBound"
                                            OnRowCancelingEdit="grdFeaturedcategory_RowCancelingEdit" OnRowEditing="grdFeaturedcategory_RowEditing"
                                            OnRowUpdating="grdFeaturedcategory_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%">
                                                    <HeaderTemplate>
                                                        <table cellpadding="2" cellspacing="1" border="0" align="left">
                                                            <tr style="border: 0px;">
                                                                <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                                    <strong>Category Name</strong>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("Name") %>
                                                        <asp:HiddenField ID="hdnCategoryid" runat="server" Value='<%#Eval("CategoryID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parent Category Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPname" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Order" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%#Eval("DisplayOrder") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDisplayorder" runat="server" Text='<%#Eval("DisplayOrder") %>'
                                                            MaxLength="8" Width="50%" Style="text-align: center;" OnKeyPress="return isNumberKey(event);"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Operations">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="ibtnUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Save"><i class="icon-save bigger-160"></i></asp:LinkButton>
                                                        &nbsp;<asp:LinkButton ID="ibtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"><i class="icon-remove bigger-160 red"></i></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ibtnEdit" runat="server"
                                                            CausesValidation="False" CommandName="Edit"><span><i class="icon-edit bigger-160 green"></i></span></asp:LinkButton>

                                                        <asp:LinkButton runat="server" ID="btnDelete"
                                                            message='<%# Eval("CategoryID") %>' ToolTip="Delete" CommandName="DeleteFeaturedCategory"
                                                            OnClientClick='return checkondelete("Are you sure want to delete selected Featured Category?", this.getAttribute("message"));'
                                                            CommandArgument='<%# Eval("CategoryID") %>'><span><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="tdpro-checkbox" />
                                                    <HeaderStyle CssClass="tdpro-checkbox" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle HorizontalAlign="Center" CssClass="oddrow" />
                                            <AlternatingRowStyle CssClass="altrow" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red"></EmptyDataRowStyle>
                                            <HeaderStyle ForeColor="White" Font-Bold="false" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td></td>
                    </tr>
                    <tr>

                        <td><legend>FEATURED SYSTEM</legend></td>

                    </tr>
                    <tr align="center" style="text-align: center;">
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 80%">
                                        <table>
                                            <tr>

                                                <td style="padding-left: 10px;">
                                                    <div class="left" id="divfeaturesys" runat="server">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtfeaturesys" runat="server" Width="50px" CssClass="order-textfield"
                                                        onchange="fire(this)" onkeypress="return keyRestrict(event,'0123456789.');" MaxLength="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="display: none">
                                            <asp:CheckBox ID="chkfeaturesys" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 20%" align="right">
                                        <asp:Button ID="ibtnFeaturesystem" runat="server" OnClientClick="return openCenteredCrossSaleWindow1('feature');" Text="Add Feature System" CssClass="btn btn-mini btn-info btn-padding" />
                                        <div style="display: block;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 4px;">
                        <td>
                            <asp:GridView ID="grdFeaturedSystem" GridLines="None" runat="server" ForeColor="White" CssClass="table table-striped table-bordered table-hover dataTable"
                                AutoGenerateColumns="false" RowStyle-ForeColor="Black" CellPadding="0" CellSpacing="1"
                                BorderColor="#e7e7e7" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                BorderWidth="1" HeaderStyle-ForeColor="#3c2b1b" Width="100%" OnRowCancelingEdit="grdFeaturedSystem_RowCancelingEdit"
                                OnRowEditing="grdFeaturedSystem_RowEditing" OnRowUpdating="grdFeaturedSystem_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60%">
                                        <HeaderTemplate>
                                            <table cellpadding="2" cellspacing="1" border="0" align="left">
                                                <tr style="border: 0px;">
                                                    <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                        <strong>Title</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Name") %>
                                            <asp:HiddenField ID="hdnPostid" runat="server" Value='<%#Eval("PostID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("SKU") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Order" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("DisplayOrder") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDisplayorder" runat="server" OnKeyPress="return isNumberKey(event);"
                                                MaxLength="8" Text='<%#Eval("DisplayOrder") %>' Width="50%" Style="text-align: center"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operations" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="ibtnUpdate" runat="server" CausesValidation="True" CommandName="Update"><i class="icon-save bigger-160"></i></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="ibtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"><i class="icon-remove bigger-160 red"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ibtnEdit" runat="server"
                                                CausesValidation="False" CommandName="Edit"><span><i class="icon-edit bigger-160 green"></i></asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnDelete"
                                                message='<%# Eval("PostID") %>' ToolTip="Delete" OnClientClick='return checkondeleteFeaturedSystem("Are you sure want to delete selected Featured System?", this.getAttribute("message"));'
                                                CommandArgument='<%# Eval("PostID") %>'><span><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="tdpro-checkbox" />
                                        <HeaderStyle CssClass="tdpro-checkbox" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" CssClass="oddrow" />
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle ForeColor="White" Font-Bold="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td></td>
                    </tr>
                    <tr>

                        <td><legend>BEST SELLER(s)</legend></td>

                    </tr>
                    <tr align="center" style="text-align: center;">
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 80%">
                                        <table>
                                            <tr>
                                                <td style="padding-left: 10px;">
                                                    <div class="left" style="float: right;" id="divbestseller" runat="server">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbestseller" runat="server" Width="50px" CssClass="order-textfield"
                                                        onchange="fire(this)" onkeypress="return keyRestrict(event,'0123456789.');" MaxLength="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="display: none">
                                            <asp:CheckBox ID="chkbestseller" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 20%" align="right">
                                        <asp:Button ID="ibtnBestseller" runat="server" OnClientClick="return openCenteredCrossSaleWindow1('best');" CssClass="btn btn-mini btn-info btn-padding" Text="Add Best Seller" />
                                        <div style="display: none;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 4px;">
                        <td>
                            <asp:GridView ID="grdBestSeller" GridLines="None" runat="server" ForeColor="White" CssClass="table table-striped table-bordered table-hover dataTable"
                                AutoGenerateColumns="false" RowStyle-ForeColor="Black" CellPadding="0" CellSpacing="1"
                                EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                HeaderStyle-ForeColor="#3c2b1b" Width="100%" OnRowCancelingEdit="grdBestSeller_RowCancelingEdit"
                                OnRowEditing="grdBestSeller_RowEditing" OnRowUpdating="grdBestSeller_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60%">
                                        <HeaderTemplate>
                                            <table cellpadding="2" cellspacing="1" border="0" align="center">
                                                <tr style="border: 0px;">
                                                    <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                        <strong>Title</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Name") %>
                                            <asp:HiddenField ID="hdnPostId" runat="server" Value='<%#Eval("PostID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("SKU") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Order" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("DisplayOrder") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDisplayorder" runat="server" OnKeyPress="return isNumberKey(event);"
                                                MaxLength="8" Width="50%" Style="text-align: center;" Text='<%#Eval("DisplayOrder") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operations" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="ibtnUpdate" runat="server" CausesValidation="True" CommandName="Update"><i class="icon-save bigger-160 "></i></span></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="ibtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"><i class="icon-remove bigger-160 red"></i></span></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ibtnEdit" runat="server"
                                                CausesValidation="False" CommandName="Edit"><i class="icon-edit bigger-160 green"></i></span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnDelete"
                                                message='<%# Eval("PostID") %>' ToolTip="Delete" CommandName="DeleteFeaturedCategory"
                                                OnClientClick='return checkondeleteBestSeller("Are you sure want to delete selected Best Seller(s)?", this.getAttribute("message"));'
                                                CommandArgument='<%# Eval("PostID") %>'><i class="icon-trash bigger-160 red"></i></span></asp:LinkButton>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="tdpro-checkbox" />
                                        <HeaderStyle CssClass="tdpro-checkbox" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" CssClass="oddrow" />
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle ForeColor="White" Font-Bold="false" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <tr>

                                <td><legend>NEW ARRIVAL(s)</legend></td>

                            </tr>
                        </td>
                    </tr>
                    <tr align="center" style="text-align: center;">
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 80%">
                                        <table>
                                            <tr>
                                                <td style="padding-left: 10px;">
                                                    <div class="left" id="divnewarrival" runat="server">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnewarrival" runat="server" Width="50px" CssClass="order-textfield"
                                                        onchange="fire(this)" onkeypress="return keyRestrict(event,'0123456789.');" MaxLength="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="display: none">
                                            <asp:CheckBox ID="chknewarrival" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 20%" align="right">
                                        <asp:Button ID="ibtnnewarrival" runat="server" OnClientClick="return openCenteredCrossSaleWindow1('new');" CssClass="btn btn-mini btn-info btn-padding" Text="Add New Arrival" />
                                        <div style="display: none;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 4px;">
                        <td>
                            <asp:GridView ID="grdNewarrival" GridLines="None" runat="server" CssClass="table table-striped table-bordered table-hover dataTable"
                                AutoGenerateColumns="false" RowStyle-ForeColor="Black" CellPadding="0" CellSpacing="1"
                                EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center"
                                HeaderStyle-ForeColor="#3c2b1b" Width="100%" OnRowCancelingEdit="grdNewarrival_RowCancelingEdit"
                                OnRowEditing="grdNewarrival_RowEditing" OnRowUpdating="grdNewarrival_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60%">
                                        <HeaderTemplate>
                                            <table cellpadding="2" cellspacing="1" border="0" align="center">
                                                <tr style="border: 0px;">
                                                    <td style="border: 0px; padding: 0px; background-color: transparent;">
                                                        <strong>Title</strong>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Name") %>
                                            <asp:HiddenField ID="hdnPostid" runat="server" Value='<%#Eval("PostID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("SKU") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Display Order" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("DisplayOrder") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDisplayorder" OnKeyPress="return isNumberKey(event);" Width="50%"
                                                MaxLength="8" Style="text-align: center;" runat="server" Text='<%#Eval("DisplayOrder") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operations" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="10%">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="ibtnUpdate" runat="server" CausesValidation="True" CommandName="Update"><i class="icon-save bigger-160 "></i></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="ibtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"><i class="icon-remove bigger-160 red"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ibtnEdit" runat="server" CausesValidation="False" CommandName="Edit"><i class="icon-edit bigger-160 green"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnDelete"
                                                message='<%# Eval("PostID") %>' ToolTip="Delete" OnClientClick='return checkondeleteNewArrival("Are you sure want to delete selected New Arrival(s)?", this.getAttribute("message"));'
                                                CommandArgument='<%# Eval("PostID") %>'><i class="icon-trash bigger-160 red"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="tdpro-checkbox" />
                                        <HeaderStyle CssClass="tdpro-checkbox" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Center" CssClass="oddrow" />
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle ForeColor="White" Font-Bold="false" />
                            </asp:GridView>
                            <div style="display: none;">
                                <asp:Button ID="btnhdnDelete" runat="server" Text="Button" OnClick="btnhdnDelete_Click"
                                     />
                                <asp:HiddenField ID="hdnDelete" runat="server" Value="0" />
                            </div>
                            <div style="display: none;">
                                <asp:Button ID="btnhdnFeaturedPost" runat="server" Text="Button" OnClick="btnhdnFeaturedPost_Click"
                                     />
                                <asp:HiddenField ID="hdnFeaturedPost" runat="server" Value="0" />
                            </div>
                            <div style="display: none;">
                                <asp:Button ID="btnhdnBestSeller" runat="server" Text="Button" OnClick="btnhdnBestSeller_Click"
                                    />
                                <asp:HiddenField ID="hdnBestSeller" runat="server" Value="0" />
                            </div>
                            <div style="display: none;">
                                <asp:Button ID="btnhdnNewArrival" runat="server" Text="Button" OnClick="btnhdnNewArrival_Click"
                                    />
                                <asp:HiddenField ID="hdnNewArrival" runat="server" Value="0" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </div>



    <script language="javascript" type="text/javascript">

        function openCenteredCrossSaleWindow1(mode) {
            if (document.getElementById('<%=ddlStore.ClientID %>').value != "0") {
                var width = 1030;
                var height = 600;
                var left = parseInt((screen.availWidth / 2) - (width / 2));
                var top = parseInt((screen.availHeight / 2) - (height / 2));
                var StoreID = document.getElementById('<%=ddlStore.ClientID %>').value;
                var windowFeatures = "width=" + width + ",height=" + height + ",status=no,resizable=yes,scrollbars=yes,left=" + left + ",top=" + top + ",screenX=" + left + ",screenY=" + top;
                window.open('IndexPageConfigPopup.aspx?StoreID=' + StoreID + '&mode=' + mode, "Mywindow", windowFeatures);
                return false;
            }
            else {
                jAlert('Select Store!', 'Message', '<%=ddlStore.ClientID %>');
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                jAlert('Enter Valid Display Order!', 'Message');
                return false;
            }
            return true;
        }

        function testi() {
            var bt = document.getElementById('<%=btnFeatureCategory.ClientID %>');
            if (bt) {
                bt.click();
            }
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ImageButton ID="imgbtnsave" runat="server" AlternateText="Save" ToolTip="Save"
                ImageUrl="~/App_Themes/Gray/images/save.gif" OnClick="imgbtnsave_Click" Style="display: none" />
            <asp:ImageButton ID="imgbtntxtsave" runat="server" AlternateText="Save" ToolTip="Save"
                ImageUrl="~/App_Themes/Gray/images/save.gif" OnClick="imgbtntxtsave_Click" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
