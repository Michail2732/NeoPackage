using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class PackageEntityReport
    {
        public IList<EntityStateResult> ParametersResult { get; }
        public IList<EntityStateResult> UserParametersResult { get; }
        public EntityStateResult EntityResult { get; }

        public PackageEntityReport(IList<EntityStateResult> parametersResult, 
            IList<EntityStateResult> userParametersResult, EntityStateResult entityResult)
        {
            ParametersResult = parametersResult ?? throw new ArgumentNullException(nameof(parametersResult));
            UserParametersResult = userParametersResult ?? throw new ArgumentNullException(nameof(userParametersResult));
            EntityResult = entityResult;
        }
    }
}
