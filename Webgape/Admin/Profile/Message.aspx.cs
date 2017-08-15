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
    public partial class Message : System.Web.UI.Page
    {
        #region Declaration
        public int Templatecount = 0;
        MessageComponent objMessageComponent = new MessageComponent();
        AdminComponent admincomp = new AdminComponent();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    btndelete.Visible = false;
                    btnSaveMessage.Visible = false;
                    if (!string.IsNullOrEmpty(Request.QueryString["MessageID"]) && Convert.ToString(Request.QueryString["MessageID"]) != "0")
                    {
                        objMessageComponent = new MessageComponent();
                        DataSet DsMessage = new DataSet();
                        DsMessage = objMessageComponent.GetMessageByMessageId(Convert.ToInt32(Request.QueryString["MessageID"]), 1);
                        if (DsMessage != null && DsMessage.Tables.Count > 0 && DsMessage.Tables[0].Rows.Count > 0)
                        {
                            txtfrom.Text = DsMessage.Tables[0].Rows[0]["MessageFrom"].ToString();
                            txtto.Text = DsMessage.Tables[0].Rows[0]["MessageTo"].ToString();
                            txtmessage.Text = DsMessage.Tables[0].Rows[0]["Message"].ToString();
                            hdnfromid.Value = DsMessage.Tables[0].Rows[0]["FromId"].ToString();
                            hdntoid.Value = DsMessage.Tables[0].Rows[0]["ToId"].ToString();

                            if (Session["AdminID"] != null)
                            {
                                if (Convert.ToInt32(Session["AdminID"]) == Convert.ToInt32(hdnfromid.Value))
                                {
                                    btndelete.Visible = true;
                                    btnSaveMessage.Visible = false;
                                }
                            }
                            lblHeader.Text = "Edit Message";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ToId"]) && Convert.ToString(Request.QueryString["ToId"]) != "0")
                {
                    DataSet DsMessage = new DataSet();
                    admincomp = new AdminComponent();
                    DsMessage = admincomp.GetAdminProfileByAdminId(Convert.ToInt32(Request.QueryString["ToId"]));
                    txtfrom.Text = Session["AdminName"].ToString();
                    txtto.Text = DsMessage.Tables[0].Rows[0]["FirstName"].ToString() + " " + DsMessage.Tables[0].Rows[0]["LastName"].ToString();
                    hdnfromid.Value = Session["AdminID"].ToString();
                    hdntoid.Value = DsMessage.Tables[0].Rows[0]["AdminID"].ToString();
                    btnSaveMessage.Visible = true;
                }
                if (hdntoid.Value == hdnfromid.Value)
                {

                }
            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            int MessageID = 0;
            bool MessageUpdated = false;
            objMessageComponent = new MessageComponent();

            if (txtmessage.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please Enter Message.', 'Message','');});", true);
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["MessageID"]) && Convert.ToString(Request.QueryString["MessageID"]) != "0")
            {
                MessageUpdated = objMessageComponent.UpdateMessage(Convert.ToInt32(Request.QueryString["MessageID"]), txtmessage.Text.Trim(), 2);
                if (MessageUpdated)
                    Response.Redirect("MessageList.aspx?status=updated");
            }
            else
            {
                if (Session["AdminID"] != null)
                {
                    MessageID = objMessageComponent.InsertMessage(Convert.ToInt32(Session["AdminID"]), Convert.ToInt32(hdntoid.Value), txtmessage.Text.Trim(), 1);
                }
                if (MessageID > 0)
                    Response.Redirect("MessageList.aspx?status=inserted");
            }
        }

        protected void btnCancelMessage_Click(object sender, EventArgs e)
        {
            Response.Redirect("MessageList.aspx");
        }

        protected void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["MessageID"]) && Convert.ToString(Request.QueryString["MessageID"]) != "0")
            {
                objMessageComponent.DeleteMessage(Convert.ToInt32(Request.QueryString["MessageID"]));
            }
            Response.Redirect("MessageList.aspx?status=deleted");
        }
    }
}