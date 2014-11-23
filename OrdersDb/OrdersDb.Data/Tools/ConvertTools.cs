using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OrdersDb.Data.Tools
{
    public class ConvertTools
    {
        public static List<List<string>> TableToList(string fileName)
        {
            var doc = new HtmlDocument();
            doc.Load(fileName);
            var list = new List<List<string>>();
            foreach (var node in doc.DocumentNode.SelectNodes("//table[1]").Descendants("tr"))
            {
                var tds = node.SelectNodes("td");
                if (tds != null && tds.Any())
                {
                    var item = tds.Select(cell => cell.InnerText).ToList();
                    list.Add(item);
                }
            }
            return list;
        }

        public static List<T> TableTo<T>(string filename, Func<List<string>, T> func)
        {
            var list = TableToList(filename).Distinct().ToList();
            return list.Select(func).ToList();
        }
    }
}
