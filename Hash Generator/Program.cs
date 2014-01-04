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
                root.SetAttribute("update_server", "http://pic.cm/releases/live/");
                xml.AppendChild(root);
                foreach (String file in filePaths)
                {
                    XmlElement child = xml.CreateElement("file");
                    child.SetAttribute("name", file.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length));
                    child.SetAttribute("hash", MD5.GetMd5HashFromFile(file));
                    root.AppendChild(child);
                }
                xml.Save("C:\\Users\\Connor\\Desktop\\Version.xml");
            }
            catch (Exception Err)
            {
                Console.WriteLine(Err);
            }
        }
    }
}
