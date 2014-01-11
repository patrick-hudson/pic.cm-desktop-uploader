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
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Text;
using System.Threading;

using Piccm_Uploader;
using Piccm_Uploader.Core;
using Piccm_Uploader.Capture;

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
            this.FormClosing += Settings_FormClosing;

            if (Properties.Settings.Default.CopyAfterUpload)
            {
                checkBox1.Checked = Properties.Settings.Default.CopyAfterUpload;
            }
            if (Properties.Settings.Default.CheckForUpdates)
            {
                checkBox6.Checked = Properties.Settings.Default.CheckForUpdates;
            }

            if (Properties.Settings.Default.RunAtStartup)
            {
                checkBox2.Checked = Properties.Settings.Default.RunAtStartup;
            }
            checkBox7.Checked = Properties.Settings.Default.ProxyEnabled;
            label1.Enabled = Properties.Settings.Default.ProxyEnabled;
            label2.Enabled = Properties.Settings.Default.ProxyEnabled;
            textBox1.Enabled = Properties.Settings.Default.ProxyEnabled;
            numericUpDown1.Enabled = Properties.Settings.Default.ProxyEnabled;
            textBox1.Text = Properties.Settings.Default.ProxyAddress;
            if (Properties.Settings.Default.SaveLocal)
            {
                checkBox3.Checked = Properties.Settings.Default.SaveLocal;
            }
            if (Properties.Settings.Default.SoundAfterUpload)
            {
                checkBox4.Checked = Properties.Settings.Default.SoundAfterUpload;
            }

            try
            {
               // numericUpDown1.Value = Convert.ToInt32(Properties.Settings.Default.ProxyPort);
            }
            catch
            {
               // numericUpDown1.Value = 0;
            }

            string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
            if (File.Exists(References.APPDATA + "hotkeys.ini"))
            {
                var cropped = Properties.Settings.Default.HotKey_Area;
                var desktop = Properties.Settings.Default.HotKey_Desktop;
                var window = Properties.Settings.Default.HotKey_ActiveWindow;

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

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
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
            Properties.Settings.Default.CheckForUpdates = checkBox6.Checked;
            if (checkBox6.Checked)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CopyAfterUpload = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //run @ startup
            Properties.Settings.Default.RunAtStartup = checkBox2.Checked;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            try
            {
                if (Properties.Settings.Default.RunAtStartup) rk.SetValue("Pic.cm Desktop Uploader", Application.ExecutablePath.ToString());
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
            Properties.Settings.Default.SoundAfterUpload = checkBox4.Checked;
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProxyEnabled = checkBox7.Checked;
            label1.Enabled = Properties.Settings.Default.ProxyEnabled;
            label2.Enabled = Properties.Settings.Default.ProxyEnabled;
            textBox1.Enabled = Properties.Settings.Default.ProxyEnabled;
            numericUpDown1.Enabled = Properties.Settings.Default.ProxyEnabled;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProxyAddress = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProxyPort = Convert.ToInt16(numericUpDown1.Value.ToString());
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocal = checkBox3.Checked;
        }

        private void btnCoppedScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            //  The key event should not be sent to the underlying control.
            e.SuppressKeyPress = true;

            // Check whether the modifier keys are pressed.
            if (e.Modifiers != Keys.None)
            {
                Keys key = Keys.None;
                ModifierKeys modifiers = KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    RegisterHotkeys.croppedScreenShotKeyHook.Dispose();
                    RegisterHotkeys.croppedScreenShotKeyHook = new KeyboardHook();
                    RegisterHotkeys.croppedScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.CroppedScreenshotHotKeyPressed);
                    // register the control + 1 combination as hot key.
                    RegisterHotkeys.croppedScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Properties.Settings.Default.HotKey_Area = string.Format("{0}+{1}", modifiers, key);

                    btnCoppedScreenshot.Text = string.Format("{0}+{1}", modifiers, key);
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
                ModifierKeys modifiers = KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    //RegisterHotkeys.activeWindowsScreenShotKeyHook.Dispose();
                    //RegisterHotkeys.activeWindowsScreenShotKeyHook = new KeyboardHook();
                    //RegisterHotkeys.activeWindowsScreenShotKeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.ScreenshotActiveWindowHotKeyPressed);

                    // register the control + 1 combination as hot key.
                    RegisterHotkeys.activeWindowsScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Properties.Settings.Default.HotKey_ActiveWindow = string.Format("{0}+{1}", modifiers, key);

                    btnUploadWindow.Text = string.Format("{0}+{1}", modifiers, key);
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
                ModifierKeys modifiers = KeyboardHook.GetModifiers(e.KeyData, out key);

                // The pressed key is valid.
                if (key != Keys.None)
                {
                    RegisterHotkeys.desktopScreenShotKeyHook.Dispose();
                    RegisterHotkeys.desktopScreenShotKeyHook = new KeyboardHook();
                    RegisterHotkeys.desktopScreenShotKeyHook.KeyPressed +=
                    new EventHandler<KeyPressedEventArgs>(Program.MainClassInstance.hook_KeyPressed);
                    // register the control + 1 combination as hot key.
                    RegisterHotkeys.desktopScreenShotKeyHook.RegisterHotKey(modifiers,
                        key);

                    string hotkeyConfigPath = References.APPDATA + "hotkeys.ini";
                    if (!File.Exists(References.APPDATA + "hotkeys.ini"))
                    {
                        if (!Directory.Exists(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty)))
                            Directory.CreateDirectory(hotkeyConfigPath.Replace("hotkeys.ini", string.Empty));
                        var file = File.Create(hotkeyConfigPath);
                        file.Close();
                    }

                    Properties.Settings.Default.HotKey_Desktop = string.Format("{0}+{1}", modifiers, key);

                    btnUploadDesktop.Text = string.Format("{0}+{1}", modifiers, key);
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

        private void ButtonCheckForUpdate_Click(object sender, System.EventArgs e)
        {
            Update workerUpdate = new Update();
            Thread threadUpdate = new Thread(workerUpdate.InitUpdate);
            threadUpdate.Start();
        }
    }
}
