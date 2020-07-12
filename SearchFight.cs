using LA_searchfight.Model;
using LA_searchfight.SearchModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LA_searchfight
{
    public class SearchFight
    {        
        List<ISearchEngine> SearchEngines;

        public List<SearchTopic> Topics { get; }

        public SearchFight()
        {
            Topics = new List<SearchTopic>();

            SearchEngines = new List<ISearchEngine>();

            ISearchEngine yahoo = new YahooSearchEngine();
            ISearchEngine google = new GoogleSearchEngine();

            SearchEngines.Add(yahoo);
            SearchEngines.Add(google);
        }

        public void Fight(string[] SearchTopics)
        {
            foreach (var query in SearchTopics)
            {
                SearchTopic topic = new SearchTopic
                {
                    Topic = query,
                    ParsedTopic = parseQuery(query),
                    Results = new List<SearchResult>()
                };

                foreach(var engine in SearchEngines)
                {
                    SearchResult oResult = new SearchResult();

                    oResult.string_result = engine.obtainEngineResult(topic.ParsedTopic);
                    oResult.result = engine.parseResult(oResult.string_result);
                    oResult.Engine = engine.getEngineName();

                    topic.Results.Add(oResult);

                }

                Topics.Add(topic);
            }
        }

        public string parseQuery(string query)
        {
            string trimmedResult = "";

            for (int i = 0; i < query.Length; i++)
            {
                var character = query[i];
                if (character == ' ')
                    trimmedResult += "%20";
                else
                    trimmedResult += character;
            }

            return trimmedResult;
        }

        public Dictionary<string, string> EngineResults()
        {            
            Dictionary<string, Dictionary<string, long>> pivot = new Dictionary<string, Dictionary<string, long>>();
            Dictionary<string, string> results = new Dictionary<string, string>();

            foreach (var engine in SearchEngines)
            {
                pivot.Add(engine.getEngineName(), new Dictionary<string, long>());
                results.Add(engine.getEngineName(), "");
            }

            foreach (var topic in Topics)
            {
                foreach (var inResult in topic.Results)
                {
                    pivot[inResult.Engine].Add(topic.Topic,inResult.result);
                }
            }

            foreach(var engine in pivot)
            {
                long max = -1;
                string best = "";

                foreach (var topic in engine.Value)
                {
                    if(topic.Value > max - 1)
                    {
                        max = topic.Value;
                        best = topic.Key;
                    }
                }

                results[engine.Key] = best;
            }

            return results;
        }

        public string BestTopic()
        {
            long max = -1;
            string Best = "";

            Dictionary<string, long> results = new Dictionary<string, long>();                        

            foreach (var topic in Topics)
            {
                results.Add(topic.Topic, 0);
                foreach (var inResult in topic.Results)
                {
                    results[topic.Topic] += inResult.result;
                }

                if (max < results[topic.Topic])
                {
                    max = results[topic.Topic];
                    Best = topic.Topic;
                }
            }            
            
            return Best;
        }

    }
}
