﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piccm_Uploader.History
{
    public class ImageData
    {
        public static string image_name;
        public static string image_type;
        public static int image_width;
        public static int image_height;
        public static int image_bytes;
        public static string image_id_public;
        public static string image_delete_hash;
        public static string image_date;

        internal static void Save()
        {
            SQLiteDatabase sqldb = new SQLiteDatabase();

            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("image_name", image_name);
            data.Add("image_type", image_type);
            data.Add("image_width", image_width.ToString());
            data.Add("image_height", image_height.ToString());
            data.Add("image_bytes", image_bytes.ToString());
            data.Add("image_id_public", image_id_public);
            data.Add("image_delete_hash", image_delete_hash);
            data.Add("image_date", image_date);

            try
            {
                sqldb.Insert("history", data);
            }
            catch (Exception crap)
            {
                Console.WriteLine(crap.Message);
            }
        }
    }
}