using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piccm_Uploader.Core
{
    public class References
    {

        private static string _url_upload = "http://pic.cm/api.php", _url_view = "http://i.pic.cm/";

        internal enum Icon
        {
            ICON_DEFAULT,
            ICON_UPLOAD
        }

        internal enum Sound
        {
            SOUND_JINGLE,
            WIN_ASTERISK
        }

        internal enum ClickAction
        {
            NOTHING,
            CANCEL_UPLOAD
        }

        internal static string URL_UPLOAD
        {
            get { return _url_upload; }
            set { _url_upload = value; }
        }

        internal static string URL_VIEW
        {
            get { return _url_view; }
            set { _url_view = value; }
        }

        internal static string APIKey = "thisismyapikeynooneshouldknowmyapikey";

        internal static string VERSIONNUMBER = "1.0";
        internal static string BUILDNUMER = "%JENKINS_BUILD_NUMER%";
        internal static string BUILDDATE = "%JENKINS_BUILD_DATE%";
    }
}
