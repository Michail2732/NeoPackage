using System;
using System.Linq;
using Package.Building.Exceptions;
using Package.Building.Context;
using Package.Building.Pipeline;
using Package.Domain;
using Package.Domain.Factories;

namespace Package.Building.Services
{
    public class PackageBuildingService : IPackageBuildingService
    {
        private readonly IPackageBuildingContextFactory _contextFactory;

        public PackageBuildingService(
            IPackageBuildingContextFactory contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public RootPackage BuildPackage(PackageBuildingPipeline pipeline)
        {
            var context = _contextFactory.Build();
            try
            {

                context.UserParameters = pipeline.Parammeters;
                pipeline.Current?.Invoke(context);
            }
            catch (Exception e) when (!(e is PackageBuildingException))
            { throw new PackageBuildingException("Building error", e); }

            return CreatePackageFromContext(context);
        }


        private RootPackage CreatePackageFromContext(
            PackageBuildingContext context)
        {
            var items = context.InternalPackageItems.TakeAll();
            if (context.InternalPackageItemBuilders.Count != 0)
            {
                items.AddRange(context.InternalPackageItemBuilders.TakeAll()
                    .Select(a =>a.Build()));
            }

            var rootBuilder = new RootPackageBuilder();
            foreach (var packageItem in items)
                rootBuilder.AddChild(packageItem);
            return rootBuilder.Build();
        }
    }
}
