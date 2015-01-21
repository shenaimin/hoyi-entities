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

namespace Infrastructure.Database.ents
{
    [Serializable]
    public class EntityAttr : Attribute
    {
        public EntityAttr() { }
        public string TableName { get; set; }
        public string EntityName { get; set; }

        public EntityAttr(string _tableName, string _entityName)
        {
            TableName = _tableName;
            EntityName = _entityName;
        }
    }
}