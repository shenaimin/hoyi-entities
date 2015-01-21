/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.Database
{
    public class PublicService : baseExpert
    {
        public static PublicService Create() 
        {
            return new PublicService();
        }
        /// <summary>
        /// 根据表名,
        /// </summary>
        /// <param name="fpy_field">like中间的值，例如 like '%你好%'  则 fpy_field="你好"</param>
        /// <param name="maintable">主表名，例如要查询 bas_menu 内的name为like '%fpy_field%'的值</param>
        /// <param name="idname">id 的名字.</param>
        /// <param name="filterfield">几个查询字段的名称.</param>
        /// <returns></returns>
        public DataTable LikeByFPY(string fpy_field, string maintable, string idname, string[] filterfield)
        {
            string filter = "";
            foreach (string str in filterfield)
            {
                filter += " " + str + " like @fpy_field or";
            }
            filter = filter.TrimEnd("or".ToArray());

            string cmd = "select * from " + maintable + 
                            " where " + idname + " in(        " +
                            "    select menuid from " + maintable + "_fpy where " + filter +
                            ") limit 0, 20;";
            DbParameter[] parameters = {
					new MySqlParameter("@fpy_field", MySqlDbType.VarChar) };
            parameters[0].Value = fpy_field;

            return this.ExecuteTable(cmd, parameters.ToList(), false);
        }
    }
}
