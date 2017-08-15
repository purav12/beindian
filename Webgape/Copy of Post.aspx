<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="WebReminds.Post" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript' src="/style/js/comment-reply.js"></script>
    <script type='text/javascript' src="/style/js/zeta_slider.js"></script>
    <script src="/style/js/jquery.easing.1.3.min.js"></script>
    <script src="/style/js/jquery.slickforms.js"></script>
    <script src="/style/js/jquery.flexslider-min.js"></script>
    <script src="/style/js/jquery.tools.min.js"></script>
    <script src="/style/js/scriptsLatest.js"></script>
    <script src="/style/js/jquery-alerts.js"></script>
    <link href="/style/css/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript">
        function setcommentid(CommentId) {
            document.getElementById('ContentPlaceHolder1_hdnmaincomment').value = CommentId;
        }
        function resetcommentid() {
            document.getElementById('ContentPlaceHolder1_hdnmaincomment').value = '';
        }

        function ScrollToComment() {
            $('html, body').animate({ scrollTop: $('#comment-wrapper').offset().top }, 'slow');
            return false;
        }
        function validate() {
            if (document.getElementById("ContentPlaceHolder1_txtsubscribe") != null && document.getElementById("ContentPlaceHolder1_txtsubscribe").value.replace(/^\s+|\s+$/g, '') == '') {
                jAlert('Please Enter Email Address', 'Message', 'ContentPlaceHolder1_txtsearch');
                document.getElementById("ContentPlaceHolder1_txtsubscribe").focus();
                return false;

            }
            else if (document.getElementById("ContentPlaceHolder1_txtsubscribe") != null && document.getElementById("ContentPlaceHolder1_txtsubscribe").value.replace(/^\s+|\s+$/g, '') != '' && !checkemail1(document.getElementById("ContentPlaceHolder1_txtsubscribe").value.replace(/^\s+|\s+$/g, ''))) {
                jAlert('Please enter valid Email.', 'Message', 'ContentPlaceHolder1_txtsearch');
                document.getElementById("ContentPlaceHolder1_txtsubscribe").focus();
                return false;
            }
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

    </script>
    <style type="text/css">
        .Repeater, .Repeater td, .Repeater td {
            border: 1px solid #ccc;
        }

            .Repeater td {
                background-color: #eee !important;
            }

            .Repeater th {
                background-color: #6C6C6C !important;
                color: White;
                font-size: 10pt;
                line-height: 200%;
            }

            .Repeater span {
                color: black;
                font-size: 10pt;
                line-height: 200%;
            }

        .page_enabled, .page_disabled {
            display: inline-block;
            height: 20px;
            min-width: 20px;
            line-height: 20px;
            text-align: center;
            text-decoration: none;
            border: 1px solid #ccc;
        }

        .page_enabled {
            background-color: #eee;
            color: #000;
        }

        .page_disabled {
            background-color: #000000;
            color: #fff !important;
        }

        .Commnettext {
            border: 1px dotted #65816E;
        }
    </style>
    <style type="text/css">
        ul.tabs li a.current,
        .panes {
            background-color: rgba(0, 0, 0, 0.25);
        }

        ul.tabs li a,
        .toggle h4.title.active {
            background-color: rgba(0, 0, 0, 0.40);
        }

        ul.tabs li a {
            font-weight: bold;
        }

        ul.tabs li a {
            font-family: Open Sans Condensed;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="wrapper">
        <div id="navigation">
            <div class="breadcum">
                &nbsp;<asp:Literal ID="ltBreadcrmbs" runat="server"></asp:Literal>
                <div style="float: right; padding-right: 5px;" class="intro">
                    <a>
                        <span style="float: left" class="meta-nav-prev">
                            <img src="/Images/visitor.png" style="padding-top:5px" />
                        </span>
                        <span style="float: left" class="meta-nav-prev">&nbsp;Visitor :
                            <asp:Label ID="lblvisitor" runat="server"></asp:Label></span>
                    </a>
                </div>
            </div>
        </div>
        <div class="content">
            <div id="zetaSlider" runat="server" visible="false" class="zetaSlider">
                <div class="zetaHolder box" runat="server" id="divimgtype" style="height: 440px;">
                    <div class="zetaWrapper">
                        <div class="zetaEmpty" style="overflow: hidden; width: 5712px; left: 0px;">
                            <%--  <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>--%>
                            <%-- <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>
                            <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>--%>
                            <asp:Literal ID="ltBindMoreImage" runat="Server"></asp:Literal>
                            <%--  <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>
                            <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>
                            <div class="zetaImageBox frame" style="display: block;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>
                            <div class="zetaImageBox frame" style="display: block; margin-right: 18px;">
                                <img src='/style/images/art/test.jpg' width="800px" height="400px" alt='' />
                            </div>
                            <div class="zetaImageBox frame" style="display: block;">
                                <img src='http://themes.iki-bir.com/obscura-wp/wp-content/uploads/s7-full.jpg' width="800px" height="400px" alt='' />
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div class="zetaControls">
                    <a href="#" class="zetaBtnPrev">Previous</a>
                    <a href="#" class="zetaBtnNext">Next</a>
                </div>
                <div class="zetaWarning" style="">
                    <div class="navigate">Navigate by</div>
                    <div class="drag">Dragging</div>
                    <div class="arrow">Arrows</div>
                    <div class="keys">Keyboard</div>
                    <div class="clear"></div>
                </div>
            </div>
            <div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#zetaSlider').zetaSlider();
                    });
                </script>
            </div>
            <div class="post format-image box" >
                <div class="details" >
                    <span class="icon-user"><a style="color: #ffffff; font-size: larger;" href="#">
                        <asp:Literal ID="ltradminname" runat="server"></asp:Literal>
                    </a></span>
                    <span class="icon-image" style="margin-left: 10px">
                        <asp:Literal ID="ltrdatetime" runat="server"></asp:Literal></span>

                    <div class="img-left" style="float: right; margin-top: 3px;">
                        <script src="https://apis.google.com/js/platform.js"></script>
                        <div class="g-ytsubscribe" data-channel="Webreminds" data-layout="default" data-count="default"></div>
                    </div>
                    <div class="img-left" style="float: right; width: 50px; margin-top: 5px; margin-right: 17px;">
                        <g:plusone size="medium"></g:plusone>
                    </div>
                    <div class="img-left" style="float: right; width: 85px; margin-top: 5px;">
                        <a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal"
                            data-text="<%=PostName %>">Tweet</a>
                    </div>
                    <div class="img-left" style="float: right; margin-right: 5px;">
                        <div id="fb-root"></div>
                        <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
}
                                (document, 'script', 'facebook-jssdk'));
                        </script>
                        <div class="fb-share-button" data-href='<%=Request.RawUrl %>' data-type="button_count">
                        </div>


                    </div>
                </div>

                <%--for video--%>
                <div class="video frame" visible="false" runat="server" id="divvidtype">
                    <%--<iframe src="//player.vimeo.com/video/108883478?color=a89767" width="800" height="400" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>--%>

                    <iframe width="800" height="400" src="//www.youtube.com/embed/WuTfJHnz5kU" frameborder="0" allowfullscreen></iframe>
                </div>
                <%--for video--%>
                <h1 class="title">
                    <asp:Literal ID="ltrposttitle" runat="server"></asp:Literal>
                </h1>
                <asp:Literal ID="ltrpostdescription" runat="server"></asp:Literal>
                <div class="post-nav">
                    <span class="nav-prev"><a href="#">&larr; Rock Paper Scissors Lizard Spock</a></span>
                    <span class="nav-next"><a href="#">Charming Winter &rarr;</a></span>
                    <div class="clear"></div>
                </div>
            </div>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <div id="comment-wrapper" class="box">
                        <div style="float: right">
                            <asp:Repeater ID="rptPagertop" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPagetop" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div id="comments">
                            <h3 id="comments-title">
                                <asp:Label ID="lblcommentcount" runat="server" Text="Label"></asp:Label>
                                Responses to "<asp:Label ID="lblposttitle" runat="server" Text=""></asp:Label>"    </h3>
                            <ol id="singlecomments" class="commentlist">
                                <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                            </ol>
                        </div>
                        <div style="float: right">
                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div id="comment-form" style="margin-top: 10px" class="comment-form">
                            <div id="respond">
                                <h3 id="reply-title">Submit a comment <small><a rel="nofollow" id="cancel-comment-reply-link" href="/post/#respond" onclick="setcommentid(1)" style="display: none;">Cancel reply</a></small></h3>
                                <div style="margin-bottom: 20px;" id="commentform">
                                    <p class="comment-notes">Your email address will not be published. Required fields are marked <span class="required">*</span></p>
                                    <p class="comment-form-author">
                                        <label for="author">Name</label>
                                        <span class="required">*</span>
                                        <asp:TextBox ID="txtname" CssClass="Commnettext" runat="server"></asp:TextBox>
                                    </p>
                                    <p class="comment-form-email">
                                        <label for="email">Email</label>
                                        <span class="required">*</span>
                                        <asp:TextBox ID="txtemail" CssClass="Commnettext" runat="server"></asp:TextBox>
                                    </p>
                                    <p class="comment-form-comment">
                                        <label for="comment">Comment</label>
                                        <asp:TextBox ID="txtcomment" CssClass="Commnettext" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </p>
                                    <p class="form-submit">
                                        <asp:Button ID="btnsubmitcomment" class="button" runat="server" Text="Post Comment" OnClick="btnsubmitcomment_Click" />
                                        <input type='hidden' name='comment_post_ID' value='13' id='comment_post_ID' />
                                        <input type='hidden' name='comment_parent' id='comment_parent' value='0' />
                                    </p>
                                </div>
                            </div>
                            <!-- #respond -->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="sidebar box">
            <div class="sidebox widget">
                <!-- the tabs -->
                <ul class="tabs">
                    <li><a class="" href="#">Related Post</a></li>
                    <li><a class="" href="#">Popular Post</a></li>
                </ul>
                <!-- tab "panes" -->
                <div class="panes">
                    <div class="pane">
                        <ul class="post-list">
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s1.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Charming Winter Populer</a></h6>
                                    <em>28th Sep 2045</em>
                                </div>
                            </li>
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s2.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Trickling Stream</a></h6>
                                    <em>5th Sep 2045</em>
                                </div>
                            </li>
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s3.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Morning Glory</a></h6>
                                    <em>26th Sep 2045</em>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="pane">
                        <ul class="post-list">
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s1.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Charming Winter</a></h6>
                                    <em>28th Sep 2045</em>
                                </div>
                            </li>
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s2.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Trickling Stream</a></h6>
                                    <em>5th Sep 2045</em>
                                </div>
                            </li>
                            <li>
                                <div class="frame">
                                    <a href="#">
                                        <img src="/style/images/art/s3.jpg" alt=""></a>
                                </div>
                                <div class="meta">
                                    <h6><a href="#">Morning Glory</a></h6>
                                    <em>26th Sep 2045</em>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="clear"></div>
            </div>

            <div class="sidebox widget">
                <h3 class="widget-title">Advertise</h3>
                <div>Advertise Here. </div>
            </div>

            <div class="sidebox widget">
                <h3 class="widget-title">Subscribe</h3>
                <div>
                    <asp:TextBox ID="txtsubscribe" class="searchbox" placeholder="Subscribe For This Post" onblur="this.placeholder='Subscribe For This Post'" onfocus="this.placeholder=''" runat="server"></asp:TextBox>
                    <asp:Button ID="btnsubscribe" runat="server" class="button" OnClientClick="return validate();" Style="opacity: 1;" Text="subscribe" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="sidebox widget">
                <div class="clear"></div>
            </div>
        </div>
        <div class="clear"></div>
        <div style="display: none;">
            <input type="hidden" id="hdnmaincomment" runat="server" />
        </div>
    </div>

    <script src="/style/js/scripts.js"></script>
</asp:Content>
