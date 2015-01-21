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
    /// 获取分页信息参数模型,此模型用于模块内部填写,以便不暴露敏感信息,此处为敏感信息.
    /// </summary>
    [Serializable]
    public class IPagingDataInfo
    {
        public IPagingDataInfo()
        { 
        
        }

        public IPagingDataInfo(string sortedFields, string pageSize, string pageIndex)
        {
            this.SortedFields = sortedFields;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }
        /// <summary>
        /// 排序字段.按此字段排序.
        /// </summary>
        public string SortedFields { get; set; }
        /// <summary>
        /// 分页大小,一页条数.
        /// </summary>
        public string PageSize { get; set; }
        /// <summary>
        /// 页码,当前第几页.
        /// </summary>
        public string PageIndex { get; set; }
    }
}
