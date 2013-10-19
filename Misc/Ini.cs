/*
* Copyright (c) 2013 Dobrescu Andrei
* 
* This file is part of Universal Chevereto Uploadr.
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Universal Chevereto Uploadr. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Universal_Chevereto_Uploadr 
{
	//this is a helper class used to read and write to ini files
    public class Ini
    {
        public string path;

        //some interop WinApi dll imports
        [DllImport ("kernel32")] private static extern long WritePrivateProfileString (string section, string key, string val, string filePath);
        [DllImport ("kernel32")] private static extern int    GetPrivateProfileString (string section, string key, string def, StringBuilder retVal, int size, string filePath);
   
        public Ini (string INIPath)
        {
            path=INIPath;
        }
    
        public void IniWrite (string Section, string Key, string Value)
        {
            WritePrivateProfileString (Section, Key, Value, this.path);
        }
   
        public string IniRead (string Section,string Key)
        {
    	    StringBuilder sb=new StringBuilder (255);
    	    GetPrivateProfileString(Section,Key,"",sb,255, this.path);
    	    return sb.ToString ();
        }
    }
}