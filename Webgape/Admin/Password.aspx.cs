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
    public partial class Password : System.Web.UI.Page
    {

        #region Declaration
        PasswordComponent passcomp = new PasswordComponent();
        CommonDAC commandac = new CommonDAC();
        AdminComponent admincomp = new AdminComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (admincomp.IsSuperAdmin(Convert.ToInt32(Session["AdminID"])) != 0)
                    {
                        BindPassType();
                        if (Request.QueryString["PasswordId"] != null && Request.QueryString["PasswordId"].ToString() != "")
                        {
                            BindPasswordDetails(Convert.ToInt32(Request.QueryString["PasswordId"]));
                        }
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

        protected void BindPassType()
        {
            ddlpasstype.Items.Clear();
            DataSet dsPassType = new DataSet();

            dsPassType = commandac.GetCommonDataSet("select Id,Type from tb_PasswordType");
            if (dsPassType != null && dsPassType.Tables.Count > 0 && dsPassType.Tables[0].Rows.Count > 0)
            {
                ddlpasstype.DataSource = dsPassType;
                ddlpasstype.DataTextField = "Type";
                ddlpasstype.DataValueField = "Id";
            }
            else
            {
                ddlpasstype.DataSource = null;
            }
            ddlpasstype.DataBind();
            ddlpasstype.Items.Insert(0, new ListItem("Select Password Type", "0"));
            ddlpasstype.SelectedIndex = 4;
        }

        protected void BindPasswordDetails(int PasswordId)
        {
            DataSet dsPassword = new DataSet();
            dsPassword = passcomp.GetPasswordDetails(PasswordId);
            if (dsPassword != null && dsPassword.Tables.Count > 0 && dsPassword.Tables[0].Rows.Count > 0)
            {
                txttitle.Text = dsPassword.Tables[0].Rows[0]["Title"].ToString();
                txtidentifier.Text = dsPassword.Tables[0].Rows[0]["Identifier"].ToString();
                txtpassword.Text = dsPassword.Tables[0].Rows[0]["Password"].ToString();
                txtcomment.Text = dsPassword.Tables[0].Rows[0]["Comment"].ToString();
                ddlpasstype.SelectedIndex = Convert.ToInt32(dsPassword.Tables[0].Rows[0]["TypeId"]);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PasswordId"] != null && Request.QueryString["PasswordId"].ToString() != "")
            {
                bool PasswordUpdated = false;

                PasswordUpdated = passcomp.UpdatePassword(ddlpasstype.SelectedIndex, txttitle.Text.Trim(), txtidentifier.Text.Trim(), txtpassword.Text.Trim(), txtcomment.Text.Trim(), Convert.ToInt32(Session["AdminID"]), Convert.ToInt32(Request.QueryString["PasswordId"]));

                if (PasswordUpdated)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('Password Updated Successfully.', 'Message');});", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailUpdate", "$(document).ready( function() {jAlert('Soomething went wrong while updating Password try again later.', 'Message');});", true);
                    return;
                }
            }
            else
            {
                int Passwordadded = 0;
                Passwordadded = passcomp.InsertPassword(ddlpasstype.SelectedIndex, txttitle.Text.Trim(), txtidentifier.Text.Trim(), txtpassword.Text.Trim(), txtcomment.Text.Trim(), Convert.ToInt32(Session["AdminID"]));
                if (Passwordadded > 0)
                {
                    txtpassword.Text = "";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('Password Inserted Successfully.', 'Message');});", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailInsert", "$(document).ready( function() {jAlert('Soomething went wrong while creating Password please try again later.', 'Message');});", true);
                    return;
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordList.aspx");
        }
    }
}