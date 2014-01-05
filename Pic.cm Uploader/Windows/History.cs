using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Piccm_Uploader.Core;
using Piccm_Uploader.History;

namespace Piccm_Uploader.Windows
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
            listView1.MultiSelect = false;
            listView1.DoubleClick += new EventHandler(OpenPictureBox);
        }

        private void History_Load(object sender, EventArgs e)
        {
            try
            {
                SQLiteDatabase sqldb = new SQLiteDatabase();
                DataTable history;
                String query = "SELECT id, image_name, image_type, image_width, image_height, image_bytes, image_id_public, image_delete_hash, image_date, image_data FROM history;";
                history = sqldb.GetDataTable(query);
                ImageList imgList = new ImageList();

                listView1.LargeImageList = imgList;
                listView1.LargeImageList.ImageSize = new Size(100, 90);

                for (int i = 0; i < history.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = history.Rows[i]["image_name"].ToString();
                    item.Name = "lvi-id:" + history.Rows[i]["id"].ToString();
                    if (history.Rows[i]["image_data"].ToString().Length > 10)
                    {
                        string data = Encoding.Default.GetString((byte[])history.Rows[i]["image_data"]);
                        byte[] bytes =Convert.FromBase64String(data);
                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                        imgList.Images.Add(image);
                    }
                    else
                    {
                        Console.WriteLine("No data for image " + history.Rows[i]["image_name"]);
                        Image tmp = LoadImage(References.URL_VIEW + history.Rows[i]["image_name"].ToString() + ".th." + history.Rows[i]["image_type"].ToString());
                        MemoryStream ms = new MemoryStream();
                        tmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("image_data", Convert.ToBase64String(ms.ToArray(), Base64FormattingOptions.None));
                        ms.Dispose();
                        sqldb.Update("history", data, String.Format("id = {0}", history.Rows[i]["id"]));
                        imgList.Images.Add(tmp);
                    }
                    item.ImageIndex = i + 1;
                    listView1.Items.Add(item);
                }

            }
            catch (Exception fail)
            {
                Console.WriteLine("Database error: {0}\n{1}", fail.Message, fail.StackTrace);
                MessageBox.Show("Unable to load history, a database error occured.");
                this.Close();
            }
        }

        private Image LoadImage(string url)
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

        public void OpenPictureBox(object sender, EventArgs e)
        {
            string itemid = listView1.SelectedItems[0].Text;
            Picture picWindow = new Picture(itemid);
            picWindow.Show();
        }
    }
}
