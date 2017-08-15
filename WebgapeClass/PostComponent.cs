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
    public class PostComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetIndexPagePost(int opt, int CategoryId, int PostTypeId, string searchvalue)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@CatId", CategoryId);
            cmd.Parameters.AddWithValue("@PostType", PostTypeId);
            cmd.Parameters.AddWithValue("@SearchValue", searchvalue);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetPopularPostCount(int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetPopularPostCount(int opt, int rowcount, int nextcount)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@rowcount", rowcount);
            cmd.Parameters.AddWithValue("@nextcount", nextcount);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetRelatedPostCount(int PostId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PostId", PostId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetUserPostCount(int UserId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PostAdminId", UserId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetUserPostForPage(string UserIds, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PageAdminIds", UserIds);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetRelatedPostCount(int PostId, int opt, int rowcount, int nextcount)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PostId", PostId);
            cmd.Parameters.AddWithValue("@rowcount", rowcount);
            cmd.Parameters.AddWithValue("@nextcount", nextcount);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetUserPostCount(int UserId, int opt, int rowcount, int nextcount)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PostAdminId", UserId);
            cmd.Parameters.AddWithValue("@rowcount", rowcount);
            cmd.Parameters.AddWithValue("@nextcount", nextcount);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetUserPostForPage(string UserIds, int opt, int rowcount, string PostIDs, int nextcount)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            cmd.Parameters.AddWithValue("@PostAdminIds", UserIds);
            cmd.Parameters.AddWithValue("@rowcount", rowcount);
            cmd.Parameters.AddWithValue("@nextcount", nextcount);
            cmd.Parameters.AddWithValue("@PostIDs", PostIDs);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetIndexPageScrollPost(int opt, int rowcount, int nextcount, int CatId, int PostType, string PostIDs, string searchvalue)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetIndexPagePost";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@opt", opt);
            cmd.Parameters.AddWithValue("@rowcount", rowcount);
            cmd.Parameters.AddWithValue("@nextcount", nextcount);
            cmd.Parameters.AddWithValue("@CatId", CatId);
            cmd.Parameters.AddWithValue("@PostType", PostType);
            cmd.Parameters.AddWithValue("@PostIDs", PostIDs);
            cmd.Parameters.AddWithValue("@SearchValue", searchvalue);
            return objSql.GetDs(cmd);

            //objSql = new SQLAccess();
            //cmd = new SqlCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "usp_Post_GetIndexPageScrollPost";
            //cmd.Parameters.AddWithValue("@opt", opt);
            //cmd.Parameters.AddWithValue("@PageIndex", PageIndex);
            //cmd.Parameters.AddWithValue("@PageSize", PageSize);
            //cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            //return objSql.GetDsForScrollPost(cmd);
        }

        public DataSet GetPostByAdminId(int AdminId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostByAdminId";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetPostList(int AdminId, int PostTypeId, int CategoryId, string SearchBy, string SearchValue, string status, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@PostTypeId", PostTypeId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetDisplayOrderByPostIDAndCategoryID(Int32 PostID, Int32 CategoryID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostByCategoryId";
            cmd.Parameters.AddWithValue("@PostID", PostID);
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@opt", 3);
            return objSql.GetDs(cmd);
        }

        public DataSet GetSimplePostList(string SearchValue, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostList";
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetPostByPostId(int PostId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostByPostId";
            cmd.Parameters.AddWithValue("@PostId", PostId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetNextPerviousPost(int PostId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostByPostId";
            cmd.Parameters.AddWithValue("@PostId", PostId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertPost(string Title, int SKU, int PostTypeID, int AdminId, string SEName, string SortDescription, string Description, string MainCategory, string CategoryName, string SEDescription, string SETitle, string SEKeywords, string Summary, bool Active, string VideoLink, string RelatedPost)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_InsertPost";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@PostTypeID", PostTypeID);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SEName", SEName);
            cmd.Parameters.AddWithValue("@SortDescription", SortDescription);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@SKU", SKU);
            cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmd.Parameters.AddWithValue("@MainCategory", MainCategory);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@Summary", Summary);
            cmd.Parameters.AddWithValue("@VideoLink", VideoLink);
            cmd.Parameters.AddWithValue("@RelatedPost", RelatedPost);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public bool UpdatePost(string Title, int PostTypeID, int AdminId, string SEName, string SortDescription, string Description, string MainCategory, string CategoryName, string SEDescription, string SETitle, string SEKeywords, string Summary, int PostId, string VideoLink, string RelatedPost)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_InsertPost";
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@PostTypeID", PostTypeID);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SEName", SEName);
            cmd.Parameters.AddWithValue("@SortDescription", SortDescription);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@MainCategory", MainCategory);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@Summary", Summary);
            cmd.Parameters.AddWithValue("@VideoLink", VideoLink);
            cmd.Parameters.AddWithValue("@RelatedPost", RelatedPost);
            cmd.Parameters.AddWithValue("@PostId", PostId);
            cmd.Parameters.AddWithValue("@Mode", 2);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }

        public DataSet DisplyPostByOption(string DisplayOption, Int32 PostCount)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Post_GetPostByFeatured";
            cmd.Parameters.AddWithValue("@DisplayOption", DisplayOption);
            cmd.Parameters.AddWithValue("@Display", PostCount);
            cmd.Parameters.AddWithValue("@opt", 1);
            return objSql.GetDs(cmd);
        }
    }
}
