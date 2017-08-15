using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Posts
{
    public partial class Post : System.Web.UI.Page
    {
        #region Declaration
        CommonDAC commandac = new CommonDAC();
        PostComponent postcomment = new PostComponent();
        public string MainCategory;
        public string CategoryName;
        ConfigurationComponent objConfiguration = new ConfigurationComponent();

        public static string PostTempPath = string.Empty;
        public static string AudPostTempPath = string.Empty;
        public static string PostIconPath = string.Empty;
        public static string PostMediumPath = string.Empty;
        public static string PostLargePath = string.Empty;
        public static string PostMicroPath = string.Empty;
        public static string PostAudPath = string.Empty;
        public static string PostVidPath = string.Empty;

        static Size thumbNailSizeLarge = Size.Empty;
        static Size thumbNailSizeMediam = Size.Empty;
        static Size thumbNailSizeIcon = Size.Empty;
        static Size thumbNailSizeMicro = Size.Empty;

        static int finHeight;
        static int finWidth;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            BindSize();
            if (!IsPostBack)
            {
                PostTempPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/");
                AudPostTempPath = string.Concat(AppLogic.AppConfigs("AudioPathPost"), "Temp/");
                PostIconPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Icon/");
                PostMediumPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Medium/");
                PostLargePath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Large/");
                PostMicroPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Micro/");
                PostAudPath = AppLogic.AppConfigs("AudioPathPost");
                BindCategory();
                BindPostType();
                if (Request.QueryString["postid"] != null && Request.QueryString["postid"].ToString() != "")
                {
                    BindPostDetails(Convert.ToInt32(Request.QueryString["postid"]));
                }

                if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString() != "")
                {
                    //Edit
                    btnupmoretemp.Visible = false;
                    btnupmore.Visible = true;
                }
                else
                {
                    btnupmoretemp.Visible = true;
                    btnupmore.Visible = false;

                    ImgLarge.Src = PostMediumPath + "image_not_available.jpg";
                }
                Master.HeadTitle("BeIndian - Add Post", "BeIndian.in - Add Post, Admin Add Post", "BeIndian.in - To Add Post by Admin");
            }
        }

        protected void BindCategory()
        {
            DataSet dsCategory = new DataSet();
            CategoryComponent dac = new CategoryComponent();
            dsCategory = dac.GetCategory(3);
            LoadCategoryTree(dsCategory);
        }

        public void LoadCategoryTree(DataSet dsCategory)
        {
            string CatName = "";
            trvCategories.Nodes.Clear();
            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0 && dsCategory.Tables[0].Select("ParentCategoryID=0").Length > 0)
            {
                foreach (DataRow dr in dsCategory.Tables[0].Select("ParentCategoryID=0"))
                {
                    CatName = dr["Name"].ToString();
                    TreeNode myNode = new TreeNode();
                    myNode.Text = "<span onclick=\"this.parentNode.parentNode.firstChild.checked=!this.parentNode.parentNode.firstChild.checked;return false;\">" + CatName + "</span>";
                    myNode.Value = dr["CategoryID"].ToString();
                    myNode.ShowCheckBox = true;
                    myNode.CollapseAll();
                    trvCategories.Nodes.Add(myNode);
                    LoadChildNode(Convert.ToInt32(dr["CategoryID"].ToString()), myNode, dsCategory);
                }
            }
        }

        public void LoadChildNode(int id, TreeNode tn, DataSet dsCategory)
        {
            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0 && dsCategory.Tables[0].Select("ParentCategoryID=" + id).Length > 0)
            {
                foreach (DataRow dr in dsCategory.Tables[0].Select("ParentCategoryID=" + id))
                {
                    string ChildCatName = dr["Name"].ToString();
                    TreeNode tnChild = new TreeNode();
                    tnChild.Text = "<span onclick=\"this.parentNode.parentNode.firstChild.checked=!this.parentNode.parentNode.firstChild.checked;return false;\">" + ChildCatName + "</span>";
                    tnChild.Value = dr["CategoryID"].ToString();
                    tnChild.ShowCheckBox = true;
                    tnChild.CollapseAll();
                    tn.ChildNodes.Add(tnChild);
                    LoadChildNode(Convert.ToInt32(dr["CategoryID"].ToString()), tnChild, dsCategory);
                }
            }
        }

        public static DataSet GetCategoryByStoreID(Int32 StoreID, Int32 Option)
        {
            DataSet DSCategory = new DataSet();
            CategoryComponent dac = new CategoryComponent();
            DSCategory = dac.GetCategory(Option);
            return DSCategory;
        }

        protected void BindPostType()
        {
            ddlposttype.Items.Clear();
            DataSet dsPostType = new DataSet();

            dsPostType = commandac.GetCommonDataSet("select PostTypeID,Name from tb_postType");
            if (dsPostType != null && dsPostType.Tables.Count > 0 && dsPostType.Tables[0].Rows.Count > 0)
            {
                ddlposttype.DataSource = dsPostType;
                ddlposttype.DataTextField = "Name";
                ddlposttype.DataValueField = "PostTypeID";
            }
            else
            {
                ddlposttype.DataSource = null;
            }
            ddlposttype.DataBind();
            ddlposttype.Items.Insert(0, new ListItem("Select Post Type", "0"));
            ddlposttype.SelectedIndex = 0;
        }

        protected void BindPostDetails(int PostId)
        {
            DataSet dsPost = new DataSet();
            dsPost = postcomment.GetPostByPostId(PostId, 1);
            if (dsPost != null && dsPost.Tables.Count > 0 && dsPost.Tables[0].Rows.Count > 0)
            {
                txtPostTitle.Text = dsPost.Tables[0].Rows[0]["Title"].ToString();
                ddlposttype.SelectedIndex = Convert.ToInt16(dsPost.Tables[0].Rows[0]["PostTypeID"]);
                if (ddlposttype.SelectedItem.ToString().ToLower() == "image")
                {
                    divimage.Visible = true;
                    divvideo.Visible = false;
                    divaudio.Visible = false;
                }
                else if (ddlposttype.SelectedItem.ToString().ToLower() == "video")
                {
                    divimage.Visible = false;
                    divvideo.Visible = true;
                    divaudio.Visible = false;
                }
                else if (ddlposttype.SelectedItem.ToString().ToLower() == "audio")
                {
                    divimage.Visible = false;
                    divvideo.Visible = false;
                    divaudio.Visible = true;
                }
                txtsortdesc.Text = dsPost.Tables[0].Rows[0]["SortDescription"].ToString();
                txtDescription.Text = Server.HtmlDecode(dsPost.Tables[0].Rows[0]["Description"].ToString());
                txtSETitle.Text = dsPost.Tables[0].Rows[0]["SETitle"].ToString();
                txtSKU.Text = dsPost.Tables[0].Rows[0]["SKU"].ToString();
                txtSEKeyword.Text = dsPost.Tables[0].Rows[0]["SEKeywords"].ToString();
                txtSEDescription.Text = dsPost.Tables[0].Rows[0]["SEDescription"].ToString();
                txtRelPosts.Text = dsPost.Tables[0].Rows[0]["RelatedPost"].ToString();
                txtvidlink.Text = dsPost.Tables[0].Rows[0]["VideoLink"].ToString();
                txtSummary.Text = dsPost.Tables[0].Rows[0]["Summary"].ToString();
                txtadminsummary.Text = dsPost.Tables[0].Rows[0]["AdminSummary"].ToString();
                txtusersummary.Text = dsPost.Tables[0].Rows[0]["UserSummary"].ToString();
            }

            #region Get Image

            string Imagename = dsPost.Tables[0].Rows[0]["Imagename"].ToString();
            String strImageName = RemoveSpecialCharacter(Convert.ToString(dsPost.Tables[0].Rows[0]["SKU"]).ToCharArray()) + "_" + PostId + ".jpg";

            if (Imagename.ToString().Trim().ToLower().IndexOf("http") > -1)
            {
                if (Imagename.ToString().Trim().ToLower().IndexOf("<img") > -1 || Imagename.ToString().Trim().ToLower().IndexOf("< img") > -1)
                {
                    String[] strImg = System.Text.RegularExpressions.Regex.Split(Imagename, "src", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                    if (strImg.Length > 1)
                    {
                        String strTemp = strImg[1].Trim().Replace("=", "").Replace(">", "");
                        Imagename = strTemp.ToString().Trim();
                    }
                }
                System.Net.WebClient objClient = new System.Net.WebClient();
                String strSavedImgPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/") + strImageName.ToString();
                objClient.DownloadFile(Imagename.ToString(), Server.MapPath(strSavedImgPath));
                if (File.Exists(Server.MapPath(strSavedImgPath)))
                {
                    ImgLarge.Src = strSavedImgPath.ToString();
                    ViewState["File"] = strImageName.ToString();
                }
                else
                {
                    ImgLarge.Src = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Large/image_not_available.jpg");
                }
            }
            else
            {
                PostMediumPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Medium/");
                if (!Directory.Exists(Server.MapPath(PostMediumPath)))
                    Directory.CreateDirectory(Server.MapPath(PostMediumPath));

                string strFilePath = Server.MapPath(PostMediumPath + Imagename);
                btnDelete.Visible = false;
                if (Request.QueryString["CloneID"] == null)
                {
                    if (File.Exists(strFilePath))
                    {
                        ViewState["DelImage"] = Imagename;
                        btnDelete.Visible = true;
                        ImgLarge.Src = PostMediumPath + Imagename;
                    }
                    else
                    {
                        ViewState["DelImage"] = null;
                        btnDelete.Visible = false;
                        ImgLarge.Src = PostMediumPath + "image_not_available.jpg";
                    }
                }
                else
                {
                    clsvariables.LoadAllPath();
                    string strFilePathOld = Server.MapPath(string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Medium/") + Imagename);
                    if (!Directory.Exists(Server.MapPath(AppLogic.AppConfigs("ImagePathPost"))))
                    {
                        Directory.CreateDirectory(Server.MapPath(AppLogic.AppConfigs("ImagePathPost")));
                    }
                    if (File.Exists(strFilePathOld.Replace("/Medium/", "/Large/")))
                    {
                        FileInfo flOld = new FileInfo(strFilePathOld.ToString().Replace("/Medium/", "/Large/"));

                        FileInfo fl = new FileInfo(strFilePath.ToString().Replace("/Medium/", "/Large/"));
                        ViewState["File"] = fl.Name.ToString();
                        clsvariables.LoadAllPath();
                        if (!Directory.Exists(Server.MapPath(AppLogic.AppConfigs("ImagePathPost") + "/Temp")))
                        {
                            Directory.CreateDirectory(Server.MapPath(AppLogic.AppConfigs("ImagePathPost") + "/Temp"));
                        }
                        File.Copy(strFilePathOld.Replace("/Medium/", "/Large/"), Server.MapPath(AppLogic.AppConfigs("ImagePathPost") + "Temp/" + fl.Name.ToString()), true);
                        ImgLarge.Src = AppLogic.AppConfigs("ImagePathPost") + "Temp/" + fl.Name.ToString();
                    }
                    else
                    {
                        ImgLarge.Src = PostMediumPath + "image_not_available.jpg";
                        ViewState["File"] = null;
                    }
                }
            }
            #endregion

            #region  Bind Category and Main Category Data

            if (dsPost.Tables[0].Rows[0]["MainCategory"] != null && dsPost.Tables[0].Rows[0]["MainCategory"].ToString() != "")
            {
                txtMaincategory.Text = dsPost.Tables[0].Rows[0]["MainCategory"].ToString().Trim();
            }

            DataSet dsCategory = new DataSet();
            CategoryComponent catc = new CategoryComponent();
            dsCategory = catc.getCategorydetailsbyPost(Convert.ToInt32(Request.QueryString["postid"]));
            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                string Category = ",";
                for (int j = 0; j < dsCategory.Tables[0].Rows.Count; j++)
                {
                    Category += dsCategory.Tables[0].Rows[j]["CategoryID"].ToString().Trim() + ",";
                }
                BindTreeData(Category);
            }
            #endregion
        }

        public void BindTreeData(string Category)
        {
            string CategoryList = Category;
            StringArrayConverter Categoryconvertor = new StringArrayConverter();
            Array CategoryArray = (Array)Categoryconvertor.ConvertFrom(CategoryList);
            if (CategoryArray.Length != 0)
            {
                foreach (TreeNode tn in trvCategories.Nodes)
                {
                    for (int j = 0; j < CategoryArray.Length; j++)
                    {
                        if (Category.Contains("," + tn.Value.ToString() + ","))
                        {
                            tn.Checked = true;
                            tn.Expanded = true;
                            if (tn.Parent != null)
                                tn.Parent.Expanded = true;
                        }
                        EditTree(Category, tn);
                    }
                }
            }
        }

        public void EditTree(string Name, TreeNode tn)
        {
            foreach (TreeNode tnchild in tn.ChildNodes)
            {
                if (Name.Contains("," + tnchild.Value.ToString() + ","))
                {
                    while (tn.Parent != null)
                    {
                        tn.Parent.Expanded = true;
                        tn = tn.Parent;
                    }
                    tnchild.Checked = true;
                    tnchild.Parent.Expanded = true;
                }
                EditTree(Name, tnchild);
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Rights", "$(document).ready( function() {jAlert('please insert post description.', 'Message');});", true);
                return;
            }
            if (Request.QueryString["mode"] != null && Request.QueryString["mode"].ToString() != "")
            {
                if (Session["AdminID"] != null && Request.QueryString["postid"] != null)
                {
                    int AdminId = Convert.ToInt32(commandac.GetScalarCommonData("select AdminId from tb_Post where PostID='" + Request.QueryString["postid"] + "'"));
                    if (Convert.ToInt32(Session["AdminID"]) == AdminId)
                    {
                        UpdatePost();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Rights", "$(document).ready( function() {jAlert('You have no rights to update this post.', 'Message');});", true);
                        return;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "SessionError", "$(document).ready( function() {jAlert('Soomething went wrong please reload page.', 'Message');});", true);
                    return;
                }
            }
            else
            {
                InsertPost();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            bool Flag = false;
            StringArrayConverter Storeconvertor = new StringArrayConverter();
            string sImageextension = AppLogic.AppConfigs("AllowedExtensions");
            Array StoreArray = (Array)Storeconvertor.ConvertFrom(sImageextension);
            PostTempPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/");
            if (!Directory.Exists(Server.MapPath(PostTempPath)))
                Directory.CreateDirectory(Server.MapPath(PostTempPath));

            for (int j = 0; j < StoreArray.Length; j++)
                if (fuPostIcon.FileName.Length > 0 && Path.GetExtension(fuPostIcon.FileName.ToString().ToLower()) == StoreArray.GetValue(j).ToString().ToLower())
                    Flag = true;

            if (Flag)
            {
                if (fuPostIcon.FileName.Length > 0)
                {
                    ViewState["File"] = fuPostIcon.FileName.ToString();
                    fuPostIcon.SaveAs(Server.MapPath(PostTempPath) + fuPostIcon.FileName);
                    ImgLarge.Src = PostTempPath + fuPostIcon.FileName;
                }
                else
                {
                    ViewState["File"] = null;
                }
            }
            else
            {
                string message = "Only " + AppLogic.AppConfigs("AllowedExtensions") + " Images are allowed";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "PDFMsg", "$(document).ready( function() {jAlert('" + message + "', 'Message','ContentPlaceHolder1_fuPostIcon');});", true);
            }

        }

        protected void btnUploadAud_Click(object sender, EventArgs e)
        {

            bool Flag = false;
            StringArrayConverter Storeconvertor = new StringArrayConverter();
            string sImageextension = ".mp3,.mp4";
            Array StoreArray = (Array)Storeconvertor.ConvertFrom(sImageextension);
            AudPostTempPath = string.Concat(AppLogic.AppConfigs("AudioPathPost"), "Temp/");
            if (!Directory.Exists(Server.MapPath(AudPostTempPath)))
                Directory.CreateDirectory(Server.MapPath(AudPostTempPath));

            for (int j = 0; j < StoreArray.Length; j++)
                if (fileaud.FileName.Length > 0 && Path.GetExtension(fileaud.FileName.ToString().ToLower()) == StoreArray.GetValue(j).ToString().ToLower())
                    Flag = true;

            if (Flag)
            {
                if (fileaud.FileName.Length > 0)
                {
                    ViewState["AudFile"] = fileaud.FileName.ToString();
                    fileaud.SaveAs(Server.MapPath(AudPostTempPath) + fileaud.FileName);
                    lblsortaudpath.Text = fileaud.FileName;
                    lblaudpath.Text = AudPostTempPath + fileaud.FileName;
                }
                else
                {
                    ViewState["AudFile"] = null;
                }
            }
            else
            {
                string message = "Only " + ".mp3,.mp4" + " Images are allowed";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "PDFMsg", "$(document).ready( function() {jAlert('" + message + "', 'Message','ContentPlaceHolder1_fileaud');});", true);
            }
        }

        public void InsertPost()
        {
            int Postadded = 0;
            int SKU = 0;
            bool Active = true;

            string CategoryValue = "";
            CategoryValue = SetCategory();

            if (string.IsNullOrEmpty(CategoryValue.ToString()))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msgcateInsert", "$(document).ready( function() {jAlert('You must select at least one category!', 'Message');});", true);
                return;
            }

            string SEName = RemoveSpecialCharacter(txtPostTitle.Text.Trim().ToLower().ToCharArray());
            Int32 ChkSKu = Convert.ToInt32(commandac.GetScalarCommonData("select ISNULL(Max(SKU),1) from tb_Post"));
            if (ChkSKu > 0)
            {
                SKU = ChkSKu + 1;
            }

            Postadded = postcomment.InsertPost(txtPostTitle.Text.Trim(), SKU, ddlposttype.SelectedIndex, Convert.ToInt32(Session["AdminID"]), SEName, txtsortdesc.Text.Trim(), txtDescription.Text.Trim(), MainCategory, CategoryName, txtSEDescription.Text.Trim(), txtSETitle.Text.Trim(), txtSEKeyword.Text.Trim(), txtSummary.Text.Trim(), Active, txtvidlink.Text.Trim(), txtRelPosts.Text.Trim());

            if (Postadded > 0)
            {
                try
                {
                    AddCategory(CategoryValue, Postadded);

                    // Save fields like audio name when user uploaded save it vie this 
                    //if (ddlposttype.SelectedValue == "Image")
                    //{

                    //}

                    string strImageName = "";
                    string strAudName = "";
                    if (SKU > 0)
                    {
                        if (ddlposttype.SelectedItem.ToString() == "Image")
                        {
                            strImageName = SKU + "_" + Postadded + ".jpg";
                            SaveImage(strImageName);
                            commandac.ExecuteCommonData("update tb_Post set ImageName='" + strImageName + "' where PostId='" + Postadded + "'");
                        }
                        else if (ddlposttype.SelectedItem.ToString() == "Audio")
                        {
                            strAudName = SKU + "_" + Postadded + ".mp3";
                            SaveAudio(strAudName);
                            commandac.ExecuteCommonData("update tb_Post set AudioName ='" + strAudName + "' where PostId='" + Postadded + "'");
                        }

                    }

                }
                catch { }
                Response.Redirect("/admin/posts/postlist.aspx", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailInsert", "$(document).ready( function() {jAlert('Soomething went wrong while creating post try again later.', 'Message');});", true);
                return;
            }
        }

        public void UpdatePost()
        {
            bool PostUpdated = false;
            string SEName = RemoveSpecialCharacter(txtPostTitle.Text.Trim().ToLower().ToCharArray());
            CategoryComponent catcomp = new CategoryComponent();
            catcomp.DeleteCategoryforPost(Convert.ToInt32(Request.QueryString["postid"].ToString()));
            string CategoryValue = "";
            CategoryValue = SetCategory();
            if (string.IsNullOrEmpty(CategoryValue.ToString()))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msgCategory", "$(document).ready( function() {jAlert('You must select at least one category!', 'Message');});", true);
                return;
            }


            PostUpdated = postcomment.UpdatePost(txtPostTitle.Text.Trim(), ddlposttype.SelectedIndex, Convert.ToInt32(Session["AdminID"]), SEName, txtsortdesc.Text.Trim(), txtDescription.Text.Trim(), MainCategory, CategoryName, txtSEDescription.Text.Trim(), txtSETitle.Text.Trim(), txtSEKeyword.Text.Trim(), txtSummary.Text.Trim(), Convert.ToInt32(Request.QueryString["postid"]), txtvidlink.Text.Trim(), txtRelPosts.Text.Trim());

            if (PostUpdated)
            {
                try
                {
                    AddCategory(CategoryValue, Convert.ToInt32(Request.QueryString["postid"].ToString()));
                    #region Save and Update Image
                    string strImageName = "";
                    string ImageNameupdate = string.Empty;
                    try
                    {
                        if (!string.IsNullOrEmpty(txtSKU.Text.ToString().Trim()))
                        {
                            strImageName = RemoveSpecialCharacter(txtSKU.Text.ToString().ToCharArray()) + "_" + Request.QueryString["postid"].ToString() + ".jpg";
                            DataSet dsPost = postcomment.GetPostByPostId(Convert.ToInt32(Request.QueryString["postid"]), 2);
                            string Imgname = dsPost.Tables[0].Rows[0]["ImageName"].ToString();
                            if (ImgLarge.Src.Contains(PostTempPath))
                            {
                                SaveImage(strImageName);
                            }
                            else if (!string.IsNullOrEmpty(Imgname.ToString()))
                            {
                                if (File.Exists(Server.MapPath(PostLargePath + Imgname)))
                                    File.Move(Server.MapPath(PostLargePath + Imgname), Server.MapPath(PostLargePath + strImageName));

                                if (File.Exists(Server.MapPath(PostMediumPath + Imgname)))
                                    File.Move(Server.MapPath(PostMediumPath + Imgname), Server.MapPath(PostMediumPath + strImageName));

                                if (File.Exists(Server.MapPath(PostMicroPath + Imgname)))
                                    File.Move(Server.MapPath(PostMicroPath + Imgname), Server.MapPath(PostMicroPath + strImageName));

                                if (File.Exists(Server.MapPath(PostIconPath + Imgname)))
                                    File.Move(Server.MapPath(PostIconPath + Imgname), Server.MapPath(PostIconPath + strImageName));
                            }
                        }


                        if (string.IsNullOrEmpty(ImgLarge.Src.ToString()) || ImgLarge.Src.ToString().ToLower().Contains("image_not_available"))
                            ImageNameupdate = "";
                        else
                            ImageNameupdate = strImageName;

                        commandac.ExecuteCommonData("update tb_Post set ImageName='" + ImageNameupdate + "' where PostId='" + Request.QueryString["postid"].ToString() + "'");
                    }
                    catch { }

                    #endregion

                    #region Save and Update Audio
                    string strAudioName = "";
                    string AudioNameupdate = string.Empty;
                    try
                    {
                        if (!string.IsNullOrEmpty(txtSKU.Text.ToString().Trim()))
                        {
                            strAudioName = RemoveSpecialCharacter(txtSKU.Text.ToString().ToCharArray()) + "_" + Request.QueryString["postid"].ToString() + ".mp3";
                            DataSet dsPost = postcomment.GetPostByPostId(Convert.ToInt32(Request.QueryString["postid"]), 2);
                            string Audname = dsPost.Tables[0].Rows[0]["AudioName"].ToString();
                            if (lblaudpath.Text.Contains(PostTempPath))
                            {
                                SaveImage(strImageName);
                            }
                            else if (!string.IsNullOrEmpty(Audname.ToString()))
                            {
                                if (File.Exists(Server.MapPath(PostAudPath + Audname)))
                                    File.Move(Server.MapPath(PostAudPath + Audname), Server.MapPath(PostAudPath + strAudioName));
                            }
                        }


                        if (string.IsNullOrEmpty(lblaudpath.Text.ToString()))
                            AudioNameupdate = "";
                        else
                            AudioNameupdate = strAudioName;

                        commandac.ExecuteCommonData("update tb_Post set AudioName='" + AudioNameupdate + "' where PostId='" + Request.QueryString["postid"].ToString() + "'");
                    }
                    catch { }

                    #endregion

                }
                catch { }
                Response.Redirect("/admin/posts/postlist.aspx", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailUpdate", "$(document).ready( function() {jAlert('Soomething went wrong while updating post try again later.', 'Message');});", true);
                return;
            }
        }

        public string SetCategory()
        {
            Int32 CatIDforMainCat = 0;
            ArrayList Category = new ArrayList();
            Category = AddPostCategory();
            Category.Sort();
            string CategoryValue = ",";
            for (int i = 0; i < Category.Count; i++)
            {
                if (i == 0)
                {
                    CatIDforMainCat = Convert.ToInt32(Category[i].ToString().Trim());
                }

                if (i == Category.Count)
                    CategoryValue += Category[i].ToString().Trim();
                else
                    CategoryValue += Category[i].ToString().Trim() + ",";
            }

            if (Category.Count == 0)
            {
                return "";
            }

            if (string.IsNullOrEmpty(txtMaincategory.Text) || !string.IsNullOrEmpty(CatIDforMainCat.ToString()))
            {
                CategoryComponent catc = new CategoryComponent();
                DataSet dsMainCat = catc.getCatdetailbycatid(CatIDforMainCat);
                String CatNameforMainCat = "";
                if (dsMainCat != null && dsMainCat.Tables.Count > 0 && dsMainCat.Tables[0].Rows.Count > 0)
                {
                    CatNameforMainCat = Convert.ToString(dsMainCat.Tables[0].Rows[0]["Name"]);
                }
                txtMaincategory.Text = CatNameforMainCat;
            }

            if (!string.IsNullOrEmpty(txtMaincategory.Text))
            {
                CategoryName = txtMaincategory.Text;
                MainCategory = RemoveSpecialCharacter(txtMaincategory.Text.Trim().ToLower().ToCharArray());
            }
            return CategoryValue;
        }

        public ArrayList AddPostCategory()
        {
            ArrayList CategoryArray = new ArrayList();
            foreach (TreeNode tn in trvCategories.Nodes)
            {
                if (tn.Checked == true)
                {
                    CategoryArray.Add(tn.Value);
                }
                CategoryArray = GetCategoryIDList(CategoryArray, tn);
            }
            return CategoryArray;
        }

        public ArrayList GetCategoryIDList(ArrayList array, TreeNode tn)
        {
            foreach (TreeNode tchild in tn.ChildNodes)
            {
                if (tchild.Checked == true)
                {
                    array.Add(tchild.Value);
                }
                GetCategoryIDList(array, tchild);
            }
            return array;
        }

        public void AddCategory(string CategoryID, int PostId)
        {
            Int32 CatID = 0;
            StringArrayConverter Catconvertor = new StringArrayConverter();
            Array CatArray = (Array)Catconvertor.ConvertFrom(CategoryID);
            for (int i = 0; i < CatArray.Length; i++)
            {
                if (CatArray.GetValue(i).ToString() != "")
                {
                    CatID = Convert.ToInt32(CatArray.GetValue(i).ToString());
                    int PostCategoryID = CatID;
                    int PostPostID = PostId;
                    int DisplayOrder = 0;
                    DataSet dsDisplay = postcomment.GetDisplayOrderByPostIDAndCategoryID(PostId, Convert.ToInt32(CatID));
                    if (dsDisplay != null && dsDisplay.Tables.Count > 0 && dsDisplay.Tables[0].Rows.Count > 0)
                    {
                        DisplayOrder = Convert.ToInt32(dsDisplay.Tables[0].Rows[0]["DisplayOrder"].ToString().Trim());
                    }
                    commandac.ExecuteCommonData("insert into tb_PostCategory (CategoryID,PostID,DisplayOrder) values ('" + PostCategoryID + "','" + PostPostID + "','" + DisplayOrder + "') ");
                }
            }
        }

        protected void SaveImage(string FileName)
        {
            //create icon folder 
            if (!Directory.Exists(Server.MapPath(PostIconPath)))
                Directory.CreateDirectory(Server.MapPath(PostIconPath));

            //create Medium folder 
            if (!Directory.Exists(Server.MapPath(PostMediumPath)))
                Directory.CreateDirectory(Server.MapPath(PostMediumPath));

            //create Large folder 
            if (!Directory.Exists(Server.MapPath(PostLargePath)))
                Directory.CreateDirectory(Server.MapPath(PostLargePath));

            //create Micro folder 
            if (!Directory.Exists(Server.MapPath(PostMicroPath)))
                Directory.CreateDirectory(Server.MapPath(PostMicroPath));

            if (ImgLarge.Src.Contains(PostTempPath))
            {
                try
                {
                    CreateImage("Medium", FileName);
                    CreateImage("Icon", FileName);
                    CreateImage("Micro", FileName);
                    CreateImage("Large", FileName);
                }
                catch (Exception ex)
                {
                    lblMsg.Text += "<br />" + ex.Message;
                }
                finally
                {
                    DeleteTempFile("icon");
                }
            }
        }

        protected void SaveAudio(string FileName)
        {
            //create audio folder 
            if (!Directory.Exists(Server.MapPath(PostAudPath)))
                Directory.CreateDirectory(Server.MapPath(PostAudPath));


            if (lblaudpath.Text.Contains(AudPostTempPath))
            {
                try
                {
                    CreateAud("Normal", FileName);
                }
                catch (Exception ex)
                {
                    lblMsg.Text += "<br />" + ex.Message;
                }
                finally
                {
                    DeleteTempFile("audio");
                }
            }
        }

        protected void CreateAud(string Size, string FileName)
        {
            try
            {
                string strFile = null;
                String strPath = "";
                if (lblaudpath.Text.ToString().IndexOf("?") > -1)
                {
                    strPath = lblaudpath.Text.Split('?')[0];
                }
                else
                {
                    strPath = lblaudpath.Text.ToString();
                }
                strFile = Server.MapPath(strPath);
                string strFilePath = "";
                Size = Size.ToLower();
                switch (Size)
                {
                    case "normal":
                        strFilePath = Server.MapPath(PostAudPath + FileName);

                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelAud"] != null && ViewState["DelAud"].ToString().Trim().Length > 0)
                            {
                                DeleteAudio(PostAudPath + ViewState["DelAud"].ToString());
                            }
                        }
                        break;
                }
                ResizeAudio(strFile, Size, strFilePath);
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Drawing")
                    lblMsg.Text = "<br />Error Saving " + Size + " Image..Please check that Directory exists..";
                else
                    lblMsg.Text += "<br />" + ex.Message;
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }

        }


        protected void CreateImage(string Size, string FileName)
        {
            try
            {
                string strFile = null;
                String strPath = "";
                if (ImgLarge.Src.ToString().IndexOf("?") > -1)
                {
                    strPath = ImgLarge.Src.Split('?')[0];
                }
                else
                {
                    strPath = ImgLarge.Src.ToString();
                }
                strFile = Server.MapPath(strPath);
                string strFilePath = "";
                Size = Size.ToLower();
                switch (Size)
                {
                    case "large":
                        strFilePath = Server.MapPath(PostLargePath + FileName);

                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostLargePath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "medium":
                        strFilePath = Server.MapPath(PostMediumPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostMediumPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "icon":
                        strFilePath = Server.MapPath(PostIconPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostIconPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "micro":
                        strFilePath = Server.MapPath(PostMicroPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostMicroPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                }
                ResizePhoto(strFile, Size, strFilePath);
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Drawing")
                    lblMsg.Text = "<br />Error Saving " + Size + " Image..Please check that Directory exists..";
                else
                    lblMsg.Text += "<br />" + ex.Message;
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }

        }

        public void ResizePhoto(string strFile, string Size, string strFilePath)
        {
            switch (Size)
            {
                case "medium":
                    finHeight = thumbNailSizeMediam.Height;
                    finWidth = thumbNailSizeMediam.Width;
                    break;
                case "icon":
                    finHeight = thumbNailSizeIcon.Height;
                    finWidth = thumbNailSizeIcon.Width;
                    break;
                case "micro":
                    finHeight = thumbNailSizeMicro.Height;
                    finWidth = thumbNailSizeMicro.Width;
                    break;

            }
            if (Size == "large")
            {
                File.Copy(strFile, strFilePath, true);
            }
            else
                ResizeImage(strFile, finWidth, finHeight, strFilePath);
        }

        public void ResizeImage(string strFile, int FinWidth, int FinHeight, string strFilePath)
        {
            System.Drawing.Image imgWebgape = System.Drawing.Image.FromFile(strFile);
            float resizePercent = 0;
            int resizedHeight = imgWebgape.Height;
            int resizedWidth = imgWebgape.Width;

            resizedHeight = FinHeight;
            resizedWidth = FinWidth;

            #region commented
            //if (imgWebgape.Height >= FinHeight && imgWebgape.Width >= FinWidth)
            //{
            //    float resizePercentHeight = 0;
            //    float resizePercentWidth = 0;
            //    resizePercentHeight = (FinHeight * 100) / imgWebgape.Height;
            //    resizePercentWidth = (FinWidth * 100) / imgWebgape.Width;
            //    if (resizePercentHeight < resizePercentWidth)
            //    {
            //        resizedHeight = FinHeight;
            //        resizedWidth = (int)Math.Round(resizePercentHeight * imgWebgape.Width / 100.0);
            //    }
            //    if (resizePercentHeight >= resizePercentWidth)
            //    {
            //        resizedWidth = FinWidth;
            //        resizedHeight = (int)Math.Round(resizePercentWidth * imgWebgape.Height / 100.0);
            //    }
            //}
            //else if (imgWebgape.Width >= FinWidth && imgWebgape.Height <= FinHeight)
            //{
            //    resizedWidth = FinWidth;
            //    resizePercent = (FinWidth * 100) / imgWebgape.Width;
            //    resizedHeight = (int)Math.Round((imgWebgape.Height * resizePercent) / 100.0);
            //}

            //else if (imgWebgape.Width <= FinWidth && imgWebgape.Height >= FinHeight)
            //{
            //    resizePercent = (FinHeight * 100) / imgWebgape.Height;
            //    resizedHeight = FinHeight;
            //    resizedWidth = (int)Math.Round(resizePercent * imgWebgape.Width / 100.0);
            //}
            #endregion

            Bitmap resizedPhoto = new Bitmap(resizedWidth, resizedHeight, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(resizedPhoto);

            int destWidth = resizedWidth;
            int destHeight = resizedHeight;
            int sourceWidth = imgWebgape.Width;
            int sourceHeight = imgWebgape.Height;

            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle DestRect = new Rectangle(0, 0, destWidth, destHeight);
            Rectangle srcRect = new Rectangle(0, 0, sourceWidth, sourceHeight);
            grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            grPhoto.DrawImage(imgWebgape, DestRect, srcRect, GraphicsUnit.Pixel);

            GenerateImage(resizedPhoto, strFilePath, FinWidth, FinHeight);

            resizedPhoto.Dispose();
            grPhoto.Dispose();
            imgWebgape.Dispose();

        }

        public void ResizeAudio(string strFile, string Size, string strFilePath)
        {
            File.Copy(strFile, strFilePath, true);
        }

        private void BindSize()
        {
            DataSet dsIconWidth = objConfiguration.GetImageSizeByType("PostIconWidth");
            DataSet dsIconHeight = objConfiguration.GetImageSizeByType("PostIconHeight");
            if ((dsIconWidth != null && dsIconWidth.Tables.Count > 0 && dsIconWidth.Tables[0].Rows.Count > 0) && (dsIconHeight != null && dsIconHeight.Tables.Count > 0 && dsIconHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeIcon = new Size(Convert.ToInt32(dsIconWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsIconHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsLargeWidth = objConfiguration.GetImageSizeByType("PostLargeWidth");
            DataSet dsLargeHeight = objConfiguration.GetImageSizeByType("PostLargeHeight");
            if ((dsLargeWidth != null && dsLargeWidth.Tables.Count > 0 && dsLargeWidth.Tables[0].Rows.Count > 0) && (dsLargeHeight != null && dsLargeHeight.Tables.Count > 0 && dsLargeHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeLarge = new Size(Convert.ToInt32(dsLargeWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsLargeHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsMediumWidth = objConfiguration.GetImageSizeByType("PostMediumWidth");
            DataSet dsMediumHeight = objConfiguration.GetImageSizeByType("PostMediumHeight");
            if ((dsMediumWidth != null && dsMediumWidth.Tables.Count > 0 && dsMediumWidth.Tables[0].Rows.Count > 0) && (dsMediumHeight != null && dsMediumHeight.Tables.Count > 0 && dsMediumHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeMediam = new Size(Convert.ToInt32(dsMediumWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsMediumHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsMicroWidth = objConfiguration.GetImageSizeByType("PostMicroWidth");
            DataSet dsMicroHeight = objConfiguration.GetImageSizeByType("PostMicroHeight");
            if ((dsMicroWidth != null && dsMicroWidth.Tables.Count > 0 && dsMicroWidth.Tables[0].Rows.Count > 0) && (dsMicroHeight != null && dsMicroHeight.Tables.Count > 0 && dsMicroHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeMicro = new Size(Convert.ToInt32(dsMicroWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsMicroHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }
        }

        private void GenerateImage(Bitmap extBMP, string DestFileName, int DefWidth, int DefHeight)
        {
            Encoder Enc = Encoder.SaveFlag;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");
            EncParm = new EncoderParameter(Encoder.Quality, (long)600);
            EncParms.Param[0] = new EncoderParameter(Encoder.Quality, (long)600);

            if (extBMP != null && extBMP.Width < (DefWidth) && extBMP.Height < (DefHeight))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startX = (int)(DefWidth / 2) - (extBMP.Width / 2);
                int startY = (int)(DefHeight / 2) - (extBMP.Height / 2);
                g.DrawImage(extBMP, startX, startY);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null && extBMP.Width < (DefWidth))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startX = (int)(DefWidth / 2) - (extBMP.Width / 2);
                g.DrawImage(extBMP, startX, 0);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null && extBMP.Height < (DefHeight))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startY = (int)(DefHeight / 2) - (extBMP.Height / 2);
                g.DrawImage(extBMP, 0, startY);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null)
            {
                extBMP.Save(DestFileName, CodecInfo, EncParms);
                extBMP.Dispose();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string resizeMimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == resizeMimeType)
                    return codecs[i];
            return null;
        }

        protected void DeleteTempFile(string strsize)
        {
            try
            {
                if (strsize == "icon")
                {
                    string path = string.Empty;
                    if (ViewState["File"] != null && ViewState["File"].ToString().Trim().Length > 0)
                    {
                        path = Server.MapPath(PostTempPath + ViewState["File"].ToString());
                    }

                    File.Delete(path);
                }
                if (strsize == "audio")
                {
                    string path = string.Empty;
                    if (ViewState["AudFile"] != null && ViewState["AudFile"].ToString().Trim().Length > 0)
                    {
                        path = Server.MapPath(AudPostTempPath + ViewState["AudFile"].ToString());
                    }

                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }
        }

        protected void DeleteImage(string ImageName)
        {
            try
            {
                if (File.Exists(Server.MapPath(ImageName)))
                    File.Delete(Server.MapPath(ImageName));
            }
            catch (Exception ex)
            {
                lblMsg.Text += "<br />" + ex.Message;
                //CommonOperation.WriteLog("\r\n Error Description: " + ex.Message + "\r\n" + ex.StackTrace + " \r\n Method :->DeleteImage() \r\n Date: " + System.DateTime.Now + "\r\n");
            }
        }

        protected void DeleteAudio(string AudioName)
        {
            try
            {
                if (File.Exists(Server.MapPath(AudioName)))
                    File.Delete(Server.MapPath(AudioName));
            }
            catch (Exception ex)
            {
                lblMsg.Text += "<br />" + ex.Message;
                //CommonOperation.WriteLog("\r\n Error Description: " + ex.Message + "\r\n" + ex.StackTrace + " \r\n Method :->DeleteImage() \r\n Date: " + System.DateTime.Now + "\r\n");
            }
        }


        protected void btnCancle_Click(object sender, EventArgs e)
        {
        }

        public static string RemoveSpecialCharacter(char[] charr)
        {
            string res = "";
            string value = new string(charr);
            value = value.Replace('~', '-');
            value = value.Replace('!', '-');
            value = value.Replace('@', '-');
            value = value.Replace('#', '-');
            value = value.Replace('$', '-');
            value = value.Replace('%', '-');
            value = value.Replace('^', '-');
            value = value.Replace('&', '-');
            value = value.Replace('*', '-');
            value = value.Replace('(', '-');
            value = value.Replace(')', '-');
            value = value.Replace('_', '-');
            value = value.Replace('+', '-');
            value = value.Replace('|', '-');
            value = value.Replace('\\', '-');
            value = value.Replace('/', '-');
            value = value.Replace('?', '-');
            value = value.Replace('\'', '-');
            value = value.Replace('"', '-');
            value = value.Replace(' ', '-');
            value = value.Replace('>', '-');
            value = value.Replace('<', '-');
            value = value.Replace('.', '-');
            value = value.Replace(',', '-');
            value = value.Replace(':', '-');
            value = value.Replace("'", "-");
            value = value.Replace("--", "-");
            value = value.Replace("--", "-");
            value = value.Replace("--", "-");
            res = value;
            return res;
        }

        protected void ddlposttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlposttype.SelectedItem.ToString().ToLower() == "image")
            {
                divimage.Visible = true;
                divvideo.Visible = false;
                divaudio.Visible = false;
            }
            else if (ddlposttype.SelectedItem.ToString().ToLower() == "video")
            {
                divimage.Visible = false;
                divvideo.Visible = true;
                divaudio.Visible = false;
            }
            else if (ddlposttype.SelectedItem.ToString().ToLower() == "audio")
            {
                divimage.Visible = false;
                divvideo.Visible = false;
                divaudio.Visible = true;
            }
            else
            {
                divimage.Visible = false;
                divvideo.Visible = false;
                divaudio.Visible = false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteImage(PostLargePath + ViewState["DelImage"].ToString());
            DeleteImage(PostMediumPath + ViewState["DelImage"].ToString());
            DeleteImage(PostIconPath + ViewState["DelImage"].ToString());
            DeleteImage(PostMicroPath + ViewState["DelImage"].ToString());
            ViewState["DelImage"] = null;
            Response.Cache.SetExpires(DateTime.Now - TimeSpan.FromDays(1));
            ImgLarge.Src = PostMediumPath + "image_not_available.jpg";
            btnDelete.Visible = false;
        }
    }
}