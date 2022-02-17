using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using CheckPackage.Core.Resources;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Base.Commands
{
    public class ParameterTreeValuesCheck : ParameterCheckCommand
    {
        public string TreeValuesId { get; }

        public ParameterTreeValuesCheck(string treeId, string errorMessage) : base(errorMessage)
        {
            TreeValuesId = treeId ?? throw new ArgumentNullException(nameof(treeId));            
        }

        // todo: messages
        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {
            var tree = context.RepositoryProvider.GetRepository<ValueTreeResource, string>().GetItem(TreeValuesId);
            if (tree == null)
                return Result.Error("todo: messages");
            ValueTreeNode? currentNode = new ValueTreeNode(new List<string>(), tree.Nodes);
            string parameterValue = parameter.Value.ToString();
            return new Result(tree.Nodes.Any(a => a.Values.Contains(parameterValue)), null);
        }
    }
}
