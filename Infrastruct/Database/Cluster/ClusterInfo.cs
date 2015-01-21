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

namespace Infrastructure.Database.Cluster
{
    /// <summary>
    /// 
    /// 描述单个集群。
    /// 
    /// 单个集群内的每台服务器的数据内容是相同的，
    /// 
    /// 写的时候往主服务器去写，而读的时候，则负载去读每个子服务器，
    ///     或者子服务器少于一个量的时候可以从主服务器/备用主服务器内读数据。
    ///     另一种做法是把主服务器/备用主服务器也加入到子服务器内,这样可能会出现读的负载问题.
    ///     
    ///     解决这个问题: 弄一个集群类型，
    ///         即: 完全读写分离 ALL_SPLIT_R_W,
    ///             半读写分离方式 HALF_SPLIT_R_W,
    ///             读写不分离方式 UN_SPLIT_R_W,
    ///             单服务器方式   SIMPLE_DBSERVER
    ///             
    ///     完全读写分离 ALL_SPLIT_R_W        严格按照Master/RE_Master 服务器只写，Slavers  服务器只读.
    ///     半读写分离方式 HALF_SPLIT_R_W     Master 服务器只写， RE_Master服务器和Slavers服务器只读.
    ///     读写不分离方式 UN_SPLIT_R_W.      所有服务器都做读写操作，在单服务器，所有服务器双向同步的时候可以使用.
    ///     但服务器方式   SIMPLE_DBSERVER
    ///     
    /// 部署的时候请根据不同的读写分离方式来部署主/从服务器的同步方向.
    /// 
    /// 单个集群由单个主服务器、N个备用主服务器、N个子服务器组成，
    /// 
    /// 主服务器和备用主服务器当做写的服务器，子服务器作为读的服务器.
    /// 
    /// </summary>
    public class ClusterInfo
    {
        /// <summary>
        /// 主服务器,一般作为写服务器.
        /// </summary>
        public IDatabase MasterServer;
        /// <summary>
        /// 备用主服务器, 一般作为写服务器，当主服务器崩掉后，就自动切换备用服务器为主服务器.
        /// </summary>
        public List<IDatabase> RE_Masters;
        /// <summary>
        /// 子服务器, 一般做为读服务器，
        /// </summary>
        public List<IDatabase> Slavers;
        /// <summary>
        /// 读写分离类型.
        /// </summary>
        public Cluster_R_W_TYPE R_W_Type;
        /// <summary>
        /// 初始化的内容不能为null,例如 服务器列表没有，则弄个空的列表.
        /// </summary>
        /// <param name="_master"></param>
        /// <param name="_re_masters"></param>
        /// <param name="_slavers"></param>
        /// <param name="_r_w_type"></param>
        public void Init(IDatabase _master, List<IDatabase> _re_masters, List<IDatabase> _slavers, Cluster_R_W_TYPE _r_w_type)
        {
            MasterServer = _master;
            RE_Masters = _re_masters;
            Slavers = _slavers;
            R_W_Type = _r_w_type;
        }
        /// <summary>
        /// 获取写服务器.
        /// </summary>
        /// <returns></returns>
        public IDatabase Get_WRITE_SERVER()
        {
            List<IDatabase> tmp = new List<IDatabase>();
            switch (R_W_Type)
            {
                case Cluster_R_W_TYPE.ALL_SPLIT_R_W:

                    tmp.Add(MasterServer);
                    tmp.AddRange(RE_Masters);

                    return GetRandomServer(tmp);

                case Cluster_R_W_TYPE.HALF_SPLIT_R_W:

                    return MasterServer;

                case Cluster_R_W_TYPE.UN_SPLIT_R_W:
                    tmp.Add(MasterServer);
                    tmp.AddRange(RE_Masters);
                    tmp.AddRange(Slavers);

                    return GetRandomServer(tmp);
                case Cluster_R_W_TYPE.SIMPLE_DBSERVER:
                    return MasterServer;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 获取一个新,主要区别事务和非事务的写服务器.
        /// </summary>
        /// <returns></returns>
        public IDatabase Get_NEW_WRITE_SERVER()
        {
            return DatabaseFactory.CopyDatabase(this.Get_WRITE_SERVER());
        }
        /// <summary>
        /// 获取读服务器.
        /// </summary>
        /// <returns></returns>
        public IDatabase Get_READ_SERVER()
        {
            List<IDatabase> tmp = new List<IDatabase>();
            switch (R_W_Type)
            {
                case Cluster_R_W_TYPE.ALL_SPLIT_R_W:
                    return GetRandomServer(Slavers);
                case Cluster_R_W_TYPE.HALF_SPLIT_R_W:
                    tmp.AddRange(RE_Masters);
                    tmp.AddRange(Slavers);

                    return GetRandomServer(tmp);
                case Cluster_R_W_TYPE.UN_SPLIT_R_W:
                    tmp.Add(MasterServer);
                    tmp.AddRange(RE_Masters);
                    tmp.AddRange(Slavers);

                    return GetRandomServer(tmp);
                case Cluster_R_W_TYPE.SIMPLE_DBSERVER:
                    return MasterServer;
                default:
                    break;
            }

            return null;
        }
        /// <summary>
        /// 获取单个随机服务器.
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public IDatabase GetRandomServer(List<IDatabase> servers)
        {
            return servers.Count > 0 ? servers[GetRandomI(servers.Count)] : null;
        }
        /// <summary>
        /// 获取随机数.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public int GetRandomI(int len)
        {
            Random rand = new Random();
            return rand.Next(len);
        }
    }
    /// <summary>
    /// 
    ///     完全读写分离 ALL_SPLIT_R_W        严格按照Master/RE_Master 服务器只写，Slavers  服务器只读.
    ///     半读写分离方式 HALF_SPLIT_R_W     Master 服务器只写， RE_Master服务器和Slavers服务器只读.
    ///     读写不分离方式 UN_SPLIT_R_W.      所有服务器都做读写操作，在单服务器，所有服务器双向同步的时候可以使用.
    ///     单服务器方式.  SIMPLE_DBSERVER
    ///     
    /// </summary>
    public enum Cluster_R_W_TYPE
    {
        /// <summary>
        /// 完全读写分离
        /// </summary>
        ALL_SPLIT_R_W, 
        /// <summary>
        /// 半读写分离方式
        /// </summary>
        HALF_SPLIT_R_W,      
        /// <summary>
        /// 读写不分离方式
        /// </summary>
        UN_SPLIT_R_W,       
        /// <summary>
        /// 单服务器方式
        /// </summary>
        SIMPLE_DBSERVER     
    }
}
