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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Piccm_Uploader.Capture
{
    //class used to select a region from the screen in order to crop the screenshot according to that region
    public partial class CropArea : Form
    {
        public Rectangle Output;
        public CropSelector TheSelector;
        public Button UploadButton;
        public int MouseState;
        public Point OldMousePos;

        public CropArea()
        {
            InitializeComponent();
            //make the form transparent
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.BackColor = Color.Black;
            this.Opacity = 0.4;
            this.TransparencyKey = Color.Pink;
            this.KeyUp += new KeyEventHandler(CropArea_KeyUp);
            this.MouseDown += new MouseEventHandler(CropArea_MouseDown);
            this.MouseMove += new MouseEventHandler(CropArea_MouseMove);
            this.MouseUp += new MouseEventHandler(CropArea_MouseUp);
            MouseState = 0;
        }

        void CropArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseState == 2 || MouseState == 1)
            {
                this.Cursor = Cursors.Cross;
                MouseState = 0;
            }
        }

        void CropArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseState == 1 || MouseState == 2)
            {
                MouseState = 2;
                Rectangle r = new Rectangle(0, 0, 0, 0);
                if (MousePosition.X > OldMousePos.X && MousePosition.Y > OldMousePos.Y)
                    r = new Rectangle(OldMousePos.X, OldMousePos.Y, MousePosition.X - OldMousePos.X + 1, MousePosition.Y - OldMousePos.Y + 1);
                else if (MousePosition.X < OldMousePos.X && MousePosition.Y > OldMousePos.Y)
                    r = new Rectangle(MousePosition.X, OldMousePos.Y, OldMousePos.X - MousePosition.X + 1, MousePosition.Y - OldMousePos.Y + 1);
                else if (MousePosition.X > OldMousePos.X && MousePosition.Y < OldMousePos.Y)
                    r = new Rectangle(OldMousePos.X, MousePosition.Y, MousePosition.X - OldMousePos.X + 1, OldMousePos.Y - MousePosition.Y + 1);
                else if (MousePosition.X < OldMousePos.X && MousePosition.Y < OldMousePos.Y)
                    r = new Rectangle(MousePosition.X, MousePosition.Y, OldMousePos.X - MousePosition.X + 1, OldMousePos.Y - MousePosition.Y + 1);
                if (r.Width > 10 && r.Height > 10)
                {
                    this.Cursor = Cursors.Cross;
                    if (TheSelector == null)
                    {
                        TheSelector = new CropSelector();
                        TheSelector.Location = new Point(r.X, r.Y);
                        TheSelector.Size = new Size(r.Width, r.Height);
                        TheSelector.KeyUp += CropArea_KeyUp;
                        TheSelector.Parent = this;
                        TheSelector.SizeChanged += delegate
                        {
                            if (TheSelector.Width <= 15 || TheSelector.Height <= 15)
                            {
                                this.Cursor = Cursors.Cross;
                                this.Controls.Remove(TheSelector);
                                this.Controls.Remove(UploadButton);
                                TheSelector = null;
                                UploadButton = null;
                            }
                            else UploadButton.Location = new Point(TheSelector.Location.X + TheSelector.Width - 50, TheSelector.Location.Y + TheSelector.Height + 5);
                        };
                        TheSelector.LocationChanged += delegate
                        {
                            if (UploadButton != null)
                                UploadButton.Location = new Point(TheSelector.Location.X + TheSelector.Width - 50, TheSelector.Location.Y + TheSelector.Height + 5);
                        };
                        UploadButton = new Button();
                        UploadButton.FlatStyle = FlatStyle.Flat;
                        UploadButton.Text = "Upload";
                        UploadButton.BackColor = Color.White;
                        UploadButton.Size = new Size(51, 25);
                        UploadButton.Location = new Point(TheSelector.Location.X + TheSelector.Width - 50, TheSelector.Location.Y + TheSelector.Height + 5);
                        UploadButton.Click += new EventHandler(UploadButton_Click);
                        UploadButton.Parent = this;
                    }
                    else
                    {
                        if (TheSelector != null)
                        {
                            TheSelector.Location = new Point(r.X, r.Y);
                            TheSelector.Size = new Size(r.Width, r.Height);
                        }
                        if (UploadButton != null) UploadButton.Location = new Point(TheSelector.Location.X + TheSelector.Width - 50, TheSelector.Location.Y + TheSelector.Height + 5);
                    }
                }
                else
                {
                    if (TheSelector != null)
                    {
                        this.Cursor = Cursors.Cross;
                        this.Controls.Remove(TheSelector);
                        this.Controls.Remove(UploadButton);
                        TheSelector = null;
                        UploadButton = null;
                    }
                }
            }
        }

        void UploadButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Point p = this.PointToScreen(new Point(TheSelector.Location.X, TheSelector.Location.Y));
            Size sz = new Size(TheSelector.Width, TheSelector.Height);
            Output = new Rectangle(p, sz);
        }

        void CropArea_MouseDown(object sender, MouseEventArgs e)
        {
            MouseState = 1;
            OldMousePos = MousePosition;
        }

        void CropArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Program.MainClassInstance.resetScreen();
        }
    }
}
