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
    public partial class Category : System.Web.UI.Page
    {
        #region Declaration

        #region components

        CategoryComponent catcomp = new CategoryComponent();
        ConfigurationComponent objConfiguration = new ConfigurationComponent();
        CommonDAC commandac = new CommonDAC();
        #endregion


        #region Variables

        public static string CategoryTempPath = string.Empty;
        public static string CategoryIconPath = string.Empty;
        public static string CategoryMediumPath = string.Empty;
        public static string CategoryLargePath = string.Empty;
        public static string CategoryMicroPath = string.Empty;
        public static string CategoryBannerPath = string.Empty;
        public static string CategoryBannerTempPath = string.Empty;

        static Size thumbNailSizeIcon = Size.Empty;
        static Size thumbNailSizeMicro = Size.Empty;
        static Size thumbNailSizeBanner = Size.Empty;
        public static System.Text.StringBuilder sbCatMap = null;
        public static System.Text.StringBuilder sb = null;
        static string[] cmid;
        static string[] parentValue;
        int finHeight;
        int finWidth;

        public String strSearchedCategory = ",";
        public String strNodeExpand = ",";
        public String strParentNodeExpand = ",";
        public ArrayList arrSearchedCategory = new ArrayList();
        Int32 nodecount = -1;
        Int32 nodecount1 = 0;
        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            clsvariables.LoadAllPath();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"].ToString().Equals("Inserted"))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Category Inserted Successfully.', 'Message');});", true);
                    }
                    else if (Request.QueryString["mode"].ToString().Equals("Updated"))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Category Updated Successfully.', 'Message');});", true);
                    }
                }

                if (Request.QueryString["Search"] != null)
                {
                    ViewState["SearchEnabled"] = "1";
                    txtSearch.Text = Request.QueryString["Search"].ToString();
                }
                if (Request.QueryString["ActiveStatus"] != null)
                {
                    rdlCategoryStatus.SelectedIndex = Convert.ToInt32(Request.QueryString["ActiveStatus"]);
                }
                if (Request.QueryString["Mode"] != null && Request.QueryString["ID"] != null && Request.QueryString["Mode"] == "edit")
                {
                    lblTitle.Text = "Update Category";
                    BindData(Convert.ToInt32(Request.QueryString["ID"]));                  
                }
                else
                {
                    lblTitle.Text = "Add Category";
                    rblIsfeaturedNo.Checked = true;
                    BindImage("");
                }
                bindTreeviewwithcategory();
                FillMasterCategorylist();

            }

            BindSizes();
            Page.MaintainScrollPositionOnPostBack = true;
        }

        private void BindSizes()
        {
            try
            {
                DataSet dsIconWidth = objConfiguration.GetImageSizeByType( "CategoryIconWidth");
                DataSet dsIconHeight = objConfiguration.GetImageSizeByType( "CategoryIconHeight");
                if ((dsIconWidth != null && dsIconWidth.Tables.Count > 0 && dsIconWidth.Tables[0].Rows.Count > 0) && (dsIconHeight != null && dsIconHeight.Tables.Count > 0 && dsIconHeight.Tables[0].Rows.Count > 0))
                {
                    thumbNailSizeIcon = new Size(Convert.ToInt32(dsIconWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsIconHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
                }
                DataSet dsBannerWidth = objConfiguration.GetImageSizeByType( "CategoryBannerWidth");
                DataSet dsBannerHeight = objConfiguration.GetImageSizeByType( "CategoryBannerHeight");
                if ((dsBannerWidth != null && dsBannerWidth.Tables.Count > 0 && dsBannerWidth.Tables[0].Rows.Count > 0) && (dsBannerHeight != null && dsBannerHeight.Tables.Count > 0 && dsBannerHeight.Tables[0].Rows.Count > 0))
                {
                    thumbNailSizeBanner = new Size(Convert.ToInt32(dsBannerWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsBannerHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));

                }
            }
            catch { }
        }

        private void BindData(int catId)
        {
            DataSet ds = catcomp.getCatdetailbycatid(catId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["DisplayOrder"] != null)
                    txtdisplayorder.Text = ds.Tables[0].Rows[0]["DisplayOrder"].ToString();
                else
                    txtdisplayorder.Text = "0";

                txtSummary.Text = Server.HtmlDecode(ds.Tables[0].Rows[0]["Summary"].ToString());
                txtDescription.Text = Server.HtmlDecode(ds.Tables[0].Rows[0]["Description"].ToString());
                txtseodescription.Text = ds.Tables[0].Rows[0]["SEDescription"].ToString();
                txtsetitle.Text = ds.Tables[0].Rows[0]["SETitle"].ToString();
                txtsekeyword.Text = ds.Tables[0].Rows[0]["SEKeywords"].ToString();

                if (rblPublished.Items.FindByValue(ds.Tables[0].Rows[0]["Active"].ToString()) != null)
                    rblPublished.Items.FindByValue(ds.Tables[0].Rows[0]["Active"].ToString()).Selected = true;
                else rblPublished.Items.FindByValue("False").Selected = true;

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IsFeatured"].ToString()))
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeatured"].ToString()))
                    { rblIsfeatured.Checked = true; }
                    else
                        rblIsfeaturedNo.Checked = true;
                }

                txtHeaderText.Text = Convert.ToString(ds.Tables[0].Rows[0]["HeaderText"].ToString());
                txtShortTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ShortName"].ToString());

                BindImage(ds.Tables[0].Rows[0]["ImageName"].ToString());
                ViewState["ImageName"] = Convert.ToString(ds.Tables[0].Rows[0]["ImageName"]);

                if (Request.QueryString["ID"] != null )
                {                    
                    DataSet DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=0");
                    if (DSTempCat != null && DSTempCat.Tables.Count > 0 && DSTempCat.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                        {
                            if (Request.QueryString["ID"] != null && Convert.ToInt32(dr["CategoryID"]) == Convert.ToInt32(Request.QueryString["ID"]))
                            {
                                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ShowOnHeader"].ToString()) && Convert.ToBoolean(ds.Tables[0].Rows[0]["ShowOnHeader"].ToString()))
                                    chkShowOnHeader.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ShowOnHeader"].ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                rblIsfeaturedNo.Checked = true;
            }
        }

        private void BindImage(string imageName)
        {
            imageName = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Icon/") + imageName;
            if (File.Exists(Server.MapPath(imageName)))
            {
                imgIcon.Src = imageName;

            }
            else
            {
                imgIcon.Src = string.Concat(AppLogic.AppConfigs("ImagePathCategory") + "Icon/image_not_available.jpg");

            }
        }

        private void bindTreeviewwithcategory()
        {
            if (Request.QueryString["ID"] != null)
            {
                bindParentValue();
            }

            tvCategory.Nodes.Clear();
            TreeNode rootnode = new TreeNode();
            rootnode.Text = "Root Category";
            rootnode.Value = "0";
            rootnode.NavigateUrl = "javascript:void(0);";
            rootnode.ShowCheckBox = true;
            tvCategory.Nodes.Add(rootnode);

            DataSet DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=0 and c.Deleted=0");

            if (DSTempCat != null && DSTempCat.Tables.Count > 0 && DSTempCat.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                {
                    if (Request.QueryString["ID"] != null && Convert.ToInt32(dr["CategoryID"]) != Convert.ToInt32(Request.QueryString["ID"]))
                    {
                        TreeNode node = new TreeNode(dr["Name"].ToString(), dr["CategoryID"].ToString());
                        node.NavigateUrl = "javascript:void(0);";
                        node.ShowCheckBox = true;

                        if (strNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                        {
                            if (strParentNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                            {
                                node.Checked = true;
                            }
                            node.Expanded = true;
                        }
                        else
                        {
                            node.Checked = false;
                            node.Collapse();
                        }

                        tvCategory.Nodes.Add(node);
                        childnode(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), node);
                    }
                    else if (Request.QueryString["ID"] == null)
                    {
                        TreeNode node = new TreeNode(dr["Name"].ToString(), dr["CategoryID"].ToString());
                        node.NavigateUrl = "javascript:void(0);";
                        node.ShowCheckBox = true;
                        node.Collapse();
                        tvCategory.Nodes.Add(node);
                        childnode(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), node);
                    }
                }
            }

            if (Request.QueryString["Root"] != null && Convert.ToString(Request.QueryString["Root"]) == "1")
            {
                tvCategory.Nodes[0].Checked = true;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "ChkRootNode();", true);
            }
            if (Request.QueryString["Mode"] != null && Request.QueryString["ID"] != null && Request.QueryString["Mode"] == "edit")
            {
                if (strNodeExpand.Length == 1)
                {
                    tvCategory.Nodes[0].Checked = true;
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "ChkRootNode();", true);
                }
            }
        }

        private void GetParentCategoryForSearch(String CatName)
        {
            DataSet dsSearchCategory = new DataSet();
            dsSearchCategory = catcomp.SearchCategory(CatName, 3);
            if (dsSearchCategory != null && dsSearchCategory.Tables.Count > 0 && dsSearchCategory.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSearchCategory.Tables[0].Rows.Count; i++)
                {
                    if (!strSearchedCategory.Contains("," + dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ","))
                    {
                        strSearchedCategory += dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ",";
                    }
                    if (Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]) != 0)
                    {
                        if (!strSearchedCategory.Contains("," + dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ","))
                        {
                            strSearchedCategory += dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ",";
                        }
                        GetParentCategoryForSearch(Convert.ToString(dsSearchCategory.Tables[0].Rows[i]["ParentcategoyName"]));
                    }
                }
            }
        }

        public Int32 PostCount(Int32 CategoryId)
        {
            DataSet dsCategory = new DataSet();
            dsCategory = commandac.GetCommonDataSet("EXEC usp_CategoryPostCount " + CategoryId + ", 0");
            Int32 PCount = 0;
            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                DataSet dsParent = new DataSet();

                dsParent = commandac.GetCommonDataSet("SELECT count(Postid) as tt FROM dbo.tb_Post WHERE PostID IN(SELECT PostID FROM dbo.tb_PostCategory WHERE CategoryID=" + CategoryId + ") AND ISNULL(Active ,0)=1 AND ISNULL(Deleted,0)=0");
                if (dsParent != null && dsParent.Tables.Count > 0 && dsParent.Tables[0].Rows.Count > 0)
                {
                    PCount = Convert.ToInt32(dsParent.Tables[0].Rows[0]["tt"].ToString());
                }
                Int32 catid = 0;
                Int32 parentcategid = 0;
                for (int i = 0; i < dsCategory.Tables[0].Rows.Count; i++)
                {
                    if (catid != Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString()) || parentcategid != Convert.ToInt32(dsCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString()))
                    {

                        PCount = LoadCategoryChild(Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString()), PCount);

                    }
                    catid = Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString());
                    parentcategid = Convert.ToInt32(dsCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString());
                }
            }
            else
            {
                dsCategory = commandac.GetCommonDataSet("SELECT count(Postid) as tt FROM dbo.tb_Post WHERE PostID IN(SELECT PostID FROM dbo.tb_PostCategory WHERE CategoryID=" + CategoryId + ") AND ISNULL(Active ,0)=1 AND ISNULL(Deleted,0)=0");
                if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
                {
                    PCount = Convert.ToInt32(dsCategory.Tables[0].Rows[0]["tt"].ToString());
                }
            }
            return PCount;

        }

        public Int32 LoadCategoryChild(Int32 CategoryId, Int32 count)
        {

            DataSet dsCategory = new DataSet();
            dsCategory = commandac.GetCommonDataSet("EXEC usp_CategoryPostCount " + CategoryId + ", 0");

            if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                DataSet dsParent = new DataSet();
                dsParent = commandac.GetCommonDataSet("SELECT count(Postid) as tt FROM dbo.tb_Post WHERE PostID IN(SELECT PostID FROM dbo.tb_PostCategory WHERE CategoryID=" + CategoryId + ") AND ISNULL(Active ,0)=1 AND ISNULL(Deleted,0)=0");
                if (dsParent != null && dsParent.Tables.Count > 0 && dsParent.Tables[0].Rows.Count > 0)
                {
                    count += Convert.ToInt32(dsParent.Tables[0].Rows[0]["tt"].ToString());
                }
                Int32 catid = 0;
                Int32 parentcategid = 0;
                for (int i = 0; i < dsCategory.Tables[0].Rows.Count; i++)
                {
                    if (catid != Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString()) || parentcategid != Convert.ToInt32(dsCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString()))
                    {


                        count = LoadCategoryChild(Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString()), count);

                    }
                    catid = Convert.ToInt32(dsCategory.Tables[0].Rows[i]["CategoryID"].ToString());
                    parentcategid = Convert.ToInt32(dsCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString());
                }

            }
            else
            {
                dsCategory = commandac.GetCommonDataSet("SELECT count(Postid) as tt FROM dbo.tb_Post WHERE PostID IN(SELECT PostID FROM dbo.tb_PostCategory WHERE CategoryID=" + CategoryId + ") AND ISNULL(Active ,0)=1 AND ISNULL(Deleted,0)=0");
                if (dsCategory != null && dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
                {
                    count += Convert.ToInt32(dsCategory.Tables[0].Rows[0]["tt"].ToString());
                }

            }
            return count;
        }

        private void bindParentValue()
        {
            sbCatMap = new System.Text.StringBuilder();
            strNodeExpand = ",";
            strParentNodeExpand = ",";
            DataSet dsSearchCategory = new DataSet();
            dsSearchCategory = catcomp.ExpandedCategory(Convert.ToInt32(Request.QueryString["ID"]), 2);

            if (dsSearchCategory != null && dsSearchCategory.Tables.Count > 0 && dsSearchCategory.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsSearchCategory.Tables[0].Rows.Count; j++)
                {
                    if (!strParentNodeExpand.Contains("," + dsSearchCategory.Tables[0].Rows[j]["ParentCategoryID"].ToString() + ","))
                    {
                        strParentNodeExpand += dsSearchCategory.Tables[0].Rows[j]["ParentCategoryID"].ToString() + ",";
                    }
                }

                for (int i = 0; i < dsSearchCategory.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]) != 0)
                    {
                        if (!strNodeExpand.Contains("," + dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ","))
                        {
                            strNodeExpand += dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ",";
                        }
                        parsenode(Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]));
                    }
                }
            }

            if (Request.QueryString["ID"] != null)
            {
                DataSet dsParentCategory = catcomp.GetCategoryDetailBycategoryIdWithParentCategoryID(Convert.ToInt32(Request.QueryString["ID"]));
                if (dsParentCategory != null && dsParentCategory.Tables.Count > 0 && dsParentCategory.Tables[0].Rows.Count > 0)
                {
                    int pCount = dsParentCategory.Tables[0].Rows.Count;
                    for (int i = 0; i < pCount; i++)
                    {
                        sbCatMap.Append(dsParentCategory.Tables[0].Rows[i]["CategoryMappingID"].ToString() + ",");
                    }
                }
            }
        }

        private void parsenode(Int32 CatID)
        {
            DataSet dsSearchCategory = new DataSet();
            dsSearchCategory = catcomp.ExpandedCategory(CatID, 2);
            if (dsSearchCategory != null && dsSearchCategory.Tables.Count > 0 && dsSearchCategory.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSearchCategory.Tables[0].Rows.Count; i++)
                {
                    if (!strNodeExpand.Contains("," + dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ","))
                    {
                        strNodeExpand += dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ",";
                    }
                    if (Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]) != 0)
                    {
                        if (!strNodeExpand.Contains("," + dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ","))
                        {
                            strNodeExpand += dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ",";
                        }
                        parsenode(Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]));
                    }
                }
            }
        }

        private void childnode(int catId, TreeNode node)
        {
            DataSet DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=" + catId + " and c.Deleted=0");
            if (DSTempCat != null && DSTempCat.Tables.Count > 0 && DSTempCat.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                {
                    if (Request.QueryString["ID"] != null && Convert.ToInt32(dr["CategoryID"]) != Convert.ToInt32(Request.QueryString["ID"]))
                    {
                        TreeNode tnchild = new TreeNode();
                        tnchild.NavigateUrl = "javascript:void(0);";
                        tnchild.Value = dr["CategoryID"].ToString();
                        tnchild.ShowCheckBox = true;
                        tnchild.Text = dr["Name"].ToString();
                        tnchild.ToolTip = dr["Name"].ToString();

                        if (strNodeExpand.Contains("," + dr["Name"].ToString() + ","))
                        {
                            if (strParentNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                            {
                                tnchild.Checked = true;
                            }
                            tnchild.Expanded = true;
                        }
                        else
                        {
                            tnchild.Checked = false;
                            tnchild.Collapse();
                        }

                        node.ChildNodes.Add(tnchild);
                        childnode(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), tnchild);
                    }
                    else if (Request.QueryString["ID"] == null)
                    {
                        TreeNode tnchild = new TreeNode();
                        tnchild.NavigateUrl = "javascript:void(0);";
                        tnchild.Value = dr["CategoryID"].ToString();
                        tnchild.ShowCheckBox = true;
                        tnchild.Text = dr["Name"].ToString();
                        tnchild.ToolTip = dr["Name"].ToString();
                        node.ChildNodes.Add(tnchild);
                        childnode(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), tnchild);
                    }
                }
            }
        }

        private void FillMasterCategorylist()
        {
            catid.Visible = false;
            tvMasterCategoryList.Nodes.Clear();

            #region For Search Category

            if (ViewState["SearchEnabled"] != null && Convert.ToString(ViewState["SearchEnabled"]) == "1" && txtSearch.Text.Trim() != "")
            {
                DataSet dsSearchCategory = new DataSet();
                if (ddlSearch.SelectedIndex == 1)
                {
                    dsSearchCategory = catcomp.SearchCategory(txtSearch.Text.Trim(), 1);
                    if (dsSearchCategory != null && dsSearchCategory.Tables.Count > 0 && dsSearchCategory.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsSearchCategory.Tables[0].Rows.Count; i++)
                        {
                            if (!strSearchedCategory.Contains("," + dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ","))
                            {
                                strSearchedCategory += dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ",";
                            }
                        }
                    }
                }
                else if (ddlSearch.SelectedIndex == 0)
                {
                    dsSearchCategory = catcomp.SearchCategory(txtSearch.Text.Trim(), 2);

                    if (dsSearchCategory != null && dsSearchCategory.Tables.Count > 0 && dsSearchCategory.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsSearchCategory.Tables[0].Rows.Count; i++)
                        {
                            if (!strSearchedCategory.Contains("," + dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ","))
                            {
                                strSearchedCategory += dsSearchCategory.Tables[0].Rows[i]["CategoryID"].ToString() + ",";
                            }

                            if (Convert.ToInt32(dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"]) != 0)
                            {
                                if (!strSearchedCategory.Contains("," + dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ","))
                                {
                                    strSearchedCategory += dsSearchCategory.Tables[0].Rows[i]["ParentCategoryID"].ToString() + ",";
                                }
                                GetParentCategoryForSearch(Convert.ToString(dsSearchCategory.Tables[0].Rows[i]["ParentcategoyName"]));
                            }
                        }
                    }
                }
            }

            #endregion

            DataSet DSTempCat = null;

            if (rdlCategoryStatus.SelectedIndex != 0)
            {
                if (rdlCategoryStatus.SelectedIndex == 1)
                {
                    DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=0 and c.Deleted=0 and c.Active=1 ");

                }
                else
                {
                    DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=0 and c.Deleted=0 and c.Active=0 ");
                }

            }
            else
            {
                DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID=0 and c.Deleted=0");
            }

            if (DSTempCat != null && DSTempCat.Tables.Count > 0 && DSTempCat.Tables[0].Rows.Count > 0)
            {
                if (ViewState["SearchEnabled"] != null && Convert.ToString(ViewState["SearchEnabled"]) == "1" && txtSearch.Text.Trim() != "")
                {
                    foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                    {
                        if (strSearchedCategory.Contains("," + dr["CategoryID"].ToString() + ","))
                        {
                            nodecount++;
                            string Catename = "";
                            Int32 Pc = PostCount(Convert.ToInt32(dr["CategoryID"]));
                            if (Pc > 0)
                            {
                                Catename = dr["Name"].ToString() + "(" + Pc + ")";
                            }
                            else
                            {
                                Catename = dr["Name"].ToString() + "(" + Pc + ")";
                            }
                            TreeNode node = new TreeNode(Catename, dr["CategoryID"].ToString());
                            node.ToolTip = dr["Name"].ToString();
                            if (txtSearch.Text.ToString().Trim() != "")
                            {
                                node.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Search=" + Server.UrlEncode(txtSearch.Text.ToString().Trim()) + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                            }
                            else
                            {
                                node.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                            }
                            node.ImageUrl = "/Admin/Images/tree-folder.gif";

                            if (strNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                            {
                                node.Expanded = true;
                                node.Selected = true;
                                node.SelectAction = TreeNodeSelectAction.SelectExpand;

                            }
                            else
                            {
                                node.Checked = false;
                                node.Selected = false;
                                node.Collapse();
                            }


                            tvMasterCategoryList.Nodes.Add(node);
                            BindchildnodeForMaster(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), node);
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                    {
                        nodecount++;
                        string Catename = "";
                        Int32 Pc = PostCount(Convert.ToInt32(dr["CategoryID"]));
                        if (Pc > 0)
                        {
                            Catename = dr["Name"].ToString() + "(" + Pc + ")";
                        }
                        else
                        {
                            Catename = dr["Name"].ToString() + "(" + Pc + ")";
                        }

                        TreeNode node = new TreeNode(Catename, dr["CategoryID"].ToString());
                        node.ToolTip = dr["Name"].ToString();
                        if (txtSearch.Text.ToString().Trim() != "")
                        {
                            node.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Search=" + Server.UrlEncode(txtSearch.Text.ToString().Trim()) + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                        }
                        else
                        {
                            node.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                        }
                        node.ImageUrl = "/Admin/Images/tree-folder.gif";

                        if (strNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                        {
                            node.Expanded = true;
                            node.Selected = true;
                            node.SelectAction = TreeNodeSelectAction.SelectExpand;
                        }
                        else
                        {
                            node.Checked = false;
                            node.Selected = false;
                            node.Collapse();
                        }
                        tvMasterCategoryList.Nodes.Add(node);
                        BindchildnodeForMaster(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), node);
                    }
                }
            }

            if (tvMasterCategoryList.Nodes.Count == 0)
            {
                trCollexpand.Visible = false;
            }
            else
            {
                trCollexpand.Visible = true;
            }
        }

        private void BindchildnodeForMaster(int catId, TreeNode node)
        {


            DataSet DSTempCat = commandac.GetCommonDataSet("select cm.CategoryID,ParentCategoryID,c.Name from tb_CategoryMapping cm INNER JOIN tb_Category c on c.CategoryID=cm.CategoryID where ParentCategoryID='" + catId + "' and c.Deleted=0");


            if (DSTempCat != null && DSTempCat.Tables.Count > 0 && DSTempCat.Tables[0].Rows.Count > 0)
            {
                if (ViewState["SearchEnabled"] != null && Convert.ToString(ViewState["SearchEnabled"]) == "1" && txtSearch.Text.Trim() != "" && ddlSearch.SelectedIndex == 0)
                {
                    foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                    {
                        if (strSearchedCategory.Contains("," + Convert.ToInt32(dr["CategoryID"]) + ","))
                        {
                            nodecount++;
                            TreeNode tnchild = new TreeNode();
                            if (txtSearch.Text.ToString().Trim() != "")
                            {
                                tnchild.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Search=" + Server.UrlEncode(txtSearch.Text.ToString().Trim()) + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                            }
                            else
                            {

                                tnchild.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                            }
                            tnchild.Value = dr["CategoryID"].ToString();

                            string Catename = "";
                            Int32 Pc = PostCount(Convert.ToInt32(dr["CategoryID"]));
                            if (Pc > 0)
                            {
                                Catename = dr["Name"].ToString() + "(" + Pc + ")";
                            }
                            else
                            {
                                Catename = dr["Name"].ToString() + "(" + Pc + ")";
                            }

                            tnchild.Text = Catename;
                            tnchild.ToolTip = dr["Name"].ToString();
                            tnchild.ImageUrl = "/Admin/Images/tree-folder.gif";

                            if (strNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                            {
                                tnchild.Expanded = true;
                                tnchild.Selected = true;
                                tnchild.SelectAction = TreeNodeSelectAction.SelectExpand;
                            }
                            else
                            {
                                tnchild.Checked = false;
                                tnchild.Selected = false;
                                tnchild.Collapse();
                            }

                            node.ChildNodes.Add(tnchild);
                            BindchildnodeForMaster(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), tnchild);
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in DSTempCat.Tables[0].Rows)
                    {
                        nodecount++;
                        TreeNode tnchild = new TreeNode();
                        if (txtSearch.Text.ToString().Trim() != "")
                        {
                            tnchild.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Search=" + Server.UrlEncode(txtSearch.Text.ToString().Trim()) + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                        }
                        else
                        {
                            tnchild.NavigateUrl = "/Admin/Posts/Category.aspx?Node=" + nodecount.ToString() + "&ActiveStatus=" + rdlCategoryStatus.SelectedIndex.ToString() + "&Mode=edit&ID=" + dr["CategoryID"].ToString();
                        }
                        tnchild.Value = dr["CategoryID"].ToString();
                        string Catename = "";
                        Int32 Pc = PostCount(Convert.ToInt32(dr["CategoryID"]));
                        if (Pc > 0)
                        {
                            Catename = dr["Name"].ToString() + "(" + Pc + ")";
                        }
                        else
                        {
                            Catename = dr["Name"].ToString() + "(" + Pc + ")";
                        }

                        tnchild.Text = Catename;
                        tnchild.ToolTip = dr["Name"].ToString();
                        tnchild.ImageUrl = "/Admin/Images/tree-folder.gif";
                        if (strNodeExpand.Contains("," + dr["CategoryID"].ToString() + ","))
                        {
                            tnchild.Expanded = true;
                            tnchild.Selected = true;
                            tnchild.SelectAction = TreeNodeSelectAction.SelectExpand;

                        }
                        else
                        {
                            tnchild.Checked = false;
                            tnchild.Selected = false;
                            tnchild.Collapse();
                        }

                        node.ChildNodes.Add(tnchild);
                        BindchildnodeForMaster(Convert.ToInt32(dr["CategoryID"].ToString().Trim()), tnchild);
                    }
                }

            }
        }


        public string GetSelectedParents()
        {
            String SelectedParents = string.Empty;
            foreach (TreeNode tn in tvCategory.Nodes)
            {
                if (tn.Checked == true)
                {
                    SelectedParents += "," + tn.Value;
                }
                SelectedParents = GetCategoryIDList(SelectedParents, tn);
            }

            if (SelectedParents.Length > 0 && SelectedParents.ToString().Substring(0, 1) == ",")
                SelectedParents = SelectedParents.Substring(1);

            return SelectedParents;
        }

        public String GetCategoryIDList(String SelectedParents, TreeNode tn)
        {
            foreach (TreeNode tchild in tn.ChildNodes)
            {
                if (tchild.Checked == true)
                {
                    SelectedParents += "," + tchild.Value;
                }
                SelectedParents = GetCategoryIDList(SelectedParents, tchild);
            }
            return SelectedParents;
        }

        protected void imgbtnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Mode"] != null && Request.QueryString["ID"] != null )
            {
                updateCategory();
                Response.Redirect("/Admin/Posts/Category.aspx?mode=Updated");
            }
            else
            {
                #region Check Name is Exist or Not
                if (txtname.Text != "")
                {
                    DataSet dsCatname = catcomp.getCategorydetails(txtname.Text.Trim());
                    if (dsCatname != null && dsCatname.Tables.Count > 0 && dsCatname.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Text = "This Category Name Already Exists!";
                    }
                    else
                    {
                        insertCategory();
                        Response.Redirect("/Admin/Posts/Category.aspx?mode=Inserted");
                    }
                }
                #endregion
            }
        }

        private void updateCategory()
        {
            int CategoryID = Convert.ToInt32(Request.QueryString["ID"]);
            string Name = txtname.Text.Trim();
            string sename = RemoveSpecialCharacter(txtname.Text.Trim().ToLower().ToCharArray());
            string SEDescription = txtseodescription.Text.Trim();
            string SEKeywords = txtsekeyword.Text.Trim();
            int CreatedBy = Convert.ToInt32(Session["AdminID"]);
            DateTime CreatedOn = System.DateTime.Now;
            string SETitle = txtsetitle.Text.Trim();
            string Summary = txtSummary.Text.ToString();
            string Description = txtDescription.Text.ToString();
            bool Active = Convert.ToBoolean(rblPublished.SelectedValue);
            int UpdatedBy = Convert.ToInt32(Session["AdminID"]);
            DateTime UpdatedOn = System.DateTime.Now;
            string ImageName = string.Empty;
            string BannerImageName = string.Empty;
            int DisplayOrder = 0;
            bool IsFeatured;
            bool ShowOnHeader = false;
            bool Deleted = false;
            if (ViewState["File"] != null)
            {
                ImageName = sename + "_" + Request.QueryString["ID"].ToString() + ".jpg";//ViewState["File"].ToString();
                SaveImage(ImageName, "");
            }
            else
            {
                if (!imgIcon.Src.ToString().Contains("image_not_available"))
                {
                    if (ViewState["ImageName"] != null)
                        ImageName = Convert.ToString(ViewState["ImageName"]);
                }
            }
       
            if (txtdisplayorder.Text != null && txtdisplayorder.Text != "")
                DisplayOrder = Convert.ToInt32(txtdisplayorder.Text.Trim());



            if (rblIsfeatured.Checked == true)
                IsFeatured = Convert.ToBoolean(rblIsfeatured.Checked);
            else IsFeatured = false;

            string HeaderText = txtHeaderText.Text.ToString().Trim().Replace("'", "''");
            string ShortName = txtShortTitle.Text.ToString().Trim().Replace("'", "''");

            if (tvCategory.Nodes[0].Checked == true)
            {
                ShowOnHeader = chkShowOnHeader.Checked;
            }
            catcomp.updateCategory(CategoryID, Name, sename, SEDescription, SEKeywords, CreatedBy, CreatedOn, SETitle, Summary, Description, Active, UpdatedBy, UpdatedOn, ImageName, BannerImageName, DisplayOrder, IsFeatured, ShowOnHeader, Deleted, HeaderText, ShortName, 2);

            string ChkParent = GetSelectedParents();
            int j = 0;

            objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), 0, "Category", 2, 0);

            String[] arrParent = ChkParent.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (String strParent in arrParent)
            {
                parentValue = sbCatMap.ToString().Split(',');

                string[] pv = new string[parentValue.Length - 1];
                Array.Copy(parentValue, pv, parentValue.Length - 1);
                int count = pv.Count();
                int p = pv.Count();

                #region same mapping count
                if (arrParent.Count() == count)
                {
                    for (int k = j; j < arrParent.Count(); j++)
                    {
                        catcomp.Update(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent), Convert.ToInt32(pv[j].ToString()));
                        objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent) , "Category", 1, 1);
                        j += 1;
                        break;
                    }
                }
                #endregion

                #region if selected node greater than mapping count
                else if (arrParent.Count() > count)
                {
                    for (int k = j; j < arrParent.Count(); j++)
                    {
                        if (j < count)
                        {
                            catcomp.Update(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent), Convert.ToInt32(pv[j].ToString()));
                            objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent), "Category", 1, 1);
                            j = k + 1;
                            break;
                        }
                        else
                        {
                            int ParentCategoryID = Convert.ToInt32(strParent);
                            int CategoryIDMap = Convert.ToInt32(Request.QueryString["ID"]);
                            commandac.ExecuteCommonData("insert into tb_CategoryMapping (CategoryID,ParentCategoryID) Values ('" + CategoryIDMap + "','" + ParentCategoryID + "')");
                            objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent), "Category", 1, 1);
                            break;
                        }
                    }

                }
                #endregion
                else if (arrParent.Count() < count)
                {
                    for (int k = j; j <= pv.Count(); j++)
                    {

                        if (j < arrParent.Count())
                        {
                            catcomp.Update(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent), Convert.ToInt32(pv[j].ToString()));
                            objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(strParent),  "Category", 1, 1);
                            j = k + 1;
                            if (j != arrParent.Count())
                                break;
                        }
                        else
                        {
                            catcomp.delete(Convert.ToInt32(pv[j - 1].ToString()));

                        }
                    }
                }


            }

        }

        private void insertCategory()
        {
            String BannerImageName = string.Empty;
            String bannerPath = string.Empty;

            string Name = txtname.Text.Trim();
            string sename = RemoveSpecialCharacter(txtname.Text.Trim().ToLower().ToCharArray());
            string SEDescription = txtseodescription.Text.Trim();
            string SEKeywords = txtsekeyword.Text.Trim();
            int DisplayOrder = 0;
            DateTime CreatedOn = DateTime.Now;
            string SETitle = txtsetitle.Text.Trim();
            string Summary = txtSummary.Text.ToString();
            string Description = txtDescription.Text.ToString();
            bool Active = Convert.ToBoolean(rblPublished.SelectedValue);
            int CreatedBy = Convert.ToInt32(Session["AdminID"]);           
            if (txtdisplayorder.Text != null && txtdisplayorder.Text != "")
                DisplayOrder = Convert.ToInt32(txtdisplayorder.Text.Trim());

            bool Deleted = false;
            bool IsFeatured = false;
            bool ShowOnHeader = false;
            if (rblIsfeatured.Checked == true)
                IsFeatured = Convert.ToBoolean(rblIsfeatured.Checked);
            else IsFeatured = false;

            string HeaderText = txtHeaderText.Text.ToString().Trim().Replace("'", "''");
            string ShortName = txtShortTitle.Text.ToString().Trim().Replace("'", "''");

            if (tvCategory.Nodes[0].Checked == true)
            {
                ShowOnHeader = chkShowOnHeader.Checked;
            }

            int NewCategory = 0;
            NewCategory = catcomp.insertcategory(Name, sename, SEDescription, SEKeywords, CreatedBy, CreatedOn, SETitle, Summary, Description, Active, BannerImageName, DisplayOrder, IsFeatured, ShowOnHeader, Deleted, HeaderText, ShortName, 1);

            string catid = NewCategory.ToString();

            if (ViewState["File"] != null)
            {
                string ImageName = sename + "_" + catid + ".jpg";
                SaveImage(ImageName, "");
                commandac.ExecuteCommonData("update tb_category set imagename='" + ImageName + "' where categoryid=" + catid);
            }

            int parentId = 0;

            objConfiguration.GetBreadCrum(Convert.ToInt32(Request.QueryString["ID"]), 0 , "Category", 2, 0);
            string ChkParent = GetSelectedParents();
            if (ChkParent == "")
            {
                if (catid != null)
                {
                    int ParentCategoryID = parentId;
                    int CategoryIDMap = Convert.ToInt32(catid);
                    commandac.ExecuteCommonData("insert into tb_CategoryMapping (CategoryID,ParentCategoryID) Values ('" + CategoryIDMap + "','" + ParentCategoryID + "')");
                    objConfiguration.GetBreadCrum(Convert.ToInt32(catid), parentId , "Category", 1, 1);
                }
            }
            else
            {
                String[] arrParent = ChkParent.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (String strParent in arrParent)
                {
                    int ParentCategoryID = Convert.ToInt32(strParent);
                    int CategoryIDMap = Convert.ToInt32(catid);
                    commandac.ExecuteCommonData("insert into tb_CategoryMapping (CategoryID,ParentCategoryID) Values ('" + CategoryIDMap + "','" + ParentCategoryID + "')");
                    objConfiguration.GetBreadCrum(Convert.ToInt32(catid), Convert.ToInt32(strParent) , "Category", 1, 1);
                }
            }
            clearselection();
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
        protected void SaveImage(string FileName, string type)
        {
            CategoryTempPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Temp/");
            CategoryIconPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Icon/");
            CategoryMediumPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Medium/");
            CategoryLargePath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Large/");
            CategoryMicroPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Micro/");
            CategoryBannerPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Banner/");
            CategoryBannerTempPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Temp/Banner/");
            //create icon folder 
            //clsvariables.LoadAllPath();
            if (!Directory.Exists(Server.MapPath(CategoryIconPath)))
                Directory.CreateDirectory(Server.MapPath(CategoryIconPath));

            //create Medium folder 
            if (!Directory.Exists(Server.MapPath(CategoryMediumPath)))
                Directory.CreateDirectory(Server.MapPath(CategoryMediumPath));

            //create Large folder 
            if (!Directory.Exists(Server.MapPath(CategoryLargePath)))
                Directory.CreateDirectory(Server.MapPath(CategoryLargePath));

            //create Banner folder 
            if (!Directory.Exists(Server.MapPath(CategoryBannerPath)))
                Directory.CreateDirectory(Server.MapPath(CategoryBannerPath));

            //create Micro folder 
            if (!Directory.Exists(Server.MapPath(CategoryMicroPath)))
                Directory.CreateDirectory(Server.MapPath(CategoryMicroPath));

            if (type != "banner" && type == "")
            {
                if (imgIcon.Src.Contains(CategoryTempPath))
                {
                    try
                    {
                        CreateImage("Icon", FileName);
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
                        path = Server.MapPath(CategoryTempPath + ViewState["File"].ToString());
                    }

                    File.Delete(path);
                }              
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("Category.aspx - Admin", ex.Message, ex.StackTrace);
            }
        }

        protected void DeleteImage(string ImageName)
        {
            try
            {
                if (File.Exists((ImageName)))
                    File.Delete((ImageName));

            }
            catch (Exception ex)
            {
                lblMsg.Text += "<br />" + ex.Message;
            }
        }

        protected void CreateImage(string Size, string FileName)
        {
            try
            {
                string strFile = "";
                String strPath = "";
                string strFileBanner = "";
                String strPathBanner = "";
                Size = Size.ToLower();

                if (imgIcon.Src.ToString().IndexOf("?") > -1)
                {
                    strPath = imgIcon.Src.Split('?')[0];
                }
                else
                {
                    strPath = imgIcon.Src.ToString();
                }
                
                strFile = Server.MapPath(strPath);
                strFileBanner = Server.MapPath(strPathBanner);
                string Path = "";
                switch (Size)
                {
                    case "icon":
                        Path = Server.MapPath(CategoryIconPath + FileName);
                        if (Request.QueryString["Mode"] == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(Server.MapPath(CategoryIconPath + ViewState["DelImage"].ToString()));
                            }
                        }
                        break;
                    case "micro":
                        Path = Server.MapPath(CategoryMicroPath + FileName);
                        if (Request.QueryString["Mode"] == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(Server.MapPath(CategoryMicroPath + ViewState["DelImage"].ToString()));
                            }
                        }
                        break;
                    case "banner":
                        Path = Server.MapPath(CategoryBannerPath + FileName);
                        if (Request.QueryString["Mode"] == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(Server.MapPath(CategoryBannerPath + ViewState["DelImage"].ToString()));
                            }
                        }
                        break;
                }
                if (Size == "icon")
                    ResizePhoto(strFile, Size, Path);
                if (Size == "banner")
                    ResizePhoto(strFileBanner, Size, Path);
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Drawing")
                    lblMsg.Text = "<br />Error Saving " + Size + " Image..Please check that Directory exists..";
                else
                    lblMsg.Text += "<br />" + ex.Message;
                CommonDAC.ErrorLog("Category.aspx - Admin", ex.Message, ex.StackTrace);
            }
        }

        public void ResizePhoto(string strFile, string Size, string strFilePath)
        {
            switch (Size)
            {
                case "icon":
                    finHeight = thumbNailSizeIcon.Height;
                    finWidth = thumbNailSizeIcon.Width;
                    break;
                case "micro":
                    finHeight = thumbNailSizeMicro.Height;
                    finWidth = thumbNailSizeMicro.Width;
                    break;
                case "banner":
                    finHeight = thumbNailSizeBanner.Height;
                    finWidth = thumbNailSizeBanner.Width;
                    break;
            }
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
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == resizeMimeType)
                    return codecs[i];
            return null;
        }

        private void clearselection()
        {
            txtdisplayorder.Text = string.Empty;
            txtcatidbystore.Text = string.Empty;
            txtname.Text = string.Empty;
            txtsekeyword.Text = string.Empty;
            txtseodescription.Text = string.Empty;
            txtsetitle.Text = string.Empty;
        }

        protected void imgCancle_Click(object sender, EventArgs e)
        {
            clearselection();
            Response.Redirect("/Admin/Posts/Category.aspx");
        }

        protected void ibtnUpload_Click(object sender, EventArgs e)
        {
            bool Flag = false;
            StringArrayConverter Storeconvertor = new StringArrayConverter();
            string sImageextension = AppLogic.AppConfigs("AllowedExtensions");
            Array StoreArray = (Array)Storeconvertor.ConvertFrom(sImageextension);
            string CategoryTempPath = string.Concat(AppLogic.AppConfigs("ImagePathCategory"), "Temp/");
            if (!Directory.Exists(Server.MapPath(CategoryTempPath)))
                Directory.CreateDirectory(Server.MapPath(CategoryTempPath));


            for (int j = 0; j < StoreArray.Length; j++)
                if (fuIcon.FileName.Length > 0 && Path.GetExtension(fuIcon.FileName.ToString().ToLower()) == StoreArray.GetValue(j).ToString().ToLower())
                    Flag = true;

            if (Flag)
            {
                if (fuIcon.FileName.Length > 0)
                {
                    ViewState["File"] = fuIcon.FileName.ToString();
                    fuIcon.SaveAs(Server.MapPath(CategoryTempPath) + fuIcon.FileName);
                    imgIcon.Src = CategoryTempPath + fuIcon.FileName;
                    lblMsg.Text = "";
                }
            }
            else
                lblMsg.Text = "Only " + AppLogic.AppConfigs("AllowedExtensions") + " Images are allowed";

        }
      
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            try
            {
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                System.IO.StringWriter stringWriter = new System.IO.StringWriter(stringBuilder);
                System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
                base.Render(htmlWriter);
                string yourHtml = stringBuilder.ToString();//.Replace(stringBuilder.ToString().IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=") + ,""); // ***** Parse and Modify This *****
                yourHtml = yourHtml.Replace("id=\"ContentPlaceHolder1_tvCategoryn0CheckBox\"", "id=\"ContentPlaceHolder1_tvCategoryn0CheckBox\" onclick=\"ChkRootNode();\" onchange=\"ChkRootNode();\"");
                yourHtml = yourHtml.Replace("id=&#34;ContentPlaceHolder1_tvMasterCategoryListt", "onclick=\"chkHeight();\" id=&#34;ContentPlaceHolder1_tvMasterCategoryListt");
                yourHtml = yourHtml.Replace("id=\"ContentPlaceHolder1_tvMasterCategoryListt", "onclick=\"chkHeight();\" id=\"ContentPlaceHolder1_tvMasterCategoryListt");
                yourHtml = yourHtml.Replace("style=\"white-space:nowrap;\"", "");



                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:white;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: white;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #ffffff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#ffffff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#fff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #fff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:white", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: white", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #ffffff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#ffffff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#fff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #fff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                if (Request.QueryString["Node"] != null)
                {
                    if (Request.QueryString["Search"] != null && txtSearch.Text.ToString().Trim() == "")
                    {

                    }
                    else
                    {
                        yourHtml = yourHtml.Replace("id=\"ContentPlaceHolder1_tvMasterCategoryListt" + Request.QueryString["Node"].ToString() + "\"", " style=\"Color:#000 !important;\" id=\"ContentPlaceHolder1_tvMasterCategoryListt" + Request.QueryString["Node"].ToString() + "\"");
                    }
                }
                writer.Write(yourHtml);
            }
            catch
            {
            }
        }

        protected void lnkCollapseAll_Click(object sender, EventArgs e)
        {
            tvMasterCategoryList.CollapseAll();
        }

        protected void lnkExpandAll_Click(object sender, EventArgs e)
        {
            tvMasterCategoryList.ExpandAll();
        }

        protected void ibtngo_Click(object sender, EventArgs e)
        {

            strSearchedCategory = ",";
            ViewState["SearchEnabled"] = "1";
            ViewState["SearchText"] = txtSearch.Text.Trim();
            FillMasterCategorylist();
            if (txtSearch.Text.ToString().Trim() == "")
            {
                nodecount1 = 0;
                nodecount = -1;
            }
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdlCategoryStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ibtngo_Click(null, null);
        }


        [System.Web.Services.WebMethodAttribute(),
        System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {

            string cmdString = "";
            List<string> txtItems = new List<string>();
            CommonDAC commandac = new CommonDAC();
            if (contextKey != null && contextKey != "")
            {
                string[] arr;

                string searchby = "";//SKU OR Post Name
                string status = "";//Active, Inactive, Deleted
             



                if (searchby.ToLower() == "name")
                {
                    cmdString = "SELECT Top 20 cat.Name AS CategoryName FROM dbo.tb_Category AS cat INNER JOIN dbo.tb_CategoryMapping AS mapping ON cat.CategoryID = mapping.CategoryID " +
                                  " LEFT OUTER JOIN dbo.tb_Category AS cat1 ON mapping.ParentCategoryID = cat1.CategoryID   WHERE  ISNULL(cat.Deleted,0)=0  " +
                                  " and cat.Name LIKE '%" + prefixText + "%'";

                }
                else if (searchby.ToLower() == "parentcatname")
                {
                    cmdString = "SELECT Top 20 cat.Name AS CategoryName FROM dbo.tb_Category AS cat INNER JOIN dbo.tb_CategoryMapping AS mapping ON " +
                            " cat.CategoryID = mapping.CategoryID LEFT OUTER JOIN dbo.tb_Category AS cat1 ON mapping.ParentCategoryID = cat1.CategoryID  " +
                            " WHERE mapping.ParentCategoryID=0 AND ISNULL(cat.Deleted,0)=0   AND cat.Name LIKE '%" + prefixText + "%' ";
                }


                #region old code

                #endregion

                DataSet ds = commandac.GetCommonDataSet(cmdString);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    String dbValues;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dbValues = row[0].ToString();
                        dbValues = dbValues.ToLower();
                        txtItems.Add(dbValues);
                    }
                }
                else
                {
                }

            }

            return txtItems.ToArray();
        }
    }
}