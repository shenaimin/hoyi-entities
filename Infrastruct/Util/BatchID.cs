using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    public class BatchID
    {
        /// <summary>
        /// 获取 年月日 时分秒 毫秒 + 3位随机组成的批次号.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomBatch()
        {
            return DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("/", "") + DateTime.Now.Millisecond.ToString()+ GetRandomSuffix(3);//// +Random();
        }
        /// <summary>
        /// 获取随机数，长度为length.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomSuffix(int length)
        {
            string ss = "";
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int x = rand.Next(0, 9);
                ss += x.ToString();
            }
            return ss;
        }
    }
}
