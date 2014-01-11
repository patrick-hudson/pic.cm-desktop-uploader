using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

using Piccm_Uploader.Core;

namespace Piccm_Uploader
{
    class Update
    {
        public void InitUpdate()
        {

            History.SQLiteDatabase sqldb = new History.SQLiteDatabase();

            System.Data.DataTable db = sqldb.GetDataTable("PRAGMA user_version;");
            int dbversion = Convert.ToInt16(db.Rows[0][0]);
            if (dbversion < References.DBVERSION)
            {
#if DEBUG
                Console.WriteLine("Found database version: " + dbversion);
#endif
                if (dbversion == 0)
                    sqldb.ExecuteNonQuery("ALTER TABLE history ADD COLUMN image_data BLOB;" +
                                          "DELETE FROM history WHERE id NOT IN (SELECT MAX(id) FROM history GROUP BY image_name);" +
                                          "PRAGMA user_version = 1;");
            }

            if (File.Exists("update.bat"))
                File.Delete("update.bat");


            try
            {
                Queue<String> downloadList = new Queue<String>();
                string message = String.Empty, downloadurl = String.Empty;

                XmlDocument xdoc = new XmlDocument();
#if !DEBUG
                xdoc.Load("http://pic.cm/releases/live/Version.xml");
#else
                xdoc.Load("http://pic.cm/releases/dev/Version.xml");
#endif

                XmlNode root = xdoc.SelectSingleNode("//root");
                XmlNodeList nodeList = root.SelectNodes("file");

                downloadurl = root.Attributes["update_server"].Value;

                foreach (XmlNode node in nodeList)
                {
                    string fileName = node.Attributes["name"].Value;
                    if (File.Exists(fileName))
                    {
                        if (!GetMd5HashFromFile(fileName).ToUpper().Equals(node.Attributes["hash"].Value))
                        {
                            downloadList.Enqueue(fileName);
                        }
                    }
                    else
                    {
                        downloadList.Enqueue(fileName);
                    }
                }

                if (downloadList.Count > 0)
                {
                    if (MessageBox.Show("Download update?", "Update Available!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        while (downloadList.Count > 0)
                        {
                            string file = downloadList.Dequeue();
                            DownloadFile(downloadurl, file);
                        }

                        File.WriteAllText("update.bat", Resources.Resource.update);
                        Process.Start("update.bat");
                        Application.Exit();
                    }
                }
                else if (Program.updateFirstStart == false)
                {
                    MessageBox.Show("No updates currently available", "Try again later!", MessageBoxButtons.OK);
                }

            }
#if DEBUG
            catch (Exception err)
#else
            catch(Exception)
#endif
            {
#if !DEBUG
                MessageBox.Show("An error occured contacting the update server.", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                Console.WriteLine(err.Message);
                Console.WriteLine(err.StackTrace);
#endif
            }
        }

        private static void DownloadFile(string UpdateURL, string File)
        {
            string sUrlToReadFileFrom = UpdateURL + File;
            string sFilePathToWriteFileTo = File + ".update";

            Uri url = new Uri(sUrlToReadFileFrom);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();

            Int64 iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                {
                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;
                        }

                        streamLocal.Close();
                    }

                    streamRemote.Close();
                }
            }
        }

        private static void CopyStream(Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        private static string GetMd5HashFromFile(string fileName)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }
    }
}
