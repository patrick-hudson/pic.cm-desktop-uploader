using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Piccm_Uploader.Core;
using Piccm_Uploader.History;

namespace Piccm_Uploader.Windows
{
    public partial class Picture : Form
    {
        private string pictureid = String.Empty;
        private DataTable imgdata = new DataTable();

        public Picture(string id)
        {
            pictureid = id;
            this.Icon = Resources.Resource.default_large;
            InitializeComponent();
        }

        private void Picture_Load(object sender, EventArgs e)
        {
            SQLiteDatabase sqldb = new SQLiteDatabase();
            String query = "SELECT id, image_name, image_type, image_width, image_height, image_bytes, image_id_public, image_delete_hash, image_date, image_data FROM history WHERE image_name = '{0}';";
            imgdata = sqldb.GetDataTable(String.Format(query, pictureid));
            String url = References.URL_VIEW + imgdata.Rows[0]["image_name"] + "." + imgdata.Rows[0]["image_type"];
            String bburl = References.URL_VIEW + imgdata.Rows[0]["image_name"] + ".th." + imgdata.Rows[0]["image_type"];

            pictureBox.Load(url);

            if (pictureBox.Image.Size.Width > pictureBox.Width || pictureBox.Image.Size.Width > pictureBox.Width)
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            else
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            labelImageUploadDateData.Text = imgdata.Rows[0]["image_date"].ToString();
            labelImageFileSizeData.Text = SizeSuffix(Convert.ToInt64(imgdata.Rows[0]["image_bytes"]));

            textBoxDBBLink.Text = "[IMG]" + url + "[/IMG]";
            textBoxDHtml.Text = "<img src=\"" + url + "\" alt=\"\" />";
            textBoxDLink.Text = url;
            textBoxDViewer.Text = References.URL_SITE + "image/" + imgdata.Rows[0]["image_id_public"];

            textBoxThumbLink.Text = bburl;
            textBoxThumbBB.Text = "[IMG]" + bburl + "[/IMG]";
            textBoxThumbHtml.Text = "<img src=\"" + bburl + "\" alt=\"\" />";

        }

        static string SizeSuffix(Int64 value)
        {
            string[] suffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n1} {1}", dValue, suffixes[i]);
        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }
    }
}
