using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Text;
using System.Xml;

using Piccm_Uploader;
using Piccm_Uploader.History;
using System.Windows.Forms;

namespace Piccm_Uploader.Core
{
    public static class Upload
    {

        public static Queue<String> uploadQueue = new Queue<string>();

        public static void UploadBitmap(Bitmap bitmap)
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

            SendRequest(data);

            // Check if we need to save locally
            if (Sets.SaveScreenshots)
                SaveLocal((Image)bitmap);
        }

        public static void UploadURL(String url)
        {
            SendRequest(null, url);
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

        private static void SendRequest(byte[] img = null, String remoteUrl = null)
        {
            Notifications.SetIcon(References.Icon.ICON_UPLOAD);
            HttpWebRequest request = WebRequest.Create(References.URL_UPLOAD) as HttpWebRequest;
            request.Timeout = 600000; // Nice long timeout, should upload most files
            request.Method = "POST";

            if (img != null)
            {
                try
                {
                    byte[] dataBuffer = Encoding.ASCII.GetBytes("format=xml&key=" + References.APIKey + "&upload=" + ToBase64(img));
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = dataBuffer.Length;

                    // Here is where I need to compress the above byte array using GZipStream
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(dataBuffer, 0, dataBuffer.Length);
                    //using (GZipStream zipStream = new GZipStream(postStream, CompressionMode.Compress))
                    //{
                    //    zipStream.Write(dataBuffer, 0, dataBuffer.Length);
                    //}
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }


            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                HttpWebResponse response = null;
                StreamReader reader = null;

                response = request.GetResponse() as HttpWebResponse;
                reader = new StreamReader(response.GetResponseStream());
                xmlDoc.LoadXml(reader.ReadToEnd());

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

                    PreviousUpload.image_name = image_name[0].InnerText;
                    PreviousUpload.image_type = image_type[0].InnerText;

                    string url = References.URL_VIEW + PreviousUpload.image_name + "." + PreviousUpload.image_type;

                    Notifications.ResetIcon();
                    Notifications.NotifyUser("Upload Complete!", "Click here to view your image", 1000, ToolTipIcon.Info, url);

                    if (Sets.CopyAfterUpload)
                    {
                        Clipboard.SetText(url);
                    }

                    if (Sets.Sound)
                    {
                        Notifications.NotifySound(References.Sound.SOUND_JINGLE);
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG 
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
#endif
                Notifications.ResetIcon();
                Notifications.NotifyUser("Upload Failed", "An error occuring during the upload process", 1000, ToolTipIcon.Error);
            }
            Program.MainClassInstance.resetScreen();
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
    }
}
