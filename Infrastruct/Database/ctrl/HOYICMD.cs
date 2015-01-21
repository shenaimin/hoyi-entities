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
    /// <summary>
    /// 命令
    /// </summary>
    [Serializable]
    public class HOYICMD
    {
        public HOYICMD() { }

        public string selectCmd { get; set; }
        /// <summary>
        /// 实体类型.
        /// </summary>
        public Type EntType { get; set; }
        /// <summary>
        /// 字段.
        /// </summary>
        public string F_Fields { get; set; }
        /// <summary>
        /// 更新.
        /// </summary>
        public string F_UPDATE { get; set; }
        /// <summary>
        /// 条件.
        /// </summary>
        public string F_Where { get; set; }
        /// <summary>
        /// 排序.
        /// </summary>
        public string F_Order { get; set; }
        /// <summary>
        /// 分页.
        /// </summary>
        public string F_Limit { get; set; }
        /// <summary>
        /// 单页行数.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 页面总条数
        /// </summary>
        public int PgCount { get; set; }
        /// <summary>
        /// 数据总条数.
        /// </summary>
        public int DataCount { get; set; }

        public int CalcPageSize()
        {
            if (PageSize > 0)
            {
                PgCount = (DataCount / PageSize) + ((DataCount % PageSize) > 0 ? 1 : 0);
            }
            return PgCount;
        }

        /// <summary>
        /// 计算Limit 值.
        /// </summary>
        /// <returns></returns>
        public string CalcLimit()
        {
            if (pageIndex <= 0)
                pageIndex = 1;
            string limit = "";
            if (PageSize > 0 && PgCount > 0)
            {
                if (pageIndex <= PgCount)
                {
                    limit = ((pageIndex - 1) * PageSize).ToString() + "," + PageSize.ToString();
                }
                else if (pageIndex > PgCount)
                {
                    pageIndex = PgCount;
                    limit = ((PgCount - 1) * PageSize).ToString() + "," + PageSize.ToString();
                }
            }
            else
            {
                limit = "0," + PageSize.ToString();
            }
            return limit;
        }
        /// <summary>
        /// 表名.
        /// </summary>
        public string F_TableName { get; set; }

        public List<DbParameter> parameter { get; set; }

        public List<string> addedfield { get; set; }

        public List<string> paraaddedfield { get; set; }
    }
}
