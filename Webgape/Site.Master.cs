using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WebgapeClass;

namespace Webgape
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Declaration
        public bool IsApp;
        public string strGoogle;
        public string strChannelId;
        public string city;
        public string IP;
        public string country;
        public string Browser;
        public string Version;
        public string YoutubeLink = string.Empty;
        public string TwitterLink = string.Empty;
        public string FacebookLink = string.Empty;
        TopicComponent Topicclass = new TopicComponent();
        CategoryComponent catcmp = new CategoryComponent();
        PostComponent postcomp = new PostComponent();
        #endregion

        /// <summary>
        /// Page load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //if (Session["UserRecorded"] == null)
                //{
                //    if (Request.Cookies["VisitorId"] != null && !string.IsNullOrEmpty(Request.Cookies["VisitorId"].Value))
                //    {
                //        VisitorComponent visitorcmp = new VisitorComponent();
                //        visitorcmp.AddCookieVisitor(Convert.ToInt32(Request.Cookies["VisitorId"].Value));
                //        Session["UserRecorded"] = Request.Cookies["VisitorId"].Value;

                //        System.Web.HttpCookie VisitorCookie = new System.Web.HttpCookie("VisitorId", Session["UserRecorded"].ToString());
                //        VisitorCookie.Expires = DateTime.Now.AddYears(1);
                //        Response.Cookies.Add(VisitorCookie);
                //    }
                //    else
                //    {
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction();", true);
                //    }
                //}
                BindCategory();
                BindPopularPost();
                DoAutoLogin();
                BindLinks();
            }
            IsApp = Global.IsApp;
            strGoogle = Convert.ToString(AppLogic.AppConfigs("WebGapeGoogleAnalyticalScript"));//UA-54763665-1
            strChannelId = Convert.ToString(AppLogic.AppConfigs("YoutubeChannelId"));//UA-54763665-1            
            AddSiteConfig();
            //BindVisitor();
            BindActiveMenu();
        }

        private void BindLinks()
        {
            YoutubeLink = AppLogic.AppConfigs("YoutubeLink").ToString();
            TwitterLink = AppLogic.AppConfigs("TwitterLink").ToString();
            FacebookLink = AppLogic.AppConfigs("FacebookLink").ToString();
        }

        public void BindActiveMenu()
        {
            if (Request.Url.ToString().ToLower().IndexOf("category.aspx") > -1)
            {
                lihome.Attributes.Add("class", "");
                licategory.Attributes.Add("class", "active");
                licontact.Attributes.Add("class", "");
            }
            else if (Request.Url.ToString().ToLower().IndexOf("contact.aspx") > -1)
            {
                licontact.Attributes.Add("class", "active");
                lihome.Attributes.Add("class", "");
                licategory.Attributes.Add("class", "");
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

        public void AddOGTag(string Url, string Type, string Title, string Description, string Image)
        {
            System.Web.UI.HtmlControls.HtmlHead Head = (System.Web.UI.HtmlControls.HtmlHead)Page.Header;
            System.Web.UI.HtmlControls.HtmlMeta HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "og:url";
            HtmlMeta.Content = Url;
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "og:type";
            HtmlMeta.Content = Type;
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "og:title";
            HtmlMeta.Content = Title;
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "og:description";
            HtmlMeta.Content = Description;
            Head.Controls.Add(HtmlMeta);

            HtmlMeta = new System.Web.UI.HtmlControls.HtmlMeta();
            HtmlMeta.Name = "og:image";
            HtmlMeta.Content = Image;
            Head.Controls.Add(HtmlMeta);

        }

        /// <summary>
        /// To add title,keyword and description
        /// </summary>
        private void AddSiteConfig()
        {
            if (Request.Url.ToString().ToLower().IndexOf("subcategory.aspx") > -1)
            {
            }
            else if (Request.Url.ToString().ToLower().IndexOf("post.aspx") > -1)
            {
            }
            else if (Request.Url.ToString().ToLower().IndexOf("page.aspx") > -1)
            {
            }
            else
            {
                HeadTitle(AppLogic.AppConfigs("SiteSETitle"), AppLogic.AppConfigs("SiteSEKeywords"), AppLogic.AppConfigs("SiteSEDescription"));
            }
        }

        /// <summary>
        /// Bind category on header menu
        /// </summary>
        public void BindCategory()
        {
            DataSet DsCategory = new DataSet();
            string categorylist = "";
            DsCategory = catcmp.getHeaderCategory();
            if (DsCategory != null && DsCategory.Tables.Count > 0 && DsCategory.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drcat in DsCategory.Tables[0].Rows)
                {
                    categorylist += "<li><a href='/" + drcat["SEName"] + "'>" + drcat["Name"] + "</a></li>";
                }
                ltrcategory.Text = categorylist;
            }
            else
            {
                ltrcategory.Visible = false;
            }
        }

        /// <summary>
        /// To Bind Popular Post
        /// </summary>
        public void BindPopularPost()
        {
            DataSet DsPost = new DataSet();
            string strPopPostData = "";
            DsPost = postcomp.GetPopularPostCount(4);
            if (DsPost != null && DsPost.Tables.Count > 0 && DsPost.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < DsPost.Tables[0].Rows.Count; i++)
                {
                    strPopPostData += "<li><div class='frame'>";
                    strPopPostData += "<a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "'>";
                    strPopPostData += "<img src='" + GetIconImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    strPopPostData += "</a>";
                    strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    strPopPostData += "</div></li>";
                }
                ltrpopularpost.Text = strPopPostData;
            }
            else
            {
                ltrpopularpost.Visible = false;
            }
        }

        /// <summary>
        /// To Get Visitor's Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btngeo_Click(object sender, EventArgs e)
        {

            if (Session["UserRecorded"] == null)
            {
                if (Request.Cookies["VisitorId"] != null && !string.IsNullOrEmpty(Request.Cookies["VisitorId"].Value))
                {
                    Session["UserRecorded"] = Request.Cookies["VisitorId"].Value;
                }
                else
                {
                    VisitorComponent visitorcmp = new VisitorComponent();

                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                    }

                    //Old Code
                    //string json = hdnGeo.Value;

                    //var dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
                    //if (dictionary != null)
                    //{
                    //    city = dictionary["city"];
                    //    IP = dictionary["IP"];
                    //    country = dictionary["country"];
                    //}
                    //Old Code

                    //New Code 1
                    //Initializing a new xml document object to begin reading the xml file returned
                    //XmlDocument doc = new XmlDocument();
                    //doc.Load("https://www.freegeoip.net/xml/?q=" + ipAddress);
                    //XmlNodeList nodeLstCountryCode = doc.GetElementsByTagName("CountryCode");
                    //XmlNodeList nodeLstIP = doc.GetElementsByTagName("IP");
                    //XmlNodeList nodeLstCity = doc.GetElementsByTagName("City");

                    //city = nodeLstCity[0].InnerText;
                    //IP = ipAddress;
                    //country = nodeLstCountryCode[0].InnerText;
                    //New Code 1


                    //New Code 2
                    XmlDocument doc = new XmlDocument();
                    doc.Load("http://ip-api.com/xml/" + ipAddress);
                    XmlNodeList responseXML = doc.GetElementsByTagName("query");
                    try
                    {
                        city = responseXML.Item(0).ChildNodes[5].InnerText.ToString();
                        IP = ipAddress;
                        country = responseXML.Item(0).ChildNodes[2].InnerText.ToString();
                        //New Code 2
                    }
                    catch
                    {

                    }

                    Browser = Request.Browser.Browser;
                    Version = Request.Browser.Version;
                    Session["UserRecorded"] = visitorcmp.InsertMasterVisitor(IP, country, city, Browser, false);

                    System.Web.HttpCookie VisitorCookie = new System.Web.HttpCookie("VisitorId", Session["UserRecorded"].ToString());
                    VisitorCookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(VisitorCookie);
                }
            }
        }

        //public void BindVisitor()
        //{
        //    VisitorComponent visitorcmp = new VisitorComponent();
        //    DataSet Dsvisitor = new DataSet();
        //    Dsvisitor = visitorcmp.Addvisitor(Request.RawUrl.ToString());
        //    if (Dsvisitor != null && Dsvisitor.Tables.Count > 0 && Dsvisitor.Tables[0].Rows.Count > 0)
        //    {
        //        lblvisitors.Text = Dsvisitor.Tables[0].Rows[0]["Visitor"].ToString();
        //        lblpageviews.Text = Dsvisitor.Tables[0].Rows[0]["PageViews"].ToString();
        //        lblUniqueVisitor.Text = Dsvisitor.Tables[0].Rows[0]["UniqueVisitor"].ToString();
        //    }

        //    if (Session["UserRecorded"] != null)
        //    {
        //        DataSet DsvisitorDetails = new DataSet();

        //        DsvisitorDetails = visitorcmp.GetVisitorDetails(Convert.ToInt32(Session["UserRecorded"]));
        //        if (DsvisitorDetails != null && DsvisitorDetails.Tables.Count > 0 && DsvisitorDetails.Tables[0].Rows.Count > 0)
        //        {
        //            city = DsvisitorDetails.Tables[0].Rows[0]["City"].ToString();
        //            IP = DsvisitorDetails.Tables[0].Rows[0]["Ip"].ToString();
        //            country = DsvisitorDetails.Tables[0].Rows[0]["CountryName"].ToString();
        //        }
        //        Browser = Request.Browser.Browser;
        //        Version = Request.Browser.Version;
        //        //string json = hdnGeo.Value;
        //        //var dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
        //        //if (dictionary != null)
        //        //{
        //        //    city = dictionary["city"];
        //        //    IP = dictionary["IP"];
        //        //    country = dictionary["country"];
        //        //}
        //        //Browser = Request.Browser.Browser;
        //        //Version = Request.Browser.Version;
        //    }
        //}

        public String GetIconImage(String img)
        {
            clsvariables.LoadAllPath();
            String[] AllowedExtensions = AppLogic.AppConfigs("AllowedExtensions").Split(',');
            String imagepath = String.Empty;
            Random rd = new Random();
            imagepath = AppLogic.AppConfigs("ImagePathPost") + "Icon/" + img;
            if (img != "")
            {
                if (File.Exists(Server.MapPath(imagepath)))
                {
                    return imagepath;
                }
            }
            else
            {
                return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Icon/image_not_available.jpg");
            }

            return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Icon/image_not_available.jpg");
        }

        public String SetNameWithNoDot(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67);
            return Server.HtmlEncode(Name);
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        public String SetDescription(String Description)
        {
            if (Description.IndexOf("<br/>") > 0)
                Description = Description.Substring(0, Description.IndexOf("<br/>"));

            if (Description.Length > 200)
                Description = Description.Substring(0, 190) + "...";

            //return Server.HtmlEncode(Description);
            return Description;
        }

    }
}