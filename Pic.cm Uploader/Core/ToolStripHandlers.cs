using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;


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

                if(invalidFiles.Count > 0)
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

                // Check if the text is actually a file path
                if (Validity.CheckFile(s))
                {
                    // If it is a path, check if it is an allowed image type
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
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap)) // If the data in the clipboard is a bitmap
            {
                Upload.UploadBitmap(new Bitmap(Clipboard.GetImage()));
            }
        }

        internal static void ShowHistory(object sender, EventArgs e)
        {
            Windows.History history = new Windows.History(true);
            history.Show();
        }

        internal static void ShowSettings(object sender, EventArgs e)
        {
            Windows.Settings settings = new Windows.Settings();
            settings.Show();
        }

        internal static void CheckForUpdate(object sender, EventArgs e)
        {
            Windows.Settings settings = new Windows.Settings();
            settings.checkForUpdates();
            settings.Close();
        }

        internal static void ShowAbout(object sender, EventArgs e)
        {
            new Windows.AboutBox().Show();
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

            DragDropFiles ddf = new DragDropFiles();
        }

        internal static void UploadUrl(object sender, EventArgs e)
        {
            UrlUpload uu = new UrlUpload();
            uu.Show();
        }
    }
}
