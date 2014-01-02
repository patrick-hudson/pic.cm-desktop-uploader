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
        public NotifyIcon notify = new NotifyIcon();
        private ContextMenu contextmenu = new ContextMenu();
        public static KeyboardHook desktopScreenShotKeyHook = null;
        public static KeyboardHook croppedScreenShotKeyHook = null;
        public static KeyboardHook activeWindowsScreenShotKeyHook = null;

        public void BuildContextMenu()
        {
            //build the main menu
            //RegisterGlobalHotKeys();
            contextmenu.MenuItems.Clear();

            contextmenu.MenuItems.Add(new MenuItem("Upload files", new EventHandler(Core.ToolStripHandlers.UploadFile)));
            contextmenu.MenuItems.Add(new MenuItem("Upload from clipboard", new EventHandler(Core.ToolStripHandlers.UploadClipboard)));
            contextmenu.MenuItems.Add(new MenuItem("Pic desktop", new EventHandler(Program.MainClassInstance.uploadDesktopScreenshotToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("Pic area", new EventHandler(Program.MainClassInstance.uploadCroppedScreenshotToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("Pic active window", new EventHandler(Program.MainClassInstance.ScreenshotActiveWindow)));
            contextmenu.MenuItems.Add(new MenuItem("-"));

            contextmenu.MenuItems.Add(new MenuItem("Drag && Drop files", new EventHandler(Program.MainClassInstance.dragDropFilesToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("Remote upload", new EventHandler(Program.MainClassInstance.UrlUpload)));
            contextmenu.MenuItems.Add(new MenuItem("-"));

            contextmenu.MenuItems.Add(new MenuItem("Settings", new EventHandler(Program.MainClassInstance.optionsToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("History", new EventHandler(Program.MainClassInstance.uploadedPhotosToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("About", new EventHandler(Program.MainClassInstance.aboutToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("Check For Updates", new EventHandler(Program.MainClassInstance.updateToolStripMenuItem_Click)));
            contextmenu.MenuItems.Add(new MenuItem("-"));

            contextmenu.MenuItems.Add(new MenuItem("Exit", new EventHandler(Menu_OnExit)));
            notify.Icon = Resources.Resource.default_small;
        }

        public void CancelTheUpload()
        {
            //if I choose to cancel...
            contextmenu.MenuItems.Clear();
            contextmenu.MenuItems.Add(new MenuItem("Cancel the upload?", delegate
            {
                Program.ApplicationRestart();
            }));
            notify.Icon = Resources.Resource.uploading;
        }

        public void ClearMenu()
        {
            contextmenu.MenuItems.Clear();
        }

        public Checker()
        {
            //here is the constructor of the class
            BuildContextMenu();
            //initialize the notify icon
            notify = new NotifyIcon();
            notify.Text = "Pic.cm";
            notify.ContextMenu = contextmenu;
            notify.Icon = Resources.Resource.default_small;
            notify.Visible = true;
        }

        void Menu_OnExit(Object sender, EventArgs e)
        {
            Program.checker.contextmenu.Dispose();
            notify.Visible = false;
            notify.Dispose();
            Application.Exit();
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
