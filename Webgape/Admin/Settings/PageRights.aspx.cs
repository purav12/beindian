using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.ADMIN.Settings
{
    public partial class PageRights : System.Web.UI.Page
    {
        #region Declaration
        AdminRightsComponent objAdminRightComponent = null;
        DataSet dsadmin = new DataSet();
        CommonDAC comandac = new CommonDAC();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPageRightList();
            }
        }

        protected void btnUpdatePageRight_Click(object sender, EventArgs e)
        {
            AdminRightsComponent objAdmin = new AdminRightsComponent();
            if (gvAdminPageRights.Rows.Count > 0)
            {
                for (int i = 0; i < gvAdminPageRights.Rows.Count; i++)
                {
                    Label POSTID = (Label)gvAdminPageRights.Rows[i].FindControl("lblCompareAdminID");
                    CheckBox ISPOPULAR = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkPopulerListed");
                    CheckBox ISINDEXTOP = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkIndexListed");
                    CheckBox ISPAGEINDEXTOP = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkPageListed");
                    CheckBox ISCATEGORYINDEXTOP = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkCategoryListed");
                    comandac.ExecuteCommonData("UPDATE tb_post set ISPOPULAR = '" + ISPOPULAR.Checked + "' , ISINDEXTOP = '" + ISINDEXTOP.Checked + "', ISPAGEINDEXTOP = '" + ISPAGEINDEXTOP.Checked + "', ISCATEGORYINDEXTOP = '" + ISCATEGORYINDEXTOP.Checked + "' where POSTID = "+ POSTID.Text + " ");
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "RightsInserted", "jAlert('Updated Successfully.','Message');", true);
            }
        }

        protected void gvAdminPageRights_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminPageRights.PageIndex = e.NewPageIndex;
            GetPageRightList();
        }
        private void GetPageRightList()
        {
            objAdminRightComponent = new AdminRightsComponent();
            DataSet dsPageRights = null;
            dsPageRights = comandac.GetCommonDataSet(txtquery.Text.Trim());
            if (dsPageRights != null && dsPageRights.Tables.Count > 0 && dsPageRights.Tables[0].Rows.Count > 0)
            {
                gvAdminPageRights.DataSource = dsPageRights;
            }
            else
            {
                gvAdminPageRights.DataSource = null;
            }
            gvAdminPageRights.DataBind();
        }
    }
}