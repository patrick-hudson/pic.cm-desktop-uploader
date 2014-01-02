using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Piccm_Uploader.Core
{
    class Notifications
    {
        public static NotifyIcon notifyIcon = new NotifyIcon();
        public static ContextMenuStrip NotifyIconMenu = new ContextMenuStrip();

        private static ToolStripMenuItem uploadFile, uploadClipboard, uploadDesktop, uploadArea, uploadActiveWindow, uploadDragDrop, uploadRemote, windowSettings, windowHistory, windowAbout, windowSettingsUpdate, exitApp;
        private static ToolStripSeparator toolStripSeparator1, toolStripSeparator2, toolStripSeparator3;

        internal static void Initialize()
        {
            uploadFile = new ToolStripMenuItem();
            uploadFile.Name = "uploadFile";
            uploadFile.Text = "Upload &file";
            uploadFile.Enabled = true;
            uploadFile.Click += new System.EventHandler(ToolStripHandlers.UploadFile);

            uploadClipboard = new ToolStripMenuItem();
            uploadClipboard.Name = "uploadClipboard";
            uploadClipboard.Text = "Upload from &clipboard";
            uploadClipboard.Enabled = true;
            uploadClipboard.Click += new System.EventHandler(ToolStripHandlers.UploadClipboard);

            uploadDesktop = new ToolStripMenuItem();
            uploadDesktop.Name = "uploadDesktop";
            uploadDesktop.Text = "Pic &desktop";
            uploadDesktop.Enabled = true;
            uploadDesktop.Click += new System.EventHandler(ToolStripHandlers.UploadDesktop);

            uploadArea = new ToolStripMenuItem();
            uploadArea.Name = "uploadArea";
            uploadArea.Text = "Pic &area";
            uploadArea.Enabled = true;
            uploadArea.Click += new System.EventHandler(ToolStripHandlers.UploadArea);

            uploadActiveWindow = new ToolStripMenuItem();
            uploadActiveWindow.Name = "uploadActiveWindow";
            uploadActiveWindow.Text = "Pic active &window";
            uploadActiveWindow.Enabled = false;
            //uploadActiveWindow.Click += new System.EventHandler(Program.MainClassInstance.uploadDesktopScreenshotToolStripMenuItem_Click);

            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator1.Name = "toolStripSeparator1";

            uploadDragDrop = new ToolStripMenuItem();
            uploadDragDrop.Name = "uploadDragDrop";
            uploadDragDrop.Text = "Drag and Dro&p";
            uploadDragDrop.Enabled = true;
            uploadDragDrop.Click += new System.EventHandler(ToolStripHandlers.DragDrop);

            uploadRemote = new ToolStripMenuItem();
            uploadRemote.Name = "uploadRemote";
            uploadRemote.Text = "Upload from &URL";
            uploadRemote.Enabled = true;
            uploadRemote.Click += new System.EventHandler(ToolStripHandlers.UploadUrl);

            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator2.Name = "toolStripSeparator2";

            windowSettings = new ToolStripMenuItem();
            windowSettings.Name = "windowSettings";
            windowSettings.Text = "&Settings";
            windowSettings.Enabled = true;
            windowSettings.Click += new System.EventHandler(ToolStripHandlers.ShowSettings); 

            windowHistory = new ToolStripMenuItem();
            windowHistory.Name = "windowHistory";
            windowHistory.Text = "&History";
            windowHistory.Enabled = true;
            windowHistory.Click += new System.EventHandler(ToolStripHandlers.ShowHistory);

            windowAbout = new ToolStripMenuItem();
            windowAbout.Name = "windowAbout";
            windowAbout.Text = "A&bout";
            windowAbout.Enabled = true;
            windowAbout.Click += new System.EventHandler(ToolStripHandlers.ShowAbout);

            windowSettingsUpdate = new ToolStripMenuItem();
            windowSettingsUpdate.Name = "windowSettingsUpdate";
            windowSettingsUpdate.Text = "Chec&k for updates";
            windowSettingsUpdate.Enabled = true;
            windowSettingsUpdate.Click += new System.EventHandler(ToolStripHandlers.CheckForUpdate);

            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator3.Name = "toolStripSeparator3";

            exitApp = new ToolStripMenuItem();
            exitApp.Name = "exitApp";
            exitApp.Text = "E&xit";
            exitApp.Enabled = true;
            exitApp.Click += new System.EventHandler(ToolStripHandlers.Close);
            exitApp.Image = Resources.Resource.default_png;


            NotifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { 
                uploadFile, 
                uploadClipboard, 
                uploadDesktop, 
                uploadArea, 
                uploadActiveWindow,
                toolStripSeparator1,
                uploadDragDrop,
                uploadRemote,
                toolStripSeparator2, 
                windowSettings, 
                windowHistory, 
                windowAbout, 
                windowSettingsUpdate,
                toolStripSeparator3, 
                exitApp
            });

            notifyIcon.Icon = Resources.Resource.default_small;
            notifyIcon.Text = "Pic.cm Desktop Client";
            notifyIcon.ContextMenuStrip = NotifyIconMenu;
            notifyIcon.Visible = true;
        }

        public static void ResetIcon()
        {
            SetIcon(0);
        }

        public static void SetIcon(References.Icon reference)
        {
            switch (reference)
            {
                case References.Icon.ICON_DEFAULT:
                    notifyIcon.Icon = Resources.Resource.default_small;
                    break;
                case References.Icon.ICON_UPLOAD:
                    notifyIcon.Icon = Resources.Resource.uploading;
                    break;
                default:
                    notifyIcon.Icon = Resources.Resource.default_small;
                    break;
            }
        }

        public static void NotifySound(References.Sound sound = References.Sound.WIN_ASTERISK){
            switch (sound)
            {
                case References.Sound.SOUND_JINGLE:
                    (new SoundPlayer(Resources.Resource.jingle)).PlaySync();
                    break;
                default:
                    SystemSounds.Asterisk.Play();
                    break;
            }
        }

        public static void NotifyUser(String Title, String Message, int DisplayTime = 1000, ToolTipIcon toolTipIcon = ToolTipIcon.None, String UriToOpen = null)
        {
            if (UriToOpen != null)
            {
                System.Diagnostics.Process.Start(UriToOpen);
            }

            notifyIcon.BalloonTipTitle = Title;
            notifyIcon.BalloonTipText = Message;
            notifyIcon.BalloonTipIcon = toolTipIcon;
            notifyIcon.ShowBalloonTip(DisplayTime);
        }
    }
}