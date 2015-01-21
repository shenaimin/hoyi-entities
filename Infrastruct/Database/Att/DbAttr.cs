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

namespace Infrastructure.Database.Att
{
    /// <summary>
    /// 数据库的属性
    /// </summary>
    [Serializable]
    public class DbAttr : Attribute
    {
        public DbAttr() { }
        /// <summary>
        /// 数据库类型.
        /// </summary>
        public datatype type { get; set; }
        /// <summary>
        /// 长度.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 小数点前的长度.
        /// </summary>
        public int Prefix { get; set; }
        /// <summary>
        /// 小数点后的长度.
        /// </summary>
        public int suffix { get; set; }
        /// <summary>
        /// 是否主键.
        /// </summary>
        public bool isPK { get; set; }
        /// <summary>
        /// 是否自增.
        /// </summary>
        public bool Identity { get; set; }
        /// <summary>
        /// 备注.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 字段名.
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 默认值.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否不能为空.
        /// </summary>
        public bool NotNULL { get; set; }

        public DbAttr(datatype _type, int _length)
        {
            this.type = _type;
            this.Length = _length;
        }

        public DbAttr(datatype _type, int _length, string _comment)
        {
            this.type = _type;
            this.Length = _length;
            this.Comment = _comment;
        }

        public DbAttr(datatype _type, int _length, string _fieldName,  string _comment)
        {
            this.type = _type;
            this.Length = _length;
            this.Comment = _comment;
            this.FieldName = _fieldName;
        }

        public DbAttr(datatype _type, int _length, bool _identity, bool _isPK)
        {
            this.type = _type;
            this.Length = _length;
            this.Identity = _identity;
            this.isPK = _isPK;
        }

        public DbAttr(datatype _type, int _length, bool _identity, bool _isPK, string _fieldName, string _comment)
        {
            this.type = _type;
            this.Length = _length;
            this.Identity = _identity;
            this.isPK = _isPK;
            this.FieldName = _fieldName;
            this.Comment = _comment;
        }

        public DbAttr(datatype _type, int _length, bool _identity, bool _isPK, int _prefix, int _suffix, string _fieldName, string _comment)
        {
            this.type = _type;
            this.Length = _length;
            this.Identity = _identity;
            this.isPK = _isPK;
            this.Prefix = _prefix;
            this.suffix = _suffix;
            this.FieldName = _fieldName;
            this.Comment = _comment;
        }
    }
}
