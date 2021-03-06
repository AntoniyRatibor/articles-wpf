﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Articles.Core
{
    class PageParser
    {
        public Dictionary<string, int> ParsePage(string address, int pageFrom, int pageTo)
        {
            Dictionary<string, int> Dict = new Dictionary<string, int>();
            HtmlWeb web = new HtmlWeb();

            pageFrom = pageFrom - 1;
            for (int i = pageFrom; i < pageTo; i++)
            {
                HtmlDocument page = web.Load(address + i.ToString());
                HtmlNode nodeOl = page.DocumentNode.SelectSingleNode("//body/div/div/div/div/ol");
                HtmlNodeCollection nodesLi = nodeOl.ChildNodes;

                foreach (HtmlNode node in nodesLi)
                {
                    if (node.Name != "#text")
                    {
                        HtmlNode newNode = node.SelectSingleNode(node.XPath + "/div/h6/a");
                        int curPage = i + 1;
                        string article = Regex.Replace(newNode.InnerText, @"\s+", " ");
                        if (!Dict.ContainsKey(article))
                        {
                            Dict.Add(article, curPage);
                        }
                    }
                }
            }
            web = null;
            return Dict;
        }
    }
}
