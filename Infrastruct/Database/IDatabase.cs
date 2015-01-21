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
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace Infrastructure.Database
{
    /// <summary>
    /// Database 基类,描述数据库操作的最基本的方法.
    /// </summary>
    [Serializable]
    public abstract class IDatabase
    {
        /// <summary>
        /// 是否自动提交，否则启动事务。
        /// </summary>
        public bool autoCommit { get; set; }
        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction transaction { get; set; }
        /// <summary>
        /// 连接.
        /// </summary>
        public DbConnection connection { get; set; }
        /// <summary>
        /// 命令.
        /// </summary>
        public DbCommand command { get; set; }
        /// <summary>
        /// 数据适配器.
        /// </summary>
        protected DbDataAdapter adapter { get; set; }
        /// <summary>
        /// 数据库构造.
        /// </summary>
        public IDatabase() 
        {
            this.autoCommit = true;
            //this.initConnection();
        }
        /// <summary>
        ///  数据库构造.
        /// </summary>
        /// <param name="connectName"></param>
        public IDatabase(string connectName)
        {
            this.autoCommit = true;
            this.initConnection(connectName);
        }
        /// <summary>
        /// 连接字符串.
        /// </summary>
        public string connectionString = null;
        /// <summary>
        /// 
        /// 数据库服务器的名称, 为ConnectionString 内的 Name的值.
        /// 如果是单个服务器，则直接取 Default
        /// 
        /// </summary>
        public string DBServerName = "Default";
        /// <summary>
        /// 数据库类型.sql or oracle, mysql.
        /// </summary>
        public DatabaseType dbType;
        /// <summary>
        /// 获取连接字符串.
        /// </summary>
        /// <returns></returns>
        public string getConnection()
        {
            return this.connectionString;
        }
        public void InitAndOpenTransaction() {
            this.autoCommit = false;
            this.initConnection();
            this.OpenConnection();
            this.BeginTransaction();
        }

        public void BeginTransaction() {
            this.transaction = this.connection.BeginTransaction();
        }
        public void CommitTranscation()
        {
            this.transaction.Commit();
        }
        public void RollBackTransaction() {
            this.transaction.Rollback();
        }
        public abstract void OpenConnection();
        public void CloseConnection() {
            if (this.connection.State != null)
            {
                this.connection.Close();
                this.connection.Dispose();
            }
        }
        /// <summary>
        /// 初始化连接字符串.
        /// </summary>
        public void initConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings[DBServerName].ConnectionString;
        }
        /// <summary>
        /// 初始化连接字符串.
        /// </summary>
        /// <param name="_DBServerName">连接字符串 NAME 的键值.</param>
        public void initConnection(string _DBServerName)
        {
            connectionString = ConfigurationManager.ConnectionStrings[_DBServerName].ConnectionString;
        }
        /// <summary>
        /// 执行命令,返回DataTable.
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteTable(string command);
        /// <summary>
        /// 执行存储过程,返回DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteTable(string command, List<DbParameter> parameters, bool isSTOREDPRO);
        /// <summary>
        /// 执行存储过程,返回DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteTable(string command, List<DbParameter> parameters);
        /// <summary>
        /// 执行命令,返回Dataset
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        public abstract DataSet ExecuteDataSet(string command);
        /// <summary>
        /// 执行命令,返回带连接的Reader.用完此方法后,请使用DataReader.Close(),Connection.Close();
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        public abstract IDataReader ExecuteDataReader(string command);
        /// <summary>
        /// 执行命令,返回受影响条数.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(string command);
        /// <summary>
        /// 执行带参数命令，返回受影响行数.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteParamNonQuery(string command, List<DbParameter> parameters);
        /// <summary>
        /// 执行带参数命令，返回受影响行数.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteParamExists(string command, List<DbParameter> parameters);

        public abstract object ExecuteScalar(string command, List<DbParameter> parameters);
    }
}
