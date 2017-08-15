using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.ChipsSeller
{
    public partial class Profile : System.Web.UI.Page
    {
        #region Variable
        PostComponent postcomp = new PostComponent();
        AdminComponent admincomp = new AdminComponent();
        CommentComponent cmtcomp = new CommentComponent();
        SubscriptionComponent subcomp = new SubscriptionComponent();
        CommonDAC commandac = new CommonDAC();
        public string strChannelId;
        public string Commencount;
        public string PostName;
        public string PagingCommencount;
        public int AdminId;
        private int PageSize = 12;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddVisitor();
                FillUserDetails();
                AddVsisitor();
                BindComment(1);
                BindDetails();
                PopulatePager(1);
                FillStatusTill();

                Master.FindControl("headertext").Visible = false;
                Master.FindControl("headericon").Visible = false;
            }
            strChannelId = Convert.ToString(AppLogic.AppConfigs("YoutubeChannelId"));
        }

        public void BindDetails()
        {
            DataSet dsdetails = new DataSet();
            dsdetails = commandac.GetCommonDataSet("SELECT * FROM tb_ChipsSellerStatus");
            if (dsdetails != null && dsdetails.Tables.Count > 0 && dsdetails.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsdetails.Tables[0].Rows.Count; i++)
                {

                    if (dsdetails.Tables[0].Rows[i]["Title"].ToString() == "Thanks")
                    {
                        lblTagLine.Text = dsdetails.Tables[0].Rows[i]["Description"].ToString();
                    }
                    else if (dsdetails.Tables[0].Rows[i]["Title"].ToString() == "VisitorCount")
                    {
                        lblvisitor.Text = dsdetails.Tables[0].Rows[i]["Description"].ToString();
                    }
                    else if (dsdetails.Tables[0].Rows[i]["Title"].ToString() == "AdminName")
                    {
                        ltradminname.Text = dsdetails.Tables[0].Rows[i]["Description"].ToString();
                        hdnownerid.Value = dsdetails.Tables[0].Rows[i]["Text"].ToString();
                    }
                    else if (dsdetails.Tables[0].Rows[i]["Title"].ToString() == "Date")
                    {
                        ltrdatetime.Text = String.Format("{0:MMMMMMMMM dd, yyyy}", Convert.ToDateTime(dsdetails.Tables[0].Rows[i]["Description"]));
                    }
                }
            }
            BindRelatedPost();
        }

        public void AddVisitor()
        {
            commandac.ExecuteCommonData("UPDATE tb_ChipsSellerStatus SET Description = Description + 1 WHERE Title = 'VisitorCount'");
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcountry.SelectedIndex == 0)
            {
                liindian.Visible = true;
                othercon.Visible = false;
            }
            else
            {
                liindian.Visible = false;
                othercon.Visible = true;
            }
        }

        #region Bind Details
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

        private void AddVsisitor()
        {
            if (Session["UserRecorded"] != null)
            {
                VisitorComponent visitorcmp = new VisitorComponent();
                int subvisitor = 0;
                subvisitor = visitorcmp.InsertSubVisitor("ChipsSeller", 0, Convert.ToInt32(Session["UserRecorded"]));
            }
        }
        #endregion

        #region CommentBind
        private void BindComment(int PageIndex)
        {
            ltrcomment.Text = "<li class='comment byuser comment-author-admin bypostauthor even thread-even depth-1' >";
            DataSet DSComment = new DataSet();
            DataSet DSChildComment = new DataSet();
            DataSet dsadmin = new DataSet();
            DSComment = cmtcomp.GetCommentByPostId(0, "ChipsSeller", PageIndex, PageSize, 1); // Post's EntityTypeId = 1000
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
            BindComment(pageIndex);
            PopulatePager(pageIndex);
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ScrollToComment();", true);
        }

        protected void btnsubmitcomment_Click(object sender, EventArgs e)
        {
            int ParentId = Convert.ToInt32(Page.Request.Form["comment_parent"]);
            int commentid = 0;

            commentid = cmtcomp.InsertComment(Convert.ToInt32(hdnownerid.Value), 0, "ChipsSeller", txtname.Text.Trim(), txtemail.Text.Trim(), Convert.ToInt32(Session["AdminID"]), ParentId, txtcomment.Text.Trim());
            if (commentid > 0)
            {
                BindComment(1);
                txtcomment.Text = "";
                dvstatus.Visible = true;
            }
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

        public void BindRelatedPost()
        {
            DataSet DsPost = new DataSet();
            string strPopPostData = "";
            DsPost = postcomp.GetPopularPostCount(10); ;
            if (DsPost != null && DsPost.Tables.Count > 0 && DsPost.Tables[0].Rows.Count > 0)
            {
                for (Int32 i = 0; i < DsPost.Tables[0].Rows.Count; i++)
                {
                    strPopPostData += "<li><div class='frame'>";
                    strPopPostData += "<a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>";
                    strPopPostData += "<img src='" + GetMicroSideBarImage(DsPost.Tables[0].Rows[i]["ImageName"].ToString()) + "' alt='' />";
                    strPopPostData += "</a>";
                    strPopPostData += "</div><div class='meta'>";
                    strPopPostData += "<h6><a href='/post/" + DsPost.Tables[0].Rows[i]["PostId"].ToString() + "/" + DsPost.Tables[0].Rows[i]["SEName"].ToString() + "'>" + SetSideName(DsPost.Tables[0].Rows[i]["Title"].ToString()) + "</a></h6>";
                    strPopPostData += "<em>" + SetDescription(DsPost.Tables[0].Rows[i]["SortDescription"].ToString()) + "</em>";
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SQLAccessChips cslac = new SQLAccessChips();
            string t1;

            string City = "", IP = "", Country = "";
            try
            {
                HiddenField hdnID = (HiddenField)Page.Master.FindControl("hdnGeo");

                var dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(hdnID.Value);
                if (dictionary != null)
                {
                    City = dictionary["city"];
                    IP = dictionary["IP"];
                    Country = dictionary["country"];
                }

            }
            catch
            {

            }

            try
            {
                cslac.ExecuteNonQuery("INSERT INTO WebgapeLog (ViewedOn,Type, Statement, City, Country, IP) VALUES (DATEADD(MINUTE,(750),GETDATE()),'2','PIN used " + txtpin.Text + " for Profile view','" + City + "','" + Country + "','" + IP + "')");
            }
            catch
            {

            }

            dt = cslac.Getdata("select Customer_Name from Rate Where SpecialPin='" + txtpin.Text + "'");

            if (dt != null && dt.Rows.Count != 0)
            {
                useronly.Visible = true;
                dvpin.Visible = true;
                lblTagLine.Visible = true;
                t1 = dt.Rows[0]["Customer_Name"].ToString();

                ViewState["Customer_Name"] = t1;

                dt = cslac.Getdata("select NewMobile,NewEmail,ISNULL(OUTSIDEINDIA,'No') AS OUTSIDEINDIA,Location from Customer Where Name ='" + t1 + "'");
                txtmobno.Text = dt.Rows[0]["NewMobile"].ToString(); ;
                txtemailid.Text = dt.Rows[0]["NewEmail"].ToString();
                if (dt.Rows[0]["OUTSIDEINDIA"].ToString() == "Yes")
                {
                    othercon.Visible = true;
                    liindian.Visible = false;
                    txtlocation.Text = dt.Rows[0]["Location"].ToString();
                }
                else
                {
                    try
                    {
                        othercon.Visible = false;
                        liindian.Visible = true;
                        drpcitylist.SelectedValue = dt.Rows[0]["Location"].ToString(); ;
                    }
                    catch
                    {

                    }
                }
                lblpinstatus.Visible = false;
                divpin.Visible = false;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('PIN does not Exists.', 'Message','');});", true);
                return;
            }
        }

        protected void btnpin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SQLAccessChips cslac = new SQLAccessChips();

            string City = "", IP = "", Country = "";
            try
            {
                HiddenField hdnID = (HiddenField)Page.Master.FindControl("hdnGeo");

                var dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(hdnID.Value);
                if (dictionary != null)
                {
                    City = dictionary["city"];
                    IP = dictionary["IP"];
                    Country = dictionary["country"];
                }

            }
            catch
            {

            }

            try
            {
                cslac.ExecuteNonQuery("INSERT INTO WebgapeLog (ViewedOn,Type, Statement, City, Country, IP) VALUES (DATEADD(MINUTE,(750),GETDATE()),'4','PIN updated with " + txtnewpin.Text + ".','" + City + "','" + Country + "','" + IP + "')");
            }
            catch
            {

            }

            if (txtnewpin.Text.Length < 5)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('PIN length must be greater then 5.', 'Message','');});", true);
                return;
            }
            if (txtnewpin.Text == txtconfirmnewpin.Text)
            {
                dt = cslac.Getdata("update Rate set SpecialPin = '" + txtnewpin.Text + "'  WHERE Customer_Name = '" + ViewState["Customer_Name"].ToString() + "' ");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('New PIN and Confirm New PIN should be same.', 'Message','');});", true);
                return;
            }
            divstatus.Visible = true;
            lblstatusd.Text = "PIN updated successfully.";
        }

        protected void btnprofile_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SQLAccessChips cslac = new SQLAccessChips();

            string City = "", IP = "", Country = "";
            try
            {
                HiddenField hdnID = (HiddenField)Page.Master.FindControl("hdnGeo");

                var dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(hdnID.Value);
                if (dictionary != null)
                {
                    City = dictionary["city"];
                    IP = dictionary["IP"];
                    Country = dictionary["country"];
                }

            }
            catch
            {

            }

            try
            {
                if (ddlcountry.SelectedIndex == 0)
                {
                    cslac.ExecuteNonQuery("INSERT INTO WebgapeLog (ViewedOn,Type, Statement, City, Country, IP) VALUES (DATEADD(MINUTE,(750),GETDATE()),'3','Customer Details updated with: NewMobile = " + txtmobno.Text + " AND NewEmail = " + txtemailid.Text + " AND OUTSIDEINDIA= No AND  Location = " + drpcitylist.SelectedValue + " " + txtnewpin.Text + ".','" + City + "','" + Country + "','" + IP + "')");
                }
                else
                {
                    cslac.ExecuteNonQuery("INSERT INTO WebgapeLog (ViewedOn,Type, Statement, City, Country, IP) VALUES (DATEADD(MINUTE,(750),GETDATE()),'3','Customer Details updated with: NewMobile = " + txtmobno.Text + " AND NewEmail = " + txtemailid.Text + " AND OUTSIDEINDIA= Yes AND  Location = " + drpcitylist.SelectedValue + " " + txtnewpin.Text + ".','" + City + "','" + Country + "','" + IP + "')");
                }
            }
            catch
            {

            }


            if (ddlcountry.SelectedIndex == 0)
            {
                dt = cslac.Getdata("update Customer set NewMobile = '" + txtmobno.Text + "',NewEmail = '" + txtemailid.Text + "',OUTSIDEINDIA='No',Location = '" + drpcitylist.SelectedValue + "' WHERE NAME = '" + ViewState["Customer_Name"].ToString() + "' ");
            }
            else
            {
                dt = cslac.Getdata("update Customer set NewMobile = '" + txtmobno.Text + "',NewEmail = '" + txtemailid.Text + "',,OUTSIDEINDIA='Yes',Location = '" + txtlocation.Text + "' WHERE NAME = '" + ViewState["Customer_Name"].ToString() + "' ");
            }
            divstatus.Visible = true;
            lblstatusd.Text = "Profile updated successfully.";
        }



        protected void btnsubscribe_Click(object sender, EventArgs e)
        {
            int subadded = 0;

            subadded = subcomp.InsertSubscription(txtsubscribe.Text.Trim(), 0, "ChipsSeller");
            if (subadded > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('You have subscribed to this page successfully.', 'Message');});", true);
                return;
            }
            else if (subadded == -1)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('You have already subscribed to this page.', 'Message');});", true);
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailInsert", "$(document).ready( function() {jAlert('Subscription Failed, Please try again.', 'Message');});", true);
                return;
            }
        }

        public void FillStatusTill()
        {
            DataSet dsdetailsTill = new DataSet();
            SQLAccessChips cslac = new SQLAccessChips();
            dsdetailsTill = cslac.GetDs("SELECT SUM(CONVERT(float,Stock))*3 AS 'TotalStock',MAX(ID)*3  AS 'TotalOrders' FROM DAILYDETAILS");
            if (dsdetailsTill != null && dsdetailsTill.Tables.Count > 0 && dsdetailsTill.Tables[0].Rows.Count > 0)
            {
                lblstatustill.Text = "Total Orders Till Date: <b>" + dsdetailsTill.Tables[0].Rows[0]["TotalOrders"].ToString() + " Orders </b>, Total Chips Sold Out: <b>" + dsdetailsTill.Tables[0].Rows[0]["TotalStock"].ToString() + " Million. </b>(Updated every 5 Hours)";
            }
        }

    }
}