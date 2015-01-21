using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Infrastructure.Util
{
    public class NavigationUrlController
    {
        /// <summary>
        /// 获取导航路径，XML路径配置在AppConfig\appSetting\NavigationUrl路径;
        /// </summary>
        /// <param name="urlId"></param>
        /// <returns></returns>
        public static string GetNavigationUrl(string urlId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["NavigationUrl"];
                XmlDocument doc = new XmlDocument();
                doc.Load(url);

                XmlElement urls = doc.SelectSingleNode("/SkipUrl/jump[@id='" + urlId + "']") as XmlElement;

                return urls.GetAttribute("value");
            }
            catch (Exception Ex)
            {
                throw new Exception("Error:" + Ex.Message);
            }
        }
    }


    #region  XML 格式
    /*   XML 格式        
     * <?xml version="1.0" encoding="utf-8" ?>
        <SkipUrl>
          <jump id="ManageLogin" value="http://localhost:12333/UserLogin.aspx"></jump>
          <jump id="ManageDefault" value="http://localhost:12333/Default.aspx"></jump>

          <jump id="IconManage" value="http://localhost:12319/IconManage.aspx"></jump>
          <jump id="TypeManage" value="http://localhost:12319/TypeManage.aspx"></jump>
          <jump id="TypeEnterprise" value="http://localhost:12319/TypeEnterprise.aspx"></jump>

          <jump id="WZMapManageDefault" value="http://localhost:12323/Default.aspx"></jump>
          <jump id="WZBusManage" value="http://localhost:12323/BusWay/BusManage.aspx"></jump>
          <jump id="RAMapManageDefault" value="http://localhost:12323/Default.aspx"></jump>
          <jump id="RABusManage" value="http://localhost:12323/BusWay/BusManage.aspx"></jump>
        </SkipUrl>
     * 
     */
    #endregion
}
