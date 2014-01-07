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
        private int lastid = 0;

        public History()
        {
            InitializeComponent();
            listView1.MultiSelect = false;
            listView1.DoubleClick += new EventHandler(OpenPictureBox);
        }

        private void History_Load(object sender, EventArgs e)
        {
            int histupdate = 0;
            try
            {
                SQLiteDatabase sqldb = new SQLiteDatabase();
                String query = "SELECT id, image_name, image_type, image_data FROM history;";
                DataTable history = sqldb.GetDataTable(query);
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
                        byte[] bytes = Convert.FromBase64String(data);
                        Image image;
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            image = Image.FromStream(ms);
                        }
                        imgList.Images.Add(image);
                    }
                    else
                    {
                        DBSaveImage.Save(References.URL_VIEW + item.Text + ".th." + history.Rows[i]["image_type"].ToString(), Convert.ToInt32(history.Rows[i]["id"]));
                        histupdate++;
                    }
                    item.ImageIndex = i;
                    lastid = Convert.ToInt32(history.Rows[i]["id"]);
                    listView1.Items.Add(item);
                }

            }
            catch (Exception fail)
            {
                Console.WriteLine("Database error: {0}\n{1}", fail.Message, fail.StackTrace);
                MessageBox.Show("Unable to load history, a database error occured.");
                this.Close();
            }

            if (histupdate > 0)
            {
                MessageBox.Show("Your history has been updated. Please re-open this window.");
            }
        }

        public void OpenPictureBox(object sender, EventArgs e)
        {
            string itemid = listView1.SelectedItems[0].Text;
            Picture picWindow = new Picture(itemid);
            if (ToolStripHandlers.CheckForm(picWindow))
            {
                MessageBox.Show("Unable to open, please close the current image window.", "Unable to open", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                picWindow.Show();
            }
        }

        public void refreshHistory(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            History_Load(sender, e);
        }

    }
}
