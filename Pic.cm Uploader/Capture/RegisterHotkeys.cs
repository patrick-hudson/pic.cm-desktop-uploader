using System;
using System.Windows.Forms;

using Piccm_Uploader.Core;

namespace Piccm_Uploader.Capture
{
    public class RegisterHotkeys
    {
        public static KeyboardHook desktopScreenShotKeyHook = null;
        public static KeyboardHook croppedScreenShotKeyHook = null;
        public static KeyboardHook activeWindowsScreenShotKeyHook = null;

        public static void Register()
        {
            if (desktopScreenShotKeyHook == null)
            {
                // hot key for desktop screenshot
                desktopScreenShotKeyHook = new KeyboardHook();
                desktopScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                desktopScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control, Keys.D2);
            }

            if (croppedScreenShotKeyHook == null)
            {
                // hot key for desktop screenshot
                croppedScreenShotKeyHook = new KeyboardHook();
                croppedScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.CroppedScreenshotHotKeyPressed);
                croppedScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control, Keys.D1);
            }

            if (activeWindowsScreenShotKeyHook == null)
            {
                // TODO Redo capture active window
                 activeWindowsScreenShotKeyHook = new KeyboardHook();
                 activeWindowsScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(ToolStripHandlers.UploadClipboard);
                 activeWindowsScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control, Keys.D3);
            }
        }

        private void Unregister()
        {
            if (desktopScreenShotKeyHook != null)
            {
                desktopScreenShotKeyHook.Dispose();
                desktopScreenShotKeyHook = null;
            }
        }
    }
}
