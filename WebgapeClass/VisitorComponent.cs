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
    public class VisitorComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public Int32 InsertMasterVisitor(string Ip, string Country, string City, string Browser, bool IsAdmin)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Visitor";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Ip", Ip);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@Browser", Browser);
            cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public Int32 InsertSubVisitor(string EntityName, int EntityId, int VisitorId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Visitor";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.Parameters.AddWithValue("@Mode", 2);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public DataSet Addvisitor(string Link)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Link", Link);
            cmd.CommandText = "usp_Visitor";
            cmd.Parameters.AddWithValue("@Mode", 3);
            return objSql.GetDs(cmd);
        }

        public DataSet GetVisitorDetails(int VisitorId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Visitor";
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.Parameters.AddWithValue("@Mode", 7);
            return objSql.GetDs(cmd);
        }

        public void AddCookieVisitor(int VisitorId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Visitor";
            cmd.Parameters.AddWithValue("@VisitorId", VisitorId);
            cmd.Parameters.AddWithValue("@Mode", 8);
            objSql.ExecuteNonQuery(cmd);
        }

        public DataSet GetCountryDetails(int EntityId, string EntityName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetVisitorDetails";
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@opt", 1);
            return objSql.GetDs(cmd);
        }

        public DataSet GetCityDetails(int EntityId, string EntityName, string CountryName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetVisitorDetails";
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@opt", 2);
            return objSql.GetDs(cmd);
        }

        public DataSet GetEntityInfo(int EntityId, string EntityName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_FillDropdown";
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@Mode", 3);
            return objSql.GetDs(cmd);
        }
         
        public Int32 InsertPageVisitor(string EntityName, int EntityId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Visitor";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@Mode", 6);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }
    }
}
