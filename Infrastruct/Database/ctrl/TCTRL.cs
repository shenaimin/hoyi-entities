/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.ents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.ctrl
{

    public static class TCTRL
    {
        public static IEnumerable<TSource> H_Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {

            return null;
        }

        public static List<T> H_Where<T>(this Entity entity, Func<T, bool> predicate)
        {
            return null;
        }
    }

}
