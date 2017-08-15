<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Webgape.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contact - BeIndian.In</title>
    <meta name="description" content="Contact - BeIndian.In" />
    <meta name="keywords" content="Contact" />
    <style type="text/css">
        .Commnettext {
            border: 1px dotted #65816E;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $('html,body').animate({
                scrollTop: $(".title").offset().top
            },
               'slow');
        });
    </script>
    <script type="text/javascript">
        function validatecontact() {
            if ((document.getElementById('ContentPlaceHolder1_txtname').value) == '') {
                jAlert('Please enter Name', 'Message', 'ContentPlaceHolder1_txtname');
                return false;
            }
            if ((document.getElementById('ContentPlaceHolder1_txtemail').value) == '') {
                jAlert('Please enter Email', 'Message', 'ContentPlaceHolder1_txtemail');
                return false;
            }
            if (document.getElementById("ContentPlaceHolder1_txtemail") != null && document.getElementById("ContentPlaceHolder1_txtemail").value.replace(/^\s+|\s+$/g, '') != '' && !checkemail1(document.getElementById("ContentPlaceHolder1_txtemail").value.replace(/^\s+|\s+$/g, ''))) {
                jAlert('Please enter valid Email', 'Message', 'ContentPlaceHolder1_txtemail');
                return false;
            }
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
            if ((document.getElementById('ContentPlaceHolder1_txtmessage').value) == '') {
                jAlert('Please Enter Message', 'Message', 'ContentPlaceHolder1_txtmessage');
                return false;
            }

        }
    </script>
    <script src="style/js/jquery-alerts.js"></script>
    <link href="style/css/jquery.alerts.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
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
        <div class="content box">
            <h1 class="title">Contact</h1>
            <div class="map">
                <img src="Images/contact_us.jpg" style="border: 2px solid;" />
            </div>
            <h3>Feel Free to Drop Us a Line</h3>
            <div class="download-box" runat="server" visible="false" id="dvstatus">
                <asp:Label ID="lblstatus" runat="server"></asp:Label>
            </div>
            <p>We promise never to share, trade, sell, deliver, reveal, publicize, or market your email address in any way, shape, or form.</p>
            <div class="form-container">
                <div class="forms" style="margin-bottom: 20px;">
                    <fieldset>
                        <ol>
                            <li class="form-row text-input-row">
                                <label>Name</label>
                                <asp:TextBox ID="txtname" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                            </li>
                            <li class="form-row text-input-row">
                                <label>Email</label>
                                <asp:TextBox ID="txtemail" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                            </li>
                            <li class="form-row text-input-row">
                                <label>Problem?</label>
                                <asp:DropDownList ID="ddlproblem" CssClass="searchbox" runat="server"></asp:DropDownList>
                            </li>
                            <li class="form-row text-area-row">
                                <label>Message</label>
                                <%--<asp:TextBox ID="txtmessage" CssClass="text-area required Commnettext" runat="server"></asp:TextBox>--%>
                                <asp:TextBox ID="txtmessage" CssClass="text-area required Commnettext" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </li>
                            <li class="button-row">
                                <asp:Button ID="btnsubmitcomment" OnClientClick="return validatecontact();" class="btn-submit button" runat="server" OnClick="btnsubmit_Click" Text="Submit" />
                            </li>
                        </ol>
                    </fieldset>
                </div>
            </div>
        </div>

        <div class="sidebar box">
            <div class="sidebox widget">
                <h3 class="widget-title">Connect with us</h3>
                <p>Like, Comment, share and connect with us , you can find us on</p>
                <p>
                    <span class="lite1">Facebook:</span>&nbsp; <a href="<%=FacebookLink%>" target="_blank"><u><%=FacebookLink%></u></a><br/>
                    <span class="lite1">You-Tube:</span>&nbsp;<a href="<%=YoutubeLink%>" target="_blank"><u>Youtube Channel</u></a><br/>
                    <span class="lite1">Twitter:</span>&nbsp; <a href="<%=TwitterLink%>" target="_blank"><u><%=TwitterLink%></u></a>
                    <%--<span class="lite1">E-mail:</span>&nbsp; <a href="mailto:admin@puravam.com?Subject=Contact%20Us" target="_blank"><u>admin@puravam.com</u></a><br />--%>
                </p>
            </div>
            <div class="sidebox widget">
                <h3 class="widget-title">Advertise</h3>
                <div>
                    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                    <!-- Post Page Responsive Side Menu -->
                    <ins class="adsbygoogle"
                        style="display: block"
                        data-ad-client="ca-pub-1373088425496976"
                        data-ad-slot="6569598241"
                        data-ad-format="auto"></ins>
                    <script>
                        (adsbygoogle = window.adsbygoogle || []).push({});
                    </script>
                </div>
            </div>
            <div class="sidebox widget">
                <h3 class="widget-title">Frequently Asked Questions</h3>
                <p>Comming soon..... </p>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="wrapper">
            <div style="margin-top: 10px;">
                <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                <!-- Index Page Bottom Advertise -->
                <ins class="adsbygoogle"
                    style="display: block"
                    data-ad-client="ca-pub-1373088425496976"
                    data-ad-slot="2139398647"
                    data-ad-format="auto"></ins>
                <script>
                    (adsbygoogle = window.adsbygoogle || []).push({});
                </script>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
