using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public class GroupParametersTemplateResource : IEntity<string>
    {
        public string Id { get; private set; }
        public string TemplateRaw { get; private set; }

        public GroupParametersTemplateResource(string id, string templateRaw)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            TemplateRaw = templateRaw ?? throw new ArgumentNullException(nameof(templateRaw));
        }
    }    
}
