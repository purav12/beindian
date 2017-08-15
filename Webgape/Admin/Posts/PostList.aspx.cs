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
    public partial class postlist : System.Web.UI.Page
    {
        public int Postcount = 0;
        CommonDAC commandac = new CommonDAC();
        PostComponent postcomp = new PostComponent();
        public static bool isDescendPostID = false;
        public static bool isDescendTitle = false;
        public static bool isDescendMaincat = false;
        public static bool isDescendViewcount = false;
        public static bool isDescendCreatedon = false;
        public static bool isDescendStatus = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPostTypeDropDown();
                FillPostGrid();
                Master.HeadTitle("BeIndian - Post List", "BeIndian.in - Post List, Admin Post List", "BeIndian.in - List of Post by Admin");
            }
        }

        private void FillPostTypeDropDown()
        {
            ddlPostType.Items.Clear();
            DataSet dsPostType = new DataSet();

            dsPostType = commandac.GetCommonDataSet("select PostTypeID,Name from tb_postType");
            if (dsPostType != null && dsPostType.Tables.Count > 0 && dsPostType.Tables[0].Rows.Count > 0)
            {
                ddlPostType.DataSource = dsPostType;
                ddlPostType.DataTextField = "Name";
                ddlPostType.DataValueField = "PostTypeID";
            }
            else
            {
                ddlPostType.DataSource = null;
            }
            ddlPostType.DataBind();
            ddlPostType.Items.Insert(0, new ListItem("All Post", "0"));
            ddlPostType.SelectedIndex = 0;
        }


        private void FillPostGrid()
        {
            DataSet dspost = new DataSet();
            if (Session["AdminID"] != null)
            {
                dspost = postcomp.GetPostList(Convert.ToInt32(Session["AdminID"]), ddlPostType.SelectedIndex, ddlCategory.SelectedIndex, ddlSearch.SelectedValue, txtSearch.Text.Trim(), ddlStatus.SelectedValue, 1);
                Postcount = dspost.Tables[0].Rows.Count;
                ViewState["GridDataTable"] = dspost;
                grdPost.DataSource = dspost;
                grdPost.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdPost.PageIndex = 0;
            FillPostGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdPost.PageIndex = 0;
            txtSearch.Text = "";
            ddlCategory.SelectedIndex = 0;
            ddlPostType.SelectedIndex = 0;
            ddlSearch.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            FillPostGrid();
        }

        protected void ddlPostStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPost.PageIndex = 0;
            FillPostGrid();
        }

        protected void ddlpostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPost.PageIndex = 0;
            FillPostGrid();
        }

        protected void Sorting(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn != null)
            {
                if (btn.CommandArgument == "ASC")
                {
                    //grdPost.Sort(btn.CommandName.ToString(), SortDirection.Ascending);
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btn.Text = str;
                    if (btn.ID == "btnPostID")
                    {
                        isDescendPostID = false;
                    }
                    else if (btn.ID == "btnTitle")
                    {
                        isDescendTitle = false;
                    }
                    else if (btn.ID == "btncat")
                    {
                        isDescendMaincat = false;
                    }
                    else if (btn.ID == "btnView")
                    {
                        isDescendViewcount = false;
                    }
                    else if (btn.ID == "btncreatedon")
                    {
                        isDescendCreatedon = false;
                    }
                    else if (btn.ID == "btnstatus")
                    {
                        isDescendStatus = false;
                    }
                    
                    if (ViewState["GridDataTable"] != null)
                    {
                        DataView dv = new DataView();
                        DataSet ds = new DataSet();
                        ds = (DataSet)ViewState["GridDataTable"];
                        dv = ds.Tables[0].DefaultView;
                        dv.Sort = btn.CommandName.ToString() + " ASC";
                        dv.ToTable();

                        grdPost.DataSource = dv;
                        grdPost.DataBind();
                    }



                    btn.ToolTip = "Descending Order";
                    btn.CommandArgument = "DESC";
                }
                else if (btn.CommandArgument == "DESC")
                {
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btn.Text = str;
                    if (btn.ID == "btnPostID")
                    {
                        isDescendPostID = true;
                    }
                    else if (btn.ID == "btnTitle")
                    {
                        isDescendTitle = true;
                    }
                    else if (btn.ID == "btncat")
                    {
                        isDescendMaincat = true;
                    }
                    else if (btn.ID == "btnView")
                    {
                        isDescendViewcount = true;
                    }
                    else if (btn.ID == "btncreatedon")
                    {
                        isDescendCreatedon = true;
                    }
                    else if (btn.ID == "btnstatus")
                    {
                        isDescendStatus = true;
                    }
                    if (ViewState["GridDataTable"] != null)
                    {
                        DataView dv = new DataView();
                        DataSet ds = new DataSet();
                        ds = (DataSet)ViewState["GridDataTable"];
                        dv = ds.Tables[0].DefaultView;
                        dv.Sort = btn.CommandName.ToString() + " DESC";
                        dv.ToTable();

                        grdPost.DataSource = dv;
                        grdPost.DataBind();
                    }



                    btn.ToolTip = "Ascending Order";
                    btn.CommandArgument = "ASC";
                }
            }
        }

        protected void grdPost_Sorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Header
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (isDescendPostID == false)
                {
                    LinkButton btnPostID = (LinkButton)e.Row.FindControl("btnPostID");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btnPostID.Text = str;
                    btnPostID.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btnPostID = (LinkButton)e.Row.FindControl("btnPostID");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btnPostID.Text = str;
                    btnPostID.CommandArgument = "ASC";

                }

                if (isDescendTitle == false)
                {
                    LinkButton btnTitle = (LinkButton)e.Row.FindControl("btnTitle");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btnTitle.Text = str;
                    btnTitle.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btnTitle = (LinkButton)e.Row.FindControl("btnTitle");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btnTitle.Text = str;
                    btnTitle.CommandArgument = "ASC";

                }

                if (isDescendMaincat == false)
                {
                    LinkButton btncat = (LinkButton)e.Row.FindControl("btncat");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btncat.Text = str;
                    btncat.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btncat = (LinkButton)e.Row.FindControl("btncat");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btncat.Text = str;
                    btncat.CommandArgument = "ASC";

                }

                if (isDescendViewcount == false)
                {
                    LinkButton btnView = (LinkButton)e.Row.FindControl("btnView");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btnView.Text = str;
                    btnView.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btnView = (LinkButton)e.Row.FindControl("btnView");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btnView.Text = str;
                    btnView.CommandArgument = "ASC";

                }

                if (isDescendCreatedon == false)
                {
                    LinkButton btncreatedon = (LinkButton)e.Row.FindControl("btncreatedon");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btncreatedon.Text = str;
                    btncreatedon.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btncreatedon = (LinkButton)e.Row.FindControl("btncreatedon");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btncreatedon.Text = str;
                    btncreatedon.CommandArgument = "ASC";

                }


                if (isDescendStatus == false)
                {
                    LinkButton btnstatus = (LinkButton)e.Row.FindControl("btnstatus");
                    string str = "<img src='../assets/icon/order-date.png' />";
                    btnstatus.Text = str;
                    btnstatus.CommandArgument = "DESC";

                }
                else
                {
                    LinkButton btnstatus = (LinkButton)e.Row.FindControl("btnstatus");
                    string str = "<img src='../assets/icon/order-date-up.png' />";
                    btnstatus.Text = str;
                    btnstatus.CommandArgument = "ASC";

                }
            }
            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrtitle = (Literal)e.Row.FindControl("ltrtitle");
                Literal ltrPostId = (Literal)e.Row.FindControl("ltrPostId");
                Label lblType = (Label)e.Row.FindControl("lblType");
                HiddenField HiddenField = (HiddenField)e.Row.FindControl("hdnType");

                ltrtitle.Text = "<a href='/admin/posts/post.aspx?postid=" + ltrPostId.Text.Trim() + "&mode=edit'>" + ltrtitle.Text + "</a>";
                ltrPostId.Text = "<a href='/admin/posts/post.aspx?postid=" + ltrPostId.Text.Trim() + "&mode=edit'>" + ltrPostId.Text + "</a>";
                int PostTypeID = Convert.ToInt32(HiddenField.Value);
                lblType.Text = Convert.ToString(commandac.GetScalarCommonData("select Name from tb_PostType where PostTypeId='" + PostTypeID + "'"));

                Literal ltrStatus = (Literal)e.Row.FindControl("ltrStatus");
                HiddenField hdnActive = (HiddenField)e.Row.FindControl("hdnActive");
                if (hdnActive.Value != "")
                {
                    if (hdnActive.Value.ToString().ToLower() == "true")
                    {
                        ltrStatus.Text = "<span style='width:70%' class=\"label label-success\">Active</span>";
                    }
                    else
                    {
                        ltrStatus.Text = "<span style='width:70%' class=\"label label-warning\">In-Active</span>";
                    }
                }
                else
                {
                    ltrStatus.Text = "<span style='width:70%' class=\"label label-warning\">In-Active</span>";
                }
            }
        }

        protected void grdPost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPost.PageIndex = e.NewPageIndex;
            FillPostGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
    }
}