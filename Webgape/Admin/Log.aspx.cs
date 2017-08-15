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
    public partial class Log : System.Web.UI.Page
    {
        #region Declaration
        ArchiveComponent archvcomp = new ArchiveComponent();
        CommonDAC commandac = new CommonDAC();
        public int Logscount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillLogGrid();
            }
        }


        private void FillLogGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["entity"]))
                {
                    dsmsg = archvcomp.GetArchiveList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), Request.QueryString["entity"].ToString(), Convert.ToInt32(Session["AdminID"]), 1);
                }
                Logscount = dsmsg.Tables[0].Rows.Count;
                grdLog.DataSource = dsmsg;
                grdLog.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdLog.PageIndex = 0;
            FillLogGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdLog.PageIndex = 0;
            FillLogGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdLog.PageIndex = 0;
            txtSearch.Text = "";
            FillLogGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLog.PageIndex = e.NewPageIndex;
            FillLogGrid();
        }

    }
}