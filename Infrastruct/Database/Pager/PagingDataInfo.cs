/*
 *          Author:Sam
 *          Email:ellen@hoyi.org
 *          CreateDate:2008-11-21
 *          ModifyDate:2008-11-21
 *          hoyi entities @ hoyi.org
 *          使用请在项目关于内标注hoyi版权，
 *          hoyi版权归hoyi.org所有
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database.Pager
{
    /// <summary>
    /// 获取分页数据参数模型.此模型用于模块内部填写,此处为不敏感信息.
    /// </summary>
    public class PagingDataInfo:IPagingDataInfo
    {
        public PagingDataInfo()
        { 
        
        }

        public PagingDataInfo(IPagingDataInfo pdata)
        {
            this.SortedFields = pdata.SortedFields;
            this.PageIndex = pdata.PageIndex;
            this.PageSize = pdata.PageSize;
        }

        public PagingDataInfo(IPagingDataInfo pdata,string tableName,string filter,string fields)
        {
            this.SortedFields = pdata.SortedFields;
            this.PageIndex = pdata.PageIndex;
            this.PageSize = pdata.PageSize;

            this.TableName = tableName;
            this.Filter = filter;
            this.Fields = fields;
        }
        /// <summary>
        /// 表名.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 筛选条件.
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 搜索出的字段,全部可用*表示.
        /// </summary>
        public string Fields { get; set; }
    }
}
