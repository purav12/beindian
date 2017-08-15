using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Settings
{
    public partial class ImageSize : System.Web.UI.Page
    {
        #region Declaration
        ConfigurationComponent cfg = new ConfigurationComponent();
        #endregion

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        public DataSet BindData()
        {
            DataSet dssize = cfg.GetImagesize();
            GetValue(dssize);
            return dssize;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Hashtable ht = new Hashtable();
            if (Session["ht"] != null)
                ht = (Hashtable)Session["ht"];
            if (ht != null)
            {
                UpdateThis(ht, "CategoryIconHeight",  txtCategoryIconHeight.Text);
                UpdateThis(ht, "CategoryIconWidth",  txtCategoryIconWidth.Text);
                UpdateThis(ht, "ProductIconHeight",  txtProductIconHeight.Text);
                UpdateThis(ht, "ProductIconWidth",  txtProductIconwidth.Text);
                UpdateThis(ht, "ProductLargeHeight",  txtProductLargeHeight.Text);
                UpdateThis(ht, "ProductLargeWidth",  txtProductLargeWidth.Text);
                UpdateThis(ht, "ProductMediumHeight",  txtProductMediumHeight.Text);
                UpdateThis(ht, "ProductMediumWidth",  txtProductMediumWidth.Text);
                UpdateThis(ht, "ProductMicroHeight",  txtProductMicroHeight.Text);
                UpdateThis(ht, "ProductMicroWidth",  txtProductMicroWidth.Text);
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Image Size Updated Successfully.', 'Message');});", true);
            }
        }

        private void UpdateThis(Hashtable ht, string Imagename, string Imagesize)
        {
            cfg.UpdateImageSize(Imagename, Imagesize, 2);
        }

        public void GetValue(DataSet Ds)
        {
            int StoreID = 1;
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
            {
                Hashtable ht = new Hashtable();
                for (int cnt = 0; cnt < Ds.Tables[0].Rows.Count; cnt++)
                {
                    ht.Add((string.IsNullOrEmpty(Ds.Tables[0].Rows[cnt]["StoreID"].ToString()) ? "1" : Ds.Tables[0].Rows[cnt]["StoreID"].ToString()) + Ds.Tables[0].Rows[cnt]["ImageName"].ToString(), Ds.Tables[0].Rows[cnt]["ImageSize"].ToString());
                }
                Session["ht"] = ht;
                txtCategoryIconHeight.Text = (ht[StoreID + "CategoryIconHeight"] == null) ? "" : ht[StoreID + "CategoryIconHeight"].ToString();
                txtCategoryIconWidth.Text = (ht[StoreID + "CategoryIconWidth"] == null) ? "" : ht[StoreID + "CategoryIconWidth"].ToString();
                txtProductIconHeight.Text = (ht[StoreID + "ProductIconHeight"] == null) ? "" : ht[StoreID + "ProductIconHeight"].ToString();
                txtProductIconwidth.Text = (ht[StoreID + "ProductIconWidth"] == null) ? "" : ht[StoreID + "ProductIconWidth"].ToString();
                txtProductLargeHeight.Text = (ht[StoreID + "ProductLargeHeight"] == null) ? "" : ht[StoreID + "ProductLargeHeight"].ToString();
                txtProductLargeWidth.Text = (ht[StoreID + "ProductLargeWidth"] == null) ? "" : ht[StoreID + "ProductLargeWidth"].ToString();
                txtProductMediumHeight.Text = (ht[StoreID + "ProductMediumHeight"] == null) ? "" : ht[StoreID + "ProductMediumHeight"].ToString();
                txtProductMediumWidth.Text = (ht[StoreID + "ProductMediumWidth"] == null) ? "" : ht[StoreID + "ProductMediumWidth"].ToString();
                txtProductMicroHeight.Text = (ht[StoreID + "ProductMicroHeight"] == null) ? "" : ht[StoreID + "ProductMicroHeight"].ToString();
                txtProductMicroWidth.Text = (ht[StoreID + "ProductMicroWidth"] == null) ? "" : ht[StoreID + "ProductMicroWidth"].ToString();
            }
        }

        private void ClearData()
        {
            txtCategoryIconHeight.Text = "";
            txtCategoryIconWidth.Text = "";
            txtProductIconwidth.Text = "";
            txtProductIconHeight.Text = "";
            txtProductLargeHeight.Text = "";
            txtProductLargeWidth.Text = "";
            txtProductMediumHeight.Text = "";
            txtProductMediumWidth.Text = "";
            txtProductMicroHeight.Text = "";
            txtProductMicroWidth.Text = "";
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ADMIN/Dashboard.aspx");
        }
    }
}