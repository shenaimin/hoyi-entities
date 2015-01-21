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
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.ctrl
{
    public class ConsoleDebug
    {
        public static void WriteCmd(string strcmd, HOYICMD cmd)
        {
            if (HOYIConf.OpenDebug)
            {
                //Console.WriteLine("SQL:" + strcmd);

                //Console.WriteLine("parameters:");

                CONSOLEWRITE("SQL:" + strcmd);
                CONSOLEWRITE("parameters:");

                foreach (DbParameter par in cmd.parameter)
                {
                    CONSOLEWRITE(par.ParameterName + "  ---->  " + par.Value);
                    //Console.WriteLine(par.ParameterName + "  ---->  " + par.Value);
                }
            }
        }
        public static void WriteCmd(string strcmd, List<DbParameter> parameter)
        {
            if (HOYIConf.OpenDebug)
            {
                //Console.WriteLine("SQL:" + strcmd);

                //Console.WriteLine("parameters:");

                CONSOLEWRITE("SQL:" + strcmd);
                CONSOLEWRITE("parameters:");
                foreach (DbParameter par in parameter)
                {
                    CONSOLEWRITE(par.ParameterName + "  ---->  " + par.Value);
                    //Console.WriteLine(par.ParameterName + "  ---->  " + par.Value);
                }
            }
        }

        public static void CONSOLEWRITE(string ccc)
        {
            System.Diagnostics.Debug.WriteLine(ccc);
        }
    }
}
