using CheckPackage.Core.Entities;
using CheckPackage.Core.Selectors;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class OneParameterSelector : ParameterSelectCommand
    {
        public string ParameterId { get; }
        public bool IsUserParameter { get; }

        public OneParameterSelector(string parameterId, bool isUserParameter)
        {
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            IsUserParameter = isUserParameter;
        }

        protected override IEnumerable<Parameter> InnerSelect(IEnumerable<Entity_> entities, PackageContext context)
        {
            if (IsUserParameter)
                foreach (var entity in entities)
                {
                    if (entity.UserParameters.ContainsKey(ParameterId))                    
                        yield return new Parameter(ParameterId, entity.UserParameters[ParameterId]);                                            
                }
            else
                foreach (var entity in entities)
                    if (entity.Parameters.ContainsKey(ParameterId))
                        yield return new Parameter(ParameterId, entity.Parameters[ParameterId]);
            yield break;
        }
    }
}
