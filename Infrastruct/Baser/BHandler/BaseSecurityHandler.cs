using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Infrastructure.Baser.Codec;
namespace Infrastructure.Baser.BHandler
{
    /// <summary>
    /// 加验证的Handler处理基类，若Cookies内没有用户，则跳转到登录页，
    /// 所有Handler都要继承，若有其他相冲突的，则可统一修改.
    /// </summary>
    public class BaseSecurityHandler : BaseHandler
    {
        public virtual bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }
        /// <summary>
        /// Cookie存的userid,有则读取，没有则为空
        /// </summary>
        public string CK_userid;
        /// <summary>
        /// Cookie存的username,有则读取，没有则为空
        /// </summary>
        public string CK_username;

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            HttpCookie c_userid = context.Request.Cookies["userid"];
            HttpCookie c_username = context.Request.Cookies["username"];

            if (c_userid == null || c_username == null)
            {
                RedirectLogin(context);
            }
            else if (c_userid.Value == null || c_username.Value == null)
            {
                RedirectLogin(context);
            }
            else if (c_userid.Value.Length <= 0 || c_username.Value.Length <= 0)
            {
                RedirectLogin(context);
            }
            CK_userid =Base64Encrypt.Base64Decrypt(c_userid.Value);
            CK_username =Base64Encrypt.Base64Decrypt(c_username.Value);
        }

        public void RedirectLogin(HttpContext context)
        {
            context.Response.Redirect("~/index.html");
            context.Response.End();
        
        }
    }
}
