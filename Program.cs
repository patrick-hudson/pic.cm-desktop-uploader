/*
* Copyright (c) 2013 Patrick Hudson
* 
* This file is part of Universal Chevereto Uploadr.
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Universal Chevereto Uploadr. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Universal_Chevereto_Uploadr.Misc;

namespace Universal_Chevereto_Uploadr
{
    static class Program
    {
    	//some globals
        [DllImport ("user32.dll")] public static extern IntPtr GetForegroundWindow (); 
        public static List <string> FilesToUpload;
        public static List <UploadedPhoto> History;
        public static string Url, Key;
        public static History HistoryForm;
        public static MainClass MainClassInstance;
        public static Checker checker;

        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            //here we read app's settings, configuration (api key, url) and history
            Sets.ReadSets ();
            ReadConfig ();
            ReadHistory ();
            //a strange bug's fix
            if (Sets.Bug563Fix) Sets.Bug563Fix=false;
            else
            {
                Process []p=Process.GetProcessesByName (Process.GetCurrentProcess ().ProcessName);
                if (p.Length>1) Process.GetCurrentProcess ().Kill ();
            }
            //some init
            FilesToUpload=new List <string> ();
            MainClassInstance=new MainClass ();
            MainClassInstance.Wins=new IntPtr [3];
            MainClassInstance.Wins[0]=GetForegroundWindow ();
            MainClassInstance.Wins[1]=MainClassInstance.Wins[0];
            MainClassInstance.Wins[2]=MainClassInstance.Wins[1];
            //initializing the formless notify icon and its menu
            checker=new Checker ();
            ReadHotkeysConfig();
            Application.Run ();
        }

        public static void ReadHistory ()
        {
        	/* this function reads the content of history.xml, which contains the links
        	 * to the photos you have uploaded in the past.*/
            History=new List <UploadedPhoto> ();
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\history.xml") == false)
            {
                StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\history.xml");
                sw.WriteLine ("<xml><nr-of-files>0</nr-of-files><files></files></xml>");
                sw.Close ();
            }
            XmlTextReader reader = new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\history.xml");
            int n=0;
            string ReadedNode="";
            UploadedPhoto aux=new UploadedPhoto ();
            while (reader.Read ()) 
            {
                switch (reader.NodeType) 
                {
                    case XmlNodeType.Element:
                        ReadedNode=reader.Name;
                        if (reader.Name=="file") aux=new UploadedPhoto ();
                        break;
                    case XmlNodeType.Text:
                        //parse the content of a "photo"...
                        string s=reader.Value;
                        switch (ReadedNode)
                        {
                            case "nr-of-files": n=Convert.ToInt32 (s);      break;
                            case "id":          aux.Id=Convert.ToInt32 (s); break;
                            case "local-name":  aux.LocalName=s;            break;
                            case "server-name": aux.ServerName=s;           break;
                            case "direct-link": aux.DirectLink=s;           break;
                            case "short-url":   aux.ShortUrl=s;             break;
                            case "viewer":      aux.Viewer=s;               break;
                            case "mini":        aux.Miniatura=s;            break;
                            case "from-last-upload": aux.FromLastUpload=Convert.ToBoolean (s); break;
                            case "delete-link": aux.Delete=s;				break;
                            default: break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        //...and add that "photo" to the "history"
                        if (reader.Name=="file") History.Add (aux);
                        break;
                }
            }
            reader.Close ();
        }
        
        public static void WriteHistory ()
        {
        	/* this funtion writes the content of history.xml with the content of the history list*/
            StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\imageuploader\\history.xml");
        	sw.WriteLine ("<xml><nr-of-files>"+History.Count.ToString ());
        	sw.WriteLine ("</nr-of-files><files>");
        	foreach (UploadedPhoto p in History)
        	{
        		sw.WriteLine ("<file>");
        		sw.WriteLine ("<id>"+p.Id+"</id>");
        		sw.WriteLine ("<local-name>"+p.LocalName+"</local-name>");
        		sw.WriteLine ("<server-name>"+p.ServerName+"</server-name>");
        		sw.WriteLine ("<direct-link>"+p.DirectLink+"</direct-link>");
        		sw.WriteLine ("<short-url>"+p.ShortUrl+"</short-url>");
        		sw.WriteLine ("<viewer>"+p.Viewer+"</viewer>");
        		sw.WriteLine ("<mini>"+p.Miniatura+"</mini>");
        		sw.WriteLine ("<from-last-upload>"+p.FromLastUpload.ToString ()+"</from-last-upload>");
        		sw.WriteLine ("<delete-link>"+p.Delete+"</delete-link>");
        		sw.WriteLine ("</file>");
        	}
        	sw.WriteLine ("</files></xml>");
        	sw.Close ();
        }

        public static void ApplicationRestart ()
        {
        	/*funtion which correctly restarts the app (Application.Restart () didn't worked so well here).
        	  It is called each time the upload is done*/
            Sets.Bug563Fix=true;
            Process.Start (Application.ExecutablePath);
            Process.GetCurrentProcess ().Kill ();
        }
        
        public static void ReadConfig ()
        {
        	//read config
            /*if (File.Exists (".\\config.ini")==false) -- We don't need this We're awesome
            {
            	MessageBox.Show ("config.ini file do not exist");
            	Application.Exit ();
            }
             */
            //remember to set your url to api and the api key in config.ini!
            Url = "http://pic.cm/api.php";
            Key = "thisismyapikeynooneshouldknowmyapikey";
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
