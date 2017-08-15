<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Webgape.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - BeIndian.In</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="Login - BeIndian.In" />
    <meta name="keywords" content="Login" />
    <link rel="shortcut icon" href="/style/images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Logincss/css/style.css" />
    <script src="Logincss/js/cufon-yui.js" type="text/javascript"></script>
    <script src="Logincss/js/ChunkFive_400.font.js" type="text/javascript"></script>
    <script src="/style/js/jquery-1.7.2.min.js"></script>
    <script src="/style/js/jquery-alerts.js"></script>
    <link href="/style/css/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript">
        Cufon.replace('h1', { textShadow: '1px 1px #fff' });
        Cufon.replace('h2', { textShadow: '1px 1px #fff' });
        Cufon.replace('h3', { textShadow: '1px 1px #000' });
        Cufon.replace('.back');
    </script>
    <script type="text/javascript">
        function Checkfields() {
            if (document.getElementById('<%=txtuname.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter Username or email address.";
                return false;
            }
            else if (document.getElementById('<%=txtpass.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter password.";
                return false;
            }
        return true;
    }
    </script>
</head>
<body>
    <div class="wrapper" style="min-height: 0px">
       <div style="margin-bottom:5px">
            <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
            <!-- All page main advertise -->
            <ins class="adsbygoogle"
                style="display: block"
                data-ad-client="ca-pub-1373088425496976"
                data-ad-slot="5649438247"
                data-ad-format="auto"></ins>
            <script>
                (adsbygoogle = window.adsbygoogle || []).push({});
            </script>
        </div>
    </div>
    <div class="wrapper">
        <h1></h1>
        <h2>
            <asp:Label ID="lblerror" runat="server"></asp:Label>
        </h2>
        <div class="content">
            <div id="form_wrapper" class="form_wrapper">
                <form class="login active" id="loginform" runat="server">
                    <h3>Login</h3>
                    <div>
                        <label>Username:</label>
                        <asp:TextBox ID="txtuname" TabIndex="1" runat="server" placeholder="Username Or Email Id"></asp:TextBox>
                    </div>
                    <div>
                        <label>Password: <a href="ForgotPassword.aspx" class="forgot linkform">Forgot your password?</a></label>
                        <asp:TextBox ID="txtpass" TabIndex="2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="bottom">
                        <div class="remember">
                            <input id="chkstore" runat="server" type="checkbox" checked="checked" /><span>Keep me logged in</span>
                        </div>
                        <asp:Button ID="btnloggin" class="btnlogin" TabIndex="3" Text="Login" runat="server" OnClientClick="return Checkfields();" OnClick="btnloggin_Click" />
                        <a href="SignUp.aspx" rel="register" class="linkform">You don't have an account yet? Register here</a>
                        <div class="clear"></div>
                    </div>
                </form>
            </div>
            <div class="clear"></div>
        </div>
        <a class="back" href="/">back to home</a>
    </div>
</body>
</html>
