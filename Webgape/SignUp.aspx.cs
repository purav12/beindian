using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class SignUp : System.Web.UI.Page
    {
        AdminComponent AdminComponent = new AdminComponent();
        CommonDAC CommonDAC = new CommonDAC();
        public string RandomCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnsignup_Click(object sender, EventArgs e)
        {

            string encryptpass = string.Empty;
            if (txtFname.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter First Name.', 'Message','');});", true);
                txtFname.Focus();
                return;
            }
            if (txtLname.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Last Name.', 'Message','');});", true);
                txtLname.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Email Address.', 'Message','');});", true);
                txtEmail.Focus();
                return;
            }
            if (!new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Match(txtEmail.Text.ToString().Trim()).Success)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Valid Email Address.', 'Message','');});", true);
                txtEmail.Focus();
                return;
            }
            else if (txtPassword.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Password.', 'Message','');});", true);
                txtPassword.Focus();
                return;
            }
            else if (txtPassword.Text.Length < 6)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Password length must be 5 characters.', 'Message','');});", true);
                txtPassword.Focus();
                return;
            }

            encryptpass = SecurityComponent.Encrypt(txtPassword.Text);

            if (CheckMail(txtEmail.Text.Trim()) > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('User already exists with same Email , Click on forgot password to retriew it.', 'Message','');});", true);
                txtEmail.Focus();
                return;
            }
            else
            {
                int adminid = AdminComponent.InsertAdmin(txtFname.Text.Trim(), txtLname.Text.Trim(), txtEmail.Text.Trim(), encryptpass);
                SendMail();
                Dologin(adminid);
            }
        }

        public Int32 CheckMail(string Email)
        {
            Int32 isExists = 0;
            isExists = AdminComponent.IsExist(Email);
            return isExists;
        }

        public void Dologin(int AdminId)
        {
            DataSet dsAdmin = new DataSet();
            dsAdmin = AdminComponent.GetAdminProfileByAdminId(AdminId);
            if (dsAdmin != null && dsAdmin.Tables.Count > 0 && dsAdmin.Tables[0].Rows.Count > 0)
            {
                if (dsAdmin.Tables[0].Rows[0]["FirstName"].ToString() != null && dsAdmin.Tables[0].Rows[0]["FirstName"].ToString() != "" && dsAdmin.Tables[0].Rows[0]["LastName"].ToString() != null || dsAdmin.Tables[0].Rows[0]["LastName"].ToString() != "")
                {
                    Session["AdminName"] = dsAdmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsAdmin.Tables[0].Rows[0]["LastName"].ToString();
                }
                else if (dsAdmin.Tables[0].Rows[0]["UserName"].ToString() != null || dsAdmin.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    Session["AdminName"] = "user94758";
                }
                Session["AdminID"] = dsAdmin.Tables[0].Rows[0]["AdminID"].ToString();
                AppLogic.ApplicationStart();
                AdminComponent.GetAllPageRightsByAdminID(Convert.ToInt32(Session["AdminID"]));

                System.Web.HttpCookie custCookie = new System.Web.HttpCookie("AdmincustomeruserId", txtEmail.Text.ToString());
                custCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(custCookie);
                custCookie = new System.Web.HttpCookie("Admincustomerpassword", txtPassword.Text.Trim().ToString());
                custCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(custCookie);

                Response.Redirect("/Admin/Dashboard.aspx");
            }
            else
            {
                lblerror.Text = "Whoops! Something went wrong. Please try again.";
                lblerror.Visible = true;
            }
        }

        private void SendMail()
        {
            EmailTemplateComponent objEmailTemplateComponent = new EmailTemplateComponent();
            DataSet dsCreateAccount = new DataSet();
            dsCreateAccount = objEmailTemplateComponent.GetEmailTemplateByTemplateName("CreateAccount",2);
            
            if (dsCreateAccount != null && dsCreateAccount.Tables.Count > 0 && dsCreateAccount.Tables[0].Rows.Count > 0)
            {
                String strBody = "";
                String strSubject = "";
                string encryptpass = string.Empty;
                string code = string.Empty;
                
                DataSet dsadmin = new DataSet();
                strBody = dsCreateAccount.Tables[0].Rows[0]["EmailBody"].ToString();
                strSubject = dsCreateAccount.Tables[0].Rows[0]["Subject"].ToString();
                encryptpass = SecurityComponent.Encrypt(txtPassword.Text);
                dsadmin = AdminComponent.GetAdminCode(txtEmail.Text.ToString());
                code = dsadmin.Tables[0].Rows[0]["CODE"].ToString();

                strSubject = Regex.Replace(strSubject, "###STORENAME###", AppLogic.AppConfigs("STORENAME").ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###STOREPATH###", AppLogic.AppConfigs("STOREPATH").ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###LIVE_SERVER###", AppLogic.AppConfigs("LIVE_SERVER").ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###STORENAME###", AppLogic.AppConfigs("STORENAME").ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###FIRSTNAME###", txtFname.Text.ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###LASTNAME###", txtLname.Text.ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###PASSWORD###", txtPassword.Text.ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###CODE###", code.Substring(0, 5), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###VERIFICATIONLINK###", "Login.aspx?Email=" + txtEmail.Text.ToString() + "&Code=" + code.Substring(0, 5), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###EMAIL###", txtEmail.Text.ToString(), RegexOptions.IgnoreCase);

                strBody = Regex.Replace(strBody, "###YEAR###", AppLogic.AppConfigs("YEAR").ToString(), RegexOptions.IgnoreCase);

                AlternateView av = AlternateView.CreateAlternateViewFromString(strBody.ToString(), null, "text/html");
                CommonOperations.SendMail(txtEmail.Text.ToString().Trim(), strSubject.ToString(), strBody.ToString(), Request.UserHostAddress.ToString(), true, av);
            }
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}