using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public class RegexItemResource: IRepositoryItem<string>
    {
        public string Id { get; }
        public string RegexPattern { get; }
        public bool IsComposite { get; }

        public RegexItemResource(string id, string regexPattern, bool isComposite)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            RegexPattern = regexPattern ?? throw new ArgumentNullException(nameof(regexPattern));
            IsComposite = isComposite;
        }
    }
}
