using System;

namespace Health.Service.Entities
{
    public class Activity : IEntity
    {
        public Guid Id { get; set; }
        // type of activity
        public string Type { get; set; }
        // duration in minutes
        public decimal Duration { get; set; }
        // amount of calories burnt
        public decimal Calories { get; set; }
    }
}