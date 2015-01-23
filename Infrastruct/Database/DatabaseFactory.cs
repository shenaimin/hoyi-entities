/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2008-11-19
 *          ModifyDate:2008-11-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Infrastructure.Database.Cluster;

namespace Infrastructure.Database
{
    /// <summary>
    /// 数据库工厂.
    /// </summary>
    public static class DatabaseFactory
    {
        /// <summary>
        /// 提供程序与DatabaseType集合.
        /// </summary>
        private static Dictionary<string, DatabaseType> AllDatabaseType = new Dictionary<string,DatabaseType>();
        /// <summary>
        /// DatabaseType与Type集合.
        /// </summary>
        private static Dictionary<DatabaseType, Type> AllDatabaseFunction = new Dictionary<DatabaseType, Type>();        
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public static ConnectType connectionStringName = ConnectType.Default;
        /// <summary>
        /// 当前数据库类型.
        /// </summary>
        public static DatabaseType CurrentDataType = DatabaseType.Unknow;
        /// <summary>
        /// 获取当前数据库类型.同一集群的数据库类型，最好弄成一样的。
        /// 这里就不考虑以前的做法，如果是单个服务器就根据单集群的方式来来做。
        /// </summary>
        /// <returns></returns>
        public static DatabaseType getCurrentDataType()
        {
            return DatabaseFactory.Create().dbType;

            //DatabaseFactory.initDatabaseType();
            //CurrentDataType = AllDatabaseType[ConfigurationManager.ConnectionStrings[DatabaseFactory.connectionStringName.ToString()].ProviderName];
            //return CurrentDataType;
        }
        /// <summary>
        /// 初始化数据库类型集合.
        /// </summary>
        private static void initDatabaseType()
        {
            if (AllDatabaseType.Count == 0)
            {
                AllDatabaseType.Add("System.Data.SqlClient", DatabaseType.SQL);
                AllDatabaseType.Add("System.Data.OracleClient", DatabaseType.ORACLE);
                AllDatabaseType.Add("System.Data.MySqlClient", DatabaseType.MYSQL_MariaDB);
            }

            if (AllDatabaseFunction.Count == 0)
            {
                AllDatabaseFunction.Add(DatabaseType.SQL, typeof(SqlDatabase));
                AllDatabaseFunction.Add(DatabaseType.ORACLE, typeof(OracleDatabase));
                AllDatabaseFunction.Add(DatabaseType.MYSQL_MariaDB, typeof(MySqlDatabase));
            }
        }
        /// <summary>
        /// 根据单个连接字符串,初始化数据库.
        /// </summary>
        /// <param name="conns"></param>
        /// <returns></returns>
        public static IDatabase InitDatabase(ConnectionStringSettings conns)
        {
            DatabaseFactory.initDatabaseType();
            if (conns.ProviderName != "" && AllDatabaseType.ContainsKey(conns.ProviderName))
            {
                DatabaseType Dbtype = AllDatabaseType[conns.ProviderName];
                Type type = AllDatabaseFunction[Dbtype];
                IDatabase database = Activator.CreateInstance(type, new object[] { conns.Name }) as IDatabase;
                database.dbType = Dbtype;
                database.connectionString = conns.ConnectionString;
                database.DBServerName = conns.Name;

                return database;
            }
            return null;
        }
        public static IDatabase CopyDatabase(IDatabase _database) 
        {
            DatabaseFactory.initDatabaseType();
            DatabaseType Dbtype = _database.dbType;
            Type type = AllDatabaseFunction[Dbtype];
            IDatabase database = Activator.CreateInstance(type, new object[] { _database.DBServerName }) as IDatabase;
            database.dbType = Dbtype;
            database.connectionString = _database.connectionString;
            database.DBServerName = _database.DBServerName;
            // 主要供事务使用，这里不用自动提交 。
            database.autoCommit = false;

            return database;
        }
        /// <summary>
        /// 创建/获取 数据库.
        /// 如果是集群，则默认获取读的服务器.
        /// </summary>
        /// <returns></returns>
        public static IDatabase Create()
        {
            return DatabaseFactory.Create(true);
        }
        /// <summary>
        /// 创建/获取 数据库.
        /// 如果是集群，则默认获取读的服务器.
        /// </summary>
        /// <param name="_Are_READ_SERVER">是否是读的数据库，true 为 读数据库， false 为写数据库. </param>
        /// <returns></returns>
        public static IDatabase Create(bool _Are_READ_SERVER)
        {
            DataBaseCluster.InitServers();
            return _Are_READ_SERVER ? DataBaseCluster.Get_READ_SERVER() : DataBaseCluster.Get_WRITE_SERVER();
        }
    }
}