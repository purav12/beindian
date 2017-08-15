using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebgapeClass;

namespace Webgape
{
    public class Global : System.Web.HttpApplication
    {
        public static bool IsApp = false;

        protected void Application_Start(object sender, EventArgs e)
        {
            AppLogic.ApplicationStart();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var url = Request.Url.AbsoluteUri;
            if (url.Contains("mobile.beindian.in"))
            {
                IsApp = true;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Request.UserHostAddress.ToString();
            string ipAddress = Request.UserHostAddress.ToString();
            if (ipAddress == "5.188.211.170" || ipAddress == "46.161.14.99" || ipAddress == "46.161.14.99")
            {
                Context.Response.Status = "403 Forbidden";
                Context.Response.StatusCode = 403;
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            bool isPostback = string.Equals(Request.ServerVariables["REQUEST_METHOD"], "POST");

            int ID = 0;
            string Name = string.Empty;
            string parentName = string.Empty;
            string grandName = string.Empty;
            string strCurrentPath = string.Empty;

            if (Request.RawUrl.ToString() == "/")
                Context.RewritePath("~/default.aspx");

            if (Request.RawUrl.ToString().ToLower().Contains("/admin/"))
            {
                if (!Request.Url.Authority.StartsWith("www.") && AppLogic.AppConfigBool("UseWWWRedirect"))
                {
                    Response.Redirect(Request.Url.Scheme + "://www." + Request.Url.Authority); //e.g  http://www.xyz.com
                }
                else
                {
                    Context.RewritePath(Request.RawUrl);
                    return;
                }
            }

            if (Request.RawUrl.ToString().ToLower().Contains("/login/"))
            {
                Context.RewritePath(Request.RawUrl);
                return;
            }

            //** Check for Redirect logic. 
            if (!Request.Url.Authority.StartsWith("www.") && AppLogic.AppConfigBool("UseWWWRedirect"))
            {
                Response.Redirect(Request.Url.Scheme + "://www." + Request.Url.Authority); //e.g  http://www.xyz.com
            }

            string FullPath = Server.UrlDecode(Request.RawUrl.ToLower());
            string AppPath = Request.Url.Scheme + "://" + Request.Url.Authority;

            if (AppLogic.AppConfigBool("UseLiveRewritePath"))
            {
                AppPath += ":" + Request.Url.Port + Request.ApplicationPath.ToLower();
            }
            else
            {
                AppPath += Request.ApplicationPath.ToLower();

            }

            string strExtension = System.IO.Path.GetExtension(Request.Url.ToString().Replace("\"", "%22").Replace("<", "").Replace(">", "").Replace("|", "")).ToLower();

            //Return when extension are .aspx and url found in physical path.
            try
            {
                if (strExtension.StartsWith(".aspx") && System.IO.File.Exists(Server.MapPath(Request.Url.AbsolutePath)) && (!Request.Url.AbsolutePath.StartsWith("/Redirect.aspx")))
                {
                    return;
                }
            }
            catch
            {
            }

            if (Request.RawUrl.ToString().ToLower().IndexOf("post.aspx") > -1)
            {
                Context.RewritePath("~/" + Request.RawUrl.ToString().Substring(Request.RawUrl.ToString().LastIndexOf("/") + 1, Request.RawUrl.ToString().Length - Request.RawUrl.ToString().LastIndexOf("/") - 1));
                return;
            }
            if (Request.RawUrl.ToString().ToLower().IndexOf("category.aspx") > -1)
            {
                Context.RewritePath("~/" + Request.RawUrl.ToString().Substring(Request.RawUrl.ToString().LastIndexOf("/") + 1, Request.RawUrl.ToString().Length - Request.RawUrl.ToString().LastIndexOf("/") - 1));
                return;
            }
            if (Request.RawUrl.ToString().ToLower().IndexOf("subcategory.aspx") > -1)
            {
                Context.RewritePath("~/" + Request.RawUrl.ToString().Substring(Request.RawUrl.ToString().LastIndexOf("/") + 1, Request.RawUrl.ToString().Length - Request.RawUrl.ToString().LastIndexOf("/") - 1));
                return;
            }

            //Return when JS,CSS or .axd files are found
            if (strExtension.StartsWith(".axd") || strExtension == ".js" || strExtension == ".css")
                return;

            //retuen when Image or Xml  files are found
            if (strExtension == ".jpg" || strExtension == ".gif" || strExtension == ".jpeg" || strExtension == ".png"
                || strExtension == ".xml")
            { return; }

            //Counvert into Lower Case
            strCurrentPath = Request.Path.ToLower();

            if (strCurrentPath.StartsWith("/facebook"))
            {
                HttpContext.Current.Response.Redirect("https://facebook.com/BeIndianOfficial");
            }
            else if (strCurrentPath.StartsWith("/youtube"))
            {
                HttpContext.Current.Response.Redirect("https://www.youtube.com/channel/UC-sKpou93MkXNE-NaE2b1EQ");
            }
			else if (strCurrentPath.StartsWith("/youttube"))
            {
                HttpContext.Current.Response.Redirect("https://www.youtube.com/channel/UC-sKpou93MkXNE-NaE2b1EQ");
            }
            else if (strCurrentPath.StartsWith("/twitter"))
            {
                HttpContext.Current.Response.Redirect("https://twitter.com/justbeindian");
            }

            //** Category 
            string[] Catgories = FullPath.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int TotalCount = Catgories.Length;
            bool isPost = false;//Set for Post details
            bool isUser = false;

            //** Check for Post
            if (TotalCount > 1 && FullPath.ToLower().Contains("/post/"))
                isPost = true;

            //** Check for User            
            if (TotalCount > 1 && FullPath.ToLower().Contains("/user/"))
                isUser = true;

            if (TotalCount > 2)
                grandName = Catgories[TotalCount - 3];
            if (TotalCount > 1)
                parentName = Catgories[TotalCount - 2];
            if (TotalCount > 0)
            {
                Name = Catgories[TotalCount - 1].Split("?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].ToString().Trim();
            }

            if (isUser)
            {
                CommonDAC cmd = new CommonDAC();
                string CheckUserId = Convert.ToString(cmd.GetScalarCommonData("SELECT ISNULL(AdminID,0) FROM tb_Admin WHERE AdminID='" + Name + "'"));

                if (CheckUserId != null && CheckUserId != "" && CheckUserId != "0")
                {
                    Context.RewritePath("/user.aspx?UID=" + Name);

                }
                else
                {
                    Context.RewritePath("/Rewriter.aspx");
                }
                return;
            }

            if (isPost)
            {
                CommonDAC cmd = new CommonDAC();
                string CheckPostId = string.Empty;
                int PostId = 0;

                if (TotalCount == 2)
                {
                    PostId = Convert.ToInt32(Name);
                }
                else
                {
                    PostId = Convert.ToInt32(parentName);
                }

                CheckPostId = Convert.ToString(cmd.GetScalarCommonData("SELECT ISNULL(PostID,0) FROM dbo.tb_post WHERE PostID='" + PostId + "'"));

                if (CheckPostId != null && CheckPostId != "" && CheckPostId != "0")
                {
                    Context.RewritePath("/post.aspx?PID=" + PostId);
                }
                else
                {
                    Context.RewritePath("/Rewriter.aspx");
                }
                return;
            }

            if (ID == 0 && !isPostback)
            {
                if (parentName == "")
                {
                    //First check if page exist with same name or not
                    CommonDAC cmd = new CommonDAC();
                    string CheckPageId = string.Empty;

                    CheckPageId = Convert.ToString(cmd.GetScalarCommonData("SELECT ISNULL(PageId,0) FROM dbo.tb_PageRedirection WHERE LinkName='" + Name + "'"));

                    if (CheckPageId != null && CheckPageId != "" && CheckPageId != "0")
                    {
                        Context.RewritePath("/page.aspx?PID=" + CheckPageId);
                        return;
                    }
                    //First check if page exist with same name or not

                    DataSet dsman = new DataSet();
                    CategoryComponent catcomp = new CategoryComponent();
                    int CategoryId = 0;

                    CategoryId = catcomp.GetCategoryId(Name);

                    if (CategoryId != 0)
                    {
                        Context.RewritePath("~/category.aspx?CategoryId=" + CategoryId.ToString());
                        return;
                    }
                }
            }
            if (strCurrentPath.Contains("/"))
            {
                DataSet dsTopic = new DataSet();
                TopicComponent TopicComponent = new TopicComponent();
                string id = strCurrentPath.Substring(1);
                dsTopic = TopicComponent.GetTopicList(id.Split('.')[0].ToString().Replace("-", " "));
                if (dsTopic != null && dsTopic.Tables.Count > 0 && dsTopic.Tables[0].Rows.Count > 0)
                {
                    Context.RewritePath("StaticPage.aspx?StaticPage=" + id.Split('.')[0].ToString().Replace("-", " "));
                    return;
                }
            }
            if (strCurrentPath.Contains("/redirect"))
            {
                if (Name.ToLower() == "youtube")
                {
                    HttpContext.Current.Response.Redirect("https://www.youtube.com/channel/UC-sKpou93MkXNE-NaE2b1EQ");
                }
                else if (Name.ToLower() == "facebook")
                {
                    HttpContext.Current.Response.Redirect("https://facebook.com/BeIndianOfficial");
                }
                else if (Name.ToLower() == "twitter")
                {
                    HttpContext.Current.Response.Redirect("https://twitter.com/justbeindian");
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            WebgapeClass.ErrorHandlerComponent handler = new WebgapeClass.ErrorHandlerComponent();
            handler.HandleException();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}