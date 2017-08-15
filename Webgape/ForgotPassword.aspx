<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Webgape.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forgot Password - BeIndian.In</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="Forgot Password - BeIndian.In" />
    <meta name="keywords" content="Forgot Password" />
    <link rel="shortcut icon" href="/style/images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Logincss/css/style.css" />
    <script src="Logincss/js/cufon-yui.js" type="text/javascript"></script>
    <script src="Logincss/js/ChunkFive_400.font.js" type="text/javascript"></script>
    <script src="/style/js/jquery-1.7.2.min.js"></script>
    <script src="/style/js/jquery-alerts.js"></script>
    <link href="/style/css/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript">
        function Checkfields() {
            if (document.getElementById('<%=txtuname.ClientID %>').value == '') {
                jAlert('Please enter email address.', 'Message', '<%=txtuname.ClientID %>');
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        Cufon.replace('h1', { textShadow: '1px 1px #fff' });
        Cufon.replace('h2', { textShadow: '1px 1px #fff' });
        Cufon.replace('h3', { textShadow: '1px 1px #000' });
        Cufon.replace('.back');
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
            <asp:Label ID="lblerror" runat="server"></asp:Label></h2>
        <div class="content">
            <div id="form_wrapper" class="form_wrapper">
                <form class="forgot_password active" id="forgotform" runat="server">
                    <h3>Forgot Password</h3>
                    <div>
                        <label>Username or Email:</label>
                        <asp:TextBox ID="txtuname" runat="server" placeholder="Username Or Email Id"></asp:TextBox>
                    </div>
                    <div class="bottom">
                        <asp:Button ID="btnforgotpassword" class="btnlogin" Text="Forgot Password" OnClientClick="return Checkfields();" runat="server" OnClick="btnforgot_Click" />
                        <a href="Login.aspx" rel="login" class="linkform">Suddenly remebered? Log in here</a>
                        <a href="SignUp.aspx" rel="register" class="linkform">You don't have an account? Register here</a>
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
