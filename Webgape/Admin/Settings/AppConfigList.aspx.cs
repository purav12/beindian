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
    public partial class AppConfigList : System.Web.UI.Page
    {
        #region Declaration
        public static bool isDescendConfigName = false;
        public static bool isDescendConfigValue = false;
        public int StoreID = 1;
        CommonDAC commondac = new CommonDAC();
        #endregion

      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Application configuration inserted successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Application configuration updated successfully.', 'Message','');});", true);

                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdApplicationConfig.PageIndex = 0;
            grdApplicationConfig.DataBind();
            if (grdApplicationConfig.Rows.Count == 0)
                trBottom.Visible = false;
        }

        protected void Sorting(object sender, EventArgs e)
        {
            ImageButton lb = (ImageButton)sender;
            if (lb != null)
            {
                if (lb.CommandArgument == "ASC")
                {
                    grdApplicationConfig.Sort(lb.CommandName.ToString(), SortDirection.Ascending);
                    lb.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date-up.png";
                    if (lb.ID == "btnConfigName")
                    {
                        isDescendConfigName = false;
                    }
                    else if (lb.ID == "btnConfigValue")
                    {
                        isDescendConfigValue = false;
                    }

                    lb.AlternateText = "Descending Order";
                    lb.ToolTip = "Descending Order";
                    lb.CommandArgument = "DESC";
                }
                else if (lb.CommandArgument == "DESC")
                {
                    grdApplicationConfig.Sort(lb.CommandName.ToString(), SortDirection.Descending);
                    lb.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date.png";
                    if (lb.ID == "btnConfigName")
                    {
                        isDescendConfigName = true;
                    }
                    else if (lb.ID == "btnConfigValue")
                    {
                        isDescendConfigValue = true;
                    }
                    lb.AlternateText = "Ascending Order";
                    lb.ToolTip = "Ascending Order";
                    lb.CommandArgument = "ASC";
                }
            }
        }

        protected void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            ConfigurationComponent objAppComp = new ConfigurationComponent();
            int totalRowCount = grdApplicationConfig.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdApplicationConfig.Rows[i].FindControl("hdnConfigid");
                CheckBox chk = (CheckBox)grdApplicationConfig.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    int  AppConfigID=  Convert.ToInt16(hdn.Value);
                    commondac.ExecuteCommonData("update tb_AppConfig set Deleted=1 where AppConfigID='" + AppConfigID + "'");
                }
            }
            grdApplicationConfig.DataBind();
        }

        protected void grdApplicationConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int ConfigId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("AppConfiguration.aspx?AppConfigID=" + ConfigId); //Edit application configuration

            }

            else if (e.CommandName == "DeleteAppConfig")
            {
                int ConfigId = Convert.ToInt32(e.CommandArgument);
                commondac.ExecuteCommonData("update tb_AppConfig set Deleted=1 where AppConfigID='" + ConfigId + "'");
                this.Response.Redirect("AppConfigList.aspx", false);

            }
        }

        protected void grdApplicationConfig_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grdApplicationConfig.Rows.Count > 0)
            {
                trBottom.Visible = true;
            }
            else
            {
                trBottom.Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (isDescendConfigName == false)
                {
                    ImageButton btnConfigName = (ImageButton)e.Row.FindControl("btnConfigName");
                    btnConfigName.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date.png";
                    btnConfigName.AlternateText = "Ascending Order";
                    btnConfigName.ToolTip = "Ascending Order";
                    btnConfigName.CommandArgument = "DESC";
                }
                else
                {
                    ImageButton btnConfigName = (ImageButton)e.Row.FindControl("btnConfigName");
                    btnConfigName.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date-up.png";
                    btnConfigName.AlternateText = "Descending Order";
                    btnConfigName.ToolTip = "Descending Order";
                    btnConfigName.CommandArgument = "ASC";
                }
                if (isDescendConfigValue == false)
                {
                    ImageButton btnConfigValue = (ImageButton)e.Row.FindControl("btnConfigValue");
                    btnConfigValue.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date.png";
                    btnConfigValue.AlternateText = "Ascending Order";
                    btnConfigValue.ToolTip = "Ascending Order";
                    btnConfigValue.CommandArgument = "DESC";
                }
                else
                {
                    ImageButton btnConfigValue = (ImageButton)e.Row.FindControl("btnConfigValue");
                    btnConfigValue.ImageUrl = "/App_Themes/" + Page.Theme + "/icon/order-date-up.png";
                    btnConfigValue.AlternateText = "Descending Order";
                    btnConfigValue.ToolTip = "Descending Order";
                    btnConfigValue.CommandArgument = "ASC";
                }
            }
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            grdApplicationConfig.PageIndex = 0;
            grdApplicationConfig.DataBind();
        }
    }
}