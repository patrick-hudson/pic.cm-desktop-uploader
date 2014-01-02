using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Piccm_Uploader.Misc;
using Piccm_Uploader.Core;

namespace Piccm_Uploader
{
    public class Checker
    {
        public static KeyboardHook desktopScreenShotKeyHook = null;
        public static KeyboardHook croppedScreenShotKeyHook = null;
        public static KeyboardHook activeWindowsScreenShotKeyHook = null;

        public void CancelTheUpload()
        {
            // TODO Allow Canceling   
        }

        public static void RegisterGlobalHotKeys()
        {
            if (desktopScreenShotKeyHook == null)
            {
                // hot key for desktop screenshot
                desktopScreenShotKeyHook = new KeyboardHook();
                desktopScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                // register the control + 2 combination as hot key.
                desktopScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control,
                    Keys.D2);
            }

            if (croppedScreenShotKeyHook == null)
            {
                // hot key for desktop screenshot
                croppedScreenShotKeyHook = new KeyboardHook();
                croppedScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.CroppedScreenshotHotKeyPressed);
                // register the control + 1 combination as hot key.
                croppedScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control,
                    Keys.D1);
            }

            if (activeWindowsScreenShotKeyHook == null)
            {
                // hot key for desktop screenshot
                activeWindowsScreenShotKeyHook = new KeyboardHook();
                activeWindowsScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.ScreenshotActiveWindowHotKeyPressed);
                // register the control + 3 combination as hot key.
                activeWindowsScreenShotKeyHook.RegisterHotKey(ModifierKeys.Control,
                    Keys.D3);
            }
        }

        private void UnRegisterGlobalHotKeys()
        {
            if (desktopScreenShotKeyHook != null)
            {
                desktopScreenShotKeyHook.Dispose();
                desktopScreenShotKeyHook = null;
            }
        }
    }
}
