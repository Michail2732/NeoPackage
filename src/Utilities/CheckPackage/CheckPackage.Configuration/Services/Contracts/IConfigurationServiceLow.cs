using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Services
{
    public interface IConfigurationServiceLow
    {
        Dictionary<string, JToken> GetDictionaries();
        Dictionary<string, JToken> GetRules();
        Dictionary<string, JToken> GetChecks();
        void UpdateRule(string sectionId, JToken newRules);
        void UpdateDictionary(string sectionId, JToken newDicts);
        void UpdateСheck(string sectionId, JToken newChecks);
    }
}
