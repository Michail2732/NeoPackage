using Package.Abstraction.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public class ValueTreeResource : IRepositoryItem<string>
    {
        public string Id { get; }
        public IReadOnlyList<ValueTreeNode> Nodes { get; private set; }

        public ValueTreeResource(IReadOnlyList<ValueTreeNode> nodes, string id)
        {
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }        
    }

    public class ValueTreeNode
    {
        public IReadOnlyList<string> Values { get; private set; }
        public IReadOnlyList<ValueTreeNode> Nodes { get; private set; }

        public ValueTreeNode(IReadOnlyList<string> values, IReadOnlyList<ValueTreeNode> nodes)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
        }
    }
}
