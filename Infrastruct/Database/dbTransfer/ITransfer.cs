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
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.dbTransfer
{
    /// <summary>
    ///  转换语句.
    /// </summary>
    public interface ITransfer
    {
        string InitCmd(string[] filter, string[] operates, object[] values, string[] nextops, string[] filtertype, HOYICMD cmd);

        List<DbParameter> InitParams(Entity entity, string[] filter, string[] operates, object[] values,HOYICMD cmd);

        List<DbParameter> InitParams(Type entityType, string[] filter, string[] operates, object[] values, HOYICMD cmd);

        List<DbParameter> InitParams<T>(T entity, string[] filter, string[] operates, object[] values, HOYICMD cmd);

        string InsertCmd(Entity entity, bool addprimkey = false);

        string DeleteACmd(Entity entity);

        string ExistsCmd(Entity entity);

        string UpdateCmd(Entity entity);

        string UpdateCmd(Entity entity, AttField[] fields);
        string UpdateCmd(Entity entity, string[] attfield);
        List<DbParameter> AllParams(Entity entity, bool containidentity);


    }
}
