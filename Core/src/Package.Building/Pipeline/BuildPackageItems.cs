using System;
using System.Collections.Generic;
using System.Linq;
using Package.Building.Context;
using Package.Domain.Factories;

namespace Package.Building.Pipeline
{
    internal sealed class BuildPackageItems: IBuildPipelineItem
    {
        private readonly IFillingRule[] _fillingRules;

        public BuildPackageItems(IFillingRule[] fillingRules)
        {
            _fillingRules = fillingRules ?? throw new ArgumentNullException(nameof(fillingRules));
        }

        private IBuildPipelineItem? _next;
        public IBuildPipelineItem? Next
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
            var builders = new List<PackageItemBuilder>(context.InternalPackageItemBuilders.Count);
            foreach (var fillingRule in _fillingRules)
            {
                var matchedBuilders = context.InternalPackageItemBuilders.TakeMany(a
                        => fillingRule.IsMatch(a, context));
                foreach (var matchBuilder in matchedBuilders)
                {
                    fillingRule.Fill(matchBuilder, context);
                    if (!builders.Contains(matchBuilder))
                        builders.Add(matchBuilder);
                }
            }
            if (builders.Count > 0)
                context.InternalPackageItems.AddRange(builders.Select(a=> a.Build()));
            Next?.Invoke(context);
        }
    }
}