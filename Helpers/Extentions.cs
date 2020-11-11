using System.Text;
using System.Security.Cryptography;
using System;
namespace AuthenticationAPI
{
    public static class Extentions
    { 
        public static string EncryptString(this string pass)
        {
            using(MD5 md5 = MD5.Create())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8.GetBytes(pass));
                return Convert.ToBase64String(data);
            }
        }
    }
}