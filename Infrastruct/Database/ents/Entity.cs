/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Infrastructure.Database.Att;
using Infrastructure.Database.conf;
using System.Data.Common;
using Infrastructure.Database.dbTransfer;
using System.Data;
using Infrastructure.Database.ctrl;
using Infrastructure.Database.Model;

namespace Infrastructure.Database.ents
{
    /// <summary>
    /// 实体的基类.
    /// </summary>
    [Serializable]
    public abstract class Entity : baseExpert, IEntity
    {

        public Entity() { }

        #region IEntity Members

        private bool _isPersisted = false;

        private string _tableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string getTableName()
        {
            if (_tableName == null)
            {

                Type type = this.GetType();

                object obj = Activator.CreateInstance(type);

                foreach (object p in type.GetCustomAttributes(true))
                {
                    if (p is EntityAttr)
                    {
                        _tableName = ((EntityAttr)p).TableName;
                    }
                }
            }
            return _tableName;
        }
        private string _entityComment;

        public string ToJson(params object[] filtername)
        {
            string flname = "";
            string json = "{";

            Type type = this.GetType();
            object obj = Activator.CreateInstance(type);
            foreach (PropertyInfo proin in type.GetProperties())
            {
                object[] prs = proin.GetCustomAttributes(true);
                if (prs.Length > 0)
                {
                    if (prs[0] is DbAttr)
                    {
                        flname = ((DbAttr)prs[0]).FieldName;
                        if (filtername.Count() == 0 || filtername.Contains(flname) || filtername.Where(s => ( (s is AttField) ?  (((AttField)s).fieldname.Equals(flname)) : s.ToString().Equals(flname))).Count() > 0)
                        {
                            object value = proin.GetValue(this, null);
                            json += "\"" + flname + "\":\"" + (value == null ? "" : value.ToString()) + "\",";
                        }
                    }
                }
            }
            json = json.TrimEnd(',');
            json += "}";
            return json;
        }

        public static string ToJson<T>(List<T> ents, params object[] filtername)
        {
            string json = "[";
            foreach (T ts in ents)
            {
                json += (ts as Entity).ToJson(filtername) + ",";
            }
            json = json.TrimEnd(',');
            json += "]";
            return json;
        }

        /// <summary>
        /// 实体名，一般为中文名.
        /// </summary>
        public string getEntityComment()
        {
            if (_entityComment == null)
            {
                Type type = this.GetType();

                object obj = Activator.CreateInstance(type);

                foreach (object p in type.GetCustomAttributes(true))
                {
                    if (p is EntityAttr)
                    {
                        _entityComment = ((EntityAttr)p).EntityName;
                    }
                }
            }
            return _entityComment;
        }


        /**/
        /// <summary>
        /// 该对象是否已持久化
        /// </summary>
        //public virtual bool IsPersisted
        //{
        //    get { return _isPersisted; }
        //    protected set { _isPersisted = value; }
        //}

