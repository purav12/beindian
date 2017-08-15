using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        AdminComponent Admincomponent = new AdminComponent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblerror.Visible = false;
            }
        }

        protected void btnforgot_Click(object sender, EventArgs e)
        {
            DataSet dsAdmin = new DataSet();
            dsAdmin = Admincomponent.GetForgotpassinfo(txtuname.Text.Trim());
            if (dsAdmin != null && dsAdmin.Tables.Count > 0 && dsAdmin.Tables[0].Rows.Count > 0)
            {
                SendMail();
            }
            else
            {
                lblerror.Text = "Whoops! We didn't recognise your username or Email. Please try again.";
                lblerror.Visible = true;
            }
        }

        public void SendMail()
        {
            //CommonOperations.SendMail(ltEmailTo.Text, MailSubject, MailBody, Request.UserHostAddress.ToString(), true, null);
        }
    }
}