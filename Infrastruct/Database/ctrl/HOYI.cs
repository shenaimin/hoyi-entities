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
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.ctrl
{
    public class HOYI
    {
        public HOYI() { }

        public static HOYICMD E<T>()
        {
            HOYICMD hcmd = new HOYICMD();
            hcmd.EntType = typeof(T);
            hcmd.parameter = new List<DbParameter>();
            hcmd.F_TableName = HOYISQL.GetTableName(hcmd.EntType);
            return hcmd;
        }
    }
}
