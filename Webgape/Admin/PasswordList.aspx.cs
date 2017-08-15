using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;


namespace Webgape.Admin
{
    public partial class PasswordList : System.Web.UI.Page
    {
        #region Declaration
        PasswordComponent passcomp = new PasswordComponent();
        AdminComponent admincomp = new AdminComponent();
        CommonDAC commandac = new CommonDAC();
        public int Passwordcount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (admincomp.IsSuperAdmin(Convert.ToInt32(Session["AdminID"])) != 0)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                        {
                            String strStatus = Convert.ToString(Request.QueryString["status"]);
                            if (strStatus == "inserted")
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Password added successfully.', 'Message','');});", true);
                            }
                            else if (strStatus == "updated")
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Password updated successfully.', 'Message','');});", true);
                            }
                        }
                        BindPasswordType();
                        FillPasswordGrid();
                    }
                    else
                    {
                        Response.Redirect("/Admin/Dashboard.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

        }


        private void FillPasswordGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = passcomp.GetPasswordList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), ddlType.SelectedValue, 1);
                Passwordcount = dsmsg.Tables[0].Rows.Count;
                grdPassword.DataSource = dsmsg;
                grdPassword.DataBind();
            }
        }

        protected void BindPasswordType()
        {
            ddlType.Items.Clear();
            DataSet dsPasswordType = new DataSet();

            dsPasswordType = commandac.GetCommonDataSet("SELECT Id,Type FROM tb_PasswordType ORDER BY DisplayOrder");
            if (dsPasswordType != null && dsPasswordType.Tables.Count > 0 && dsPasswordType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsPasswordType;
                ddlType.DataTextField = "Type";
                ddlType.DataValueField = "Id";
            }
            else
            {
                ddlType.DataSource = null;
            }
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("Select Password Type", "0"));
            //ddlType.SelectedIndex = 1;
        }


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPassword.PageIndex = 0;
            FillPasswordGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdPassword.PageIndex = 0;
            FillPasswordGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdPassword.PageIndex = 0;
            txtSearch.Text = "";
            //ddlType.SelectedIndex = 1;
            FillPasswordGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdPassword.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdPassword.Rows[i].FindControl("hdnTskid");
                CheckBox chk = (CheckBox)grdPassword.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    passcomp.DeletePassword(Convert.ToInt16(hdn.Value));
                    lblMessage.Text = "Password Deleted Successfully";
                }
            }
            FillPasswordGrid();
        }

        protected void grdPassword_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString().Trim().ToLower() == "delete")
            {
                int MessageID = Convert.ToInt32(e.CommandArgument);
                passcomp.DeletePassword(MessageID);
                lblMessage.Text = "Password Deleted Successfully";
            }
            FillPasswordGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPassword.PageIndex = e.NewPageIndex;
            FillPasswordGrid();
        }
        protected void grdPassword_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

    }
}