using CheckPackage.Core.Checks;
using CheckPackage.Core.Resources;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.Base.Commands
{
    public class EntityTreeValuesCheck : EntityCheckCommand
    {
        public string TreeValuesId { get; }
        public IReadOnlyList<string> ParameterIds { get; }

        public EntityTreeValuesCheck(string treeId, string errorMessage,
            IReadOnlyList<string> parameterIds, ChildEntitiesCheck? childCheck = null) : base(errorMessage, childCheck)
        {
            TreeValuesId = treeId ?? throw new ArgumentNullException(nameof(treeId));
            ParameterIds = parameterIds;
        }
                
        // todo: message
        protected override Result InnerCheck(Entity_ entity, PackageContext context)
        {            
            var tree = context.RepositoryProvider.GetRepository<ValueTreeResource, string>().GetItem(TreeValuesId);
            if (tree == null)
                return Result.Error("todo: messages");
            ValueTreeNode? currentNode = new ValueTreeNode(new List<string>(), tree.Nodes);
            foreach (var parameterId in ParameterIds)
            {
                if (!entity.Parameters.ContainsKey(parameterId))
                    return Result.Error(context.Messages[MessageKeys.NotFoundParameterInEntity, parameterId, entity.Name]);
                string parameterValue = entity.Parameters[parameterId];
                currentNode = currentNode.Nodes.FirstOrDefault(a => a.Values.Contains(parameterValue));
                if (currentNode == null)
                    return Result.Error();
            }                        
            return new Result(true, null);
        }
    }
}
