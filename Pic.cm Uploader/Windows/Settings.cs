/*
* Copyright (c) 2013 Patrick Hudson
* 
* This file is part of Pic.cm Uploader
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Pic.cm Uploader If not, see http://www.gnu.org/licenses/.
 * test
 * 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;
using Piccm_Uploader.Misc;
using System.IO;

using Piccm_Uploader;
using Piccm_Uploader.Core;

namespace Piccm_Uploader.Windows
{
    //class dealing with app's settings
    public partial class Settings : Form
    {
        //public static event EventHandler AutomaticUpdaterOnUpdateAvailable;
        public Settings()
        {
            InitializeComponent();
            this.ShowInTaskbar = true;
            this.Icon = Resources.Resource.default_large;

            if (Sets.CopyAfterUpload)
            {
                checkBox1.Checked = Sets.CopyAfterUpload;
            }
            if (Sets.AutoUpdateCheck)
            {
                checkBox6.Checked = Sets.AutoUpdateCheck;
            }

            if (Sets.StartOnStartup)
            {
                checkBox2.Checked = Sets.StartOnStartup;
            }
            checkBox7.Checked = Sets.ProxyOn;
            label1.Enabled = Sets.ProxyOn;
            label2.Enabled = Sets.ProxyOn;
            textBox1.Enabled = Sets.ProxyOn;
            numericUpDown1.Enabled = Sets.ProxyOn;
            textBox1.Text = Sets.ProxyServer;
            if (Sets.SaveScreenshots)
            {
                checkBox3.Checked = Sets.SaveScreenshots;
            }
            if (Sets.Sound)
            {
                checkBox4.Checked = Sets.Sound;
            }

            try
            {
                numericUpDown1.Value = Convert.ToInt32(Sets.ProxyPort);
            }
            catch
            {
                numericUpDown1.Value = 0;
            }

            string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
            if (File.Exists(References.APPDATA + "hotkeys.ini"))
            {
                Ini i = new Ini(hotkeyConfigPath);
                var cropped = i.IniRead("hotkey", "cropped");
                var desktop = i.IniRead("hotkey", "desktop");
                var window = i.IniRead("hotkey", "window");

                string[] keys = cropped.Split(new char[] { '+' });
                if (keys != null && keys.Length > 1)
                {
                    var modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    var key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);
                    btnCoppedScreenshot.Text = string.Format("{0}+{1}",
                       modifierKey, key);
                }

                keys = desktop.Split(new char[] { '+' });
                if (keys != null && keys.Length > 1)
                {
                    var modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    var key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);

                    btnUploadDesktop.Text = string.Format("{0}+{1}",
                       modifierKey, key);

                }

                keys = window.Split(new char[] { '+' });
                if (keys != null && keys.Length > 1)
                {
                    var modifierKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), keys[0], true);
                    var key = (Keys)Enum.Parse(typeof(Keys), keys[1], true);

                    btnUploadWindow.Text = string.Format("{0}+{1}",
                      modifierKey, key);

                }
            }
        }

        //event handler for auto updating
        private void AutomaticUpdaterOnUpdateAvailable(object sender, EventArgs eventArgs)
        {

            DialogResult dialogResult = MessageBox.Show("New Update! Would you like to install now?", "Update?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // TODO Alert user to update
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Sets.AutoUpdateCheck = checkBox6.Checked;
            if (checkBox6.Checked)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Sets.CopyAfterUpload = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //run @ startup
            Sets.StartOnStartup = checkBox2.Checked;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            try
            {
                if (Sets.StartOnStartup) rk.SetValue("Pic.cm Desktop Uploader", Application.ExecutablePath.ToString());
                else rk.DeleteValue("Pic.cm Desktop Uploader", true);
            }
            catch
            {
                MessageBox.Show("Unexpected error.");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Sets.Sound = checkBox4.Checked;
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Sets.ProxyOn = checkBox7.Checked;
            label1.Enabled = Sets.ProxyOn;
            label2.Enabled = Sets.ProxyOn;
            textBox1.Enabled = Sets.ProxyOn;
            numericUpDown1.Enabled = Sets.ProxyOn;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Sets.ProxyServer = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Sets.ProxyPort = numericUpDown1.Value.ToString();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            Sets.SaveScreenshots = checkBox3.Checked;
        }

        private void btnCoppedScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            //  The key event should not be sent to the underlying control.
            e.SuppressKeyPress = true;

            // Check whether the modifier keys are pressed.
            if (e.Modifiers != Keys.None)
            {
                Keys key = Keys.None;
                Piccm_Uploader.Misc.ModifierKeys modifiers = Piccm_Uploader.Misc.KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    Checker.croppedScreenShotKeyHook.Dispose();
                    Checker.croppedScreenShotKeyHook = new Misc.KeyboardHook();
                    Checker.croppedScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.CroppedScreenshotHotKeyPressed);
                    // register the control + 1 combination as hot key.
                    Checker.croppedScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Ini i = new Ini(hotkeyConfigPath);
                    i.IniWrite("hotkey", "cropped", string.Format("{0}+{1}", modifiers, key));

                    btnCoppedScreenshot.Text = string.Format("{0}+{1}",
                       modifiers, key);
                }
            }
        }

        private void btnUploadWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //  The key event should not be sent to the underlying control.
            e.SuppressKeyPress = true;

            // Check whether the modifier keys are pressed.
            if (e.Modifiers != Keys.None)
            {
                Keys key = Keys.None;
                Piccm_Uploader.Misc.ModifierKeys modifiers = Piccm_Uploader.Misc.KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    Checker.activeWindowsScreenShotKeyHook.Dispose();
                    Checker.activeWindowsScreenShotKeyHook = new Misc.KeyboardHook();
                    Checker.activeWindowsScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.ScreenshotActiveWindowHotKeyPressed);
                    // register the control + 1 combination as hot key.
                    Checker.activeWindowsScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Ini i = new Ini(hotkeyConfigPath);
                    i.IniWrite("hotkey", "window", string.Format("{0}+{1}", modifiers, key));

                    btnUploadWindow.Text = string.Format("{0}+{1}",
                       modifiers, key);
                }
            }
        }

        private void btnUploadDesktop_KeyDown(object sender, KeyEventArgs e)
        {
            //  The key event should not be sent to the underlying control.
            e.SuppressKeyPress = true;

            // Check whether the modifier keys are pressed.
            if (e.Modifiers != Keys.None)
            {
                Keys key = Keys.None;
                Piccm_Uploader.Misc.ModifierKeys modifiers = Piccm_Uploader.Misc.KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    Checker.desktopScreenShotKeyHook.Dispose();
                    Checker.desktopScreenShotKeyHook = new Misc.KeyboardHook();
                    Checker.desktopScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                    // register the control + 1 combination as hot key.
                    Checker.desktopScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Ini i = new Ini(hotkeyConfigPath);
                    i.IniWrite("hotkey", "desktop", string.Format("{0}+{1}", modifiers, key));

                    btnUploadDesktop.Text = string.Format("{0}+{1}",
                       modifiers, key);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots") == true)
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Pic.cm\\Screenshots";
                prc.Start();
            }
            else
            {
                MessageBox.Show("You don't currently have any saved images.", "No Images");
            }

        }
        public void checkForUpdates()
        {
        }

        private void ButtonCheckForUpdate_Click(object sender, System.EventArgs e)
        {
            checkForUpdates();
        }
    }
}
