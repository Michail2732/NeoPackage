using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Output;
using CheckPackage.Core.Selectors;
using Package.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Configuration.Json.Binder
{
    public class JsonToCommandBindService : IJsonToCommandBindService
    {
        private readonly BinderServiceBuildArgs _binders;

        internal JsonToCommandBindService(BinderServiceBuildArgs binders)
        {
            _binders = binders ?? throw new ArgumentNullException(nameof(binders));
        }

        public EntityCheckCommand Bind(EntityCheckJson json)
        {
            var binder = _binders.EntityCheckCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public PackageCheckCommand Bind(PackageCheckJson json)
        {
            var binder = _binders.PackageCheckCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var binder = _binders.ParameterCheckCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public EntityConditionCommand Bind(EntityConditionJson json)
        {
            var binder = _binders.EntityConditionCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public PackageConditionCommand Bind(PackageConditionJson json)
        {
            var binder = _binders.PackageConditionCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public ParameterConditionCommand Bind(ParameterConditionJson json)
        {
            var binder = _binders.ParameterConditionCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public EntityOutputCommand Bind(EntityOutputJson json)
        {
            var binder = _binders.EntityOutputCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public PackageOutputCommand Bind(PackageOutputJson json)
        {
            var binder = _binders.PackageOutputCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public ParameterOutputCommand Bind(ParameterOutputJson json)
        {
            var binder = _binders.ParameterOutputCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public ParameterExtractCommand Bind(ParameterExtractJson json)
        {
            var binder = _binders.ParameterExtractCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }

        public ParameterSelectCommand Bind(ParametersSelectorJson json)
        {
            var binder = _binders.ParameterSelectCommandBinder.FirstOrDefault(a => a.CanBind(json)) ??
                throw new ConfigurationException($"Could not found command binder for type {json.GetType()}");
            return binder.Bind(json);
        }
    }
}
