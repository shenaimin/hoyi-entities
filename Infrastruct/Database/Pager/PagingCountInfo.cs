/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2008-11-21
 *          ModifyDate:2008-11-21
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.Pager
{
    /// <summary>
    /// 获取分页总个数参数模型.
    /// </summary>
    public class PagingCountInfo
    {
        /// <summary>
        /// 表名.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 分页字段,按此字段进行分页.
        /// </summary>
        public string CountFields { get; set; }
        /// <summary>
        /// 筛选条件,例如: id like "aa%";
        /// </summary>
        public string Filter { get; set; }
    }
}
