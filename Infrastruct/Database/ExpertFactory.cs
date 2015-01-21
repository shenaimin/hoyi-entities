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

namespace Infrastructure.Database
{
    public class ExpertFactory
    {
        public static baseExpert New(Type tp)
        {
            baseExpert be = (baseExpert)Activator.CreateInstance(tp);
            return be;
        }
        public static baseExpert New(Type tp, IDatabase _database)
        {
            baseExpert be = (baseExpert)Activator.CreateInstance(tp);
            be.setDatabase(_database);
            return be;
        }
    }
}
