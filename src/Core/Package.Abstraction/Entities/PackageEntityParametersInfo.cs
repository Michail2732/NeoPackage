using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct PackageEntityParametersInfo
    {
        public readonly IReadOnlyDictionary<string, string> Parameters;
        public readonly IReadOnlyDictionary<string, UserParameter> UserParemeters;

        public PackageEntityParametersInfo(IReadOnlyDictionary<string, string> parameters, 
            IReadOnlyDictionary<string, UserParameter> userParemeters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            UserParemeters = userParemeters ?? throw new ArgumentNullException(nameof(userParemeters));
        }
    }
}
