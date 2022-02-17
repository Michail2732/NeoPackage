using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Selectors;
using CheckPackage.PackageBuilding.Rules;
using Package.Abstraction.Entities;
using Package.Building.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageBuilding.Builders
{
    public class EntityBuilder : IEntityBuilder
    {
        private readonly IExtractService _extractsService;
        private readonly IConditionService _conditionsService;
        private readonly ISelectorService _selectService;

        public EntityBuilder(IExtractService extractsService, 
            IConditionService conditionsService, ISelectorService selectService)
        {
            _extractsService = extractsService ?? throw new ArgumentNullException(nameof(extractsService));
            _conditionsService = conditionsService ?? throw new ArgumentNullException(nameof(conditionsService));
            _selectService = selectService ?? throw new ArgumentNullException(nameof(selectService));
        }

        public EntityBuildingResult Build(IEnumerable<Entity_> children, uint level, PackageContext context)
        {
            var rule = GetRule(children, level, context, CancellationToken.None);
            if (rule == null)
                return new EntityBuildingResult();
            return ExtractValues(children, rule, context, CancellationToken.None);
        }

        public async Task<EntityBuildingResult> BuildAsync(IEnumerable<Entity_> children, uint level, PackageContext context, CancellationToken ct)
        {
            var rule =await Task.Run(() =>  GetRule(children, level,  context, CancellationToken.None), ct);
            if (rule == null)
                return new EntityBuildingResult();
            return await Task.Run(() => ExtractValues(children, rule, context, CancellationToken.None), ct);
        }        

        private EntityBuildRule? GetRule(IEnumerable<Entity_> children, uint level, PackageContext context, CancellationToken ct)
        {
            var resource = context.RepositoryProvider.GetRepository<EntityBuildRule, string>();
            var rules = resource.Get(a => a.EntityLevel == level).OrderBy(a => a.Priority);                        
            foreach (var rule in rules)            
                if (rule.Conditions == null || rule.Conditions != null && children.All(a =>                    
                        _conditionsService.Resolve(a ,rule.Conditions)))
                        return rule;            
            return null;
        }


        private EntityBuildingResult ExtractValues(IEnumerable<Entity_> children, EntityBuildRule rule, PackageContext context, CancellationToken ct)
        {                        
            Dictionary<string, string> resultParameters = new Dictionary<string, string>();
            Dictionary<string, UserParameter_> resultUserParameters = new Dictionary<string, UserParameter_>();
            IEnumerable<Entity_> matchedEntities = new List<Entity_>();
            IEnumerable<Parameter> parameters = new List<Parameter>();            
            foreach (var parameterRule in rule.ParameterRules)
            {
                matchedEntities = children;
                if (parameterRule.Conditions != null)
                    matchedEntities = children.Where(a =>_conditionsService.Resolve(a, parameterRule.Conditions));
                if (matchedEntities.Any())
                {
                    if (parameterRule.Selector != null)
                        parameters = _selectService.Select(matchedEntities, parameterRule.Selector);
                    parameters = _extractsService.Extract(parameters, parameterRule.Extracter);
                    if (parameters.Count() > 0)
                        foreach (var parameter in parameters)                        
                            if (parameter.IsString)
                                resultParameters[parameter.Id] = (string)parameter.Value;
                            else
                                resultUserParameters[parameter.Id] = new UserParameter_(parameter.Id, parameter.Value);                        
                }
            }
            string name = children.Select(a => a.Parameters.ContainsKey(rule.GroupBy)
                ? a.Parameters[rule.GroupBy] : "").Where(a => !string.IsNullOrEmpty(a)).Distinct().FirstOrDefault() ?? "unknown";
            return new EntityBuildingResult(resultParameters, resultUserParameters, name, Guid.NewGuid().ToString());
        }

    }
}
