using System;
using System.Security.Cryptography;
using System.Text;

namespace TestWebApi.Controllers.utils
{
    // 该类为MD5加密算法实现
    public class MD5Helper
    {
        public static string MD5Pwd(string rpwd)
        {
            string Ret;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(rpwd);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            Ret = Convert.ToBase64String(encryptdata);
            return Ret;
        }
    }
}
