using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Health.Contracts;
using Health.Service.Dtos;
using Health.Service.Entities;
using Health.Service.Repositories;

namespace Health.Service.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<Nutrition> _nutritionRepository;
        private readonly IRepository<Sleep> _sleepRepository;

        public HealthController(IRepository<Activity> itemsRepository)
        {
            _activityRepository = itemsRepository;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<StatsDto>> GetStatsAsync()
        {
            var activities = (await _activityRepository.GetAllAsync());
            var nutrition = (await _nutritionRepository.GetAllAsync());
            var sleep = (await _sleepRepository.GetAllAsync());

            decimal totalCaloriesBurnt = 0;
            decimal totalActivitiesDuration = 0;
            foreach (var activity in activities)
            {
                totalActivitiesDuration += activity.Duration;
                totalCaloriesBurnt += activity.Calories;
            }

            decimal averageActivitiesDuration = totalActivitiesDuration / activities.Count;
            decimal averageCaloriesLost = totalCaloriesBurnt / activities.Count;

            decimal totalCaloriesGained = nutrition.Sum(x => x.Calories);
            decimal averageCaloriesGained = totalCaloriesGained / nutrition.Count;

            decimal totalSleepDuration = sleep.Sum(x => x.Duration);
            decimal averageSleepDuration = totalSleepDuration / sleep.Count;

            StatsDto stats = new StatsDto
            (
                totalCaloriesGained,
                totalCaloriesBurnt,
                averageActivitiesDuration,
                totalActivitiesDuration,
                averageSleepDuration,
                totalSleepDuration
            );

            return Ok(stats);
        }

        [HttpPost("activity")]
        public async Task<ActionResult<ActivityDto>> CreateActivityAsync(CreateActivityDto dto)
        {
            var activity = new Activity
            {
                Id = Guid.NewGuid(),
                Type = dto.Type,
                Duration = dto.Duration,
                Calories = dto.Calories
            };
            await _activityRepository.CreateAsync(activity);

            return CreatedAtAction("Activity record created", new {id = activity.Id}, activity.AsDto());
        }

        [HttpPost("nutrition")]
        public async Task<ActionResult<NutritionDto>> CreateNutritionAsync(CreateNutritionDto dto)
        {
            var nutrition = new Nutrition
            {
                Id = Guid.NewGuid(),
                DishName = dto.DishName,
                PortionSize = dto.PortionSize,
                Calories = dto.Calories
            };
            await _nutritionRepository.CreateAsync(nutrition);

            return CreatedAtAction("Nutrition record created", new {id = nutrition.Id}, nutrition.AsDto());
        }

        [HttpPost("sleep")]
        public async Task<ActionResult<SleepDto>> CreateSleepAsync(CreateSleepDto dto)
        {
            var sleep = new Sleep
            {
                Id = Guid.NewGuid(),
                Duration = dto.Duration
            };
            await _sleepRepository.CreateAsync(sleep);

            return CreatedAtAction("Sleep record created", new {id = sleep.Id}, sleep.AsDto());
        }
    }
}