using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Security.Principal;
using Infrastructure.Util;

namespace Infrastructure.Baser
{
    public class SecurityPageBaser : Page
    {
        protected override void OnPreLoad(EventArgs e)
        {
            string url = Request.Url.ToString();

            HttpCookie cookies = Context.Request.Cookies[".1"];//HttpContext.Current.Request.Cookies

            if (cookies == null)
            {
                Response.Redirect(new LoginJumpUrl().GetJumpUrlToNextPage(Server, "jumpUrl.xml", "ManageLogin") + "?url=" + url);
            }
            else
            {
                //FormsAuthenticationTicket tickets = FormsAuthentication.Decrypt(cookies.Value);
                //if (Cache[tickets.Name] == null)
                //{
                //    Response.Redirect(new LoginJumpUrl().GetJumpUrlToNextPage(Server, "jumpUrl.xml", "ManageLogin") + "?url=" + url);
                //}
                //else
                //{
                string UrlRole = HttpUtility.UrlDecode(cookies.Values["roleurl"]);

                string requestUrl = Request.Url.OriginalString;
                requestUrl = requestUrl.Replace(":80", "");
                if (Request.Url.Query != "") { requestUrl = requestUrl.Replace(HttpUtility.UrlDecode(Request.Url.Query), ""); }
                if (!UrlRole.Contains(requestUrl))
                {
                    Response.Redirect(new LoginJumpUrl().GetJumpUrlToNextPage(Server, "jumpUrl.xml", "ManageLogin") + "?url=" + url);
                }
                //else
                //{
                //    string[] roles = UrlRole.Split(new char[] { ',' });
                //    FormsIdentity identity = new FormsIdentity(tickets);
                //    GenericPrincipal priciple = new GenericPrincipal(identity, roles);
                //    Context.User = priciple;
                //}
                UrlRole = "";
                //}
            }
            base.OnPreLoad(e);
        }
    }
}
