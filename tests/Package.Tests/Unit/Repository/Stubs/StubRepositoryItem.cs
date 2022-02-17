using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Tests.Unit.Repository
{
    public class StubRepositoryItem : IRepositoryItem<string>
    {
        public string Id { get; set; }
    }

    public class StubRepositoryItem2 : IRepositoryItem<string>
    {
        public string Id { get; set; }
    }
}
