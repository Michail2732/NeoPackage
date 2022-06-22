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
        
        private IBuildPipelineItem? _next;
        IBuildPipelineItem? IBuildPipelineItem.Next
        {
            get => _next;
            set
            {
                if (value != null && this.HasLoop(value))
                    throw new ArgumentException("cyclic link detected");
                _next = value;
            }
        }

        public void Invoke(PackageBuildingContext context)
        {
            var newItems = BuildItems(context);
            if (newItems.Any())
                context.InternalPackageItems.AddRange(newItems);
        }

        public abstract ICollection<PackageItem> BuildItems(PackageBuildingContext context);
    }
}