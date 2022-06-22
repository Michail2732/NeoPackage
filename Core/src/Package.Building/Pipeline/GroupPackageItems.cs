using System;
using System.Linq;
using Package.Building.Context;
using Package.Domain.Factories;

namespace Package.Building.Pipeline
{
    internal sealed class GroupPackageItems: IBuildPipelineItem
    {
        internal static readonly string GroupIdProperty = "##group-id";
        private readonly IGroupingRule[] _groupingRules;

        public GroupPackageItems(IGroupingRule[] groupingRules)
        {
            _groupingRules = groupingRules ?? throw new ArgumentNullException(nameof(groupingRules));
            _groupingRules = _groupingRules.OrderBy(a => a.Priority).ToArray();
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

        public void Invoke(
            PackageBuildingContext context)
        {
            foreach (var groupingRule in _groupingRules)
            {
                var matchedItems = context.PackageItems.TakeMany(a => 
                    groupingRule.IsMatch(a, context));
                if (matchedItems.Count == 0)
                    continue;
                
                var itemBuilder = new PackageItemBuilder();
                matchedItems.ForEach(a => itemBuilder.AddChild(a));
                var groupId = groupingRule.GetGroupIdentity(matchedItems, context);
                itemBuilder.Properties[GroupIdProperty] = groupId ?? string.Empty; 
                context.InternalPackageItemBuilders.Add(itemBuilder);
            }
            Next?.Invoke(context);
        }
    }
}