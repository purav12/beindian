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
    public partial class CommentList : System.Web.UI.Page
    {
        public int Commentcount = 0;
        CommonDAC commandac = new CommonDAC();
        CommentComponent commencomp = new CommentComponent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Comment added successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "deleted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Comment deleted successfully.', 'Message','');});", true);

                    }
                }
                FillCommentGrid();
                Master.HeadTitle("BeIndian - Comment List", "BeIndian.in - Comment List, Admin Comment List", "BeIndian.in - Comment List by Admin");
            }
        }

        private void FillCommentGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = commencomp.GetCommponentList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), ddlType.SelectedValue, ddlEntityType.SelectedValue, 1);
                Commentcount = dsmsg.Tables[0].Rows.Count;
                grdComment.DataSource = dsmsg;
                grdComment.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdComment.PageIndex = 0;
            FillCommentGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdComment.PageIndex = 0;
            FillCommentGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdComment.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            FillCommentGrid();
        }

        protected void grdComment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdComment.PageIndex = e.NewPageIndex;
            FillCommentGrid();
        }
        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

    }
}