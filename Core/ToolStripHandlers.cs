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
        public static void UploadFile(object sender, EventArgs e)
        {
            // Clear the upload queue
            Program.FilesToUpload.Clear();

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
                            Program.FilesToUpload.Add(fileName);
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
                Uploadr.StartUpload();
            }
            else
                Program.checker.BuildContextMenu();
        }

        public static void UploadClipboard(object sender, EventArgs e)
        {
            Program.FilesToUpload.Clear();

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
                        Program.FilesToUpload.Add(s);
                        Uploadr.StartUpload();
                    }
                }
                else
                {
                    Program.checker.BuildContextMenu();
                    MessageBox.Show("Invaild file specified");
                }
            }
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap)) // If the data in the clipboard is a bitmap
            {
                Bitmap b = new Bitmap(Clipboard.GetImage()); // Copy the data from the clipboard, and store locally.
                MemoryStream ms = new MemoryStream(); // Open up a memory stream to save the image too, saves writing temp files
                b.Save(ms, ImageFormat.Png); // Convert the image to PNG using the memory stream as output
                Uploadr.StartUpload(ms.ToArray()); // Upload from memory stream
                ms.Close(); // And then flush the memory stream.
            }
        }
    }
}
