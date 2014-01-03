using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Hash_Generator
{
    public class MD5
    {

        public static string GetMd5HashFromFile(string fileName)
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
