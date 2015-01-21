/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2008-11-19
 *          ModifyDate:2008-11-19
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database
{
    /// <summary>
    /// 当前系统所支持的数据库类型.
    /// </summary>
    public enum DatabaseType
    {
        Unknow,
        SQL,
        ORACLE,
        MYSQL_MariaDB
    }
    /// <summary>
    /// 当前连接类型.
    /// </summary>
    public enum ConnectType
    {
        Default,
        UserConnect,
        RAConnect
    }
    

    public class DatabaseConfig
    {
        /// <summary>
        /// 默认数据库连接名称.
        /// </summary>
        public static string DefaultConnectName = "Default";
    }
    

}
