using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Rewriter : System.Web.UI.Page
    {
        public string YoutubeLink = string.Empty;
        public string TwitterLink = string.Empty;
        public string FacebookLink = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/");
            BindLinks();
        }

        private void BindLinks()
        {
            YoutubeLink = AppLogic.AppConfigs("YoutubeLink").ToString();
            TwitterLink = AppLogic.AppConfigs("TwitterLink").ToString();
            FacebookLink = AppLogic.AppConfigs("FacebookLink").ToString();
        }
    }
}