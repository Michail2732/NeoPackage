using CheckPackage.Core.Conditions;
using CheckPackage.Core.Resources;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class EntityTreeValuesCondition: EntityConditionCommand
    {
        public string TreeValuesId { get; }
        public IReadOnlyList<string> ParameterIds { get; }

        public EntityTreeValuesCondition(string treeValuesId, IReadOnlyList<string> parameterIds)
        {
            TreeValuesId = treeValuesId ?? throw new ArgumentNullException(nameof(treeValuesId));
            ParameterIds = parameterIds ?? throw new ArgumentNullException(nameof(parameterIds));
        }

        //todo: messages
        protected override bool InnerResolve(Entity_ entity, PackageContext context)
        {
            var tree = context.RepositoryProvider.GetRepository<ValueTreeResource, string>().GetItem(TreeValuesId);
            if (tree == null)
            {
                context.Logger.LogError("todo: messages");
                return false;
            } 
            ValueTreeNode? currentNode = new ValueTreeNode(new List<string>(), tree.Nodes);
            foreach (var parameterId in ParameterIds)
            {
                if (!entity.Parameters.ContainsKey(parameterId))
                    return false;
                string parameterValue = entity.Parameters[parameterId];
                currentNode = currentNode.Nodes.FirstOrDefault(a => a.Values.Contains(parameterValue));
                if (currentNode == null)
                    return false;
            }
            return true;
        }
    }
}
