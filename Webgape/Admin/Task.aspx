<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Task.aspx.cs" Inherits="Webgape.Admin.Task" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Js/jquery-1.3.2.js"></script>
    <script src="../style/js/jquery-1.7.2.min.js"></script>
    <script src="Js/jquery-calender-custom.js"></script>
    <script src="Js/jquery-calender.js"></script>
    <script src="Js/jquery-alerts.js"></script>
    <link href="Css/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript">
        var $j = jQuery.noConflict();
        $j(function () {
            $j('#ContentPlaceHolder1_txttaskdate').datetimepicker({
                showButtonPanel: true, ampm: false,
                showHour: false, showMinute: false, showSecond: false, showTime: false, showOn: "button",
                buttonImage: "/Admin/Images/date-icon.png", buttonImageOnly: true
            });
        });
    </script>
    <script type="text/javascript">
        function ValidatePage() {
            if ((document.getElementById('ContentPlaceHolder1_txttaskdate').value).replace(/^\s*\s*$/g, '') == '') {
                jAlert('Please Enter Task Date', 'Message', 'ContentPlaceHolder1_txttaskdate');
                $('html, body').animate({ scrollTop: $('#ContentPlaceHolder1_txttaskdate').offset().top }, 'slow');
                return false;
            }

            if (document.getElementById("ContentPlaceHolder1_ddltaskstatus") != null && document.getElementById("ContentPlaceHolder1_ddltaskstatus").selectedIndex == 0) {
                jAlert('Please Select Task Status', 'Message', 'ContentPlaceHolder1_ddltaskstatus');
                $('html, body').animate({ scrollTop: $('#ContentPlaceHolder1_ddltaskstatus').offset().top }, 'slow');
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
            <li class="active">Task </li>
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
            <h1>Task
            </h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <!--PAGE CONTENT BEGINS-->
                <div class="widget-header header-color-dark">
                    <h5 class="smaller">Add Task</h5>
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
                                                    <span class="star-red">*</span>Date</label>
                                                <div class="controls">
                                                    <asp:TextBox runat="server" Style="margin-right: 5px;" ID="txttaskdate" CssClass="span4"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label" for="form-field-1">
                                                    <span class="star-red">*</span>Task</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txttask" runat="server" CssClass="width-95" Width="98%" Style="float: left; margin: 10px 0 0 0;"
                                                        Columns="6" Height="90px" Rows="6" TextMode="MultiLine"></asp:TextBox>
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
                                </div>
                                <div style="margin-bottom: 5px; line-height: 9px;">
                                    <br />
                                    <div style="text-align: center;" class="divfloatingcss" id="divfloating">
                                        <div style="margin-bottom: 1px; margin-top: 3px;">
                                            <asp:LinkButton ID="btnSave" runat="server" AlternateText="Save" ToolTip="Save" CausesValidation="true" CssClass="btn btn-small btn-success" Text="Save"
                                                OnClientClick="return ValidatePage();" ValidationGroup="profiles" OnClick="btnSave_Click"></asp:LinkButton>
                                            <%--OnClick="btnSave_Click"--%>
                                            &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnCancle" runat="server" AlternateText="Cancel" ToolTip="Cancel" CssClass="btn btn-small btn-success" Text="Cancel"
                                                        CausesValidation="false" OnClick="btnCancle_Click"></asp:LinkButton>
                                            <%--OnClick="btnCancle_Click"--%>
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
