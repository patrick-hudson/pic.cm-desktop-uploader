using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Piccm_Uploader.Misc
{
    class ScreenGrab
    {
        #region Exported WIN APIs
        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);
        [DllImport("GDI32.dll")]
        public static extern int CreateCompatibleDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(int hdc);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(int hObject);


        [DllImport("gdi32.dll")]
        static extern int CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("GDI32.dll")]
        public static extern int SelectObject(int hdc, int hgdiobj);
        #endregion

        public static Bitmap CaptureScreen(int x, int y, int wid, int hei)
        {
            //create DC for the entire virtual screen
            int hdcSrc = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            int hdcDest = CreateCompatibleDC(hdcSrc);
            int hBitmap = CreateCompatibleBitmap(hdcSrc, wid, hei);
            SelectObject(hdcDest, hBitmap);

            // set the destination area White - a little complicated
            Bitmap bmp = new Bitmap(wid, hei);
            Image ii = (Image)bmp;
            Graphics gf = Graphics.FromImage(ii);
            gf.Dispose();
            ii.Dispose();
            bmp.Dispose();

            //Now copy the areas from each screen on the destination hbitmap
            Screen[] screendata = Screen.AllScreens;
            int X, X1, Y, Y1;
            for (int i = 0; i < screendata.Length; i++)
            {
                if (screendata[i].Bounds.X > (x + wid) || (screendata[i].Bounds.X + screendata[i].Bounds.Width) < x || screendata[i].Bounds.Y > (y + hei) || (screendata[i].Bounds.Y + screendata[i].Bounds.Height) < y)
                {// no common area
                }
                else
                {
                    // something  common
                    if (x < screendata[i].Bounds.X) X = screendata[i].Bounds.X; else X = x;
                    if ((x + wid) > (screendata[i].Bounds.X + screendata[i].Bounds.Width)) X1 = screendata[i].Bounds.X + screendata[i].Bounds.Width; else X1 = x + wid;
                    if (y < screendata[i].Bounds.Y) Y = screendata[i].Bounds.Y; else Y = y;
                    if ((y + hei) > (screendata[i].Bounds.Y + screendata[i].Bounds.Height)) Y1 = screendata[i].Bounds.Y + screendata[i].Bounds.Height; else Y1 = y + hei;
                    // Main API that does memory data transfer
                    BitBlt(hdcDest, X - x, Y - y, X1 - X, Y1 - Y, hdcSrc, X, Y, 0x40000000 | 0x00CC0020); //SRCCOPY AND CAPTUREBLT
                }
            }

            DeleteDC(hdcSrc);
            DeleteDC(hdcDest);
            return Bitmap.FromHbitmap(new IntPtr(hBitmap));

        }
    }
}
