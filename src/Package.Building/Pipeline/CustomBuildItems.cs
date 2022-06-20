using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Package.Building.Context;
using Package.Domain;

namespace Package.Building.Pipeline
{
    public abstract class CustomBuildItems: IBuildPipelineItem
    {
        protected readonly IFillingRule[] FillingRules;

        protected CustomBuildItems(IFillingRule[] fillingRules)
        {
            FillingRules = fillingRules ?? throw new ArgumentNullException(nameof(fillingRules));
        }
        
        IBuildPipelineItem? IBuildPipelineItem.Next { get; set; }
        
        public void Invoke(PackageBuildingContext context)
        {
            var newItems = BuildItems(context);
            if (newItems.Any())
                context.InternalPackageItems.AddRange(newItems);
        }

        public abstract ICollection<PackageItem> BuildItems(PackageBuildingContext context);
    }
}