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
using Infrastructure.Database.ctrl;
using Infrastructure.Database.ents;
using Infrastructure.Database.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.Database.dbTransfer
{
    /// <summary>
    /// Mysql 语法.
    /// </summary>
    public class MySqlTransfer : ITransfer
    {
        /// <summary>
        /// 根据Filter获取Where的列表.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string InitCmd(string[] filter, string[] operates, object[] values, string[] nextops, string[] filtertype, HOYICMD cmd)
        {
            string cmdwhere = "";

            string wh = "", op = "", nextop = "", paraname = "", filtertp = "";
            object value = null;
            int tmpxx = 0;
            Date tmpdate;
            List<string> addedfield = cmd.addedfield == null ? new List<string>() : cmd.addedfield;
            for (int i = 0; i < filter.Length; i++)
            {
                wh = filter[i];
                op = operates[i];
                nextop = i >= 0 ? nextops[i] : "";
                value = values[i];
                filtertp = filtertype[i];

                if (value is HOYICMD)
                {
                    switch (op)
                    {
                        case "cmdin":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + " in (" + (value as HOYICMD).selectCmd + ") ";
                            break;
                        case "cmdnotin":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + " not in (" + (value as HOYICMD).selectCmd + ") ";
                            break;
                        default:
                            break;
                    }
                }
                else if (value is AttField)
                {
                    tmpxx = addedfield.Where(s => s.Equals(wh)).Count();
                    paraname = "@" + wh + tmpxx;

                    addedfield.Add(wh);

                    switch (op)
                    {
                        case "equals":
                            //cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + paraname;
                            //break;

                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') = STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else if (filtertp == "2")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') = STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                            }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + ((AttField)value).fieldname;
                            }
                            break;
                        case "except":
                            //cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "!=" + paraname;
                            //break;

                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') != STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else if (filtertp == "2")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') != STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                            }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "!=" + ((AttField)value).fieldname;
                            }
                            break;
                    }
                }
                else
                {
                    tmpxx = addedfield.Where(s => s.Equals(wh)).Count();
                    paraname = "@" + wh + tmpxx;

                    addedfield.Add(wh);

                    switch (op)
                    {
                        case "indate":
                            if (filtertp == "2")
                            {
                                tmpdate = (Date)value;
                                switch (tmpdate.datetype)
                                {
                                    case DateType.TODAY:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "to_days(" + wh + ") = to_days(now())";
                                        break;
                                    case DateType.YESTERDAY:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "DATE_SUB(CURDATE(), INTERVAL 1 DAY) = date(" + wh + ")";
                                        break;
                                    case DateType.BEFORE_YESTERDAY:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "DATE_SUB(CURDATE(), INTERVAL 2 DAY) = date(" + wh + ")";
                                        break;
                                    case DateType.TOMORROW:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "DATE_SUB(CURDATE(), INTERVAL -1 DAY) = date(" + wh + ")";
                                        break;
                                    case DateType.ONEWEEK:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "YEARWEEK(date_format(" + wh + ",'%Y-%m-%d')) = YEARWEEK(now())";
                                        break;
                                    case DateType.ONEMONTH:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "DATE_FORMAT(" + wh + ", '%Y%m') = DATE_FORMAT(CURDATE(), '%Y%m')";
                                        break;
                                    case DateType.ONEYEAR:
                                        cmdwhere += ConAndOr(cmdwhere, nextop) + "DATE_FORMAT(" + wh + ", '%Y') = DATE_FORMAT(CURDATE(), '%Y')";
                                        break;
                                    case DateType.CUSDAY:
                                        break;
                                    case DateType.UNKNOWN:
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case "equals":
                            //cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + paraname;
                            //break;

                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') = STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else if (filtertp == "2")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') = STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                            }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + paraname;
                            }
                            break;
                        case "except":
                            //cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "!=" + paraname;
                            //break;
                            
                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') != STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else
                                if (filtertp == "2")
                                {
                                    cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') != STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                                }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "!=" + paraname;
                            }
                            break;
                        case "-=":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + wh + "-" + paraname;
                            break;
                        case "+=":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "=" + wh + "+" + paraname;
                            break;
                        case "in":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "in (" + paraname + ")";
                            break;
                        case "%":
                            cmdwhere += ConAndOr(cmdwhere, nextop) + wh + " like " + paraname;
                            break;
                        case "/":
                            if (value != null && value.ToString().Length > 0)
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + " like " + paraname;
                            }
                            break;
                        case ">=":
                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') >= STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else
                                if (filtertp == "2")
                                {
                                    cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') >= STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                                }
                            else
                            {
                                cmdwhere += ", " + wh + "=" + paraname;
                            }
                            break;
                        case "<=":
                            if (filtertp == "1")
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') <= STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else
                                if (filtertp == "2")
                                {
                                    cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') <= STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                                }
                            else
                            {
                                cmdwhere += ", " + wh + "=" + paraname;
                            }
                            break;
                        case ">":
                            if (filtertp == "1")
                            {
                                //STR_TO_DATE( createtime  ,'%Y/%m/%d %H:%i:%s')  > STR_TO_DATE('2014/12/8 10:02:40','%Y/%m/%d %H:%i:%s')

                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') > STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else
                                if (filtertp == "2")
                                {
                                    //STR_TO_DATE( createtime  ,'%Y/%m/%d %H:%i:%s')  > STR_TO_DATE('2014/12/8 10:02:40','%Y/%m/%d %H:%i:%s')

                                    cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') > STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                                }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + ">" + paraname;
                            }
                            break;
                        case "<":
                            if (filtertp == "1")
                            {
                                //STR_TO_DATE( createtime  ,'%Y/%m/%d %H:%i:%s')  > STR_TO_DATE('2014/12/8 10:02:40','%Y/%m/%d %H:%i:%s')

                                cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d %H:%i:%s') < STR_TO_DATE(" + paraname + ",'%Y/%m/%d %H:%i:%s')";
                            }
                            else
                                if (filtertp == "2")
                                {
                                    //STR_TO_DATE( createtime  ,'%Y/%m/%d %H:%i:%s')  > STR_TO_DATE('2014/12/8 10:02:40','%Y/%m/%d %H:%i:%s')

                                    cmdwhere += ConAndOr(cmdwhere, nextop) + "STR_TO_DATE(" + wh + ",'%Y/%m/%d') < STR_TO_DATE(" + paraname + ",'%Y/%m/%d')";
                                }
                            else
                            {
                                cmdwhere += ConAndOr(cmdwhere, nextop) + wh + "<" + paraname;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            cmdwhere = cmdwhere.TrimStart(',');
            cmd.addedfield = addedfield;
            return cmdwhere;
        }
        /// <summary>
        /// 根据 &,| 操作符号返回连接字符串.
        /// </summary>
        /// <param name="ops"></param>
        /// <returns></returns>
        public string ConAndOr(string cmdwhere, string ops)
        {
            if (cmdwhere.Trim().Length <= 0)
                return "";
            switch (ops)
            {
                case "&":
                    return " and ";
                case "|":
                    return " or ";
                default:
                    break;
            }
            return "";
        }
        /// <summary>
        /// 根据Filter和Values获取参数列表.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filter"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<DbParameter> InitParams(Entity entity, string[] filter, string[] operates, object[] values,HOYICMD cmd)
        {
            return this.InitParams(entity.GetType(), filter, operates, values, cmd);
        }

        public List<DbParameter> InitParams<T>(T entity, string[] filter, string[] operates, object[] values,HOYICMD cmd)
        {
            return this.InitParams(entity, filter,operates, values, cmd);
        }

        public List<DbParameter> InitParams(Type entityType, string[] filter, string[] operates, object[] values, HOYICMD cmd)
        {
            PropertyInfo[] entityProperties = entityType.GetProperties();

            List<DbParameter> parameters = cmd.parameter == null ? new List<DbParameter>() : cmd.parameter;
            MySqlParameter tmpparas;
            //MySqlDbType dbType;
            DbAttr tmpattr;

            int tmpxx = 0;
            List<string> addedfield = cmd.paraaddedfield == null ? new List<string>() : cmd.paraaddedfield;
            string op;
            object value;
            HOYICMD tmpcmd;
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    for (int i = 0; i < filter.Length; i++)
                    {
                        if (filter[i].Equals(tmpattr.FieldName))
                        {
                            op = operates[i];
                            value = values[i];

                            if (value is HOYICMD)
                            {
                                tmpcmd = value as HOYICMD;
                                switch (op)
                                {
                                    case "cmdin":
                                        cmd.parameter.AddRange(tmpcmd.parameter);
                                        break;
                                    case "cmdnotin":
                                        cmd.parameter.AddRange(tmpcmd.parameter);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (value is AttField)
                            {
                                addedfield.Add(tmpattr.FieldName);
                            }
                            else
                            {

                                tmpxx = addedfield.Where(s => s.Equals(tmpattr.FieldName)).Count();

                                if (op.Equals("%") || op.Equals("/"))
                                {
                                    if (op.Equals("/"))
                                    {
                                        if (value != null && value.ToString().Length > 0)
                                        {
                                            tmpparas = new MySqlParameter("@" + tmpattr.FieldName + tmpxx.ToString(), "%" + values[i] + "%");
                                            parameters.Add(tmpparas);
                                            addedfield.Add(tmpattr.FieldName);
                                        }
                                    }
                                    else
                                    {
                                        tmpparas = new MySqlParameter("@" + tmpattr.FieldName + tmpxx.ToString(), "%" + values[i] + "%");
                                        parameters.Add(tmpparas);
                                        addedfield.Add(tmpattr.FieldName);
                                    }
                                }
                                else
                                {
                                    tmpparas = new MySqlParameter("@" + tmpattr.FieldName + tmpxx.ToString(), values[i]);
                                    parameters.Add(tmpparas);
                                    addedfield.Add(tmpattr.FieldName);
                                }
                            }
                        }
                    }
                }
            }
            cmd.paraaddedfield = addedfield;

            return parameters;
        }

        public string DeleteACmd(Entity entity)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            DbAttr tmpattr;
            string fields = "";
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    //if (!tmpattr.Identity)
                    //{
                        fields += tmpattr.FieldName + " = @" + tmpattr.FieldName + " and ";
                    //}
                }
            }
            fields = fields.Length > 0 ? " where " + fields.Substring(0, fields.Length - 4) : "";
            return "delete from " + entity.getTableName() + fields;
        }

        public string UpdateCmd(Entity entity)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            DbAttr tmpattr;
            string fields = "",  primary= "";
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    if (tmpattr.Identity)
                    {
                        primary += tmpattr.FieldName + " = @" + tmpattr.FieldName + " and ";
                    }
                    else
                    {
                        fields += tmpattr.FieldName + " = @" + tmpattr.FieldName + ", ";
                    }
                }
            }
            primary = primary.Length > 0 ? primary.Substring(0, primary.Length - 4) : "";
            fields = fields.Length > 0 ?  fields.Substring(0, fields.Length - 2) : "";
            return "update " + entity.getTableName()+ " set " + fields + " where " + primary;
        }
        /// <summary>
        /// 根据实体的条件来更新.即条件除外的不会更新.
        /// 如果当前条件跟其他条件冲突，则更新有问题，这种更新建议使用非实例化后的方法更新.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attfield"></param>
        /// <returns></returns>
        public string UpdateCmd(Entity entity, string[] attfield)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            DbAttr tmpattr;
            string fields = "", primary = "";
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    fields += tmpattr.FieldName + " = @" + tmpattr.FieldName + ", ";
                }
            }
            foreach (string item in attfield)
            {
                primary += item + " = @" + item + " and ";
            }

            primary = primary.Length > 0 ? primary.Substring(0, primary.Length - 4) : "";
            fields = fields.Length > 0 ? fields.Substring(0, fields.Length - 2) : "";
            return "update " + entity.getTableName() + " set " + fields + " where " + primary;
        }
        /// <summary>
        /// 根据实体的条件来更新.即条件除外的不会更新.
        /// 如果当前条件跟其他条件冲突，则更新有问题，这种更新建议使用非实例化后的方法更新.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attfield"></param>
        /// <returns></returns>
        public string UpdateCmd(Entity entity, AttField[] attfield)
        {
            string[] strs = attfield.Select(s => s.fieldname).ToArray();
            return UpdateCmd(entity, strs);
        }

        public string InsertCmd(Entity entity, bool addprimkey = false)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            DbAttr tmpattr;
            string fields = "", paravalues = "";
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    if (!tmpattr.Identity)
                    {
                        fields += tmpattr.FieldName + ",";
                        paravalues += "@" + tmpattr.FieldName + ",";
                    }
                    else {
                        if (addprimkey)
                        {
                            fields += tmpattr.FieldName + ",";
                            paravalues += "@" + tmpattr.FieldName + ",";
                        }
                    }
                }
            }
            fields = fields.TrimEnd(',');
            paravalues = paravalues.TrimEnd(',');
            return "insert into " + entity.getTableName() + " (" + fields + ") " + " values (" + paravalues + ");";
        }

        public List<DbParameter> AllParams(Entity entity, bool containidentity)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            List<DbParameter> parameters = new List<DbParameter>();
            MySqlParameter tmpparas;
            object val;
            DbAttr tmpattr;
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    if (tmpattr.Identity)
                    {
                        if (containidentity)
                        {
                            val = pro.GetValue(entity, null);
                            tmpparas = new MySqlParameter("@" + tmpattr.FieldName, val);
                            parameters.Add(tmpparas);
                        }
                    }
                    else
                    {
                        val = pro.GetValue(entity, null);
                        tmpparas = new MySqlParameter("@" + tmpattr.FieldName, val);
                        parameters.Add(tmpparas);
                    }
                }
            }
            return parameters;
        }

        public string ExistsCmd(Entity entity)
        {
            PropertyInfo[] entityProperties = entity.GetType().GetProperties();

            DbAttr tmpattr;
            string fields = "";
            foreach (PropertyInfo pro in entityProperties)
            {
                object[] prs = pro.GetCustomAttributes(true);
                if (prs.Length > 0 && prs[0] is DbAttr)
                {
                    tmpattr = prs[0] as DbAttr;
                    //if (!tmpattr.Identity)
                    //{
                    fields += tmpattr.FieldName + " = @" + tmpattr.FieldName + " and ";
                    //}
                }
            }
            fields = fields.Length > 0 ? " where " + fields.Substring(0, fields.Length - 4) : "";
            return "select count(*) from " + entity.getTableName() + fields;
        }
    }
}
