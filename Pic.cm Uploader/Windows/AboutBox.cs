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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

using Piccm_Uploader.Core;

namespace Piccm_Uploader.Windows
{
    //about
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            labelVersionData.Text = References.VERSIONNUMBER + "." + References.BUILDNUMER;
            labelBuildDateData.Text = References.BUILDDATE;
        }

        private void labelCopyRight_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pic.cm/");
        }

        private KonamiSequence sequence = new KonamiSequence();

        private void AboutBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.KeyCode))
            {
                MessageBox.Show("Scrappy is a nigger!");
                System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=DC-kQOOwngQ");
            }
        }
    }

    public class KonamiSequence
    {

        List<Keys> Keys = new List<Keys>{System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.Up, 
                                       System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.Down, 
                                       System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.Right, 
                                       System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.Right, 
                                       System.Windows.Forms.Keys.B, System.Windows.Forms.Keys.A};
        private int mPosition = -1;

        public int Position
        {
            get { return mPosition; }
            private set { mPosition = value; }
        }

        public bool IsCompletedBy(Keys key)
        {

            if (Keys[Position + 1] == key)
            {
                // move to next
                Position++;
            }
            else if (Position == 1 && key == System.Windows.Forms.Keys.Up)
            {
                // stay where we are
            }
            else if (Keys[0] == key)
            {
                // restart at 1st
                Position = 0;
            }
            else
            {
                // no match in sequence
                Position = -1;
            }

            if (Position == Keys.Count - 1)
            {
                Position = -1;
                return true;
            }

            return false;
        }
    }
}
