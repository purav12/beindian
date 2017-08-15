using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class TopicComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetTopicList(string TopicName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Topic";
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.Parameters.AddWithValue("@Mode", 1);
            return objSql.GetDs(cmd);
        }

        public DataSet GetAllTopicList(string TopicName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Topic";
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.Parameters.AddWithValue("@Mode", 4);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertPost(string Title,string TopicName,bool ShowOnSiteMap,string Description,string SEKeywords,string SEDescription,string SETitle,string SEName,bool Deleted,int CreatedBy)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Topic_InsertTopic";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.Parameters.AddWithValue("@ShowOnSiteMap", ShowOnSiteMap);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@SEName", SEName);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@Mode", 1);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        public bool UpdatePost(string Title, string TopicName, bool ShowOnSiteMap, string Description, string SEKeywords, string SEDescription, string SETitle, string SEName, bool Deleted, int UpdatedBy, int TopicID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Topic_InsertTopic";
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@TopicName", TopicName);
            cmd.Parameters.AddWithValue("@ShowOnSiteMap", ShowOnSiteMap);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@SEKeywords", SEKeywords);
            cmd.Parameters.AddWithValue("@SEDescription", SEDescription);
            cmd.Parameters.AddWithValue("@SETitle", SETitle);
            cmd.Parameters.AddWithValue("@SEName", SEName);
            cmd.Parameters.AddWithValue("@Deleted", Deleted);
            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
            cmd.Parameters.AddWithValue("@TopicID", TopicID);            
            cmd.Parameters.AddWithValue("@Mode", 2);
            bool isupdated = Convert.ToBoolean(objSql.ExecuteNonQuery(cmd));
            return isupdated;
        }
        
    }
}
