using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Validation.Services
{
    public class EntityValidationEventArgs
    {
        public readonly EntityReport Report;
        public bool Cancel { get; set; }

        public EntityValidationEventArgs(EntityReport report)
        {
            Report = report ?? throw new ArgumentNullException(nameof(report));
        }
    }
}
