using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webgape.Admin.Settings
{
    public partial class ProfilePageConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.HeadTitle("BeIndian - Profile Page Configuration", "BeIndian.in - Profile Page Configuration, Admin Profile Page Configuration", "BeIndian.in - Profile Page Configuration");
            }
        }
    }
}