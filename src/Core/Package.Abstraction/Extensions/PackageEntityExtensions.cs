using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Abstraction.Extensions
{
    public static class PackageEntityExtensions
    {
        public static uint GetHeigtEntity(this PackageEntity packageEntity)
        {
            if (packageEntity == null) return 0;
            return 1 + packageEntity.Children.Max(a => a.GetHeigtEntity());
        }
    }
}
