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
    public partial class Earning : System.Web.UI.Page
    {
        public int Earningcount = 0;
        CommonDAC commandac = new CommonDAC();
        EarningComponent earningcomp = new EarningComponent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPointGrid();
                Master.HeadTitle("BeIndian - Earning", "BeIndian.in - Earning, Admin Earning", "BeIndian.in - Earning of Admin");
            }
        }

        private void FillPointGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = earningcomp.GetEarningList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), 1);
                Earningcount = dsmsg.Tables[0].Rows.Count;
                grdEarning.DataSource = dsmsg;
                grdEarning.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdEarning.PageIndex = 0;
            FillPointGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdEarning.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            FillPointGrid();
        }

        protected void grdEarning_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEarning.PageIndex = e.NewPageIndex;
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