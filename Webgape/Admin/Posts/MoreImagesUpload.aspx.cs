using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using WebgapeClass;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;

namespace Webgape.Admin.Posts
{
    public partial class MoreImagesUpload : System.Web.UI.Page
    {
        #region Declaration
        int lastimageid = 0;
        String ImageName = String.Empty;
        public static string PostTempPath = string.Empty;
        public static string PostIconPath = string.Empty;
        public static string PostMediumPath = string.Empty;
        public static string PostLargePath = string.Empty;
        public static string PostMicroPath = string.Empty;
        static int finHeight;
        static int finWidth;
        static Size thumbNailSizeLarge = Size.Empty;
        static Size thumbNailSizeMediam = Size.Empty;
        static Size thumbNailSizeIcon = Size.Empty;
        static Size thumbNailSizeMicro = Size.Empty;
        PostComponent postcomment = new PostComponent();
        ConfigurationComponent objConfiguration = new ConfigurationComponent();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            BindSize();

            if (!IsPostBack)
            {
                PostTempPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Temp/");
                PostIconPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Icon/");
                PostMediumPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Medium/");
                PostLargePath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Large/");
                PostMicroPath = string.Concat(AppLogic.AppConfigs("ImagePathPost"), "Micro/");

                String PostID = Convert.ToString(Request.QueryString["ID"]);
                DataSet DsPost = new DataSet();
                DsPost = postcomment.GetPostByPostId(Convert.ToInt32(PostID), 1);
                if (DsPost != null && DsPost.Tables.Count > 0 && DsPost.Tables[0].Rows.Count > 0)
                {
                    string Imagename = Convert.ToString(DsPost.Tables[0].Rows[0]["Imagename"]);
                    lblPostName.Text = Convert.ToString(DsPost.Tables[0].Rows[0]["Title"]);
                    SetMainImage(Imagename);

                    if (!string.IsNullOrEmpty(Imagename.ToString()))
                    {
                        String[] StrArr = Imagename.ToString().Split('.');
                        if (ltOldimages.Text == "")
                        {
                            Imagename = Convert.ToString(DsPost.Tables[0].Rows[0]["sku"]) + "_" + PostID.ToString();
                        }
                        else
                        {
                            Imagename = StrArr[0];
                        }
                    }
                    else
                    {
                        Imagename = Convert.ToString(DsPost.Tables[0].Rows[0]["sku"]) + "_" + PostID.ToString();
                    }

                    string SKU = Convert.ToString(DsPost.Tables[0].Rows[0]["SKU"]);
                    ViewState["ImageName"] = Imagename;
                    ViewState["PostID"] = PostID;
                    LoadImages(Convert.ToInt32(PostID));
                }
            }
        }

