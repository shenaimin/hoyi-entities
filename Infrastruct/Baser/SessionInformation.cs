using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Infrastructure.Baser
{
    public class SessionInformation
    {
        public static SessionInformation Instance
        {
            get
            {
                if (HttpContext.Current.Session["SessionInformation"] == null)
                    HttpContext.Current.Session["SessionInformation"] = new SessionInformation();
                return (SessionInformation)HttpContext.Current.Session["SessionInformation"];
            }
        }

        public void Clear()
        {
            HttpContext.Current.Session["SessionInformation"] = new SessionInformation();
        }

        private string theme;

        public string Theme
        {
            get
            {
                return theme;
            }
            set { theme = value; }
        }
    }
}
