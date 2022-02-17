using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CheckPackage.Configuration.Json.Binder
{
    internal class BinderServiceBuildArgs
    {
        public readonly IEnumerable<IEntityCheckCommandBinder>  EntityCheckCommandBinder;
        public readonly IEnumerable<IPackageCheckCommandBinder>  PackageCheckCommandBinder;
        public readonly IEnumerable<IParameterCheckCommandBinder>  ParameterCheckCommandBinder;
        public readonly IEnumerable<IEntityConditionCommandBinder>  EntityConditionCommandBinder;
        public readonly IEnumerable<IPackageConditionCommandBinder>  PackageConditionCommandBinder;
        public readonly IEnumerable<IParameterConditionCommandBinder>  ParameterConditionCommandBinder;
        public readonly IEnumerable<IParameterExtractCommandBinder>  ParameterExtractCommandBinder;
        public readonly IEnumerable<IEntityOutputCommandBinder>  EntityOutputCommandBinder;
        public readonly IEnumerable<IPackageOutputCommandBinder>  PackageOutputCommandBinder;
        public readonly IEnumerable<IParameterOutputCommandBinder>  ParameterOutputCommandBinder;
        public readonly IEnumerable<IParameterSelectCommandBinder>  ParameterSelectCommandBinder;

        public BinderServiceBuildArgs(IEnumerable<IEntityCheckCommandBinder> entityCheckCommandBinder, IEnumerable<IPackageCheckCommandBinder> packageCheckCommandBinder, IEnumerable<IParameterCheckCommandBinder> parameterCheckCommandBinder, IEnumerable<IEntityConditionCommandBinder> entityConditionCommandBinder, IEnumerable<IPackageConditionCommandBinder> packageConditionCommandBinder, IEnumerable<IParameterConditionCommandBinder> parameterConditionCommandBinder, IEnumerable<IParameterExtractCommandBinder> parameterExtractCommandBinder, IEnumerable<IEntityOutputCommandBinder> entityOutputCommandBinder, IEnumerable<IPackageOutputCommandBinder> packageOutputCommandBinder, IEnumerable<IParameterOutputCommandBinder> parameterOutputCommandBinder, IEnumerable<IParameterSelectCommandBinder> parameterSelectCommandBinder)
        {
            EntityCheckCommandBinder = entityCheckCommandBinder ?? throw new ArgumentNullException(nameof(entityCheckCommandBinder));
            PackageCheckCommandBinder = packageCheckCommandBinder ?? throw new ArgumentNullException(nameof(packageCheckCommandBinder));
            ParameterCheckCommandBinder = parameterCheckCommandBinder ?? throw new ArgumentNullException(nameof(parameterCheckCommandBinder));
            EntityConditionCommandBinder = entityConditionCommandBinder ?? throw new ArgumentNullException(nameof(entityConditionCommandBinder));
            PackageConditionCommandBinder = packageConditionCommandBinder ?? throw new ArgumentNullException(nameof(packageConditionCommandBinder));
            ParameterConditionCommandBinder = parameterConditionCommandBinder ?? throw new ArgumentNullException(nameof(parameterConditionCommandBinder));
            ParameterExtractCommandBinder = parameterExtractCommandBinder ?? throw new ArgumentNullException(nameof(parameterExtractCommandBinder));
            EntityOutputCommandBinder = entityOutputCommandBinder ?? throw new ArgumentNullException(nameof(entityOutputCommandBinder));
            PackageOutputCommandBinder = packageOutputCommandBinder ?? throw new ArgumentNullException(nameof(packageOutputCommandBinder));
            ParameterOutputCommandBinder = parameterOutputCommandBinder ?? throw new ArgumentNullException(nameof(parameterOutputCommandBinder));
            ParameterSelectCommandBinder = parameterSelectCommandBinder ?? throw new ArgumentNullException(nameof(parameterSelectCommandBinder));
        }
    }
}
