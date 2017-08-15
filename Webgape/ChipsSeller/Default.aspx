<%@ Page Title="" Language="C#" MasterPageFile="~/ChipsSeller/ChipsSeller.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Webgape.ChipsSeller.Default" %>

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
        function validatecomment() {
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
            if ((document.getElementById('ContentPlaceHolder1_txtcomment').value) == '') {
                jAlert('Please enter Comment', 'Message', 'ContentPlaceHolder1_txtcomment');
                return false;
            }
            if ((document.getElementById('ContentPlaceHolder1_txtans').value) != '6') {
                jAlert('Please enter correct answer', 'Message', 'ContentPlaceHolder1_txtans');
                return false;
            }
        }
        function validatecontact() {
            if ((document.getElementById('ContentPlaceHolder1_txtpin').value) == '') {
                jAlert('Please Enter PIN', 'Message', 'ContentPlaceHolder1_txtpin');
                return false;
            }
        }
    </script>
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
                jAlert('Please enter Email Address', 'Message', 'ContentPlaceHolder1_txtsearch');
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
    <link href="/style/css/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>

    <div class="wrapper">
        <div id="navigation">
            <div class="breadcum">
                &nbsp;
                
                <a title='Home' href='/'>Home </a>&rarr; 
                    <a href='/ChipsSeller/Default.aspx' title='Chips Seller'>Chips Seller</a>

                <div style="float: right; padding-right: 5px;" class="intro">
                    <a href="/Visitor.aspx?EId=0&Ename=ChipsSeller">
                        <span style="float: left" class="meta-nav-prev">
                            <img src="/Images/visitor.png" style="padding-top: 5px" />
                        </span>
                        <span style="float: left" class="meta-nav-prev">&nbsp;Views :
                            <asp:Label ID="lblvisitor" runat="server"></asp:Label></span>
                    </a>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="post format-image box" style="overflow: inherit;">
                <div class="details" style="margin-top: -20px; margin-bottom: 20px;">
                    <span class="icon-user"><a style="color: #ffffff; font-size: larger;" href="#">
                        <asp:Literal ID="ltradminname" runat="server"></asp:Literal>
                    </a></span>
                    <span class="icon-image" style="margin-left: 10px">
                        <asp:Literal ID="ltrdatetime" runat="server"></asp:Literal></span>

                    <div class="img-left" style="float: right; margin-top: 3px;">
                        <script src="https://apis.google.com/js/platform.js"></script>
                        <div class="g-ytsubscribe" data-channelid="<%=strChannelId%>" data-layout="default"></div>
                    </div>
                    <div class="img-left" style="float: right; width: 46px; margin-top: 5px; margin-right: 17px;">
                        <g:plusone size="medium"></g:plusone>
                    </div>
                    <div class="img-left" style="float: right; width: 66px; margin-top: 5px;">
                        <a href="http://twitter.com/share" class="twitter-share-button" data-count="horizontal"
                            data-text="<%=Request.RawUrl %>">Tweet</a>
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

                <h1 class="title">Chips Seller India
                </h1>
                <asp:Label ID="lblpinstatus" runat="server" Text="[Enter your PIN if you have ever purchased chips before]"></asp:Label>


                <div class="form-container" id="divpin" runat="server">
                    <br />
                    <div class="forms" style="margin-bottom: 20px;">
                        <fieldset>
                            <ol>
                                <li class="form-row text-input-row">
                                    <label>PIN</label>
                                    <asp:TextBox ID="txtpin" TextMode="Password" Width="50%" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                </li>
                                <li class="button-row">
                                    <asp:Button ID="Button1" OnClientClick="return validatecontact();" class="btn-submit button" runat="server" OnClick="btnsubmit_Click" Text="Submit" />
                                </li>
                            </ol>
                        </fieldset>
                    </div>
                </div>

                <div id="useronly" visible="false" runat="server">
                    <asp:Label ID="lblwelcomeleft" Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="lblname" Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="lblwelcomeright" Visible="false" runat="server"></asp:Label>

                    <br />
                    <br />

                    <asp:Label ID="lblhistory" runat="server" Text="Label"></asp:Label>

                    <br />
                    <br />
                    <asp:GridView ID="grdDaily" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="mGrid" AllowPaging="True" GridLines="None" PagerStyle-CssClass="pgr"
                        AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdDaily_PageIndexChanging"
                        PageSize="10">
                        <Columns>
                            <asp:BoundField DataField="MainId" HeaderText="Order#" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Stock" HeaderText="Million" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount (INR)" />
                            <asp:BoundField DataField="CreateDate" HeaderText="Date" />
                            <asp:BoundField DataField="Rate" HeaderText="Rate" />
                            <asp:BoundField DataField="Comment" HeaderText="Comment" />
                            <asp:BoundField DataField="Balance" HeaderText="Balance" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    Total
                    <br />
                    <asp:GridView ID="grdbuysell" runat="server" CssClass="mGrid" AllowPaging="True"
                        GridLines="None" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                        AutoGenerateColumns="False">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="Type" HeaderText="Status" />
                            <asp:BoundField DataField="Total" HeaderText="Amount" />
                            <asp:TemplateField HeaderText="Chips">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Chips") %>'></asp:Label>
                                    &nbsp
                                            <asp:Label ID="Label2" runat="server" Text='Million'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AverageRate" HeaderText="AverageRate" />
                        </Columns>

                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblTagLine" Visible="false" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />

                </div>

                You can <asp:Button ID="btnupdateprofile" class="btn-submit button" runat="server" OnClick="btnupdateprofile_Click" Text="Update Profile" />
                <br />
                <asp:Literal ID="lblstatustill" runat="server"></asp:Literal>
            </div>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <div id="comment-wrapper" class="box">

                        <ul class="tabs">
                            <li><a class="" href="#">Comment</a></li>
                            <li><a class="" href="#">Facebook Comment</a></li>
                        </ul>
                        <!-- tab "panes" -->
                        <div class="panes">
                            <div class="pane">
                                <ul class="post-list">
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
                                            Responses to this Page </h3>
                                        <ol id="singlecomments" class="commentlist">
                                            <asp:Literal ID="ltrcomment" runat="server"></asp:Literal>
                                        </ol>
                                    </div>
                                    <div style="float: right; margin-bottom: 5%;">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ul>
                            </div>
                            <div class="pane">
                                <ul class="post-list">
                                    <div class="fb-comments" style="background-color: #fff;" data-href="http://BeIndian.in/chipsseller" data-numposts="5"></div>
                                    <div id="fb-comment-root"></div>

                                    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.4";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
                                </ul>
                            </div>
                        </div>
                        <div class="clear"></div>


                        <div id="comment-form" style="margin-top: 10px" class="comment-form">
                            <div id="respond">
                                <h3 id="reply-title">Submit a comment <small><a rel="nofollow" id="cancel-comment-reply-link" href="/post/#respond" onclick="setcommentid(1)" style="display: none;">Cancel reply</a></small></h3>
                                <div style="margin-bottom: 20px;" id="commentform">
                                    <p class="comment-notes">Your email address will not be published. Required fields are marked <span class="required">*</span></p>
                                    <div class="download-box" runat="server" visible="false" id="dvstatus">
                                        <asp:Label ID="lblstatus" Text="Comment Inserted Successfully" runat="server"></asp:Label>
                                    </div>
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
                                    <p class="comment-form-comment">
                                        <label for="comment">4 + 2 = </label>
                                        <asp:TextBox ID="txtans" CssClass="Commnettext" runat="server"></asp:TextBox>
                                    </p>
                                    <p class="form-submit">
                                        <asp:Button ID="btnsubmitcomment" OnClientClick="return validatecomment();" class="btn-submit button" runat="server" Text="Post Comment" OnClick="btnsubmitcomment_Click" />
                                        <input type='hidden' name='comment_post_ID' value='0' id='comment_post_ID' />
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
                </ul>
                <!-- tab "panes" -->
                <div class="panes">
                    <div class="pane">
                        <ul class="post-list">
                            <asp:Literal ID="ltrrelatedpost" runat="server"></asp:Literal>
                        </ul>
                    </div>
                </div>
                <div class="clear"></div>
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
                <h3 class="widget-title">Subscribe</h3>
                <div>
                    <asp:TextBox ID="txtsubscribe" class="searchbox" placeholder="Subscribe For This Page" onblur="this.placeholder='Subscribe For This Post'" onfocus="this.placeholder=''" runat="server"></asp:TextBox>
                    <asp:Button ID="btnsubscribe" runat="server" class="btn-submit button" OnClientClick="return validate();" Style="opacity: 1;" Text="subscribe" OnClick="btnsubscribe_Click" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="sidebox widget">
                <div class="clear"></div>
            </div>
        </div>
        <div class="clear"></div>
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
        <div class="clear"></div>
        <div style="display: none;">
            <input type="hidden" id="hdnmaincomment" runat="server" />
            <input type="hidden" id="hdnownerid" runat="server" />
        </div>
    </div>

    <script src="/style/js/scripts.js"></script>
</asp:Content>
