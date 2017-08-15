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
    public class AdminRightsComponent
    {

        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        CommonDAC commandac = new CommonDAC();
        #endregion

        public Int32 Insert_Update_PageRightsForAdmin(Int32 MainAdminTypeID, Int32 CompareAdminID, Int32 InnerRightsID, Boolean IsListed, Boolean IsModify, Int32 CreatedBy)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_AdminRights_Insert_Update_PageRightsForAdmin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@MainAdminTypeID", MainAdminTypeID);
            cmd.Parameters.AddWithValue("@CompareAdminID", CompareAdminID);
            cmd.Parameters.AddWithValue("@InnerRightsID", InnerRightsID);
            cmd.Parameters.AddWithValue("@IsListed", IsListed);
            cmd.Parameters.AddWithValue("@IsModify", IsModify);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public Int32 Insert_Update_AdminTypeRights(Int32 AdminTypeID, string Rights)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_AdminRights_Insert_Update_AdminTypeRights";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@AdminTypeID", AdminTypeID);
            cmd.Parameters.AddWithValue("@Rights", Rights);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public DataSet GetAdminPageRightList(Int32 AdminTypeID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_AdminRights_GetAdminRightDetail";
            cmd.Parameters.AddWithValue("@AdminTypeID", AdminTypeID);
            cmd.Parameters.AddWithValue("@Mode", 1);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAdminTypeList()
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_AdminRights_GetAdminRightDetail";
            cmd.Parameters.AddWithValue("@Mode", 2);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAdminRightsList()
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_AdminRights_GetAdminRightDetail";
            cmd.Parameters.AddWithValue("@Mode", 3);
            return objSql.GetDs(cmd);
        }
    }

   
}
