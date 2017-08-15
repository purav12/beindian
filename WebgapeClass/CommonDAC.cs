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
    public class CommonDAC
    {
        #region Declaration
        private SqlCommand cmd = null;
        private SQLAccess objSql = null;
        private SqlDataAdapter adp = null;
        #endregion

        public Object GetCategoryID(string CatName, string ParentName, string GrandName, int StoreID, int CategoryID, int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Global";
            cmd.Parameters.AddWithValue("@CatName", CatName);

            if (ParentName != "" && ParentName.Length > 0)
                cmd.Parameters.AddWithValue("@ParentName", ParentName);

            if (GrandName != "" && GrandName.Length > 0)
                cmd.Parameters.AddWithValue("@GrandName", GrandName);

            cmd.Parameters.AddWithValue("@StoreID", StoreID);

            if (CategoryID > 0)
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@Mode", Mode);

            return objSql.ExecuteScalarQuery(cmd);
        }

        public DataTable GetCommonDataTable(string Query)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Common_Dataset";
            cmd.Parameters.AddWithValue("@Query", Query);
            return objSql.GetDt(cmd);
        }

        public DataSet GetCommonDataSet(string Query)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Common_Dataset";
            cmd.Parameters.AddWithValue("@Query", Query);
            return objSql.GetDs(cmd);
        }

        public DataSet GetContactUsDropdownData(string CategoryName)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_FillDropdown";
            cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
            cmd.Parameters.AddWithValue("@Mode", 1);
            return objSql.GetDs(cmd);
        }

        public DataSet GetDropdownData(int Mode)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_FillDropdown";
            cmd.Parameters.AddWithValue("@Mode", Mode);
            return objSql.GetDs(cmd);
        }

        public Object GetScalarCommonData(string Query)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Common_Dataset";
            cmd.Parameters.AddWithValue("@Query", Query);
            return objSql.ExecuteScalarQuery(cmd);
        }

        public Object ExecuteCommonData(string Query)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Common_Dataset";
            cmd.Parameters.AddWithValue("@Query", Query);
            return objSql.ExecuteNonQuery(cmd);
        }

        public Object ExecuteDatabaseBackup(string Path)
        {
            objSql = new SQLAccess();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_DatabaseBackup";
            cmd.Parameters.AddWithValue("@BackupPath", Path);
            return objSql.ExecuteNonQuery(cmd);
        }

        public static void ErrorLog(string PageName, string Error, string ErrorDetails)
        {
            HttpContext context = HttpContext.Current;
            FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(@"/ErrorLog.xml"));
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(info.FullName))
            {

                XmlTextWriter textWritter = new XmlTextWriter(info.FullName, null);
                textWritter.WriteStartDocument();
                textWritter.WriteStartElement("Root");
                textWritter.WriteEndElement();

                textWritter.Close();
            }
            xmlDoc.Load(info.FullName);

            XmlElement subRoot = xmlDoc.CreateElement("ErrorLogInfo");

            //DateTime
            XmlElement appendedElementDateTime = xmlDoc.CreateElement("DateTime");
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            XmlText xmlTextDateTime = xmlDoc.CreateTextNode(DateTime.Now.ToString() + ", IST - " + indianTime);
            appendedElementDateTime.AppendChild(xmlTextDateTime);
            subRoot.AppendChild(appendedElementDateTime);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //Page
            XmlElement appendedElementPage = xmlDoc.CreateElement("Page");
            XmlText xmlTextPage = xmlDoc.CreateTextNode(PageName);
            appendedElementPage.AppendChild(xmlTextPage);
            subRoot.AppendChild(appendedElementPage);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //Method
            XmlElement appendedElementIPAddress = xmlDoc.CreateElement("IPAddress");
            XmlText xmlTextIPAddress = xmlDoc.CreateTextNode(context.Request.UserHostAddress.ToString());
            appendedElementIPAddress.AppendChild(xmlTextIPAddress);
            subRoot.AppendChild(appendedElementIPAddress);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //Error
            XmlElement appendedElementError = xmlDoc.CreateElement("Error");
            XmlText xmlTextError = xmlDoc.CreateTextNode(Error);
            appendedElementError.AppendChild(xmlTextError);
            subRoot.AppendChild(appendedElementError);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //StackTrace
            XmlElement appendedElementStackTrace = xmlDoc.CreateElement("StackTrace");
            XmlText xmlTextStackTrace = xmlDoc.CreateTextNode(ErrorDetails);
            appendedElementStackTrace.AppendChild(xmlTextStackTrace);
            subRoot.AppendChild(appendedElementStackTrace);
            xmlDoc.DocumentElement.AppendChild(subRoot);

            //Save Doc
            xmlDoc.Save(info.FullName);
        }
    }
}
