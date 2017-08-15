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
    public partial class Task : System.Web.UI.Page
    {
        #region Declaration
        TaskComponent tasccomp = new TaskComponent();
        CommonDAC commandac = new CommonDAC();
        AdminComponent admincomp = new AdminComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    if (admincomp.IsSuperAdmin(Convert.ToInt32(Session["AdminID"])) != 0)
                    {
                        txttaskdate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(DateTime.Now));
                        if (Request.QueryString["taskid"] != null && Request.QueryString["taskid"].ToString() != "")
                        {
                            BindTaskDetails(Convert.ToInt32(Request.QueryString["taskid"]));
                        }
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

        protected void BindTaskDetails(int TaskId)
        {
            DataSet dsTask = new DataSet();
            dsTask = tasccomp.GetTaskDetails(TaskId);
            if (dsTask != null && dsTask.Tables.Count > 0 && dsTask.Tables[0].Rows.Count > 0)
            {
                txttaskdate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dsTask.Tables[0].Rows[0]["TaskDate"].ToString()));
                txttask.Text = dsTask.Tables[0].Rows[0]["Task"].ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["taskid"] != null && Request.QueryString["taskid"].ToString() != "")
            {
                bool TaskUpdated = false;

                TaskUpdated = tasccomp.UpdateTask(Convert.ToDateTime(txttaskdate.Text), txttask.Text.Trim(), Convert.ToInt32(Session["AdminID"]), Convert.ToInt32(Request.QueryString["taskid"]));

                if (TaskUpdated)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('Task Updated Successfully.', 'Message');});", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailUpdate", "$(document).ready( function() {jAlert('Soomething went wrong while updating Task try again later.', 'Message');});", true);
                    return;
                }
            }
            else
            {
                int taskadded = 0;
                taskadded = tasccomp.InsertTask(Convert.ToDateTime(txttaskdate.Text), txttask.Text.Trim(), Convert.ToInt32(Session["AdminID"]));
                if (taskadded > 0)
                {
                    txttask.Text = "";
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Success Message", "$(document).ready( function() {jAlert('Task Inserted Successfully.', 'Message');});", true);
                    return;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "FailInsert", "$(document).ready( function() {jAlert('Soomething went wrong while creating Task please try again later.', 'Message');});", true);
                    return;
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaskList.aspx");
        }

    }
}