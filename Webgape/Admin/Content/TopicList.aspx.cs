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
    public partial class TopicList : System.Web.UI.Page
    {
        #region Variable declaration
        CommonDAC commandac = new CommonDAC();
        TopicComponent topiccomp = new TopicComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Topic inserted successfully.', 'Message');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Topic updated successfully.', 'Message');});", true);
                    }
                }
            }
        }

        public void BindGrid()
        {
            DataSet DsTopic = new DataSet();
            DsTopic = topiccomp.GetAllTopicList(txtSearch.Text.Trim());
            if (DsTopic != null && DsTopic.Tables.Count > 0 && DsTopic.Tables[0].Rows.Count > 0)
            {
                grdTopic.DataSource = DsTopic;
                grdTopic.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdTopic.PageIndex = 0;
            BindGrid();
            if (grdTopic.Rows.Count == 0)
                trBottom.Visible = false;
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            grdTopic.PageIndex = 0;
            BindGrid();
        }

        protected void btnDeleteTopic_Click(object sender, EventArgs e)
        {
            TopicComponent objTopicComp = new TopicComponent();
            int totalRowCount = grdTopic.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdTopic.Rows[i].FindControl("hdnTopicID");
                CheckBox chk = (CheckBox)grdTopic.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    commandac.ExecuteCommonData("update tb_Topic set Deleted=1 where TopicID='" + Convert.ToInt16(hdn.Value) + "'");
                }
            }
            grdTopic.DataBind();
            if (grdTopic.Rows.Count == 0)
                trBottom.Visible = false;
        }

        protected void grdTopic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int TopicID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("Topic.aspx?TopicID=" + TopicID); //Edit Topic
            }
            else if (e.CommandName == "ShowOnSiteMap")
            {
                string status = e.CommandArgument.ToString();
                GridViewRow grrow = (GridViewRow)(((System.Web.UI.Control)e.CommandSource).NamingContainer);
                HiddenField hdnTopicID = (HiddenField)grrow.FindControl("hdnTopicID");
                HiddenField hdnSiteMap = (HiddenField)grrow.FindControl("hdnSiteMap");


                if (hdnSiteMap.Value.ToString().ToLower() == "true" || hdnSiteMap.Value.ToString().ToLower() == "1")
                {
                    commandac.ExecuteCommonData("UPDATE TB_TOPIC SET ShowOnSiteMap=0 WHERE TopicID='" + hdnTopicID.Value.ToString() + "'");
                    grdTopic.DataBind();
                }
                else
                {
                    commandac.ExecuteCommonData("UPDATE TB_TOPIC SET ShowOnSiteMap=1 WHERE TopicID='" + hdnTopicID.Value.ToString() + "'");
                    grdTopic.DataBind();
                }
            }
        }

        /// <summary>
        /// Set dynamic images for edit button and sorting 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTopic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grdTopic.Rows.Count > 0)
            {
                trBottom.Visible = true;
            }
            else
            {
                trBottom.Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrStatus = (Literal)e.Row.FindControl("ltrStatus");
                HiddenField hdnSiteMap = (HiddenField)e.Row.FindControl("hdnSiteMap");
               
                if (hdnSiteMap.Value != "")
                {
                    if (hdnSiteMap.Value.ToString().ToLower() == "true")
                    {
                        ltrStatus.Text = "<span class=\"label label-success\">Active</span>";
                    }
                    else
                    {
                        ltrStatus.Text = "<span class=\"label label-warning\">In-Active</span>";
                    }
                }
                else
                {
                    ltrStatus.Text = "<span class=\"label label-warning\">In-Active</span>";
                }
            }           
        }
    }
}

