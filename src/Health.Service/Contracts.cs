using System;

namespace Health.Contracts
{
    public record OrderItemCreated(Guid ItemId, string Name, string Description);
    public record OrderItemUpdated(Guid ItemId, string Name, string Description);
    public record OrderItemDeleted(Guid ItemId);
}