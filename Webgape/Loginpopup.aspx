<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginpopup.aspx.cs" Inherits="Webgape.Loginpopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style/css/style.css" rel="stylesheet" />
    <script src="/style/js/jquery-1.7.2.min.js"></script>
    <script>
        function disablePopupmaster() {
            //alert(popupStatuspricequote);
            //if (popupStatuspricequote == 1) {
            $("#backgroundPopup").fadeOut("slow");
            $("#popupContactpricequote1").fadeOut("slow");
            //    popupStatuspricequote = 0;
            //}
        }
    </script>
</head>
<body style="background: none; margin-left: 30px;">
    <form id="form1" runat="server" style="width: 100%;">
        <div>
            <p style="margin-top: 40px; font-size: 22px; color: #E6E6E6;">You need to login first</p>
            <p>
                <asp:Button ID="btnlogin" class="button" Style="opacity: 1; margin-left: 75px;" runat="server" Text="Login" OnClick="btnlogin_Click" />
            </p>
            <p style="margin-top: 40px; font-size: 22px; color: #E6E6E6; margin-left: 87px;">
                OR
            </p>
            <p>
                <asp:Button ID="btnregister" class="button" Style="opacity: 1; margin-left: 70px;" runat="server" Text="Register" OnClick="btnregister_Click" />
            </p>
        </div>
        <%--<div id="loginform" style="display: block; visibility: visible;">
            <form method="post">
                <p>You need to login first</p>
                <input type="image" id="close_login" src="images/close.png">
                <input type="text" id="login" placeholder="Email Id">
                <input type="password" id="password" placeholder="************">
                <input type="submit" id="dologin" value="Login">
            </form>
        </div>--%>
    </form>

</body>
</html>
