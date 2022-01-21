using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public readonly struct EntityStateResult
    {
        public readonly string EntityId;
        public readonly string? Message;
        public readonly State Status;

        public EntityStateResult(string entityId, string? message, State status)
        {
            EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
            Message = message;
            Status = status;
        }
    }
}
