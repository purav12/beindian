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
    public class UserComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        #endregion

        public DataSet GetAllUserDetails(string SearchValue)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Admin_GetAdminList";
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@Mode", 2);
            return objSql.GetDs(cmd);
        }

    }
}
