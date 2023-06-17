using Health.Service.Dtos;
using Health.Service.Entities;

namespace Health.Service
{
    public static class Extensions
    {
        public static ActivityDto AsDto(this Activity item) => new(item.Id, item.Type, item.Duration, item.Calories);
        public static SleepDto AsDto(this Sleep item) => new(item.Id, item.Duration);
        public static NutritionDto AsDto(this Nutrition item) => new(item.Id, item.DishName, item.PortionSize, item.Calories);
    }
}