using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class PackageReport
    {
        public IList<EntityReport> EntitiesReports { get; }
        public EntityStateResult PackageResult { get; }

        public PackageReport(IList<EntityReport> entitiesReports, EntityStateResult packageResult)
        {
            EntitiesReports = entitiesReports ?? throw new ArgumentNullException(nameof(entitiesReports));
            PackageResult = packageResult;
        }
    }
}
