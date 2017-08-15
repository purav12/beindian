using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{

    public class SecurityComponent
    {
        #region Declaration
        static string EncryptionKey;
        #endregion
        
        public static string Encrypt(string strText)
        {
            Byte[] IV = { 12, 34, 56, 78, 90, 89, 25, 49 };
            try
            {

                //EncryptionKey =  Convert.ToString(ObjAppconfig.GetAppConfigByName("EncryptionKey"));
                EncryptionKey = System.Configuration.ConfigurationSettings.AppSettings["EncryptionKey"].ToString();
                Byte[] bykey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
                Byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
                cs.Write(InputByteArray, 0, InputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public static string Decrypt(string strText)
        {
            Byte[] IV = { 12, 34, 56, 78, 90, 89, 25, 49 };
            Byte[] inputByteArray = new Byte[strText.Length];
            try
            {

                EncryptionKey = System.Configuration.ConfigurationSettings.AppSettings["EncryptionKey"].ToString();
                //  EncryptionKey = Convert.ToString(ObjAppconfig.GetAppConfigByName("EncryptionKey"));
                Byte[] bykey = System.Text.Encoding.UTF8.GetBytes(EncryptionKey);
                //Byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bykey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
