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

namespace Piccm_Uploader
{
    public class UploadedPhoto
    {
    	//the instances of this class contains the basic information
    	//of a hosted photo, information returned by Chevereto's
    	//api when uploading a photo
        public UploadedPhoto ()
        {}

        public int Id;              
        public string LocalName;    
        public string ServerName;   
        public string DirectLink;  
        public string ShortUrl;     
        public string Viewer;       
        public string Miniatura;    
        public string Delete;       
        public bool FromLastUpload; 
    }
}
