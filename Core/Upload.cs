using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace Piccm_Uploader.Core
{
    class Upload
    {
        public static void UploadBitmap(Bitmap bitmap)
        {
            // Check if we need to save locally
            if(Sets.SaveScreenshots)
                SaveLocal((Image)bitmap);
        }

        public static void ProcessUpload()
        {

        }

        private static void SaveLocal(Image image)
        {

        }

        private static byte[] Compress(Stream input)
        {
            using (var compressStream = new MemoryStream())
            using (var compressor = new DeflateStream(compressStream, CompressionMode.Compress))
            {
                input.CopyTo(compressor);
                compressor.Close();
                return compressStream.ToArray();
            }
        }
    }
}
