using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.ADMIN.Settings
{

    public partial class OnePageMailConfig : System.Web.UI.Page
    {

        #region Declaration
        ConfigurationComponent ConfigurationC = new ConfigurationComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

       
        public void BindData()
        {
            DataSet DsAppConfig = ConfigurationC.GetMailConfig("", "" , DateTime.MaxValue, 1, 1);
            GetValue(DsAppConfig);

        }

        public void GetValue(DataSet Ds)
        {
            try
            {
                ClearData();
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    Hashtable ht = new Hashtable();
                    for (int cnt = 0; cnt < Ds.Tables[0].Rows.Count; cnt++)
                    {
                        try { ht.Add((string.IsNullOrEmpty(Ds.Tables[0].Rows[cnt]["StoreID"].ToString()) ? "1" : Ds.Tables[0].Rows[cnt]["StoreID"].ToString()) + Ds.Tables[0].Rows[cnt]["configname"].ToString(), Ds.Tables[0].Rows[cnt]["configvalue"].ToString()); }
                        catch { }
                    }

                    ViewState.Add("Hastable", ht);
                    txtContactMail_ToAddress.Text = (ht[1 + "ContactMail_ToAddress"] == null) ? "" : ht[1 + "ContactMail_ToAddress"].ToString();
                    txtHost.Text = (ht[1 + "Host"] == null) ? "" : ht[1 + "Host"].ToString();
                    txtMailFrom.Text = (ht[1 + "MailFrom"] == null) ? "" : ht[1 + "MailFrom"].ToString();
                    txtMailMe_ToAddress.Text = (ht[1 + "MailMe_ToAddress"] == null) ? "" : ht[1 + "MailMe_ToAddress"].ToString();
                    txtMailPassword.Text = (ht[1 + "MailPassword"] == null) ? "" : ht[1 + "MailPassword"].ToString();
                    txtMailPassword.Attributes.Add("value", txtMailPassword.Text);
                    txtMailUserName.Text = (ht[1 + "MailUserName"] == null) ? "" : ht[1 + "MailUserName"].ToString();
                    chkSendCustomerRegistrationMail.Checked = (ht[1 + "SendCustomerRegistrationMail"] == null) ? false : (ht[1 + "SendCustomerRegistrationMail"].ToString().ToLower() == "true") ? true : false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

         private void ClearData()
        {
            txtContactMail_ToAddress.Text = "";
            txtHost.Text = "";
            txtMailFrom.Text = "";
            txtMailMe_ToAddress.Text = "";
            txtMailPassword.Text = "";
            txtMailUserName.Text = "";
            chkSendCustomerRegistrationMail.Checked = false;

        }

         protected void imgSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Hashtable hastable = new Hashtable();

            if (ViewState["Hastable"] != null)
            {
                hastable = (Hashtable)ViewState["Hastable"];
            }

            if (hastable != null )
            {
                DateTime UpdatedOn = DateTime.Now;
                int UpdatedBy = int.Parse(Session["AdminID"].ToString());

                UpdateThis(hastable, "ContactMail_ToAddress",  txtContactMail_ToAddress.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "Host",  txtHost.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "MailFrom",  txtMailFrom.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "MailMe_ToAddress",  txtMailMe_ToAddress.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "MailPassword",  txtMailPassword.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "MailUserName",  txtMailUserName.Text, UpdatedOn, UpdatedBy);
                UpdateThis(hastable, "SendCustomerRegistrationMail",  Convert.ToString(chkSendCustomerRegistrationMail.Checked), UpdatedOn, UpdatedBy);

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Mail Configuration Updated Successfully.', 'Message','');});", true);
                BindData();
            }
        }

          private void UpdateThis(Hashtable hastable, string controlname , string txtValue, DateTime UpdatedOn, int UpdatedBy)
        {
            ConfigurationComponent configcomp = new ConfigurationComponent();
            configcomp.UpdateMailConfig(controlname, txtValue , UpdatedOn, UpdatedBy, 2);
        }

       

         protected void imgCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/Dashboard.aspx");
        }

         protected void imgTestMail_Click(object sender, EventArgs e)
        {
            bool istrue = SendTestMail();

            if (istrue)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Test Mail has been sent Successfully.', 'Message');});", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msgnotexists", "$(document).ready( function() {ShowForGotPassword();jAlert('Test Mail has not been sent ', 'Message','txtEmail');});", true);
                return;
            }
        }

         private bool SendTestMail()
        {
            AdminComponent objadmincomp = new AdminComponent();
            bool IsSent = false;
            DataSet dsMailTemplate = new DataSet();
            dsMailTemplate = objadmincomp.GetEmailTamplate("TestMailForMailconfig",1);

            if (dsMailTemplate != null && dsMailTemplate.Tables.Count > 0 && dsMailTemplate.Tables[0].Rows.Count > 0)
            {
                String strBody = "";
                String strSubject = "";
                strBody = dsMailTemplate.Tables[0].Rows[0]["EmailBody"].ToString();
                strSubject = dsMailTemplate.Tables[0].Rows[0]["Subject"].ToString();

                strSubject = Regex.Replace(strSubject, "###STORENAME###", "Webgape", RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###LIVE_SERVER###", AppLogic.AppConfigs("LIVE_SERVER").ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###STORENAME###", "Webgape", RegexOptions.IgnoreCase);

                AlternateView av = AlternateView.CreateAlternateViewFromString(strBody.ToString(), null, "text/html");
                CommonOperations.SendTestMail(txtMailUserName.Text.ToString().Trim(), txtMailPassword.Text.ToString().Trim(), txtHost.Text.ToString().Trim(), txtMailFrom.Text.ToString().Trim(), txtMailMe_ToAddress.Text.ToString().Trim(), strSubject.ToString(), strBody.ToString(), Request.UserHostAddress.ToString(), true, av);
                IsSent = true;
            }

            return IsSent;
        }

    }
}