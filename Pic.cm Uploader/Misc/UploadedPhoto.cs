using System;
using System.Collections.Generic;
using System.Text;

namespace Piccm_Uploader
{
    public class UploadedPhoto
    {
        //the instances of this class contains the basic information
        //of a hosted photo, information returned by Chevereto's
        //api when uploading a photo
        public UploadedPhoto()
        { }

        public int Id;
        public string LocalName;
        public string ServerName;
        public string DirectLink;
        public string ShortUrl;
        public string Viewer;
        public string Miniatura;
        public string Delete;
        public bool FromLastUpload;
    }
}