function ValidatePage() {
    if ((document.getElementById('ContentPlaceHolder1_txtPostTitle').value).replace(/^\s*\s*$/g, '') == '') {
        jAlert('Please Enter Post Title', 'Message', 'ContentPlaceHolder1_txtPostTitle');
        $('html, body').animate({ scrollTop: $('#ContentPlaceHolder1_txtPostTitle').offset().top }, 'slow');
        return false;
    }

    if (document.getElementById("ContentPlaceHolder1_ddlposttype") != null && document.getElementById("ContentPlaceHolder1_ddlposttype").selectedIndex == 0) {
        jAlert('Please Select Post Type', 'Message', 'ContentPlaceHolder1_ddlposttype');
        $('html, body').animate({ scrollTop: $('#ContentPlaceHolder1_ddlposttype').offset().top }, 'slow');
        return false;
    }
    return true;
}

