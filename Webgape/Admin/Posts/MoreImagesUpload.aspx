<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoreImagesUpload.aspx.cs"
    Inherits="Webgape.Admin.Posts.MoreImagesUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function DeleteImage(id) {
            document.getElementById('hdnimageUrl').value = id;
            document.getElementById('btnDeleteImg').click();
        }
    </script>
    <link type="text/css" href="../Css/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript" src="../Js/jquery-alerts.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (window.opener != null) {
                var txtName = window.opener.document.getElementById("ContentPlaceHolder1_ImgLarge").src;
                document.getElementById("hdnmainImageurl").value = txtName;
                document.getElementById("divimage").innerHTML = '<img title="Main Image" src="' + txtName.toLowerCase().replace('/large/', '/icon/') + '" style="border:solid 1px #eeeeee;width:150px;">';
            }
        });
    </script>
    <script src="/style/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Js/jquery-1.3.2.js"></script>
    <script type="text/javascript" src="../../style/js/jquery-1.7.2.min.js"></script>
    <link href="../assets/css/ace.min.css" rel="stylesheet" />
    <link href="../assets/css/ace-responsive.min.css" rel="stylesheet" />
</head>
<body style="background: none">
    <form id="form1" runat="server">

        <div class="row-fluid">
            <div class="span12">
                <div class="widget-header header-color-dark">
                    <h5>Upload More Images</h5>
                    <asp:HiddenField ID="hdnmainImageurl" runat="server" />
                    <div class="widget-toolbar">
                        <input id="hdnimageUrl" runat="server" type="hidden" value="" />
                        <div style="display: none;">
                            <asp:Button ID="btnDeleteImg" runat="server" OnClick="btnDeleteImg_Click" />
                        </div>
                        <a onclick="window.close();" style="padding-top:5px" class="show_hideMainDiv" href="javascript:void(0);"><span class="btn btn-mini btn-info">Close </span></a>
                    </div>
                </div>
            </div>
        </div>
        <div style="padding: 4px;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="content-table border-td">
                <tbody>
                    <tr>
                        <td>
                            <div class="slidingDivImage">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="20%" valign="top" style="padding-top: 10px;">
                                            <span style="font-family: Arial,sans-serif; color: #212121; font-size: 13px">&nbsp;&nbsp;
                                            Post Name </span>:
                                        </td>
                                        <td width="80%" valign="top" style="padding-top: 10px;">
                                            <asp:Label ID="lblPostName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" colspan="2" style="padding-bottom: 10px;">
                                            <table id="tblOldImage" runat="server">
                                                <tr id="trOtherImg" runat="Server">
                                                    <td width="20%" valign="middle" style="padding-top: 10px;">
                                                        <span style="font-family: Arial,sans-serif; color: #212121; font-size: 13px">&nbsp;&nbsp;
                                                        Main Image </span>:
                                                    </td>
                                                    <td style="width: 600px; height: 20px;" valign="middle">
                                                        <div id="divimage" style="width: 100%; left: 50px; padding-left: 60px;">
                                                            <asp:Literal ID="ltOldimages" runat="server"></asp:Literal>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <div style="width: 50%">
                                                <asp:Label ID="lblerror" runat="server" Style="color: red"></asp:Label>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" valign="top">
                                            <strong style="font-family: Arial,sans-serif; color: #212121; font-size: 13px">&nbsp;&nbsp;
                                            Upload Image</strong> :
                                        </td>
                                        <td>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="padding-bottom: 5px;">
                                                <tr align="left">
                                                    <td style="width: 230px">
                                                        <asp:FileUpload ID="fileUploder" runat="server" BorderColor="#8C9CB1" BorderWidth="1px"
                                                            ForeColor="#333333" BorderStyle="Solid" />
                                                    </td>
                                                    <td>&nbsp;<asp:LinkButton ID="btnUpload" runat="server" Text="Upload" ToolTip="Upload" CssClass="btn btn-mini btn-info"
                                                        OnClick="btnUpload_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="ltMoreimages" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
