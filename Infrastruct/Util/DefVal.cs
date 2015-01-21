using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Util
{
    /// <summary>
    /// 默认
    /// </summary>
    public class DefVal
    {
        /// <summary>
        /// 返回默认值，一般处理默认值为-1的字段.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DV(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return "-1";
            }
            return val;
        }
    }
}
