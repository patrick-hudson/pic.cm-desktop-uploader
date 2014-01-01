using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Piccm_Uploader.Core
{
    class ToolStripHandlers
    {
        public void UploadFile()
        {
            // Cancel and clear the upload queue
            Program.checker.CancelTheUpload();
            Program.FilesToUpload.Clear();

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Images (*.jpg, *.png, *.bmp, *.gif)|*.jpg; *.png; *.bmp; *.gif"; // Set the filter to only show images
            fileDialog.Multiselect = true; // Allow multiple files to be selected

            List<String> invalidFiles = new List<string>();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in fileDialog.FileNames)
                {
                    if (!Validity.CheckImage(fileName) || !Validity.CheckFile(fileName))
                    {
                        invalidFiles.Add(fileName);
                    }
                }

                if(invalidFiles.Count > 0)
                {
                    String invalidFileList = String.Empty;
                    foreach (String invalidFileName in invalidFiles)
                    {
                        invalidFileList += invalidFileName + ", ";
                    }
                    invalidFileList = invalidFileList.Remove(invalidFileList.Length - 1);
                    MessageBox.Show("The following file(s) are not valid and will not be uploaded.\n" + invalidFileList, "Invalid file(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                Uploadr.StartUpload();
            }
            else
                Program.checker.BuildContextMenu();
        }
    }
}
