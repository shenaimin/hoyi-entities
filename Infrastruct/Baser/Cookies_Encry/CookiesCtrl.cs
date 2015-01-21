using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Infrastructure.Baser.Cookies_Encry
{
    /// <summary>
    /// 系统内Cookies的获取和置入的方法.
    /// </summary>
    public class CookiesCtrl
    {
        /// <summary>
        /// 加解密.
        /// </summary>
        Cookies_Encry encry = new Cookies_Encry();

        /// <summary>
        /// 设置cookies.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCookie(HttpContext context, string key, string value)
        {
            context.Response.Cookies[key].Value = encry.EncryptString(value);
        }

        /// <summary>
        /// 获取key为key的cookies.
        /// </summary>
        /// <param name="context">上下文.</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public string GetCookie(HttpContext context, string key)
        {
            if (context.Request.Cookies[key] == null)
            {
                return null;
            }
            return encry.UnEncryptString(context.Request.Cookies[key].Value);
        }
    }
}
