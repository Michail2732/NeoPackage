using System.Collections.Generic;

namespace Package.Domain
{

    public interface IPackageEntity
    {
        string Id { get; }
        string Name { get; }
    }
    
}