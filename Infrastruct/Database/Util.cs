/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database
{
    public static class Util
    {
        /// <summary>
        /// def 为如果传入的对象为空，默认返回的值.
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static Int64 ToBigint(this string ob, Int64 def= -1)
        {
            if (!string.IsNullOrEmpty(ob))
            {
                return Int64.Parse(ob);
            }
            return def;
        }

        public static DateONLY ToDateOnly(this DateTime ob)
        {
            return new DateONLY(ob.Year + "/" + ob.Month + "/" + ob.Day);
        }

        public static Boolean ToBoolean(this string ob, bool def= false)
        {
            if (!string.IsNullOrEmpty(ob))
            {
                return Boolean.Parse(ob);
            }
            return def;
        }

        public static int ToInt(this string ob, int def= 0)
        {
            if (!string.IsNullOrEmpty(ob))
            {
                return int.Parse(ob);
            }
            return def;
        }

        public static string ToVarchar(this string ob)
        {
            return ob;
        }

        public static Decimal ToDecimal(this string ob, int def = 0)
        {
            if (!string.IsNullOrEmpty(ob))
            {
                return Decimal.Parse(ob);
            }
            return def;
        }

        public static DateTime ToDatetime(this string ob)
        {
            if (!string.IsNullOrEmpty(ob))
            {
                return DateTime.Parse(ob);
            }
            return DateTime.Parse("1900-1-1");
        }
    }
}
