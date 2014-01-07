using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Hash_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
                XmlDocument xml = new XmlDocument();
                XmlElement root = xml.CreateElement("root");
#if !DEBUG
                root.SetAttribute("update_server", "http://pic.cm/releases/live/");
#else
                root.SetAttribute("update_server", "http://pic.cm/releases/dev/");
#endif
                xml.AppendChild(root);
                foreach (String file in filePaths)
                {
                    FileInfo f = new FileInfo(file);
                    if (f.Name.Contains("Generator") || f.Name.Contains("Version"))
                        continue;

                    XmlElement child = xml.CreateElement("file");
                    child.SetAttribute("name", file.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length));
                    child.SetAttribute("hash", MD5.GetMd5HashFromFile(file));
                    root.AppendChild(child);
                }
                xml.Save("Version.xml");
            }
            catch (Exception Err)
            {
                Console.WriteLine(Err);
            }
        }
    }
}
