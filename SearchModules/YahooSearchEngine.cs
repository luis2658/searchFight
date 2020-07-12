using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LA_searchfight.SearchModules
{
    public class YahooSearchEngine : ISearchEngine
    {
        public string url = "https://search.yahoo.com/search?p=";

        public string obtainEngineResult(string data)
        {
            string result = "";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url + data);

            var nodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "compPagination");


            foreach (var node in nodes)
            {
                foreach (var inNode in node.Descendants("span"))
                {
                    if (inNode.InnerHtml.Contains("results"))
                    {
                        //results.Add(inNode.InnerText);
                        result = inNode.InnerText;
                        break;
                    }
                }

                if (result != "")
                {
                    break;
                }
            }

            return result;
        }
        public long parseResult(string data)
        {
            long result = 0;

            string[] _bases = data.Split(' ');


            string _aux = _bases[0];

            string trimmedResult = "";

            for (int i = 0; i < _aux.Length; i++)
            {
                var character = _aux[i];
                if (character != ',')
                    trimmedResult += character;
            }

            result = Convert.ToInt32(trimmedResult);

            return result;
        }

        public string getEngineName()
        {
            return "Yahoo";
        }
    }
}
