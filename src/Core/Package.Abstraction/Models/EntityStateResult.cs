using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct EntityStateResult
    {
        public readonly string EntityId;
        public readonly string? Message;
        public readonly Critical Status;

        public EntityStateResult(string entityId, string? message, Critical status)
        {
            EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
            Message = message;
            Status = status;
        }
    }
}
