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
using Piccm_Uploader.Misc;

using Piccm_Uploader.Core;

namespace Piccm_Uploader
{
    static class Program
    {
        //some globals
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        public static MainClass MainClassInstance;
        public static Checker checker;

        [STAThread]
        static void Main()
        {
#if DEBUG
            var screens = Screen.AllScreens;
            foreach (var screen in screens)
            {
                Console.WriteLine(screen.DeviceName + ") X: " + screen.Bounds.X + ", Y: " + screen.Bounds.Y);
                Console.WriteLine(screen.DeviceName + ") Width: " + screen.Bounds.Width + ", Height: " + screen.Bounds.Height);
            }
#endif

            Update workerUpdate = new Update();
            Thread threadUpdate = new Thread(workerUpdate.InitUpdate);
            Thread threadUpload = new Thread(Upload.ProcessQueue);

            threadUpdate.Start();
            threadUpload.Start();

            Application.EnableVisualStyles();


            //Application.SetCompatibleTextRenderingDefault(false);
            //here we read app's settings, configuration (api key, url) and history
            Core.Notifications.Initialize();
            Sets.ReadSets();
            //a strange bug's fix
            if (Sets.Bug563Fix) Sets.Bug563Fix = false;
            else
            {
                Process[] p = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                if (p.Length > 1) Process.GetCurrentProcess().Kill();
            }
            //some init
            MainClassInstance = new MainClass();
            MainClassInstance.Wins = new IntPtr[3];
            MainClassInstance.Wins[0] = GetForegroundWindow();
            MainClassInstance.Wins[1] = MainClassInstance.Wins[0];
            MainClassInstance.Wins[2] = MainClassInstance.Wins[1];
            //initializing the formless notify icon and its menu
            checker = new Checker();
            ReadHotkeysConfig();
            Application.Run();
        }
        
        public static void ReadHotkeysConfig()
        {
            //read config
            string hotkeyConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\hotkeys.ini";
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\hotkeys.ini"))
            {
                Ini i = new Ini(hotkeyConfigPath);
                var cropped = i.IniRead("hotkey", "cropped");
                var desktop = i.IniRead("hotkey", "desktop");
                var window = i.IniRead("hotkey", "window");

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

                if (Checker.croppedScreenShotKeyHook != null)
                    Checker.croppedScreenShotKeyHook.Dispose();

                // hot key for desktop screenshot
                Checker.croppedScreenShotKeyHook = new KeyboardHook();
                Checker.croppedScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.CroppedScreenshotHotKeyPressed);
                // register the control + 1 combination as hot key.
                Checker.croppedScreenShotKeyHook.RegisterHotKey(modifierKey,
                    key);



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

                if (Checker.desktopScreenShotKeyHook != null)
                    Checker.desktopScreenShotKeyHook.Dispose();

                // hot key for desktop screenshot
                Checker.desktopScreenShotKeyHook = new KeyboardHook();
                Checker.desktopScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                // register the control + 1 combination as hot key.
                Checker.desktopScreenShotKeyHook.RegisterHotKey(modifierKey,
                    key);



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

                if (Checker.activeWindowsScreenShotKeyHook != null)
                    Checker.activeWindowsScreenShotKeyHook.Dispose();

                // hot key for desktop screenshot
                Checker.activeWindowsScreenShotKeyHook = new KeyboardHook();
                Checker.activeWindowsScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.ScreenshotActiveWindowHotKeyPressed);
                // register the control + 1 combination as hot key.
                Checker.activeWindowsScreenShotKeyHook.RegisterHotKey(modifierKey,
                    key);

            }
            else
            {
                Checker.RegisterGlobalHotKeys();
            }
        }
    }
}
