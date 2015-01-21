using Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Infrastructure.Baser
{
    /// <summary>
    /// 需要验证的页面，页面的验证写在这里，
    /// 如果用户未登录，则返回空.
    /// 
    /// 这里可以做一些权限的认证。例如某个用户不拥有访问当前页面的时候，也可以拒绝访问.
    /// 
    /// </summary>
    public class ValidatedPage : Page
    {
        protected override void OnPreLoad(EventArgs e)
        {
            //if (Session["username"] != null)
            //{
            //    base.OnPreLoad(e);
            //}
            //else
            //{
            //    //拿到自己的路径，跟当前用户可以用的路径进行比较。
            //    JSController.Alert("请登录!");
            //    Response.Redirect("~/index.aspx");
            //}
        }
    }
}
