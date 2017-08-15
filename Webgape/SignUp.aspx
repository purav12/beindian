<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Webgape.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register - BeIndian.In</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="Register - BeIndian.In" />
    <meta name="keywords" content="Register" />
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
            if (document.getElementById('<%=txtFname.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter First Name.";
                return false;
            }
            else if (document.getElementById('<%=txtLname.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter Last Name.";
                return false;
            }
            else if (document.getElementById('<%=txtEmail.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter email address.";
                return false;
            }
            else if (document.getElementById('<%=txtEmail.ClientID %>').value != '' && !checkemail1(document.getElementById('<%=txtEmail.ClientID %>').value)) {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter valid email address.";
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID %>').value == '') {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Please enter password.";
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID %>').value.length < 5) {
                document.getElementById('<%=lblerror.ClientID %>').innerText = "Password length must be 5 characters.";
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
            <div id="form_wrapper" class="form_wrapper" style="margin: 0 22.5%;">
                <form class="register active" id="registerform" runat="server">
                    <h3>Register</h3>
                    <div class="column" style="margin-bottom: 20px;">
                        <div>
                            <label>First Name:</label>
                            <asp:TextBox ID="txtFname" runat="server" placeholder="Enter First Name" TabIndex="1"></asp:TextBox>
                        </div>
                        <div>
                            <label>Email:</label>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email Address" TabIndex="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="column" style="margin-bottom: 20px;">
                        <div>
                            <label>Last Name:</label>
                            <asp:TextBox ID="txtLname" runat="server" placeholder="Enter Last Name" TabIndex="2"></asp:TextBox>
                        </div>
                        <div>
                            <label>Password:</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="Enter Password" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="bottom">
                        <div class="remember" style="width: 40%;">
                            <input type="checkbox" runat="server" checked="checked" />
                            <span>Agree &nbsp;
                                <a href="Terms-and-conditions.aspx" target="_blank" style="display: inline; padding: 0px;">Terms And Conditions</a>
                            </span>
                        </div>
                        <asp:Button ID="btnsignup" OnClientClick="return Checkfields();" TabIndex="5" class="btnlogin" Text="Sign Up" runat="server" OnClick="btnsignup_Click" />
                        <a href="Login.aspx" rel="Login.aspx" class="linkform">You have an account already? Log in here</a>
                        <a href="ForgotPassword.aspx" class="linkform">Forgot your password?</a>
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
