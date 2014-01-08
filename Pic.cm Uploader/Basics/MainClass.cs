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
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

using Piccm_Uploader.Capture;
using Piccm_Uploader.Core;

namespace Piccm_Uploader
{
    //the main class of the program, containing on-click events to context menu's items
    public class MainClass
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

        private static CropForm[] cropForms;
        public static CropForm cropForm;
        public int capd = 0;

        public MainClass()
        {
            //var t = new System.Windows.Forms.Timer();
            //t.Interval = 100;
            //t.Tick += delegate
            //{
            //    //@ every 100 ms, get the active window
            //    IntPtr i = GetForegroundWindow();
            //    if (i == IntPtr.Zero) return;
            //    if (i != Wins[0])
            //    {
            //        //I memorize 3 windows because:
            //        //Wins[2] is the "active" window, the window i want to screenshot, which
            //        //is no more so active, because, by selecting the notif icon and choosing
            //        //an item from the menu, I activate the Windows Desktop window then the
            //        //"virtual" window containing the context menu
            //        Wins[2] = Wins[1];
            //        //Wins[1] is this app's window
            //        Wins[1] = Wins[0];
            //        //Wins[0] is the Destop window
            //        Wins[0] = i;
            //    }
            //};
            //t.Start();
        }

        public void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (capd == 0)
            {
                capd = 1;
                DesktopScreenShot();
            }
        }

        public static void DesktopScreenShot()
        {
            // TODO Check if currently uploading

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

            Upload.UploadBitmap(ScreenGrab.CaptureScreen(xmin, ymin, xmax, ymax));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public void CroppedScreenshotHotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (capd == 0)
            {
                capd = 1;
                CropScreenshot();
            }

        }

        internal static void CropScreenshot()
        {
            // TODO Check if currently uploading


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
            cropForm = new CropForm(new MainClass(), string.Empty, xmin, ymin, xmax, ymax, 0);
            cropForm.Show();
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
            // TODO Rewrite This Class
            ////get the window handle
            //RECT r = new RECT();
            //GetWindowRect(new HandleRef(this, Wins[2]), out r);
            ////screenshot
            //Bitmap b = Screenshot();
            //string filepath = "";
            //if (Sets.SaveScreenshots)
            //{
            //    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots") == false) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots");
            //    filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots\\Active " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month + "." +
            //        DateTime.Now.Year + "." + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second;
            //    for (int x = 0; File.Exists(filepath); x++) filepath += x.ToString();
            //    filepath += ".png";
            //}
            ////crop it
            //Rectangle z = new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1);
            //Bitmap Output = new Bitmap(z.Width, z.Height);
            //Graphics g = Graphics.FromImage(Output);
            //g.DrawImage(b, new Rectangle(0, 0, Output.Width, Output.Height), z, GraphicsUnit.Pixel);
            //Program.checker.CancelTheUpload();
            ////and upload it
            //if (Sets.SaveScreenshots)
            //{
            //    Output.Save(filepath);
            //    Program.FilesToUpload.Add(filepath);
            //    Uploadr.StartUpload();
            //}
            //else
            //{
            //    MemoryStream ms = new MemoryStream();
            //    Output.Save(ms, ImageFormat.Jpeg);
            //    Uploadr.StartUpload(ms.ToArray());
            //}
        }

        public void Grabwholescreen(int sc)  // called when ALL screen capture; sc is screen number
        {
            var screens = Screen.AllScreens;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                cropForms[i].Close();
                cropForms[i].Dispose();
            }
            Core.Upload.UploadBitmap(ScreenGrab.CaptureScreen(screens[sc].Bounds.X, screens[sc].Bounds.Y, screens[sc].Bounds.Width, screens[sc].Bounds.Height));
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

            Core.Upload.UploadBitmap(ScreenGrab.CaptureScreen(finalx, finaly, finalwidth, finalheight));
        }

        public void resetScreen()
        {
            capd = 0;
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