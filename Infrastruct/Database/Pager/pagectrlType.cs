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

namespace Infrastructure.Database.Pager
{
    /// <summary>
    /// 分页控件会用到的一些类型.
    /// </summary>
    public enum pagectrlType
    {
        Default,
        Search,
        Update,
        Add,
        Delete,
        RELATION,
        All,
        Current
    }
}
