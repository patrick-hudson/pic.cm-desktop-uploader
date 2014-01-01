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
            try
            {
                Image img = Image.FromFile(fileName);

                if (img.RawFormat == ImageFormat.Jpeg
                    || img.RawFormat == ImageFormat.Png
                    || img.RawFormat == ImageFormat.Bmp
                    || img.RawFormat == ImageFormat.Gif)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static Boolean CheckFile(String fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if(fileInfo.Exists) // Does the file exist
            {
                if (fileInfo.Length < 10480000) // If it exists is it smaller than 9.99451 MB (10485760 for 10MB)
                    return true;
            }
            return false;
        }
    }
}
