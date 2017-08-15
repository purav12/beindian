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
    public partial class HeaderLinkList : System.Web.UI.Page
    {
        #region Declaration

        //HeaderlinkComponent headercomp = new HeaderlinkComponent();
        CommonDAC commandac = new CommonDAC();
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
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Header Link inserted successfully.', 'Message');});", true);
                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Header Link updated successfully.', 'Message');});", true);
                    }
                }
            }
        }

  
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdheaderlink.PageIndex = 0;
            grdheaderlink.DataBind();
        }

        protected void btnshowall_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            grdheaderlink.PageIndex = 0;
            grdheaderlink.DataBind();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdheaderlink.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdheaderlink.Rows[i].FindControl("hdnheaderlinkid");
                CheckBox chk = (CheckBox)grdheaderlink.Rows[i].FindControl("chkselect");
                if (chk.Checked == true)
                {
                    commandac.ExecuteCommonData("delete from tb_HeaderLinks where PageId='" + hdn.Value + "'");
                    
                }
            }
            grdheaderlink.DataBind();
        }

        protected void btndelete1_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdheaderlink.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdheaderlink.Rows[i].FindControl("hdnheaderlinkid");
                CheckBox chk = (CheckBox)grdheaderlink.Rows[i].FindControl("chkselect");
                if (chk.Checked == true)
                {
                    commandac.ExecuteCommonData("delete from tb_HeaderLinks where PageId='" + hdn.Value + "'");
                }
            }
            grdheaderlink.DataBind();
        }

        protected void grdheaderlink_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grdheaderlink.Rows.Count > 0)
                trBottom.Visible = true;
            else
                trBottom.Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void grdheaderlink_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int headerlinkid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("HeaderLink.aspx?HeaderLinkID=" + headerlinkid);
            }
        }
    }
}