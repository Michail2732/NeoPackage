using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Extensions;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Mapping;
using Newtonsoft.Json;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Resources
{
    public class ColumnMappingResourceAdapter: IConfigurationAdapter<ColumnMappingResource>
    {
        private readonly IConfigurationServiceLow _configuration;
        private readonly IJsonConverterFacade _converterFacade;
        private readonly MessagesService _messages;

        public ColumnMappingResourceAdapter(IConfigurationServiceLow configuration, 
            IJsonConverterFacade converterFacade, MessagesService messages)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _converterFacade = converterFacade ?? throw new ArgumentNullException(nameof(converterFacade));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
        }

        public IList<ColumnMappingResource> Get()
        {
            var rules = _configuration.GetRules();                        
            if (rules.ContainsKey("loadlist_columns"))
                throw new ConfigurationException(_messages.Get(MessageKeys.NotFoundEntityBuild, "loadlist_columns"));
            try
            {
                var jsonConverters = _converterFacade.GetConverters();
                var result = JsonConvert.DeserializeObject<List<LoadlistColumnRuleJson>>(
                    rules["loadlist_columns"].ToString(), jsonConverters);
                return result.Select(a => new ColumnMappingResource
                {
                    Column = a.Column,
                    Conditions = a.Conditions.Select(b => _converterFacade.ConditionConvert(b)).RollUp(),
                    Extracts = a.Extracts.Select(b => _converterFacade.ExtractConvert(b)).ToList(),
                    Id = a.Id,
                    Name = a.Name
                }).ToList();

            }
            catch (JsonException ex)
            {
                throw new ConfigurationException(_messages.Get(MessageKeys.IncorrectSectionStruct,
                    "loadlist_columns"), ex);
            }
        }
    }
}
