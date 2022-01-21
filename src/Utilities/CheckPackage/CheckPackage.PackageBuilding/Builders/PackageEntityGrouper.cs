using CheckPackage.Core.Conditions;
using CheckPackage.PackageBuilding.Resource;
using Package.Abstraction.Entities;
using Package.Building.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageBuilding.Builders
{
    public class PackageEntityGrouper : IEntityGrouper
    {
        private readonly IConditionService _conditionsService;

        public PackageEntityGrouper(IConditionService conditionsService)
        {
            _conditionsService = conditionsService ?? throw new ArgumentNullException(nameof(conditionsService));
        }

        public IEnumerable<IGrouping<GroupKey, PackageEntity>> Group(IEnumerable<PackageEntity> entities,
            uint level, PackageContext context)
        {
            var resource = context.ResourceProvider.GetStorage<EntityBuildRuleResource, string>();
            var rules = resource.Get(a => a.EntityLevel == level).OrderBy(a => a.Priority);
            List<IGrouping<GroupKey, PackageEntity>> result = new List<IGrouping<GroupKey, PackageEntity>>();
            List<PackageEntity> allEntities = entities.ToList();
            IEnumerable<PackageEntity> matchedEntities = new List<PackageEntity>();
            foreach (var rule in rules)
            {
                if (rule.Conditions != null)
                {                    
                    matchedEntities = allEntities.Where(a => _conditionsService.Resolve(a, rule.Conditions));
                    foreach (var matchEntity in matchedEntities)
                        allEntities.Remove(matchEntity);
                }
                else
                    matchedEntities = allEntities;
                result.Add(new PackageEntitiesGroup(matchedEntities, new GroupKey(new Dictionary<string, string>())));
            }
            return result;
        }

        public async Task<IEnumerable<IGrouping<GroupKey, PackageEntity>>> GroupAsync(IEnumerable<PackageEntity> entities,
            uint level, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var resource = context.ResourceProvider.GetStorage<EntityBuildRuleResource, string>();
            var rules = (await resource.GetAsync(a => a.EntityLevel == level, ct)).OrderBy(a => a.Priority);
            List<IGrouping<GroupKey, PackageEntity>> result = new List<IGrouping<GroupKey, PackageEntity>>();
            List<PackageEntity> allEntities = entities.ToList();
            IEnumerable<PackageEntity> matchedEntities = new List<PackageEntity>();                                    
            foreach (var rule in rules)
            {
                ct.ThrowIfCancellationRequested();
                if (rule.Conditions != null)
                {
                    matchedEntities = allEntities.Where(a => _conditionsService.Resolve(a, rule.Conditions));
                    foreach (var matchEntity in matchedEntities)
                        allEntities.Remove(matchEntity);
                }
                else
                    matchedEntities = allEntities;
                result.Add(new PackageEntitiesGroup(matchedEntities, new GroupKey(new Dictionary<string, string>())));
            }
            return result;
        }
    }
}
