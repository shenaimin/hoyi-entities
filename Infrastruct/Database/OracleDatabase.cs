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
using System.Data.Common;

namespace Infrastructure.Database
{
    /// <summary>
    /// Oracle数据库方法,当前为空.
    /// </summary>
    public class OracleDatabase : IDatabase
    {
        public override void OpenConnection()
        {
            throw new NotImplementedException();
        }

        public override DataTable ExecuteTable(string command)
        {
            throw new NotImplementedException();
        }

        public override DataTable ExecuteTable(string command, List<DbParameter> parameters, bool isSTOREDPRO)
        {
            throw new NotImplementedException();
        }

        public override DataTable ExecuteTable(string command, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public override DataSet ExecuteDataSet(string command)
        {
            throw new NotImplementedException();
        }

        public override IDataReader ExecuteDataReader(string command)
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery(string command)
        {
            throw new NotImplementedException();
        }

        public override int ExecuteParamNonQuery(string command, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public override int ExecuteParamExists(string command, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public override object ExecuteScalar(string command, List<DbParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}