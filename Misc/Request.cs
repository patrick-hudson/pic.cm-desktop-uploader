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
using System.Net;
using System.Web;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Diagnostics;

namespace Piccm_Uploader
{
	//this is a helper class for making HTTP requests to the site
    public static class Request
    {
        public static bool Verify (string url)
        {
        	//function to verify if a site is up or not
        	//I used this to verify if the computer is connected to the internet, making a request
        	//to google.com (yes, if google.com is down... but usually it is not...)
            try
            {
                WebRequest webRequest=WebRequest.Create (url);  
                WebResponse webResponse=webRequest.GetResponse ();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Post (string url, string args, string file="null", byte []bytes=null, string urlupload=null)
        {
        	//the main funtion that makes requests
        	/* Arguments:
        	 * url=the url of the api.php
        	 * arg(s)=the arguments, this usually contains the "?key=<<api key>>"
        	 * The next args deals with the data you want to upload:
        	 * * Upload a local file with:
        	 * * * file [optional]=the location of the file ou want to upload
        	 * * * OR btes [optional]=the bytes contained by a file/picture created in a memory stream
        	 * * * Upload a remote file with:
        	 * * *urlupload [optional]=the url of the picture you want to upload*/
            try
            {
                HttpWebRequest req=(HttpWebRequest)WebRequest.Create (url);
                req.KeepAlive=false;
                req.ProtocolVersion=HttpVersion.Version10;
                req.Method="POST";
                if (Sets.ProxyOn)
                {
                	//set proxy
                    req.Proxy=new WebProxy (Sets.ProxyServer, Convert.ToInt32 (Sets.ProxyPort));
                }
                string arg=args;
                //baza64s is the string that contains the url or byte array (converted to base64)
                string baza64s="";
                if (file=="null"&&bytes!=null&&urlupload==null)
                {
                	//upload a byte array
                    baza64s=ToBase64 (bytes);
                    arg+="&upload=";
                }
                else if (file!="null"&&bytes==null&&urlupload==null)
                {
                	//upload a local file
                    byte[] filebytes=File.ReadAllBytes (file);
                    baza64s=ToBase64 (filebytes);
                    arg+="&upload=";
                }
                else if (file=="null"&&bytes==null&&urlupload!=null)
                {
                	//remote upload
                    arg+="&upload="+urlupload;
                    baza64s="";
                }
                else baza64s="";
                arg+=baza64s;
                //make the request...
                byte[] argb=Encoding.ASCII.GetBytes (arg);
                req.ContentType="application/x-www-form-urlencoded";
                req.ContentLength=argb.Length;
                Stream r=req.GetRequestStream ();
                r.Write (argb, 0, argb.Length);
                WebResponse response = req.GetResponse ();
                Stream data = response.GetResponseStream();
                StreamReader sReader = new StreamReader(data);
                String sResponse = sReader.ReadToEnd();
                response.Close ();
                //...and return the response
                return sResponse;
            }
            catch (Exception ee)
            {
            	//something's wrong
                DialogResult dr=MessageBox.Show ("Error data: "+ee.Message, "Error", MessageBoxButtons.AbortRetryIgnore);
                if (dr==DialogResult.Abort) 
                {
                    Program.ApplicationRestart ();
                    return "{Abort}";
                }
                else if (dr==DialogResult.Retry)  return "{Retry}";
                else if (dr==DialogResult.Ignore) return "{Ignore}";
                else return "{Unexpected}";
            }
        }

        private static string ToBase64 (byte [] s)
        {
        	//convert a byte array to a base64 string
            string k=Convert.ToBase64String (s, Base64FormattingOptions.None);
            //url encodes the string - because in the url you don't need " " instead of "%20" 
            string str=HttpUtility.UrlEncode (k);
            return str;
        }
    }
}
