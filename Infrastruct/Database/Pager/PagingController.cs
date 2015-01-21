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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.Configuration;
using MySql.Data.MySqlClient;
using Infrastructure.Database.Cluster;

namespace Infrastructure.Database.Pager
{
    /// <summary>
    /// 分页控制类.
    /// </summary>
    public class PagingController
    {
        /// <summary>
        /// 获取分页总个数存储过程名称.
        /// </summary>
        public static string GetRecordStoreName = "GetRecordCount";
        /// <summary>
        /// 获取分页后数据存储过程名称.
        /// </summary>
        public static string GetDataStoreName = "GetPagingData";
        /// <summary>
        /// 通用每页条数.
        /// </summary>
        public static int CustomPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        /// <summary>
        /// 
        /// </summary>
        public PagingController()
        {

        }
        /// <summary>
        /// 获取分页总个数.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static int GetRecordCount(PagingCountInfo info)
        {
             info.TableName = info.TableName.Replace("[", "").Replace("]", "");
            info.Filter = info.Filter.Replace("[", "").Replace("]", "");
            info.CountFields = info.CountFields.Replace("[", "").Replace("]", "");
            string where = info.Filter.Trim(' ').Length == 0 ? "" : " where " + info.Filter;
            string cmd = "select count(" + info.CountFields + ") from " + info.TableName + where;

            return Convert.ToInt32(DataBaseCluster.Get_READ_SERVER().ExecuteTable(cmd).Rows[0][0]);
        }
        /// <summary>
        /// 获取分页数据.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static DataTable GetPagingData(PagingDataInfo info)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter tableName = null, sortedFields = null, pageSize = null, pageIndex = null, fields = null, filter = null;

            switch (DatabaseFactory.getCurrentDataType())
            {
                case DatabaseType.Unknow:
                    break;
                case DatabaseType.SQL:

                    tableName = new MySqlParameter("@TableName", MySqlDbType.VarChar, 1000);
                    tableName.Direction = ParameterDirection.Input;
                    tableName.Value = info.TableName;

                    sortedFields = new MySqlParameter("@SortedFields", MySqlDbType.VarChar, 1000);
                    sortedFields.Direction = ParameterDirection.Input;
                    sortedFields.Value = info.SortedFields;

                    pageSize = new MySqlParameter("@PageSize", MySqlDbType.VarChar, 1000);
                    pageSize.Direction = ParameterDirection.Input;
                    pageSize.Value = info.PageSize;

                    pageIndex = new MySqlParameter("@pageIndex", MySqlDbType.VarChar, 1000);
                    pageIndex.Direction = ParameterDirection.Input;
                    pageIndex.Value = info.PageIndex;

                    fields = new MySqlParameter("@Fields", MySqlDbType.VarChar, 1000);
                    fields.Direction = ParameterDirection.Input;
                    fields.Value = info.Fields;

                    filter = new MySqlParameter("@Filter", MySqlDbType.VarChar, 1000);
                    filter.Direction = ParameterDirection.Input;
                    filter.Value = info.Filter;

                    parameters.Add(tableName);
                    parameters.Add(sortedFields);
                    parameters.Add(pageSize);
                    parameters.Add(pageIndex);
                    parameters.Add(fields);
                    parameters.Add(filter);

                    return DataBaseCluster.Get_READ_SERVER().ExecuteTable(PagingController.GetDataStoreName, parameters);

                case DatabaseType.ORACLE:
                    break;
                case DatabaseType.MYSQL_MariaDB:
                   info.SortedFields = info.SortedFields.Replace("[", "").Replace("]", "");   
                   info.TableName = info.TableName.Replace("[", "").Replace("]", "");
                   info.Fields = info.Fields.Replace("[", "").Replace("]", "");
                   info.Filter = info.Filter.Replace("[", "").Replace("]", "");
                    string where = info.Filter.Trim(' ').Length == 0 ? "" : " where " + info.Filter;
                    string sorted = info.SortedFields.Trim(' ').Length == 0 ? "" : " order by " + info.SortedFields;
                   // sorted = sorted.ToLower().EndsWith("ASC") || sorted.ToLower().EndsWith("DESC") ? sorted : sorted + " asc ";
                    if (sorted.ToLower().Contains("asc") || sorted.ToLower().Contains("desc"))
                    {

                    }
                    else
                    {
                        sorted = sorted + " asc";
                    }

                    int psize = Convert.ToInt32(info.PageSize);
                    int pindex = Convert.ToInt32(info.PageIndex);
                   // pindex = pindex == 0 ? 1 : pindex;
                    int prelimit = psize * (pindex - 1);//(@PageIndex-1)*@PageSize+1
                    if (prelimit < 0)
                    {
                        prelimit = 0;
                    }
                    string limit = " limit " + prelimit + "," + psize;
                 

                    string cmd = "select " + info.Fields + " from " + info.TableName + where + sorted + limit;

                    return DataBaseCluster.Get_READ_SERVER().ExecuteTable(cmd);

                default:
                    break;
            }

            return new DataTable();
        }
        /// <summary>
        /// 执行相应读取分页数据方法.
        /// </summary>
        /// <param name="type">类.写法:typeof(namespace.Class);</param>
        /// <param name="methodName">方法名.</param>
        /// <param name="pDataInfo">分页参数.</param>
        /// <param name="parameters">方法需要用到的参数.</param>
        /// <returns></returns>
        public static DataTable Execute(Type type, string methodName, IPagingDataInfo pDataInfo, object parameters)
        {
            object obj = Activator.CreateInstance(type);
            MethodInfo mo = type.GetMethod(methodName);

            object[] param = { pDataInfo };
            object[] param1 = { pDataInfo, parameters };

            if (parameters != null)
            {
                //此处将参数分开用了逗号,如果现实中逗号不能满足可能需要更改.
                object[] param2 = parameters.ToString().Split(',');
                //add by wzy 2009-01-15 start
                if (parameters.ToString().IndexOf("DATEDIFF") > -1)
                {
                    param2 = parameters.ToString().Split('^');
                }
                //add by wzy 2009-01-15 end
                if (param2.Length > 1)
                {
                    ArrayList arr = new ArrayList();
                    arr.Insert(0, pDataInfo);

                    for (int i = 1; i < param2.Length + 1; i++)
                    {
                        arr.Insert(i, param2[i - 1]);
                    }

                    param = new object[arr.Count];
                    for (int i = 0; i < arr.Count; i++)
                    {
                        param[i] = arr[i];
                    }                    
                }
                else
                {
                    param = param1;
                }                
            }

            return (DataTable)mo.Invoke(obj, param);
        }
        /// <summary>
        /// 根据总条数,和页条数,计算共有多少页.
        /// </summary>
        /// <param name="recordCount">总条数.</param>
        /// <param name="pageSize">一页多少条.</param>
        /// <returns></returns>
        public static int CalcPageCount(int recordCount, int pageSize)
        {
            int pageCount = 0;
            if (pageSize > 0)
            {
                pageCount = recordCount / Convert.ToInt32(pageSize);
                pageCount = recordCount % Convert.ToInt32(pageSize) > 0 ? pageCount + 1 : pageCount;
            }
            return pageCount;
        }
    }
}