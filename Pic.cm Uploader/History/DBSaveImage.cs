using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Piccm_Uploader.Core;

namespace Piccm_Uploader.History
{
    public class DBSaveImage
    {
        public static void Save(String url, int imageid = 0)
        {
            SQLiteDatabase sqldb = new SQLiteDatabase();
            Image tmp = LoadImage(url);
            MemoryStream ms = new MemoryStream();
            tmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("image_data", Convert.ToBase64String(ms.ToArray(), Base64FormattingOptions.None));
            ms.Close();
                if (imageid == 0)
                {
                    sqldb.Update("history", data, "id = (SELECT MAX(id) FROM history)");
                }
                else
                {
                    sqldb.Update("history", data, String.Format("id = {0}", imageid));
                }
        }

        public static string Getbase64(String url)
        {
            Image tmp = LoadImage(url);
            MemoryStream ms = new MemoryStream();
            tmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Dictionary<string, string> data = new Dictionary<string, string>();
            string rd = Convert.ToBase64String(ms.ToArray(), Base64FormattingOptions.None);
            ms.Close();
            return rd;
        }

        private static Image LoadImage(string url)
        {
            System.Net.WebRequest request =
                System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();

            return bmp;
        }
    }
}
