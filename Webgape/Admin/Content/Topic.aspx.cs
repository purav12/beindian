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


namespace Webgape.Admin.Content
{
    public partial class Topic : System.Web.UI.Page
    {
        #region Variable declaration
        TopicComponent objTopicComp = null;
        DataSet dstopic = new DataSet();
        CommonDAC commandac = new CommonDAC();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["TopicID"]) && Convert.ToString(Request.QueryString["TopicID"]) != "0")
                {
                    objTopicComp = new TopicComponent();
                    //Display selected Topic detail for edit mode
                    dstopic = commandac.GetCommonDataSet("select * from tb_Topic where TopicID='" + Request.QueryString["TopicID"] + "'");
                    if (dstopic != null && dstopic.Tables.Count > 0 && dstopic.Tables[0].Rows.Count > 0)
                    {
                        lblHeader.Text = "Edit Topic";
                        txtTitle.Text = dstopic.Tables[0].Rows[0]["Title"].ToString();
                        txtTopicName.Text = dstopic.Tables[0].Rows[0]["TopicName"].ToString();
                        ckeditordescription.Text = dstopic.Tables[0].Rows[0]["Description"].ToString();
                        txtSEDescription.Text = dstopic.Tables[0].Rows[0]["SEDescription"].ToString();
                        txtSEKeywords.Text = dstopic.Tables[0].Rows[0]["SEKeywords"].ToString();
                        txtSETitle.Text = dstopic.Tables[0].Rows[0]["SETitle"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dstopic.Tables[0].Rows[0]["ShowOnSiteMap"].ToString()))
                        chkShowOnSiteMap.Checked = Convert.ToBoolean(dstopic.Tables[0].Rows[0]["ShowOnSiteMap"].ToString());
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objTopicComp = new TopicComponent();
            if (!string.IsNullOrEmpty(Request.QueryString["TopicID"]) && Convert.ToString(Request.QueryString["TopicID"]) != "0")
            {
                string sename = CommonOperations.RemoveSpecialCharacter(txtTitle.Text.Trim().ToCharArray());

                bool TopicUpdated = false;
                TopicUpdated = objTopicComp.UpdatePost(txtTitle.Text.Trim(), txtTopicName.Text.Trim(), chkShowOnSiteMap.Checked, ckeditordescription.Text.Trim(), txtSEKeywords.Text.Trim(), txtSEDescription.Text.Trim(), txtSETitle.Text.Trim(), sename, false, Convert.ToInt32(Session["AdminID"].ToString()), Convert.ToInt32(Request.QueryString["TopicID"]));
                if (TopicUpdated)
                {
                    Response.Redirect("topiclist.aspx?status=updated");
                }
            }
            else
            {
                int count = Convert.ToInt32(commandac.GetScalarCommonData("select Count(*) from tb_Topic where Title = '" + txtTitle.Text.Trim() + "'"));
                if (count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Topic Name already exists.', 'Message');});", true);
                    return;
                }

                string sename = CommonOperations.RemoveSpecialCharacter(txtTitle.Text.Trim().ToCharArray());
                Int32 isadded = objTopicComp.InsertPost(txtTitle.Text.Trim(), txtTopicName.Text.Trim(), chkShowOnSiteMap.Checked, ckeditordescription.Text.Trim(), txtSEKeywords.Text.Trim(), txtSEDescription.Text.Trim(), txtSETitle.Text.Trim(), sename, false, Convert.ToInt32(Session["AdminID"].ToString()));
                if (isadded > 0)
                {
                    Response.Redirect("TopicList.aspx?status=inserted");
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TopicList.aspx");
        }



    }
}