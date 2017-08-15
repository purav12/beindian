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
    public class AdminComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        public DataTable dtAdminRightsList = null;
        #endregion

        public Int32 InsertAdmin(string FirstName, string LastName, string Email, string Password)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@EmailID", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public Int32 AdminProfile(string Firstname, string LastName, string Username, string Email, int AdminId, string ImageName, bool IsPic)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_Profile";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EmailID", Email);
            cmd.Parameters.AddWithValue("@UserName", Username);
            cmd.Parameters.AddWithValue("@FirstName", Firstname);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@ImageName", ImageName);
            cmd.Parameters.AddWithValue("@IsPic", IsPic);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public DataSet GetAdminProfileByAdminId(int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_Profile";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 2);
            return objSql.GetDs(cmd);
        }

        public void AddAdminView(int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_Profile";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 4);
            objSql.GetDt(cmd);
        }

        public DataTable GetAdminRightsByAdminId(int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_Profile";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 3);
            return objSql.GetDt(cmd);
        }

        public DataSet GetAdminRightsDSByAdminId(int AdminTypeID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_Profile";
            cmd.Parameters.AddWithValue("@AdminTypeID", AdminTypeID);
            cmd.Parameters.AddWithValue("@Mode", 4);
            return objSql.GetDs(cmd);
        }

        public Int32 UpdateAdminProfile(string Email, string Password)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EmailID", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public DataSet GetAdminForLogin(string UserName, string Password)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@EmailID", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Mode", 2);
            return objSql.GetDs(cmd);
        }

        public DataSet VerifyAdmin(string UserName, string Password)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@EmailID", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Mode", 5);
            return objSql.GetDs(cmd);
        }

        public DataSet GetForgotpassinfo(string UserName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@EmailID", UserName);
            cmd.Parameters.AddWithValue("@Mode", 4);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAdminCountsByAdminId(Int32 AdminID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@AdminID", AdminID);
            cmd.Parameters.AddWithValue("@Mode", 11);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAdminCountsForMaster(Int32 AdminID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@AdminID", AdminID);
            cmd.Parameters.AddWithValue("@Mode", 12);
            return objSql.GetDs(cmd);
        }

        public int IsSuperAdmin(int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 13);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public Int32 IsExist(string Email)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EmailID", Email);
            cmd.Parameters.AddWithValue("@Mode", 10);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public Int32 IsValidate(string Email, string Code)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EmailID", Email);
            cmd.Parameters.AddWithValue("@Password", Code);
            cmd.Parameters.AddWithValue("@Mode", 5);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public void GetAllPageRightsByAdminID(Int32 AdminID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            DataSet dsright = new DataSet();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@AdminID", AdminID);
            cmd.Parameters.AddWithValue("@Mode", 3);
            dsright = objSql.GetDs(cmd);
            if (dsright != null && dsright.Tables.Count > 0 && dsright.Tables[0].Rows.Count > 0)
            {
                dtAdminRightsList = dsright.Tables[0];
                System.Web.HttpContext.Current.Session["dtAdminRightsList"] = dsright.Tables[0];
            }
        }

        public DataSet GetEmailTamplate(String Lable, Int32 StoreID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@Label", Lable);
            cmd.Parameters.AddWithValue("@StoreID", StoreID);
            cmd.Parameters.AddWithValue("@Mode", 9);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAdminCode(string EmailId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin";
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@Mode", 14);
            return objSql.GetDs(cmd);
        }
    }
}
