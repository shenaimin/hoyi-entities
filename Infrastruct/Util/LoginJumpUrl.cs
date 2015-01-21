using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Infrastructure.Util
{
    public class LoginJumpUrl
    {
        public string GetJumpUrlToNextPage(System.Web.HttpServerUtility server,string url, string valueKey)
        {
            if (!url.ToLower().Contains(".xml"))
            {
                url += ".xml";
            }
            XmlTextReader objXMLReader = new XmlTextReader(server.MapPath(url));
            string strNodeResult = "";
            while (objXMLReader.Read())
            {
                if (objXMLReader.NodeType.Equals(XmlNodeType.Element))
                {
                    if (objXMLReader.AttributeCount > 0)
                    {
                        while (objXMLReader.MoveToNextAttribute())
                        {
                            if ("id".Equals(objXMLReader.Name) && valueKey.Equals(objXMLReader.Value))
                            {
                                if (objXMLReader.MoveToNextAttribute())
                                {
                                    strNodeResult = objXMLReader.Value;
                                }
                            }
                        }
                    }
                }
            }
            return strNodeResult;
        }
    }
}
