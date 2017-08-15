using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape
{
    public partial class Utility : System.Web.UI.Page
    {
        CommonDAC commandac = new CommonDAC();
        ConfigurationComponent objConfiguration = new ConfigurationComponent();
        string PostTempPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/");
        string AudPostTempPath = string.Concat(AppLogic.AppConfigs("AudioPathPost"), "Temp/");
        string PostIconPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Icon/");
        string PostMediumPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Medium/");
        string PostLargePath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Large/");
        string PostMicroPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Micro/");
        static int finHeight;
        static int finWidth;
        static Size thumbNailSizeLarge = Size.Empty;
        static Size thumbNailSizeMediam = Size.Empty;
        static Size thumbNailSizeIcon = Size.Empty;
        static Size thumbNailSizeMicro = Size.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindSize();
            AddImagesViaYoutube();
        }

        public void AddImagesViaYoutube()
        {
            DataSet dsPost = new DataSet();
            string VideoLink, VideoId, FIleName = string.Empty;
            dsPost = commandac.GetCommonDataSet("SELECT * FROM tb_post where AdminId = 42 and PostId <> 1576 ORDER BY 1 DESC");
            if (dsPost != null && dsPost.Tables.Count > 0 && dsPost.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsPost.Tables[0].Rows.Count; i++)
                {
                    VideoLink = dsPost.Tables[0].Rows[i]["VideoLink"].ToString();
                    VideoId = VideoLink.Substring(VideoLink.IndexOf("embed/") + 6, VideoLink.IndexOf("?show") - (VideoLink.IndexOf("embed/") + 6));
                    FIleName = dsPost.Tables[0].Rows[i]["PostId"].ToString();
                    DownloadImage(FIleName, VideoId);
                }
            }
        }

        public void DownloadImage(string FIleName, string VideoId)
        {
            string strImageName = "";

            strImageName = "10_" + FIleName + ".jpg";

            System.Drawing.Image image = DownloadImageFromUrl("http://img.youtube.com/vi/"+ VideoId+ "/maxresdefault.jpg");
            String strSavedImgPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/");// + strImageName.ToString();

            string fileName = System.IO.Path.Combine(Server.MapPath(strSavedImgPath), strImageName);
            image.Save(fileName);
            ImgLarge.Src = strSavedImgPath + strImageName;
            //Download Process
            //System.Net.WebClient objClient = new System.Net.WebClient();

            //objClient.DownloadFile(strImageName.ToString(), Server.MapPath(strSavedImgPath));
            //if (File.Exists(Server.MapPath(strSavedImgPath)))
            //{
            //    ImgLarge.Src = strSavedImgPath.ToString();
            //    ViewState["File"] = strImageName.ToString();
            //}
            //Download Process

            SaveImage(strImageName);
            commandac.ExecuteCommonData("update tb_Post set ImageName='" + strImageName + "' where PostId='" + FIleName + "'");
        }

        public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            System.Drawing.Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        protected void SaveImage(string FileName)
        {
            //create icon folder 
            if (!Directory.Exists(Server.MapPath(PostIconPath)))
                Directory.CreateDirectory(Server.MapPath(PostIconPath));

            //create Medium folder 
            if (!Directory.Exists(Server.MapPath(PostMediumPath)))
                Directory.CreateDirectory(Server.MapPath(PostMediumPath));

            //create Large folder 
            if (!Directory.Exists(Server.MapPath(PostLargePath)))
                Directory.CreateDirectory(Server.MapPath(PostLargePath));

            //create Micro folder 
            if (!Directory.Exists(Server.MapPath(PostMicroPath)))
                Directory.CreateDirectory(Server.MapPath(PostMicroPath));

            if (ImgLarge.Src.Contains(PostTempPath))
            {
                try
                {
                    CreateImage("Medium", FileName);
                    CreateImage("Icon", FileName);
                    CreateImage("Micro", FileName);
                    CreateImage("Large", FileName);
                }
                catch (Exception ex)
                {
                    //lblMsg.Text += "<br />" + ex.Message;
                }
                finally
                {
                    //DeleteTempFile("icon");
                }
            }
        }

        protected void CreateImage(string Size, string FileName)
        {
            try
            {
                string strFile = null;
                String strPath = "";
                if (ImgLarge.Src.ToString().IndexOf("?") > -1)
                {
                    strPath = ImgLarge.Src.Split('?')[0];
                }
                else
                {
                    strPath = ImgLarge.Src.ToString();
                }
                strFile = Server.MapPath(strPath);
                string strFilePath = "";
                Size = Size.ToLower();
                switch (Size)
                {
                    case "large":
                        strFilePath = Server.MapPath(PostLargePath + FileName);

                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostLargePath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "medium":
                        strFilePath = Server.MapPath(PostMediumPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostMediumPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "icon":
                        strFilePath = Server.MapPath(PostIconPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostIconPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                    case "micro":
                        strFilePath = Server.MapPath(PostMicroPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(PostMicroPath + ViewState["DelImage"].ToString());
                            }
                        }
                        break;
                }
                ResizePhoto(strFile, Size, strFilePath);
            }
            catch (Exception ex)
            {
                if (ex.Source == "System.Drawing")
                    lblMsg.Text = "<br />Error Saving " + Size + " Image..Please check that Directory exists..";
                else
                    lblMsg.Text += "<br />" + ex.Message;
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }

        }

        private void BindSize()
        {
            DataSet dsIconWidth = objConfiguration.GetImageSizeByType("PostIconWidth");
            DataSet dsIconHeight = objConfiguration.GetImageSizeByType("PostIconHeight");
            if ((dsIconWidth != null && dsIconWidth.Tables.Count > 0 && dsIconWidth.Tables[0].Rows.Count > 0) && (dsIconHeight != null && dsIconHeight.Tables.Count > 0 && dsIconHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeIcon = new Size(Convert.ToInt32(dsIconWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsIconHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsLargeWidth = objConfiguration.GetImageSizeByType("PostLargeWidth");
            DataSet dsLargeHeight = objConfiguration.GetImageSizeByType("PostLargeHeight");
            if ((dsLargeWidth != null && dsLargeWidth.Tables.Count > 0 && dsLargeWidth.Tables[0].Rows.Count > 0) && (dsLargeHeight != null && dsLargeHeight.Tables.Count > 0 && dsLargeHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeLarge = new Size(Convert.ToInt32(dsLargeWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsLargeHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsMediumWidth = objConfiguration.GetImageSizeByType("PostMediumWidth");
            DataSet dsMediumHeight = objConfiguration.GetImageSizeByType("PostMediumHeight");
            if ((dsMediumWidth != null && dsMediumWidth.Tables.Count > 0 && dsMediumWidth.Tables[0].Rows.Count > 0) && (dsMediumHeight != null && dsMediumHeight.Tables.Count > 0 && dsMediumHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeMediam = new Size(Convert.ToInt32(dsMediumWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsMediumHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }

            DataSet dsMicroWidth = objConfiguration.GetImageSizeByType("PostMicroWidth");
            DataSet dsMicroHeight = objConfiguration.GetImageSizeByType("PostMicroHeight");
            if ((dsMicroWidth != null && dsMicroWidth.Tables.Count > 0 && dsMicroWidth.Tables[0].Rows.Count > 0) && (dsMicroHeight != null && dsMicroHeight.Tables.Count > 0 && dsMicroHeight.Tables[0].Rows.Count > 0))
            {
                thumbNailSizeMicro = new Size(Convert.ToInt32(dsMicroWidth.Tables[0].Rows[0]["Size"].ToString().Trim()), Convert.ToInt32(dsMicroHeight.Tables[0].Rows[0]["Size"].ToString().Trim()));
            }
        }

        public void ResizePhoto(string strFile, string Size, string strFilePath)
        {
            switch (Size)
            {
                case "medium":
                    finHeight = thumbNailSizeMediam.Height;
                    finWidth = thumbNailSizeMediam.Width;
                    break;
                case "icon":
                    finHeight = thumbNailSizeIcon.Height;
                    finWidth = thumbNailSizeIcon.Width;
                    break;
                case "micro":
                    finHeight = thumbNailSizeMicro.Height;
                    finWidth = thumbNailSizeMicro.Width;
                    break;

            }
            if (Size == "large")
            {
                File.Copy(strFile, strFilePath, true);
            }
            else
                ResizeImage(strFile, finWidth, finHeight, strFilePath);
        }

        public void ResizeImage(string strFile, int FinWidth, int FinHeight, string strFilePath)
        {
            System.Drawing.Image imgWebgape = System.Drawing.Image.FromFile(strFile);
            float resizePercent = 0;
            int resizedHeight = imgWebgape.Height;
            int resizedWidth = imgWebgape.Width;

            resizedHeight = FinHeight;
            resizedWidth = FinWidth;

            #region commented
            //if (imgWebgape.Height >= FinHeight && imgWebgape.Width >= FinWidth)
            //{
            //    float resizePercentHeight = 0;
            //    float resizePercentWidth = 0;
            //    resizePercentHeight = (FinHeight * 100) / imgWebgape.Height;
            //    resizePercentWidth = (FinWidth * 100) / imgWebgape.Width;
            //    if (resizePercentHeight < resizePercentWidth)
            //    {
            //        resizedHeight = FinHeight;
            //        resizedWidth = (int)Math.Round(resizePercentHeight * imgWebgape.Width / 100.0);
            //    }
            //    if (resizePercentHeight >= resizePercentWidth)
            //    {
            //        resizedWidth = FinWidth;
            //        resizedHeight = (int)Math.Round(resizePercentWidth * imgWebgape.Height / 100.0);
            //    }
            //}
            //else if (imgWebgape.Width >= FinWidth && imgWebgape.Height <= FinHeight)
            //{
            //    resizedWidth = FinWidth;
            //    resizePercent = (FinWidth * 100) / imgWebgape.Width;
            //    resizedHeight = (int)Math.Round((imgWebgape.Height * resizePercent) / 100.0);
            //}

            //else if (imgWebgape.Width <= FinWidth && imgWebgape.Height >= FinHeight)
            //{
            //    resizePercent = (FinHeight * 100) / imgWebgape.Height;
            //    resizedHeight = FinHeight;
            //    resizedWidth = (int)Math.Round(resizePercent * imgWebgape.Width / 100.0);
            //}
            #endregion

            Bitmap resizedPhoto = new Bitmap(resizedWidth, resizedHeight, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(resizedPhoto);

            int destWidth = resizedWidth;
            int destHeight = resizedHeight;
            int sourceWidth = imgWebgape.Width;
            int sourceHeight = imgWebgape.Height;

            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle DestRect = new Rectangle(0, 0, destWidth, destHeight);
            Rectangle srcRect = new Rectangle(0, 0, sourceWidth, sourceHeight);
            grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            grPhoto.DrawImage(imgWebgape, DestRect, srcRect, GraphicsUnit.Pixel);

            GenerateImage(resizedPhoto, strFilePath, FinWidth, FinHeight);

            resizedPhoto.Dispose();
            grPhoto.Dispose();
            imgWebgape.Dispose();

        }

        private void GenerateImage(Bitmap extBMP, string DestFileName, int DefWidth, int DefHeight)
        {
            Encoder Enc = Encoder.SaveFlag;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");
            EncParm = new EncoderParameter(Encoder.Quality, (long)600);
            EncParms.Param[0] = new EncoderParameter(Encoder.Quality, (long)600);

            if (extBMP != null && extBMP.Width < (DefWidth) && extBMP.Height < (DefHeight))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startX = (int)(DefWidth / 2) - (extBMP.Width / 2);
                int startY = (int)(DefHeight / 2) - (extBMP.Height / 2);
                g.DrawImage(extBMP, startX, startY);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null && extBMP.Width < (DefWidth))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startX = (int)(DefWidth / 2) - (extBMP.Width / 2);
                g.DrawImage(extBMP, startX, 0);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null && extBMP.Height < (DefHeight))
            {
                Bitmap newBMP = new Bitmap(DefWidth, DefHeight);
                newBMP.SetResolution(extBMP.HorizontalResolution, extBMP.VerticalResolution);
                Graphics g = Graphics.FromImage(newBMP);
                g.Clear(Color.White);
                int startY = (int)(DefHeight / 2) - (extBMP.Height / 2);
                g.DrawImage(extBMP, 0, startY);
                newBMP.Save(DestFileName, CodecInfo, EncParms);
                newBMP.Dispose();
                extBMP.Dispose();
            }
            else if (extBMP != null)
            {
                extBMP.Save(DestFileName, CodecInfo, EncParms);
                extBMP.Dispose();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string resizeMimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == resizeMimeType)
                    return codecs[i];
            return null;
        }

        protected void DeleteTempFile(string strsize)
        {
            try
            {
                if (strsize == "icon")
                {
                    string path = string.Empty;
                    if (ViewState["File"] != null && ViewState["File"].ToString().Trim().Length > 0)
                    {
                        path = Server.MapPath(PostTempPath + ViewState["File"].ToString());
                    }

                    File.Delete(path);
                }
                if (strsize == "audio")
                {
                    string path = string.Empty;
                    if (ViewState["AudFile"] != null && ViewState["AudFile"].ToString().Trim().Length > 0)
                    {
                        path = Server.MapPath(AudPostTempPath + ViewState["AudFile"].ToString());
                    }

                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }
        }

        protected void DeleteImage(string ImageName)
        {
            try
            {
                if (File.Exists(Server.MapPath(ImageName)))
                    File.Delete(Server.MapPath(ImageName));
            }
            catch (Exception ex)
            {
                lblMsg.Text += "<br />" + ex.Message;
                //CommonOperation.WriteLog("\r\n Error Description: " + ex.Message + "\r\n" + ex.StackTrace + " \r\n Method :->DeleteImage() \r\n Date: " + System.DateTime.Now + "\r\n");
            }
        }

    }
}