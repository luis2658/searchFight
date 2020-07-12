using LA_searchfight.Model;
using System;

namespace LA_searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] baia = { "kotlin", "java",".net"};

            SearchFight currentFight = new SearchFight();

            currentFight.Fight(args);

            var topics = currentFight.Topics;

            foreach(var topic in topics)
            {
                ReadTopicResults(topic);
            }

            var engineResults = currentFight.EngineResults();

            foreach (var topic in engineResults)
            {
                Console.WriteLine("{0} Winner: {1}",topic.Key,topic.Value );
            }

            Console.WriteLine("Total Winner: {0}", currentFight.BestTopic());
        }

        public static void ReadTopicResults(SearchTopic topic)
        {
            Console.WriteLine("Search {0}", topic.Topic);

            foreach (var result in topic.Results)
            {
                Console.WriteLine("\t {0}: {1}", result.Engine, result.result);
            }
        }
    }
}
