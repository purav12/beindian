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
    public partial class NotificationList : System.Web.UI.Page
    {
        public int Notificationcount = 0;
        CommonDAC commandac = new CommonDAC();
        NotificationComponent notcomp = new NotificationComponent();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillNotGrid();
                Master.HeadTitle("BeIndian - Notification", "BeIndian.in - Notification, Admin NotificationList", "BeIndian.in - Notification of Admin");
            }
        }

        private void FillNotGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = notcomp.GetNotificationList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), 1);
                Notificationcount = dsmsg.Tables[0].Rows.Count;
                grdNotification.DataSource = dsmsg;
                grdNotification.DataBind();
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdNotification.PageIndex = 0;
            FillNotGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdNotification.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;           
            FillNotGrid();
        }

        protected void grdNotification_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdNotification.PageIndex = e.NewPageIndex;
            FillNotGrid();
        }
        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }

    }
}