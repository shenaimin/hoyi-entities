using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    public class CmpUtil
    {
        public static CmpUtil Create()
        {
            return new CmpUtil();
        }
        /// <summary>
        /// 判断是否为空，为空就返回:retva
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ComplexZero(string str, string retva = "0")
        {
            if (string.IsNullOrEmpty(str))
            {
                return retva;
            }
            return str;
        }
        /// <summary>
        /// Object 对象是否为空.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOEmp(object str)
        {
            if (str == null)
            {
                return true;
            }
            else if (string.IsNullOrEmpty(str.ToString()))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加百分号 例如:%1%2%
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string addPercent(string str) {
            string s = str;
            string tmp = "%";
            if (!IsNullOEmp(str))
            {
                for (int i = 0; i < str.Length; i++ )
                {
                    tmp += s[i] + "%";
                    //s = s.Insert(i, "%");
                }
                return tmp;
            }
            return null;
        }
    }
}
