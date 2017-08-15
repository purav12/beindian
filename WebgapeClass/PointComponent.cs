using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class PointComponent
    {
        #region declaration
        SQLAccess objSql;
        SqlCommand cmd;
        #endregion

        public DataSet GetPointList(int AdminId, string SearchBy, string SearchValue, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Point_GetPointList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

    }
}