        public static AttField GetFieldByName(Type EntityType, string fieldname)
        {
            try
            {
                object obj = Activator.CreateInstance(EntityType);

                FieldInfo fl = EntityType.GetField(fieldname);
                return fl.GetValue(obj) as AttField;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static AttField GetFieldByName<T>(string fieldname)
        {
            try
            {
                Type type = typeof(T);

                object obj = Activator.CreateInstance(type);

                FieldInfo fl = type.GetField(fieldname);
                return fl.GetValue(obj) as AttField;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private List<DbAttr> _attrs;
        /// <summary>
        /// DbAttr的属性.
        /// </summary>
        public List<DbAttr> attrs
        {
            get
            {
                if (_attrs == null)
                {
                    _attrs = new List<DbAttr>();
                    Type type = this.GetType();
                    //object obj = Activator.CreateInstance(type);
                    foreach (FieldInfo fd in type.GetFields())
                    {
                        object[] fds = fd.GetCustomAttributes(true);
                        if (fds.Length > 0)
                        {
                            if (fds[0] is DbAttr)
                            {
                                _attrs.Add(fds[0] as DbAttr);
                            }
                        }
                    }
                    foreach (PropertyInfo proin in type.GetProperties())
                    {
                        object[] prs = proin.GetCustomAttributes(true);
                        if (prs.Length > 0)
                        {
                            if (prs[0] is DbAttr)
                            {
                                _attrs.Add(prs[0] as DbAttr);
                            }
                        }
                    }
                }
                return _attrs;
            }
        }

        private List<PropertyInfo> _pros;
        /// <summary>
        /// 属性.
        /// </summary>
        public List<PropertyInfo> pros
        {
            get
            {
                if (_pros == null)
                {
                    _pros = new List<PropertyInfo>();
                    Type type = this.GetType();
                    object obj = Activator.CreateInstance(type);
                    foreach (PropertyInfo proin in type.GetProperties())
                    {
                        _pros.Add(proin);
                    }
                }
                return _pros;
            }
        }
        /// <summary>
        /// 拷贝一个实体.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Type type = this.GetType();

            object obj = Activator.CreateInstance(type);

            foreach (PropertyInfo p in type.GetProperties())
            {
                object value = p.GetValue(this, null);
                p.SetValue(obj, value, null);
            }

            return obj;
        }


        private string _entityId;
        /// <summary>
        /// 实体ID.
        /// </summary>
        public string GetEntityID()
        {
            return _entityId;
        }

        public void setEntityId(string EntityId)
        {
            _entityId = EntityId;
        }

        #endregion IEntity Members

        #region Entity Method
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <returns>成功返回受影响的条数，失败返回-1,报错信息，请查看Console</returns>
        public int Insert(IDatabase _database = null, bool addprimkey = false)
        {
            //try
            //{
            if (_database != null)
            {
                this.setDatabase(_database);
            }
            string cmd = TransFactory.Instance.InsertCmd(this, addprimkey);

            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, addprimkey);

            ConsoleDebug.WriteCmd(cmd, parameters);

            return this.ExecuteParamNonQuery(cmd, parameters);
            //}
            //catch (Exception ex)
            //{
            //    if (HOYIConf.OpenError)
            //    {
            //        Console.WriteLine("INSERT ERROR:" + ex.Message);
            //    }
            //    return -1;
            //}
        }

        /// <summary>
        /// 插入对象并且返回自增的ID.
        /// </summary>
        /// <returns></returns>
        public object Insert_RETURN_OBJID(IDatabase _database = null)
        {
            if (_database != null)
            {
                this.setDatabase(_database);
            }
            string cmd = TransFactory.Instance.InsertCmd(this) + "select LAST_INSERT_ID();";

            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, false);

            ConsoleDebug.WriteCmd(cmd, parameters);
            return Int32.Parse(this.ExecuteScalar(cmd, parameters).ToString());
        }
        /// <summary>
        /// 插入对象并且返回自增的ID.
        /// </summary>
        /// <returns></returns>
        public int Insert_RETURN_ID(IDatabase _database = null)
        {
            object rid = Insert_RETURN_OBJID(_database);
            return rid == null ? -1 : Int32.Parse(rid.ToString());
        }
        /// <summary>
        /// 插入对象并且返回自增的ID.
        /// </summary>
        /// <returns></returns>
        public Int64 Insert_RETURN_64ID(IDatabase _database = null)
        {
            object rid = Insert_RETURN_OBJID(_database);
            return rid == null ? -1 : Int64.Parse(rid.ToString());
        }
        /// <summary>
        /// 插入对象并且返回自增的ID.
        /// </summary>
        /// <returns></returns>
        public string Insert_RETURN_STRID(IDatabase _database = null)
        {
            object rid = Insert_RETURN_OBJID(_database);
            return rid == null ? "" : rid.ToString();
        }

        public int Delete(IDatabase _database = null)
        {
            if (_database != null)
            {
                this.setDatabase(_database);
            }
            string cmd = TransFactory.Instance.DeleteACmd(this);
            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, true);

            ConsoleDebug.WriteCmd(cmd, parameters);
            return this.ExecuteParamNonQuery(cmd, parameters);
        }
        /// <summary>
        /// 判断是否存在.
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            string strcmd = TransFactory.Instance.ExistsCmd(this);
            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, true);

            ConsoleDebug.WriteCmd(strcmd, parameters);
            DataTable dt = baseExpert.STA_ExecuteTable(strcmd, parameters, false);
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
        ///  不建议使用.
        /// </summary>
        /// <returns></returns>
        public int Update(IDatabase _database = null)
        {
            if (_database != null)
            {
                this.setDatabase(_database);
            }
            string cmd = TransFactory.Instance.UpdateCmd(this);
            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, true);

