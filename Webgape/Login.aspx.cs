using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Login : System.Web.UI.Page
    {
        AdminComponent Admincomponent = new AdminComponent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Email"] != null && Request.QueryString["Code"] != null)
                {
                    VerifyUser(Request.QueryString["Email"].ToString(), Request.QueryString["Code"].ToString());
                }

                if (Session["AdminID"] != null)
                {
                    Session["AdminID"] = null;
                    Session["AdminName"] = null;
                    Session["UserRecorded"] = null;
                    System.Web.HttpCookie custCookie = new System.Web.HttpCookie("IsLogout", "Yes");
                    custCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(custCookie);
                    Response.Redirect("Default.aspx");
                }

                if (Request.Cookies["Adminusername"] != null && !string.IsNullOrEmpty(Request.Cookies["Adminusername"].Value))
                {
                    txtuname.Text = Request.Cookies["Adminusername"].Value;
                }
                if (Request.Cookies["Adminpassword"] != null && !string.IsNullOrEmpty(Request.Cookies["Adminpassword"].Value))
                {
                    txtpass.Attributes.Add("value", Request.Cookies["Adminpassword"].Value);
                    chkstore.Checked = true;
                }
            }
        }

        protected void btnloggin_Click(object sender, EventArgs e)
        {
            DataSet dsAdmin = new DataSet();
            dsAdmin = Admincomponent.GetAdminForLogin(txtuname.Text.Trim(), SecurityComponent.Encrypt(txtpass.Text.Trim()));
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
                Admincomponent.GetAllPageRightsByAdminID(Convert.ToInt32(Session["AdminID"]));
                if (chkstore.Checked == true)
                {
                    System.Web.HttpCookie custCookie = new System.Web.HttpCookie("Adminusername", txtuname.Text.ToString());
                    custCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(custCookie);
                    custCookie = new System.Web.HttpCookie("Adminpassword", txtpass.Text.Trim().ToString());
                    custCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(custCookie);

                    custCookie = new System.Web.HttpCookie("IsLogout", "No");
                    Request.Cookies["IsLogout"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(custCookie);
                }
                else
                {
                    if (Request.Cookies["Adminusername"] != null)
                    {
                        Request.Cookies["Adminusername"].Expires = DateTime.Now.AddDays(-1);
                    }
                    if (Request.Cookies["Adminpassword"] != null)
                    {
                        Request.Cookies["Adminpassword"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
                Response.Redirect("/Admin/Dashboard.aspx");
            }
            else
            {
                lblerror.Text = "Whoops! We didn't recognise your username or password. Please try again.";
                lblerror.Visible = true;
            }
        }

        public void VerifyUser(string Email, string Password)
        {
            DataSet dsAdmin = new DataSet();
            dsAdmin = Admincomponent.VerifyAdmin(Email, Password);
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
                Admincomponent.GetAllPageRightsByAdminID(Convert.ToInt32(Session["AdminID"]));
                if (chkstore.Checked == true)
                {
                    System.Web.HttpCookie custCookie = new System.Web.HttpCookie("Adminusername", Email);
                    custCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(custCookie);
                    custCookie = new System.Web.HttpCookie("Adminpassword", Password);
                    custCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(custCookie);

                    custCookie = new System.Web.HttpCookie("IsLogout", "No");
                    Request.Cookies["IsLogout"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(custCookie);
                }
                else
                {
                    if (Request.Cookies["Adminusername"] != null)
                    {
                        Request.Cookies["Adminusername"].Expires = DateTime.Now.AddDays(-1);
                    }
                    if (Request.Cookies["Adminpassword"] != null)
                    {
                        Request.Cookies["Adminpassword"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
                Response.Redirect("/Admin/Profile/Profile.aspx");
            }
            else
            {
                lblerror.Text = "Something went worng. Please try again.";
                lblerror.Visible = true;
            }
        }

    }
}