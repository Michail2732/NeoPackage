using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Validation.Services
{
    public class EntityValidationEventArgs
    {
        public readonly PackageEntityReport Report;
        public bool Cancel { get; set; }

        public EntityValidationEventArgs(PackageEntityReport report)
        {
            Report = report ?? throw new ArgumentNullException(nameof(report));
        }
    }
}
