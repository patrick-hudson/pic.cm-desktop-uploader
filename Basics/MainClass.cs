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
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using Piccm_Uploader.Core;
using Piccm_Uploader.Misc;
using Piccm_Uploader.Basics;

namespace Piccm_Uploader
{
    //the main class of the program, containing on-click events to context menu's items
    public partial class MainClass
    {

        //some interop used for screenshooting a active window
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);
        public IntPtr[] Wins;

        private CropForm[] cropForms;
        public CropForm cropForm;
        public int capd = 0;

        public MainClass()
        {
            var t = new System.Windows.Forms.Timer();
            t.Interval = 100;
            t.Tick += delegate
            {
                //@ every 100 ms, get the active window
                IntPtr i = GetForegroundWindow();
                if (i == IntPtr.Zero) return;
                if (i != Wins[0])
                {
                    //I memorize 3 windows because:
                    //Wins[2] is the "active" window, the window i want to screenshot, which
                    //is no more so active, because, by selecting the notif icon and choosing
                    //an item from the menu, I activate the Windows Desktop window then the
                    //"virtual" window containing the context menu
                    Wins[2] = Wins[1];
                    //Wins[1] is this app's window
                    Wins[1] = Wins[0];
                    //Wins[0] is the Destop window
                    Wins[0] = i;
                }
            };
            t.Start();
        }

