using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Contact : System.Web.UI.Page
    {
        CommonDAC Commdac = new CommonDAC();
        ContactComponent ContComp = new ContactComponent();
        VisitorComponent vistcomp = new VisitorComponent();
        public string YoutubeLink = string.Empty;
        public string TwitterLink = string.Empty;
        public string FacebookLink = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vistcomp.InsertPageVisitor("Contact", 0);
                FillDropdown();
                BindLinks();
            }
        }

        private void BindLinks()
        {
            YoutubeLink = AppLogic.AppConfigs("YoutubeLink").ToString();
            TwitterLink = AppLogic.AppConfigs("TwitterLink").ToString();
            FacebookLink = AppLogic.AppConfigs("FacebookLink").ToString();
        }

        public void FillDropdown()
        {
            DataSet DSDdl = new DataSet();
            DSDdl = Commdac.GetContactUsDropdownData("ContactUs");
            ddlproblem.DataSource = DSDdl;

            if (DSDdl != null && DSDdl.Tables.Count > 0 && DSDdl.Tables[0].Rows.Count > 0)
            {
                ddlproblem.DataSource = DSDdl.Tables[0];
                ddlproblem.DataTextField = "Item";
                ddlproblem.DataValueField = "Item";
                ddlproblem.DataBind();
                ddlproblem.Items.Insert(0, new ListItem("Select Problem", "0"));
            }
            else
            {
                ddlproblem.DataSource = null;
                ddlproblem.DataBind();
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            int VisitorId = 0;
            int ContactID = 0;
            if (Session["UserRecorded"] == null)
            {
                VisitorId = Convert.ToInt32(Session["UserRecorded"]);
            }
            ContactID = ContComp.InsertContact(txtname.Text.Trim(), txtemail.Text.Trim(), ddlproblem.SelectedValue, txtmessage.Text.Trim(), VisitorId);
            if (ContactID > 0)
            {
                txtmessage.Text = "";
                lblstatus.Text = "Form submitted successfully.";
                dvstatus.Visible = true;
            }

        }
    }
}