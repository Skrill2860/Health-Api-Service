using System;

namespace Health.Service.Entities
{
    public class Nutrition : IEntity
    {
        public Guid Id { get; set; }
        // name of dish
        public string DishName { get; set; }
        // portion size in gramms
        public decimal PortionSize { get; set; }
        // amount of calories gained
        public decimal Calories { get; set; }
    }
}