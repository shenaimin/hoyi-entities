﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Baser.Codec
{
    /// <summary>
    /// BASE64加密类
    /// </summary>
    public class Base64Encrypt
    {

        /// <summary>
        /// /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
        /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
        /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
        /// 系統中而不同。
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64EncryptString(string str)
        {
            byte[] bytestr = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytestr);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encryptstr"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string encryptstr)
        {
            try
            {
                byte[] decryptstr = Convert.FromBase64String(encryptstr);
                return Encoding.UTF8.GetString(decryptstr);
            }
            catch (Exception)
            {
                return encryptstr;
              
            }
        }
    }
}
