using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;

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

            bitmap.Save(pngstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Save(jpgstream, System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("PNG Size: " + (pngstream.Length / 1024));
            Console.WriteLine("JPEG Size: " + (jpgstream.Length / 1024));

            jpgstream.Close();
            pngstream.Close();

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
            // TODO Save local image
        }

        private static void SendRequest(byte[] bytes = null, String remoteUrl = null)
        {
            HttpWebRequest request = WebRequest.Create(References.URL_UPLOAD + "?") as HttpWebRequest;
            request.Timeout = 600000;
            request.Method = "POST";  // POS

            if (bytes != null)
            {
                request.ContentType = "text/xml";
                request.ContentLength = bytes.Length;

                // Here is where I need to compress the above byte array using GZipStream
                using (Stream postStream = request.GetRequestStream())
                {
                    using (var zipStream = new GZipStream(postStream, CompressionMode.Compress))
                    {
                        zipStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            XmlDocument xmlDoc = new XmlDocument();
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                reader = new StreamReader(response.GetResponseStream());
                //xmlDoc.LoadXml(reader.ReadToEnd());
                Console.WriteLine(reader.ReadToEnd());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
    }
}
