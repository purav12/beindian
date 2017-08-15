using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        PostComponent postcomponent = new PostComponent();
        AdminComponent admincomp = new AdminComponent();
        CommonDAC commondac = new CommonDAC();
        DataSet dsPost = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCounts();
                FillPostData();
                Master.HeadTitle("BeIndian - Dashboard", "BeIndian.in, Webgape - Dashboard, Admin Panel", "BeIndian.in Dashboard panel");
            }
        }

        public void FillPostData()
        {
            if (Session["AdminID"] != null)
            {
                dsPost = postcomponent.GetPostByAdminId(Convert.ToInt32(Session["AdminID"]),1);
                if (dsPost != null && dsPost.Tables.Count > 0 && dsPost.Tables[0].Rows.Count > 0)
                {
                    grdtop10post.DataSource = dsPost;
                    grdtop10post.DataBind();
                }
                else
                {
                    grdtop10post.DataSource = null;
                    grdtop10post.DataBind();
                }
            }
        }

        public void FillCounts()
        {
            if (Session["AdminID"] != null)
            {
                DataSet dsCount = new DataSet();
                dsCount = admincomp.GetAdminCountsByAdminId(Convert.ToInt32(Session["AdminID"]));

                ltrtotalpost.Text = dsCount.Tables[0].Rows[0]["TotalCount"].ToString();
                ltractivepost.Text = dsCount.Tables[0].Rows[1]["TotalCount"].ToString();
                ltrpendingpost.Text = dsCount.Tables[0].Rows[2]["TotalCount"].ToString();
                ltrmessage.Text = dsCount.Tables[0].Rows[3]["TotalCount"].ToString();
                ltrnotification.Text = dsCount.Tables[0].Rows[4]["TotalCount"].ToString();
                ltrearnings.Text = dsCount.Tables[0].Rows[5]["TotalCount"].ToString();
                ltrpoints.Text = dsCount.Tables[0].Rows[6]["TotalCount"].ToString();
            }
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

        protected void grdtop10post_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrPost = (Literal)e.Row.FindControl("ltrPost");
                HiddenField hdnActive = (HiddenField)e.Row.FindControl("hdnActive");
                if (hdnActive.Value != "")
                {
                    if (hdnActive.Value.ToString().ToLower() == "true")
                    {
                        ltrPost.Text = "<span style='width:45%' class=\"label label-success\">Active</span>";
                    }
                    else
                    {
                        ltrPost.Text = "<span style='width:60%' class=\"label label-warning\">In-Active</span>";
                    }
                }
                else
                {
                    ltrPost.Text = "<span style='width:45%' class=\"label label-warning\">In-Active</span>";
                }
            }
        }

    }
}