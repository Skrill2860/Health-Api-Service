using System;
using System.ComponentModel.DataAnnotations;

namespace Health.Service.Dtos
{
    public record ActivityDto(Guid Id, string Name, decimal Duration, decimal Calories);
    public record CreateActivityDto([Required] string Type, [Range(0, 10000)] decimal Duration, [Range(0, 10000)] decimal Calories);

    public record SleepDto(Guid Id, [Range(0, 2000)] decimal Duration);
    public record CreateSleepDto([Range(0, 2000)] decimal Duration);

    public record NutritionDto(Guid Id, string DishName, [Range(0, 100000)] decimal PortionSize, [Range(0, 10000)] decimal Calories);
    public record CreateNutritionDto([Required] string DishName, [Range(0, 100000)] decimal PortionSize, [Range(0, 10000)] decimal Calories);

    public record StatsDto(
        decimal TotalCaloriesGained,
        decimal TotalCaloriesBurnt,
        decimal AverageActivityDuration,
        decimal TotalActivityDuration,
        decimal AverageSleepDuration,
        decimal TotalSleepDuration);
}