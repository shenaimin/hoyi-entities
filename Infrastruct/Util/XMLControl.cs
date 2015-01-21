using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Xml;

namespace Infrastructure.Util
{
    /// <summary>
    /// XMLControl 的摘要说明
    /// </summary>
    public class XMLControl
    {
        protected string strXMLFile;
        protected XmlDocument objXMLDoc = new XmlDocument();

        public XMLControl(string XMLFile)
        {
            // 
            // TODO: 在這裡加入建構函式的程式碼 
            // 
            try
            {
                objXMLDoc.Load(XMLFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                strXMLFile = XMLFile;
            }
        }

        public void Replace(string id, string name, string imageurl, string alterurl, string DataBase, string DataBaseCotent)
        {
            XmlNode objRootNode = objXMLDoc.DocumentElement;
            XmlNode objChildNode;

            objChildNode = objRootNode.SelectSingleNode("descendant::Type[id='" + id + "']");

            objChildNode.SelectSingleNode("name").InnerText = name;
            objChildNode.SelectSingleNode("imageurl").InnerText = imageurl;
            objChildNode.SelectSingleNode("alterurl").InnerText = alterurl;
            objChildNode.SelectSingleNode("DataBase").InnerText = DataBase;
            objChildNode.SelectSingleNode("DataBaseCotent").InnerText = DataBaseCotent;

            Save();
        }

        /// <summary>
        /// 根據ID刪除一個節點。
        /// </summary>
        /// <param name="Node"></param>
        public void Delete(string TypeID)
        {
            XmlNode root = objXMLDoc.DocumentElement;
            root.RemoveChild(objXMLDoc.SelectSingleNode("descendant::Type[id='" + TypeID + "']"));
            Save();
        }

        public void InsertNode(string id, string name, string imageurl, string alterurl, string DataBase, string DataBaseCotent)
        {
            XmlNode objRootNode = objXMLDoc.DocumentElement;

            XmlElement objChildNode = objXMLDoc.CreateElement("Type");
            objRootNode.AppendChild(objChildNode);

            XmlElement objElement1 = objXMLDoc.CreateElement("id");
            objElement1.InnerText = id;
            objChildNode.AppendChild(objElement1);

            XmlElement objElement2 = objXMLDoc.CreateElement("name");
            objElement2.InnerText = name;
            objChildNode.AppendChild(objElement2);

            XmlElement objElement3 = objXMLDoc.CreateElement("imageurl");
            objElement3.InnerText = imageurl;
            objChildNode.AppendChild(objElement3);

            XmlElement objElement4 = objXMLDoc.CreateElement("alterurl");
            objElement4.InnerText = alterurl;
            objChildNode.AppendChild(objElement4);

            XmlElement objElement5 = objXMLDoc.CreateElement("DataBase");
            objElement5.InnerText = DataBase;
            objChildNode.AppendChild(objElement5);

            XmlElement objElement6 = objXMLDoc.CreateElement("DataBaseCotent");
            objElement6.InnerText = DataBaseCotent;
            objChildNode.AppendChild(objElement6);

            Save();
        }

        public string GetContent(string ByElementName, string ByElementContent, string GetElementName)
        {
            XmlNode objRootNode = objXMLDoc.SelectSingleNode("descendant::Type[" + ByElementName + "='" + ByElementContent + "']");
            return objRootNode.SelectSingleNode(GetElementName).InnerText;
        }

        public void BindDDL_Data(DropDownList DDL)
        {
            DataSet objDataSet = new DataSet();
            objDataSet.ReadXml(strXMLFile);

            DataTable DT = objDataSet.Tables["table"].Clone();
            //DT=objDataSet.Tables["table"]
            List<DataRow> drs = objDataSet.Tables["table"].Select("content <> ''").ToList();

            for (int i = 0; i < drs.Count; i++)
            {
                DT.ImportRow(drs[i]);
            }

            DDL.DataSource = DT;
            DDL.DataTextField = DT.Columns[1].ColumnName.ToString();
            DDL.DataValueField = DT.Columns[0].ColumnName.ToString();
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("请选择", ""));
            DDL.SelectedIndex = 0;
        }

        /// <summary>
        /// 保存文檔。
        /// </summary>
        public void Save()
        {
            try
            {
                objXMLDoc.Save(strXMLFile);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            objXMLDoc = null;
        }
    }
}
