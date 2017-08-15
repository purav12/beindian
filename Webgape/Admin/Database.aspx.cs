using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin
{
    public partial class Database : System.Web.UI.Page
    {
        #region Declaration
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
                        BindTables();
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

        protected void BindTables()
        {
            ddltablenames.Items.Clear();
            DataSet dsPassType = new DataSet();

            dsPassType = commandac.GetCommonDataSet("SELECT ID,TABLENAME FROM TB_DATABASE");
            if (dsPassType != null && dsPassType.Tables.Count > 0 && dsPassType.Tables[0].Rows.Count > 0)
            {
                ddltablenames.DataSource = dsPassType;
                ddltablenames.DataTextField = "TABLENAME";
                ddltablenames.DataValueField = "Id";
            }
            else
            {
                ddltablenames.DataSource = null;
            }
            ddltablenames.DataBind();
            ddltablenames.Items.Insert(0, new ListItem("Select Table Name", "0"));
            ddltablenames.SelectedIndex = 0;
        }

        protected void ddltablenames_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds, ds1 = new DataSet();
            string Description = "";
            Description = ddltablenames.SelectedItem.ToString();

            ds = commandac.GetCommonDataSet("SELECT COLUMN_NAME 'All_Columns' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + ddltablenames.SelectedItem + "' ");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Description += ds.Tables[0].Rows[i]["All_Columns"].ToString() + "  ";
            }
            txtColumnname.Text = Description;

            ds1 = commandac.GetCommonDataSet("SELECT DBName,TableName,imp1,imp2,imp3,LastBackupOn,Active,Important,Comment,DisplayOrder FROM TB_DATABASE WHERE TableName = '" + ddltablenames.SelectedItem + "' ");
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                txtcomment.Text = ds1.Tables[0].Rows[0]["Comment"].ToString();
            }

            txtQuery.Text = "select top 10 * from " + ddltablenames.SelectedItem + " order by 1 desc";
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet Ds = new DataSet();
                DataTable dt = new DataTable();
                Ds = commandac.GetCommonDataSet(txtQuery.Text);
                dt = Ds.Tables[0];
                grdtempgrid.DataSource = dt;
                grdtempgrid.DataBind();
            }
            catch
            {
                Response.Redirect("Database.aspx");
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            Ds = commandac.GetCommonDataSet(txtQuery.Text);
            Export(Ds);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        public void Export(DataSet Ds)
        {
            DataTable dt = new DataTable();
            dt = Ds.Tables[0];
            grdtempgrid.DataSource = dt;
            grdtempgrid.DataBind();
            Response.Clear();
            Response.Buffer = true;
            if (txtname.Text != "")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + txtname.Text + ".xls");
            }
            else
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grdtempgrid.AllowPaging = false;
            grdtempgrid.RenderControl(hw);
            string temp = sw.ToString();
            Response.Output.Write(temp);
            Response.Flush();
            Response.End();
            //try
            //{

            //    for (int i = 0; i < Ds.Tables[0].Columns.Count; i++)
            //    {
            //        wr.Write(Ds.Tables[0].Columns[i].ToString().ToUpper() + "\t");
            //    }

            //    wr.WriteLine();

            //    //write rows to excel file
            //    for (int i = 0; i < (Ds.Tables[0].Rows.Count); i++)
            //    {
            //        for (int j = 0; j < Ds.Tables[0].Columns.Count; j++)
            //        {
            //            if (Ds.Tables[0].Rows[i][j] != null)
            //            {
            //                wr.Write(Convert.ToString(Ds.Tables[0].Rows[i][j]) + "\t");
            //            }
            //            else
            //            {
            //                wr.Write("\t");
            //            }
            //        }
            //        //go to next line
            //        wr.WriteLine();
            //    }
            //    //close file
            //    wr.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }



    }
}