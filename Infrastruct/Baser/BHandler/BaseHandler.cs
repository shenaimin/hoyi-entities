using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Infrastructure.Baser.BHandler
{
    /// <summary>
    /// Handler的基类，可以在这里处理所有的Handler权限和分页情况，若没有，则不需要设置，有就可以使用context的参数.
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public virtual bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }


        public string pageIndex = "0";
        public string pageSize = "3";

        public virtual void ProcessRequest(HttpContext context)
        {
            //page, rows
            if (context.Request.Params["page"] != null)
            {
                pageIndex = context.Request.Params["page"];
            }
            if (context.Request.Params["rows"] != null)
            {
                pageSize = context.Request.Params["rows"];
            }
        }

        #endregion
    }
}
