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
    public class CommentComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetCommentByPostId(int EntityId, string EntityName, int PageIndex, int PageSize, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_GetCommentByPostId";
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@PageIndex", PageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetChildCommentByPostId(int CommentId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_GetCommentByPostId";
            cmd.Parameters.AddWithValue("@CommentId", CommentId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }


        public DataSet GetCommponentList(int AdminId, string SearchBy, string SearchValue, string status, string EntityType, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_GetCommentList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@EntityType", EntityType);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }


        public DataSet GetCommentByCommentId(int CommentId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_GetCommentByCommentId";
            cmd.Parameters.AddWithValue("@CommentId", CommentId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public Int32 UpdateComment(string Comment, int CommentId, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_InsertComment";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Comment", Comment);
            cmd.Parameters.AddWithValue("@CommentId", CommentId);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }


        public Int32 InsertComment(int OwnerId, int EntityId, string EntityName,string @Name, string EmailId, int AdminId, int ParentId, string Comment)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_InsertComment";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@OwnerId", OwnerId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@Name", Name);            
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@ParentId", ParentId);
            cmd.Parameters.AddWithValue("@Comment", Comment);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public void DeleteComment(Int32 CommentID, string DeleteReason)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Comment_InsertComment";
            cmd.Parameters.AddWithValue("@CommentID", CommentID);
            cmd.Parameters.AddWithValue("@DeleteReason", DeleteReason);
            cmd.Parameters.AddWithValue("@Mode", 3);
            objSql.GetDs(cmd);
        }

    }
}
