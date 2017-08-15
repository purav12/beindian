using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class TaskComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetTaskDetails(int TaskId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Task_GetTaskList";
            cmd.Parameters.AddWithValue("@TaskId", TaskId); 
            cmd.Parameters.AddWithValue("@opt", 2);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertTask(DateTime TaskDate, string Task, int AdminId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Task_InsertTask";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@TaskDate", TaskDate);
            cmd.Parameters.AddWithValue("@Task", Task);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public bool UpdateTask(DateTime TaskDate, string Task, int AdminId, int TaskId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Task_InsertTask";
            cmd.Parameters.AddWithValue("@TaskDate", TaskDate);
            cmd.Parameters.AddWithValue("@Task", Task);
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@TaskId", TaskId);
            cmd.Parameters.AddWithValue("@Mode", 2);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }


        public DataSet GetTaskList(int AdminId, string SearchValue, string status, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Task_GetTaskList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId); 
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public void DeleteTask(Int32 TaskId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Task_GetTaskList";
            cmd.Parameters.AddWithValue("@TaskId", TaskId);
            cmd.Parameters.AddWithValue("@opt", 3);
            objSql.GetDs(cmd);
        }

    }
}
