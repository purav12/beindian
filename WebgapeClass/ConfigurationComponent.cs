using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{

    public class ConfigurationComponent
    {
        #region Declaration

        private SqlCommand cmd = null;
        private SQLAccess objSql = null;

        #endregion

        #region Key functions

      
        public DataSet GetMailConfig(string ConfigName, string ConfigValue, DateTime UpdatedOn, int UpdatedBy, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_MailConfig";
            if (ConfigName != "" && ConfigName.Length > 0)
                cmd.Parameters.AddWithValue("@ConfigName", ConfigName);
            if (ConfigValue != "" && ConfigValue.Length > 0)
                cmd.Parameters.AddWithValue("@ConfigValue", ConfigValue);
            cmd.Parameters.AddWithValue("@UpdatedOn", UpdatedOn);
            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

      
        public Object UpdateMailConfig(string ConfigName, string ConfigValue, DateTime UpdatedOn, int UpdatedBy, int Mode)
        {

            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_MailConfig";
            cmd.Parameters.AddWithValue("@ConfigName", ConfigName);
            cmd.Parameters.AddWithValue("@ConfigValue", ConfigValue);
            cmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
            cmd.Parameters.AddWithValue("@UpdatedOn", UpdatedOn);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

      
        public DataSet GetImagesize()
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_ImageSize";
            cmd.Parameters.AddWithValue("@Mode", 1);
            return objSql.GetDs(cmd);

        }

      
        public DataSet GetImageSizeByType( string ImageName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_ImageSize";
            cmd.Parameters.AddWithValue("@Mode", 3);
            cmd.Parameters.AddWithValue("@ImageName", ImageName);
            return objSql.GetDs(cmd);
        }

     
        public DataSet UpdateImageSize(string ImageName, string ImageSize, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_ImageSize";
            cmd.Parameters.AddWithValue("@ImageName", ImageName);
            cmd.Parameters.AddWithValue("@ImageSize", ImageSize);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

            
        #endregion

        public String GetBreadCrum(int CategoryID, int ParentId,  string Type, int Mode, int IsInsert)
        {
            string BreadCrumPath = "";
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetBreadCrumbPath";
            cmd.Parameters.AddWithValue("@ID", CategoryID);
            cmd.Parameters.AddWithValue("@PID", ParentId);
            cmd.Parameters.AddWithValue("@AppPath", "");
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            cmd.Parameters.AddWithValue("@IsInsert", IsInsert);
            BreadCrumPath = Convert.ToString(objSql.ExecuteScalarQuery(cmd));
            return BreadCrumPath;

        }

        public Int32 UpdateAppConfigvalue(String Configname, string flag, Int32 StoreID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_UpdateConfig";
            cmd.Parameters.AddWithValue("@Configname", Configname);
            cmd.Parameters.AddWithValue("@flag", flag);
            cmd.Parameters.AddWithValue("@StoreID", StoreID);
            cmd.Parameters.AddWithValue("@Mode", 1);
            return Convert.ToInt32(objSql.ExecuteNonQuery(cmd));
        }

        public DataSet GetConfigDescription(String Configname, int Storeid)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_UpdateConfig";
            cmd.Parameters.AddWithValue("@Configname", Configname);
            cmd.Parameters.AddWithValue("@StoreID", Storeid);
            cmd.Parameters.AddWithValue("@Mode", 2);
            return objSql.GetDs(cmd);
        }

    }
}
