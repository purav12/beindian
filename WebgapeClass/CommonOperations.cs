using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebgapeClass
{
    public class CommonOperations
    {
        CommonDAC commandac = new CommonDAC();
        
        public static string RemoveSpecialCharacter(char[] charr)
        {
            string res = "";
            string value = new string(charr);

            value = value.Replace('~', '-');
            value = value.Replace('!', '-');
            value = value.Replace('@', '-');
            value = value.Replace('#', '-');
            value = value.Replace('$', '-');
            value = value.Replace('%', '-');
            value = value.Replace('^', '-');
            value = value.Replace('&', '-');
            value = value.Replace('*', '-');
            value = value.Replace('(', '-');
            value = value.Replace(')', '-');
            value = value.Replace('_', '-');
            value = value.Replace('+', '-');
            value = value.Replace('|', '-');
            value = value.Replace('\\', '-');
            value = value.Replace('/', '-');
            value = value.Replace('?', '-');
            value = value.Replace('\'', '-');
            value = value.Replace('"', '-');
            value = value.Replace(' ', '-');
            value = value.Replace('>', '-');
            value = value.Replace('<', '-');
            value = value.Replace('.', '-');
            value = value.Replace(',', '-');
            value = value.Replace(':', '-');
            value = value.Replace("'", "-");
            value = value.Replace("--", "-");
            value = value.Replace("--", "-");
            value = value.Replace("--", "-");

            res = value;
            return res;

        }

        #region SendMail

        public static Int32 SendMailOld(String MailTo, String MailSubject, String MailBody, String IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            String host = AppLogic.AppConfigs("Host");
            String username = AppLogic.AppConfigs("MailUserName");
            String password = AppLogic.AppConfigs("MailPassword");
            String FromID = AppLogic.AppConfigs("MailFrom");
            String FromName = AppLogic.AppConfigs("MailFromDisplayName");

            MailMessage Msg = new MailMessage();
            SmtpClient MailObj = new SmtpClient(host);
            MailObj.UseDefaultCredentials = false;
            MailObj.Credentials = new System.Net.NetworkCredential(username, password);


            if (AppLogic.AppConfigs("MailPort") != "")
            {
                MailObj.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort"));
                MailObj.EnableSsl = true;
            }

            MailObj.DeliveryMethod = SmtpDeliveryMethod.Network;
            Msg.From = new MailAddress(FromID, FromName);
            String[] MailID = MailTo.Split(';');
            for (Int32 i = 0; i < MailID.Length; i++)
                Msg.To.Add(new MailAddress(MailID[i].ToString()));
            Msg.Subject = MailSubject;
            Msg.Body = MailBody;
            Msg.IsBodyHtml = IsBodyHtml;
            if (Attachment != null)
                Msg.AlternateViews.Add(Attachment);


            
            string body = Msg.Body.ToString();
            string FromMail = Msg.From.ToString();
            string ToEmail = Msg.To.ToString();
            string Subject = Msg.Subject.ToString();
            DateTime MailDate = DateTime.Now;
            try
            {
                MailObj.Send(Msg);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);
                String Body = "<br/><br/>Problem in sending mail from user id: <b>" + Msg.From.ToString() + "</b><br/>";
                Body += "To ID: <b>" + Msg.To.ToString() + "</b><br/>";
                Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                Body += "<br/><br/>Message Body: " + MailBody.ToString();
                body = Body;
                Subject = "Failure :" + Msg.Subject.ToString();
            }

            CommonDAC commandac = new CommonDAC();
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + Msg.From.ToString() + "','" + Msg.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));
            
            return intMailID;
        }

        public static Int32 SendMail(String MailTo, String MailSubject, String MailBody, String IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            if (AppLogic.AppConfigBool("IsServer"))
            {
                String host = AppLogic.AppConfigs("Host"); //relay-hosting.secureserver.net local - smtp.gmail.com
                String username = AppLogic.AppConfigs("MailUserName");
                String password = AppLogic.AppConfigs("MailPassword");
                String FromID = AppLogic.AppConfigs("MailFrom");
                String FromName = AppLogic.AppConfigs("MailFromDisplayName");

                MailMessage message = new MailMessage();
                SmtpClient mailClient = new SmtpClient();

                mailClient.Host = host;
                if (AppLogic.AppConfigs("MailPort") != "")
                {
                    mailClient.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort")); //25 local - 587
                }

                message.From = new MailAddress(FromID, FromName);
                String[] MailID = MailTo.Split(';');
                for (Int32 i = 0; i < MailID.Length; i++)
                    message.To.Add(new MailAddress(MailID[i].ToString()));

                message.Subject = MailSubject;
                message.Body = MailBody;
                message.IsBodyHtml = IsBodyHtml;
                if (Attachment != null)
                    message.AlternateViews.Add(Attachment);


                string body = message.Body.ToString();
                string FromMail = message.From.ToString();
                string ToEmail = message.To.ToString();
                string Subject = message.Subject.ToString();
                DateTime MailDate = DateTime.Now;
                try
                {
                    mailClient.Send(message);
                }
                catch (Exception ex)
                {
                    CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);
                    String Body = "<br/><br/>Problem in sending mail from user id: <b>" + message.From.ToString() + "</b><br/>";
                    Body += "To ID: <b>" + message.To.ToString() + "</b><br/>";
                    Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                    Body += "<br/><br/>Message Body: " + MailBody.ToString();
                    body = Body;
                    Subject = "Failure :" + message.Subject.ToString();
                }

                CommonDAC commandac = new CommonDAC();
                int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + message.From.ToString() + "','" + message.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));

                return intMailID;
            }
            else
            {
                int intMailID;
                intMailID = SendMailOld(MailTo, MailSubject, MailBody, IPAddress, IsBodyHtml, Attachment);
                return intMailID;
            }
        }
   
        ////Not Used Yet
        public static Int32 SendMailWithReplyTo(String ReplyTo, String MailTo, String MailSubject, String MailBody, String IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            String host = AppLogic.AppConfigs("Host");
            String username = AppLogic.AppConfigs("MailUserName");
            String password = AppLogic.AppConfigs("MailPassword");
            String FromID = AppLogic.AppConfigs("MailFrom");

            MailMessage Msg = new MailMessage();
            SmtpClient MailObj = new SmtpClient(host);
            MailObj.UseDefaultCredentials = false;
            MailObj.Credentials = new System.Net.NetworkCredential(username, password);

            if (AppLogic.AppConfigs("MailPort") != "")
            {
                MailObj.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort"));
                MailObj.EnableSsl = true;
            }

            MailObj.DeliveryMethod = SmtpDeliveryMethod.Network;
            Msg.From = new MailAddress(FromID, FromID);
            String[] MailID = MailTo.Split(';');
            for (Int32 i = 0; i < MailID.Length; i++)
                Msg.To.Add(new MailAddress(MailID[i].ToString()));
            Msg.Subject = MailSubject;
            Msg.Body = MailBody;
            Msg.IsBodyHtml = IsBodyHtml;
            Msg.ReplyTo = new MailAddress(ReplyTo, ReplyTo);
            if (Attachment != null)
                Msg.AlternateViews.Add(Attachment);

            string body = Msg.Body.ToString();
            string FromMail = Msg.From.ToString();
            string ToEmail = Msg.To.ToString();
            string Subject = Msg.Subject.ToString();
            DateTime MailDate = DateTime.Now;
            try
            {
                MailObj.Send(Msg);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);

                String Body = "<br/><br/>Problem in sending mail from user id: <b>" + Msg.From.ToString() + "</b><br/>";
                Body += "To ID: <b>" + Msg.To.ToString() + "</b><br/>";
                Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                Body += "<br/><br/>Message Body: " + MailBody.ToString();
                body = Body;
                Subject = "Failure :" + Msg.Subject.ToString();
            }
            CommonDAC commandac = new CommonDAC();
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + Msg.From.ToString() + "','" + Msg.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));
            return intMailID;
        }

        public static Int32 SendMailAttachment(string MailFrom, string MailTo, string MailCC, string MailBCC, string MailSubject, string MailBody, string IPAddress, bool IsBodyHtml, AlternateView Attachment, string filename)
        {
            MailMessage Msg = new MailMessage();
            string host = AppLogic.AppConfigs("Host");
            string username = AppLogic.AppConfigs("MailUserName");
            string password = AppLogic.AppConfigs("MailPassword");
            string FromID = MailFrom;

            SmtpClient MailObj = new SmtpClient(host);
            MailObj.UseDefaultCredentials = false;
            MailObj.Credentials = new System.Net.NetworkCredential(username, password);

            if (AppLogic.AppConfigs("MailPort") != "")
            {
                MailObj.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort"));
                MailObj.EnableSsl = true;
            }

            MailObj.DeliveryMethod = SmtpDeliveryMethod.Network;
            Msg.From = new MailAddress(FromID, FromID);
            string[] MailID = MailTo.Split(';');
            for (int i = 0; i < MailID.Length; i++)
                Msg.To.Add(new MailAddress(MailID[i].ToString()));

            if (!string.IsNullOrEmpty(MailCC.Trim()))
            {
                string[] MailIDCC = MailCC.Split(';');
                for (int i = 0; i < MailIDCC.Length; i++)
                    Msg.CC.Add(new MailAddress(MailIDCC[i].ToString()));
            }
            if (!string.IsNullOrEmpty(MailBCC.Trim()))
            {
                string[] MailIDBCC = MailBCC.Split(';');
                for (int i = 0; i < MailIDBCC.Length; i++)
                    Msg.Bcc.Add(new MailAddress(MailIDBCC[i].ToString()));
            }

            Msg.Subject = MailSubject;
            Msg.Body = MailBody;
            Msg.IsBodyHtml = IsBodyHtml;
            if (Attachment != null)
                Msg.AlternateViews.Add(Attachment);

            try
            {
                if (filename != null && filename.ToString() != "")
                {
                    Msg.Attachments.Add(new System.Net.Mail.Attachment(filename.ToString()));
                }
            }
            catch { }

            string body = Msg.Body.ToString();
            string FromMail = Msg.From.ToString();
            string ToEmail = Msg.To.ToString();
            string Subject = Msg.Subject.ToString();
            DateTime MailDate = DateTime.Now;
            try
            {
                MailObj.Send(Msg);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);

                string Body = "<br/><br/>Problem in sending mail from user id: <b>" + Msg.From.ToString() + "</b><br/>";
                Body += "To ID: <b>" + Msg.To.ToString() + "</b><br/>";
                Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                Body += "<br/><br/>Message Body: " + MailBody.ToString();
                body = Body;
                Subject = MailSubject;
            }
            Int32 MailLogID = 0;
            CommonDAC commandac = new CommonDAC();
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + Msg.From.ToString() + "','" + Msg.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));
            return MailLogID;
        }
        
        public static Int32 SendMail(string MailFrom, string MailTo, string MailCC, string MailBCC, string MailSubject, string MailBody, string IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            MailMessage Msg = new MailMessage();
            string host = AppLogic.AppConfigs("Host");
            string username = AppLogic.AppConfigs("MailUserName");
            string password = AppLogic.AppConfigs("MailPassword");
            string FromID = MailFrom;

            SmtpClient MailObj = new SmtpClient(host);
            MailObj.UseDefaultCredentials = false;
            MailObj.Credentials = new System.Net.NetworkCredential(username, password);

            if (AppLogic.AppConfigs("MailPort") != "")
            {
                MailObj.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort"));
                MailObj.EnableSsl = true;
            }

            MailObj.DeliveryMethod = SmtpDeliveryMethod.Network;
            Msg.From = new MailAddress(FromID, FromID);
            string[] MailID = MailTo.Split(';');
            for (int i = 0; i < MailID.Length; i++)
                Msg.To.Add(new MailAddress(MailID[i].ToString()));

            if (!string.IsNullOrEmpty(MailCC.Trim()))
            {
                string[] MailIDCC = MailCC.Split(';');
                for (int i = 0; i < MailIDCC.Length; i++)
                    Msg.CC.Add(new MailAddress(MailIDCC[i].ToString()));
            }
            if (!string.IsNullOrEmpty(MailBCC.Trim()))
            {
                string[] MailIDBCC = MailBCC.Split(';');
                for (int i = 0; i < MailIDBCC.Length; i++)
                    Msg.Bcc.Add(new MailAddress(MailIDBCC[i].ToString()));
            }

            Msg.Subject = MailSubject;
            Msg.Body = MailBody;
            Msg.IsBodyHtml = IsBodyHtml;
            if (Attachment != null)
                Msg.AlternateViews.Add(Attachment);

            string body = Msg.Body.ToString();
            string FromMail = Msg.From.ToString();
            string ToEmail = Msg.To.ToString();
            string Subject = Msg.Subject.ToString();
            DateTime MailDate = DateTime.Now;
            try
            {
                MailObj.Send(Msg);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);

                string Body = "<br/><br/>Problem in sending mail from user id: <b>" + Msg.From.ToString() + "</b><br/>";
                Body += "To ID: <b>" + Msg.To.ToString() + "</b><br/>";
                Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                Body += "<br/><br/>Message Body: " + MailBody.ToString();
                body = Body;
                Subject = MailSubject;
            }
            Int32 MailLogID = 0;
            CommonDAC commandac = new CommonDAC();
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + Msg.From.ToString() + "','" + Msg.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));
            return MailLogID;
        }
 
        public static Int32 SendTestMail(String MailUserName, String MailPassword, String MailHost, String MailFrom, String MailTo, String MailSubject, String MailBody, String IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            String host = MailHost;
            String username = MailUserName;
            String password = MailPassword;
            String FromID = MailFrom;
            string subject = string.Empty;
            string body = string.Empty;
            MailMessage Msg = new MailMessage();
            SmtpClient MailObj = new SmtpClient(host);
            MailObj.UseDefaultCredentials = false;
            MailObj.Credentials = new System.Net.NetworkCredential(username, password);


            if (AppLogic.AppConfigs("MailPort") != "")
            {
                MailObj.Port = Convert.ToInt32(AppLogic.AppConfigs("MailPort"));
                MailObj.EnableSsl = true;
            }

            MailObj.DeliveryMethod = SmtpDeliveryMethod.Network;
            Msg.From = new MailAddress(FromID, FromID);
            String[] MailID = MailTo.Split(';');
            for (Int32 i = 0; i < MailID.Length; i++)
                Msg.To.Add(new MailAddress(MailID[i].ToString()));
            Msg.Subject = MailSubject;
            Msg.Body = MailBody;
            Msg.IsBodyHtml = IsBodyHtml;
            if (Attachment != null)
                Msg.AlternateViews.Add(Attachment);


            
            subject = Msg.Subject.ToString();
            body = Msg.Body.ToString();
            CommonDAC commandac = new CommonDAC();

            try
            {
                MailObj.Send(Msg);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);

                body = "<br/><br/>Problem in sending mail from user id: <b>" + Msg.From.ToString() + "</b><br/>";
                body += "To ID: <b>" + Msg.To.ToString() + "</b><br/>";
                body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                body += "<br/><br/>Message Body: " + MailBody.ToString();
                subject = "Failure :" + Msg.Subject.ToString();
            }
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + Msg.From.ToString() + "','" + Msg.To.ToString() + "','" + subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));
            return intMailID;
        }

        public static Int32 SendTestMailWeb(String MailUserName, String MailPassword, String MailHost, String MailFrom, String MailFromDisplay, String MailTo, String Port, String MailSubject, String MailBody, String IPAddress, bool IsBodyHtml, AlternateView Attachment)
        {
            String host = MailHost;
            String username = MailUserName;
            String password = MailPassword;
            String FromID = MailFrom;
            String FromName = MailFromDisplay;

            MailMessage message = new MailMessage();
            SmtpClient mailClient = new SmtpClient();

            mailClient.Host = host;
            if (Port != "")
            {
                mailClient.Port = Convert.ToInt32(Port); //25 local - 587
            }

            message.From = new MailAddress(FromID, FromName);
            String[] MailID = MailTo.Split(';');
            for (Int32 i = 0; i < MailID.Length; i++)
                message.To.Add(new MailAddress(MailID[i].ToString()));

            message.Subject = MailSubject;
            message.Body = MailBody;
            message.IsBodyHtml = IsBodyHtml;
            if (Attachment != null)
                message.AlternateViews.Add(Attachment);


            string body = message.Body.ToString();
            string FromMail = message.From.ToString();
            string ToEmail = message.To.ToString();
            string Subject = message.Subject.ToString();
            DateTime MailDate = DateTime.Now;
            try
            {
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("CommonOperations.cs", ex.Message, ex.StackTrace);
                String Body = "<br/><br/>Problem in sending mail from user id: <b>" + message.From.ToString() + "</b><br/>";
                Body += "To ID: <b>" + message.To.ToString() + "</b><br/>";
                Body += "<br/><br/><b>Error Description: <b/>" + ex.Message.ToString();
                Body += "<br/><br/>Message Body: " + MailBody.ToString();
                body = Body;
                Subject = "Failure :" + message.Subject.ToString();
            }

            CommonDAC commandac = new CommonDAC();
            int intMailID = Convert.ToInt32(commandac.ExecuteCommonData("insert into tb_MailLog (FromMail,ToEmail,Subject,IPAddress,MailDate,Body) values ('" + message.From.ToString() + "','" + message.To.ToString() + "','" + Subject + "','" + IPAddress + "','" + DateTime.Now + "','" + body + "') "));

            return intMailID;
        }

        #endregion


    }
}
