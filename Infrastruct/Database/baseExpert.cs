/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.Cluster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.Database
{
    /// <summary>
    /// 操作类的基类.
    /// </summary>
    [Serializable]
    public abstract class baseExpert
    {
        public baseExpert() { }

        IDatabase database = null;
        public int LAST_INSERT_ID()
        {
            StringBuilder command = new StringBuilder();
            command.Append(" SELECT LAST_INSERT_ID();");

            return Convert.ToInt32(this.ExecuteTable(command.ToString(), null, false).Rows[0][0]);
        }
        public void setDatabase(IDatabase _database)
        {
            database = _database;
            autoCommit = false;
        }
        /// <summary>
        /// 是否自动提交, 不自动提交则启动事务.commit之后才会提交.
        /// </summary>
        bool autoCommit = true;
        #region 执行
        /// <summary>
        /// 执行命令,返回DataTable.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public DataTable ExecuteTable(string command)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command);
            }
            return database.ExecuteTable(command);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行存储过程,返回DataTable.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteTable(string command, List<DbParameter> parameters)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command, parameters);
            }
            return database.ExecuteTable(command, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行存储过程,返回DataTable.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="isSTOREPRU"></param>
        /// <returns></returns>
        public DataTable ExecuteTable(string command, List<DbParameter> parameters, bool isSTOREPRU)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command, parameters, isSTOREPRU);
            }
            return database.ExecuteTable(command, parameters, isSTOREPRU);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行命令,返回Dataset.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string command)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteDataSet(command);
            }
            return database.ExecuteDataSet(command);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行命令,返回受影响的行数.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public int ExecuteNounery(string command)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteNonQuery(command);
            }
            return database.ExecuteNonQuery(command);
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }
        /// <summary>
        /// 执行sql语句，返回受影响的行数.
        /// 读写服务器不选择true false 则默认选择读服务器.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteParamNonQuery(string command, List<DbParameter> parameters)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteParamNonQuery(command, parameters);
            }
            return database.ExecuteParamNonQuery(command, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }
        /// <summary>
        /// 执行命令,返回带连接的Reader.用完此方法后,请使用DataReader.Close(),Connection.Close();
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public IDataReader ExecuteDataReader(string command)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteDataReader(command);
            }
            return database.ExecuteDataReader(command);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行sql语句，主要用于判断是否有数据存在.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteParamExists(string command, List<DbParameter> parameters)
        {
            //try
            //{

            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteParamExists(command, parameters);
            }
            return database.ExecuteParamExists(command, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }
        /// <summary>
        /// 执行sql语句，主要用于判断是否有数据存在.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="_Are_READ_SERVER">是否写服务器. true 为读服务器， false 为写服务器. </param>
        /// <returns></returns>
        public int ExecuteParamExists(string command, List<DbParameter> parameters, bool _Are_READ_SERVER)
        {
            //try
            //{

            if (database == null)
            {
                return DataBaseCluster.Get_READ_SERVER().ExecuteParamExists(command, parameters);
            }
            return database.ExecuteParamExists(command, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }

        public object ExecuteScalar(string command, List<DbParameter> parameters)
        {
            //try
            //{
            if (database == null)
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteScalar(command, parameters);
            }
            return database.ExecuteScalar(command, parameters);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }

        #endregion

        #region static 执行


        /// <summary>
        /// 执行命令,返回DataTable.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public static DataTable STA_ExecuteTable(string command)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command);
        }
        /// <summary>
        /// 执行存储过程,返回DataTable.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable STA_ExecuteTable(string command, List<DbParameter> parameters)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command, parameters);

        }
        /// <summary>
        /// 执行存储过程,返回DataTable.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="isSTOREPRU"></param>
        /// <returns></returns>
        public static DataTable STA_ExecuteTable(string command, List<DbParameter> parameters, bool isSTOREPRU)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteTable(command, parameters, isSTOREPRU);

        }
        /// <summary>
        /// 执行命令,返回Dataset.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public static DataSet STA_ExecuteDataset(string command)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteDataSet(command);

        }
        /// <summary>
        /// 执行命令,返回受影响的行数.
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public static int STA_ExecuteNounery(string command, IDatabase _database = null)
        {
            if (_database != null)
            {
                return _database.ExecuteNonQuery(command);
            }
            else
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteNonQuery(command);
            }

        }
        /// <summary>
        /// 执行sql语句，返回受影响的行数.
        /// 读写服务器不选择true false 则默认选择读服务器.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int STA_ExecuteParamNonQuery(string command,  List<DbParameter> parameters,IDatabase _database = null)
        {
            if (_database != null)
            {
                return _database.ExecuteParamNonQuery(command, parameters);
            }
            else
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteParamNonQuery(command, parameters);
            }

        }
        /// <summary>
        /// 执行命令,返回带连接的Reader.用完此方法后,请使用DataReader.Close(),Connection.Close();
        /// </summary>
        /// <param name="command">待执行的命令.</param>
        /// <returns></returns>
        public static IDataReader STA_ExecuteDataReader(string command)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteDataReader(command);

        }
        /// <summary>
        /// 执行sql语句，主要用于判断是否有数据存在.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int STA_ExecuteParamExists(string command, List<DbParameter> parameters)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteParamExists(command, parameters);

        }
        /// <summary>
        /// 执行sql语句，主要用于判断是否有数据存在.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="_Are_READ_SERVER">是否写服务器. true 为读服务器， false 为写服务器. </param>
        /// <returns></returns>
        public static int STA_ExecuteParamExists(string command, List<DbParameter> parameters, bool _Are_READ_SERVER)
        {
            return DataBaseCluster.Get_READ_SERVER().ExecuteParamExists(command, parameters);

        }

        public static object STA_ExecuteScalar(string command, List<DbParameter> parameters, IDatabase _database = null)
        {
            if (_database != null)
            {
                return _database.ExecuteScalar(command, parameters);
            }
            else
            {
                return DataBaseCluster.Get_WRITE_SERVER().ExecuteScalar(command, parameters);
            }
        }

        #endregion
    }
}