        public void uploadFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //upload from clipboard - upload if the clipboard contains the PATH to a file or a bitmap
            //if the clipboard contains a URL, see the UrlUpload class
            Program.checker.CancelTheUpload();
            ResetArrays();
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                string s = Clipboard.GetText(TextDataFormat.Text);
                if (Validity.CheckFile(s))
                {
                    if (Validity.CheckImage(s))
                    {
                        //if there is a path to an image file
                        Program.FilesToUpload.Add(s);
                        Program.checker.CancelTheUpload();
                        Uploadr.StartUpload();
                    }
                    else
                    {
                        //if there is a path, but not to an image file
                        Program.checker.BuildContextMenu();
                        MessageBox.Show("Invalid image path in url.");
                    }
                }
                else
                {
                    //if there is a text but not a path
                    Program.checker.BuildContextMenu();
                    MessageBox.Show("Unknown data in clipboard");
                }
            }
            else if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap))
            {
                //if threre is a bitmap
                Image img = Clipboard.GetImage();
                Bitmap b = new Bitmap(img);
                MemoryStream ms = new MemoryStream();
                //"burn" the image to the memory - just like saving it as a file,
                //the bitmap contains the same data if I save it to a local disk or to memory
                b.Save(ms, ImageFormat.Png);
                //and upload it as a bte array
                Uploadr.StartUpload(ms.ToArray());
                //CancelTheUpload ()=the context menu is replaced by a menu in which you can choose to cancel your upload
                Program.checker.CancelTheUpload();
            }
        }

        public void uploadDesktopScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesktopScreenShot();
        }

        public void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (capd == 0)
            {
                capd = 1;
                DesktopScreenShot();
            }
        }

        private void DesktopScreenShot()
        {
            //upload a full desktop screenshot
            Program.checker.CancelTheUpload();
            ResetArrays();
            //screenshot it...
            //Bitmap b = Screenshot();

            int xmin = 0, xmax = 0;
            int ymin = 0, ymax = 0;
            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                if (xmin > screens[i].Bounds.X)
                    xmin = screens[i].Bounds.X;

                if (ymin > screens[i].Bounds.Y)
                    ymin = screens[i].Bounds.Y;

                xmax += screens[i].Bounds.Width;
                if (ymax < screens[i].Bounds.Height) ymax = screens[i].Bounds.Height;
            }

            Image img = ScreenGrab.CaptureScreen(xmin, ymin, xmax, ymax);
            string filepath = "";
            //...save it to memory...
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            if (Sets.SaveScreenshots)
            {
                //..if the picture must be saved to the disk, do it...
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots") == false) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots");
                filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots\\Desktop " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." +
                    DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
                for (int x = 0; File.Exists(filepath); x++) filepath += x.ToString();
                filepath += ".png";
                img.Save(filepath);
                if (Validity.CheckFile(filepath))
                {
                    //...upload it if the above operation is successfull
                    Program.FilesToUpload.Add(filepath);
                    Uploadr.StartUpload();
                }
                else Uploadr.StartUpload(ms.ToArray());
                //or upload the memory stream turned into a byte array
            }

            else Uploadr.StartUpload(ms.ToArray());
            //upload as a byte array
            Program.checker.CancelTheUpload();
            img.Dispose();
        }

        public void uploadedPhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show hostory
            Program.checker.ClearMenu();
            Program.ReadHistory();
            Program.HistoryForm = new History(true);
            Program.HistoryForm.ShowDialog();
        }

        public void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show about window
            Program.checker.ClearMenu();
            new AboutBox().Show();
        }

        public void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show settings window
            Program.checker.ClearMenu();
            Settings s = new Settings();
            s.Show();
        }

        public void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show settings window
            Program.checker.ClearMenu();
            Settings s = new Settings();
            s.Show();
            s.checkForUpdates();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void ResetArrays()
        {
            Program.FilesToUpload.Clear();
        }

        public void uploadCroppedScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CropScreenshot();
        }

        public void CroppedScreenshotHotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (capd == 0)
            {
                capd = 1;
                CropScreenshot();
            }

        }

        private void CropScreenshot()
        {
            //upload a cropped screenshot:
            Program.checker.CancelTheUpload();
            ResetArrays();
            //open the cropper

            cropForms = new CropForm[Screen.AllScreens.Length];
            int i = 0;
            // MousedownScreen = -1;
            int xmin = 0, xmax = 0;
            int ymin = 0, ymax = 0;
            var screens = Screen.AllScreens;
            for (i = 0; i < screens.Length; i++)
            {
                if (xmin > screens[i].Bounds.X)
                    xmin = screens[i].Bounds.X;

                if (ymin > screens[i].Bounds.Y)
                    ymin = screens[i].Bounds.Y;

                xmax += screens[i].Bounds.Width;
                if (ymax < screens[i].Bounds.Height) ymax = screens[i].Bounds.Height;
            }
            cropForm = new CropForm(this, string.Empty, xmin, ymin, xmax, ymax, 0);
            cropForm.Show();

            //CropArea ca = new CropArea();
            //if (ca.ShowDialog() == DialogResult.OK)
            //{
            //    //screenshot
            //    Bitmap b = Screenshot();
            //    string filepath = "";
            //    if (Sets.SaveScreenshots)
            //    {
            //        if (Directory.Exists(".\\Screenshots") == false) Directory.CreateDirectory(".\\Screenshots");
            //        filepath = ".\\Screenshots\\Cropped Screenshot " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." +
            //        DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
            //        for (int x = 0; File.Exists(filepath); x++) filepath += x.ToString();
            //        filepath += ".png";
            //    }
            //    //crop it
            //    Rectangle z = ca.Output;
            //    Bitmap Output = new Bitmap(z.Width, z.Height);
            //    Graphics g = Graphics.FromImage(Output);
            //    g.DrawImage(b, new Rectangle(0, 0, Output.Width, Output.Height), ca.Output, GraphicsUnit.Pixel);
            //    //and upload it
            //    if (Sets.SaveScreenshots)
            //    {
            //        Output.Save(filepath);
            //        Program.FilesToUpload.Add(filepath);
            //        RunUploader();
            //    }
            //    else
            //    {
            //        MemoryStream ms = new MemoryStream();
            //        Output.Save(ms, ImageFormat.Png);
            //        Uploadr.StartUpload(ms.ToArray());
            //    }
            //}
        }

        public Bitmap Screenshot()
        {
            //makes a full desktop screenshot
            Bitmap b = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
                              Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return b;
        }

        public void dragDropFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opens the drag and drop tool to upload files
            //useful if ou have to upload photos from different locations
            // - just drag and drop them from Windows Explorer
            Program.checker.CancelTheUpload();
            ResetArrays();
            DragDropFiles ddf = new DragDropFiles();
            if (ddf.ShowDialog() == DialogResult.OK)
                Uploadr.StartUpload();
        }

        public void ScreenshotActiveWindow(object sender, EventArgs e)
        {
            CaptureActiveWindow();
        }
        public void ScreenshotActiveWindowHotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            CaptureActiveWindow();
        }
        private void CaptureActiveWindow()
        {
            //get the window handle
            RECT r = new RECT();
            GetWindowRect(new HandleRef(this, Wins[2]), out r);
            ResetArrays();
            //screenshot
            Bitmap b = Screenshot();
            string filepath = "";
            if (Sets.SaveScreenshots)
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots") == false) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots");
                filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots\\Active " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." +
                    DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
                for (int x = 0; File.Exists(filepath); x++) filepath += x.ToString();
                filepath += ".png";
            }
            //crop it
            Rectangle z = new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1);
            Bitmap Output = new Bitmap(z.Width, z.Height);
            Graphics g = Graphics.FromImage(Output);
            g.DrawImage(b, new Rectangle(0, 0, Output.Width, Output.Height), z, GraphicsUnit.Pixel);
            Program.checker.CancelTheUpload();
            //and upload it
            if (Sets.SaveScreenshots)
            {
                Output.Save(filepath);
                Program.FilesToUpload.Add(filepath);
                Uploadr.StartUpload();
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                Output.Save(ms, ImageFormat.Jpeg);
                Uploadr.StartUpload(ms.ToArray());
            }
        }

        public void UrlUpload(object sender, EventArgs e)
        {
            //remote upload
            Program.checker.CancelTheUpload();
            UrlUpload uu = new UrlUpload();
            if (uu.ShowDialog() == DialogResult.Cancel) Program.checker.BuildContextMenu();
        }

        public void Grabwholescreen(int sc)  // called when ALL screen capture; sc is screen number
        {
            var screens = Screen.AllScreens;
            for (int j = 0; j < Screen.AllScreens.Length; j++) { cropForms[j].Close(); cropForms[j].Dispose(); }
            var screen = ScreenGrab.CaptureScreen(screens[sc].Bounds.X, screens[sc].Bounds.Y, screens[sc].Bounds.Width, screens[sc].Bounds.Height);
            SaveCroppedScreenshot(screen);
        }

        public void Smallscreengrab(int sc, int x, int y, int x1, int y1) // grab part of screen
        {
            var screens = Screen.AllScreens;

#if DEBUG
            Console.WriteLine("Small Screen Grab:\nStart X:" + x + ", Start Y: " + y + "\nEnd X:" + x1 + ", end Y: " + y1);
#endif

            cropForm.Close();
            cropForm.Dispose();
            //resetScreen();
            int finalx, finaly, finalwidth, finalheight;
            int X1 = Math.Min(x, x1), X2 = Math.Max(x, x1), Y1 = Math.Min(y, y1), Y2 = Math.Max(y, y1);
            finalx = X1 + screens[sc].Bounds.X;
            finaly = Y1 + screens[sc].Bounds.Y;
            finalwidth = X2 - X1 + 1;
            finalheight = Y2 - Y1 + 1;
            var screen = ScreenGrab.CaptureScreen(finalx, finaly, finalwidth, finalheight);
            SaveCroppedScreenshot(screen);

        }
        public void resetScreen()
        {
            capd = 0;
        }

        private void SaveCroppedScreenshot(Image img)
        {
            string filepath = "";
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 150L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            if (Sets.SaveScreenshots)
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots") == false) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots");
                filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots\\Cropped " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." +
                    DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
                for (int x = 0; File.Exists(filepath); x++) filepath += x.ToString();
                filepath += ".png";
            }
            if (Sets.SaveScreenshots)
            {
                img.Save(filepath);
                Program.FilesToUpload.Add(filepath);
                Uploadr.StartUpload();
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, jgpEncoder, myEncoderParameters);
                Uploadr.StartUpload(ms.ToArray());
            }

            img.Dispose();
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
