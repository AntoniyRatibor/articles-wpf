﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Articles.Core
{
    class XmlDB : PageParser
    {
        private string DBpath;

        public XmlDB(string DBpath)
        {
            this.DBpath = DBpath;
        }

        public void DBCreator()
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(DBpath, Encoding.UTF8);
            xmlWriter.WriteStartElement("main");
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        public Dictionary<string, int> FillUp(string address, int pageFrom, int pageTo)
        {
            Dictionary<string, int> Dict = ParsePage(address, pageFrom, pageTo);

            XmlDocument xmlDB = new XmlDocument();
            xmlDB.Load(DBpath);

            ICollection<string> keys = Dict.Keys;
            foreach (string key in keys)
            {
                XmlNode article = xmlDB.CreateElement("article");
                XmlAttribute page = xmlDB.CreateAttribute("page");
                article.InnerText = key;
                page.Value = Dict[key].ToString();
                xmlDB.DocumentElement.AppendChild(article);
                article.Attributes.Append(page);
            }

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };

            using (XmlWriter writer = XmlWriter.Create(DBpath, settings))
            {
                xmlDB.Save(writer);
            }
            return Dict;
        }

        public Dictionary<string, int> DBDictFromXml()
        {
            Dictionary<string, int> Dict = new Dictionary<string, int>();

            XmlDocument xmlDB = new XmlDocument();
            xmlDB.Load(DBpath);
            XmlNodeList articleNodes = xmlDB.GetElementsByTagName("article");

            foreach (XmlNode article in articleNodes)
            {
                Dict[article.InnerText] = Convert.ToInt32(article.Attributes["page"].Value);
            }
            return Dict;
        }
    }
}
