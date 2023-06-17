using System;

namespace Health.Service.Entities
{
    public class Sleep : IEntity
    {
        public Guid Id { get; set; }
        // duration in minutes
        public decimal Duration { get; set; }
    }
}