/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2015-01-20
 *          ModifyDate:2015-01-20
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using Infrastructure.Database.conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.dbTransfer
{
    public class TransFactory
    {
        private static ITransfer _Instance;
        public static ITransfer Instance { 
            get 
            {
                if (_Instance == null)
                {
                    _Instance = TransFactory.Create();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }

        public static ITransfer Create()
        {
            switch (DatabaseFactory.getCurrentDataType())
            {
                case DatabaseType.Unknow:
                    return new MySqlTransfer();
                case DatabaseType.SQL:
                    return new MySqlTransfer();
                case DatabaseType.ORACLE:
                    return new MySqlTransfer();
                case DatabaseType.MYSQL_MariaDB:
                    return new MySqlTransfer();
                default:
                    break;
            }
            return new MySqlTransfer();
        }
    }
}
