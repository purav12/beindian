using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Profile
{
    public partial class Point : System.Web.UI.Page
    {
        public int Pointcount = 0;
        CommonDAC commandac = new CommonDAC();
        PointComponent pointcomp = new PointComponent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPointGrid();
                Master.HeadTitle("BeIndian - Points", "BeIndian.in - Points, Admin Points", "BeIndian.in - Points of Admin");
            }
        }

        private void FillPointGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = pointcomp.GetPointList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), 1);
                Pointcount = dsmsg.Tables[0].Rows.Count;
                grdPoint.DataSource = dsmsg;
                grdPoint.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdPoint.PageIndex = 0;
            FillPointGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdPoint.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            FillPointGrid();
        }

        protected void grdPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPoint.PageIndex = e.NewPageIndex;
            FillPointGrid();
        }
        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

    }
}