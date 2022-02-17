using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Services
{
    public class LevelBuildEventArgs : EventArgs
    {
        public IReadOnlyList<Entity_> Entities { get; }
        public uint Level { get; }
        public bool Cancel { get; set; }

        public LevelBuildEventArgs(IReadOnlyList<Entity_> entities, uint level)
        {
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
            Level = level;
        }
    }
}
