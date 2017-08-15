using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebgapeClass
{
    public class MessageComponent
    {
        #region declaration
        SQLAccess objSql;
        SqlCommand cmd;
        #endregion

        public DataSet GetMessageList(int AdminId, string SearchBy, string SearchValue, string status, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Message_GetMessageList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public void DeleteMessage(Int32 MessageId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Message_GetMessageList";
            cmd.Parameters.AddWithValue("@MessageId", MessageId);
            cmd.Parameters.AddWithValue("@opt", 3);
            objSql.GetDs(cmd);
        }


        public DataSet GetMessageByMessageId(int MessageId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Message_GetMessageByMessageId";
            cmd.Parameters.AddWithValue("@MessageId", MessageId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public bool UpdateMessage(int MessageId, string Message, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Message_InsertMessage";
            cmd.Parameters.AddWithValue("@MessageId", MessageId);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }
        public Int32 InsertMessage(int FromId, int ToId, string Message, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Message_InsertMessage";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@FromId", FromId);
            cmd.Parameters.AddWithValue("@ToId", ToId);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }
    }
}