            ConsoleDebug.WriteCmd(cmd, parameters);
            return this.ExecuteParamNonQuery(cmd, parameters);
        }
        /// <summary>
        /// 不建议使用.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int Update(AttField[] fields, IDatabase _database = null)
        {
            if (_database != null)
            {
                this.setDatabase(_database);
            }
            string cmd = TransFactory.Instance.UpdateCmd(this, fields);
            List<DbParameter> parameters = TransFactory.Instance.AllParams(this, true);

            ConsoleDebug.WriteCmd(cmd, parameters);
            return this.ExecuteParamNonQuery(cmd, parameters);
        }

        #endregion Entity Method


        #region

        public static string[] Z_STR(params string[] param)
        {
            return param;
        }

        public static object[] Z_OBJ(params object[] param)
        {
            return param;
        }
        #endregion

        #region XIUXIU

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
        public void SetProValue(string proname, object val)
        {
            PropertyInfo pro = pros.Single(s => s.Name.ToLower().Equals(proname.ToLower()));
            if (pro != null)
            {
                pro.SetValue(this, val, null);
            }
        }

        public object GetProValue(string proname)
        {
            PropertyInfo pro = pros.Single(s => s.Name.Equals(proname));
            return pro != null ? pro.GetValue(this, null) : null;
        }

        #endregion XIUXIU

        #region IEntity

        /// <summary>
        /// 从Table转换到List<baseEntity>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> TransFromTable<T>(DataTable table)
        {
            List<T> ents = new List<T>();
            T ent;

            Type entityType = typeof(T);
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //int tmp;
            foreach (DataRow dr in table.Rows)
            {
                ent = Activator.CreateInstance<T>();
                foreach (DataColumn dc in table.Columns)
                {
                    foreach (PropertyInfo propInfo in entityProperties)
                    {
                        if (dc.ColumnName.ToLower().Equals(propInfo.Name.ToLower()))
                        {
                            try
                            {
                                propInfo.SetValue(ent, dr[dc], null);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                ents.Add(ent);
            }
            return ents;
        }

        /// <summary>
        /// 从Table转换到List<baseEntity>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<Entity> TransFromTable(Type entityType, DataTable table)
        {
            List<Entity> ents = new List<Entity>();
            Entity ent;

            PropertyInfo[] entityProperties = entityType.GetProperties();

            //int tmp;
            foreach (DataRow dr in table.Rows)
            {
                ent = Activator.CreateInstance(entityType) as Entity;
                foreach (DataColumn dc in table.Columns)
                {
                    foreach (PropertyInfo propInfo in entityProperties)
                    {
                        if (dc.ColumnName.ToLower().Equals(propInfo.Name.ToLower()))
                        {
                            propInfo.SetValue(ent, dr[dc], null);
                        }
                    }
                }
                ents.Add(ent);
            }
            return ents;
        }
        /// <summary>
        /// 将一个列表转换成DataTable,如果列表为空将返回空的DataTable结构
        /// </summary>
        /// <typeparam name="T">要转换的数据类型</typeparam>
        /// <param name="entityList">实体对象列表</param> 
        public static DataTable EntityListToDataTable<T>(List<T> entityList)
        {
            DataTable dt = new DataTable();

            //取类型T所有Propertie
            Type entityType = typeof(T);
            PropertyInfo[] entityProperties = entityType.GetProperties();
            Type colType = null;
            foreach (PropertyInfo propInfo in entityProperties)
            {

                if (propInfo.PropertyType.IsGenericType)
                {
                    colType = Nullable.GetUnderlyingType(propInfo.PropertyType);
                }
                else
                {
                    colType = propInfo.PropertyType;
                }

                if (colType.FullName.StartsWith("System"))
                {
                    dt.Columns.Add(propInfo.Name, colType);
                }
            }

            if (entityList != null && entityList.Count > 0)
            {
                foreach (T entity in entityList)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (PropertyInfo propInfo in entityProperties)
                    {
                        if (dt.Columns.Contains(propInfo.Name))
                        {
                            object objValue = propInfo.GetValue(entity, null);
                            newRow[propInfo.Name] = objValue == null ? DBNull.Value : objValue;
                        }
                    }
                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
        #endregion Entity
    }
}