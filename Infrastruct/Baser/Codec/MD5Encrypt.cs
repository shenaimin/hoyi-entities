using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Infrastructure.Baser.Codec
{
    /// <summary>
    /// MD5加密类
    /// </summary>
 public  class MD5Encrypt
 {
     /// <summary>
     ///获得MD5加密的字符串
     /// </summary>
     /// <param name="str">源字符串</param>
     /// <returns></returns>
     public static string MD5String(string str)
     {
         MD5 md5=new MD5CryptoServiceProvider();
         byte[] md5byte = md5.ComputeHash(Encoding.UTF8.GetBytes(str.Trim()));
         return BitConverter.ToString(md5byte).Replace("-", "");
     }
     /// <summary>
     /// 根据文件路径获得文件的MD5值
     /// </summary>
     /// <param name="path"></param>
     /// <returns></returns>
     public static string GetFileMD5HashValue(string path)
     {

         if (!File.Exists(path))
         {
             return "";
         }
         string md5str = "";
         try
         {
             FileStream fs = new FileStream(path, FileMode.Open);//打开文件
             MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();//创建一个md5服务类
             byte[] md5bytes = md5Crypto.ComputeHash(fs);//获得计算指定Stream 对象的哈希值
             fs.Close();
             md5str = BitConverter.ToString(md5bytes);//将字节数组转换为字符串
             md5str = md5str.Replace("-", "");
         }
         catch (Exception)
         {
             throw;
         }
         return md5str;
     }

     /// <summary>
     /// 根据流的MD5值
     /// </summary>
     /// <param name="path"></param>
     /// <returns></returns>
     public static string GetFileMD5HashValue(Stream stream)
     {
         if (stream.Length == 0)
         {

             return "";
         }
         string md5str = "";
         try
         {
             MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();//创建一个md5服务类

             byte[] md5bytes = md5Crypto.ComputeHash(stream);//获得计算指定Stream 对象的哈希值

             md5str = BitConverter.ToString(md5bytes);//将字节数组转换为字符串
             md5str = md5str.Replace("-", "");

         }
         catch (Exception)
         {
             throw;
         }
         return md5str;
     }
 }
}
