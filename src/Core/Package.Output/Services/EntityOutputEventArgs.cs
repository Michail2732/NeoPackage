using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Output.Services
{
    public class EntityOutputEventArgs: EventArgs
    {
        public readonly EntityReport Report;
        public bool Cancel { get; set; }

        public EntityOutputEventArgs(EntityReport report)
        {
            Report = report ?? throw new ArgumentNullException(nameof(report));
        }
    }
}
