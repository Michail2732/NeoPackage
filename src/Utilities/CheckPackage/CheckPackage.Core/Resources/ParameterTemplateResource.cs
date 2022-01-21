using Package.Abstraction.Entities;
using System;

namespace CheckPackage.Core.Resources
{
    public sealed class ParameterTemplateResource : IEntity<string>
    {
        public string Id { get; private set; }
        public string Description { get; private set; }
        public string RegexTemplate { get; private set; }        

        public ParameterTemplateResource(string id, string regexPattern, 
            string description)
        {
            RegexTemplate = regexPattern ?? throw new ArgumentNullException(nameof(regexPattern));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description;            
        }
    }
}
