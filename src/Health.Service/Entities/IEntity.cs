using System;

namespace Health.Service.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}