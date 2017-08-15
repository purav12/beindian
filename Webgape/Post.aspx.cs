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
    public partial class Post : System.Web.UI.Page
    {
        #region Variable
        PostComponent postcomp = new PostComponent();
        AdminComponent admincomp = new AdminComponent();
        CommentComponent cmtcomp = new CommentComponent();
        SubscriptionComponent subcomp = new SubscriptionComponent();
        ConfigurationComponent configcomp = new ConfigurationComponent();
        CommonDAC commandac = new CommonDAC();
        public string strChannelId;
        public string Commencount;
        public string PostName;
        public string EntityId;
        //public string FacebookLink;        
        public string PagingCommencount;
        public int AdminId;
        private int PageSize = 12;
        string StartPathParent = "";
        string EndPathParent = "";
        string strSeNameJoin = "'";
        string StartPath = "";
        string EndPath = "";
        string StrReplace = "";
        string StrSEName = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pid"] != null)
                {
                    string Strpostid = Convert.ToString(Request.QueryString["PID"]);
                    int PostId = 0;
                    int.TryParse(Strpostid, out PostId);
                    GetPostDetails(PostId);
                    //AddVsisitor(PostId);
                    BindComment(PostId, 1);
                    PopulatePager(1);

                }
                breadcrumbs();
            }
            strChannelId = Convert.ToString(AppLogic.AppConfigs("YoutubeChannelId"));
            if (Request.QueryString["pid"] != null)
            {
                string Strpostid = Convert.ToString(Request.QueryString["PID"]);
                int PostId = 0;
                int.TryParse(Strpostid, out PostId);
                //GetSEDetails(PostId); Hide from here and added on GetPostDetails() to remove multiple addition on view count
                EntityId = Request.QueryString["pid"];
            }
        }

        #region BindPost Details
        private void breadcrumbs()
        {

            try
            {
                if (Request.QueryString["PID"] != null)
                {
                    String SelectQuery = "";
                    SelectQuery = " SELECT  top 1 tb_PostCategory.CategoryID,ParentCategoryID FROM tb_PostCategory " +
                    " INNER JOIN tb_Post ON tb_PostCategory.PostID = tb_Post.PostID  " +
                    " INNER JOIN tb_Category ON tb_PostCategory.CategoryID = tb_Category.CategoryID  " +
                    " INNER join tb_CategoryMapping On tb_Category.CategoryID= tb_CategoryMapping.CategoryID WHERE tb_Category.Deleted=0 and  (tb_PostCategory.PostID = " + Request.QueryString["PID"] + ") ";

                    DataSet dsCommon = new DataSet();
                    dsCommon = commandac.GetCommonDataSet(SelectQuery);

                    if (dsCommon != null && dsCommon.Tables.Count > 0 && dsCommon.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsCommon.Tables[0].Rows[0]["CategoryID"].ToString()) && !string.IsNullOrEmpty(dsCommon.Tables[0].Rows[0]["ParentCategoryID"].ToString()))
                        {
                            ltBreadcrmbs.Text = configcomp.GetBreadCrum(Convert.ToInt32(dsCommon.Tables[0].Rows[0]["CategoryID"].ToString()), Convert.ToInt32(dsCommon.Tables[0].Rows[0]["ParentCategoryID"].ToString()), "", 3, 0);
                        }
                    }
                }
                if (ltBreadcrmbs.Text != "")
                {
                    string FinalText = string.Empty;
                    if (ltBreadcrmbs.Text.LastIndexOf("<a href") > -1)
                    {
                        StartPathParent = ltBreadcrmbs.Text.Substring(0, ltBreadcrmbs.Text.LastIndexOf("<a href") - 1);
                        EndPathParent = ltBreadcrmbs.Text.Substring(ltBreadcrmbs.Text.LastIndexOf("'/"));
                        strSeNameJoin = EndPathParent.Remove(EndPathParent.IndexOf("' title="));
                    }
                    StartPath = ltBreadcrmbs.Text.Substring(0, ltBreadcrmbs.Text.LastIndexOf("<span>") - 1);
                    EndPath = ltBreadcrmbs.Text.Substring(ltBreadcrmbs.Text.LastIndexOf("<span>"));
                    StrReplace = EndPath.Replace("<span> ", "").Replace(" </span>", "");

                    StrSEName = CommonOperations.RemoveSpecialCharacter(StrReplace.Trim().Replace("&#32;", " ").ToCharArray());

                    FinalText = StartPath + " <a href='" + "/" + StrSEName.ToLower() + "' title='" + StrReplace + "'>" + StrReplace + "</a>";
                    ltBreadcrmbs.Text = FinalText.Replace(" > ", " &rarr; ");
                }
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("Item Page-breadcrumbs", ex.Message, ex.StackTrace);

            }
        }

        private void GetPostDetails(int PostId)
        {
            FillUserDetails();
            DataSet dspost = new DataSet();
            dspost = postcomp.GetPostByPostId(PostId, 1);
            if (dspost != null && dspost.Tables.Count > 0 && dspost.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["AdminId"].ToString()))
                {
                    AdminId = Convert.ToInt32(dspost.Tables[0].Rows[0]["AdminId"].ToString());
                    hdnownerid.Value = dspost.Tables[0].Rows[0]["AdminId"].ToString();
                    FillAdminDetails(AdminId);
                }

                if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["CreatedOn"].ToString()))
                {
                    ltrdatetime.Text = String.Format("{0:MMMMMMMMM dd, yyyy}", Convert.ToDateTime(dspost.Tables[0].Rows[0]["CreatedOn"]));
                }

                ltrfacebooklink.Text = "<div class='fb-comments' style='background-color: #fff;' data-width='600' data-href='" + dspost.Tables[0].Rows[0]["FacebookCommentLink"].ToString() + "' data-numposts='5'></div>";

                ltrposttitle.Text = dspost.Tables[0].Rows[0]["Title"].ToString();
                lblposttitle.Text = SetName(dspost.Tables[0].Rows[0]["Title"].ToString());
                PostName = lblposttitle.Text;
                lblvisitor.Text = dspost.Tables[0].Rows[0]["ViewCount"].ToString();
                ltrpostdescription.Text = dspost.Tables[0].Rows[0]["Description"].ToString();
                if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["PostType"].ToString()))
                {
                    string PostType = dspost.Tables[0].Rows[0]["PostType"].ToString();
                    if (PostType == "Image")
                    {
                        imagegllery.Visible = true;
                        BindMoreImage(dspost.Tables[0].Rows[0]["ImageName"].ToString(), "jpg");
                    }
                    else if (PostType == "Video")
                    {
                        divvidtype.Visible = true;
                        if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["VideoLink"].ToString()))
                        {
                            ltrvid.Text = "<iframe width='100%' height='400' src='" + dspost.Tables[0].Rows[0]["VideoLink"].ToString() + "&autoplay=1" + "' frameborder='0' allowfullscreen='1'></iframe>";
                        }

                    }
                    else if (PostType == "Audio")
                    {
                        if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["AudioName"].ToString()))
                        {
                            ltraudpath.Text = "<audio controls='' id='testaud' preload='none' src='/Resources/BeIndian/Audio/" + dspost.Tables[0].Rows[0]["AudioName"].ToString() + "'></audio>";
                        }
                        divaudtype.Visible = true;
                    }
                }


                DataSet dsnextprevpost = new DataSet();
                dsnextprevpost = postcomp.GetNextPerviousPost(PostId, 4);
                if (dsnextprevpost != null && dsnextprevpost.Tables.Count > 0 && dsnextprevpost.Tables[0].Rows.Count > 0)
                {
                    ltrprevnextpost.Text = "<span class='nav-next'><a href='/post/" + dsnextprevpost.Tables[0].Rows[0]["PostId"].ToString() + "'>" + dsnextprevpost.Tables[0].Rows[0]["Title"].ToString() + " &rarr;</a></span>";

                    if (dsnextprevpost.Tables[0].Rows.Count > 1)
                    {
                        ltrprevnextpost.Text += "<span class='nav-prev' ><a href='/post/" + dsnextprevpost.Tables[0].Rows[1]["PostId"].ToString() + "'>&larr; " + dsnextprevpost.Tables[0].Rows[1]["Title"].ToString() + " </a></span>";
                    }
                }

                BindPopularPost();
                BindRelatedPost(PostId);
                #region Get SeDetails
                try
                {
                    String SETitle = "";
                    String SEKeywords = "";
                    String SEDescription = "";
                    String Image = "";
                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SETitle"].ToString()))
                    {
                        SETitle = dspost.Tables[0].Rows[0]["SETitle"].ToString();
                    }
                    else
                    {
                        SETitle = AppLogic.AppConfigs("SiteSETitle").ToString();
                    }

                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SEKeywords"].ToString()))
                    {
                        SEKeywords = dspost.Tables[0].Rows[0]["SEKeywords"].ToString();
                    }
                    else
                    {
                        SEKeywords = AppLogic.AppConfigs("SiteSEKeywords").ToString();
                    }


                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SEDescription"].ToString()))
                    {
                        SEDescription = dspost.Tables[0].Rows[0]["SEDescription"].ToString();
                    }
                    else
                    {
                        SEDescription = AppLogic.AppConfigs("SiteSEDescription").ToString();
                    }

                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["ImageName"].ToString()))
                    {
                        Image = AppLogic.AppConfigs("LIVE_SERVER") + GetLargeImage(dspost.Tables[0].Rows[0]["ImageName"].ToString());
                    }

                    Master.HeadTitle(SETitle, SEKeywords, SEDescription);
                    Master.AddOGTag(AppLogic.AppConfigs("LIVE_SERVER") + "/" + "post/" + PostId, "Website", SETitle, SEDescription, Image);
                }
                catch { }
                #endregion
            }
            else
            {
                Response.Redirect("/");
            }

        }

        private void GetSEDetails(int PostId)
        {
            DataSet dspost = new DataSet();
            dspost = postcomp.GetPostByPostId(PostId, 1);
            if (dspost != null && dspost.Tables.Count > 0 && dspost.Tables[0].Rows.Count > 0)
            {
                try
                {
                    String SETitle = "";
                    String SEKeywords = "";
                    String SEDescription = "";
                    String Image = "";
                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SETitle"].ToString()))
                    {
                        SETitle = dspost.Tables[0].Rows[0]["SETitle"].ToString();
                    }
                    else
                    {
                        SETitle = AppLogic.AppConfigs("SiteSETitle").ToString();
                    }

                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SEKeywords"].ToString()))
                    {
                        SEKeywords = dspost.Tables[0].Rows[0]["SEKeywords"].ToString();
                    }
                    else
                    {
                        SEKeywords = AppLogic.AppConfigs("SiteSEKeywords").ToString();
                    }


                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["SEDescription"].ToString()))
                    {
                        SEDescription = dspost.Tables[0].Rows[0]["SEDescription"].ToString();
                    }
                    else
                    {
                        SEDescription = AppLogic.AppConfigs("SiteSEDescription").ToString();
                    }

                    if (!string.IsNullOrEmpty(dspost.Tables[0].Rows[0]["ImageName"].ToString()))
                    {
                        Image = AppLogic.AppConfigs("LIVE_SERVER") + GetLargeImage(dspost.Tables[0].Rows[0]["ImageName"].ToString());
                    }

                    Master.HeadTitle(SETitle, SEKeywords, SEDescription);
                    Master.AddOGTag(AppLogic.AppConfigs("LIVE_SERVER") + "/" + "post/" + PostId, "Website", SETitle, SEDescription, Image);
                }
                catch { }
            }

        }

        #region Image , Video , Audio Binding
        private void BindMoreImage(string ImageName, string strtitle)
        {
            try
            {
                string strElementClickedID = "";
                string strImageName = "";
                string strImgName = "";
                string strImageExt = "";
                StringBuilder strMoreImg = new StringBuilder();
                string strImagePath = "", strLImagePath = "", strMediumImagePath = "";
                strImgName = ImageName;
                string[] ar = new string[2];
                char[] splitter = { '.' };
                ar = strImgName.Split(splitter);
                strImageName = ar[ar.Length - 2].ToString();
                strImageExt = ar[ar.Length - 1].ToString();
                string strmore = Request.QueryString["PID"].ToString();
                strImagePath = GetMicroImage(strImageName + "." + strImageExt);
                strMediumImagePath = GetMoreMediumImage(strImageName + "." + strImageExt);
                strLImagePath = GetMoreLargeImage(strImageName + "." + strImageExt);

                string ImagePath = GetMicroImage(ImageName);
                string LargeImagePath = GetLargeImage(ImageName);
                string MediumImagePath = GetMediumImage(ImageName);


                int CountMoreImage = 0;


                strImagePath = GetLargeImage(strImageName + "_1." + strImageExt);


                if (ImagePath != "")
                {
                    if (File.Exists(Server.MapPath(ImagePath)) && !ImagePath.Contains("image_not_available"))
                    {
                        strMoreImg.Append(BindMoreImagesforZoom(LargeImagePath, MediumImagePath, LargeImagePath));
                    }
                }

                for (int cnt = 1; cnt < 4; cnt++)
                {

                    strImagePath = GetMicroImage(strImageName + "_" + cnt.ToString() + "." + strImageExt);
                    strMediumImagePath = GetMoreMediumImage(strImageName + "_" + cnt.ToString() + "." + strImageExt);
                    strLImagePath = GetMoreLargeImage(strImageName + "_" + cnt.ToString() + "." + strImageExt);
                    strMoreImg.Append(BindMoreImagesforZoom(strImagePath, strMediumImagePath, strLImagePath));
                }

                ltBindMoreImage.Text = strMoreImg.ToString();

            }
            catch { }
        }

        protected String BindMoreImagesforZoom(String ImagePath, String MediumImgPath, String LargeImgPath)
        {
            StringBuilder StrLtr = new StringBuilder(5000);
            if (MediumImgPath.Contains(AppLogic.AppConfigs("ImagePathPost") + "medium/" + "image_not_available.jpg") || ImagePath == "")
                return string.Empty;

            StrLtr.Append("<li> <div class='outer'> <span class='inset'> <img src=\"" + LargeImgPath + "\"  alt='' /></span> </div> </li>");


            return StrLtr.ToString();
        }
        #endregion

        public void FillAdminDetails(int AdminId)
        {
            DataSet dsadmin = new DataSet();
            dsadmin = admincomp.GetAdminProfileByAdminId(AdminId);
            if (dsadmin != null && dsadmin.Tables.Count > 0 && dsadmin.Tables[0].Rows.Count > 0)
            {
                //ltradminname.Text = dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString();
                ltradminname.Text = "<a href='/User.aspx?UID=" + Convert.ToInt32(dsadmin.Tables[0].Rows[0]["AdminID"].ToString()) + "' style='color: #ffffff; font-size: larger;' title='" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "'>" + dsadmin.Tables[0].Rows[0]["FirstName"].ToString() + " " + dsadmin.Tables[0].Rows[0]["LastName"].ToString() + "</a>";
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

        private void AddVsisitor(int PostId)
        {
            if (Session["UserRecorded"] != null)
            {
                VisitorComponent visitorcmp = new VisitorComponent();
                int subvisitor = 0;
                subvisitor = visitorcmp.InsertSubVisitor("Post", PostId, Convert.ToInt32(Session["UserRecorded"]));
            }
        }
        #endregion

        #region CommentBind
        private void BindComment(int PostId, int PageIndex)
        {
            ltrcomment.Text = "<li class='comment byuser comment-author-admin bypostauthor even thread-even depth-1' >";
            DataSet DSComment = new DataSet();
            DataSet DSChildComment = new DataSet();
            DataSet dsadmin = new DataSet();
            DSComment = cmtcomp.GetCommentByPostId(PostId, "Post", PageIndex, PageSize, 1); // Post's EntityTypeId = 1000
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
            string Strpostid = Convert.ToString(Request.QueryString["PID"]);
            int PostId = 0;
            int.TryParse(Strpostid, out PostId);
            BindComment(PostId, pageIndex);
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
            if (Description.IndexOf("<br/>") > 0)
                Description = Description.Substring(0, Description.IndexOf("<br/>"));

            if (Description.Length > 90)
                Description = Description.Substring(0, 85) + "...";
            return Server.HtmlEncode(Description);
        }

        protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
        {
            if (txtcomment.Text.Trim() != "")
            {
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                e.IsValid = Captcha1.UserValidated;
                if (e.IsValid)
                {
                    int ParentId = Convert.ToInt32(Page.Request.Form["comment_parent"]);
                    int commentid = 0;
                    if (Request.QueryString["pid"] != null)
                    {
                        commentid = cmtcomp.InsertComment(Convert.ToInt32(hdnownerid.Value), Convert.ToInt32(Request.QueryString["pid"]), "Post", txtname.Text.Trim(), txtemail.Text.Trim(), Convert.ToInt32(Session["AdminID"]), ParentId, txtcomment.Text.Trim());
                        if (commentid > 0)
                        {
                            BindComment(Convert.ToInt32(Request.QueryString["pid"]), 1);
                            txtcomment.Text = "";
                            dvstatus.Visible = true;
                        }

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('Captcha is not valid.', 'Message');});", true);
                }
            }
        }

        protected void btnsubmitcomment_Click(object sender, EventArgs e)
        {
            int ParentId = Convert.ToInt32(Page.Request.Form["comment_parent"]);
            int commentid = 0;
            if (Request.QueryString["pid"] != null)
            {
                commentid = cmtcomp.InsertComment(Convert.ToInt32(hdnownerid.Value), Convert.ToInt32(Request.QueryString["pid"]), "Post", txtname.Text.Trim(), txtemail.Text.Trim(), Convert.ToInt32(Session["AdminID"]), ParentId, txtcomment.Text.Trim());
                if (commentid > 0)
                {
                    BindComment(Convert.ToInt32(Request.QueryString["pid"]), 1);
                    txtcomment.Text = "";
                    dvstatus.Visible = true;
                }

            }
        }

        #region GetImages
        public String GetMediumImage(String img)
        {
            clsvariables.LoadAllPath();
            String[] AllowedExtensions = AppLogic.AppConfigs("AllowedExtensions").Split(',');
            String imagepath = String.Empty;
            Random rd = new Random();
            imagepath = AppLogic.AppConfigs("ImagePathPost") + "Medium/" + img;
            if (img != "")
            {
                if (File.Exists(Server.MapPath(imagepath)))
                {
                    return imagepath + "?" + rd.Next(1000).ToString();
                }
            }
            else
            {
                return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Medium/image_not_available.jpg");
            }

            return string.Concat(AppLogic.AppConfigs("ImagePathPost") + "Medium/image_not_available.jpg");
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

        public String GetMicroImage(String img)
        {
            String imagepath = String.Empty;
            String Temp = AppLogic.AppConfigs("ImagePathPost") + "micro/" + img;
            imagepath = Temp;
            if (File.Exists(Server.MapPath(imagepath)))
            {
                return imagepath;
            }

            return AppLogic.AppConfigs("ImagePathPost") + "micro/" + "image_not_available.jpg";
        }

        private String GetMoreMediumImage(String img)
        {
            String imagepath = String.Empty;
            String Temp = imagepath = AppLogic.AppConfigs("ImagePathPost") + "medium/" + img;

            imagepath = Temp;

            if (File.Exists(Server.MapPath(imagepath)))
            {
                return imagepath;
            }

            return AppLogic.AppConfigs("ImagePathPost") + "medium/" + "image_not_available.jpg";
        }

        private String GetMoreLargeImage(String img)
        {
            String imagepath = String.Empty;
            String Temp = imagepath = AppLogic.AppConfigs("ImagePathPost") + "Large/" + img;

            imagepath = Temp;

            if (File.Exists(Server.MapPath(imagepath)))
            {
                return imagepath;
            }

            return AppLogic.AppConfigs("ImagePathPost") + "Large/" + "image_not_available.jpg";
        }
        #endregion

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
                    //strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "</div><div style='float: left;'>";
                    strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    //strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    strPopPostData += "</div></li>";

                    //strPopPostData += "<li><div class='frame'>";
                    //strPopPostData += "<a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "'>";
                    //strPopPostData += "<img src='" + GetMicroSideBarImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    //strPopPostData += "</a>";
                    //strPopPostData += "</div><div class='meta'>";
                    //strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetSideName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    //strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    //strPopPostData += "</div></li>";
                }
                ltrpopularpost.Text = strPopPostData;
            }
            else
            {
                ltrpopularpost.Visible = false;
            }
        }

        public void BindRelatedPost(int PostId)
        {
            DataSet DsPost = new DataSet();
            string strPopPostData = "";
            DsPost = postcomp.GetRelatedPostCount(PostId, 5);
            if (DsPost != null && DsPost.Tables.Count > 0 && DsPost.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < DsPost.Tables[0].Rows.Count; i++)
                {
                    strPopPostData += "<li><div class='frame'>";
                    strPopPostData += "<a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "'>";
                    strPopPostData += "<img src='" + GetIconImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    strPopPostData += "</a>";
                    //strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "</div><div style='float: left;'>";
                    strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    //strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    strPopPostData += "</div></li>";


                    //strPopPostData += "<li><div class='frame'>";
                    //strPopPostData += "<a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "'>";
                    //strPopPostData += "<img src='" + GetMicroSideBarImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    //strPopPostData += "</a>";
                    //strPopPostData += "</div><div class='meta'>";
                    //strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetSideName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    //strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
                    //strPopPostData += "</div></li>";
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

        public String SetSideName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        protected void btnsubscribe_Click(object sender, EventArgs e)
        {
            int subadded = 0;
            if (Request.QueryString["pid"] != null)
            {
                subadded = subcomp.InsertSubscription(txtsubscribe.Text.Trim(), Convert.ToInt32(Request.QueryString["pid"]), "Post");
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