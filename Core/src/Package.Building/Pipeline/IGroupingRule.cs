using System.Collections.Generic;
using Package.Building.Context;
using Package.Domain;

namespace Package.Building.Pipeline
{
    public interface IGroupingRule
    {
        bool IsMatch(
            PackageItem item,
            PackageBuildingContext context);
        string GetGroupIdentity(
            IEnumerable<PackageItem> items,
            PackageBuildingContext context);
        uint Priority { get; }
        
    }
}