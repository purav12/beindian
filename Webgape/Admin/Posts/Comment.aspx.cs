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
    public partial class Comment : System.Web.UI.Page
    {
        #region Declaration
        public int Templatecount = 0;
        CommentComponent objCommentComponent = new CommentComponent();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btndelete.Visible = false;
                btnSaveComment.Visible = false;
                if (!string.IsNullOrEmpty(Request.QueryString["CommentID"]) && Convert.ToString(Request.QueryString["CommentID"]) != "0")
                {
                    objCommentComponent = new CommentComponent();
                    DataSet DsComment = new DataSet();
                    DsComment = objCommentComponent.GetCommentByCommentId(Convert.ToInt32(Request.QueryString["CommentID"]), 1);
                    if (DsComment != null && DsComment.Tables.Count > 0 && DsComment.Tables[0].Rows.Count > 0)
                    {

                        txtentityname.Text = DsComment.Tables[0].Rows[0]["EntityName"].ToString();
                        txtidentifier.Text = DsComment.Tables[0].Rows[0]["EntityIdentifier"].ToString();
                        txtcomment.Text = DsComment.Tables[0].Rows[0]["Comments"].ToString();
                        txtcommentby.Text = DsComment.Tables[0].Rows[0]["AdminName"].ToString();
                        txtcommentedon.Text = DsComment.Tables[0].Rows[0]["CreatedOn"].ToString();
                        txtstate.Text = "HelpFull :" + DsComment.Tables[0].Rows[0]["HelpFullCount"].ToString() + " Spam :" + DsComment.Tables[0].Rows[0]["SpamCount"].ToString();
                        txtdelreason.Text = DsComment.Tables[0].Rows[0]["DeleteReason"].ToString();

                        if (Session["AdminID"] != null)
                        {
                            if (Convert.ToInt32(Session["AdminID"]) == Convert.ToInt32(DsComment.Tables[0].Rows[0]["AdminID"]))
                            {
                                btndelete.Visible = true;
                                btnSaveComment.Visible = true;
                            }
                            else if (Convert.ToInt32(Session["AdminID"]) == Convert.ToInt32(DsComment.Tables[0].Rows[0]["OwnerId"]))
                            {
                                btndelete.Visible = true;
                            }
                        }
                        lblHeader.Text = "Edit Comment";
                    }
                }
            }
        }

        protected void btnSaveComment_Click(object sender, EventArgs e)
        {
            int CommentID = 0;
            objCommentComponent = new CommentComponent();

            if (txtcomment.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please enter Comment.', 'Comment','');});", true);
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CommentID"]) && Convert.ToString(Request.QueryString["CommentID"]) != "0")
            {
                CommentID = objCommentComponent.UpdateComment(txtcomment.Text.Trim(), Convert.ToInt32(Request.QueryString["CommentID"]), 2);
                if (CommentID > 0)
                    Response.Redirect("CommentList.aspx?status=updated");
            }
            else
            {
             
            }
        }

        protected void btnCancelComment_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommentList.aspx");
        }

        protected void btnDeleteComment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["CommentID"]) && Convert.ToString(Request.QueryString["CommentID"]) != "0")
            {
                objCommentComponent.DeleteComment(Convert.ToInt32(Request.QueryString["CommentID"]), txtdelreason.Text.Trim());
            }
            Response.Redirect("CommentList.aspx?status=deleted");
        }
    }
}