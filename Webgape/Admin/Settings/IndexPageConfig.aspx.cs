using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;


namespace Webgape.Admin.Settings
{
    public partial class IndexPageConfig : System.Web.UI.Page
    {
        #region declaration
        ConfigurationComponent objAppComp = new ConfigurationComponent();
        CommonDAC commondac = new CommonDAC();
        CategoryComponent catcomp = new CategoryComponent();
        PostComponent postcomp = new PostComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
            AppConfig.StoreID = Convert.ToInt32(ddlStore.SelectedValue.ToString());
            if (!Page.IsPostBack)
            {
                BindFeaturedCategory();
                BindFeaturedSystem();
                BindBestSeller();
                BindNewArrival();
                Int32 ID = Convert.ToInt32(AppLogic.AppConfigs("HotDealPost").ToString());
                bindPost();
                FillHotDealPost();
                GetConfigStatusforMaxPost();
                GetHomeContent();
            }
            GetConfigStatus();
            btnFeatureCategory.Attributes.Add("onclick", "return testi();");
        }

        #region Bind New Arrival Post
        
        private void BindNewArrival()
        {
            DataSet dsNewArrival = commondac.GetCommonDataSet("select * from tb_Post where ISNULL(IsNewArrival,0)=1");
            if (dsNewArrival != null && dsNewArrival.Tables.Count > 0 && dsNewArrival.Tables[0].Rows.Count > 0)
            {
                grdNewarrival.DataSource = dsNewArrival;
                grdNewarrival.DataBind();
            }
            else
            {
                grdNewarrival.DataSource = null;
                grdNewarrival.DataBind();
            }

        }

