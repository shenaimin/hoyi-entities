/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.ctrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.Model
{
    /// <summary>
    /// 查询的日期类型.
    /// </summary>
    public enum DateType
    { 
        /// <summary>
        /// 今天
        /// </summary>
        TODAY,
        /// <summary>
        /// 昨天
        /// </summary>
        YESTERDAY,
        /// <summary>
        /// 前天
        /// </summary>
        BEFORE_YESTERDAY,
        /// <summary>
        /// 明天，一般无用
        /// </summary>
        TOMORROW,
        /// <summary>
        /// 本周
        /// </summary>
        ONEWEEK,
        /// <summary>
        /// 本月
        /// </summary>
        ONEMONTH,
        /// <summary>
        /// 本年
        /// </summary>
        ONEYEAR,
        /// <summary>
        /// 固定的日期范围
        /// </summary>
        CUSDAY,
        /// <summary>
        /// 未知.
        /// </summary>
        UNKNOWN,
    }
    /// <summary>
    /// 时间类型
    /// </summary>
    public class  DateONLY
    {
        public static DateONLY Parse(string _date)
        {
            return new DateONLY(_date);
        }

        public DateONLY(string _date)
        {
            this.date = _date;
        }

        public string date { get; set; }
    }

    /// <summary>
    /// 日期.
    /// </summary>
    public struct Date
    {
        /// <summary>
        /// 日期类型
        /// </summary>
        public DateType datetype;
        /// <summary>
        /// 间隔，默认为0
        /// </summary>
        public int Interval;

        public Date(DateType _datetype)
        {
            this.datetype = _datetype;
            this.Interval = 0;
        }

        public Date(DateType _datetype, int _interval)
        {
            this.datetype = _datetype;
            this.Interval = _interval;
        }
    }

    [Serializable]
    public class AttField
    {
        public AttField() { }

        public string fieldname { get; set; }

        public AttField(string _fieldname)
        {
            this.fieldname = _fieldname;
        }

        public override string ToString()
        {
            return this.fieldname;
        }

        public FILTER Equals(object value)
        {
            return new FILTER(this.fieldname, "equals", value);
        }

        public FILTER Except(object value)
        {
            return new FILTER(this.fieldname, "except", value);
        }

        public FILTER Like(object[] values)
        {
            FILTER prefilter = null;
            FILTER filter = null;


            foreach (var va in values)
            {
                filter = new FILTER(this.fieldname, "%", va);
                if (prefilter == null)
                {
                    prefilter = filter;
                }
                else
                {
                    filter.PreOps = "|";
                    filter.Pre = prefilter;
                    prefilter = filter;
                }
            }
            return filter;
        }
        /// <summary>
        /// In命令.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public FILTER In(HOYICMD cmd)
        {
            FILTER prefilter = null;
            FILTER filter = null;


            filter = new FILTER(this.fieldname, "cmdin", cmd);
            if (prefilter == null)
            {
                prefilter = filter;
            }
            else
            {
                filter.PreOps = "";
                filter.Pre = prefilter;
                prefilter = filter;
            }

            return filter;
        }
        /// <summary>
        /// 日期检索.ONLY 日期检索，不提供时间检索.
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public FILTER InDate(DateType tp, int interval)
        {
            Date dt = new Date(tp, interval);
            return new DateDiffFILTER(this.fieldname, "indate", dt);
        }
        /// <summary>
        /// 日期检索.ONLY 日期检索，不提供时间检索.
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public DateDiffFILTER InDate(DateType tp)
        {
            Date dt = new Date(tp);
            return new DateDiffFILTER(this.fieldname, "indate", dt);
        }

        public FILTER In(object[] values)
        {
            FILTER prefilter = null;
            FILTER filter = null;

            foreach (var va in values)
            {
                filter = new FILTER(this.fieldname, "equals", va);
                if (prefilter == null)
                {
                    prefilter = filter;
                }
                else
                {
                    filter.PreOps = "|";
                    filter.Pre = prefilter;
                    prefilter = filter;
                }
            }
            return filter;
        }

        public FILTER In(List<object> values)
        {
            object[] obj = values.ToArray();
            return In(obj);
        }


        public FILTER InENT(object[] values)
        {
            FILTER prefilter = null;
            FILTER filter = null;


            foreach (var va in values)
            {
                filter = new FILTER(this.fieldname, "equals", va);
                if (prefilter == null)
                {
                    prefilter = filter;
                }
                else
                {
                    filter.PreOps = "|";
                    filter.Pre = prefilter;
                    prefilter = filter;
                }
            }
            return filter;
        }
        /// <summary>
        /// In命令.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public FILTER NotIn(HOYICMD cmd)
        {
            FILTER prefilter = null;
            FILTER filter = null;


            filter = new FILTER(this.fieldname, "cmdnotin", cmd);
            if (prefilter == null)
            {
                prefilter = filter;
            }
            else
            {
                filter.PreOps = "";
                filter.Pre = prefilter;
                prefilter = filter;
            }

            return filter;
        }

        public FILTER NotIn(List<object> values)
        {
            object[] obj = values.ToArray();
            return NotIn(obj);
        }

        public FILTER NotIn(object[] values)
        {
            FILTER prefilter = null;
            FILTER filter = null;


            foreach (var va in values)
            {
                filter = new FILTER(this.fieldname, "except", va);
                if (prefilter == null)
                {
                    prefilter = filter;
                }
                else
                {
                    filter.PreOps = "&";
                    filter.Pre = prefilter;
                    prefilter = filter;
                }
            }
            return filter;
        }
        /// <summary>
        /// Like
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator %(AttField str, object value)
        {
            return new FILTER(str.fieldname, "%", value);
        }
        /// <summary>
        /// 绝对Like, 为空则不判断.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator /(AttField str, object value)
        {
            return new FILTER(str.fieldname, "/", value);
        }
        /// <summary>
        /// - value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator -(AttField str, object value)
        {
            return new FILTER(str.fieldname, "-=", value);
        }
        /// <summary>
        /// + value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator +(AttField str, object value)
        {
            return new FILTER(str.fieldname, "+=", value);
        }
        /// <summary>
        /// 跟equals相同
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator ==(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, "equals", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, "equals", value);
            }
            else
            {
                return new FILTER(str.fieldname, "equals", value);
            }
        }
        /// <summary>
        /// 跟except相同.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator !=(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, "except", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, "except", value);
            }
            else
            {
                return new FILTER(str.fieldname, "except", value);
            }
        }
        /// <summary>
        /// 时间比对语法.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator >(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, ">", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, ">", value);
            }
            else
            {
                return new FILTER(str.fieldname, ">", value);
            }
        }
        /// <summary>
        /// 时间比对语法.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator <(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, "<", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, "<", value);
            }
            else
            {
                return new FILTER(str.fieldname, "<", value);
            }
        }
        /// <summary>
        /// 赋值语法.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator >=(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, ">=", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, ">=", value);
            }
            else
            {
                return new FILTER(str.fieldname, ">=", value);
            }
        }
        /// <summary>
        /// 赋值语法.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator <=(AttField str, object value)
        {
            if (value is DateTime)
            {
                return new DateTimeFILTER(str.fieldname, "<=", value);
            }
            else if (value is DateONLY)
            {
                return new DateDiffFILTER(str.fieldname, "<=", value);
            }
            else
            {
                return new FILTER(str.fieldname, "<=", value);
            }
        }
    }
}
