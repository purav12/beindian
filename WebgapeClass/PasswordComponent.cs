using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class PasswordComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetPasswordDetails(int PasswordId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Password_GetPasswordList";
            cmd.Parameters.AddWithValue("@PasswordId", PasswordId);
            cmd.Parameters.AddWithValue("@opt", 2);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertPassword(int PasswordType, string Title, string Identifier, string Password, string Comment, int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Password_InsertPassword";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Type", PasswordType);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Identifier", Identifier);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Comment", Comment);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public bool UpdatePassword(int PasswordType, string Title, string Identifier, string Password, string Comment, int AdminId, int PasswordId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Password_InsertPassword";
            cmd.Parameters.AddWithValue("@Type", PasswordType);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Identifier", Identifier);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Comment", Comment);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@PasswordId", PasswordId);
            cmd.Parameters.AddWithValue("@Mode", 2);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }


        public DataSet GetPasswordList(int AdminId, string SearchBy, string SearchValue, string status, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Password_GetPasswordList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public void DeletePassword(Int32 PasswordId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Password_GetPasswordList";
            cmd.Parameters.AddWithValue("@PasswordId", PasswordId);
            cmd.Parameters.AddWithValue("@opt", 3);
            objSql.GetDs(cmd);
        }

    }
}
