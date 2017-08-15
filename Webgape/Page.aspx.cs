using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Net.Mail;

namespace Webgape
{
    public partial class Page : System.Web.UI.Page
    {
        #region Variable
        PostComponent postcomp = new PostComponent();
        PageComponent pagecomp = new PageComponent();
        AdminComponent admincomp = new AdminComponent();
        CommentComponent cmtcomp = new CommentComponent();
        CommonDAC commandac = new CommonDAC();
        public string strChannelId;
        public string Commencount;
        public string PostName;
        public string EntityId;
        public string PagingCommencount;
        public int AdminId;
        public string AdminIds;
        private int PageSize = 12;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PID"] != null)
                {
                    string Strpageid = Convert.ToString(Request.QueryString["PID"]);
                    int PageId = 0;
                    int.TryParse(Strpageid, out PageId);
                    var Result = GetPageDetails(PageId);
                    AdminId = Result.Item1;
                    AdminIds = Result.Item2;
                    BindPost(AdminIds);
                    BindComment(AdminId, 1);
                    PopulatePager(1);
                }
            }
            strChannelId = Convert.ToString(AppLogic.AppConfigs("YoutubeChannelId"));
            if (Request.QueryString["PID"] != null)
            {
                EntityId = hdnpageadminid.Value;
            }
        }

        #region BindPost Details

        private Tuple<Int32,string> GetPageDetails(int PageId)
        {
            FillUserDetails();
            string ProfilePath = string.Empty;
            string ProfileAvtarPath = string.Empty;
            Int32 AdminId = 0;
            String AdminIds = string.Empty;

            DataSet dspage = new DataSet();
            dspage = pagecomp.GetPageDetailsByPageId(PageId);
            if (dspage != null && dspage.Tables.Count > 0 && dspage.Tables[0].Rows.Count > 0)
            {
                AdminId = Convert.ToInt32(dspage.Tables[0].Rows[0]["AdminId"]);
                hdnpageadminid.Value = dspage.Tables[0].Rows[0]["AdminId"].ToString();
                hdnownerid.Value = dspage.Tables[0].Rows[0]["AdminId"].ToString();
                AdminIds = dspage.Tables[0].Rows[0]["AdminIds"].ToString();

                lblposttitle.Text = dspage.Tables[0].Rows[0]["Title"].ToString();
                ltrposttitle.Text = dspage.Tables[0].Rows[0]["Title"].ToString();
                ltrdescription.Text = dspage.Tables[0].Rows[0]["Description"].ToString();
                if (Convert.ToBoolean(dspage.Tables[0].Rows[0]["IsOfficial"]))
                {
                    ltrposttitle.Text = "&nbsp;" + ltrposttitle.Text;
                    official.Visible = true;
                }

                if (dspage.Tables.Count > 1 && dspage.Tables[0].Rows.Count > 0)
                {
                    lblvisitor.Text = dspage.Tables[1].Rows[0]["ViewCount"].ToString();
                }

                DataSet dsadmin = new DataSet();
                dsadmin = admincomp.GetAdminProfileByAdminId(AdminId);
                if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["AdminID"].ToString()))
                    {
                        ltrbreadcumb.Text = "<a href='/User.aspx?UID=" + Convert.ToInt32(dsadmin.Tables[0].Rows[0]["AdminID"].ToString()) + "' title='" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "'>" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "</a>";
                    }
                }

                ImgLarge.Src = "/admin/assets/page/" + dspage.Tables[0].Rows[0]["ImageName"] + ".png";
                imagelink.HRef = "/admin/assets/page/" + dspage.Tables[0].Rows[0]["ImageName"] + ".png";

                try
                {
                    String SETitle = "";
                    String SEKeywords = "";
                    String SEDescription = "";
                    if (!string.IsNullOrEmpty(dspage.Tables[0].Rows[0]["SETitle"].ToString()))
                    {
                        SETitle = dspage.Tables[0].Rows[0]["SETitle"].ToString();
                    }
                    else
                    {
                        SETitle = AppLogic.AppConfigs("SiteSETitle").ToString();
                    }

                    if (!string.IsNullOrEmpty(dspage.Tables[0].Rows[0]["SEKeywords"].ToString()))
                    {
                        SEKeywords = dspage.Tables[0].Rows[0]["SEKeywords"].ToString();
                    }
                    else
                    {
                        SEKeywords = AppLogic.AppConfigs("SiteSEKeywords").ToString();
                    }


                    if (!string.IsNullOrEmpty(dspage.Tables[0].Rows[0]["SEDescription"].ToString()))
                    {
                        SEDescription = dspage.Tables[0].Rows[0]["SEDescription"].ToString();
                    }
                    else
                    {
                        SEDescription = AppLogic.AppConfigs("SiteSEDescription").ToString();
                    }
                    Master.HeadTitle(SETitle, SEKeywords, SEDescription);
                }
                catch { }


            }
            else
            {
                Response.Redirect("/");
            }

            var tuple = new Tuple<int, string>(AdminId, AdminIds);
            return tuple;
        }

        public void FillUserDetails()
        {
            DataSet dsadmin = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsadmin = admincomp.GetAdminProfileByAdminId(Convert.ToInt32(Session["AdminID"]));
                if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
                {
                    txtname.Text = dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString();
                    txtemail.Text = dsadmin.Tables[0].Rows[0]["EmailID"].ToString();

                }
            }
        }
        #endregion

        #region CommentBind
        private void BindComment(int UserId, int PageIndex)
        {
            ltrcomment.Text = "<li class='comment byuser comment-author-admin bypostauthor even thread-even depth-1' >";
            DataSet DSComment = new DataSet();
            DataSet DSChildComment = new DataSet();
            DataSet dsadmin = new DataSet();
            DSComment = cmtcomp.GetCommentByPostId(UserId, "Admin", PageIndex, PageSize, 1); // Post's EntityTypeId = 1000
            if (DSComment != null && DSComment.Tables.Count > 0 && DSComment.Tables[0].Rows.Count > 0)
            {
                lblcommentcount.Text = DSComment.Tables[1].Rows[0]["RowCount"].ToString();
                PagingCommencount = DSComment.Tables[2].Rows[0]["PaggingRowCount"].ToString();
                foreach (DataRow drcomment in DSComment.Tables[0].Rows)
                {
                    int CommentId = Convert.ToInt32(drcomment["CommentId"].ToString());
                    int AdminId = Convert.ToInt32(drcomment["AdminId"].ToString());
                    string ImageName = string.Empty;
                    string Commentby = string.Empty;
                    dsadmin = admincomp.GetAdminProfileByAdminId(AdminId);
                    if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
                    {
                        ImageName = dsadmin.Tables[0].Rows[0]["ImageName"].ToString() + ".png";
                        Commentby = dsadmin.Tables[0].Rows[0]["FirstName"].ToString();
                    }
                    else
                    {
                        Random randnumber = new Random();
                        ImageName = randnumber.Next(12, 16).ToString() + ".png";

                    }
                    ltrcomment.Text += "<div id='comment-" + CommentId + "' class='com-wrap'>";
                    ltrcomment.Text += "<div class='comment-author vcard user frame'>";
                    ltrcomment.Text += "<img alt='' src='/admin/assets/avatars/" + ImageName + "' class='avatar avatar-70 photo' height='70' width='70' />";
                    ltrcomment.Text += "</div>";
                    ltrcomment.Text += "<div class='message'>";
                    ltrcomment.Text += "<span class='reply-link'><a class='comment-reply-link'  href='?replytocom=" + CommentId + "#respond' onclick='return addComment.moveForm(\"comment-" + CommentId + "\", \"" + CommentId + "\", \"respond\", \"13\")'>Reply</a></span>";
                    ltrcomment.Text += "<div class='info'>";
                    ltrcomment.Text += "<h2>" + drcomment["Name"].ToString() + "</h2>";
                    ltrcomment.Text += "<span class='meta'>" + drcomment["CreatedOn"].ToString() + "</span>";
                    ltrcomment.Text += "</div>";
                    ltrcomment.Text += "<div class='comment-body '>";
                    ltrcomment.Text += "<p>" + drcomment["Comments"].ToString() + "</p>";
                    ltrcomment.Text += "</div>";
                    ltrcomment.Text += "<span class='edit-link'></span>";
                    ltrcomment.Text += "</div>";
                    ltrcomment.Text += "<div class='clear'></div>";
                    ltrcomment.Text += "</div>";
                    ltrcomment.Text += "<div class='clear'></div>";

                    #region comment
                    if (Convert.ToBoolean(drcomment["HasChild"].ToString()))
                    {
                        DSChildComment = cmtcomp.GetChildCommentByPostId(CommentId, 2);
                        if (DSChildComment != null && DSChildComment.Tables.Count > 0 && DSChildComment.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drchildcomment in DSChildComment.Tables[0].Rows)
                            {
                                int childCommentId = Convert.ToInt32(drchildcomment["CommentId"]);
                                AdminId = Convert.ToInt32(drchildcomment["AdminId"]);
                                ImageName = string.Empty;
                                Commentby = string.Empty;
                                dsadmin = admincomp.GetAdminProfileByAdminId(AdminId);
                                if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
                                {
                                    ImageName = dsadmin.Tables[0].Rows[0]["ImageName"].ToString() + ".png";
                                    Commentby = dsadmin.Tables[0].Rows[0]["FirstName"].ToString();
                                }
                                else
                                {
                                    Random randnumber = new Random();
                                    ImageName = randnumber.Next(12, 16).ToString() + ".png";

                                }
                                ltrcomment.Text += "<ul class='children'>";
                                ltrcomment.Text += "<li class='comment odd alt depth-2'>";
                                ltrcomment.Text += "<div id='comment-" + childCommentId + "' class='com-wrap'>";
                                ltrcomment.Text += "<div class='comment-author vcard user frame'>";
                                ltrcomment.Text += "<img alt='' src='/admin/assets/avatars/" + ImageName + "' class='avatar avatar-70 photo' height='70' width='70' />";
                                ltrcomment.Text += "</div>";
                                ltrcomment.Text += "<div class='message'>";
                                ltrcomment.Text += "<span class='reply-link'><a class='comment-reply-link'  href='?replytocom=" + childCommentId + "#respond' onclick='setcommentid(" + CommentId + ");return addComment.moveForm(\"comment-" + childCommentId + "\", \"" + childCommentId + "\", \"respond\", \"13\")'>Reply</a></span>";
                                ltrcomment.Text += "<div class='info'>";
                                ltrcomment.Text += "<h2>" + drchildcomment["Name"].ToString() + "</h2>";
                                ltrcomment.Text += "<span class='meta'>" + drchildcomment["CreatedOn"].ToString() + "</span>";
                                ltrcomment.Text += "</div>";
                                ltrcomment.Text += "<div class='comment-body '>";
                                ltrcomment.Text += "<p>" + drchildcomment["Comments"].ToString() + "</p>";
                                ltrcomment.Text += "</div>";
                                ltrcomment.Text += "<span class='edit-link'></span>";
                                ltrcomment.Text += "</div>";
                                ltrcomment.Text += "<div class='clear'></div>";
                                ltrcomment.Text += "</div>";
                                ltrcomment.Text += "<div class='clear'></div>";
                                ltrcomment.Text += "</li>";
                                ltrcomment.Text += "</ul>";
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                lblcommentcount.Text = "No ";
            }
            ltrcomment.Text += "</li>";
            //ltrcomment.Text = ltrcomment.Text.ToString().Replace("'",""");
        }

        private void PopulatePager(int currentPage)
        {
            double dblPageCount = (double)(Convert.ToDecimal(PagingCommencount) / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();

            rptPagertop.DataSource = pages;
            rptPagertop.DataBind();
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            string StrUserId = Convert.ToString(hdnpageadminid.Value);
            int UserId = 0;
            int.TryParse(StrUserId, out UserId);
            BindComment(UserId, pageIndex);
            PopulatePager(pageIndex);
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ScrollToComment();", true);
        }

        #endregion CommentBind

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        public String SetDescription(String Description)
        {
            if (Description.Length > 90)
                Description = Description.Substring(0, 85) + "...";
            return Server.HtmlEncode(Description);
        }

        protected void btnsubmitcomment_Click(object sender, EventArgs e)
        {
            int ParentId = Convert.ToInt32(Page.Request.Form["comment_parent"]);
            int commentid = 0;
            
            if (hdnpageadminid.Value != null)
            {
                commentid = cmtcomp.InsertComment(Convert.ToInt32(hdnownerid.Value), Convert.ToInt32(hdnpageadminid.Value), "Admin", txtname.Text.Trim(), txtemail.Text.Trim(), Convert.ToInt32(Session["AdminID"]), ParentId, txtcomment.Text.Trim());
                if (commentid > 0)
                {
                    BindComment(Convert.ToInt32(hdnpageadminid.Value), 1);
                    txtcomment.Text = "";
                    dvstatus.Visible = true;
                }

            }
        }

        public String GetMicroSideBarImage(String img)
        {
            clsvariables.LoadAllPath();
            String[] AllowedExtensions = AppLogic.AppConfigs("AllowedExtensions").Split(',');
            String imagepath = String.Empty;
            Random rd = new Random();
            imagepath = AppLogic.AppConfigs("ImagePathPost") + "Micro/" + img;
            if (img != "")
            {
                if (File.Exists(Server.MapPath(imagepath)))
                {
                    return imagepath;
                }
            }
            else
            {
                return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Micro/image_not_available.jpg");
            }

            return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Micro/image_not_available.jpg");
        }

        public String SetNameWithNoDot(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67);
            return Server.HtmlEncode(Name);
        }

        public String SetSideName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }



        protected void btnmessage_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PID"] != null)
            {
                Response.Redirect("/Admin/Profile/Message.aspx?ToId=" + Request.QueryString["PID"].ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailMessage", "$(document).ready( function() {jAlert('Something worng, Please try again later.', 'Message');});", true);
                return;
            }
        }

        protected void btnallpost_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PID"] != null)
            {
                Response.Redirect("/UserPost.aspx?UserId=" + Request.QueryString["PID"].ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailMessage", "$(document).ready( function() {jAlert('Something worng, Please try again later.', 'Message');});", true);
                return;
            }
        }

        public void BindPost(string UserIds)
        {
            string strpostdata = string.Empty;
            DataSet PostDS = postcomp.GetUserPostForPage(UserIds, 13);
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

        protected void RepPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal ltrPostId = (Literal)e.Item.FindControl("ltrPostId");
                Literal ltrpostData = (Literal)e.Item.FindControl("ltrpostData");
                ltrpostData.Text = "";
                string strpostdata = "";

                CommonDAC commandac = new CommonDAC();

                DataSet PostDS = postcomp.GetPostByPostId(Convert.ToInt32(ltrPostId.Text), 3);

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


    }
}
