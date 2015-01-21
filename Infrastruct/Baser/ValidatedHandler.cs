using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.SessionState;
using System.Web.UI;

namespace Infrastructure.Baser
{
    public class ValidatedHandler : IHttpHandler, IRequiresSessionState
    {
        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            HttpSessionState session = context.Session;
            //权限判断
            if (session["username"] != null && !string.IsNullOrEmpty(session["username"].ToString()))
            {
            }
            else
            {
                //context.Server.Transfer("/login.aspx");

                //context.Response.Write("[{error:unlogined;}]");
                //context.Response.Flush();
                //context.Response.End();
            }
        }
    }
}
