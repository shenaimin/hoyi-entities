using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Security;
using System.Web;
using System.Security.Principal;

namespace Infrastructure.Baser.PageExtension
{
    public class PageBases:Page
    {

        //protected override void OnPreInit(EventArgs e)
        //{
        //    base.OnPreInit(e);

        //    if (!string.IsNullOrEmpty(SessionInformation.Instance.Theme))
        //    {
        //        Page.Theme = SessionInformation.Instance.Theme;
        //    }
        //    else
        //    {
        //        Page.Theme = "Default";
        //    }
        //}

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            HttpCookie cookies = Context.Request.Cookies[".1"];

            if (cookies == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                FormsAuthenticationTicket tickets = FormsAuthentication.Decrypt(cookies.Value);
                string[] roles = Cache[tickets.Name].ToString().Split(new char[] { ',' });
                FormsIdentity identity = new FormsIdentity(tickets);
                GenericPrincipal priciple = new GenericPrincipal(identity, roles);
                Context.User = priciple;
            }
        }
    }
}
