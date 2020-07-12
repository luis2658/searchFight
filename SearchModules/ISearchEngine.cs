using System;
using System.Collections.Generic;
using System.Text;

namespace LA_searchfight.SearchModules
{
    public interface ISearchEngine
    {     
        string obtainEngineResult(string topic);
        long parseResult(string data);
        string getEngineName();
    }
}
