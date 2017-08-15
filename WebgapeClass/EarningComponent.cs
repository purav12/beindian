using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class EarningComponent
    {
        #region declaration
        SQLAccess objSql;
        SqlCommand cmd;
        #endregion

        public DataSet GetEarningList(int AdminId, string SearchBy, string SearchValue, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Earning_GetEarningList";
            cmd.Parameters.AddWithValue("@AdminId", AdminId);
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

    }
}
