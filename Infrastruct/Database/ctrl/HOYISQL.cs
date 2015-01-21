/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.Att;
using Infrastructure.Database.conf;
using Infrastructure.Database.dbTransfer;
using Infrastructure.Database.ents;
using Infrastructure.Database.Model;
using Infrastructure.Database.Pager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Infrastructure.Database.ctrl
{
    public static class HOYISQL
    {
        /// <summary>
        /// 获取参数里的字段名.
        /// 例如: "userid", 4, "username", "qq"
        /// 里的, "userid", "username"
        /// </summary>
        /// <param name="paramter"></param>
        /// <returns></returns>
        public static List<string> GetNames(params object[] paramter)
        {
            List<string> names = new List<string>();
            List<Object> values = new List<object>();

            for (int i = 0; i < paramter.Length; i++)
            {
                if (i % 2 == 0)
                {
                    names.Add(paramter[i].ToString());
                }
                else
                {
                    values.Add(paramter[i]);
                }
            }
            return names;
        }

        /// <summary>
        /// 
        /// 获取参数里的字段名.
        /// 例如: "userid", 4, "username", "qq"
        /// 里的, 4, "qq"
        /// </summary>
        /// <param name="paramter"></param>
        /// <returns></returns>
        public static List<object> GetObjects(params object[] paramter)
        {
            List<string> names = new List<string>();
            List<Object> values = new List<object>();

            for (int i = 0; i < paramter.Length; i++)
            {
                if (i % 2 == 0)
                {
                    names.Add(paramter[i].ToString());
                }
                else
                {
                    values.Add(paramter[i]);
                }
            }
            return values;
        }

        public static string GetTableName(this Type entityType)
        {
            string utablename = "";
            foreach (object p in entityType.GetCustomAttributes(true))
            {
                if (p is EntityAttr)
                {
                    utablename = ((EntityAttr)p).TableName;
                }
            }
            return utablename;
        }

        public static HOYICMD WhereOr(this HOYICMD cmd, FILTER parameter)
        {
            return FILWHERE(cmd, " or ", parameter);
        }

        public static HOYICMD WhereAnd(this HOYICMD cmd, FILTER parameter)
        {
            return FILWHERE(cmd, " and ", parameter);
        }

        public static HOYICMD FILWHERE(this HOYICMD cmd, string contract, FILTER parameter)
        {
            if (parameter != null && parameter.value is HOYICMD)
            {
                HOYICMD pvalue = parameter.value as HOYICMD;
                if (pvalue != null && pvalue.addedfield != null && pvalue.addedfield.Count > 0)
                {
                    if (cmd.addedfield == null)
                    {
                        cmd.addedfield = new List<string>();
                    }
                    cmd.addedfield.AddRange(pvalue.addedfield);
                }
                if (pvalue != null && pvalue.paraaddedfield != null && pvalue.paraaddedfield.Count > 0)
                {
                    if (cmd.paraaddedfield == null)
                    {
                        cmd.paraaddedfield = new List<string>();
                    }
                    cmd.paraaddedfield.AddRange(pvalue.paraaddedfield);
                }
            }
            List<string> names = new List<string>();
            List<string> operates = new List<string>();
            List<Object> values = new List<object>();

            List<string> nextops = new List<string>();
            List<string> filtertype = new List<string>(); // 0 为 普通Filter, 1 为timefilter

            FILTER tmp = parameter;

            while (tmp != null)
            {
                names.Add(tmp.filter);
                operates.Add(tmp.OPERATES);
                if (tmp.value is DateTime)
                {
                    values.Add(((DateTime)tmp.value).ToString());
                }
                else if (tmp.value is DateONLY)
                {
                    values.Add(((DateONLY)tmp.value).date);
                }
                else
                {
                    values.Add(tmp.value);
                }

                nextops.Add(tmp.PreOps);
                if (tmp is DateTimeFILTER)
                {
                    filtertype.Add("1");
                }
                else if (tmp is DateDiffFILTER)
                {
                    filtertype.Add("2");
                }
                else
                {
                    filtertype.Add("0");
                }

                tmp = tmp.Pre;
            }

            names = DDSC(names);
            operates = DDSC(operates);
            values = DDSC(values);
            nextops = DDSC(nextops);
            filtertype = DDSC(filtertype);

            string cccmd = TransFactory.Instance.InitCmd(names.ToArray(), operates.ToArray(), values.ToArray(), nextops.ToArray(), filtertype.ToArray(), cmd);

            if (cccmd.Replace("(", "").Replace(")", "").Trim().Length > 0)
            {
                if (string.IsNullOrEmpty(cmd.F_Where))
                {
                    cmd.F_Where = " (" + cccmd + ")";
                }
                else
                {
                    cmd.F_Where += contract + " (" + cccmd + ")";
                }
            }

            //cmd.F_Where = cmd.F_Where + " " + (cccmd.Replace("(", "").Replace(")", "").Trim().Length > 0 ? contract + " (" + cccmd + ")" : "");
            cmd.parameter = TransFactory.Instance.InitParams(cmd.EntType, names.ToArray(), operates.ToArray(), values.ToArray(), cmd);

            return cmd;
        }

        public static HOYICMD Where(this HOYICMD cmd, FILTER parameter)
        {
            return FILWHERE(cmd, "", parameter);
        }

        public static int Update(this HOYICMD cmd, params FILTER[] parameters)
        {
            return Update(cmd, null, parameters);
        }

        public static int Update(this HOYICMD cmd, IDatabase _database = null, params FILTER[] parameters)
        {
            List<string> names = new List<string>();
            List<string> operates = new List<string>();
            List<Object> values = new List<object>();

            List<string> nextops = new List<string>();
            List<string> filtertype = new List<string>();

            foreach (FILTER fds in parameters)
            {
                names.Add(fds.filter);
                operates.Add(fds.OPERATES);
                values.Add(fds.value);
                nextops.Add(fds.PreOps);

                if (fds is DateTimeFILTER)
                {
                    filtertype.Add("1");
                }
                else if (fds is DateDiffFILTER)
                {
                    filtertype.Add("2");
                }
                else
                {
                    filtertype.Add("0");
                }
            }

            cmd.F_UPDATE = cmd.F_UPDATE + TransFactory.Instance.InitCmd(names.ToArray(), operates.ToArray(), values.ToArray(), nextops.ToArray(), filtertype.ToArray(), cmd);
            cmd.parameter = TransFactory.Instance.InitParams(cmd.EntType, names.ToArray(), operates.ToArray(), values.ToArray(), cmd);

            string tablename = cmd.EntType.GetTableName();
            string strcmd = "update " + tablename + " set " + cmd.F_UPDATE;
            strcmd += cmd.ContractCMD();

            ConsoleDebug.WriteCmd(strcmd, cmd);
            return baseExpert.STA_ExecuteParamNonQuery(strcmd, cmd.parameter, _database);
        }

        public static int Delete(this HOYICMD cmd, IDatabase _database = null)
        {
            string tablename = cmd.EntType.GetTableName();
            string strcmd = "delete from " + tablename;
            strcmd += cmd.ContractCMD();

            ConsoleDebug.WriteCmd(strcmd, cmd);
            return baseExpert.STA_ExecuteParamNonQuery(strcmd, cmd.parameter, _database);
        }

        #region DataCTRL
        public static HOYICMD DataCount(this HOYICMD cmd, int DataCount)
        {
            cmd.DataCount = DataCount;
            cmd.CalcPageSize();
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }
        /// <summary>
        /// 单页条数.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static HOYICMD PgSize(this HOYICMD cmd, int pagesize)
        {
            cmd.PageSize = pagesize;
            cmd.CalcPageSize();
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }
        /// <summary>
        /// 取pageindex内的数据.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static HOYICMD Jump(this HOYICMD cmd, int pageindex)
        {
            cmd.pageIndex = pageindex;
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }

        public static HOYICMD DataCount(this HOYICMD cmd, string DataCount)
        {
            cmd.DataCount = Int32.Parse(DataCount);
            cmd.CalcPageSize();
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }
        /// <summary>
        /// 单页条数.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static HOYICMD PgSize(this HOYICMD cmd, string pagesize)
        {
            cmd.PageSize = Int32.Parse(pagesize);
            cmd.CalcPageSize();
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }
        /// <summary>
        /// 取pageindex内的数据.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static HOYICMD Jump(this HOYICMD cmd, string pageindex)
        {
            cmd.pageIndex = Int32.Parse(pageindex);
            cmd.F_Limit = cmd.CalcLimit();
            return cmd;
        }
        /// <summary>
        /// 传入 IPagingDataInfo 自动计算分页情况.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pgd"></param>
        /// <returns></returns>
        public static HOYICMD PgInfo(this HOYICMD cmd, IPagingDataInfo pgd)
        {
            return cmd.PgSize(pgd.PageSize).Jump(pgd.PageIndex).Order(pgd.SortedFields);
        }
        /// <summary>
        /// 传入 IPagingDataInfo 自动计算分页情况.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pgd"></param>
        /// <param name="DataCount">数据条数.</param>
        /// <returns></returns>
        public static HOYICMD PgInfo(this HOYICMD cmd, IPagingDataInfo pgd, int DataCount)
        {
            return cmd.DataCount(DataCount).PgSize(pgd.PageSize).Jump(pgd.PageIndex).Order(pgd.SortedFields);
        }
        /// <summary>
        /// 传入 IPagingDataInfo 自动计算分页情况.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pgd"></param>
        /// <param name="DataCount">数据条数.</param>
        /// <returns></returns>
        public static HOYICMD PgInfo(this HOYICMD cmd, IPagingDataInfo pgd, string DataCount)
        {
            return cmd.DataCount(DataCount).PgSize(pgd.PageSize).Jump(pgd.PageIndex).Order(pgd.SortedFields);
        }
        #endregion DATACTRL

        #region ORDER,LIMIT


        public static HOYICMD Limit(this HOYICMD cmd, params int[] parameter)
        {
            if (parameter != null && parameter.Length > 0)
                cmd.F_Limit = " " + parameter[0].ToString() + "," + parameter[1].ToString();
            return cmd;
        }

        public static HOYICMD Order(this HOYICMD cmd, string order)
        {
            if (order != null && order.Length > 0)
            {
                if (cmd.F_Order != null && cmd.F_Order.Trim().Length > 0)
                {
                    cmd.F_Order += ",";
                }
                cmd.F_Order += " " + order;
            }
            return cmd;
        }

        public static HOYICMD Order(this HOYICMD cmd, string order, bool isasc)
        {
            if (order != null && order.Length > 0)
            {
                if (cmd.F_Order != null && cmd.F_Order.Trim().Length > 0)
                {
                    cmd.F_Order += ",";
                }
                cmd.F_Order += " " + order + (isasc ? " asc " : " desc ");
            }
            return cmd;
        }

        public static HOYICMD ASC(this HOYICMD cmd, AttField parameter)
        {
            return Order(cmd, parameter.fieldname, true);
        }

        public static HOYICMD Desc(this HOYICMD cmd, AttField parameter)
        {
            return Order(cmd, parameter.fieldname, false);
        }
        public static HOYICMD ASC(this HOYICMD cmd, params string[] parameter)
        {
            return Order(cmd, parameter[0], true);
        }

        public static HOYICMD Desc(this HOYICMD cmd, params string[] parameter)
        {
            return Order(cmd, parameter[0], false);
        }
        public static List<string> DDSC(List<string> strs)
        {
            List<string> sts = new List<string>();
            for (int i = 0; i < strs.Count; i++)
            {
                sts.Add(strs[strs.Count - 1 - i]);
            }
            return sts;
        }

        public static List<object> DDSC(List<object> strs)
        {
            List<object> sts = new List<object>();
            for (int i = 0; i < strs.Count; i++)
            {
                sts.Add(strs[strs.Count - 1 - i]);
            }
            return sts;
        }

        #endregion ORDER,LIMIT

        /// <summary>
        /// 查询第一行第一列的内容.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string Select_RC0(this HOYICMD cmd, params object[] parameter)
        {
            DataTable dt = Select(cmd, parameter);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return null;
        }

        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static HOYICMD Select_CMD(this HOYICMD cmd, params object[] parameter)
        {
            string fields = "*";
            string tablename = cmd.EntType.GetTableName();
            if (parameter != null && parameter.Length > 0)
                fields = string.Join(",", parameter);
            string strcmd = "select " + fields + " from " + tablename;
            strcmd += cmd.ContractCMD();
            cmd.selectCmd = strcmd;


            return cmd;
        }

        public static bool Exists(this HOYICMD cmd, params object[] parameter)
        {
            string fields = "count(*)";
            string tablename = cmd.EntType.GetTableName();
            if (parameter.Count() > 0)
            {
                fields = " count(" + parameter[0].ToString() + ") ";
            }
            string strcmd = "select " + fields + " from " + tablename;
            strcmd += cmd.ContractCMD();


            ConsoleDebug.WriteCmd(strcmd, cmd);

            DataTable dt = baseExpert.STA_ExecuteTable(strcmd, cmd.parameter, false);
            if (dt == null)
            {
                return false;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    return Int32.Parse(dt.Rows[0][0].ToString()) > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataTable Select(this HOYICMD cmd, params object[] parameter)
        {
            string fields = "*";
            string tablename = cmd.EntType.GetTableName();
            if (parameter != null && parameter.Length > 0)
                fields = string.Join(",", parameter);
            string strcmd = "select " + fields + " from " + tablename;
            strcmd += cmd.ContractCMD();

            ConsoleDebug.WriteCmd(strcmd, cmd);
            return baseExpert.STA_ExecuteTable(strcmd, cmd.parameter, false);
        }
        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataTable Select(this HOYICMD cmd, ref int pgcount, params object[] parameter)
        {
            pgcount = cmd.PgCount;
            return Select(cmd, parameter);
        }
        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataTable Select(this HOYICMD cmd, ref int pgcount, ref int pgindex, params object[] parameter)
        {
            pgcount = cmd.PgCount;
            pgindex = cmd.pageIndex;
            return Select(cmd, parameter);
        }
        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<T> Select<T>(this HOYICMD cmd, params object[] parameter)
        {
            DataTable dt = Select(cmd, parameter);
            return Entity.TransFromTable<T>(dt);
        }
        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<T> Select<T>(this HOYICMD cmd, ref int pgcount, params object[] parameter)
        {
            pgcount = cmd.PgCount;
            DataTable dt = Select(cmd, parameter);
            return Entity.TransFromTable<T>(dt);
        }
        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// DT 返回表格
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<T> Select<T>(this HOYICMD cmd, ref int pgcount, ref int pgindex, params object[] parameter)
        {
            pgcount = cmd.PgCount;
            pgindex = cmd.pageIndex;
            DataTable dt = Select(cmd, parameter);
            return Entity.TransFromTable<T>(dt);
        }
        /// <summary>
        /// 返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static T First<T>(this HOYICMD cmd, params object[] parameter) where T : class
        {
            List<T> ts = Select<T>(cmd, parameter);
            return (ts != null && ts.Count > 0) ? ts[0] : null;
        }
        /// <summary>
        /// 返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static T Last<T>(this HOYICMD cmd, params object[] parameter) where T : class
        {
            List<T> ts = Select<T>(cmd, parameter);
            return (ts != null && ts.Count > 0) ? ts[ts.Count - 1] : null;
        }

        /// <summary>
        /// 根据条件，查询数据.
        /// 如果不输入参数，则是拿出所有的字段.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int Count(this HOYICMD cmd, params object[] parameter)
        {
            string fields = "count(*)";
            string tablename = cmd.EntType.GetTableName();
            if (parameter != null && parameter.Length > 0)
                fields = "count(" + parameter[0] + ")";
            string strcmd = "select " + fields + " from " + tablename;
            strcmd += cmd.ContractCMD();

            ConsoleDebug.WriteCmd(strcmd, cmd);
            DataTable dt = baseExpert.STA_ExecuteTable(strcmd, cmd.parameter, false);

            return dt.Rows.Count > 0 ? Int32.Parse(dt.Rows[0][0].ToString()) : 0;
        }
        /// <summary>
        /// select distinct(parameter) from table.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataTable Distinct(this HOYICMD cmd, params object[] parameter)
        {
            if (parameter.Count() > 0)
            {
                string fields = "distinct(*)";
                string tablename = cmd.EntType.GetTableName();
                if (parameter != null && parameter.Length > 0)
                    fields = "distinct(" + parameter[0] + ")";
                string strcmd = "select " + fields + " from " + tablename;
                strcmd += cmd.ContractCMD();

                ConsoleDebug.WriteCmd(strcmd, cmd);
                return baseExpert.STA_ExecuteTable(strcmd, cmd.parameter, false);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// select distinct(parameter) from table.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<T> Distinct<T>(this HOYICMD cmd, params object[] parameter)
        {
            DataTable dt = Distinct(cmd, parameter);
            return Entity.TransFromTable<T>(dt);

        }

        /// <summary>
        /// select count(distinct(parameter)) from table.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int DistinctCount(this HOYICMD cmd, params object[] parameter)
        {
            if (parameter.Count() > 0)
            {
                string fields = "count(distinct(*))";
                string tablename = cmd.EntType.GetTableName();
                if (parameter != null && parameter.Length > 0)
                    fields = "count(distinct(" + parameter[0] + "))";
                string strcmd = "select " + fields + " from " + tablename;
                strcmd += cmd.ContractCMD();

                ConsoleDebug.WriteCmd(strcmd, cmd);
                DataTable dt = baseExpert.STA_ExecuteTable(strcmd, cmd.parameter, false);

                return dt.Rows.Count > 0 ? Int32.Parse(dt.Rows[0][0].ToString()) : 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 组合命令.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string ContractCMD(this HOYICMD cmd)
        {
            string strcmd = "";
            if (cmd.F_Where != null && cmd.F_Where.Trim().Length > 0)
            {
                if (cmd.F_Where.Replace("(", "").Replace(")", "").Trim().Length > 0)
                {
                    strcmd += " where " + cmd.F_Where;
                }
            }
            if (cmd.F_Order != null && cmd.F_Order.Trim().Length > 0)
                strcmd += " order by " + cmd.F_Order;
            if (cmd.F_Limit != null && cmd.F_Limit.Trim().Length > 0)
                strcmd += " limit " + cmd.F_Limit;
            return strcmd;
        }

        /// <summary>
        /// 将 DataTable 转换为 Json.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt) {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling 
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{\"totalCount\":\"" + dt.Rows.Count.ToString() + "\",");
                JsonString.Append("\"JsonData\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
