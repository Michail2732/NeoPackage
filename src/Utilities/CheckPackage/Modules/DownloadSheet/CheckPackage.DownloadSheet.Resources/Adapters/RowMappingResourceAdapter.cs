using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.DownloadSheet.Mapping;
using CheckPackage.Loadlist.Configuration;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Resources
{
    public class RowMappingResourceAdapter : IConfigurationAdapter<RowMappingResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converterFacade;
        private readonly MessagesService _messages;

        public RowMappingResourceAdapter(IConfigurationServiceLow configuration,
            IJsonConverterFacade converterFacade, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converterFacade = converterFacade ?? throw new ArgumentNullException(nameof(converterFacade));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<RowMappingResource> Get()
        {            
            var rules = _configuration.GetRules();            
            if (rules.ContainsKey("loadlist_rows"))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundEntityBuild, "loadlist_rows"));
            try
            {
                var serializeSettings = _converterFacade.GetConverters();
                var result = JsonConvert.DeserializeObject<List<LoadlistRowRuleJson>>(
                    rules["loadlist_rows"].ToString(), serializeSettings);
                return result.Select(a => new RowMappingResource
                { 
                      ColumnNames = a.ColumnNames,
                      EntityConditions = a.Conditions.Select(b => _converterFacade.ConditionConvert(b)).RollUp(),
                      EntityLevel = a.EntityLevel,
                      EntityNameColumn = a.EntityNameColumn,
                      IsVirtual = a.IsVirtual,
                      Id = a.Id,
                      Priority = a.Priority
                    }).ToList();

            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct,
                    "loadlist_rows"), ex);
            }
        }        
    }
}
