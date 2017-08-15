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
    public partial class AdminRights : System.Web.UI.Page
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
                GetRightList();
                GetAdminList();
                GetPageRightList();
            }
        }

        private void GetRightList()
        {
            objAdminRightComponent = new AdminRightsComponent();
            dsadmin = objAdminRightComponent.GetAdminRightsList();
            if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
            {
                chklrights.Items.Clear();
                chklrights.DataSource = dsadmin;
                chklrights.DataTextField = "Name";
                chklrights.DataValueField = "RightsID";
                chklrights.DataBind();
            }
        }

        private void GetAdminList()
        {
            objAdminRightComponent = new AdminRightsComponent();
            dsadmin = objAdminRightComponent.GetAdminTypeList();
            ddlAdmins.DataSource = dsadmin;
            ddlAdmins.DataTextField = "AdminType";
            ddlAdmins.DataValueField = "AdminTypeID";
            ddlAdmins.DataBind();
            ddlAdmins.SelectedIndex = 0;
            BindRightWithAdmin(Convert.ToInt32(ddlAdmins.SelectedItem.Value));
        }

        private void BindRightWithAdmin(int AdminTypeID)
        {
            AdminComponent objadmincomp = new AdminComponent();
            for (int cntchk = 0; cntchk < chklrights.Items.Count; cntchk++)
                chklrights.Items[cntchk].Selected = false;
            dsadmin = objadmincomp.GetAdminRightsDSByAdminId(AdminTypeID);
            if (dsadmin != null && dsadmin.Tables.Count != 0 && dsadmin.Tables[0].Rows.Count>0)
            {
                string[] Rights = dsadmin.Tables[0].Rows[0]["Rights"].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int cnt = 0; cnt < Rights.Length; cnt++)
                {
                    for (int cntchk = 0; cntchk < chklrights.Items.Count; cntchk++)
                    {
                        if (chklrights.Items[cntchk].Value == Rights[cnt].ToString())
                            chklrights.Items[cntchk].Selected = true;
                    }
                }
            }
        }

        protected void ddlAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRightWithAdmin(Convert.ToInt32(ddlAdmins.SelectedItem.Value));
            GetPageRightList();
        }

        private string SetAdminRightForUpdate()
        {
            string rights = string.Empty;
                for (int cnt = 0; cnt < chklrights.Items.Count; cnt++)
                {
                    if (chklrights.Items[cnt].Selected)
                        rights += chklrights.Items[cnt].Value.ToString().Trim() + ",";
                }
                if (rights.Length > 1 && rights.Contains(","))
                    rights = rights.Substring(0, rights.Length - 1);
            return rights;
        }

        protected void btnUpdateRights_Click(object sender, EventArgs e)
        {
            objAdminRightComponent = new AdminRightsComponent();
            Int32 IsAdded = objAdminRightComponent.Insert_Update_AdminTypeRights(Convert.ToInt32(ddlAdmins.SelectedItem.Value), SetAdminRightForUpdate());
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Admin Rights Updated Successfully.', 'Message','');});", true);
        }

        protected void btnUpdatePageRight_Click(object sender, EventArgs e)
        {
            AdminRightsComponent objAdmin = new AdminRightsComponent();
            if (gvAdminPageRights.Rows.Count > 0)
            {
                for (int i = 0; i < gvAdminPageRights.Rows.Count; i++)
                {
                    Label lblCompareAdminID = (Label)gvAdminPageRights.Rows[i].FindControl("lblCompareAdminID");
                    Label lblInnerRightsID = (Label)gvAdminPageRights.Rows[i].FindControl("lblInnerRightsID");
                    CheckBox chkIsListed = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkIsListed");
                    CheckBox chkIsModify = (CheckBox)gvAdminPageRights.Rows[i].FindControl("chkIsModify");
                    Int32 IsAdded = objAdmin.Insert_Update_PageRightsForAdmin(Convert.ToInt32(ddlAdmins.SelectedValue.ToString()), Convert.ToInt32(lblCompareAdminID.Text), Convert.ToInt32(lblInnerRightsID.Text), Convert.ToBoolean(chkIsListed.Checked), Convert.ToBoolean(chkIsModify.Checked), Convert.ToInt32(Session["AdminID"]));
                }
                GetPageRightList();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "RightsInserted", "jAlert('Page Rights Saved Successfully.','Message');", true);
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
            DataSet dsPageRights = objAdminRightComponent.GetAdminPageRightList(Convert.ToInt32(ddlAdmins.SelectedValue.ToString()));
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