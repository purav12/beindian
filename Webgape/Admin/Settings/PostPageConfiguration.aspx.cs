using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Settings
{
    public partial class PostPageConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.HeadTitle("BeIndian - Post Page Configuration", "BeIndian.in - Post Page Configuration, Admin Post Page Configuration", "BeIndian.in - Post Page Configuration");
            }
        }
    }
}