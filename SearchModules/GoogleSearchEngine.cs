using HtmlAgilityPack;
using System;
using System.Linq;

namespace LA_searchfight.SearchModules
{
    public class GoogleSearchEngine: ISearchEngine
    {
        public const string Url = "https://www.google.com/search?q=";
        public const string Engine = "Google";


        public string obtainEngineResult(string topic)
        {
            string result = "";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url + topic);

            var nodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["id"] != null && x.Attributes["id"].Value == "result-stats");

            foreach (var node in nodes)
            {

                if (node.InnerHtml.Contains("result"))
                {
                    result = node.InnerText;
                    break;
                }
            }

            return result;
        }
        public long parseResult(string data)
        {
            long result = 0;
            string[] _bases = data.Split(' ');

            string _aux = _bases[2];

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
            return "Google";
        }
    }
}
