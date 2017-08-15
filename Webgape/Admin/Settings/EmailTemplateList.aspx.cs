using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Settings
{
    public partial class EmailTemplateList : System.Web.UI.Page
    {
        #region Declaration
        public static bool isDescendLabel = false;
        public int Templatecount = 0;
        EmailTemplateComponent EmailComp = new EmailTemplateComponent();        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Email Template inserted successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Email Template updated successfully.', 'Message','');});", true);
                    }
                }
                BindGrid();
            }
        }

        public void BindGrid()
        {
            EmailComp = new EmailTemplateComponent();
            DataSet DsMailTemplate = new DataSet();
            DsMailTemplate = EmailComp.GetEmailTemplateList(ddlSearch.SelectedValue, txtSearch.Text.Trim(), 1); //1 For Gett full List
            Templatecount = DsMailTemplate.Tables[0].Rows.Count;
            ViewState["GridDataTable"] = DsMailTemplate;
            grdEmailTemplate.DataSource = DsMailTemplate;
            grdEmailTemplate.DataBind();
        }

        protected void grdEmailTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                int TemplateID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("EmailTemplate.aspx?EmailTemplateID=" + TemplateID);
            }
        }

        protected void grdEmailTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (isDescendLabel == false)
                {
                    ImageButton btnLabel = (ImageButton)e.Row.FindControl("btnLabel");
                    btnLabel.ImageUrl = "/Admin/Images/order-date.png";
                    btnLabel.AlternateText = "Ascending Order";
                    btnLabel.ToolTip = "Ascending Order";
                    btnLabel.CommandArgument = "DESC";
                }
                else
                {
                    ImageButton btnLabel = (ImageButton)e.Row.FindControl("btnLabel");
                    btnLabel.ImageUrl = "/Admin/Images/order-date-up.png";
                    btnLabel.AlternateText = "Descending Order";
                    btnLabel.ToolTip = "Descending Order";
                    btnLabel.CommandArgument = "ASC";
                }
            }
        }

        protected void Sorting(object sender, EventArgs e)
        {
            ImageButton btnSorting = (ImageButton)sender;
            if (btnSorting != null)
            {
                if (btnSorting.CommandArgument == "ASC")
                {
                    //grdEmailTemplate.Sort(btnSorting.CommandName.ToString(), SortDirection.Ascending);
                    btnSorting.ImageUrl = "/Admin/Images/order-date-up.png";
                    if (btnSorting.ID == "btnLabel")
                    {
                        isDescendLabel = false;
                    }

                    if (ViewState["GridDataTable"] != null)
                    {
                        DataView dv = new DataView();
                        DataSet ds = new DataSet();
                        ds = (DataSet)ViewState["GridDataTable"];
                        dv = ds.Tables[0].DefaultView;
                        dv.Sort = btnSorting.CommandName.ToString() + " ASC";
                        dv.ToTable();

                        grdEmailTemplate.DataSource = dv;
                        grdEmailTemplate.DataBind();
                    }

                    btnSorting.AlternateText = "Descending Order";
                    btnSorting.ToolTip = "Descending Order";
                    btnSorting.CommandArgument = "DESC";
                }
                else if (btnSorting.CommandArgument == "DESC")
                {
                    //grdEmailTemplate.Sort(btnSorting.CommandName.ToString(), SortDirection.Descending);
                    btnSorting.ImageUrl = "/Admin/Images/order-date.png";
                    if (btnSorting.ID == "btnLabel")
                    {
                        isDescendLabel = true;
                    }

                    if (ViewState["GridDataTable"] != null)
                    {
                        DataView dv = new DataView();
                        DataSet ds = new DataSet();
                        ds = (DataSet)ViewState["GridDataTable"];
                        dv = ds.Tables[0].DefaultView;
                        dv.Sort = btnSorting.CommandName.ToString() + " DESC";
                        dv.ToTable();

                        grdEmailTemplate.DataSource = dv;
                        grdEmailTemplate.DataBind();
                    }

                    btnSorting.AlternateText = "Ascending Order";
                    btnSorting.ToolTip = "Ascending Order";
                    btnSorting.CommandArgument = "ASC";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            BindGrid();
        }

        protected void grdEmailTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEmailTemplate.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}