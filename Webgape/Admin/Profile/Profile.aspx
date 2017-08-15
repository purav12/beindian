<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Webgape.Admin.Profile.Profile" %>

<%@ MasterType VirtualPath="~/Admin/Admin.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>--%>

    <script src="../Js/jquery-1.3.2.min.js"></script>
    <script src="../Js/jquery-1.3.2.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_rbtavtar_0').on('change', function () {
                if ($(this).is(':checked')) {
                    $('#ContentPlaceHolder1_divavtar').show();
                    $('#ContentPlaceHolder1_divupload').hide();
                }
            });
            $('#ContentPlaceHolder1_rbtavtar_1').on('change', function () {
                if ($(this).is(':checked')) {
                    $('#ContentPlaceHolder1_divavtar').hide();
                    $('#ContentPlaceHolder1_divupload').show();
                }
            });

            if (document.getElementById("ContentPlaceHolder1_rbtavtar_1").checked) {
                $('#ContentPlaceHolder1_divavtar').hide();
                $('#ContentPlaceHolder1_divupload').show();
            }
            //($('#ContentPlaceHolder1_rbtavtar_1').is(':checked')) {
            //    $('#ContentPlaceHolder1_divavtar').hide();
            //    $('#ContentPlaceHolder1_divupload').show();
            //};
        });
    </script>
    <script type="text/javascript">
        function SelectADropdownItem(val) {
            var d = document.getElementById('<%= ddlavtar.ClientID %>');
            for (var i = 0; i < d.length; i++) {
                if (d[i].value == val) { d[i].selected = true; }
                else { d[i].selected = false; }
            }
            var imgTag;
            imgTag = document.getElementById('ConfidentM');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('SmartM');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('CoolM');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('ConfidentF');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('SmartF');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('CoolF');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('7');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('8');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('9');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('10');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('11');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('12');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('13');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('14');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('15');
            imgTag.style.border = '1px solid';
            imgTag = document.getElementById('16');
            imgTag.style.border = '1px solid';


            imgTag = document.getElementById(val);
            imgTag.style.border = '5px solid';
        }
    </script>

    <link href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script src="../Js/jquery-alerts.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumbs" id="breadcrumbs">
        <ul class="breadcrumb">
            <li><i class="icon-home home-icon"></i><a href="/admin/Dashboard.aspx">Home</a> <span class="divider"><i
                class="icon-angle-right arrow-icon"></i></span></li>
            <li class="active">General</li>
        </ul>
        <ul class="breadcrumb" style="float: right">
            <li><i class="icon-book home-icon"></i><a href="/Admin/Log.aspx?entity=Admin">Log</a> <span class="divider">
                <i class="icon-angle-right arrow-icon"></i></span></li>
        </ul>
    </div>

    <div class="page-content">
        <div class="page-header position-relative">
            <h1>Edit Profile</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <!--PAGE CONTENT BEGINS-->
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Edit Profile</h5>
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
                                                    <span class="star-red">*</span>First Name</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtfirstname" CssClass="span4" MaxLength="400"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvfirstname" runat="server" ControlToValidate="txtfirstname" ErrorMessage="Please enter First Name"
                                                        SetFocusOnError="True" Display="Dynamic" ValidationGroup="profiles" ForeColor="Red">

                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Last Name</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtlastnames" CssClass="span4" MaxLength="400"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvlastname" runat="server" ControlToValidate="txtlastnames"
                                                        ValidationGroup="profiles" ErrorMessage="Please enter Last Name" SetFocusOnError="True" Display="Dynamic"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>User Name</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtusername" CssClass="span4" MaxLength="400"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvusername" runat="server" ControlToValidate="txtusername"
                                                        ValidationGroup="profiles" ErrorMessage="Please enter User Name" SetFocusOnError="True" Display="Dynamic"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Email</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtemail" CssClass="span4" MaxLength="400"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtemail"
                                                        ValidationGroup="profiles" ErrorMessage="Please enter Email" SetFocusOnError="True" Display="Dynamic"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfvemailcheck" SetFocusOnError="True" Display="Dynamic"
                                                        ForeColor="Red" ValidationGroup="profiles" runat="server" ErrorMessage="Enter valid email" ControlToValidate="txtemail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <div class="control-group" runat="server" id="dvcode">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Enter Code</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" ID="txtcode" CssClass="span4" MaxLength="400"></asp:TextBox>
                                                    &nbsp;
                                                      <asp:Button ID="btnvalidateemaild" class="btn btn-mini btn-info btn-padding margin-right"
                                                          runat="server" Style="margin-left: 10px;" Text="Validate EmailId" OnClick="btnvalidateemaild_Click"
                                                          TabIndex="26" />
                                                </div>
                                            </div>

                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Profile Picture</label>
                                                <div class="controls">
                                                    <asp:RadioButtonList CssClass="input-radio" Style="margin-top: 3px; float: left" ID="rbtavtar" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="Use Avtar" Selected="True" Value="Select An Avtar"></asp:ListItem>
                                                        <asp:ListItem Text="Upload Picture" style="margin-left: 5px" Value="Upload Picture"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="control-group" id="divavtar" style="display: block;" runat="server">
                                                <div class="controls">
                                                    <img id="ConfidentM" onclick="SelectADropdownItem('ConfidentM')" src="/admin/assets/avatars/ConfidentM.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="ConfidentM" />
                                                    <img id="SmartM" onclick="SelectADropdownItem('SmartM')" src="/admin/assets/avatars/SmartM.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="SmartM" />
                                                    <img id="CoolM" onclick="SelectADropdownItem('CoolM')" src="/admin/assets/avatars/CoolM.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="CoolM" />
                                                    <img id="ConfidentF" onclick="SelectADropdownItem('ConfidentF')" src="/admin/assets/avatars/ConfidentF.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="Sand dollar design" />
                                                    <br>
                                                    <img id="SmartF" onclick="SelectADropdownItem('SmartF')" src="/admin/assets/avatars/SmartF.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="Sandria design" />
                                                    <img id="CoolF" onclick="SelectADropdownItem('CoolF')" src="/admin/assets/avatars/CoolF.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="Blue wheel design" />
                                                    <img id="7" onclick="SelectADropdownItem('7')" src="/admin/assets/avatars/7.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="7" />
                                                    <img id="8" onclick="SelectADropdownItem('8')" src="/admin/assets/avatars/8.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="8" />
                                                    <br>
                                                    <img id="9" onclick="SelectADropdownItem('9')" src="/admin/assets/avatars/9.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="9" />
                                                    <img id="10" onclick="SelectADropdownItem('10')" src="/admin/assets/avatars/10.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="10" />
                                                    <img id="11" onclick="SelectADropdownItem('11')" src="/admin/assets/avatars/11.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="11" />
                                                    <img id="12" onclick="SelectADropdownItem('12')" src="/admin/assets/avatars/12.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="12" />
                                                    <br>
                                                    <img id="13" onclick="SelectADropdownItem('13')" src="/admin/assets/avatars/13.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="13" />
                                                    <img id="14" onclick="SelectADropdownItem('14')" src="/admin/assets/avatars/14.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="14" />
                                                    <img id="15" onclick="SelectADropdownItem('15')" src="/admin/assets/avatars/15.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="15" />
                                                    <img id="16" onclick="SelectADropdownItem('16')" src="/admin/assets/avatars/16.png" style="border: 1px solid; height: 70px; width: 70px; cursor: pointer; margin: 10px 10px 10px 10px;" alt="16" />
                                                    <br>
                                                    <br>

                                                    <asp:DropDownList ID="ddlavtar" Style="display: none" Width="150px" runat="server">
                                                        <asp:ListItem>Select Your Avtar.</asp:ListItem>
                                                        <asp:ListItem Value="ConfidentM">ConfidentM</asp:ListItem>
                                                        <asp:ListItem Value="SmartM">SmartM</asp:ListItem>
                                                        <asp:ListItem Value="CoolM">CoolM</asp:ListItem>
                                                        <asp:ListItem Value="ConfidentF">ConfidentF</asp:ListItem>
                                                        <asp:ListItem Value="SmartF">SmartF</asp:ListItem>
                                                        <asp:ListItem Value="CoolF">CoolF</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="control-group" id="divupload" style="display: none;" runat="server">
                                                <div class="controls">
                                                    <div class="row-fluid">
                                                        <img alt="Upload" id="ImgLarge" src="../images/icon-image.gif"
                                                            runat="server" style="margin-bottom: 5px; border: 1px solid darkgray" /><br />
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
                                                        </table>
                                                    </div>
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
                                                OnClick="btnSave_Click" ValidationGroup="profiles"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnCancle" runat="server" AlternateText="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success" Text="Cancel"
                                                        OnClick="btnCancle_Click" CausesValidation="false"></asp:LinkButton>
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
