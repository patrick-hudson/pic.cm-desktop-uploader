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
            String query = "SELECT id, image_name, image_type, image_width, image_height, image_bytes, image_id_public, image_delete_hash, image_date FROM history WHERE image_name = '{0}';";
            imgdata = sqldb.GetDataTable(String.Format(query, pictureid));
            String url = References.URL_VIEW + imgdata.Rows[0]["image_name"] + "." + imgdata.Rows[0]["image_type"];
            String bburl = References.URL_VIEW + imgdata.Rows[0]["image_name"] + ".th." + imgdata.Rows[0]["image_type"];
            String delete = References.URL_SITE + "delete/image/" + imgdata.Rows[0]["image_name"] + "/" + imgdata.Rows[0]["image_delete_hash"];

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
            textBoxDViewer.Text = References.URL_SITE + "image/" + imgdata.Rows[0]["image_name"];

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


        private void Delete_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you really sure you'd like to delete this image? It will be gone forever", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(References.URL_SITE + "delete-confirm/image/" + imgdata.Rows[0]["image_name"] + "/" + imgdata.Rows[0]["image_delete_hash"]);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                SQLiteDatabase sqldb = new SQLiteDatabase();
                sqldb.Delete("history", String.Format("image_name = '{0}'", pictureid));
                this.Close();

            }

        }

    }
}
