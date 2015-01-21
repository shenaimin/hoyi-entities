/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.ctrl
{
    /// <summary>
    /// 日期比较,例如，今天 昨天 一周内,本周，本月.
    /// 使用方式为: 
    /// value= today、tomorrow、oneweek、onemonth、oneyear、
    /// </summary>
    [Serializable]
    public class DateDiffFILTER : FILTER
    {   
        public DateDiffFILTER():base() {
            
        }

        public DateDiffFILTER(string _filter, string _operates, object _value)
            : base(_filter, _operates, _value)
        {

        }

    }

    [Serializable]
    public class DateTimeFILTER : FILTER { 
        
        public DateTimeFILTER():base() {
            
        }

        public DateTimeFILTER(string _filter, string _operates, object _value)
            : base(_filter, _operates, _value)
        {

        }
    }

    [Serializable]
    public class FILTER
    {
        public FILTER() { }

        public FILTER(string _filter, string _operates, object _value)
        {
            this.filter = _filter;
            this.OPERATES = _operates;
            this.value = _value;
        }
        /// <summary>
        /// 条件
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// 操作符号
        /// </summary>
        public string OPERATES { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object value { get; set; }

        /// <summary>
        /// 下一个命令组合，如果是1个，则有一个组合
        /// </summary>
        public FILTER Pre { get; set; }
        /// <summary>
        /// 下一个命令组合的操作
        /// </summary>
        public string PreOps { get; set; }
        /// <summary>
        /// and 操作符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator &(FILTER fil, FILTER value)
        {
            value.Pre = fil;
            value.PreOps = "&";
            return value;
        }
        /// <summary>
        /// or 操作符号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FILTER operator |(FILTER fil, FILTER value)
        {
            value.Pre = fil;
            value.PreOps = "|";
            return value;
        }
    }
}
