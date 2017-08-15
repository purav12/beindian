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
    public partial class Default : System.Web.UI.Page
    {
        PostComponent PostClass = new PostComponent();
        TopicComponent Topicclass = new TopicComponent();
        CategoryComponent catcmp = new CategoryComponent();
        CommonDAC comdac = new CommonDAC();
        DataSet DsData = new DataSet();
        public string YoutubeLink = string.Empty;
        public string TwitterLink = string.Empty;
        public string FacebookLink = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCategoryDropDown();
                FillPostTypeDropdown();
                BindPost();
                BindStaticData();
                BindLinks();
            }
        }

        private void BindLinks()
        {
            YoutubeLink = AppLogic.AppConfigs("YoutubeLink").ToString();
            TwitterLink = AppLogic.AppConfigs("TwitterLink").ToString();
            FacebookLink = AppLogic.AppConfigs("FacebookLink").ToString();
        }

        /// <summary>
        /// Bind Category
        /// </summary>
        private void FillCategoryDropDown()
        {
            ddlCategory.Items.Clear();
            DsData = catcmp.GetCategory(3);

            int count = 1;
            ListItem LT2 = new ListItem();
            DataRow[] drCatagories = null;

            drCatagories = DsData.Tables[0].Select("ParentCategoryID = 0");

            if (DsData != null && drCatagories.Length > 0)
            {
                foreach (DataRow selDR in drCatagories)
                {
                    LT2 = new ListItem();
                    LT2.Text = "...|" + count + "|" + selDR["Name"].ToString();
                    LT2.Value = selDR["CategoryID"].ToString();
                    ddlCategory.Items.Add(LT2);
                    SetChildCategory(Convert.ToInt32(selDR["CategoryID"].ToString()), count);
                }
            }
            ddlCategory.Items.Insert(0, new ListItem("All Category", "0"));
        }

        /// <summary>
        /// Bind PostType
        /// </summary>
        private void FillPostTypeDropdown()
        {
            ddlPostType.Items.Clear();
            DsData = comdac.GetDropdownData(2);
            ddlPostType.DataSource = DsData;
            ddlPostType.DataTextField = "Name";
            ddlPostType.DataValueField = "PostTypeID";
            ddlPostType.DataBind();

            ddlPostType.Items.Insert(0, new ListItem("All Type", "0"));
        }

        private void SetChildCategory(int ID, int Number)
        {
            int count = Number;
            string st = "...";
            for (int i = 0; i < count; i++)
            {
                st += st;
            }
            DataRow[] drCatagories = null;
            drCatagories = DsData.Tables[0].Select("ParentCategoryID=" + ID.ToString());
            ListItem LT2;
            int innercount = 0;
            if (drCatagories.Length > 0)
            {
                innercount++;
                foreach (DataRow selDR in drCatagories)
                {
                    LT2 = new ListItem();
                    LT2.Text = st + "|" + (count + 1) + "|" + selDR["Name"].ToString();
                    LT2.Value = selDR["CategoryID"].ToString();
                    ddlCategory.Items.Add(LT2);
                    SetChildCategory(Convert.ToInt32(selDR["CategoryID"].ToString()), innercount + Number);
                }
            }
        }


        public void BindPost()
        {
            string strpostdata = string.Empty;
            DataSet PostDS = PostClass.GetIndexPagePost(1, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlPostType.SelectedValue), txtsearch.Text.Trim());
            if (PostDS != null && PostDS.Tables.Count > 0 && PostDS.Tables[0].Rows.Count > 0)
            {
                //Remove index to show top index
                for (int i = 0; i < PostDS.Tables[2].Rows.Count; i++)
                {
                    PostDS.Tables[0].Rows[i]["PostId"] = PostDS.Tables[2].Rows[i]["PostId"];
                }

                for (int i = 0; i < PostDS.Tables[0].Rows.Count; i++)
                {
                    hdnpostIds.Value += PostDS.Tables[0].Rows[i]["PostId"].ToString() + ",";
                }
                hdnpostIds.Value = hdnpostIds.Value.TrimEnd(',');
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

        public void BindStaticData()
        {
            DataSet dsTopicData = new DataSet();
            CommonDAC Commondac = new CommonDAC();
            dsTopicData = Topicclass.GetTopicList("IndexPageWelcomeText");
            if (dsTopicData != null && dsTopicData.Tables.Count > 0 && dsTopicData.Tables[0].Rows.Count > 0)
            {
                if (dsTopicData.Tables[0].Rows[0]["Description"].ToString() == "")
                {
                    ltrintrotext.Text = "";
                }
                else
                {
                    ltrintrotext.Text = dsTopicData.Tables[0].Rows[0]["Description"].ToString();
                }
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnpostIds.Value = "";
            BindPost();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            hdnpostIds.Value = "";
            BindPost();
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