/*
* Copyright (c) 2013 Patrick Hudson
* 
* This file is part of Pic.cm Uploader
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Pic.cm Uploader If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;

namespace Piccm_Uploader.Windows
{
    public partial class DragDropFiles : Form
    {
        //I could used a Dictionary but with a list you cand access the items with number values
        private List<KeyValuePair<string, string>> Items;

        public DragDropFiles()
        {
            InitializeComponent();
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width - 5, Screen.PrimaryScreen.Bounds.Height - this.Height - 40);
            this.TopMost = false;
            this.ShowInTaskbar = true;
            this.Icon = Resources.Resource.default_large;
            Items = new List<KeyValuePair<string, string>>();
            StateButtons();
            this.FormClosing += new FormClosingEventHandler(DragDropFiles_FormClosing);
        }


        void DragDropFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Items.Count == 0)
            {
                e.Cancel = false;
            }

            else
            {
                foreach (var v in Items)
                {
                    Core.Upload.uploadQueue.Enqueue(v.Key);
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsSelectedIndex(int x)
        {
            //check if the x-th item from the list is selected
            foreach (int i in listBox1.SelectedIndices) if (i == x) return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //remove and item from the list
            while (listBox1.SelectedItems.Count > 0) RemoveItems();
            //StateButtons=Load/Refresh buttons'states (Ok and Remove)
            StateButtons();
        }

        private void RemoveItems()
        {
            //remove all the items from the list
            for (int j = 0; j < Items.Count; j++)
            {
                if (IsSelectedIndex(j))
                {
                    listBox1.Items.RemoveAt(j);
                    Items.RemoveAt(j);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count >= 1) button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void iDragEnter(object sender, DragEventArgs e)
        {
            //drag
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void StateButtons()
        {
            //refresh the buttons
            if (Items.Count >= 1)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void iDragDrop(object sender, DragEventArgs e)
        {
            //and drop
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                int someorall = 0;
                foreach (string s in filePaths)
                {
                    //check the files
                    if (CheckForValidFile(s) == false || CheckForValidImage(s) == false)
                    {
                        someorall++;
                        continue;
                    }
                    string filename = "";
                    {
                        string[] l = s.Split("\\.".ToCharArray());
                        filename = l[l.Length - 2];
                    }
                    Items.Add(new KeyValuePair<string, string>(s, filename));
                    listBox1.Items.Add(filename);
                    StateButtons();
                }
                if (someorall != 0)
                {
                    if (someorall == filePaths.Length) MessageBox.Show("All files are invalid");
                    else MessageBox.Show("Some files are invalid.");
                }
            }
        }

        private bool CheckForValidFile(string s)
        {
            if (File.Exists(s) == false)
            {
                Debug.WriteLine("File " + s + " does not exist");
                return false;
            }
            FileInfo fi = new FileInfo(s);
            if (fi.Length > 6.291e+6) return false;
            return true;
        }

        private bool CheckForValidImage(string s)
        {
            try
            {
                Image img = Image.FromFile(s);
                if (img.RawFormat.Equals(ImageFormat.Jpeg)) { }
                else if (img.RawFormat.Equals(ImageFormat.Png)) { }
                else if (img.RawFormat.Equals(ImageFormat.Bmp)) { }
                else if (img.RawFormat.Equals(ImageFormat.Gif)) { }
                else
                {
                    Debug.WriteLine("Image " + s + " - not recognised the image format");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
