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
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

using Piccm_Uploader.Capture;
using Piccm_Uploader.Core;

namespace Piccm_Uploader
{
    static class Program
    {
        //some globals
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        public static MainClass MainClassInstance;
        public static Boolean updateFirstStart = true;

        [STAThread]
        static void Main()
        {
            if (!File.Exists(References.APPDATA + "history.db"))
                System.IO.File.WriteAllBytes(References.APPDATA + "history.db", Resources.Resource.history);

            // Start the upload thread
            Thread threadUpload = new Thread(Upload.ProcessQueue);
            threadUpload.SetApartmentState(ApartmentState.STA);
            threadUpload.Start();

            // Is this needed?
            MainClassInstance = new MainClass();
            MainClassInstance.Wins = new IntPtr[3];
            MainClassInstance.Wins[0] = GetForegroundWindow();
            MainClassInstance.Wins[1] = MainClassInstance.Wins[0];
            MainClassInstance.Wins[2] = MainClassInstance.Wins[1];
            ReadHotkeysConfig();

            // check for updates
            Update workerUpdate = new Update();
            Thread threadUpdate = new Thread(workerUpdate.InitUpdate);
            threadUpdate.Start();
            threadUpdate.Join();
            updateFirstStart = false;

            // show the notify icon in teh taskbar
            Notifications.Initialize();
            Notifications.ResetIcon();
            Notifications.ClickHandler(References.ClickAction.NOTHING);

            // start the application
            Application.ApplicationExit += new EventHandler(OnExit);
            Application.EnableVisualStyles();
            Application.Run();
        }

        private static void OnExit(object sender, EventArgs e)
        {
            Notifications.notifyIcon.Visible = false;
            Notifications.notifyIcon.Icon = null;
            Notifications.notifyIcon.Dispose();
            Environment.Exit(0);
        }

        public static void ReadHotkeysConfig()
        {
            //read config
            string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
            if (File.Exists(References.APPDATA + "hotkeys.ini"))
            {
                var cropped = Properties.Settings.Default.HotKey_Area;
                var desktop = Properties.Settings.Default.HotKey_Desktop;
                var window = Properties.Settings.Default.HotKey_ActiveWindow;

                string[] keys = cropped.Split(new char[] { '+' });
                ModifierKeys modifierKey;
                Keys key;

                if (keys == null || keys.Length < 2)
                {
                    modifierKey = ModifierKeys.Control;
                    key = Keys.D1;
                }
                else
                {
                    modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);
                }

                if (RegisterHotkeys.croppedScreenShotKeyHook != null)
                    RegisterHotkeys.croppedScreenShotKeyHook.Dispose();

                // hot key for desktop screenshot
                RegisterHotkeys.croppedScreenShotKeyHook = new KeyboardHook();
                RegisterHotkeys.croppedScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(ToolStripHandlers.UploadClipboard);
                // register the control + 1 combination as hot key.
                RegisterHotkeys.croppedScreenShotKeyHook.RegisterHotKey(modifierKey, key);



                keys = desktop.Split(new char[] { '+' });
                if (keys == null || keys.Length < 2)
                {
                    modifierKey = ModifierKeys.Control;
                    key = Keys.D2;
                }
                else
                {
                    modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);
                }

                if (RegisterHotkeys.desktopScreenShotKeyHook != null)
                    RegisterHotkeys.desktopScreenShotKeyHook.Dispose();

                // hot key for desktop screenshot
                RegisterHotkeys.desktopScreenShotKeyHook = new KeyboardHook();
                RegisterHotkeys.desktopScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                // register the control + 1 combination as hot key.
                RegisterHotkeys.desktopScreenShotKeyHook.RegisterHotKey(modifierKey, key);

                keys = window.Split(new char[] { '+' });
                if (keys == null || keys.Length < 2)
                {
                    modifierKey = ModifierKeys.Control;
                    key = Keys.D3;
                }
                else
                {
                    modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);
                }

                if (RegisterHotkeys.activeWindowsScreenShotKeyHook != null)
                    RegisterHotkeys.activeWindowsScreenShotKeyHook.Dispose();

                //// hot key for desktop screenshot
                //RegisterHotkeys.activeWindowsScreenShotKeyHook = new KeyboardHook();
                //RegisterHotkeys.activeWindowsScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.ScreenshotActiveWindowHotKeyPressed);
                //// register the control + 1 combination as hot key.
                //RegisterHotkeys.activeWindowsScreenShotKeyHook.RegisterHotKey(modifierKey, key);

            }
            else
            {
                RegisterHotkeys.Register();
            }
        }
    }
}
