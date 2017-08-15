using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WebgapeClass;

namespace Webgape.Admin.Users
{
    public partial class UserList : System.Web.UI.Page
    {
        #region Declaration
        public int Usercount = 0;
        UserComponent objusercomp = new UserComponent();
        DataSet DsUser = new DataSet();
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
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('User inserted successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('User updated successfully.', 'Message','');});", true);
                    }
                }
                BindGrid();
                Master.HeadTitle("BeIndian.in - User List", "BeIndian.in - User List, Admin User List", "BeIndian.in - User List by Admin");
            }
        }

        public void BindGrid()
        {
            DsUser = objusercomp.GetAllUserDetails(txtSearch.Text.Trim());
            if (DsUser != null && DsUser.Tables[0] != null && DsUser.Tables[0].Rows.Count != 0)
            {
                Usercount = DsUser.Tables[0].Rows.Count;
                ViewState["GridDataTable"] = DsUser;
                grdUser.DataSource = DsUser;
                grdUser.DataBind();
            }
            else
            {
                grdUser.DataSource = null;
                grdUser.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdUser.PageIndex = 0;
            BindGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            grdUser.PageIndex = 0;
            BindGrid();
        }


        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString().Trim().ToLower() == "delete")
            {
            }
            if (e.CommandName.ToString().Trim().ToLower() == "edit")
            {
                int gUserID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("User.aspx?mode=edit&UserID=" + gUserID.ToString(), true);
            }
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal lblFirstName = (Literal)e.Row.FindControl("lblFirstName");
                HiddenField hdnFrmid = (HiddenField)e.Row.FindControl("hdnFrmid");

                lblFirstName.Text = "<a href='/User.aspx?UID=" + hdnFrmid.Value + "'>" + lblFirstName.Text + "</a>";
            }
        }


        protected void btnhdnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string CommandName = btn.CommandName;
            int myid = 0;
            int AdminId = Convert.ToInt32(btn.CommandArgument);
            if (Session["AdminID"] != null)
            {
                myid = Convert.ToInt32(Session["AdminID"]);
            }
            if (AdminId == myid)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('You cant send message to yourself.', 'Message','');});", true);
            }
            else
            {
                Response.Redirect("/Admin/Profile/Message.aspx?ToId=" + AdminId);
            }

        }

        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            txtSearch.Text = "";
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
        }
    }
}