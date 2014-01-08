using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Piccm_Uploader.Core
{
    class Validity
    {
        public static Boolean CheckImage(String fileName)
        {
            Image img = Image.FromFile(fileName);
            Console.WriteLine(img.PixelFormat + " " + img.RawFormat);
            if (img.RawFormat.Equals(ImageFormat.Jpeg)
                || img.RawFormat.Equals(ImageFormat.Png)
                || img.RawFormat.Equals(ImageFormat.Bmp)
                || img.RawFormat.Equals(ImageFormat.Gif))
                return true;
            return false;
        }

        public static Boolean CheckFile(String fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists) // Does the file exist
                {
                    if (fileInfo.Length < 10480000) // If it exists is it smaller than 9.99451 MB (10485760 for 10MB)
                        return true;
                }
            }
            catch (Exception e)
            {
#if DEBUD
                Console.WriteLine(e.Message);
#endif
                return false;
            }
            return false;
        }

        internal static bool CheckURL(string s)
        {
            Uri uriResult;
            try
            {
                return Uri.TryCreate(s, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            catch
            {
                return false;
            }
        }
    }
}
