using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        #region Declaration
        DataTable admin = null;
        string[] Rights = null;
        AdminComponent admincomp = new AdminComponent();
        CommonDAC CommonDAC = new CommonDAC();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DoAutoLogin();
            }
            if (Session["AdminID"] != null)
            {
                string httpurl = Request.Url.ToString();
                if (httpurl.ToLower().StartsWith("http://"))
                {
                    if (AppLogic.AppConfigBool("UseSSLAdmin"))
                    {
                        httpurl = httpurl.Replace("http", "https").ToString();
                        Response.Redirect(httpurl);
                    }
                }
                if (Session["AdminID"] == null || Session["AdminID"].ToString() == "")
                {
                    Response.Redirect("/Login.aspx");
                }


                //if (Request.RawUrl.Contains("/Dashboard.aspx"))
                //{
                //    SpaceDashboard.Attributes.Add("style", "min-height:0px;");
                //    SpaceDashboard.Visible = true;
                //}
                litDate.Text = DateTime.Now.ToLongDateString();

                if (!IsPostBack)
                {
                    if (Session["AdminID"] != null || Session["AdminID"].ToString() != "")
                    {
                        BindHeaderFields();
                        BindActiveleft();
                        SetAdminRights();
                        SetAdminPageRight();
                        BindActiveImage();
                    }
                }
                AddSiteConfig();
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
            BindVisitor();
        }


        /// <summary>
        /// To add title,keyword and description
        /// </summary>
        private void AddSiteConfig()
        {
            //if (Request.Url.ToString().ToLower().IndexOf("subcategory.aspx") > -1)
            //{
            //}
            //else if (Request.Url.ToString().ToLower().IndexOf("post.aspx") > -1)
            //{
            //}
            //else
            //{
            //    HeadTitle("Webgape - Dashboard", "Webgape.Com, Webgape - Dashboard, Admin Panel", "Webgape.com Dashboard panel");
            //}
        }

        /// <summary>
        /// Access Directly From other Pages to se Configurations
        /// </summary>
        /// <param name="SiteSETitle"></param>
        /// <param name="SiteSEKeywords"></param>
        /// <param name="SiteSEDescription"></param>
        public void HeadTitle(string SiteSETitle, string SiteSEKeywords, string SiteSEDescription)
        {
            Page.Title = SiteSETitle;

            System.Web.UI.HtmlControls.HtmlHead Head = (System.Web.UI.HtmlControls.HtmlHead)Page.Header;
            System.Web.UI.HtmlControls.HtmlMeta HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "Title";
            HtmlMeta.Content = Convert.ToString(SiteSETitle);
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "Keywords";
            HtmlMeta.Content = Convert.ToString(SiteSEKeywords);
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "Description";
            HtmlMeta.Content = Convert.ToString(SiteSEDescription);
            Head.Controls.Add(HtmlMeta);

            if (Request.Url.ToString().ToLower().IndexOf("index.aspx") > -1)
            {
                //if (!String.IsNullOrEmpty(AppLogic.AppConfigs("GoogleSiteVerification")) && AppLogic.AppConfigs("GoogleSiteVerification") != "")
                //{
                //    HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
                //    HtmlMeta.Name = "google-site-verification";
                //    HtmlMeta.Content = AppLogic.AppConfigs("GoogleSiteVerification");
                //    Head.Controls.Add(HtmlMeta);
                //}

                if (!String.IsNullOrEmpty(AppLogic.AppConfigs("MSValidate.01")) && AppLogic.AppConfigs("MSValidate.01") != "")
                {
                    HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
                    HtmlMeta.Name = "msvalidate.01";
                    HtmlMeta.Content = AppLogic.AppConfigs("MSValidate.01");//5A2D0856848664C274770B7BC7491E88
                    Head.Controls.Add(HtmlMeta);
                }

                if (!String.IsNullOrEmpty(AppLogic.AppConfigs("Y_Key")) && AppLogic.AppConfigs("Y_Key") != "")
                {
                    HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
                    HtmlMeta.Name = "y_key";
                    HtmlMeta.Content = AppLogic.AppConfigs("Y_Key");
                    Head.Controls.Add(HtmlMeta);
                }

            }

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "googlebot";
            HtmlMeta.Content = "index, follow";
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "revisit-after";
            HtmlMeta.Content = "3 Days";
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "robots";
            HtmlMeta.Content = "all";
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "robots";
            HtmlMeta.Content = "index, follow";
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "author";
            HtmlMeta.Content = AppLogic.AppConfigs("StorePath");
            Head.Controls.Add(HtmlMeta);

        }

        public void BindHeaderFields()
        {
            if (Session["AdminID"] != null)
            {
                DataSet dsCount = new DataSet();
                dsCount = admincomp.GetAdminCountsForMaster(Convert.ToInt32(Session["AdminID"]));
                lblmsgcount.Text = dsCount.Tables[0].Rows[0]["TotalCount"].ToString();
                lblnotcount.Text = dsCount.Tables[0].Rows[1]["TotalCount"].ToString();
            }
        }

        public void BindActiveleft()
        {

            string strurl = Request.RawUrl.ToString().ToLower();
            if (strurl.ToLower().Contains("/admin/dashboard.aspx"))
            {
                lidashboard.Attributes.Add("class", "active");
            }
            if (strurl.ToLower().Contains("/profile/"))
            {
                liprofile.Attributes.Add("class", "active");
            }
            else if (strurl.ToLower().Contains("/posts/"))
            {
                lipost.Attributes.Add("class", "active");
            }
            else if (strurl.ToLower().Contains("/users/"))
            {
                liuser.Attributes.Add("class", "active");
            }
            else if (strurl.ToLower().Contains("/content/"))
            {
                licontent.Attributes.Add("class", "active");
            }
            else if (strurl.ToLower().Contains("/settings/"))
            {
                lisettings.Attributes.Add("class", "active");
            }
            else if (strurl.ToLower().Contains("/reports/"))
            {
                lireports.Attributes.Add("class", "active");
            } 

            else
            {
                lidashboard.Attributes.Add("class", "active");
            }
        }

        private void SetAdminRights()
        {
            if (Session["AdminID"] != null)
            {
                admin = admincomp.GetAdminRightsByAdminId(Convert.ToInt32(Session["AdminID"]));
                if (admin != null && admin.Rows.Count != 0)
                {
                    Rights = admin.Rows[0]["Rights"].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int cnt = 0; cnt < Rights.Length; cnt++)
                    {
                        switch (Convert.ToInt16(Rights[cnt]))
                        {
                            case 13:
                                Page.Master.FindControl("liprofile").Visible = true;
                                break;
                            case 14:
                                Page.Master.FindControl("lipost").Visible = true;
                                break;
                            case 15:
                                Page.Master.FindControl("liuser").Visible = true;
                                break;
                            case 16:
                                Page.Master.FindControl("licontent").Visible = true;
                                break;
                            case 17:
                                Page.Master.FindControl("lisettings").Visible = true;
                                break;
                            case 18:
                                Page.Master.FindControl("lireports").Visible = true;
                                break;
                           }
                    }
                }
            }
        }

        public void SetAdminPageRight()
        {
            if (Session["dtAdminRightsList"] != null)
            {
                DataTable dtright = new DataTable();
                dtright = (DataTable)Session["dtAdminRightsList"];
                if (dtright != null && dtright.Rows.Count > 0)
                {
                    for (int i = 0; i < dtright.Rows.Count; i++)
                    {
                        switch (Convert.ToString(dtright.Rows[i]["PageName"]))
                        {
                            case "General":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    ligeneral.Visible = true;
                                }
                                else
                                {
                                    ligeneral.Visible = false;
                                }
                                break;
                            case "Message":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    limessage.Visible = true;
                                }
                                else
                                {
                                    limessage.Visible = false;
                                }
                                break;
                            case "Notification":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    linotification.Visible = true;
                                }
                                else
                                {
                                    linotification.Visible = false;
                                }
                                break;
                            case "Erning":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liearnings.Visible = true;
                                }
                                else
                                {
                                    liearnings.Visible = false;
                                }
                                break;
                            case "Points":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    lipoints.Visible = true;
                                }
                                else
                                {
                                    lipoints.Visible = false;
                                }
                                break;
                            case "Post List":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    lipostlist.Visible = true;
                                }
                                else
                                {
                                    lipostlist.Visible = false;
                                }
                                break;
                            case "Add Post":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liaddpost.Visible = true;
                                }
                                else
                                {
                                    liaddpost.Visible = false;
                                }
                                break;
                            case "Comment":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    licomment.Visible = true;
                                }
                                else
                                {
                                    licomment.Visible = false;
                                }
                                break;
                            case "Category List":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    licatlist.Visible = true;
                                }
                                else
                                {
                                    licatlist.Visible = false;
                                }
                                break;
                            case "Add Category":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liaddcat.Visible = true;
                                }
                                else
                                {
                                    liaddcat.Visible = false;
                                }
                                break;
                            case "Generate XML":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    ligooglexml.Visible = true;
                                }
                                else
                                {
                                    ligooglexml.Visible = false;
                                }
                                break;
                            case "User List":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liuserlist.Visible = true;
                                }
                                else
                                {
                                    liuserlist.Visible = false;
                                }
                                break;
                            case "Topic list":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    litopiclist.Visible = true;
                                }
                                else
                                {
                                    litopiclist.Visible = false;
                                }
                                break;
                            case "Add Topic":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liaddtopic.Visible = true;
                                }
                                else
                                {
                                    liaddtopic.Visible = false;
                                }
                                break;
                            case "Subscription":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    lisubscription.Visible = true;
                                }
                                else
                                {
                                    lisubscription.Visible = false;
                                }
                                break;                    
                            case "Testimonial List":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    litestimoniallist.Visible = true;
                                }
                                else
                                {
                                    litestimoniallist.Visible = false;
                                }
                                break;
                            case "Admin Rights":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liadminrights.Visible = true;
                                    lipagerights.Visible = true;
                                }
                                else
                                {
                                    liadminrights.Visible = false;
                                    lipagerights.Visible = false;
                                }
                                break;
                            case "Mail Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    limailconfig.Visible = true;
                                }
                                else
                                {
                                    limailconfig.Visible = false;
                                }
                                break;
                            case "Image Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liimageconfig.Visible = true;
                                }
                                else
                                {
                                    liimageconfig.Visible = false;
                                }
                                break;
                            case "Profile Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liprofilepageconfiguration.Visible = true;
                                }
                                else
                                {
                                    liprofilepageconfiguration.Visible = false;
                                }
                                break;
                            case "Post Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    lipostpageconfig.Visible = true;
                                }
                                else
                                {
                                    lipostpageconfig.Visible = false;
                                }
                                break;
                            case "Application Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liapplicationconfiguration.Visible = true;
                                }
                                else
                                {
                                    liapplicationconfiguration.Visible = false;
                                }
                                break;
                            case "Index Page Configuration":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liindexpageconfiguration.Visible = true;
                                }
                                else
                                {
                                    liindexpageconfiguration.Visible = false;
                                }
                                break;
                            case "Header Links":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liheaderlinks.Visible = true;
                                }
                                else
                                {
                                    liheaderlinks.Visible = false;
                                }
                                break;
                            case "Email Templates":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    liemailtemplates.Visible = true;
                                }
                                else
                                {
                                    liemailtemplates.Visible = false;
                                }
                                break;
                            case "Database Backup":
                                if (Convert.ToBoolean(dtright.Rows[i]["isListed"]))
                                {
                                    lidatabasebackup.Visible = true;
                                }
                                else
                                {
                                    lidatabasebackup.Visible = false;
                                }
                                break;

                        }
                    }
                }
            }
        }

        public void BindActiveImage()
        {

            string strurl = Request.RawUrl.ToString().ToLower();
            if (strurl.ToLower().Contains("/admin/dashboard.aspx"))
            {

            }
            else if (strurl.ToLower().Contains("/profile/"))
            {
                if ((Rights != null && !Rights.Contains("13")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
            else if (strurl.ToLower().Contains("/posts/"))
            {
                if ((Rights != null && !Rights.Contains("14")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
            else if (strurl.ToLower().Contains("/user/"))
            {
                if ((Rights != null && !Rights.Contains("15")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
            else if (strurl.ToLower().Contains("/content/"))
            {
                if ((Rights != null && !Rights.Contains("16")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
            else if (strurl.ToLower().Contains("/setting/"))
            {
                if ((Rights != null && !Rights.Contains("17")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
            else if (strurl.ToLower().Contains("/admin/report.aspx"))
            {
                if ((Rights != null && !Rights.Contains("18")))
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            } 
        }

        public void DoAutoLogin()
        {
            if (Session["AdminID"] == null)
            {
                if (Request.Cookies["IsLogout"] == null)
                {
                    System.Web.HttpCookie custCookie = new System.Web.HttpCookie("IsLogout", "No");
                    custCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(custCookie);
                }
                if (Request.Cookies["Adminusername"] != null && !string.IsNullOrEmpty(Request.Cookies["Adminusername"].Value) && Request.Cookies["Adminpassword"] != null && !string.IsNullOrEmpty(Request.Cookies["Adminpassword"].Value) && Request.Cookies["IsLogout"] != null && Convert.ToString(Request.Cookies["IsLogout"].Value) == "No")
                {
                    DataSet dsAdmin = new DataSet();
                    AdminComponent Admincomponent = new AdminComponent();
                    dsAdmin = Admincomponent.GetAdminForLogin(Request.Cookies["Adminusername"].Value, SecurityComponent.Encrypt(Request.Cookies["Adminpassword"].Value));
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
                    }
                }
            }
        }

        public void BindVisitor()
        {
            VisitorComponent visitorcmp = new VisitorComponent();
            visitorcmp.Addvisitor(Request.RawUrl.ToString());
        }

    }
}
