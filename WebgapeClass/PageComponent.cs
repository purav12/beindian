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
    public class PageComponent
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        public DataTable dtAdminRightsList = null;
        #endregion

        public DataSet GetPageDetailsByPageId(int PageId)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Page_GetPageByPageId";
            cmd.Parameters.AddWithValue("@PageId", PageId);
            cmd.Parameters.AddWithValue("@opt", 1);
            return objSql.GetDs(cmd);
        }
    }
}
