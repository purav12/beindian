<%@ Page Title="" Language="C#" MasterPageFile="~/ChipsSeller/ChipsSeller.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Webgape.ChipsSeller.Profile" %>

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
            <div class="post format-image box">
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

                <h1 class="title">Update Profile
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
                <div class="download-box" runat="server" visible="false" id="divstatus">
                    <asp:Label ID="lblstatusd" runat="server"></asp:Label>
                </div>
                <div id="useronly" visible="false" runat="server">
                    <%--<h2 class="title">Update Details
                    </h2>--%>
                    <div class="form-container">
                        <div class="forms" style="margin-bottom: 20px;">
                            <fieldset>
                                <ol>
                                    <li class="form-row text-input-row">
                                        <label>Contact No</label>
                                        <asp:TextBox ID="txtmobno" Width="50%" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="form-row text-input-row">
                                        <label>Email Id</label>
                                        <asp:TextBox ID="txtemailid" Width="50%" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="form-row text-input-row">
                                        <label>Country</label>
                                        <asp:DropDownList ID="ddlcountry" CssClass="searchbox" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" runat="server" AutoPostBack="True">
                                            <asp:ListItem Selected="selected">India</asp:ListItem>
                                            <asp:ListItem>-Other-</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="form-row text-input-row" runat="server" id="othercon">
                                        <label>Location</label>
                                        <asp:TextBox ID="txtlocation" Width="50%" placeholder="Country / City" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="form-row text-input-row" runat="server" id="liindian">
                                        <label>Location</label>
                                        <asp:DropDownList ID="drpcitylist" CssClass="searchbox" runat="server">
                                            <asp:ListItem Selected="selected">-Select-</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Top Metropolitan Cities-</asp:ListItem>
                                            <asp:ListItem>Ahmedabad</asp:ListItem>
                                            <asp:ListItem>Bengaluru/Bangalore</asp:ListItem>
                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                            <asp:ListItem>Chennai</asp:ListItem>
                                            <asp:ListItem>Delhi</asp:ListItem>
                                            <asp:ListItem>Gurgaon</asp:ListItem>
                                            <asp:ListItem>Hyderabad/Secunderabad</asp:ListItem>
                                            <asp:ListItem>Kolkatta</asp:ListItem>
                                            <asp:ListItem>Mumbai</asp:ListItem>
                                            <asp:ListItem>Noida</asp:ListItem>
                                            <asp:ListItem>Pune</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Andhra Pradesh-</asp:ListItem>
                                            <asp:ListItem>Anantapur</asp:ListItem>
                                            <asp:ListItem>Guntakal</asp:ListItem>
                                            <asp:ListItem>Guntur</asp:ListItem>
                                            <asp:ListItem>Hyderabad/Secunderabad</asp:ListItem>
                                            <asp:ListItem>kakinada</asp:ListItem>
                                            <asp:ListItem>kurnool</asp:ListItem>
                                            <asp:ListItem>Nellore</asp:ListItem>
                                            <asp:ListItem>Nizamabad</asp:ListItem>
                                            <asp:ListItem>Rajahmundry</asp:ListItem>
                                            <asp:ListItem>Tirupati</asp:ListItem>
                                            <asp:ListItem>Vijayawada</asp:ListItem>
                                            <asp:ListItem>Visakhapatnam</asp:ListItem>
                                            <asp:ListItem>Warangal</asp:ListItem>
                                            <asp:ListItem>Andra Pradesh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Arunachal Pradesh-</asp:ListItem>
                                            <asp:ListItem>Itanagar</asp:ListItem>
                                            <asp:ListItem>Arunachal Pradesh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Assam-</asp:ListItem>
                                            <asp:ListItem>Guwahati</asp:ListItem>
                                            <asp:ListItem>Silchar</asp:ListItem>
                                            <asp:ListItem>Assam-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Bihar-</asp:ListItem>
                                            <asp:ListItem>Bhagalpur</asp:ListItem>
                                            <asp:ListItem>Patna</asp:ListItem>
                                            <asp:ListItem>Bihar-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Chhattisgarh-</asp:ListItem>
                                            <asp:ListItem>Bhillai</asp:ListItem>
                                            <asp:ListItem>Bilaspur</asp:ListItem>
                                            <asp:ListItem>Raipur</asp:ListItem>
                                            <asp:ListItem>Chhattisgarh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Goa-</asp:ListItem>
                                            <asp:ListItem>Panjim/Panaji</asp:ListItem>
                                            <asp:ListItem>Vasco Da Gama</asp:ListItem>
                                            <asp:ListItem>Goa-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Gujarat-</asp:ListItem>
                                            <asp:ListItem>Ahmedabad</asp:ListItem>
                                            <asp:ListItem>Anand</asp:ListItem>
                                            <asp:ListItem>Ankleshwar</asp:ListItem>
                                            <asp:ListItem>Bharuch</asp:ListItem>
                                            <asp:ListItem>Bhavnagar</asp:ListItem>
                                            <asp:ListItem>Bhuj</asp:ListItem>
                                            <asp:ListItem>Gandhinagar</asp:ListItem>
                                            <asp:ListItem>Gir</asp:ListItem>
                                            <asp:ListItem>Jamnagar</asp:ListItem>
                                            <asp:ListItem>Kandla</asp:ListItem>
                                            <asp:ListItem>Porbandar</asp:ListItem>
                                            <asp:ListItem>Rajkot</asp:ListItem>
                                            <asp:ListItem>Surat</asp:ListItem>
                                            <asp:ListItem>Vadodara/Baroda</asp:ListItem>
                                            <asp:ListItem>Valsad</asp:ListItem>
                                            <asp:ListItem>Vapi</asp:ListItem>
                                            <asp:ListItem>Gujarat-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Haryana-</asp:ListItem>
                                            <asp:ListItem>Ambala</asp:ListItem>
                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                            <asp:ListItem>Faridabad</asp:ListItem>
                                            <asp:ListItem>Gurgaon</asp:ListItem>
                                            <asp:ListItem>Hisar</asp:ListItem>
                                            <asp:ListItem>Karnal</asp:ListItem>
                                            <asp:ListItem>Kurukshetra</asp:ListItem>
                                            <asp:ListItem>Panipat</asp:ListItem>
                                            <asp:ListItem>Rohtak</asp:ListItem>
                                            <asp:ListItem>Haryana-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Himachal Pradesh-</asp:ListItem>
                                            <asp:ListItem>Dalhousie</asp:ListItem>
                                            <asp:ListItem>Dharmasala</asp:ListItem>
                                            <asp:ListItem>Kulu/Manali</asp:ListItem>
                                            <asp:ListItem>Shimla</asp:ListItem>
                                            <asp:ListItem>Himachal Pradesh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Jammu and Kashmir-</asp:ListItem>
                                            <asp:ListItem>Jammu</asp:ListItem>
                                            <asp:ListItem>Srinagar</asp:ListItem>
                                            <asp:ListItem>Jammu and Kashmir-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Jharkhand-</asp:ListItem>
                                            <asp:ListItem>Bokaro</asp:ListItem>
                                            <asp:ListItem>Dhanbad</asp:ListItem>
                                            <asp:ListItem>Jamshedpur</asp:ListItem>
                                            <asp:ListItem>Ranchi</asp:ListItem>
                                            <asp:ListItem>Jharkhand-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Karnataka-</asp:ListItem>
                                            <asp:ListItem>Bengaluru/Bangalore</asp:ListItem>
                                            <asp:ListItem>Belgaum</asp:ListItem>
                                            <asp:ListItem>Bellary</asp:ListItem>
                                            <asp:ListItem>Bidar</asp:ListItem>
                                            <asp:ListItem>Dharwad</asp:ListItem>
                                            <asp:ListItem>Gulbarga</asp:ListItem>
                                            <asp:ListItem>Hubli</asp:ListItem>
                                            <asp:ListItem>Kolar</asp:ListItem>
                                            <asp:ListItem>Mangalore</asp:ListItem>
                                            <asp:ListItem>Mysoru/Mysore</asp:ListItem>
                                            <asp:ListItem>Karnataka-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Kerala-</asp:ListItem>
                                            <asp:ListItem>Calicut</asp:ListItem>
                                            <asp:ListItem>Cochin</asp:ListItem>
                                            <asp:ListItem>Ernakulam</asp:ListItem>
                                            <asp:ListItem>Kannur</asp:ListItem>
                                            <asp:ListItem>Kochi</asp:ListItem>
                                            <asp:ListItem>Kollam</asp:ListItem>
                                            <asp:ListItem>Kottayam</asp:ListItem>
                                            <asp:ListItem>Kozhikode</asp:ListItem>
                                            <asp:ListItem>Palakkad</asp:ListItem>
                                            <asp:ListItem>Palghat</asp:ListItem>
                                            <asp:ListItem>Thrissur</asp:ListItem>
                                            <asp:ListItem>Trivandrum</asp:ListItem>
                                            <asp:ListItem>Kerela-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Madhya Pradesh-</asp:ListItem>
                                            <asp:ListItem>Bhopal</asp:ListItem>
                                            <asp:ListItem>Gwalior</asp:ListItem>
                                            <asp:ListItem>Indore</asp:ListItem>
                                            <asp:ListItem>Jabalpur</asp:ListItem>
                                            <asp:ListItem>Ujjain</asp:ListItem>
                                            <asp:ListItem>Madhya Pradesh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Maharashtra-</asp:ListItem>
                                            <asp:ListItem>Ahmednagar</asp:ListItem>
                                            <asp:ListItem>Aurangabad</asp:ListItem>
                                            <asp:ListItem>Jalgaon</asp:ListItem>
                                            <asp:ListItem>Kolhapur</asp:ListItem>
                                            <asp:ListItem>Mumbai</asp:ListItem>
                                            <asp:ListItem>Mumbai Suburbs</asp:ListItem>
                                            <asp:ListItem>Nagpur</asp:ListItem>
                                            <asp:ListItem>Nasik</asp:ListItem>
                                            <asp:ListItem>Navi Mumbai</asp:ListItem>
                                            <asp:ListItem>Pune</asp:ListItem>
                                            <asp:ListItem>Solapur</asp:ListItem>
                                            <asp:ListItem>Maharashtra-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Manipur-</asp:ListItem>
                                            <asp:ListItem>Imphal</asp:ListItem>
                                            <asp:ListItem>Manipur-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Meghalaya-</asp:ListItem>
                                            <asp:ListItem>Shillong</asp:ListItem>
                                            <asp:ListItem>Meghalaya-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Mizoram-</asp:ListItem>
                                            <asp:ListItem>Aizawal</asp:ListItem>
                                            <asp:ListItem>Mizoram-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Nagaland-</asp:ListItem>
                                            <asp:ListItem>Dimapur</asp:ListItem>
                                            <asp:ListItem>Nagaland-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Orissa-</asp:ListItem>
                                            <asp:ListItem>Bhubaneshwar</asp:ListItem>
                                            <asp:ListItem>Cuttak</asp:ListItem>
                                            <asp:ListItem>Paradeep</asp:ListItem>
                                            <asp:ListItem>Puri</asp:ListItem>
                                            <asp:ListItem>Rourkela</asp:ListItem>
                                            <asp:ListItem>Orissa-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Punjab-</asp:ListItem>
                                            <asp:ListItem>Amritsar</asp:ListItem>
                                            <asp:ListItem>Bathinda</asp:ListItem>
                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                            <asp:ListItem>Jalandhar</asp:ListItem>
                                            <asp:ListItem>Ludhiana</asp:ListItem>
                                            <asp:ListItem>Mohali</asp:ListItem>
                                            <asp:ListItem>Pathankot</asp:ListItem>
                                            <asp:ListItem>Patiala</asp:ListItem>
                                            <asp:ListItem>Punjab-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Rajasthan-</asp:ListItem>
                                            <asp:ListItem>Ajmer</asp:ListItem>
                                            <asp:ListItem>Jaipur</asp:ListItem>
                                            <asp:ListItem>Jaisalmer</asp:ListItem>
                                            <asp:ListItem>Jodhpur</asp:ListItem>
                                            <asp:ListItem>Kota</asp:ListItem>
                                            <asp:ListItem>Udaipur</asp:ListItem>
                                            <asp:ListItem>Rajasthan-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Sikkim-</asp:ListItem>
                                            <asp:ListItem>Gangtok</asp:ListItem>
                                            <asp:ListItem>Sikkim-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Tamil Nadu-</asp:ListItem>
                                            <asp:ListItem>Chennai</asp:ListItem>
                                            <asp:ListItem>Coimbatore</asp:ListItem>
                                            <asp:ListItem>Cuddalore</asp:ListItem>
                                            <asp:ListItem>Erode</asp:ListItem>
                                            <asp:ListItem>Hosur</asp:ListItem>
                                            <asp:ListItem>Madurai</asp:ListItem>
                                            <asp:ListItem>Nagerkoil</asp:ListItem>
                                            <asp:ListItem>Ooty</asp:ListItem>
                                            <asp:ListItem>Salem</asp:ListItem>
                                            <asp:ListItem>Thanjavur</asp:ListItem>
                                            <asp:ListItem>Tirunalveli</asp:ListItem>
                                            <asp:ListItem>Trichy</asp:ListItem>
                                            <asp:ListItem>Tuticorin</asp:ListItem>
                                            <asp:ListItem>Vellore</asp:ListItem>
                                            <asp:ListItem>Tamil Nadu-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Tripura-</asp:ListItem>
                                            <asp:ListItem>Agartala</asp:ListItem>
                                            <asp:ListItem>Tripura-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Union Territories-</asp:ListItem>
                                            <asp:ListItem>Chandigarh</asp:ListItem>
                                            <asp:ListItem>Dadra & Nagar Haveli-Silvassa</asp:ListItem>
                                            <asp:ListItem>Daman & Diu</asp:ListItem>
                                            <asp:ListItem>Delhi</asp:ListItem>
                                            <asp:ListItem>Pondichery</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Uttar Pradesh-</asp:ListItem>
                                            <asp:ListItem>Agra</asp:ListItem>
                                            <asp:ListItem>Aligarh</asp:ListItem>
                                            <asp:ListItem>Allahabad</asp:ListItem>
                                            <asp:ListItem>Bareilly</asp:ListItem>
                                            <asp:ListItem>Faizabad</asp:ListItem>
                                            <asp:ListItem>Ghaziabad</asp:ListItem>
                                            <asp:ListItem>Gorakhpur</asp:ListItem>
                                            <asp:ListItem>Kanpur</asp:ListItem>
                                            <asp:ListItem>Lucknow</asp:ListItem>
                                            <asp:ListItem>Mathura</asp:ListItem>
                                            <asp:ListItem>Meerut</asp:ListItem>
                                            <asp:ListItem>Moradabad</asp:ListItem>
                                            <asp:ListItem>Noida</asp:ListItem>
                                            <asp:ListItem>Varanasi/Banaras</asp:ListItem>
                                            <asp:ListItem>Uttar Pradesh-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Uttaranchal-</asp:ListItem>
                                            <asp:ListItem>Dehradun</asp:ListItem>
                                            <asp:ListItem>Roorkee</asp:ListItem>
                                            <asp:ListItem>Uttaranchal-Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-West Bengal-</asp:ListItem>
                                            <asp:ListItem>Asansol</asp:ListItem>
                                            <asp:ListItem>Durgapur</asp:ListItem>
                                            <asp:ListItem>Haldia</asp:ListItem>
                                            <asp:ListItem>Kharagpur</asp:ListItem>
                                            <asp:ListItem>Kolkatta</asp:ListItem>
                                            <asp:ListItem>Siliguri</asp:ListItem>
                                            <asp:ListItem>West Bengal - Other</asp:ListItem>
                                            <asp:ListItem disabled="disabled">-Other-</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="button-row">
                                        <asp:Button ID="Button2" class="btn-submit button" runat="server" OnClick="btnprofile_Click" Text="Update Profile" />
                                    </li>
                                </ol>
                            </fieldset>
                        </div>
                    </div>


                </div>

                <div id="dvpin" visible="false" runat="server">
                    <h1 class="title">Update PIN
                    </h1>
                    <div class="form-container">
                        <div class="forms" style="margin-bottom: 20px;">
                            <fieldset>
                                <ol>
                                    <li class="form-row text-input-row">
                                        <label>New PIN</label>
                                        <asp:TextBox ID="txtnewpin" TextMode="Password" Width="50%" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="form-row text-input-row" runat="server" id="Li1">
                                        <label>Confirm New PIN</label>
                                        <asp:TextBox ID="txtconfirmnewpin" TextMode="Password" Width="50%" CssClass="text-input required Commnettext" runat="server"></asp:TextBox>
                                    </li>

                                    <li class="button-row">
                                        <asp:Button ID="btnchangepin" class="btn-submit button" runat="server" OnClick="btnpin_Click" Text="Update PIN" />
                                    </li>
                                </ol>
                            </fieldset>
                        </div>
                    </div>


                    <asp:Label ID="lblTagLine" Visible="false" runat="server" Text="Keep visiting thanks..."></asp:Label>
                    <br />
                    <br />

                </div>
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
