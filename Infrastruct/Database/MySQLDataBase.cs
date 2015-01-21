/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2014-03-27
 *          ModifyDate:2014-03-27
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Infrastructure.Database
{
    /// <summary>
    /// MYSQL数据库操作类.
    /// </summary>
    public class MySqlDatabase  : IDatabase
    {
        /// <summary>
        /// 生成MySqlDataBase实例.
        /// </summary>
        public MySqlDatabase()
        {
            //this.initConnection();
        }
        /// <summary>
        /// 生成MySqlDataBase实例.
        /// </summary>
        /// <param name="connectionName"></param>
        public MySqlDatabase(string connectionName)
        {
            this.initConnection(connectionName);
        }

        public override void OpenConnection()
        {
            this.connection = new MySqlConnection(this.connectionString);
            this.connection.Open();
        }
        /// <summary>
        /// 执行MySql语句,返回一张表.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override DataTable ExecuteTable(string _command)
        {
            MySqlConnection conn = new MySqlConnection(this.connectionString);
            MySqlCommand cmd = new MySqlCommand(_command, conn);

            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds.Tables[0];
        }
        /// <summary>
        /// 执行存储过程,返回DataTable.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="extendsionParamter">如果是TRUE表明是调用存储过程.</param>
        /// <returns></returns>
        public override DataTable ExecuteTable(string _command, List<DbParameter> parameters, bool isSTOREDPRO)
        {
            MySqlConnection conn = new MySqlConnection(this.connectionString);
            MySqlCommand cmd = new MySqlCommand(_command, conn);

            if (isSTOREDPRO)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            if (parameters != null)
            {
                foreach (DbParameter para in parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }

            MySqlDataAdapter adp = new MySqlDataAdapter((MySqlCommand)cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            
            return ds.Tables[0];
        }

        public override DataTable ExecuteTable(string _command, List<DbParameter> parameters)
        {
            return ExecuteTable(_command, parameters, true);
        }

        /// <summary>
        /// 执行MySql语句,返回填充后的Dataset.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override DataSet ExecuteDataSet(string _command)
        {
            MySqlConnection conn = new MySqlConnection(this.connectionString);
            MySqlCommand cmd = new MySqlCommand(_command, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行命令,返回带连接的Reader.用完此方法后,请使用DataReader.Close(),Connection.Close();
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override IDataReader ExecuteDataReader(string _command)
        {
            MySqlConnection conn = new MySqlConnection(this.connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(_command, conn);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        /// <summary>
        /// 执行MySql语句，主要用于判断是否有符合条件的数据.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int ExecuteParamExists(string _command, List<DbParameter> parameters)
        {
            MySqlConnection conn = new MySqlConnection(this.connectionString);
            MySqlCommand cmd = new MySqlCommand(_command, conn);
            foreach (DbParameter parameter in parameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                cmd.Parameters.Add(parameter);
            }
            conn.Open();
            //int i = cmd.ExecuteNonQuery();
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            conn.Close();
            return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        /// <summary>
        /// 执行MySql语句,返回受影响行数. 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>执行事务的时候返回值是无效的。</returns>
        public override int ExecuteNonQuery(string _command)
        {
            if (autoCommit)
            {
                connection = new MySqlConnection(this.connectionString);
            }
            MySqlCommand cmd = new MySqlCommand(_command, (MySqlConnection)connection);
            if (autoCommit == false)
            {
                if (this.transaction == null)
                {
                    this.transaction = connection.BeginTransaction();
                }
                cmd.Transaction = (MySqlTransaction)this.transaction;
            }
            if (autoCommit)
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                return i;
            }

            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 
        /// 执行Mysql语句，返回受影响的行数. 连续的增删改，才允许使用事务，其他情况不需要打开事务处理.
        /// 是否打开事务，由数据库创建时候决定,autoCommit = false就为启动事务。
        /// 
        ///	baseDatabase database = (baseDatabase)DatabaseFactory.Create(true);
        ///	IRole_userExpert _role_userExpert = (IRole_userExpert)ExpertFactory.New(typeof(Role_userExpert), database);
        ///	
        /// 使用事务的时候，
        /// 
        /// database.OpenConnection();
        /// _role_userExpert.Add(....);
        /// database.CloseConnection();
        /// 
        /// 其他时候不需要创建database.
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns>执行事务的时候返回值是无效的。</returns>
        public override int ExecuteParamNonQuery(string _command, List<DbParameter> parameters)
        {
            if (autoCommit)
            {
                connection = new MySqlConnection(this.connectionString);
            }
            MySqlCommand cmd = new MySqlCommand(_command, (MySqlConnection)connection);
            if (autoCommit == false)
            {
                cmd.Transaction = (MySqlTransaction)this.transaction;
            }

            foreach (DbParameter parameter in parameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                cmd.Parameters.Add(parameter);
            }
            if (autoCommit)
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                return i;
            }
            
            return cmd.ExecuteNonQuery();
        }

        public override object ExecuteScalar(string _command, List<DbParameter> parameters)
        {
            if (autoCommit)
            {
                connection = new MySqlConnection(this.connectionString);
            }
            MySqlCommand cmd = new MySqlCommand(_command, (MySqlConnection)connection);
            if (autoCommit == false)
            {
                cmd.Transaction = (MySqlTransaction)this.transaction;
            }

            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
            if (autoCommit)
            {
                connection.Open();
                object i = cmd.ExecuteScalar();
                connection.Close();
                return i;
            }
            return cmd.ExecuteScalar();
        }
    }
}
