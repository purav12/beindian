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
    public partial class User : System.Web.UI.Page
    {
        #region Variable
        PostComponent postcomp = new PostComponent();
        AdminComponent admincomp = new AdminComponent();
        CommentComponent cmtcomp = new CommentComponent();
        CommonDAC cmd = new CommonDAC();
        SubscriptionComponent subcomp = new SubscriptionComponent();
        ConfigurationComponent configcomp = new ConfigurationComponent();
        CommonDAC commandac = new CommonDAC();
        public string strChannelId;
        public string Commencount;
        public string PostName;
        public string EntityId;
        public string PagingCommencount;
        public int AdminId;
        private int PageSize = 12;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UID"] != null)
                {
                    

                    string Struserid = Convert.ToString(Request.QueryString["UID"]);
                    string CheckPageId = Convert.ToString(cmd.GetScalarCommonData("SELECT ISNULL(PageId,0) FROM tb_Admin WHERE AdminID='" + Struserid + "'"));

                    if (CheckPageId != null && CheckPageId != "" && CheckPageId != "0")
                    {
                        Response.Redirect("page.aspx?PID=" + CheckPageId);
                    }

                    int UserId = 0;
                    int.TryParse(Struserid, out UserId);
                    GetUserDetails(UserId);
                    AddVsisitor(UserId);
                    BindComment(UserId, 1);
                    PopulatePager(1);
                }
            }
            strChannelId = Convert.ToString(AppLogic.AppConfigs("YoutubeChannelId"));
            if (Request.QueryString["UID"] != null)
            {
                EntityId = Request.QueryString["UID"];
            }
        }

        #region BindPost Details

        private void GetUserDetails(int UserId)
        {
            FillUserDetails();
            string ProfilePath = string.Empty;
            string ProfileAvtarPath = string.Empty;

            DataSet dsadmin = new DataSet();
            admincomp.AddAdminView(UserId);
            dsadmin = admincomp.GetAdminProfileByAdminId(UserId);
            if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["AdminID"].ToString()))
                {
                    AdminId = Convert.ToInt32(dsadmin.Tables[0].Rows[0]["AdminID"].ToString());
                    hdnownerid.Value = dsadmin.Tables[0].Rows[0]["AdminID"].ToString();
                    ltradminname.Text = dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString();
                    ltrbreadcumb.Text = "<a href='/User.aspx?UID=" + Convert.ToInt32(dsadmin.Tables[0].Rows[0]["AdminID"].ToString()) + "' title='" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "'>" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "</a>";
                }

                if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["CreateDate"].ToString()))
                {
                    ltrdatetime.Text = String.Format("{0:MMMMMMMMM dd, yyyy}", Convert.ToDateTime(dsadmin.Tables[0].Rows[0]["CreateDate"]));
                }

                ltrfacebooklink.Text = "<div class='fb-comments' style='background-color: #fff;' data-width='600' data-href='" + dsadmin.Tables[0].Rows[0]["FacebookCommentLink"].ToString() + "' data-numposts='5'></div>";

                ltrposttitle.Text = dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString();
                lblposttitle.Text = SetName(dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString());
                lblfullname.Text = dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + " (" + dsadmin.Tables[0].Rows[0]["UserName"].ToString() + ")";

                //lblemail.Text = dsadmin.Tables[0].Rows[0]["EmailID"].ToString();
                lblemail.Text = "Hidden";
                lbladmintype.Text = dsadmin.Tables[0].Rows[0]["AdminType"].ToString();
                lblbalance.Text = dsadmin.Tables[0].Rows[0]["Earnings"].ToString();
                lblpoint.Text = dsadmin.Tables[0].Rows[0]["Points"].ToString();
                lblcreatedate.Text = String.Format("{0:MMMMMMMMM dd, yyyy}", Convert.ToDateTime(dsadmin.Tables[0].Rows[0]["CreateDate"]));

                PostName = lblposttitle.Text;
                lblvisitor.Text = dsadmin.Tables[0].Rows[0]["ViewCount"].ToString();

                ProfilePath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Avtar/");
                ProfileAvtarPath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Avtar/");


                if (Convert.ToBoolean(dsadmin.Tables[0].Rows[0]["IsPic"]))
                {
                    if (!File.Exists(Server.MapPath(ProfileAvtarPath) + AdminId + ".png"))
                    {
                        imagelink.HRef = ProfilePath + "image_not_available.jpg";
                        ImgLarge.Src = ProfilePath + "image_not_available.jpg";
                    }
                    else
                    {
                        ImgLarge.Src = ProfilePath + AdminId + ".png";
                        imagelink.HRef = ProfilePath + AdminId + ".png";
                    }
                }
                else
                {
                    ImgLarge.Src = "/admin/assets/avatars/" + dsadmin.Tables[0].Rows[0]["ImageName"] + ".png";
                    imagelink.HRef = "/admin/assets/avatars/" + dsadmin.Tables[0].Rows[0]["ImageName"] + ".png";
                }

                BindPopularPost();
                BindRelatedPost(UserId);
                try
                {
                    String SETitle = "";
                    String SEKeywords = "";
                    String SEDescription = "";
                    if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["SETitle"].ToString()))
                    {
                        SETitle = dsadmin.Tables[0].Rows[0]["SETitle"].ToString();
                    }
                    else
                    {
                        SETitle = AppLogic.AppConfigs("SiteSETitle").ToString();
                    }

                    if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["SEKeywords"].ToString()))
                    {
                        SEKeywords = dsadmin.Tables[0].Rows[0]["SEKeywords"].ToString();
                    }
                    else
                    {
                        SEKeywords = AppLogic.AppConfigs("SiteSEKeywords").ToString();
                    }


                    if (!string.IsNullOrEmpty(dsadmin.Tables[0].Rows[0]["SEDescription"].ToString()))
                    {
                        SEDescription = dsadmin.Tables[0].Rows[0]["SEDescription"].ToString();
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

        private void AddVsisitor(int UserId)
        {
            if (Session["UserRecorded"] != null)
            {
                VisitorComponent visitorcmp = new VisitorComponent();
                int subvisitor = 0;
                subvisitor = visitorcmp.InsertSubVisitor("Admin", UserId, Convert.ToInt32(Session["UserRecorded"]));
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
            string StrUserId = Convert.ToString(Request.QueryString["UID"]);
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
            if (Request.QueryString["UID"] != null)
            {
                commentid = cmtcomp.InsertComment(Convert.ToInt32(hdnownerid.Value), Convert.ToInt32(Request.QueryString["UID"]), "Admin", txtname.Text.Trim(), txtemail.Text.Trim(), Convert.ToInt32(Session["AdminID"]), ParentId, txtcomment.Text.Trim());
                if (commentid > 0)
                {
                    BindComment(Convert.ToInt32(Request.QueryString["UID"]), 1);
                    txtcomment.Text = "";
                    dvstatus.Visible = true;
                }

            }
        }

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
                    strPopPostData += "<img src='" + GetMicroSideBarImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    strPopPostData += "</a>";
                    strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "'>" + SetSideName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
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

        public void BindRelatedPost(int UserId)
        {
            DataSet dsadmin = new DataSet();
            string strPopPostData = "";
            dsadmin = postcomp.GetRelatedPostCount(UserId, 5);
            if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < dsadmin.Tables[0].Rows.Count; i++)
                {
                    strPopPostData += "<li><div class='frame'>";
                    strPopPostData += "<a href='/post/" + dsadmin.Tables[0].Rows[i]["PostId"].ToString() + "'>";
                    strPopPostData += "<img src='" + GetMicroSideBarImage(dsadmin.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    strPopPostData += "</a>";
                    strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "<h6><a href='/post/" + dsadmin.Tables[0].Rows[i]["PostId"].ToString() + "'>" + SetSideName(dsadmin.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    strPopPostData += "<em>" + SetDescription(dsadmin.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    strPopPostData += "</div></li>";
                }
                ltrrelatedpost.Text = strPopPostData;
            }
            else
            {
                ltrrelatedpost.Visible = false;
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
            if (Request.QueryString["UID"] != null)
            {
                Response.Redirect("/Admin/Profile/Message.aspx?ToId=" + Request.QueryString["UID"].ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailMessage", "$(document).ready( function() {jAlert('Something worng, Please try again later.', 'Message');});", true);
                return;
            }
        }

        protected void btnallpost_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["UID"] != null)
            {
                Response.Redirect("/UserPost.aspx?UserId=" + Request.QueryString["UID"].ToString());
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailMessage", "$(document).ready( function() {jAlert('Something worng, Please try again later.', 'Message');});", true);
                return;
            }
        }

        protected void btnsubscribe_Click(object sender, EventArgs e)
        {
            int subadded = 0;
            if (Request.QueryString["UID"] != null)
            {
                subadded = subcomp.InsertSubscription(txtsubscribe.Text.Trim(), Convert.ToInt32(Request.QueryString["UID"]), "Admin");
            }
            if (subadded > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('You have subscribed to this post successfully.', 'Message');});", true);
                return;
            }
            else if (subadded == -1)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('You have already subscribed to this post successfully.', 'Message');});", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailInsert", "$(document).ready( function() {jAlert('Subscription Failed, Please try again.', 'Message');});", true);
                return;
            }
        }

    }
}
