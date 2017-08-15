using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Content
{
    public partial class SubscriptionList : System.Web.UI.Page
    {
        public int Subscriptioncount = 0;
        CommonDAC commandac = new CommonDAC();
        SubscriptionComponent subcomp = new SubscriptionComponent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Subscription sent successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Subscription updated successfully.', 'Message','');});", true);

                    }
                }
                FillSubGrid();
                Master.HeadTitle("BeIndian - Subscription List", "BeIndian.in - Subscription List, Admin Subscription List", "BeIndian.in - Subscription List by Admin");
            }
        }

        private void FillSubGrid()
        {
            DataSet dssub = new DataSet();
            if (Session["AdminID"] != null)
            {
                dssub = subcomp.GetSubscriptionList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), ddlType.SelectedValue, 1);
                if (dssub != null && dssub.Tables[0] != null && dssub.Tables[0].Rows.Count != 0)
                {
                    Subscriptioncount = dssub.Tables[0].Rows.Count;
                    grdSubscription.DataSource = dssub;
                }
                else
                {
                    grdSubscription.DataSource = null;
                }
                grdSubscription.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdSubscription.PageIndex = 0;
            FillSubGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdSubscription.PageIndex = 0;
            FillSubGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdSubscription.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            FillSubGrid();
        }

        protected void grdSubscription_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdSubscription.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdSubscription.Rows[i].FindControl("hdnsubid");
                CheckBox chk = (CheckBox)grdSubscription.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    subcomp.DeleteSubscription(Convert.ToInt16(hdn.Value));
                    lblSubscription.Text = "Subscription Deleted Successfully";
                }
            }
            FillSubGrid();
        }

        protected void grdSubscription_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString().Trim().ToLower() == "delete")
            {
                int SubscriptionID = Convert.ToInt32(e.CommandArgument);
                subcomp.DeleteSubscription(SubscriptionID);
                lblSubscription.Text = "Subscription Deleted Successfully";
            }
            FillSubGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSubscription.PageIndex = e.NewPageIndex;
            FillSubGrid();
        }
        protected void grdSubscription_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}