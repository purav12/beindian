using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRemindsClass;

namespace WebReminds
{
    public partial class MailTest : System.Web.UI.Page
    {
        CommonOperations Commop = new CommonOperations();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsendmail_Click(object sender, EventArgs e)
        {
            CommonOperations.SendTestMailWeb(MailUserName.Text, MailPassword.Text, MailHost.Text, MailFrom.Text, MailFromDisplay.Text, MailTo.Text, Port.Text, MailSubject.Text, MailBody.Text, "", true, null);
            
        }
    }
}