using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;

namespace WebgapeClass
{
    public class SQLAccess
    {
        #region Declaration
        private SqlConnection conn = null;
        public SqlCommand cmd;
        public DataSet ds;
        public DataTable dt;
        #endregion

        public SQLAccess()
        {
            conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Webgape_Ecomm_DBEntities"]);
        }

        public bool ExecuteNonQuery(string Query)
        {
            bool result;
            cmd = new SqlCommand(Query, conn);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                int index = cmd.ExecuteNonQuery();
                if (index != 1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            catch (Exception ex)
            {
                result = false;
                HttpContext context = HttpContext.Current;
                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
            }
            return result;
        }

        public Object ExecuteNonQuery(SqlCommand cmd)
        {
            Object Obj = new Object();
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                Obj = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Obj = null;
                string Query = "";
                foreach (IDataParameter i in cmd.Parameters)
                {
                    Query = Query + "," + "Parameter" + i.ParameterName.ToString() + " -values" + i.Value.ToString();
                }

                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace + "Query-" + cmd.CommandText.ToString() + "-OrgQuery " + Query);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
            }
            return Obj;
        }

        public Object ExecuteScalarQuery(string Query)
        {
            Object Obj = new Object();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = Query;
                Obj = ExecuteScalarQuery(cmd);
            }
            catch (Exception ex)
            {
                Obj = null;
                HttpContext context = HttpContext.Current;
                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace);
            }
            finally
            {
                cmd.Dispose();
            }
            return Obj;
        }

        public Object ExecuteScalarQuery(SqlCommand cmd)
        {
            Object Obj = new Object();
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                Obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Obj = null;

                string Query = "";
                foreach (IDataParameter i in cmd.Parameters)
                {
                    Query = Query + "," + "Parameter" + i.ParameterName.ToString() + " -values" + i.Value.ToString();
                }

                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace + "Query-" + cmd.CommandText.ToString() + "-OrgQuery " + Query);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
            }
            return Obj;
        }

        public DataSet GetDs(string Sql)
        {
            SqlDataAdapter Adpt = new SqlDataAdapter();
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = Sql;
                Adpt.SelectCommand = cmd;
                Adpt.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
                //HttpContext context = HttpContext.Current;
                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
                Adpt.Dispose();
            }
            return ds;
        }
        
        public DataTable GetDt(SqlCommand cmd)
        {
            SqlDataAdapter Adpt = new SqlDataAdapter();
            try
            {
                dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.Connection = conn;

                Adpt.SelectCommand = cmd;
                Adpt.Fill(dt);
            }
            catch (Exception ex)
            {
                Adpt.Dispose();
                ds = null;
                string Query = "";
                foreach (IDataParameter i in cmd.Parameters)
                {
                    Query = Query + "," + "Parameter" + i.ParameterName.ToString() + " -values" + i.Value.ToString();
                }

                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace + "Query-" + cmd.CommandText.ToString() + "-OrgQuery " + Query);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
                Adpt.Dispose();
            }
            return dt;
        }
        
        public DataSet GetDs(SqlCommand cmd)
        {
            SqlDataAdapter Adpt = new SqlDataAdapter();
            try
            {
                ds = new DataSet();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.Connection = conn;

                Adpt.SelectCommand = cmd;
                Adpt.Fill(ds);
            }
            catch (Exception ex)
            {
                Adpt.Dispose();
                ds = null;
                string Query = "";
                foreach (IDataParameter i in cmd.Parameters)
                {
                    Query = Query + "," + "Parameter" + i.ParameterName.ToString() + " -values" + i.Value.ToString();
                }

                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace + "Query-" + cmd.CommandText.ToString() + "-OrgQuery " + Query);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
                Adpt.Dispose();
            }
            return ds;
        }

        public DataSet GetDsForScrollPost(SqlCommand cmd)
        {
            SqlDataAdapter Adpt = new SqlDataAdapter();
            try
            {
                ds = new DataSet();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                cmd.Connection = conn;

                Adpt.SelectCommand = cmd;
                Adpt.Fill(ds, "Posts");
                DataTable dt = new DataTable("PageCount");
                dt.Columns.Add("PageCount");
                dt.Rows.Add();
                dt.Rows[0][0] = cmd.Parameters["@PageCount"].Value;
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                Adpt.Dispose();
                ds = null;
                string Query = "";
                foreach (IDataParameter i in cmd.Parameters)
                {
                    Query = Query + "," + "Parameter" + i.ParameterName.ToString() + " -values" + i.Value.ToString();
                }

                CommonDAC.ErrorLog("SQLAccess.cs", ex.Message, ex.StackTrace + "Query-" + cmd.CommandText.ToString() + "-OrgQuery " + Query);
            }
            finally
            {
                if (conn != null)
                    if (conn.State == ConnectionState.Open) conn.Close();
                cmd.Dispose();
                Adpt.Dispose();
            }
            return ds;
        }
    }
}
