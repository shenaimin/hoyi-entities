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
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Database
{
    /// <summary>
    /// SQL数据库操作类.
    /// </summary>
    public class SqlDatabase : IDatabase
    {
        /// <summary>
        /// 生成SqlDatabase实例.
        /// </summary>
        public SqlDatabase()
        {
            //this.initConnection();
        }
        /// <summary>
        /// 生成SqlDatabase实例.
        /// </summary>
        /// <param name="connectionName"></param>
        public SqlDatabase(string connectionName)
        {
            this.initConnection(connectionName);
        }
        public override void OpenConnection()
        {
            this.connection = new SqlConnection(this.connectionString);
            this.connection.Open();
        }
        /// <summary>
        /// 执行Sql语句,返回一张表.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override DataTable ExecuteTable(string command)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
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
        public override DataTable ExecuteTable(string command, List<DbParameter> parameters,bool isSTOREDPRO) 
        {
            SqlConnection con = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(command, con);
            cmd.CommandText = command;

            if (isSTOREDPRO)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            
            foreach (DbParameter para in parameters)
            {
                cmd.Parameters.Add(para);
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        public override DataTable ExecuteTable(string command, List<DbParameter> parameters)
        {
            return ExecuteTable(command, parameters, true);
        }
        /// <summary>
        /// 执行Sql语句,返回填充后的Dataset.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override DataSet ExecuteDataSet(string command)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 执行命令,返回带连接的Reader.用完此方法后,请使用DataReader.Close(),Connection.Close();
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override IDataReader ExecuteDataReader(string command)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        /// <summary>
        /// 执行Sql语句,返回受影响行数.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string command)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(command, conn);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }
        /// <summary>
        /// 执行sql语句，返回受影响的行数.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int ExecuteParamNonQuery(string command, List<DbParameter> parameters)
        {
            try
            {
                SqlConnection conn = new SqlConnection(this.connectionString);
                SqlCommand cmd = new SqlCommand(command, conn);
                foreach (DbParameter parameter in parameters)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
                // 报错返回0模拟数据操作失败.
            }
        }
        /// <summary>
        /// 执行Sql语句，主要用于判断是否有符合条件的数据.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int ExecuteParamExists(string command, List<DbParameter> parameters)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(command, conn);
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
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            conn.Close();
            return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public override object ExecuteScalar(string command, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
