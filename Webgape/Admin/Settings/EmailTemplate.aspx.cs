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
    public partial class EmailTemplate : System.Web.UI.Page
    {
        #region Declaration
        public int Templatecount = 0;
        EmailTemplateComponent objEmailTempComponent = new EmailTemplateComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["EmailTemplateID"]) && Convert.ToString(Request.QueryString["EmailTemplateID"]) != "0")
                {
                    objEmailTempComponent = new EmailTemplateComponent();
                    DataSet DsMailTemplate = new DataSet();
                    DsMailTemplate = objEmailTempComponent.GetEmailTemplateByTemplateId(Convert.ToInt32(Request.QueryString["EmailTemplateID"]), 1);
                    if (DsMailTemplate != null && DsMailTemplate.Tables.Count > 0 && DsMailTemplate.Tables[0].Rows.Count > 0)
                    {
                        txtLabel.Text = DsMailTemplate.Tables[0].Rows[0]["Label"].ToString();
                        txtSubject.Text = DsMailTemplate.Tables[0].Rows[0]["Subject"].ToString();
                        txtMailBody.Text = DsMailTemplate.Tables[0].Rows[0]["EmailBody"].ToString();
                        lblHeader.Text = "Edit Email Template";
                    }
                }
            }
        }

        protected void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            int TemplateID = 0;
            objEmailTempComponent = new EmailTemplateComponent();

            if (txtMailBody.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Email Body.', 'Message','');});", true);
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["EmailTemplateID"]) && Convert.ToString(Request.QueryString["EmailTemplateID"]) != "0")
            {
                TemplateID = objEmailTempComponent.InsertEmailTemplate(txtLabel.Text.Trim(), Convert.ToInt32(Request.QueryString["EmailTemplateID"]), txtSubject.Text.Trim(), txtMailBody.Text.Trim(), null,2);
                if (TemplateID > 0)
                    Response.Redirect("EmailTemplateList.aspx?status=updated");
            }
            else
            {
                TemplateID = objEmailTempComponent.InsertEmailTemplate(txtLabel.Text.Trim(), Convert.ToInt32(Request.QueryString["EmailTemplateID"]), txtSubject.Text.Trim(), txtMailBody.Text.Trim(), null,1);
                if (TemplateID > 0)
                    Response.Redirect("EmailTemplateList.aspx?status=inserted");
            }
        }

        protected void btnCancelTemplate_Click(object sender,EventArgs e)
        {
            Response.Redirect("EmailTemplateList.aspx");
        }

    }
}