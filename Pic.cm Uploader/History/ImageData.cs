using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piccm_Uploader.History
{
    public class ImageData
    {
        internal static void Save(string image_name, string image_type, int image_width, int image_height, int image_bytes, string image_id_public, string image_delete_hash, string image_date)
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
#if DEBUG
            Console.WriteLine("Saving image data for " + image_name);
#endif
            try
            {
                sqldb.Insert("history", data);
            }
            catch (Exception crap)
            {
                Console.WriteLine(crap.Message);
            }

            data.Clear();
        }
    }
}
