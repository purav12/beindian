using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StringBuilder = System.Text.StringBuilder;
using File = System.IO.File;
using StreamWriter = System.IO.StreamWriter;
using WebgapeClass;

namespace Webgape.Admin.Posts
{
    public partial class GenerateGoogleXML : System.Web.UI.Page
    {
        #region Declaration

        TopicComponent topicComp = new TopicComponent();
        CommonDAC commdac = new CommonDAC();
        DataSet dsCategory = null;
        StringBuilder sitemap = new StringBuilder();
        String catCSSClass = String.Empty;
        int currentCategoryId = 0;
        int parentCategoryID = 0;
        bool showActive = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            btnGenerate.Attributes.Add("onclick", "return confirm('This will Delete Previously generated Sitemap.xml file, Are you Sure?');");
        }

        //private void chkRights()
        //{
        //    Boolean isModify = false;
        //    isModify = AdminRightsComponent.GetAdminRights(Convert.ToInt32(Session["AdminID"]), "SEO");
        //    if (btnGenerate.Visible == true) btnGenerate.Visible = isModify;
        //}


        /// <summary>
        /// Generate XML
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Server.MapPath("~/Sitemap.xml")))
                System.IO.File.Delete(Server.MapPath("~/Sitemap.xml"));

            sitemap.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sitemap.Append(" <urlset \n");
            sitemap.Append(" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" \n");
            sitemap.Append(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" \n");
            sitemap.Append(" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 \n");
            sitemap.Append(" http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\"> \n");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + "</loc><changefreq>weekly</changefreq><priority>1.00</priority></url>");
            GetPosts();
        }

        protected void btnReGenerate_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Server.MapPath("~/ErrorLog.xml")))
                System.IO.File.Delete(Server.MapPath("~/ErrorLog.xml"));


            sitemap.Append("<?xml version=\"1.0\"?> \n");
            sitemap.Append("<Root> \n");
            sitemap.Append("</Root> \n");
            StreamWriter sw = new StreamWriter(System.IO.File.Open(Server.MapPath("~/ErrorLog.xml"), System.IO.FileMode.Create));
            sw.Write(sitemap.ToString());
            sw.Close();
            Page.RegisterStartupScript("Msg", "<script type='text/javascript' lang='javascript'>alert('Error Log Recreated Successfully!');</script>");
        }


        /// <summary>
        /// Get Products and Product Categories
        /// </summary>
        public void GetPosts()
        {
            try
            {
                dsCategory = commdac.GetCommonDataSet("SELECT CategoryID,Name,SEName,ParentCategoryID,DisplayOrder FROM MDV_Category ORDER BY DisplayOrder ASC");
                GetPostDetail();
                GetPageDetail();
                GenerateSitemap();
                BindAdminPages();
                sitemap.Append("</urlset>");
                StreamWriter sw = new StreamWriter(System.IO.File.Open(Server.MapPath("~/Sitemap.xml"), System.IO.FileMode.Create));
                sw.Write(sitemap.ToString());
                sw.Close();
                Page.RegisterStartupScript("Msg", "<script type='text/javascript' lang='javascript'>alert('Google XML file created Successfully!');</script>");
            }
            catch { }
        }


        /// <summary>
        /// Generate SiteMap file Xml for the Topic
        /// </summary>
        private void GenerateSitemap()
        {
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Contact.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/PopularPost.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/RelatedPost.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/SignUp.aspx</loc><changefreq>weekly</changefreq><priority>0.85</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Login.aspx</loc><changefreq>weekly</changefreq><priority>0.85</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/ForgotPassword.aspx</loc><changefreq>weekly</changefreq><priority>0.85</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Visitor.aspx</loc><changefreq>weekly</changefreq><priority>0.80</priority></url>");

            //Admin Pages
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Dashboard.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Profile/Profile.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Profile/MessageList.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Profile/NotificationList.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Profile/Earning.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Profile/Point.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Posts/Postlist.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Posts/Post.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Posts/CommentList.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Posts/GenerateGoogleXML.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Users/Userlist.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Content/SubscriptionList.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Content/TestimonialList.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Settings/ProfilePageConfiguration.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Settings/PostPageConfiguration.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");

            int indexno = 0;
            foreach (DataRow selDR in dsCategory.Tables[0].Select("ParentCategoryID=0", "displayorder asc"))
            {
                if (parentCategoryID.ToString() == selDR["CategoryID"].ToString())
                {
                    if (currentCategoryId == parentCategoryID && showActive)
                    {
                        sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().Trim() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                        Session["ParCatID"] = parentCategoryID;
                    }
                    else
                        sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                    //Bind Sub Category below the Parent Category
                    if (Session["ParCatID"] != null)
                        SetChildCategory(Session["ParCatID"].ToString(), selDR["SEName"].ToString(), 0);
                    else
                        SetChildCategory(selDR["CategoryID"].ToString(), selDR["SENAME"].ToString(), 0);
                }
                else
                {
                    sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                    SetChildCategory(selDR["CategoryID"].ToString(), selDR["SENAME"].ToString(), 0);
                }
                indexno++;
            }
        }

        /// <summary>
        /// Set Child category
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="SENAME">SENAME</param>
        /// <param name="iii">iii</param>
        /// <returns>string</returns>
        public string SetChildCategory(string ID, string SENAME, Int32 iii)
        {
            if (dsCategory.Tables[0].Select("ParentCategoryID=" + ID).Length > 0)
            {
                //Bind Subcategories under the particular Parent Category
                DataRow[] selRows = dsCategory.Tables[0].Select("ParentCategoryID=" + ID, "displayorder asc");

                string strPage = Request.Url.AbsolutePath.ToLower();
                showActive = (strPage == "/category.aspx" || strPage == "/subcategory.aspx" || strPage == "/itempage.aspx");

                int index = 0;
                if (selRows != null && selRows.Length > 0)
                {
                    foreach (DataRow selDR in selRows)
                    {
                        if (dsCategory.Tables[0].Select("ParentCategoryID=" + selDR["CategoryID"].ToString()).Length > 0)
                        {
                            if (currentCategoryId.ToString() == selDR["CategoryID"].ToString() && showActive)
                                sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                            else
                                sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");

                            string str = SetChildCategory(selDR["CategoryID"].ToString(), SENAME + "/" + selDR["SEName"].ToString(), 1);

                        }
                        else
                        {
                            if (currentCategoryId.ToString() == selDR["CategoryID"].ToString() && showActive)
                            {
                                if (SENAME.ToString().ToLower().Contains("shop-by-"))
                                {
                                    sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                                }
                                else
                                {
                                    sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                                }
                            }
                            else
                            {
                                if (SENAME.ToString().ToLower().Contains("shop-by-"))
                                {
                                    sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                                }
                                else
                                {
                                    sitemap.AppendLine("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + selDR["SEName"].ToString().ToLower() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                                }
                            }
                        }

                        index++;
                    }
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// Get Product detail
        /// </summary>
        public void GetPostDetail()
        {
            try
            {
                DataSet dsProduct = new DataSet();
                dsProduct = commdac.GetCommonDataSet("SELECT P.MainCategory,P.SEName,P.PostID FROM MDV_Post P WHERE P.Deleted <> 1 AND P.Active = 1 ORDER BY P.PostID ASC");
                if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0 && dsProduct.Tables[0] != null)
                {
                    foreach (DataRow dr in dsProduct.Tables[0].Rows)
                    {
                        String str = SetSEName(dr["SEName"].ToString());
                        sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/post/" + dr["PostID"].ToString() + "/"+ str + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                        //sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/post.aspx?PID=" + dr["PostID"].ToString() + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                    }
                }
            }
            catch { }

        }

        public void GetPageDetail()
        {
            try
            {
                DataSet dsPage = new DataSet();
                dsPage = commdac.GetCommonDataSet("SELECT LinkName FROM tb_PageRedirection WHERE GenerateXML = 1");
                if (dsPage != null && dsPage.Tables.Count > 0 && dsPage.Tables[0].Rows.Count > 0 && dsPage.Tables[0] != null)
                {
                    foreach (DataRow dr in dsPage.Tables[0].Rows)
                    {
                        sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + dr["LinkName"].ToString() + "</loc><changefreq>weekly</changefreq><priority>0.99</priority></url>");
                    }
                }
            }
            catch { }

        }

        public void BindAdminPages()
        {
            sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/Admin/Dashboard.aspx</loc><changefreq>weekly</changefreq><priority>0.90</priority></url>");
        }

        /// <summary>
        /// This function set Any String to format of SEName 
        /// </summary>
        /// <param name="Word">String for SEName</param>
        /// <returns>SEName</returns>
        public static String SetSEName(String Word)
        {
            String Temp = "";
            Temp = Word.Replace("\"", "-").ToString().Replace(":", "-").Replace("!", "-").Replace("@", "-").Replace("?", "-").Replace("%", "-").Replace("*", "").Replace("$", "-").ToString();
            return Temp;
        }

        /// <summary>
        /// Append the Xml text in the Xml file
        /// </summary>
        /// <param name="CatID">Category Id</param>
        /// <param name="CatName">CategoryName</param>
        /// <param name="CategoryLevel">Category Level</param>
        private void WriteSubCategory(String CatID, String CatName, int CategoryLevel, bool isRoot)
        {
            if (isRoot)
                CatName = "";
            else
                CatName += "/";
            string SubCatName = string.Empty;
            if (dsCategory.Tables[0].Select("ParentCategoryID=" + CatID).Length > 0)
            {
                foreach (DataRow CatRW in dsCategory.Tables[0].Select("ParentCategoryID=" + CatID))
                {
                    switch (CategoryLevel)
                    {
                        case 1:
                            catCSSClass = "category";
                            break;
                        case 2:
                            catCSSClass = "sub_category";
                            break;
                        case 3:
                            catCSSClass = "item";
                            break;
                        default:
                            return;
                    }


                    sitemap.Append("<url><loc>" + AppLogic.AppConfigs("LIVE_SERVER") + "/" + SubCatName + "</loc><changefreq>weekly</changefreq><priority>0.95</priority></url>");
                    if (CategoryLevel <= 3)
                        WriteSubCategory(CatRW["CategoryID"].ToString().Trim(), SubCatName, CategoryLevel + 1, false);
                }
            }
        }
    }
}