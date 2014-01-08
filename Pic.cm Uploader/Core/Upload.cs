using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Piccm_Uploader;
using Piccm_Uploader.History;

namespace Piccm_Uploader.Core
{
    public static class Upload
    {

        internal static Queue<String> uploadQueue = new Queue<string>();
        internal static Queue<String> clipboardHack = new Queue<string>();
        private static WebClient uploadClient;
        private static Boolean _uploadLock = false;

        internal static void UploadBitmap(Bitmap bitmap)
        {
            byte[] data;
            MemoryStream pngstream = new MemoryStream();
            MemoryStream jpgstream = new MemoryStream();

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bitmap.Save(jpgstream, GetEncoderInfo("image/jpeg"), myEncoderParameters);
            bitmap.Save(pngstream, ImageFormat.Png);

            if (pngstream.Length > jpgstream.Length)
            {
                data = ToByteArray(jpgstream);
            }
            else
            {
                data = ToByteArray(pngstream);
            }

            Console.WriteLine("PNG Size: " + (pngstream.Length / 1024));
            Console.WriteLine("JPEG Size: " + (jpgstream.Length / 1024));

            jpgstream.Close();
            pngstream.Close();

            var t = new Thread(() => SendRequest(data, null));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            // Check if we need to save locally
            if (Properties.Settings.Default.CopyAfterUpload)
                SaveLocal((Image)bitmap);
        }

        private static void SaveLocal(Image image)
        {
            string picturePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string piccmPath = "\\Pic.cm";

            if (!Directory.Exists(picturePath + piccmPath))
            {
                Directory.CreateDirectory(picturePath + piccmPath);
            }

            /*
             * TODO Check config for save location
             * TODO Match file extention to type
             */

            string filepath = picturePath + piccmPath + "\\" + image.GetHashCode() + "-" + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." + DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".png";
            image.Save(filepath);
        }

        internal static void ProcessQueue()
        {
            while (true)
            {
                if (clipboardHack.Count > 0)
                {
                    string url = clipboardHack.Dequeue();
                    if (Properties.Settings.Default.CopyAfterUpload)
                    {
                        System.Windows.Forms.Clipboard.Clear();
                        System.Windows.Forms.Clipboard.SetText(url);
                    }

                    if (Properties.Settings.Default.SoundAfterUpload)
                    {
                        Notifications.NotifySound(References.Sound.SOUND_JINGLE);
                    }
                    clipboardHack.Clear();
                }

                if (uploadQueue.Count > 0 && !_uploadLock)
                {
                    string path = uploadQueue.Dequeue();
                    if (Validity.CheckURL(path))
                    {
                        SendRequest(null, path);
                    }
                    else
                    {
                        UploadBitmap(new Bitmap(Bitmap.FromFile(path, false)));
                    }
                }
                System.Threading.Thread.Sleep(200);
            }
        }

        private static void SendRequest(byte[] img = null, String remoteUrl = null)
        {
            _uploadLock = true;
            Notifications.SetIcon(References.Icon.ICON_UPLOAD);
            Notifications.ClickHandler(References.ClickAction.CANCEL_UPLOAD);
            uploadClient = new WebClient();
            uploadClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            uploadClient.UploadProgressChanged += new UploadProgressChangedEventHandler(uploadProgressChanged);
            uploadClient.UploadDataCompleted += new UploadDataCompletedEventHandler(uploadComplete);
            try
            {
                string response = String.Empty;
                if (img != null)
                {
                    byte[] dataBuffer = Encoding.ASCII.GetBytes("format=xml&key=" + References.APIKey + "&upload=" + ToBase64(img));
                    uploadClient.UploadDataAsync(new Uri(References.URL_UPLOAD), dataBuffer);
                }

                if (remoteUrl != null)
                {
                    byte[] dataBuffer = Encoding.ASCII.GetBytes("format=xml&key=" + References.APIKey + "&upload=" + HttpUtility.UrlEncode(remoteUrl));
                    uploadClient.UploadDataAsync(new Uri(References.URL_UPLOAD), dataBuffer);
                }
            }
            catch
            { }
        }

        private static void uploadComplete(object sender, UploadDataCompletedEventArgs e)
        {
            _uploadLock = false;
            if (!e.Cancelled && e.Result != null)
            {

#if DEBUG
                Console.WriteLine(Encoding.UTF8.GetString(e.Result));
#endif
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Encoding.UTF8.GetString(e.Result));

                XmlNodeList xmlStatus = xmlDoc.GetElementsByTagName("status_txt");

                if (xmlStatus[0].InnerText == "OK")
                {
                    XmlNodeList image_name = xmlDoc.GetElementsByTagName("image_name");
                    XmlNodeList image_type = xmlDoc.GetElementsByTagName("image_type");
                    XmlNodeList image_width = xmlDoc.GetElementsByTagName("image_width");
                    XmlNodeList image_height = xmlDoc.GetElementsByTagName("image_height");
                    XmlNodeList image_bytes = xmlDoc.GetElementsByTagName("image_bytes");
                    XmlNodeList image_id_public = xmlDoc.GetElementsByTagName("image_id_public");
                    XmlNodeList image_delete_hash = xmlDoc.GetElementsByTagName("image_delete_hash");
                    XmlNodeList image_date = xmlDoc.GetElementsByTagName("image_date");

                    ImageData.Save(image_name[0].InnerText, image_type[0].InnerText, Convert.ToInt32(image_width[0].InnerText), Convert.ToInt32(image_height[0].InnerText),
                         Convert.ToInt32(image_bytes[0].InnerText), image_id_public[0].InnerText, image_delete_hash[0].InnerText, image_date[0].InnerText,
                    DBSaveImage.Getbase64(References.URL_VIEW + image_name[0].InnerText + ".th." + image_type[0].InnerText));

                    string url = References.URL_VIEW + image_name[0].InnerText + "." + image_type[0].InnerText; ;
                    Notifications.NotifyUser("Upload Complete!", "Click here to view your image", 1000, ToolTipIcon.Info, url);

                    clipboardHack.Enqueue(url);
                }
            }

            if (e.Error != null)
            {

                if (e.Cancelled)
                {
                    Notifications.NotifyUser("Upload Canceled", "Your upload was canceled before it was completed", 1000, ToolTipIcon.Warning);
                }
                else
                {
                    Notifications.NotifyUser("Upload Failed", "An error occuring during the upload process", 1000, ToolTipIcon.Error);
                }
            }

            Program.MainClassInstance.resetScreen();

            Notifications.ResetIcon();
            Notifications.ClickHandler(References.ClickAction.NOTHING);
            Notifications.uploadPercent.Enabled = false;
            Notifications.uploadPercent.Text = "No current uploads";
        }

        private static void uploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            double uploadProgress = Math.Round(((double)e.BytesSent / e.TotalBytesToSend) * 100, 2);
            Notifications.uploadPercent.Text = "Uploading: " + uploadProgress + "%";
            if (!Notifications.uploadPercent.Enabled)
                Notifications.uploadPercent.Enabled = true;
        }

        private static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }

        private static string ToBase64(byte[] s)
        {
            string k = Convert.ToBase64String(s, Base64FormattingOptions.None); // Convert to base64
            string str = HttpUtility.UrlEncode(k); // Make sure there are no spaces " ", make sure they are %20
            return str;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        internal static void CancelUpload(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Notifications.NotifySound();
                if (MessageBox.Show("Are you sure you want to cancel the upload?", "Cancel upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    uploadClient.CancelAsync();
                }
            }
        }
    }
}
