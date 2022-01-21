using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CheckPackage.Configuration.Dependencies
{
    public class StrategyConverterBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public StrategyConverterBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));            
            _serviceCollection.AddSingleton(BuildProvider);
        }

        public StrategyConverterBuilder AddCheckStrategy<TConverter, TJson, TModel>() 
            where TConverter : CheckConvertStrategy<TJson, TModel>
            where TJson : BaseCheckJson
            where TModel : CheckInfo
        {
            _serviceCollection.AddSingleton<ICheckConvertStrategy, TConverter>();
            return this;
        }

        public StrategyConverterBuilder AddExtractStrategy<TConverter, TJson, TModel>()
            where TConverter : ExtractConvertStrategy<TJson, TModel>
            where TJson : BaseExtractJson
            where TModel : ExtractInfo
        {
            _serviceCollection.AddSingleton<IExtractConvertStrategy, TConverter>();
            return this;
        }

        public StrategyConverterBuilder AddConditionStrategy<TConverter, TJson, TModel>()
            where TConverter : ConditionConvertStrategy<TJson, TModel>
            where TJson : BaseConditionJson
            where TModel : ConditionInfo
        {
            _serviceCollection.AddSingleton<IConditionConvertStrategy, TConverter>();
            return this;
        }



        internal IJsonConverterFacade BuildProvider(IServiceProvider provider)
        {            
            return new JsonConverterFacade(
                provider.GetServices<IExtractConvertStrategy>().ToList(),
                provider.GetServices<ICheckConvertStrategy>().ToList(),
                provider.GetServices<IConditionConvertStrategy>().ToList());
        }        
    }
}
