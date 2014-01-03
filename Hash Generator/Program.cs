using System;
using System.Linq;
using System.IO;

namespace Hash_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(".");
                foreach (String file in filePaths)
                {
                    Console.WriteLine("<file><name>" + file + "</file><md5>" + MD5.GetMd5HashFromFile(file) + "</md5></file>");
                }
            }
            catch (Exception Err)
            {
                Console.WriteLine(Err);
            }
            Console.ReadLine();
        }
    }
}
