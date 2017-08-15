using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;


namespace WebgapeClass
{
    public class AppConfig
    {
        #region Declaration
        public SortedList ListAppConfig;
        private int _AppConfigID;
        private string _ConfigName;
        private string _Configvalue;
        public static int _StoreID = 0;
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        #region Properties

        public int AppConfigID
        {
            get { return _AppConfigID; }
        }

        public string ConfigName
        {
            get { return _ConfigName; }
        }

        public string ConfigValue
        {
            get { return _Configvalue; }
        }

        #endregion

        #region Constructors

        public AppConfig(int AppConfigID, string ConfigName, string Configvalue)
        {
            _AppConfigID = AppConfigID;
            _ConfigName = ConfigName;
            _Configvalue = Configvalue;
        }

        public AppConfig()
        {
            DataSet dsAppConfig = new DataSet();
            ListAppConfig = new SortedList();

            if (StoreID == 0)
            {
                StoreID = Convert.ToInt32(ConfigurationManager.AppSettings["GeneralStoreID"]);
            }

            dsAppConfig = GetAppConfig(StoreID);

            if (dsAppConfig != null && dsAppConfig.Tables.Count > 0 && dsAppConfig.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsAppConfig.Tables[0].Rows.Count; i++)
                {
                    if (dsAppConfig.Tables[0].Rows[i]["ConfigName"] != null)
                    {
                        ListAppConfig.Add(Convert.ToString(dsAppConfig.Tables[0].Rows[i]["ConfigName"]).ToLowerInvariant(), new AppConfig(Convert.ToInt32(dsAppConfig.Tables[0].Rows[i]["AppConfigID"].ToString()), Convert.ToString(dsAppConfig.Tables[0].Rows[i]["ConfigName"]), Convert.ToString(dsAppConfig.Tables[0].Rows[i]["ConfigValue"])));
                    }
                }

                ListAppConfig.Add("StoreID".ToLowerInvariant(), new AppConfig(dsAppConfig.Tables[0].Rows.Count + 1000000, "StoreID".ToLowerInvariant(), Convert.ToString(StoreID)));
            }
        }

        #endregion

        #region Key Functions

        public DataSet GetAppConfig(int StoreID)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[usp_AppConfig]";
            cmd.Parameters.AddWithValue("@Mode", 1);
            return objSql.GetDs(cmd);
        }


        /// <summary>
        /// Get ConfigValue from Config
        /// </summary>
        /// <param name="name">String Name</param>
        /// <returns>AppConfig Value</returns>
        public AppConfig this[string ConfigName]
        {
            get
            {
                return (AppConfig)ListAppConfig[ConfigName.ToLowerInvariant()];
            }
        }

        public static int StoreID
        {
            get { return _StoreID; }
            set
            {
                _StoreID = value;
            }
        }

        #endregion
    }
}
