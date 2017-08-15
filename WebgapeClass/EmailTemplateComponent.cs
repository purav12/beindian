using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class EmailTemplateComponent
    {
        #region declaration
        private SQLAccess objSql = null;
        private SqlCommand cmd = null;
        #endregion

        public DataSet GetEmailTemplateList(string SearchBy, string SearchValue,int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_EmailTemplate_GetTemplateList";
            cmd.Parameters.AddWithValue("@SearchBy", SearchBy);
            cmd.Parameters.AddWithValue("@SearchValue", SearchValue);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetEmailTemplateByTemplateId(int TemplateId, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_EmailTemplate_GetEmailTemplateByTemplateId";
            cmd.Parameters.AddWithValue("@TemplateId", TemplateId);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public DataSet GetEmailTemplateByTemplateName(string TemplateName, int opt)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_EmailTemplate_GetEmailTemplateByTemplateId";
            cmd.Parameters.AddWithValue("@Label", TemplateName);
            cmd.Parameters.AddWithValue("@opt", opt);
            return objSql.GetDs(cmd);
        }

        public Int32 InsertEmailTemplate(string Label, int TemplateID, string Subject, string EmailBody, string EmailType,int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_EmailTemplate_InsertEmailTemplate";
            SqlParameter paramReturnval = new SqlParameter("@Returnval", SqlDbType.Int, 4);
            paramReturnval.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(paramReturnval);
            cmd.Parameters.AddWithValue("@TemplateID", TemplateID);
            cmd.Parameters.AddWithValue("@Label", Label);
            cmd.Parameters.AddWithValue("@Subject", Subject);
            cmd.Parameters.AddWithValue("@EmailBody", EmailBody);
            cmd.Parameters.AddWithValue("@EmailType", EmailType);
            cmd.Parameters.AddWithValue("@Mode", Mode);
            objSql.ExecuteNonQuery(cmd);
            return Convert.ToInt32(paramReturnval.Value);
        }

        
    }
}
