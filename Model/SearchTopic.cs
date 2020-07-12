using System;
using System.Collections.Generic;
using System.Text;

namespace LA_searchfight.Model
{
    public class SearchTopic
    {
        public string Topic { get; set; }
        public string ParsedTopic { get; set; }
        public List<SearchResult> Results { get; set; }
    }

}
