using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Infrastructure.Baser.JXT
{
    /// <summary>
    /// 
    /// 基础页面，没有用户登录就直接跳到登录页面。
    /// 
    /// 
    /// </summary>
    public class JXTBasePage :Page
    {
        protected override void OnPreLoad(EventArgs e)
        {

            string url = Request.Url.ToString();

            // 如果没有用户登录，就直接跳到登录页面
            if (Session["userId"] == null || Session["username"] == null || Session["roleName"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

          
            base.OnPreLoad(e);
        }
    }
}
