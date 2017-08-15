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
    public partial class TaskList : System.Web.UI.Page
    {
        #region Declaration
        TaskComponent tasccomp = new TaskComponent();
        CommonDAC commandac = new CommonDAC();
        AdminComponent admincomp = new AdminComponent();
        public int Taskcount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (admincomp.IsSuperAdmin(Convert.ToInt32(Session["AdminID"])) != 0)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                        {
                            String strStatus = Convert.ToString(Request.QueryString["status"]);
                            if (strStatus == "inserted")
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Task added successfully.', 'Message','');});", true);
                            }
                            else if (strStatus == "updated")
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Task updated successfully.', 'Message','');});", true);
                            }
                        }
                        FillTaskGrid();
                    }
                    else
                    {
                        Response.Redirect("/Admin/Dashboard.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }

        }


        private void FillTaskGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = tasccomp.GetTaskList(Convert.ToInt32(Session["AdminID"]), txtSearch.Text.Trim(), Convert.ToString(ddlType.SelectedItem), 1);
                Taskcount = dsmsg.Tables[0].Rows.Count;
                grdTask.DataSource = dsmsg;
                grdTask.DataBind();
            }
        }

        //protected void BindTaskStatus()
        //{
        //    ddlType.Items.Clear();
        //    DataSet dsTaskStatus = new DataSet();

        //    dsTaskStatus = commandac.GetCommonDataSet("select Id,Status from tb_TaskStatus");
        //    if (dsTaskStatus != null && dsTaskStatus.Tables.Count > 0 && dsTaskStatus.Tables[0].Rows.Count > 0)
        //    {
        //        ddlType.DataSource = dsTaskStatus;
        //        ddlType.DataTextField = "Status";
        //        ddlType.DataValueField = "Id";
        //    }
        //    else
        //    {
        //        ddlType.DataSource = null;
        //    }
        //    ddlType.DataBind();
        //    ddlType.Items.Insert(0, new ListItem("Select Task Status", "0"));
        //    ddlType.SelectedIndex = 1;
        //}


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdTask.PageIndex = 0;
            FillTaskGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdTask.PageIndex = 0;
            FillTaskGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdTask.PageIndex = 0;
            txtSearch.Text = "";
            ddlType.SelectedIndex = 1;
            FillTaskGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdTask.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdTask.Rows[i].FindControl("hdnTskid");
                CheckBox chk = (CheckBox)grdTask.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    tasccomp.DeleteTask(Convert.ToInt16(hdn.Value));
                    lblMessage.Text = "Message Deleted Successfully";
                }
            }
            FillTaskGrid();
        }

        protected void grdTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString().Trim().ToLower() == "delete")
            {
                int MessageID = Convert.ToInt32(e.CommandArgument);
                tasccomp.DeleteTask(MessageID);
                lblMessage.Text = "Task Deleted Successfully";
            }
            FillTaskGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTask.PageIndex = e.NewPageIndex;
            FillTaskGrid();
        }
        protected void grdTask_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

    }
}