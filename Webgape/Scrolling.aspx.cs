using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Scrolling : System.Web.UI.Page
    {
        PostComponent PostClass = new PostComponent();
        public string displaytext;
        public int pagenumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "CallMyFunction", "OnLoad();", true);

            if (Request.QueryString["skip"] != null && Request.QueryString["take"] != null)
            {
                if (Request.QueryString["Popular"] != null)
                {
                    loadpopulargrid();
                }
                else if (Request.QueryString["Related"] != null)
                {
                    loadrelatedgrid();
                }
                else if (Request.QueryString["UserPost"] != null)
                {
                    loadusergrid();
                }
                else
                {
                    loadgrid();
                }

            }
        }

        public void loadgrid()
        {
            int rowcount = Convert.ToInt32(Request.QueryString["skip"]);
            int nextcount = Convert.ToInt32(Request.QueryString["take"]);
            int catid = 0, postid = 0;
            string searchvalue = string.Empty;
            string postids = string.Empty;
            if (Request.QueryString["catid"] != null)
            {
                catid = Convert.ToInt32(Request.QueryString["catid"]);
            }
            if (Request.QueryString["postid"] != null)
            {
                postid = Convert.ToInt32(Request.QueryString["postid"]);
            }
            if (Request.QueryString["searchvalue"] != null)
            {
                searchvalue = Request.QueryString["searchvalue"].ToString();
            }
            if (Request.QueryString["postids"] != null)
            {
                postids = Request.QueryString["postids"].ToString();
            }


            pagenumber = rowcount / 12 + 1;
            displaytext = "Post " + rowcount + " T0 " + nextcount;
            DataSet PostDS = PostClass.GetIndexPageScrollPost(3, rowcount, nextcount, catid, postid, postids, searchvalue); //Scroll Post

            for (int i = 0; i < PostDS.Tables[0].Rows.Count; i++)
            {
                hdnscrollpostId.Value += PostDS.Tables[0].Rows[i]["PostID"].ToString() + ",";
            }
            hdnscrollpostId.Value = hdnscrollpostId.Value.TrimEnd(',');

            RepPost.DataSource = PostDS;
            RepPost.DataBind();
        }

        public void loadpopulargrid()
        {
            int rowcount = Convert.ToInt32(Request.QueryString["skip"]);
            int nextcount = Convert.ToInt32(Request.QueryString["take"]);

            pagenumber = rowcount / 12 + 1;
            displaytext = "Post " + rowcount + " T0 " + nextcount;
            DataSet PostDS = PostClass.GetPopularPostCount(7, rowcount, nextcount);

            RepPost.DataSource = PostDS;
            RepPost.DataBind();
        }

        public void loadrelatedgrid()
        {
            int rowcount = Convert.ToInt32(Request.QueryString["skip"]);
            int nextcount = Convert.ToInt32(Request.QueryString["take"]);
            DataSet PostDS = new DataSet();
            pagenumber = rowcount / 12 + 1;
            displaytext = "Post " + rowcount + " T0 " + nextcount;
            if (Request.QueryString["PostId"] != null && Request.QueryString["PostId"] != "null")
            {
                PostDS = PostClass.GetRelatedPostCount(Convert.ToInt32(Request.QueryString["PostId"]), 9, rowcount, nextcount);
            }
            else
            {
                PostDS = null;
            }

            RepPost.DataSource = PostDS;
            RepPost.DataBind();
        }

        public void loadusergrid()
        {
            int rowcount = Convert.ToInt32(Request.QueryString["skip"]);
            int nextcount = Convert.ToInt32(Request.QueryString["take"]);
            string postids = string.Empty;
            if (Request.QueryString["postids"] != null)
            {
                postids = Request.QueryString["postids"].ToString();
            }

            DataSet PostDS = new DataSet();
            pagenumber = rowcount / 12 + 1;
            displaytext = "Post " + rowcount + " T0 " + nextcount;
            if (Request.QueryString["UserId"] != null && Request.QueryString["UserId"] != "null")
            {
                PostDS = PostClass.GetUserPostCount(Convert.ToInt32(Request.QueryString["UserId"]), 12, rowcount, nextcount);
            }
            if (Request.QueryString["UserIds"] != null && Request.QueryString["UserIds"] != "null")
            {
                PostDS = PostClass.GetUserPostForPage(Request.QueryString["UserIds"].ToString(), 14, rowcount, postids, nextcount);
                for (int i = 0; i < PostDS.Tables[0].Rows.Count; i++)
                {
                    hdnscrollpostId.Value += PostDS.Tables[0].Rows[i]["PostID"].ToString() + ",";
                }
                hdnscrollpostId.Value = hdnscrollpostId.Value.TrimEnd(',');
            }
            else
            {
                PostDS = null;
            }

            RepPost.DataSource = PostDS;
            RepPost.DataBind();
        }

        protected void RepPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrPostId = (Literal)e.Item.FindControl("ltrPostId");
                Literal ltrpostData = (Literal)e.Item.FindControl("ltrpostData");
                ltrpostData.Text = "";
                string strpostdata = "";

                CommonDAC commandac = new CommonDAC();

                DataSet PostDS = PostClass.GetPostByPostId(Convert.ToInt32(ltrPostId.Text), 3);

                if (PostDS != null && PostDS.Tables.Count > 0 && PostDS.Tables[0].Rows.Count > 0)
                {
                    for (Int32 i = 0; i < PostDS.Tables[0].Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(PostDS.Tables[0].Rows[i]["PostTypeId"].ToString()))
                        {
                            int PostType = Convert.ToInt16(PostDS.Tables[0].Rows[i]["PostTypeId"].ToString());
                            if (PostType == 1)//standard box
                            {
                                strpostdata += "<div class='post format-standard box'>";
                                strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetName(PostDS.Tables[0].Rows[i]["Title"].ToString()) + "</a></h2>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-standard'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";
                            }
                            else if (PostType == 2)//image box
                            {
                                strpostdata += "<div class='post format-image box'><div class='frame'>";
                                strpostdata += "<a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>";
                                strpostdata += "<img src='" + GetLargeImage(PostDS.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt=''></a>";
                                strpostdata += "</div>";
                                strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["Title"].ToString() + "</a></h2>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-image'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";
                            }
                            else if (PostType == 3)//video box
                            {

                                strpostdata += "<div class='post format-image box'><div class='frame'>";
                                strpostdata += "<a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>";
                                strpostdata += "<img src='" + GetLargeImage(PostDS.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt=''></a>";
                                strpostdata += "</div>";
                                strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["Title"].ToString() + "</a></h2>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-image'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";

                                ////Video
                                ////string path = "//post//" + PostDS.Tables[0].Rows[i]["PostId"].ToString();
                                ////strpostdata += "<div class='post format-video box' onclick=\"window.location.href='/post/1512'\"><div class='video frame'>";
                                //if (PostDS.Tables[0].Rows[i]["VideoLink"].ToString().Contains("www.youtube.com/"))
                                //{
                                //    strpostdata += "<div class='post format-video box'><div class='video frame'>";
                                //    strpostdata += "<iframe src='" + PostDS.Tables[0].Rows[i]["VideoLink"].ToString() + "' width='100%' height='281' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                                //}
                                //else
                                //{
                                //    strpostdata += "<div class='post format-video box'><div class='video localframe'>";
                                //    strpostdata += "<video width='100%' controls><source src='" + PostDS.Tables[0].Rows[i]["VideoLink"].ToString() + "' type='video/mp4'></video>";
                                //}
                                //strpostdata += "</div>";
                                //strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["Title"].ToString() + "</a></h2>";
                                //strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                //strpostdata += "<div class='details'><span class='icon-video'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                //strpostdata += "</div>";
                                ////Video
                            }
                            else if (PostType == 4)//audio box
                            {
                                strpostdata += "<div class='post format-audio box'><div class='audio-wrapper'><div class='vinyl'>";
                                strpostdata += "<dl>";
                                strpostdata += "<dt class='art'><img src='style/images/vinyl.png' alt='' class='highlight'><img src='style/images/art/artwork.png' alt=''></dt>";
                                strpostdata += "<dd class='song'><div class='icon-song'></div><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>Om Du Möter Varg</a></dd>";
                                strpostdata += "<dd class='artist'><div class='icon-artist'></div>Detektivbyrån</dd>";
                                strpostdata += "<dd class='album'><div class='icon-album'></div>Wermland</dd>";
                                strpostdata += "</dl>";
                                strpostdata += "</div>";
                                strpostdata += "<div class='clear'></div>";
                                strpostdata += "<div class='audio'>";
                                strpostdata += "<audio controls='' preload='none' src='Resources/BeIndian/Audio/" + PostDS.Tables[0].Rows[i]["AudioName"].ToString() + "'></audio>";
                                strpostdata += "</div></div>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-audio'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";
                            }
                            else
                            {
                                strpostdata += "<div class='post format-standard box'>";
                                strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetName(PostDS.Tables[0].Rows[i]["Title"].ToString()) + "</a></h2>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-standard'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";
                            }
                        }
                    }
                }
                ltrpostData.Text = strpostdata;
            }
        }

        public String GetLargeImage(String img)
        {
            clsvariables.LoadAllPath();
            String[] AllowedExtensions = AppLogic.AppConfigs("AllowedExtensions").Split(',');
            String imagepath = String.Empty;
            Random rd = new Random();
            imagepath = AppLogic.AppConfigs("ImagePathPost") + "Large/" + img;
            if (img != "")
            {
                if (File.Exists(Server.MapPath(imagepath)))
                {
                    //return imagepath + "?" + rd.Next(1000).ToString();
                    return imagepath;
                }
            }
            else
            {
                return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Large/image_not_available.jpg");
            }

            return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Large/image_not_available.jpg");
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

    }
}