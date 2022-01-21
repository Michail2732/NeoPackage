using CheckPackage.Base.Checks;
using CheckPackage.Base.Conditions;
using CheckPackage.Base.Configuration;
using CheckPackage.Base.Extracters;
using CheckPackage.Base.Resource;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Dependencies;
using CheckPackage.Configuration.Services;
using CheckPackage.Core.Building;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Dependencies;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Regex;
using CheckPackage.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CheckPackage.Base.Module
{
    public class CheckPackageBaseModule : ICheckPackageModule
    {
        public void AddDependencies(CheckPackageOptions options)
        {
            // checks
            options.Checks.Add<PackageEntityParameterCheck, EntityParameterCheckDto>();
            options.Checks.Add<PackageEntityParameterLengthCheck, EntityParameterLengthCheckDto>();
            options.Checks.Add<PackageEntityParameterMDictCheck, EntityParameterMDictionaryCheckDto>();
            options.Checks.Add<PackageEntityParameterSDictCheck, EntityParameterSDictionaryCheckDto>();
            // conditions
            options.Conditions.Add<ContainsConditionResolver, ContainsConditionDto>();
            options.Conditions.Add<EqualConditionResolver, EqualConditionDto>();
            options.Conditions.Add<RegexConditionResolver, RegexConditionDto>();
            // extracts
            options.Extracters.Add<SubstringExtracter, SubstringExtractDto>();
            options.Extracters.Add<RegexTemplateExtracter, RegexTemplateExtractDto>();
            options.Extracters.Add<EntityParameterExtracter, EntityParameterExtractDto>();
            options.Extracters.Add<StaticParameterExtracter, StaticParameterExtractDto>();
            options.Extracters.Add<StaticValueExtracter, StaticParameterValueExtractDto>();
            // resources
            options.Resources.AddResource<MatrixDictionaryResource, MatrixDictionary, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<ParameterCheckRuleResource, ParameterCheckRule, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<Resource.EntityBuildRuleResource, Core.Building.EntityBuildRuleResource, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<EntityCheckRuleResource, EntityCheckRule, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<Resource.ParameterTemplateResource, Core.Regex.ParameterTemplateResource, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<Resource.GroupParametersTemplateResource, Core.Regex.GroupParametersTemplateResource, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<Resource.SimpleDictionaryResource, Core.Checks.SimpleDictionaryResource, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<Resource.StaticParameterResource, Extracters.StaticParameterResource, string>(ServiceLifetime.Singleton);            
        }

        public void AddDependencies(IServiceCollection collection)
        {
            // adapters of resources
            collection.AddSingleton<IConfigurationAdapter<EntityBuildRuleResource>, EntityBuildRuleAdapter>().
                AddSingleton<IConfigurationAdapter<ParameterCheckRule>, ParameterCheckRuleAdapter>().
                AddSingleton<IConfigurationAdapter<MatrixDictionary>, MatrixDictionariesAdapter>().
                AddSingleton<IConfigurationAdapter<EntityCheckRule>, EntityCheckRuleAdapter>().
                AddSingleton<IConfigurationAdapter<Core.Regex.ParameterTemplateResource>, ParameterTemplatesAdapter>().
                AddSingleton<IConfigurationAdapter<GroupParametersTemplateResource>, RegexTemplatesAdapter>().
                AddSingleton<IConfigurationAdapter<Core.Checks.SimpleDictionaryResource>, SimpleDictionariesAdapter>().
                AddSingleton<IConfigurationAdapter<Extracters.StaticParameterResource>, StaticParameterAdapter>();
            // converter strategies 
            collection.AddConfigurationConvertStrategies(builder =>
            {
                builder.AddCheckStrategy<ValueCheckConvertStrategy, EntityParameterValueCheckJson, EntityParameterCheckDto>();
                builder.AddCheckStrategy<MDictionaryCheckConvertStrategy, EntityParameterMDictionaryCheckJson, EntityParameterMDictionaryCheckDto>();
                builder.AddCheckStrategy<SDictionaryCheckConvertStrategy, EntityParameterSDictionaryCheckJson, EntityParameterSDictionaryCheckDto>();
                builder.AddCheckStrategy<LengthCheckConvertStrategy, EntityParameterLengthCheckJson, EntityParameterLengthCheckDto>();

                builder.AddConditionStrategy<ContainsConditionConvertStrategy, ContainsConditionJson, ContainsConditionDto>();
                builder.AddConditionStrategy<EqualConditionConvertStrategy, EqualConditionJson, EqualConditionDto>();
                builder.AddConditionStrategy<RegexConditionConvertStrategy, RegexMatchConditionJson, RegexConditionDto>();

                builder.AddExtractStrategy<RegexExtractConvertStrategy, RegexExtractJson, EntityParameterExtractDto>();
                builder.AddExtractStrategy<StaticExtractConvertStrategy, StaticExtractJson, EntityParameterExtractDto>();
                builder.AddExtractStrategy<StaticValueExtractConvertStrategy, StaticValueExtractJson, EntityParameterExtractDto>();
                builder.AddExtractStrategy<SubstringExtractConvertStrategy, SubstringExtractJson, EntityParameterExtractDto>();
            });                                                                                                                                    
        }
    }
}
