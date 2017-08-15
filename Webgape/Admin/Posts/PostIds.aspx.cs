using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Posts
{
    public partial class PostIds : System.Web.UI.Page
    {
        #region Declaration
        CommonDAC commondac = new CommonDAC();
        PostComponent postcomponent = new PostComponent();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindPost();
            }

        }



        private void BindPost()
        {
            DataSet dsPost = null;
            dsPost = postcomponent.GetSimplePostList(txtsearch.Text.Trim(), 2);

            if (dsPost != null && dsPost.Tables.Count > 0 && dsPost.Tables[0].Rows.Count > 0)
            {
                grdFeaturesystem.DataSource = dsPost;
                grdFeaturesystem.DataBind();
            }
            else
            {
                grdFeaturesystem.DataSource = null;
                grdFeaturesystem.DataBind();
            }
        }


        protected void grdFeaturesystem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFeaturesystem.PageIndex = e.NewPageIndex;
            BindPost();
        }


        protected void ibtnFeaturesystemsearch_Click(object sender, EventArgs e)
        {
            if (txtsearch.Text != "")
                BindPost();
        }



        protected void ibtnfeaturesystemshowall_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            BindPost();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        protected void ibtnFeaturesystemaddtoselectionlist_Click(object sender, EventArgs e)
        {
            int TotalRowCount = grdFeaturesystem.Rows.Count;
            CheckBox chkSelect = null;
            HiddenField hdnPostid = null;
            string SelectedIds = string.Empty;
            for (int i = 0; i < TotalRowCount; i++)
            {
                chkSelect = (CheckBox)grdFeaturesystem.Rows[i].FindControl("chkSelect");
                hdnPostid = (HiddenField)grdFeaturesystem.Rows[i].FindControl("hdnPostid");
                if (chkSelect.Checked == true)
                {
                    SelectedIds += hdnPostid.Value+",";
                }
                else if (chkSelect.Checked == false)
                {
                }

            }
            ViewState["SelectedIDs"] = SelectedIds;
            if (grdFeaturesystem.Rows.Count > 0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["clientid"].ToString()))
                    {
                        string ids = ViewState["SelectedIDs"].ToString();
                        if (ids.Length > 1)
                            ids = ids.TrimEnd(",".ToCharArray());
                        Page.ClientScript.RegisterClientScriptBlock(ibtnFeaturesystemaddtoselectionlist.GetType(), "@closemsg", "window.opener.document.getElementById('" + Request.QueryString["clientid"].ToString() + "').value = '" + ids + "';window.close();", true);
                    }
                }
                catch { }
            }
        }
    }
}