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

namespace Infrastructure.Database.Cluster
{
    /// <summary>
    /// 服务器类型.
    /// </summary>
    public enum DBSERVERTYPE
    {
        UNKNOW,   // 未知
        MASTER,   // 主服务器
        RE_MASTER,// 备用主服务器
        SLAVER,   // 从服务器
        READER,   // 读服务器
        WRITER    // 写服务器
    }
}
