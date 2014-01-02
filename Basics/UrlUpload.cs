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
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Piccm_Uploader
{
	//this class is made for the remote upload: you can upload by copying the url to clipboard,
	//by manual imput or by opening the picture in Firefox or Opera web browsers
	//it uses NDDe library, a c# wrapper of DDe, used to get the url from the browsers
    public partial class UrlUpload : Form
    {
        string Clipboardz, Firefox, Opera;
        bool Is_firefox_alive, Is_opera_alive;
        bool close_mode;

        public UrlUpload ()
        {
            InitializeComponent ();
            //initialize
            close_mode=true;
            Is_firefox_alive=Is_opera_alive=false;
            var t=new System.Windows.Forms.Timer ();
            t.Interval=500;
            GetInfo ();
            t.Tick+=delegate 
            {
            	//check if a url is in Firefox, Opera or clipboard
            	GetInfo ();
                if (radioButton1.Checked) this.button1.Enabled=true;
                if (radioButton3.Checked)
                {
                	//in Firefox
                    if (Firefox.StartsWith ("{error")) this.button1.Enabled=false;
                    else this.button1.Enabled=true;
                }
                if (radioButton4.Checked)
                {
                	//in Opera
                    if (Opera.StartsWith ("{error")) this.button1.Enabled=false;
                    else this.button1.Enabled=true;
                }
                if (radioButton2.Checked) 
                {
                	//on clipboard
                    if (Clipboardz.StartsWith ("{error")) this.button1.Enabled=false;
                    else this.button1.Enabled=true;
                }
            };
            t.Start ();
            this.FormClosing+=delegate 
            {
                if (close_mode) this.DialogResult=DialogResult.Cancel;
                else this.DialogResult=DialogResult.OK;
            };
        }

        #region Get URLs from browsers & clipboard
        private void GetInfo ()
        {
        	//this function connects many other functions from this
        	//region to get the required information
        	//from clipboard:
            Clipboardz=GetClipboard ();
            CheckForValidUrl (ref Clipboardz);
        }

        private string GetClipboard ()
        {
        	//get the url from clipboard
            try
            {
                if (Clipboard.GetDataObject ().GetDataPresent (DataFormats.Text))
                    return Clipboard.GetText (TextDataFormat.Text);
                else return "{error#}";
            }
            catch 
            {
                return "{error#}";
            }
        }
        #endregion

        private void CheckForValidUrl (ref string url)
        {
        	//this function checks if the possible url is real a url, and,
			//more important, if the url contains a picture
            if (url.StartsWith ("{error")) return;
            if (url.StartsWith ("http://")||url.StartsWith ("ftp://")||url.StartsWith ("https://")) {}
            else url="{error#3}";
            if (url.EndsWith (".jpg")||url.EndsWith (".png")||url.EndsWith (".gif")||url.EndsWith (".bmp")) {}
            else url="{error#4}";
            try
            {
                new Uri (url);
            }
            catch 
            {
                url="{error#5}";
            }
        }

        private void button2_Click (object sender, EventArgs e)
        {
        	//@ Cancel pressed
            close_mode=true;
            this.Close ();
        }

        private void button1_Click (object sender, EventArgs e)
        {
        	//@ OK pressed: get the data...
            string The_url="";
            if (radioButton1.Checked) The_url=textBox1.Text;
            else if (radioButton2.Checked) The_url=Clipboardz;
            else if (radioButton3.Checked) The_url=Firefox;
            else The_url=Opera;
            CheckForValidUrl (ref The_url);
            if (The_url.StartsWith ("{error")) 
            {
                MessageBox.Show ("Invalid url.");
                close_mode=true;
                return;
            }
            close_mode=false;
            this.Close ();
            //...and then start uploading
            Uploadr.StartUpload (null, The_url);
        }
    }
}
