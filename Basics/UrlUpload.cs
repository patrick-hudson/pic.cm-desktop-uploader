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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NDde.Client;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Universal_Chevereto_Uploadr
{
	//this class is made for the remote upload: you can upload by copying the url to clipboard,
	//by manual imput or by opening the picture in Firefox or Opera web browsers
	//it uses NDDe library, a c# wrapper of DDe, used to get the url from the browsers
    public partial class UrlUpload : Form
    {
        string Clipboardz, Firefox, Opera;
        DdeClient Firefox_, Opera_;
        bool Is_firefox_alive, Is_opera_alive;
        bool close_mode;

        public UrlUpload ()
        {
            InitializeComponent ();
            //initialize
            close_mode=true;
            Is_firefox_alive=Is_opera_alive=false;
            Firefox_=new DdeClient ("Firefox", "WWW_GetWindowInfo");
            Opera_=new DdeClient ("Opera", "WWW_GetWindowInfo");
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
            	//@ existing, disconnect the DDe from the web browsers
                DisconnectDde ();
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
            Thread t=new Thread ((ThreadStart)delegate
            {
                ConnectDde ();
                //from Firefox
                //if the browser is not opened, the "Firefox" string will be null
                Firefox=GetURL (Firefox_, "Firefox");
                CheckForValidUrl (ref Firefox);
                //and from Opera
                Opera=GetURL (Opera_, "Opera");
                CheckForValidUrl (ref Opera);
            });
            t.Start ();
        }

        private void ConnectDde ()
        {
            try
            {
                if (Is_firefox_alive==false) 
                {
                	//check if the process exists
                    Process []prc=Process.GetProcessesByName ("firefox");
                    if (prc.Length!=0)
                    {
                    	//and connect to it
                        Firefox_.Connect ();
                        Is_firefox_alive=true;
                    }
                }
                if (Is_opera_alive==false) 
                {
                	//check if the process exists
                    Process []prc=Process.GetProcessesByName ("opera");
                    if (prc.Length!=0)
                    {
                    	//and connect to it
                        Opera_.Connect ();
                        Is_opera_alive=true;
                    }
                }
            }
            catch {}
        }

        private void DisconnectDde ()
        {
        	//disconnect from the browser
        	//it some or all browsers are not connected to the DDe, the
        	//Disconnect () function will not raise any exception
            if (Is_firefox_alive==true) Firefox_.Disconnect ();
            if (Is_opera_alive==true) Opera_.Disconnect ();
        }

        private string GetURL (DdeClient dde, string browser)
        {
            try 
            {
            	//connect to the browsers
                ConnectDde ();
                if (browser=="Firefox") if (Is_firefox_alive==false) return "{error#1}";
                else if (browser=="Opera") if (Is_opera_alive==false) return "{error#1}";
                //get the url
                string url=dde.Request ("URL", int.MaxValue);
                string[] text=url.Split (new string [] {"\",\"", "\""}, StringSplitOptions.RemoveEmptyEntries);
                return text[0];
            }
            catch
            {
                if (browser=="Firefox") Is_firefox_alive=false;
                else if (browser=="Opera") Is_opera_alive=false;
                return "{error#2}";
            }
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
