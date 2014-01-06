﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

using Piccm_Uploader.Windows;

namespace Piccm_Uploader.Core
{
    class ToolStripHandlers
    {
        internal static void UploadFile(object sender, EventArgs e)
        {
            // TODO Check if currently uploading
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Images (*.jpg, *.png, *.bmp, *.gif)|*.jpg; *.png; *.bmp; *.gif"; // Set the filter to only show images
            fileDialog.Multiselect = true; // Allow multiple files to be selected

            List<String> invalidFiles = new List<string>();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in fileDialog.FileNames)
                {
                    if (!Validity.CheckFile(fileName) || !Validity.CheckImage(fileName))
                        invalidFiles.Add(fileName);
                    else
                        Upload.uploadQueue.Enqueue(fileName);
                }

                if (invalidFiles.Count > 0)
                {
                    String invalidFileList = String.Empty;
                    foreach (String invalidFileName in invalidFiles)
                    {
                        invalidFileList += invalidFileName + ", ";
                    }
                    invalidFileList = invalidFileList.Remove(invalidFileList.Length - 2);
                    MessageBox.Show("The following file(s) are not valid and will not be uploaded.\n" + invalidFileList, "Invalid file(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        internal static void UploadClipboard(object sender, EventArgs e)
        {
            // TODO Check if currently uploading
            // Check if the clipboard contains text
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                // Store the text in the clipboard locally
                string s = Clipboard.GetText(TextDataFormat.Text);

                // Check if text is a HTTP or HTTPS url first to prevent mscorlib errors
                if (Validity.CheckURL(s))
                {
                    Upload.uploadQueue.Enqueue(s);
                    Console.WriteLine("URL Added " + s);
                }
                else
                {
                    // Check if the file exists
                    if (Validity.CheckFile(s))
                    {
                        // Check if the file is a valid image type
                        if (Validity.CheckImage(s))
                        {
                            Upload.uploadQueue.Enqueue(s);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invaild file specified");
                    }
                }
            }
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap)) // If the data in the clipboard is a bitmap
            {
                Upload.UploadBitmap(new Bitmap(Clipboard.GetImage())); // Upload the bitmap
            }
        }

        static Windows.History history = new Windows.History();
        static Windows.Settings settings = new Windows.Settings();
        static Windows.AboutBox about = new Windows.AboutBox();
        static DragDropFiles ddf = new DragDropFiles();
        static UrlUpload uu = new UrlUpload();
        internal static void ShowHistory(object sender, EventArgs e)
        {   
            history.Show();
            history.BringToFront();
        }

        internal static void ShowSettings(object sender, EventArgs e)
        {
            settings.Show();
            settings.BringToFront();
        }

        internal static void ShowAbout(object sender, EventArgs e)
        {
            about.Show();
            about.BringToFront();
        }

        internal static void Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        internal static void UploadDesktop(object sender, EventArgs e)
        {
            MainClass.DesktopScreenShot();
        }

        internal static void UploadArea(object sender, EventArgs e)
        {
            MainClass.CropScreenshot();
        }

        internal static void DragDrop(object sender, EventArgs e)
        {
            // TODO Check if currently uploading

            ddf.ShowDialog();
            ddf.BringToFront();
        }

        internal static void UploadUrl(object sender, EventArgs e)
        {
            uu.Show();
            uu.BringToFront();
        }
    }
}