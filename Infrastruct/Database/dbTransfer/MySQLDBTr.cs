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
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.dbTransfer
{
    public class MySQLDBTr
    {
        public static MySqlDbType TransByEnDbType(datatype dbtype)
        {
            switch (dbtype)
            {
                case datatype.Varchar:
                    return MySqlDbType.VarChar;
                case datatype.Nvarchar:
                    return MySqlDbType.VarChar;
                case datatype.Int:
                    return MySqlDbType.Int32;
                case datatype.Decimal:
                    return MySqlDbType.Decimal;
                case datatype.Bigint:
                    return MySqlDbType.Int64;
                case datatype.DateTime:
                    return MySqlDbType.DateTime;
                default:
                    break;
            }
            return MySqlDbType.VarChar;
        }

        public static datatype TransByDbType(MySqlDbType dbtype)
        {
            switch (dbtype)
            {
                case MySqlDbType.Binary:
                    break;
                case MySqlDbType.Bit:
                    break;
                case MySqlDbType.Blob:
                    break;
                case MySqlDbType.Byte:
                    break;
                case MySqlDbType.Date:
                    break;
                case MySqlDbType.DateTime:
                    return datatype.DateTime;
                case MySqlDbType.Decimal:
                    return datatype.Decimal;
                case MySqlDbType.Double:
                    break;
                case MySqlDbType.Enum:
                    break;
                case MySqlDbType.Float:
                    break;
                case MySqlDbType.Geometry:
                    break;
                case MySqlDbType.Guid:
                    break;
                case MySqlDbType.Int16:
                    break;
                case MySqlDbType.Int24:
                    break;
                case MySqlDbType.Int32:
                    return datatype.Int;
                case MySqlDbType.Int64:
                    return datatype.Bigint;
                case MySqlDbType.LongBlob:
                    break;
                case MySqlDbType.LongText:
                    break;
                case MySqlDbType.MediumBlob:
                    break;
                case MySqlDbType.MediumText:
                    break;
                case MySqlDbType.NewDecimal:
                    break;
                case MySqlDbType.Newdate:
                    break;
                case MySqlDbType.Set:
                    break;
                case MySqlDbType.String:
                    break;
                case MySqlDbType.Text:
                    break;
                case MySqlDbType.Time:
                    break;
                case MySqlDbType.Timestamp:
                    break;
                case MySqlDbType.TinyBlob:
                    break;
                case MySqlDbType.TinyText:
                    break;
                case MySqlDbType.UByte:
                    break;
                case MySqlDbType.UInt16:
                    break;
                case MySqlDbType.UInt24:
                    break;
                case MySqlDbType.UInt32:
                    return datatype.Int;
                case MySqlDbType.UInt64:
                    return datatype.Bigint;
                case MySqlDbType.VarBinary:
                    break;
                case MySqlDbType.VarChar:
                    return datatype.Varchar;
                case MySqlDbType.VarString:
                    return datatype.Varchar;
                case MySqlDbType.Year:
                    break;
                default:
                    break;
            }
            return datatype.Varchar;
        }
    }
}
