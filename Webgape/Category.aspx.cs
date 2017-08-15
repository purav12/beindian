using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Category : System.Web.UI.Page
    {
        PostComponent PostClass = new PostComponent();
        TopicComponent Topicclass = new TopicComponent();
        CategoryComponent catcmp = new CategoryComponent();
        VisitorComponent visitcomp = new VisitorComponent();
        CommonDAC comdac = new CommonDAC();
        DataSet DsData = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CategoryId = 0;
                if (Request.QueryString["CategoryId"] != null)
                {
                    CategoryId = Convert.ToInt32(Request.QueryString["CategoryId"]);
                }
                BindSubCategory(CategoryId);
                BindPost(CategoryId);
                visitcomp.InsertPageVisitor("Category", CategoryId); 
            }
        }

        public void BindSubCategory(int CategoryId)
        {
            string CategoryData = string.Empty;
            CategoryComponent catcomp = new CategoryComponent();
            DataSet CategoryDS = catcomp.GetChildCatdetailbycatid(CategoryId);

            if (CategoryDS != null && CategoryDS.Tables.Count > 0 && CategoryDS.Tables[0].Rows.Count > 0)
            {
                ltrcatttitle.Text = CategoryDS.Tables[0].Rows[0]["Name"].ToString();
                //lblcategorytitle.Text = "Post From Category : " + CategoryDS.Tables[0].Rows[0]["Name"].ToString();
                lblsubcategorytitle.Text = "Subcategory Of : " + CategoryDS.Tables[0].Rows[0]["Name"].ToString();
            }

            if (CategoryDS != null && CategoryDS.Tables.Count > 1 && CategoryDS.Tables[1].Rows.Count > 0)
            {
                for (Int32 i = 0; i < CategoryDS.Tables[1].Rows.Count; i++)
                {
                    if (i % 4 == 0)
                    {
                        CategoryData += "<div class='one-fourth last'>";
                    }
                    else
                    {
                        CategoryData += "<div class='one-fourth last'>";
                    }

                    CategoryData += "<p>";
                    CategoryData += "<a href='/" + CategoryDS.Tables[1].Rows[i]["SENAME"].ToString() + "'> <img src='/Resources/BeIndian/Category/Icon/" + CategoryDS.Tables[1].Rows[i]["ImageName"].ToString() + "' alt=''></a>";
                    CategoryData += "</p>";
                    CategoryData += "<h2 class='title'><a href='/" + CategoryDS.Tables[1].Rows[i]["SENAME"].ToString() + "'>" + CategoryDS.Tables[1].Rows[i]["Name"].ToString() + "</a></h2>";
                    CategoryData += "</div>";
                }
            }
            else
            {
                divsubcat.Visible = false;
            }

            ltrcategory.Text = CategoryData;
        }

        public void BindPost(int CategoryId)
        {
            string strpostdata = string.Empty;
            int PostTypeID = 0;

            DataSet PostDS = PostClass.GetIndexPagePost(1, CategoryId, PostTypeID, "");
            if (PostDS != null && PostDS.Tables.Count > 0 && PostDS.Tables[0].Rows.Count > 0)
            {
                //Remove index to show top index
                for (int i = 0; i < PostDS.Tables[2].Rows.Count; i++)
                {
                    PostDS.Tables[0].Rows[i]["PostId"] = PostDS.Tables[2].Rows[i]["PostId"];
                }
                
                bindmsg.Visible = true;
                emptymsg.Visible = false;
                postcount.Value = PostDS.Tables[1].Rows[0]["RowCount"].ToString();
                RepPost.DataSource = PostDS;
                RepPost.DataBind();
            }
            else
            {
                bindmsg.Visible = false;
                emptymsg.Visible = true;
                postcount.Value = "0";
                RepPost.DataSource = null;
                RepPost.DataBind();
            }
        }


        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CategoryId = 0;
            if (Request.QueryString["CategoryId"] != null)
            {
                CategoryId = Convert.ToInt32(Request.QueryString["CategoryId"]);
            }
            BindPost(CategoryId);
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int CategoryId = 0;
            if (Request.QueryString["CategoryId"] != null)
            {
                CategoryId = Convert.ToInt32(Request.QueryString["CategoryId"]);
            }
            BindPost(CategoryId);
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
                                //string path = "//post//" + PostDS.Tables[0].Rows[i]["PostId"].ToString();
                                //strpostdata += "<div class='post format-video box' onclick=\"window.location.href='/post/1512'\"><div class='video frame'>";
                                if (PostDS.Tables[0].Rows[i]["VideoLink"].ToString().Contains("www.youtube.com/"))
                                {
                                    strpostdata += "<div class='post format-video box'><div class='video frame'>";
                                    strpostdata += "<iframe src='" + PostDS.Tables[0].Rows[i]["VideoLink"].ToString() + "' width='100%' height='281' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                                }
                                else
                                {
                                    strpostdata += "<div class='post format-video box'><div class='video localframe'>";
                                    strpostdata += "<video width='100%' controls><source src='" + PostDS.Tables[0].Rows[i]["VideoLink"].ToString() + "' type='video/mp4'></video>";
                                }
                                strpostdata += "</div>";
                                strpostdata += "<h2 class='title'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["Title"].ToString() + "</a></h2>";
                                strpostdata += PostDS.Tables[0].Rows[i]["SortDescription"].ToString();
                                strpostdata += "<div class='details'><span class='icon-video'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + Convert.ToDateTime(PostDS.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString("MMMM dd, yyyy") + "</a></span> <span class='likes'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "' class='likeThis'>" + PostDS.Tables[0].Rows[i]["ViewCount"].ToString() + "</a></span> <span class='comments'><a href='/post/" + PostDS.Tables[0].Rows[i]["PostId"].ToString() + "/" + PostDS.Tables[0].Rows[i]["SEName"].ToString() + "'>" + PostDS.Tables[0].Rows[i]["CommentCount"].ToString() + "</a></span> </div>";
                                strpostdata += "</div>";
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

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        public String SetNameWithNoDot(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67);
            return Server.HtmlEncode(Name);
        }
    }
}