        protected void grdNewarrival_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdNewarrival.EditIndex = -1;
            BindNewArrival();

        }

        protected void grdNewarrival_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdNewarrival.Rows[e.RowIndex];
            HiddenField hdnPostid = (HiddenField)row.FindControl("hdnPostid");
            TextBox txtDisplayorder = (TextBox)row.FindControl("txtDisplayorder");
            if (txtDisplayorder.Text != "")
            {
                commondac.ExecuteCommonData("update tb_Post set DisplayOrder ='" + txtDisplayorder.Text.Trim() + "'  where PostId = '" + Convert.ToInt32(hdnPostid.Value) + "'   ");
            }
            grdNewarrival.EditIndex = -1;
            BindNewArrival();
        }

        protected void grdNewarrival_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdNewarrival.EditIndex = e.NewEditIndex;
            BindNewArrival();
        }

        #endregion

        #region Bind Best Seller

        private void BindBestSeller()
        {
            DataSet dsBestSeller = commondac.GetCommonDataSet("select * from tb_Post where ISNULL(IsBestSeller,0)=1");
            if (dsBestSeller != null && dsBestSeller.Tables.Count > 0 && dsBestSeller.Tables[0].Rows.Count > 0)
                grdBestSeller.DataSource = dsBestSeller;
            else grdBestSeller.DataSource = null;
            grdBestSeller.DataBind();
        }

        protected void grdBestSeller_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdBestSeller.EditIndex = -1;
            BindBestSeller();
        }

        protected void grdBestSeller_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdBestSeller.EditIndex = e.NewEditIndex;
            BindBestSeller();
        }

        protected void grdBestSeller_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdBestSeller.Rows[e.RowIndex];
            HiddenField hdnPostid = (HiddenField)row.FindControl("hdnPostId");
            TextBox txtDisplayorder = (TextBox)row.FindControl("txtDisplayorder");
            if (txtDisplayorder.Text != "")
            {
                commondac.ExecuteCommonData("update tb_Post set DisplayOrder ='" + txtDisplayorder.Text.Trim() + "'  where PostId = '" + Convert.ToInt32(hdnPostid.Value) + "'   ");
            }
            grdBestSeller.EditIndex = -1;
            BindBestSeller();
        }

        #endregion

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFeaturedCategory(); BindFeaturedSystem(); BindBestSeller(); BindNewArrival();
            GetConfigStatusforMaxPost();
        }

        private void BindFeaturedCategory()
        {
            
            DataSet dsFeaturedCategory = catcomp.GetFeaturedCategory(Convert.ToInt32(ddlStore.SelectedValue));
            if (dsFeaturedCategory != null && dsFeaturedCategory.Tables.Count > 0 && dsFeaturedCategory.Tables[0].Rows.Count > 0)
                grdFeaturedcategory.DataSource = dsFeaturedCategory;
            else grdFeaturedcategory.DataSource = null;
            grdFeaturedcategory.DataBind();
        }

        protected void btnFeatureCategory_Click(object sender, EventArgs e)
        {
            BindFeaturedCategory(); BindFeaturedSystem(); BindBestSeller(); BindNewArrival();
            GetConfigStatusforMaxPost();
        }

        protected void grdFeaturedcategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnCatid = (HiddenField)e.Row.FindControl("hdnCategoryid");
                Label lblParent = (Label)e.Row.FindControl("lblPname");

                DataSet dsPname = catcomp.GetParentCategoryNamebyCategoryID(Convert.ToInt16(hdnCatid.Value));
                if (dsPname != null && dsPname.Tables.Count > 0 && dsPname.Tables[0].Rows.Count > 0)
                {
                    int pCount = dsPname.Tables[0].Rows.Count;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < pCount; i++)
                    {
                        sb.Append(dsPname.Tables[0].Rows[i]["Name"].ToString() + ", ");

                    }
                    int length = sb.ToString().Length;
                    string pName = sb.ToString().Remove(sb.ToString().LastIndexOf(","));
                    lblParent.Text = pName.ToString();
                }
            }
        }

        protected void grdFeaturedcategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFeaturedcategory.EditIndex = e.NewEditIndex;
            BindFeaturedCategory();
        }

        protected void grdFeaturedcategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFeaturedcategory.EditIndex = -1;
            BindFeaturedCategory();
        }

        protected void grdFeaturedcategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdFeaturedcategory.Rows[e.RowIndex];
            HiddenField hdnCategoryid = (HiddenField)row.FindControl("hdnCategoryid");
            TextBox txtDisplayorder = (TextBox)row.FindControl("txtDisplayorder");
            if (txtDisplayorder.Text != "")
            {
                catcomp.UpdateCategoryDisplayOrder(Convert.ToInt32(hdnCategoryid.Value), Convert.ToInt32(ddlStore.SelectedValue), Convert.ToInt32(txtDisplayorder.Text.Trim()));
            }
            grdFeaturedcategory.EditIndex = -1;
            BindFeaturedCategory();
        }

        #region Bind Featured System

        private void BindFeaturedSystem()
        {
            DataSet dsFeature = postcomp.DisplyPostByOption("IsFeatured" , 20);
            if (dsFeature != null && dsFeature.Tables.Count > 0 && dsFeature.Tables[0].Rows.Count > 0)
                grdFeaturedSystem.DataSource = dsFeature;
            else grdFeaturedSystem.DataSource = null;
            grdFeaturedSystem.DataBind();
        }

        protected void grdFeaturedSystem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFeaturedSystem.EditIndex = -1;
            BindFeaturedSystem();
        }

        protected void grdFeaturedSystem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdFeaturedSystem.Rows[e.RowIndex];
            HiddenField hdnPostid = (HiddenField)row.FindControl("hdnPostid");
            TextBox txtDisplayorder = (TextBox)row.FindControl("txtDisplayorder");
            if (txtDisplayorder.Text != "")
            {
                commondac.ExecuteCommonData("update tb_Post set DisplayOrder ='" + txtDisplayorder.Text.Trim() + "'  where PostId = '" + Convert.ToInt32(hdnPostid.Value) + "'   ");

            }
            grdFeaturedSystem.EditIndex = -1;
            BindFeaturedSystem();
        }

        protected void grdFeaturedSystem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFeaturedSystem.EditIndex = e.NewEditIndex;
            BindFeaturedSystem();
        }

        #endregion

        #region Bind Deal of the Day Post

        private void FillHotDealPost()
        {
            string PostId = "0";
            string HotOfPrice = "0";
            PostId = Convert.ToString(commondac.GetScalarCommonData("SELECT ConfigValue FROM dbo.tb_AppConfig WHERE  ConfigName ='HotdealPost' AND StoreID=" + Convert.ToInt32(ddlStore.SelectedValue)));
            HotOfPrice = Convert.ToString(commondac.GetScalarCommonData("SELECT ConfigValue FROM dbo.tb_AppConfig WHERE  ConfigName ='HotdealPrice' AND StoreID=" + Convert.ToInt32(ddlStore.SelectedValue)));
            txtHotdealprice.Text = HotOfPrice;
            if (PostId != "0")
                ViewState["PostId"] = PostId;
            if (System.IO.File.Exists(Server.MapPath(AppLogic.AppConfigs("ImagePathBanner") + "HotDeal/" + PostId + ".jpg")))
            {
                imgBanner.Src = AppLogic.AppConfigs("ImagePathBanner") + "HotDeal/" + PostId + ".jpg";
                imgBanner.Visible = true;
            }
        }

        /// <summary>
        /// Bind Post
        /// </summary>
        private void bindPost()
        {
            DataSet DsPost = new DataSet();
            DsPost = commondac.GetCommonDataSet("SELECT PostID,SKU,Name +' - '+ SKU AS PostName FROM dbo.tb_Post WHERE isnull(Deleted,0)=0 AND  isnull(Active,0)=1 AND Name is not null AND Name!='' AND StoreID=" + Convert.ToInt32(ddlStore.SelectedValue.ToString()) + " order by Name ASC");
            if (DsPost != null && DsPost.Tables.Count > 0 && DsPost.Tables[0].Rows.Count > 0)
            {
                ddlPost.DataSource = DsPost;
                ddlPost.DataBind();
            }
        }

        /// <summary>
        /// Save button click event
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void imgSave_Click(object sender, EventArgs e)
        {
            String StrConfigValue1 = "0";
            Int32 StoID = Convert.ToInt32(ddlStore.SelectedValue.ToString());
            StrConfigValue1 = ddlPost.SelectedValue.ToString();
            string strConfig = " Update tb_AppConfig  set ConfigValue=" + StrConfigValue1 + " where StoreID=" + StoID + " and ConfigName ='HotdealPost' ";
            commondac.ExecuteCommonData(strConfig);

            Decimal HotPrice = Convert.ToDecimal(txtHotdealprice.Text);
            string strConfigPrice = " Update tb_AppConfig  set ConfigValue=" + HotPrice + " where StoreID=" + StoID + " and ConfigName ='HotDealPrice' ";
            commondac.ExecuteCommonData(strConfigPrice);

            Int32 isupdated = Convert.ToInt32(ddlPost.SelectedValue.ToString());
            if (ViewState["PostId"] != null)
            {
                try
                {
                    File.Delete(Server.MapPath(AppLogic.AppConfigs("ImagePathBanner") + "HotDeal/" + ViewState["PostId"].ToString() + ".jpg"));
                }

                catch { }
            }
            if (!string.IsNullOrEmpty(ID) && Convert.ToString(ID) != "0")
            {

                if (FileUploadBanner.FileName.Length > 0)
                {

                    FileUploadBanner.PostedFile.SaveAs(Server.MapPath(AppLogic.AppConfigs("ImagePathBanner") + "HotDeal/" + isupdated + ".jpg"));

                }
                if (isupdated > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Record Successfully Updated...', 'Message');});", true);
                    FillHotDealPost();
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Record not found.', 'Message');});", true);
                return;
            }
        }
        

        protected void imgCancle_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/Admin/Settings/IndexPageConfig.aspx");
        }
        #endregion

        protected void btnhdnDelete_Click(object sender, EventArgs e)
        {
            int CategoryID = Convert.ToInt32(hdnDelete.Value);
            commondac.ExecuteCommonData("update tb_Category set IsFeatured =0  where CategoryID = '" + CategoryID + "'   ");
            BindFeaturedCategory();
        }

        protected void btnhdnFeaturedPost_Click(object sender, EventArgs e)
        {
            int PostID = Convert.ToInt32(hdnFeaturedPost.Value);
            commondac.ExecuteCommonData("update tb_Post set IsFeatured =0  where PostID = '" + PostID + "'   ");
            BindFeaturedSystem();
        }

        protected void btnhdnBestSeller_Click(object sender, EventArgs e)
        {
            int PostID = Convert.ToInt32(hdnBestSeller.Value);
            commondac.ExecuteCommonData("update tb_Post set IsBestSeller=0  where PostID = '" + PostID + "'   ");
            BindBestSeller();
        }

        protected void btnhdnNewArrival_Click(object sender, EventArgs e)
        {
            int PostID = Convert.ToInt32(hdnNewArrival.Value);
            commondac.ExecuteCommonData("update tb_Post set IsNewArrival =0  where PostID = '" + PostID + "'   ");
            BindNewArrival();
        }

        protected void imgbtnsave_Click(object sender, ImageClickEventArgs e)
        {
            string flag = "false";
            bool f1 = false, f2 = false, f3 = false, f4 = false, f5 = false;
            objAppComp = new ConfigurationComponent();
            if (chkfeaturecat.Checked)
            {

                flag = "true";
            }
            else
            {
                flag = "false";
            }
            Int32 isupdated = objAppComp.UpdateAppConfigvalue("SwitchIndexFeaturedCategory", flag, Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                f1 = true;
            }

            flag = "false";
            if (chkfeaturesys.Checked)
            {

                flag = "true";
            }
            else
            {
                flag = "false";
            }
            isupdated = objAppComp.UpdateAppConfigvalue("SwitchIndexFeaturedSystem", flag, Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                f2 = true;
            }

            flag = "false";
            if (chkbestseller.Checked)
            {

                flag = "true";
            }
            else
            {
                flag = "false";
            }
            isupdated = objAppComp.UpdateAppConfigvalue("SwitchIndexBestseller", flag, Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                f3 = true;
            }

            flag = "false";
            if (chknewarrival.Checked)
            {

                flag = "true";
            }
            else
            {
                flag = "false";
            }
            isupdated = objAppComp.UpdateAppConfigvalue("SwitchIndexNewarrival", flag, Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                f4 = true;
            }

            flag = "false";
            if (chkwelcome.Checked)
            {

                flag = "true";
            }
            else
            {
                flag = "false";
            }
            isupdated = objAppComp.UpdateAppConfigvalue("SwitchIndexWelcomeText", flag, Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                f5 = true;
            }

            if (f1 == f2 == f3 == f4 == f5 == true)
            {
                GetConfigStatus();
            }
        }

        public void GetConfigStatus()
        {
            string flagvalue = "", id = "", divid = "", btnid = imgbtnsave.ClientID.ToString();
            string txtid = "";

            if (AppLogic.AppConfigs("SwitchIndexWelcomeText") != null && AppLogic.AppConfigs("SwitchIndexWelcomeText").ToString() != "")
            {
                if (AppLogic.AppConfigs("SwitchIndexWelcomeText").ToLower().ToString() == "true")
                {
                    flagvalue += "true" + ",";
                }
                else
                {
                    flagvalue += "false" + ",";
                }
                id += chkwelcome.ClientID.ToString() + ",";
                divid += divwelcometext.ClientID.ToString() + ",";
                txtid += txtwelcometext.ClientID.ToString() + ",";
            }

            if (AppLogic.AppConfigs("SwitchIndexFeaturedCategory") != null && AppLogic.AppConfigs("SwitchIndexFeaturedCategory").ToString() != "")
            {
                if (AppLogic.AppConfigs("SwitchIndexFeaturedCategory").ToLower().ToString() == "true")
                {
                    flagvalue += "true" + ",";
                }
                else
                {
                    flagvalue += "false" + ",";
                }
                id += chkfeaturecat.ClientID.ToString() + ",";
                divid += divfeaturecat.ClientID.ToString() + ",";
                txtid += txtfeaturecat.ClientID.ToString() + ",";
            }


            if (AppLogic.AppConfigs("SwitchIndexFeaturedSystem") != null && AppLogic.AppConfigs("SwitchIndexFeaturedSystem").ToString() != "")
            {
                if (AppLogic.AppConfigs("SwitchIndexFeaturedSystem").ToLower().ToString() == "true")
                {
                    flagvalue += "true" + ",";
                }
                else
                {
                    flagvalue += "false" + ",";
                }
                id += chkfeaturesys.ClientID.ToString() + ",";
                divid += divfeaturesys.ClientID.ToString() + ",";
                txtid += txtfeaturesys.ClientID.ToString() + ",";
            }


            if (AppLogic.AppConfigs("SwitchIndexBestseller") != null && AppLogic.AppConfigs("SwitchIndexBestseller").ToString() != "")
            {
                if (AppLogic.AppConfigs("SwitchIndexBestseller").ToLower().ToString() == "true")
                {
                    flagvalue += "true" + ",";
                }
                else
                {
                    flagvalue += "false" + ",";
                }
                id += chkbestseller.ClientID.ToString() + ",";
                divid += divbestseller.ClientID.ToString() + ",";
                txtid += txtbestseller.ClientID.ToString() + ",";
            }

            if (AppLogic.AppConfigs("SwitchIndexNewarrival") != null && AppLogic.AppConfigs("SwitchIndexNewarrival").ToString() != "")
            {
                if (AppLogic.AppConfigs("SwitchIndexNewarrival").ToLower().ToString() == "true")
                {
                    flagvalue += "true" + ",";
                }
                else
                {
                    flagvalue += "false" + ",";
                }
                id += chknewarrival.ClientID.ToString() + ",";
                divid += divnewarrival.ClientID.ToString() + ",";
                txtid += txtnewarrival.ClientID.ToString() + ",";
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "MakeCheckedall('" + flagvalue.TrimEnd(',') + "','" + id.TrimEnd(',') + "');Getstatusall('" + id.TrimEnd(',') + "','" + divid.TrimEnd(',') + "','" + btnid.TrimEnd(',') + "','" + txtid.TrimEnd(',') + "');", true);
        }

        public void GetConfigStatusforMaxPost()
        {
            if (AppLogic.AppConfigs("IndexFeaturedCategoryMaxPost") != null && AppLogic.AppConfigs("IndexFeaturedCategoryMaxPost").ToString() != "")
            {
                txtfeaturecat.Text = AppLogic.AppConfigs("IndexFeaturedCategoryMaxPost").ToString();
            }
            else
            {
                txtfeaturecat.Text = "0";
            }
            if (AppLogic.AppConfigs("IndexFeaturedSystemMaxPost") != null && AppLogic.AppConfigs("IndexFeaturedSystemMaxPost").ToString() != "")
            {
                txtfeaturesys.Text = AppLogic.AppConfigs("IndexFeaturedSystemMaxPost").ToString();
            }
            else
            {
                txtfeaturesys.Text = "0";
            }
            if (AppLogic.AppConfigs("IndexBestsellerMaxPost") != null && AppLogic.AppConfigs("IndexBestsellerMaxPost").ToString() != "")
            {
                txtbestseller.Text = AppLogic.AppConfigs("IndexBestsellerMaxPost").ToString();
            }
            else
            {
                txtbestseller.Text = "0";
            }
            if (AppLogic.AppConfigs("IndexNewarrivalMaxPost") != null && AppLogic.AppConfigs("IndexNewarrivalMaxPost").ToString() != "")
            {
                txtnewarrival.Text = AppLogic.AppConfigs("IndexNewarrivalMaxPost").ToString();
            }
            else
            {
                txtnewarrival.Text = "0";
            }
        }

        protected void imgbtntxtsave_Click(object sender, ImageClickEventArgs e)
        {
            Int32 isupdated = 0;
            bool Success = false;
            if (txtfeaturecat.Text != "")
            {
                isupdated = objAppComp.UpdateAppConfigvalue("IndexFeaturedCategoryMaxPost", txtfeaturecat.Text.Trim().ToString(), Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
                Success = true;
            }

            isupdated = 0;
            if (txtfeaturesys.Text != "")
            {
                isupdated = objAppComp.UpdateAppConfigvalue("IndexFeaturedSystemMaxPost", txtfeaturesys.Text.Trim().ToString(), Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
                Success = true;
            }

            isupdated = 0;
            if (txtbestseller.Text != "")
            {
                isupdated = objAppComp.UpdateAppConfigvalue("IndexBestsellerMaxPost", txtbestseller.Text.Trim().ToString(), Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
                Success = true;
            }

            isupdated = 0;
            if (txtnewarrival.Text != "")
            {
                isupdated = objAppComp.UpdateAppConfigvalue("IndexNewarrivalMaxPost", txtnewarrival.Text.Trim().ToString(), Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
                Success = true;
            }
            GetConfigStatusforMaxPost();

            if (Success == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "$(document).ready( function() {jAlert('Maximum Post Display has been set.', 'Message','');});", true);
                return;
            }
        }

        private void GetHomeContent()
        {
            if (AppLogic.AppConfigs("IndexWelcomeText") != null && AppLogic.AppConfigs("IndexWelcomeText").ToString() != "")
            {
                txtwelcometext.Text = AppLogic.AppConfigs("IndexWelcomeText").ToString();
            }
            else
            {
                txtwelcometext.Text = "0";
            }
        }

        protected void imgwelcomesave_Click(object sender, EventArgs e)
        {
            objAppComp = new ConfigurationComponent();
            Int32 isupdated = objAppComp.UpdateAppConfigvalue("IndexWelcomeText", txtwelcometext.Text.Trim().ToString(), Convert.ToInt32(AppLogic.AppConfigs("StoreID").ToString()));
            if (isupdated > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msgSorry", "$(document).ready( function() {jAlert('Welcome Text has been updated successfully.', 'Message','');});", true);
            }
        }
    }
}