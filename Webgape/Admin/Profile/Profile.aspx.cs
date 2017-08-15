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

namespace Webgape.Admin.Profile
{
    public partial class Profile : System.Web.UI.Page
    {
        #region Declaration
        AdminComponent admincomponent = new AdminComponent();
        CommonDAC CommonDAC = new CommonDAC();
        public static string ProfileTempPath = string.Empty;
        public static string ProfilePath = string.Empty;
        public static string ProfileAvtarPath = string.Empty;
        public static string FileName = string.Empty;
        static int finHeight;
        static int finWidth;
        DataSet dsAdmin;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                //Session["AdminID"] = 5;
                //Session["AdminName"] = "TestUser94856";

                if (!IsPostBack)
                {
                    ProfileAvtarPath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Avtar/");
                    ProfileTempPath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Temp/");
                    Filladmin();
                    Master.HeadTitle("BeIndian - Profile", "BeIndian.in - Profile, Admin Profile", "BeIndian.in - Profile of Admin");
                }

            }
            else
            {
                Response.Redirect("/Login.aspx");
            }
        }

        public void Filladmin()
        {
            if (Session["AdminID"] != null)
            {
                int AdminId = Convert.ToInt32(Session["AdminID"]);
                dsAdmin = admincomponent.GetAdminProfileByAdminId(AdminId);
                if (dsAdmin != null && dsAdmin.Tables.Count > 0 && dsAdmin.Tables[0].Rows.Count > 0)
                {
                    txtemail.Text = dsAdmin.Tables[0].Rows[0]["EmailID"].ToString().Trim();
                    txtfirstname.Text = dsAdmin.Tables[0].Rows[0]["FirstName"].ToString().Trim();
                    txtlastnames.Text = dsAdmin.Tables[0].Rows[0]["LastName"].ToString().Trim();
                    txtusername.Text = dsAdmin.Tables[0].Rows[0]["UserName"].ToString().Trim();
                    ddlavtar.SelectedValue = dsAdmin.Tables[0].Rows[0]["ImageName"].ToString().Replace(".png", "");


                    if (Convert.ToBoolean(dsAdmin.Tables[0].Rows[0]["IsRegistered"]))
                    {
                        dvcode.Visible = false;
                    }
                    else
                    {
                        dvcode.Visible = true;
                    }

                    if (Convert.ToBoolean(dsAdmin.Tables[0].Rows[0]["IsPic"]))
                    {
                        rbtavtar.SelectedIndex = 1;
                    }

                    ProfilePath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Avtar/");
                    if (!File.Exists(Server.MapPath(ProfileAvtarPath) + AdminId + ".png"))
                    {
                        ImgLarge.Src = ProfilePath + "image_not_available.jpg";
                    }
                    else
                    {
                        ImgLarge.Src = ProfilePath + AdminId + ".png";
                    }



                    if (Request.QueryString["status"] != null)
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "SelectADropdownItem('" + ddlavtar.SelectedValue + "');jAlert('Profile Updated Successfully.', 'Message','');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "SelectADropdownItem('" + ddlavtar.SelectedValue + "')", true);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string filename = fuPostIcon.FileName;
            bool IsPic = false;
            int Updated = 0;
            int UserId = Convert.ToInt32(Session["AdminID"]);
            string strImageName = Convert.ToString(UserId) + ".png";
            SaveImage(strImageName);

            if (rbtavtar.SelectedIndex == 0)
            {
                if (ddlavtar.SelectedIndex == 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please Select Your Avtar.', 'Message','');});", true);
                    return;
                }
            }
            else
            {
                if (!File.Exists(Server.MapPath(ProfileAvtarPath) + UserId + ".png"))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Please Upload image.', 'Message','');});", true);
                    return;
                }
                IsPic = true;
            }

            Updated = admincomponent.AdminProfile(txtfirstname.Text.Trim(), txtlastnames.Text.Trim(), txtusername.Text.Trim(), txtemail.Text.Trim(), UserId, ddlavtar.SelectedValue, IsPic);
            if (Updated == -1)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('User Name Already exists.', 'Message','');});", true);
                return;
            }
            else if (Updated == -2)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Email Already exists.', 'Message','');});", true);
                return;
            }
            else
            {
                Response.Redirect("profile.aspx?status=update");
            }

        }

        protected void SaveImage(string FileName)
        {
            //create icon folder 
            if (!Directory.Exists(Server.MapPath(ProfileAvtarPath)))
                Directory.CreateDirectory(Server.MapPath(ProfileAvtarPath));

            if (ImgLarge.Src.Contains(ProfileTempPath))
            {
                try
                {
                    CreateImage("Medium", FileName);
                }
                catch (Exception ex)
                {
                    lblMsg.Text += "<br />" + ex.Message;
                }
                finally
                {
                    DeleteTempFile("icon");
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
                    case "medium":
                        strFilePath = Server.MapPath(ProfileAvtarPath + FileName);
                        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToLower() == "edit")
                        {
                            if (ViewState["DelImage"] != null && ViewState["DelImage"].ToString().Trim().Length > 0)
                            {
                                DeleteImage(ProfileAvtarPath + ViewState["DelImage"].ToString());
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

        public void ResizePhoto(string strFile, string Size, string strFilePath)
        {
            switch (Size)
            {
                case "medium":
                    finHeight = 96;
                    finWidth = 96;
                    break;
            }
            if (Size == "large")
            {
                File.Copy(strFile, strFilePath, true);
            }
            else
                File.Copy(strFile, strFilePath, true);
            //ResizeImage(strFile, finWidth, finHeight, strFilePath);
        }

        public void ResizeImage(string strFile, int FinWidth, int FinHeight, string strFilePath)
        {
            System.Drawing.Image imgWebgape = System.Drawing.Image.FromFile(strFile);
            float resizePercent = 0;
            int resizedHeight = imgWebgape.Height;
            int resizedWidth = imgWebgape.Width;

            if (imgWebgape.Height >= FinHeight && imgWebgape.Width >= FinWidth)
            {
                float resizePercentHeight = 0;
                float resizePercentWidth = 0;
                resizePercentHeight = (FinHeight * 100) / imgWebgape.Height;
                resizePercentWidth = (FinWidth * 100) / imgWebgape.Width;
                if (resizePercentHeight < resizePercentWidth)
                {
                    resizedHeight = FinHeight;
                    resizedWidth = (int)Math.Round(resizePercentHeight * imgWebgape.Width / 100.0);
                }
                if (resizePercentHeight >= resizePercentWidth)
                {
                    resizedWidth = FinWidth;
                    resizedHeight = (int)Math.Round(resizePercentWidth * imgWebgape.Height / 100.0);
                }
            }
            else if (imgWebgape.Width >= FinWidth && imgWebgape.Height <= FinHeight)
            {
                resizedWidth = FinWidth;
                resizePercent = (FinWidth * 100) / imgWebgape.Width;
                resizedHeight = (int)Math.Round((imgWebgape.Height * resizePercent) / 100.0);
            }

            else if (imgWebgape.Width <= FinWidth && imgWebgape.Height >= FinHeight)
            {
                resizePercent = (FinHeight * 100) / imgWebgape.Height;
                resizedHeight = FinHeight;
                resizedWidth = (int)Math.Round(resizePercent * imgWebgape.Width / 100.0);
            }

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
                        path = Server.MapPath(ProfileTempPath + ViewState["File"].ToString());
                    }

                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                CommonDAC.ErrorLog("Post.aspx", ex.Message, ex.StackTrace);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            bool Flag = false;
            StringArrayConverter Storeconvertor = new StringArrayConverter();
            string sImageextension = AppLogic.AppConfigs("AllowedExtensions");
            Array StoreArray = (Array)Storeconvertor.ConvertFrom(sImageextension);
            ProfileTempPath = string.Concat(AppLogic.AppConfigs("ImagePathProfile"), "Temp/");
            if (!Directory.Exists(Server.MapPath(ProfileTempPath)))
                Directory.CreateDirectory(Server.MapPath(ProfileTempPath));

            for (int j = 0; j < StoreArray.Length; j++)
                if (fuPostIcon.FileName.Length > 0 && Path.GetExtension(fuPostIcon.FileName.ToString().ToLower()) == StoreArray.GetValue(j).ToString().ToLower())
                    Flag = true;

            if (Flag)
            {
                if (fuPostIcon.FileName.Length > 0)
                {
                    ViewState["File"] = fuPostIcon.FileName.ToString();
                    fuPostIcon.SaveAs(Server.MapPath(ProfileTempPath) + fuPostIcon.FileName);
                    ImgLarge.Src = ProfileTempPath + fuPostIcon.FileName;
                }
                else
                {
                    ViewState["File"] = null;
                }
            }
            else
            {
                string message = "Only " + AppLogic.AppConfigs("AllowedExtensions") + " Images are allowed";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "PDFMsg", "$(document).ready( function() {jAlert('" + message + "', 'Message','ContentPlaceHolder1_fuPostIcon');});", true);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteImage(ProfilePath + ViewState["DelImage"].ToString());
            ViewState["DelImage"] = null;
            Response.Cache.SetExpires(DateTime.Now - TimeSpan.FromDays(1));
            ImgLarge.Src = ProfilePath + "Image_not_available.jpg";
            btnDelete.Visible = false;
        }

        protected void btnvalidateemaild_Click(object sender, EventArgs e)
        {
            Int32 isValidate = 0;
            isValidate = admincomponent.IsValidate(txtemail.Text.Trim(),txtcode.Text.Trim());
            if (isValidate > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Email verified successfully.', 'Message','');});", true);
                dvcode.Visible = false;
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "$(document).ready( function() {jAlert('Code OR Email seems wrong, unable to verify your Email.', 'Message','');});", true);
                dvcode.Visible = false;
                return;
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