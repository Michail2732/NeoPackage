using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct EntityParametersInfo
    {
        public readonly IReadOnlyDictionary<string, string> Parameters;
        public readonly IReadOnlyDictionary<string, UserParameter_> UserParemeters;

        public EntityParametersInfo(IReadOnlyDictionary<string, string> parameters, 
            IReadOnlyDictionary<string, UserParameter_> userParemeters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            UserParemeters = userParemeters ?? throw new ArgumentNullException(nameof(userParemeters));
        }
    }
}
