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
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.Cluster
{
    /// <summary>
    /// 数据库集群 操作.
    /// </summary>
    public class DataBaseCluster
    {
        /// <summary>
        /// 单集群.
        /// </summary>
        public static ClusterInfo cluster_a;
        /// <summary>
        /// 集群描述 配置文件内的内容
        /// </summary>
        public static string db_cluster_info_conf = "db_cluster_info";
        /// <summary>
        /// 初始化服务器列表.
        /// </summary>
        public static void InitServers()
        {
            if (cluster_a == null)
            {
                cluster_a = new ClusterInfo();

                IDatabase tmpdb;
                List<IDatabase> dbs = new List<IDatabase>();

                foreach (ConnectionStringSettings conns in ConfigurationManager.ConnectionStrings)
                {
                    tmpdb = DatabaseFactory.InitDatabase(conns);
                    if (tmpdb != null)
                        dbs.Add(tmpdb);
                }

                string db_cluster_info = ConfigurationManager.AppSettings[db_cluster_info_conf];
                string[] dbsp = db_cluster_info.Split('|');
                string mastername = dbsp[0];
                string re_masternames = dbsp[1];
                string slavernames = dbsp[2];
                string info_r_w_types = dbsp[3];

                IDatabase master = dbs.Single(s => s.DBServerName.Equals(mastername));

                List<IDatabase> re_master = dbs.Where(s=> re_masternames.Split(',').Contains(s.DBServerName)).ToList();
                List<IDatabase> slavers = dbs.Where(s => slavernames.Split(',').Contains(s.DBServerName)).ToList();

                Cluster_R_W_TYPE r_w_type = (Cluster_R_W_TYPE)Enum.Parse(typeof(Cluster_R_W_TYPE), info_r_w_types);

                cluster_a.Init(master, re_master, slavers, r_w_type);
            }
        }
        /// <summary>
        /// 获取写服务器.
        /// </summary>
        /// <returns></returns>
        public static IDatabase Get_WRITE_SERVER()
        {
            InitServers();
            // 如果客户端有切分区的标识，就到指定分区内拿数据.
            if (cluster_a != null)
            {
                return cluster_a.Get_WRITE_SERVER();
            }
            return null;
        }
        /// <summary>
        /// 获取读服务器.
        /// </summary>
        /// <returns></returns>
        public static IDatabase Get_READ_SERVER()
        {
            InitServers();
            if (cluster_a != null)
            {
                return cluster_a.Get_READ_SERVER();
            }
            return null;
        }
        /// <summary>
        /// 获取一个事务的新的读服务器.
        /// </summary>
        /// <returns></returns>
        public static IDatabase Get_Transaction_WRITE_SERVER()
        {
            InitServers();
            if (cluster_a != null)
            {
                return cluster_a.Get_NEW_WRITE_SERVER();
            }
            return null;
        }
    }
}
