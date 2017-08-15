using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Xml;

namespace WebgapeClass
{
  
    public class CategoryComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        CommonDAC commandac = new CommonDAC();
        #endregion

        #region Properties
        private static bool _newFilter = false;
        private static string _Filter = "";
        private static int _CategoryID = 0;

        public static bool NewFilter
        {
            get { return _newFilter; }
            set { _newFilter = value; }
        }

        public static string Filter
        {
            get { return _Filter; }
            set { _Filter = value; }
        }

        public static int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        #endregion

        public DataSet GetAllCategoriesWithsearch(String FieldName, String FieldValue, String Status)
        {

            String strCondition = "";
            bool IsActive = true;
           
            if (FieldName == "ParentCatName" && FieldValue != "")
            {
                strCondition += " AND t1.ParentName LIKE '%" + FieldValue + "%'";
            }
            else if (FieldName == "Name" && FieldValue != "")
            {
                strCondition += " AND t1.Name LIKE '%" + FieldValue + "%'";
            }
            if (Status == "InActive")
            {
                IsActive = false;
            }
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllCategoryWithSearch";
            cmd.Parameters.AddWithValue("@Condition", strCondition);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            return objSql.GetDs(cmd);
        }

        public void Deletecategory(Int32 categoryId)
        {
            int query = Convert.ToInt32(commandac.GetScalarCommonData("select Count(*) from tb_CategoryMapping where CategoryID='" + categoryId + "'"));
            if (query >= 1)
            {
                int CategoryMappingId = Convert.ToInt32(commandac.GetScalarCommonData("select top 1 CategoryMappingId from tb_CategoryMapping where ParentCategoryID='" + categoryId + "' OR CategoryId='" + categoryId + "'"));
                commandac.ExecuteCommonData("delete from tb_CategoryMapping where CategoryMappingId='" + CategoryMappingId + "'");
            }
            commandac.ExecuteCommonData("Delete from tb_Category where CategoryID='" + categoryId + "'");
        }

        public DataSet getCatdetailbycatid(Int32 categoryId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
            cmd.Parameters.AddWithValue("@mode", 5);
            return objSql.GetDs(cmd);
        }

        public DataSet GetChildCatdetailbycatid(Int32 categoryId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
            cmd.Parameters.AddWithValue("@mode", 12);
            return objSql.GetDs(cmd);
        }

        public DataSet getHeaderCategory()
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@mode", 11);
            return objSql.GetDs(cmd);
        }

        public DataSet SearchCategory(String CategoryName, Int32 Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandText = "usp_Category_CategoryTreeList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CatName", CategoryName);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

        public DataSet ExpandedCategory(Int32 CategoryID, Int32 Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandText = "usp_Category_ExpandSelectedCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

        public DataSet getCategorydetails(string categoryName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@Name", categoryName);
            cmd.Parameters.AddWithValue("@mode", 2);
            return objSql.GetDs(cmd);

        }

        public DataSet GetCategoryDetailBycategoryIdWithParentCategoryID(Int32 CategoryID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@mode", 8);
            return objSql.GetDs(cmd);
        }

        public DataSet getCategorydetailsbyPost(Int32 PostId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@PostID", PostId);
            cmd.Parameters.AddWithValue("@mode", 9);
            return objSql.GetDs(cmd);
        }

        public DataSet GetCategory(Int32 Option)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetCategory";
            cmd.Parameters.AddWithValue("@opt", Option);
            return objSql.GetDs(cmd);
        }

        public void DeleteCategoryforPost(Int32 PostId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandText = "usp_DeleteCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", PostId);
            objSql.ExecuteNonQuery(cmd);
        }

        public void Update(Int32 categoryId, Int32 parentCategoryId, Int32 CategoryMappingID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_CategoryMapping_Update";
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);
            cmd.Parameters.AddWithValue("@ParentCategoryID", parentCategoryId);
            cmd.Parameters.AddWithValue("@CategoryMappingID", CategoryMappingID);
            cmd.Parameters.AddWithValue("@opt", 1);
            objSql.ExecuteNonQuery(cmd);
        }

        public bool updateCategory(int CategoryID, string Name,string sename,string SEDescription,string SEKeywords,int CreatedBy,DateTime CreatedOn,string SETitle,string Summary,string Description,bool Active,int UpdatedBy,DateTime UpdatedOn,string ImageName,string BannerImageName,int DisplayOrder,bool IsFeatured,bool ShowOnHeader,bool Deleted,string HeaderText,string ShortName,int @mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category";
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@sename", sename);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@Summary", Summary);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
            cmd.Parameters.AddWithValue("@UpdatedOn", UpdatedOn);
            cmd.Parameters.AddWithValue("@ImageName", ImageName);
            cmd.Parameters.AddWithValue("@BannerImageName", BannerImageName);
            cmd.Parameters.AddWithValue("@DisplayOrder", DisplayOrder);
            cmd.Parameters.AddWithValue("@IsFeatured", IsFeatured);
            cmd.Parameters.AddWithValue("@ShowOnHeader", ShowOnHeader);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Parameters.AddWithValue("@HeaderText", HeaderText);
            cmd.Parameters.AddWithValue("@ShortName", ShortName);
            cmd.Parameters.AddWithValue("@Mode", @mode);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }

        public Int32 insertcategory(string Name, string sename, string SEDescription, string SEKeywords, int CreatedBy, DateTime CreatedOn, string SETitle, string Summary, string Description, bool Active, string BannerImageName, int DisplayOrder, bool IsFeatured, bool ShowOnHeader, bool Deleted, string HeaderText, string ShortName, int @mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@sename", sename);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@Summary", Summary);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@BannerImageName", BannerImageName);
            cmd.Parameters.AddWithValue("@DisplayOrder", DisplayOrder);
            cmd.Parameters.AddWithValue("@IsFeatured", IsFeatured);
            cmd.Parameters.AddWithValue("@ShowOnHeader", ShowOnHeader);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Parameters.AddWithValue("@HeaderText", HeaderText);
            cmd.Parameters.AddWithValue("@ShortName", ShortName);
            cmd.Parameters.AddWithValue("@Mode", @mode);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public void delete(Int32 CategoryMappingID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_CategoryMapping_Update";
            cmd.Parameters.AddWithValue("@CategoryMappingID", CategoryMappingID);
            cmd.Parameters.AddWithValue("@opt", 2);
            objSql.ExecuteNonQuery(cmd);

        }

        public DataSet GetFeaturedCategory(Int32 StoreID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_FeaturedCategory";
            cmd.Parameters.AddWithValue("@StoreID", StoreID);
            cmd.Parameters.AddWithValue("@Opt", 1);
            return objSql.GetDs(cmd);
        }

        public DataSet GetParentCategoryNamebyCategoryID(Int32 CategoryID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category_GetAllDetail";
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@mode", 7);
            return objSql.GetDs(cmd);

        }

        public void UpdateCategoryDisplayOrder(Int32 CategoryId, Int32 StoreId, Int32 DisplayOrder)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandText = "usp_Category_FeaturedCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StoreID", StoreId);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryId);
            cmd.Parameters.AddWithValue("@DisplayOrder", DisplayOrder);
            cmd.Parameters.AddWithValue("@opt", 4);
            objSql.ExecuteNonQuery(cmd);
        }

        public Int32 GetCategoryId(string SEName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Category";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@sename", SEName);
            cmd.Parameters.AddWithValue("@Mode", 3);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

    }
}
