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
    public partial class DataBackup : System.Web.UI.Page
    {
        CommonDAC commandac = new CommonDAC();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            string DatabaseBackupPath = "";
            DatabaseBackupPath = Convert.ToString(commandac.GetScalarCommonData("SELECT ConfigValue FROM dbo.tb_AppConfig WHERE ConfigName='DatabaseBackupPath'"));

            String Filename = DatabaseBackupPath + @"\" + "Webgape_" + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + "_" + Convert.ToString(DateTime.Now.Millisecond) + ".bak";
            object i = commandac.ExecuteDatabaseBackup(Filename);

            if (Convert.ToInt32(i) != 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Database backup has been taken successfully', 'Message');});", true);

            }

        }
    }
}