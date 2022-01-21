using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Package.Building.Builders;
using Package.Abstraction.Entities;
using Package.Building.Extensions;
using Package.Localization;
using Package.Abstraction.Services;

namespace Package.Building.Services
{
    public class PackageBuildingService : IPackageBuildingService
    {
        private readonly IPackageEntityBasisBuilder _basisBuilder;
        private readonly IPackageEntityBuilder _entityBuilder;
        private readonly IPackageBuilder _packageBuilder;        
        private readonly IEntityGrouper _entityGrouper;
        private readonly IPackageContextBuilder _contextBuilder;

        public event EventHandler<LevelBuildEventArgs>? LevelBuilded;

        public PackageBuildingService(IPackageEntityBasisBuilder basisBuilder, IPackageEntityBuilder entityBuilder,
            IPackageBuilder packageBuilder, IEntityGrouper entityGrouper, IPackageContextBuilder contextBuilder)
        {
            _basisBuilder = basisBuilder ?? throw new ArgumentNullException(nameof(basisBuilder));
            _entityBuilder = entityBuilder ?? throw new ArgumentNullException(nameof(entityBuilder));
            _packageBuilder = packageBuilder ?? throw new ArgumentNullException(nameof(packageBuilder));
            _entityGrouper = entityGrouper ?? throw new ArgumentNullException(nameof(entityGrouper));
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public Package_ Create()
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                var entities = _basisBuilder.Build(context)?.Select(a => PackageEntityFactory.
                        Create(a, new List<PackageEntity>())).ToList() ?? new List<PackageEntity>();
                uint level = 0;
                while (!OnLevelBuilded(entities, level) && entities.Count > 0)
                {                    
                    var groupedEntities = _entityGrouper.Group(entities, level, context);
                    if (groupedEntities == null || !groupedEntities.Any()) break;
                    level++;
                    foreach (var entitiesGroup in groupedEntities)
                    {
                        var entityBuildResult = _entityBuilder.Build(entitiesGroup, level, context);
                        var entity = PackageEntityFactory.Create(entityBuildResult, entitiesGroup.ToList());
                        entities.ReplaceItems(entitiesGroup, entity);
                    }
                }
                var packageBuildResult = _packageBuilder.Build(entities, context);
                return PackageEntityFactory.Create(packageBuildResult, entities);
            }
            catch (Exception ex){ throw new PackageBuildingException($"Error occurred building package", ex); }            
        }

        public async Task<Package_> CreateAsync(CancellationToken ct)
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                var entitiesResults = await _basisBuilder.BuildAsync(context, ct);
                var entities = entitiesResults.Select(a => PackageEntityFactory.
                        Create(a, new List<PackageEntity>())).ToList() ?? new List<PackageEntity>();
                uint level = 0;
                while (!OnLevelBuilded(entities, level) && entities.Count > 0)
                {
                    ct.ThrowIfCancellationRequested();                    
                    var groupedEntities = await _entityGrouper.GroupAsync(entities, level, context, ct);
                    if (groupedEntities == null || !groupedEntities.Any()) break;
                    level++;
                    foreach (var entitiesGroup in groupedEntities)
                    {                        
                        var entityResult = await _entityBuilder.BuildAsync(entitiesGroup,  level, context, ct);
                        var entityBuildResult = await _entityBuilder.BuildAsync(entitiesGroup, level, context, ct);
                        var entity = PackageEntityFactory.Create(entityBuildResult, entitiesGroup.ToList());
                        entities.ReplaceItems(entitiesGroup, entity);
                    }
                }
                var packageBuildResult = await _packageBuilder.BuildAsync(entities, context, ct);
                return PackageEntityFactory.Create(packageBuildResult, entities);
            }
            catch (Exception ex) { throw new PackageBuildingException($"Error ocurred building package", ex); }
        }
        

        private bool OnLevelBuilded(IReadOnlyList<PackageEntity> entities, uint level)
        {
            var evnt = LevelBuilded;
            bool cancel = false;
            if (evnt != null)
            {
                var args = new LevelBuildEventArgs(entities, level);
                evnt.Invoke(this, args);
                cancel = args.Cancel;
            }
            return cancel;
        }
    }
}
