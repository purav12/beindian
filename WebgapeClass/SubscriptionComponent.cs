using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class SubscriptionComponent
    {
        #region declaration
        SQLAccess objSql;
        SqlCommand cmd;
        #endregion

        public DataSet GetSubscriptionList(int AdminId, string SearchBy, string SearchValue, string status, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Subscription_GetSubscriptionList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public void DeleteSubscription(Int32 SubscriptionId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Subscription_GetSubscriptionList";
            cmd.Parameters.AddWithValue("@SubscriptionId", SubscriptionId);
            cmd.Parameters.AddWithValue("@opt", 3);
            objSql.GetDs(cmd);
        }


        public DataSet GeSubscriptionBySubscriptionId(int SubscriptionId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Subscription_GetSubscriptionBySubscriptionId";
            cmd.Parameters.AddWithValue("@SubscriptionId", SubscriptionId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertSubscription(string EmailId, int EntityId, string EntityName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Subscribe_InsertSubscribe";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@EntityId", EntityId);
            cmd.Parameters.AddWithValue("@EntityName", EntityName);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }
    }
}
