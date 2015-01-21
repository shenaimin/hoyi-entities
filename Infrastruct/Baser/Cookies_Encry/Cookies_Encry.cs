using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Infrastructure.Baser.Cookies_Encry
{
    /// <summary>
    /// 针对Cookies的加密解密.
    /// </summary>
    public class Cookies_Encry
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public string UnEncryptString(string strSource)
        {
            if (strSource == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                byte[] bytes = Encoding.Unicode.GetBytes(strSource);
                byte[] key = { 43, 136, 76, 107, 172, 255, 227, 114 };
                byte[] iv = { 102, 117, 69, 24, 131, 225, 93, 170 };
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                ICryptoTransform detrans = desc.CreateDecryptor(key, iv);
                byte[] de = detrans.TransformFinalBlock(bytes, 0, bytes.Length);
                return Encoding.Unicode.GetString(de);
            }
        }
        /// <summary>
        /// 加密.
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public string EncryptString(string strSource)
        {
            if (strSource == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                byte[] bytes = Encoding.Unicode.GetBytes(strSource);
                byte[] key = { 43, 136, 76, 107, 172, 255, 227, 114 };
                byte[] iv = { 102, 117, 69, 24, 131, 225, 93, 170 };
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                ICryptoTransform entrans = desc.CreateEncryptor(key, iv);
                byte[] en = entrans.TransformFinalBlock(bytes, 0, bytes.Length);
                return Encoding.Unicode.GetString(en);
            }
        }
    }
}
