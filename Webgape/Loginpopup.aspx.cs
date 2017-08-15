using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webgape
{
    public partial class Loginpopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }

        protected void btnregister_Click(object sender, EventArgs e)
        {
            string js = "";
            js += "alert('yes'); disablePopupmaster();";
            js += "window.close();";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", js, true);
            //Server.Transfer("/SignUp.aspx");
        }
    }
}