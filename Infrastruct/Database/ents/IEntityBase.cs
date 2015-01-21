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
using System.Data;
using System.Reflection;

namespace Infrastructure.Database.ents
{
    /// <summary>
    /// 实体对象基础类，用于实体与DataTable之间的转换.
    /// </summary>
    public abstract class IEntityBase
    {
        /// <summary>
        /// 构造.
        /// </summary>
        public IEntityBase()
        { 
            
        }
        /// <summary>
        /// 表能否转换成List<IEntityBase> 转换条件为表内的字段
        /// 在实体中都能对上号。如果对不上的则不加入。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CanTrans(DataTable dt)
        {
            return true;
        }
        /// <summary>
        /// 将表内容转换成List集合.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<IEntityBase> TransDataTable(DataTable dt)
        {
            List<IEntityBase> entitys = new List<IEntityBase>();


            foreach (DataRow dr in dt.Rows)
            {
                Type type = this.GetType();
                IEntityBase entity = Activator.CreateInstance(type) as IEntityBase;

                foreach (PropertyInfo p in type.GetProperties())
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (p.Name.ToUpper() == dt.Columns[i].ColumnName.ToUpper())
                        {
                            object obj = dr.ItemArray[i];
                            p.SetValue(entity, obj, null);
                        }
                    }

                }
                entitys.Add(entity);
            }
            return entitys;
        }
    }
}
