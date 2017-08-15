using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Posts
{
    public partial class CategoryList : System.Web.UI.Page
    {
        #region Declaration
        #region component
        CategoryComponent catcomp = new CategoryComponent();
        CommonDAC commandac = new CommonDAC();
        public static bool isDescendName = false;
        #endregion

        private string StrFileName
        {
            get
            {
                if (ViewState["FileName"] == null)
                {
                    return "";
                }
                else
                {
                    return (ViewState["FileName"].ToString());
                }
            }
            set
            {
                ViewState["FileName"] = value;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"].ToString().Equals("new"))
                {
                    lblMessage.Text = "Category Inserted Successfully";
                }
                else if (Request.QueryString["mode"].ToString().Equals("edit"))
                {
                    lblMessage.Text = "Category Updated Successfully";
                }
            }
            else
            {
                lblMessage.Text = "";
            }
            if (!IsPostBack)
            {
                CategoryComponent.Filter = "";
                CategoryComponent.NewFilter = false;
                FillCategoryList();
            }
        }

        public void FillCategoryList()
        {
            DataSet dsCategoryList = new DataSet();
            dsCategoryList = catcomp.GetAllCategoriesWithsearch(Convert.ToString(ddlSearch.SelectedValue), txtSearch.Text.Trim(), Convert.ToString(ddlStatus.SelectedValue));


            if (dsCategoryList != null && dsCategoryList.Tables.Count > 0 && dsCategoryList.Tables[0].Rows.Count > 0)
            {
                gvCategory.DataSource = dsCategoryList.Tables[0];
                gvCategory.DataBind();
            }
            else
            {
                gvCategory.DataSource = null;
                gvCategory.DataBind();
            }
        }

        protected void Sorting(object sender, EventArgs e)
        {
            ImageButton lb = (ImageButton)sender;
            if (lb != null)
            {
                if (lb.CommandArgument == "ASC")
                {
                    gvCategory.Sort(lb.CommandName.ToString(), SortDirection.Ascending);
                    lb.ImageUrl = "../assets/icon/order-date-up.png";
                    if (lb.ID == "lbName")
                    {
                        isDescendName = false;
                    }


                    lb.AlternateText = "Descending Order";
                    lb.ToolTip = "Descending Order";
                    lb.CommandArgument = "DESC";
                }
                else if (lb.CommandArgument == "DESC")
                {

                    gvCategory.Sort(lb.CommandName.ToString(), SortDirection.Descending);
                    lb.ImageUrl = "../assets/icon/order-date.png";
                    if (lb.ID == "lbName")
                    {
                        isDescendName = true;
                    }

                    lb.AlternateText = "Ascending Order";
                    lb.ToolTip = "Ascending Order";
                    lb.CommandArgument = "ASC";
                }
            }
        }

        protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrStatus = (Literal)e.Row.FindControl("ltrStatus");
                HiddenField hdnActive = (HiddenField)e.Row.FindControl("hdnActive");
                if (hdnActive.Value != "")
                {
                    if (hdnActive.Value.ToString().ToLower() == "true")
                    {
                        ltrStatus.Text = "<span class=\"label label-success\">Active</span>";
                    }
                    else
                    {
                        ltrStatus.Text = "<span class=\"label label-warning\">In-Active</span>";
                    }
                }
                else
                {
                    ltrStatus.Text = "<span class=\"label label-warning\">In-Active</span>";
                }


            }
            if (gvCategory.Rows.Count > 0)
                trBottom.Visible = true;
            else
                trBottom.Visible = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (isDescendName == false)
                {
                    ImageButton lbName = (ImageButton)e.Row.FindControl("lbName");
                    lbName.ImageUrl = "../assets/icon/order-date.png";
                    lbName.AlternateText = "Ascending Order";
                    lbName.ToolTip = "Ascending Order";
                    lbName.CommandArgument = "DESC";
                }
                else
                {
                    ImageButton lbName = (ImageButton)e.Row.FindControl("lbName");
                    lbName.ImageUrl = "../assets/icon/order-date-up.png";
                    lbName.AlternateText = "Descending Order";
                    lbName.ToolTip = "Descending Order";
                    lbName.CommandArgument = "ASC";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnCatid = (HiddenField)e.Row.FindControl("hdnCatid");
                Label lblCategoryCount = (Label)e.Row.FindControl("lblCategoryCount");
                Label lblPostCount = (Label)e.Row.FindControl("lblPostCount");

                Int32 CategoryID = 0, CategoryCount = 0, Postcount = 0;
                Int32.TryParse(Convert.ToString(hdnCatid.Value), out CategoryID);
                Int32.TryParse(Convert.ToString(commandac.GetScalarCommonData("Select COUNT(*) from tb_Category where ISNULL(Active,0)=1 and ISNULL(Deleted,0)=0 and CategoryID in (Select CategoryID from tb_CategoryMapping where ParentCategoryID=" + CategoryID + ") ")), out CategoryCount);
                Int32.TryParse(Convert.ToString(commandac.GetScalarCommonData("Select COUNT(*) from tb_Post Where ISNULL(Active,0)=1 and ISNULL(Deleted,0)=0 and PostId in (Select PostId from tb_PostCategory where CategoryID=" + CategoryID + ")")), out Postcount);
                lblCategoryCount.Text = Convert.ToString(CategoryCount);
                lblPostCount.Text = Convert.ToString(Postcount);
            }
        }

        public string SetImage(bool _Active)
        {
            string _ReturnUrl;
            if (_Active)
            {
                _ReturnUrl = "../Images/active.gif";

            }
            else
            {
                _ReturnUrl = "../Images/in-active.gif";

            }
            return _ReturnUrl;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            FillCategoryList();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int totalRowCount = gvCategory.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)gvCategory.Rows[i].FindControl("hdnCatid");
                CheckBox chk = (CheckBox)gvCategory.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    DeleteImage(hdn.Value);
                    catcomp.Deletecategory(Convert.ToInt16(hdn.Value));
                    lblMessage.Text = "Category Deleted Successfully";
                }
            }
            FillCategoryList();
        }

        protected void btnDelete1_Click(object sender, EventArgs e)
        {
            int totalRowCount = gvCategory.Rows.Count;
            for (int i = 0; i < totalRowCount; i++)
            {
                HiddenField hdn = (HiddenField)gvCategory.Rows[i].FindControl("hdnCatid");
                CheckBox chk = (CheckBox)gvCategory.Rows[i].FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    DeleteImage(hdn.Value);
                    catcomp.Deletecategory(Convert.ToInt16(hdn.Value));
                    lblMessage.Text = "Category Deleted Successfully";
                }
            }
            FillCategoryList();

        }

        private void DeleteImage(string CategoryId)
        {
            DataSet ds = catcomp.getCatdetailbycatid(Convert.ToInt32(CategoryId));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ImageName"].ToString().Trim() != null && ds.Tables[0].Rows[0]["ImageName"].ToString().Trim() != "")
                {
                    string Path = AppLogic.AppConfigs("ImagePathCategory");
                    string imageName = ds.Tables[0].Rows[0]["ImageName"].ToString().Trim();
                    string bannerName = ds.Tables[0].Rows[0]["BannerImageName"].ToString().Trim();
                    string iconFile = Server.MapPath(Path) + "Icon/" + imageName;
                    string bannerFile = Server.MapPath(Path) + "Banner/" + bannerName;
                    string largeFile = Server.MapPath(Path) + "Large/" + imageName;
                    string medium = Server.MapPath(Path) + "Medium/" + imageName;
                    string micro = Server.MapPath(Path) + "Micro/" + imageName;
                    if (File.Exists(iconFile))
                        File.Delete(iconFile);
                    if (File.Exists(bannerFile))
                        File.Delete(bannerFile);
                    if (File.Exists(largeFile))
                        File.Delete(largeFile);
                    if (File.Exists(medium))
                        File.Delete(medium);
                    if (File.Exists(micro))
                        File.Delete(micro);
                }
            }
        }

        protected void btnGo_Click(object sender, ImageClickEventArgs e)
        {
            FillCategoryList();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            CategoryComponent.Filter = "";
            txtSearch.Text = "";
            ddlStatus.SelectedIndex = 0;
            ddlSearch.SelectedIndex = 0;
            gvCategory.PageIndex = 0;
            FillCategoryList();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategoryList();
        }

        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;
            FillCategoryList();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataView dvCust = new DataView();
            DataSet ds = new DataSet();
            ds = catcomp.GetAllCategoriesWithsearch(Convert.ToString(ddlSearch.SelectedValue), txtSearch.Text.Trim(), "Active");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dvCust = ds.Tables[0].DefaultView;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (dvCust != null)
                {
                    for (int i = 0; i < dvCust.Table.Rows.Count; i++)
                    {
                        object[] args = new object[4];
                        args[0] = Convert.ToString(dvCust.Table.Rows[i]["CategoryID"]);
                        if (!String.IsNullOrEmpty(dvCust.Table.Rows[i]["ParentName"].ToString()))
                        {
                            args[1] = Convert.ToString(dvCust.Table.Rows[i]["ParentName"]).Replace(",", "") + " " + ">" + "  " + Convert.ToString(dvCust.Table.Rows[i]["Name"]).Replace(",", "");
                        }
                        else
                        {
                            args[1] = Convert.ToString(dvCust.Table.Rows[i]["Name"]).Replace(",", "");
                        }

                        args[2] = Convert.ToString(dvCust.Table.Rows[i]["DisplayOrder"]);
                        args[3] = Convert.ToString(dvCust.Table.Rows[i]["ParentCategoryID"]);

                        sb.AppendLine(string.Format("{0},\"{1}\",\"{2}\",\"{3}\"", args));
                    }
                }

                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    DateTime dt = DateTime.Now;
                    String FileName = "CategoryList_" + dt.Month + "-" + dt.Day + "-" + dt.Year + "-" + dt.Hour + "-" + dt.Minute + "-" + dt.Second + ".csv";
                    string FullString = sb.ToString();
                    sb.Remove(0, sb.Length);
                    sb.AppendLine("CategoryID,CategoryName,DisplayOrder,ParentCategoryID");
                    sb.AppendLine(FullString);

                    if (!Directory.Exists(Server.MapPath("~/Admin/Files/")))
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Files/"));

                    String FilePath = Server.MapPath("~/Admin/Files/" + FileName);
                    WriteFile(sb.ToString(), FilePath);
                    Response.ContentType = "text/csv";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.TransmitFile(FilePath);
                    Response.End();
                }
            }
        }

        private void WriteFile(String Text, string FileName)
        {
            StreamWriter writer = null;
            FileInfo info = new FileInfo(FileName);
            writer = info.AppendText();
            writer.Write(Text);

            if (writer != null)
                writer.Close();
        }

        private DataTable LoadCSV(string FileName)
        {
            FileInfo info = new FileInfo(Server.MapPath(AppLogic.AppConfigs("CategoryImportPath") + "CategoryCSV/ImportCSV/") + FileName);
            StreamReader reader = info.OpenText();
            string File = reader.ReadToEnd();
            reader.Close();
            using (CsvReader csv = new CsvReader(new StringReader(File), true))
            {
                int FieldCount = csv.FieldCount;
                string[] FieldNames = csv.GetFieldHeaders();
                DataTable dtCSV = new DataTable();
                DataColumn columnID = new DataColumn();
                columnID.Caption = "Number";
                columnID.ColumnName = "Number";
                columnID.AllowDBNull = false;
                columnID.AutoIncrement = true;
                columnID.AutoIncrementSeed = 1;
                columnID.AutoIncrementStep = 1;
                dtCSV.Columns.Add(columnID);
                foreach (string FieldName in FieldNames)
                    dtCSV.Columns.Add(FieldName);
                while (csv.ReadNextRecord())
                {
                    DataRow dr = dtCSV.NewRow();
                    for (int i = 0; i < FieldCount; i++)
                    {
                        string FieldName = FieldNames[i];
                        if (!dr.Table.Columns.Contains(FieldName))
                        { continue; }

                        dr[FieldName] = csv[i];
                    }
                    dtCSV.Rows.Add(dr);
                }
                dtCSV.AcceptChanges();
                return dtCSV;
            }
        }

        private void DeleteDocument(string StrFileName)
        {
            try
            {
                string docPath = Server.MapPath(AppLogic.AppConfigs("CategoryImportPath") + "CategoryCSV/ImportCSV/") + StrFileName;
                if (File.Exists(Server.MapPath(docPath)))
                {
                    File.Delete(Server.MapPath(docPath));
                }

            }
            catch (Exception ex)
            {

            }

        }

        private bool CheckDisplayorder(DataTable dt)
        {
            int emptyDOrdr = 900;
            int DO = -1;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drr = dt.Select("ParentCategoryID=0");
                String Dorder = ",";
                if (drr.Length > 0)
                {
                    foreach (DataRow dr in drr)
                    {
                        if (String.IsNullOrEmpty(dr["DisplayOrder"].ToString()))
                        {
                            emptyDOrdr = emptyDOrdr + 1;
                            dr["DisplayOrder"] = emptyDOrdr;
                            dt.AcceptChanges();
                            Dorder = Dorder + dr["DisplayOrder"].ToString() + ",";

                        }
                        else if (Dorder.IndexOf("," + dr["DisplayOrder"].ToString() + ",") > -1)
                        {
                            return false;
                        }

                        else
                        {
                            Dorder = Dorder + dr["DisplayOrder"].ToString() + ",";
                        }
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int categoryID = Convert.ToInt32(dt.Rows[i]["CategoryID"].ToString());
                    Dorder = ",";
                    emptyDOrdr = 900;
                    drr = dt.Select("ParentCategoryID=" + categoryID);
                    if (drr.Length > 0)
                    {
                        foreach (DataRow dr in drr)
                        {
                            if (String.IsNullOrEmpty(dr["DisplayOrder"].ToString()))
                            {
                                emptyDOrdr = emptyDOrdr + 1;
                                dr["DisplayOrder"] = emptyDOrdr;
                                dt.AcceptChanges();
                                Dorder = Dorder + dr["DisplayOrder"].ToString() + ",";

                            }
                            else if (Dorder.IndexOf("," + dr["DisplayOrder"].ToString() + ",") > -1)
                            {
                                return false;
                            }

                            else
                            {
                                Dorder = Dorder + dr["DisplayOrder"].ToString() + ",";
                            }
                        }
                    }

                }
            }

            return true;

        }

        private bool InsertDataInDataBase(DataTable dt)
        {
            Boolean result = CheckDisplayorder(dt);
            if (result)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        commandac.ExecuteCommonData("update tb_Category set DisplayOrder=" + dt.Rows[i]["DisplayOrder"].ToString() + " where CategoryID=" + dt.Rows[i]["CategoryID"].ToString() + "");
                    }
                }
                else
                {
                    return false;
                    StrFileName = "";
                }
            }
            else
            {
                lblMessage.Text = "Duplicate DisplayOrder";
                lblMessage.Style.Add("color", "#FF0000");
                lblMessage.Style.Add("font-weight", "normal");
                StrFileName = "";
                return false;
            }
            FillCategoryList();
            return true;
        }

        private void FillMapping(string FileName)
        {

            FileInfo info = new FileInfo(Server.MapPath(AppLogic.AppConfigs("CategoryImportPath") + "CategoryCSV/ImportCSV/") + FileName);
            StreamReader reader = info.OpenText();
            string File = reader.ReadToEnd();
            reader.Close();
            using (CsvReader csv = new CsvReader(new StringReader(File), true))
            {
                int FieldCount = csv.FieldCount;
                string FieldStrike = ",";

                if (FieldCount > 0)
                {
                    string[] FieldNames = csv.GetFieldHeaders();

                    foreach (string FieldName in FieldNames)
                    {
                        string tempFieldName = FieldName.ToLower();
                        if (tempFieldName == "categoryid" || tempFieldName == "categoryname" || tempFieldName == "displayorder" || tempFieldName == "parentcategoryid")
                            FieldStrike += tempFieldName + ",";
                    }
                    if (FieldStrike.ToString().Length > 1)
                    {
                        if (FieldStrike.ToString().ToLower().IndexOf(",categoryid,") > -1 && FieldStrike.ToString().ToLower().IndexOf(",displayorder,") > -1 && FieldStrike.ToString().ToLower().IndexOf(",parentcategoryid,") > -1)
                        {

                        }
                        else
                        {
                            lblMessage.Text = "File Does not contain all columns";
                            lblMessage.Style.Add("color", "#FF0000");
                            lblMessage.Style.Add("font-weight", "normal");
                        }
                    }
                    if (FieldStrike.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > 1)
                    {
                        BindData();
                    }
                    else
                    {
                        lblMessage.Text = "Please Specify CategoryID,CategoryName,DisplayOrder,ParentCategoryID in file.";
                        lblMessage.Style.Add("color", "#FF0000");
                        lblMessage.Style.Add("font-weight", "normal");
                    }
                }
                else
                {
                    lblMessage.Text = "Please Specify CategoryID,CategoryName,DisplayOrder,ParentCategoryID in file.";
                    lblMessage.Style.Add("color", "#FF0000");
                    lblMessage.Style.Add("font-weight", "normal");
                }
                csv.Dispose();
            }
        }

        private void BindData()
        {
            DataTable dtCSV = LoadCSV(StrFileName);
            if (dtCSV.Rows.Count > 0)
            {

            }
            else
                lblMessage.Text = "No data exists in file.";
            lblMessage.Style.Add("color", "#FF0000");
            lblMessage.Style.Add("font-weight", "normal");
        }

    }
}