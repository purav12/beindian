<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPost.aspx.cs" Inherits="Webgape.UserPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(window).load(function () {
            document.getElementById("nomore").style.display = 'none';
            document.getElementById("showmore").style.display = 'block';

            flagin = 0;
            var rese = document.getElementById('ContentPlaceHolder1_hdncnt');
            rese.value = '12';
        });

        function chkHeight() {
            var windowHeight = 0;
            windowHeight = $(document).height(); //window.innerHeight;

            document.getElementById('loader').style.height = windowHeight + 'px';
            document.getElementById('loader').style.display = '';
        }

        function validate() {
            if ((document.getElementById('ContentPlaceHolder1_txtsearch').value) == '') {
                jAlert('Please Enter Value To Search', 'Message', 'ContentPlaceHolder1_txtsearch');
                return false;
            }

            if (document.getElementById('ContentPlaceHolder1_txtsearch').value.length < 3) {
                jAlert('Please Enter Atleast 3 words To Search', 'Message', 'ContentPlaceHolder1_txtsearch');
                return false;
            }
        }


        /*When Search Parameter Change*/
        function Loader() {
            flagin = 0;
            var rese = document.getElementById('ContentPlaceHolder1_hdncnt');
            rese.value = '12';
            if (document.getElementById('ContentPlaceHolder1_prepage') != null) {
                document.getElementById('ContentPlaceHolder1_prepage').style.display = '';
            }
        }

        /*Scrooling Or More Post Load */
        var flagin = 0;
        function Load() {
            var postcount = document.getElementById('ContentPlaceHolder1_postcount');
            var totalpoststodisplay = postcount.value;
            if (totalpoststodisplay == 0) {
                document.getElementById("nomore").style.display = 'block';
                document.getElementById("showmore").style.display = 'none';
                return;
            }

            var temp = document.getElementById('ContentPlaceHolder1_hdncnt');
            var Take; var Skip;
            Skip = parseInt(temp.value) + 1;
            Take = parseInt(temp.value) + 12;
            if (totalpoststodisplay < Skip) {
                document.getElementById("nomore").style.display = 'block';
                document.getElementById("showmore").style.display = 'none';
                return;
            }
            $('#divPostsLoader').html('<img src="/images/220.gif">');

            var divcount = document.getElementById('ContentPlaceHolder1_divcount');
            var newcount; newcount = parseInt(divcount.value) + 1;
            var UserId = <%= new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Request.QueryString["UserId"]) %>;
     
            if (totalpoststodisplay <= Take && flagin == 0) {
                $('#ContentPlaceHolder1_Div' + parseInt(divcount.value)).load("/scrolling.aspx?UserPost=true&UserId=" + UserId + "&skip=" + Skip + "&take=" + Take, function (response, status, xhr) {
                    if (status == "error") {
                        var msg = "Sorry but there was an error: ";
                        $("#error").html(msg + xhr.status + " " + xhr.statusText);
                    }
                    if (status == "success") {
                        $('#divPostsLoader').empty();
                    }
                });
                flagin = 1;
            }
            else {
                $('#ContentPlaceHolder1_Div' + parseInt(divcount.value)).load("/scrolling.aspx?UserPost=true&UserId=" + UserId + "&skip=" + Skip + "&take=" + Take, function (response, status, xhr) {
                    if (status == "error") {
                        var msg = "Sorry but there was an error: ";
                        $("#error").html(msg + xhr.status + " " + xhr.statusText);
                    }
                    if (status == "success") {
                        $('#divPostsLoader').empty();
                    }
                });

                $('#ContentPlaceHolder1_Div' + parseInt(divcount.value)).after("<div id='ContentPlaceHolder1_Div" + newcount + "'></div>");
            }
            var newdivcount = document.getElementById('ContentPlaceHolder1_divcount');
            newdivcount.value = newcount;

            var e = document.getElementById('ContentPlaceHolder1_hdncnt');
            e.value = Take;
        };

        //Scrolling
        //$(window).scroll(function () {
        //    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        //        Load();
        //    }
        //});

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
        <h1 class="title" style="text-align: center;">
            <asp:Literal Text="User Posts" runat="server"></asp:Literal>
        </h1>
        <div class="blog-wrap">
            <div class="blog-grid" runat="server">
                <asp:Repeater ID="RepPost" runat="server" OnItemDataBound="RepPost_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Literal ID="ltrpostData" runat="server"></asp:Literal>
                        <asp:Literal ID="ltrPostId" runat="server" Visible="false" Text='<%#Eval("PostID")%>'></asp:Literal>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <%--    <div id="divscrollpost" runat="server">
    </div>--%>
    <div id="Div1" runat="server">
    </div>


    <div class="wrapper">
        <div style="margin-left: auto; margin-right: auto; width: 120px;" id="divPostsLoader">
        </div>
    </div>

    <div class="wrapper" style="margin-top: 10px">
        <div id="navigation">
            <div class="intro" runat="server" id="bindmsg">
                <a href="javascript:someFunction()" onclick="Load();">
                    <span id="showmore" class="meta-nav-prev">Show More Posts</span>
                    <span id="nomore" class="meta-nav-prev">No More Post To Show</span>
                </a>
            </div>
            <div class="intro" runat="server" id="emptymsg" visible="false">
                <a href="javascript:someFunction()" onclick="Load();">
                    <span id="emptyrep" class="meta-nav-prev">No Post To Show For This Search Criteria </span>
                </a>
            </div>
        </div>
    </div>

    <div class="wrapper">

        <div>
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

    <div id="loader" style="position: absolute; font-family: arial; font-size: 16; left: 0px; top: 0px; background-color: #B9B9B9; opacity: 0.9; filter: alpha(opacity=70); layer-background-color: white; height: 100%; width: 100%; z-index: 1000; display: none;">
        <div style="border: 1px solid #ccc;">
            <table width="100%" style="position: fixed; top: 50%; left: 50%; margin: -50px 0 0 -100px;">
                <tr>
                    <td>
                        <div style="background: none repeat scroll 0 0 #FFFFFF !important; border: 5px solid #6A6A6C; padding: 20px; position: absolute; z-index: 999; top: 75px; border-radius: 10px;">
                            <img alt="" src="/images/713.gif" style="text-align: center; margin-left: 37px" />
                            <br />
                            <span class="text">Loading...Please wait!</span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="prepage" style="width: 100%; height: 100%; position: fixed; top: 0%; left: 0%; z-index: 2000; display: none; background: url(style/images/loaderbg.png) repeat !important; display: none;">
        <table style="width: 100%; position: fixed; top: 50%; left: 50%; margin: -50px 0 0 -100px;">
            <tr>
                <td>
                    <img src="style/images/loading.gif" />
                </td>
            </tr>
        </table>
    </div>

    <div style="display: none;">
        <input type="hidden" id="hdnpostId" runat="server" />
        <asp:HiddenField ID="hdncnt" runat="server" Value="12" />
        <asp:HiddenField ID="postcount" runat="server" Value="0" />
        <asp:HiddenField ID="divcount" runat="server" Value="1" />
    </div>
</asp:Content>
