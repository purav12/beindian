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
    public partial class MessageList : System.Web.UI.Page
    {
        public int Messagecount = 0;
        CommonDAC commandac = new CommonDAC();
        MessageComponent msgcomp = new MessageComponent();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    String strStatus = Convert.ToString(Request.QueryString["status"]);
                    if (strStatus == "inserted")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Message sent successfully.', 'Message','');});", true);

                    }
                    else if (strStatus == "updated")
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Message updated successfully.', 'Message','');});", true);

                    }
                }
                FillMsgGrid();
                Master.HeadTitle("BeIndian - Message", "BeIndian.in - Message, Admin MessageList", "BeIndian.in - Message of Admin");
            }
        }

        private void FillMsgGrid()
        {
            DataSet dsmsg = new DataSet();
            if (Session["AdminID"] != null)
            {
                dsmsg = msgcomp.GetMessageList(Convert.ToInt32(Session["AdminID"]), ddlSearch.SelectedValue, txtSearch.Text.Trim(), ddlType.SelectedValue, 1);
                Messagecount = dsmsg.Tables[0].Rows.Count;
                grdMessage.DataSource = dsmsg;
                grdMessage.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMessage.PageIndex = 0;
            FillMsgGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdMessage.PageIndex = 0;
            FillMsgGrid();
        }

        protected void btnShowall_Click(object sender, EventArgs e)
        {
            grdMessage.PageIndex = 0;
            txtSearch.Text = "";
            ddlSearch.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            FillMsgGrid();
        }

        protected void grdMessage_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrfrom = (Literal)e.Row.FindControl("ltrfrom");
                Literal ltrto = (Literal)e.Row.FindControl("ltrto");

                HiddenField hdnFrmid = (HiddenField)e.Row.FindControl("hdnFrmid");
                HiddenField hdnToid = (HiddenField)e.Row.FindControl("hdnToid");

                ltrfrom.Text = "<a href='/User.aspx?UID=" + hdnFrmid.Value + "'>" + ltrfrom.Text + "</a>";
                ltrto.Text = "<a href='/User.aspx?UID=" + hdnToid.Value + "'>" + ltrto.Text + "</a>";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = grdMessage.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)grdMessage.Rows[i].FindControl("hdnMsgid");
                CheckBox chk = (CheckBox)grdMessage.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    msgcomp.DeleteMessage(Convert.ToInt16(hdn.Value));
                    lblMessage.Text = "Message Deleted Successfully";
                }
            }
            FillMsgGrid();
        }

        protected void grdMessage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString().Trim().ToLower() == "delete")
            {
                int MessageID = Convert.ToInt32(e.CommandArgument);
                msgcomp.DeleteMessage(MessageID);
                lblMessage.Text = "Message Deleted Successfully";
            }
            FillMsgGrid();
        }

        public String SetName(String Name)
        {
            if (Name.Length > 70)
                Name = Name.Substring(0, 67) + "...";
            return Server.HtmlEncode(Name);
        }
        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMessage.PageIndex = e.NewPageIndex;
            FillMsgGrid();
        }
        protected void grdMessage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}