        private void SetMainImage(string ImageName)
        {
            if (!string.IsNullOrEmpty(ImageName))
            {
                String FinalImagename = PostIconPath + ImageName;
                StringBuilder sb = new StringBuilder();

                if (File.Exists(Server.MapPath(FinalImagename)))
                {
                    sb.Append("<img title=\"Main Image\" src=\"" + FinalImagename + "\" style=\"border:solid 1px #eeeeee;\">&nbsp;");
                }
                else
                {
                    if (!String.IsNullOrEmpty(hdnmainImageurl.Value))
                    {
                        sb.Append("<img title=\"Main Image\" src=\"" + hdnmainImageurl.Value.ToString() + "\" style=\"border:solid 1px #eeeeee;\">&nbsp;");
                    }

                }
                ltOldimages.Text = sb.ToString();
                if (sb == null || sb.ToString() == "")
                {
                    tblOldImage.Visible = false;
                }
                else
                {
                    tblOldImage.Visible = true;
                }
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ltOldimages.Text != "" || !String.IsNullOrEmpty(hdnmainImageurl.Value.ToString()))
            {
                if (ViewState["PostID"] != null && ViewState["PostID"].ToString() != "")
                {
                    LoadImages(Convert.ToInt32(ViewState["PostID"].ToString()));
                }
                if (fileUploder.FileName != "")
                {
                    bool Flag = false;
                    String Extension = String.Empty;
                    StringArrayConverter Storeconvertor = new StringArrayConverter();
                    Array StoreArray = (Array)Storeconvertor.ConvertFrom(AppLogic.AppConfigs("AllowedExtensions"));

                    if (!Directory.Exists(Server.MapPath(PostLargePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(PostLargePath));
                    }

                    for (int j = 0; j < StoreArray.Length; j++)
                    {
                        if (fileUploder.FileName.Length > 0)
                        {
                            if (Path.GetExtension(fileUploder.FileName.ToString().ToLower()) == StoreArray.GetValue(j).ToString())
                            {
                                Extension = StoreArray.GetValue(j).ToString();
                                Flag = true;
                            }
                        }
                    }
                    if (Flag)
                    {
                        if (fileUploder.FileName.Length > 0)
                        {
                            int lastimageindex = 0;
                            int Postid = 0;
                            if (ViewState["PostID"] != null && ViewState["PostID"].ToString() != "")
                            {
                                Postid = Convert.ToInt32(ViewState["PostID"].ToString());
                            }
                            if (ViewState["LastImageid"] != null && ViewState["LastImageid"].ToString() != "")
                            {
                                lastimageindex = Convert.ToInt32(ViewState["LastImageid"].ToString());
                            }
                            else
                            {
                                lastimageindex = 1;
                            }
                            if (lastimageindex < 11)
                            {

                                fileUploder.SaveAs(Server.MapPath(PostTempPath + fileUploder.FileName).ToString());
                                SaveImages(ViewState["ImageName"].ToString() + "_" + lastimageindex + Extension);
                                ImageName = ViewState["ImageName"].ToString();
                                LoadImages(Postid);
                            }
                            else
                            {
                                lblerror.Text = "You can add maximum of 10 Images per Post.";
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "Images Limit Reached", "$(document).ready( function() {jAlert('You can add maximum of 10 Images per Post.', 'Message');});", true);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "@msg", "alert('Please Upload Main Image ...');window.close();", true);
            }
        }

        private String CheckImagesExists(string ImageName)
        {
            String FinalImageName = String.Empty;
            Boolean Flag = false;
            StringArrayConverter Storeconvertor = new StringArrayConverter();
            Array StoreArray = (Array)Storeconvertor.ConvertFrom(AppLogic.AppConfigs("AllowedExtensions"));
            ImageName = PostLargePath + ImageName;

            for (int j = 0; j < StoreArray.Length; j++)
            {
                if (File.Exists(Server.MapPath(ImageName + StoreArray.GetValue(j))))
                {
                    Flag = true;
                    ImageName = ImageName + StoreArray.GetValue(j);
                    break;
                }

            }
            if (!Flag)
            {
                return "NotAvailable";
            }
            else
            {
                return ImageName;
            }

        }

        private void GetIndexForNewImage(int LastIndex, String MImageName)
        {
            Boolean First = true;
            for (int i = 1; i <= LastIndex; i++)
            {
                String OtherImageName = CheckImagesExists(MImageName + "_" + i);
                if (OtherImageName == "NotAvailable")
                {
                    if (First)
                    {
                        lastimageid = i;
                        ViewState["LastImageid"] = lastimageid;
                        First = false;
                    }
                }
            }
            if (lastimageid == 0)
            {
                lastimageid = LastIndex + 1;
                ViewState["LastImageid"] = lastimageid;
            }
        }

        private void GenerateHTMLForImages(String MImageName, Int32 PostId)
        {
            String SearchPartten = MImageName + "_*";
            System.IO.FileInfo[] array = new System.IO.DirectoryInfo(Server.MapPath(PostLargePath)).GetFiles(SearchPartten, System.IO.SearchOption.TopDirectoryOnly);

            Array.Sort(array, delegate(System.IO.FileInfo f1, System.IO.FileInfo f2)
            {
                return f1.CreationTime.CompareTo(f2.CreationTime);
            });
            string noimg = AppLogic.AppConfigs("Noofimages").ToString();
            int NoofImages = Convert.ToInt32((string.IsNullOrEmpty(noimg)) ? "10" : noimg);
            StringBuilder sb = new StringBuilder();
            if (array.Length > 0)
            {
                sb.AppendLine("<table><tbody><tr>");
                for (int i = 0; i < array.Length && i < NoofImages; i++)
                {
                    String[] SearchImageName = array[i].Name.ToString().Split('.');
                    String OtherImageName = CheckImagesExists(SearchImageName[0]);
                    if (OtherImageName != "NotAvailable")
                    {
                        //if (((i) % 9) == 0)
                        if (((i) % 5) == 0)
                        {
                            sb.AppendLine("</tr><tr>");
                        }
                        sb.AppendLine("<td id='Col_" + i + "_" + PostId + "'>");
                        sb.AppendLine("<table  width='190' border='0' align='center' cellpadding='0' cellspacing='0'><tr>");
                        sb.AppendLine("<td align='center' valign='middle' style='font-size:12px;color:#212121;font-family:Arial'  >" + (i + 1) + "</td></tr>");
                        sb.AppendLine("<tr><td  align='center' valign='middle' class='mian_bg'><table width='190' border='0' align='center' cellpadding='0' cellspacing='0'>");
                        sb.AppendLine("<tr><td align='center' valign='middle' class='big_img'><img id='Img_" + i + "_" + PostId + "' src='" + OtherImageName + "' width='120' height='120' style=\"border:solid 1px #eeeeee;\" /></td></tr>");
                        sb.AppendLine("<tr><td height='30' align='center' valign='middle'><input type='button'  id='Delete_" + i + "_" + PostId + "' onclick=\"javascript:DeleteImage('" + OtherImageName + "');\" style='top:10px;' class='btn btn-mini btn-info' value='Delete' title='Delete' /></td></tr>");
                        sb.AppendLine("</table></td></tr>");
                        sb.AppendLine("</table></td>");
                    }
                }
                sb.AppendLine("</tr></tbody></table>");
                ltMoreimages.Text = sb.ToString();
            }
            else
            {
                ltMoreimages.Text = "";
            }
        }

        private void countTotalNoOfImages(string MImageName)
        {
            String SearchPartten = MImageName + "_*";
            System.IO.FileInfo[] array = new System.IO.DirectoryInfo(Server.MapPath(PostLargePath)).GetFiles(SearchPartten, System.IO.SearchOption.TopDirectoryOnly);

            Array.Sort(array, delegate(System.IO.FileInfo f1, System.IO.FileInfo f2)
            {
                return f1.CreationTime.CompareTo(f2.CreationTime);
            });

            int NoofImages = 0;
            try
            {
                NoofImages = Convert.ToInt32(AppLogic.AppConfigs("Noofimages").ToString());
            }
            catch { NoofImages = 25; }

            if (array.Length >= NoofImages)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "@msg", "alert('Your Can Not Upload More than " + NoofImages + " Images.');", true);
            }
        }

        private void LoadImages(int PostId)
        {
            String MImageName = ViewState["ImageName"].ToString();
            if (MImageName != "")
            {
                String FinalImagename = PostLargePath + MImageName + "_";
                String SearchPartten = MImageName + "_*";
                if (!Directory.Exists(Server.MapPath(PostLargePath)))
                    Directory.CreateDirectory(Server.MapPath(PostLargePath));

                String[] Arrayofimages = Directory.GetFiles(Server.MapPath(PostLargePath), SearchPartten, System.IO.SearchOption.TopDirectoryOnly);
                StringBuilder sb = new StringBuilder();
                System.IO.FileInfo[] array = new System.IO.DirectoryInfo(Server.MapPath(PostLargePath)).GetFiles(SearchPartten, System.IO.SearchOption.TopDirectoryOnly);

                Array.Sort(array, delegate(System.IO.FileInfo f1, System.IO.FileInfo f2)
                {
                    return f1.CreationTime.CompareTo(f2.CreationTime);
                });

                GetIndexForNewImage(array.Length, MImageName);
                GenerateHTMLForImages(MImageName, PostId);
                countTotalNoOfImages(MImageName);

                try
                {
                    File.Delete(Server.MapPath(PostTempPath + fileUploder.FileName).ToString());
                }
                catch { }
            }
        }

        private void SaveImages(string FileName)
        {
            CreateImage("Medium", FileName);
            CreateImage("Small", FileName);
            CreateImage("micro", FileName);
            CreateImage("Large", FileName);
        }

        protected void CreateImage(string Size, string FileName)
        {
            try
            {
                string strFile = null;
                strFile = Server.MapPath(PostTempPath + fileUploder.FileName);
                string strFilePath = "";
                Size = Size.ToLower();
                switch (Size)
                {
                    case "large":
                        strFilePath = Server.MapPath(PostLargePath + FileName);
                        break;
                    case "medium":
                        strFilePath = Server.MapPath(PostMediumPath + FileName);
                        break;
                    case "small":
                        strFilePath = Server.MapPath(PostIconPath + FileName);
                        break;
                    case "micro":
                        strFilePath = Server.MapPath(PostMicroPath + FileName);
                        break;
                }
                ResizePhoto(strFile, Size, strFilePath);
            }
            catch { }
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
            System.Drawing.Image imgecommerce = System.Drawing.Image.FromFile(strFile);
            float resizePercent = 0;
            int resizedHeight = imgecommerce.Height;
            int resizedWidth = imgecommerce.Width;

            if (imgecommerce.Height >= FinHeight && imgecommerce.Width >= FinWidth)
            {
                float resizePercentHeight = 0;
                float resizePercentWidth = 0;
                resizePercentHeight = (FinHeight * 100) / imgecommerce.Height;
                resizePercentWidth = (FinWidth * 100) / imgecommerce.Width;
                if (resizePercentHeight < resizePercentWidth)
                {
                    resizedHeight = FinHeight;
                    resizedWidth = (int)Math.Round(resizePercentHeight * imgecommerce.Width / 100.0);
                }
                if (resizePercentHeight >= resizePercentWidth)
                {
                    resizedWidth = FinWidth;
                    resizedHeight = (int)Math.Round(resizePercentWidth * imgecommerce.Height / 100.0);
                }
            }
            else if (imgecommerce.Width >= FinWidth && imgecommerce.Height <= FinHeight)
            {
                resizedWidth = FinWidth;
                resizePercent = (FinWidth * 100) / imgecommerce.Width;
                resizedHeight = (int)Math.Round((imgecommerce.Height * resizePercent) / 100.0);
            }

            else if (imgecommerce.Width <= FinWidth && imgecommerce.Height >= FinHeight)
            {
                resizePercent = (FinHeight * 100) / imgecommerce.Height;
                resizedHeight = FinHeight;
                resizedWidth = (int)Math.Round(resizePercent * imgecommerce.Width / 100.0);
            }

            Bitmap resizedPhoto = new Bitmap(resizedWidth, resizedHeight, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(resizedPhoto);

            int destWidth = resizedWidth;
            int destHeight = resizedHeight;
            int sourceWidth = imgecommerce.Width;
            int sourceHeight = imgecommerce.Height;

            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle DestRect = new Rectangle(0, 0, destWidth, destHeight);
            Rectangle srcRect = new Rectangle(0, 0, sourceWidth, sourceHeight);
            grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            grPhoto.DrawImage(imgecommerce, DestRect, srcRect, GraphicsUnit.Pixel);

            GenerateImage(resizedPhoto, strFilePath, FinWidth, FinHeight);

            resizedPhoto.Dispose();
            grPhoto.Dispose();
            imgecommerce.Dispose();
        }


        private void GenerateImage(Bitmap extBMP, string DestFileName, int DefWidth, int DefHeight)
        {
            System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.SaveFlag;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");
            EncParm = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)600);
            EncParms.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)600);

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
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == resizeMimeType)
                    return codecs[i];
            return null;
        }

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

        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            if (hdnimageUrl.Value != "")
            {
                string StrValue = hdnimageUrl.Value.ToString();

                if (!string.IsNullOrEmpty(StrValue.ToString()))
                {
                    String[] StrArr = StrValue.ToString().Split('/');
                    int tot = StrArr.Count();
                    if (tot > 0)
                    {
                        StrValue = StrArr[tot - 1];

                        if (File.Exists(Server.MapPath(PostLargePath + StrValue)))
                        {
                            File.Delete(Server.MapPath(PostLargePath + StrValue));
                        }
                        if (File.Exists(Server.MapPath(PostMediumPath + StrValue)))
                        {
                            File.Delete(Server.MapPath(PostMediumPath + StrValue));
                        }
                        if (File.Exists(Server.MapPath(PostMicroPath + StrValue)))
                        {
                            File.Delete(Server.MapPath(PostMicroPath + StrValue));
                        }
                        if (File.Exists(Server.MapPath(PostIconPath + StrValue)))
                        {
                            File.Delete(Server.MapPath(PostIconPath + StrValue));
                        }

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "@msg", "alert('Image Deleted Successfully.');", true);
                        LoadImages(Convert.ToInt32(Request.QueryString["ID"]));
                    }
                }
            }
        }
        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    try
        //    {
        //        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        //        System.IO.StringWriter stringWriter = new System.IO.StringWriter(stringBuilder);
        //        System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
        //        base.Render(htmlWriter);
        //        string yourHtml = stringBuilder.ToString();//.Replace(stringBuilder.ToString().IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=") + ,""); // ***** Parse and Modify This *****

        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "/App_Themes/Gray", "/App_Themes/" + "Blue", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        //yourHtml = yourHtml.Replace("/App_Themes/Gray", "/App_Themes/" + Page.Theme.ToString());


        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:white;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: white;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #ffffff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#ffffff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#fff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #fff;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:white", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: white", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #ffffff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#ffffff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color:#fff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //        yourHtml = System.Text.RegularExpressions.Regex.Replace(yourHtml, "color: #fff", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);


        //        writer.Write(yourHtml);
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}