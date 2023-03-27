using Backend.Attributes;
using Backend.Common;
using Backend.Data;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Models.Responses.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Role.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public AdminController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _dbContext.Users.ToListAsync());
        }

        /// <summary>
        /// Get meals for specific dates and pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGeneralInformation(int? pageNumber, DateTime start, DateTime end)
        {
            int currPageNumber = pageNumber ?? 1;
            int currPageSize = Constants.Common.PAGE_SIZE;

            var meals = await _dbContext.Meals
                .Include(m => m.Food)
                .Include(m => m.User)
                .Where(m => m.Date >= start && m.Date <= end.EndOfDay())
                .Select(m =>
            new Meal
            {
                Id = m.Id,
                Calories = m.Calories,
                Date = m.Date,
                User = new() { Id = m.User.Id, Username = m.User.Username},
                Food = m.Food,
            }).OrderByDescending(o => o.Date).Skip((currPageNumber - 1) * currPageSize).Take(currPageSize).ToListAsync();

            int totalMeals = await _dbContext.Meals.Where(m => m.Date >= start && m.Date <= end.EndOfDay()).CountAsync();

            return Ok(new GetGeneralInformationResponse { Meals = meals, TotalMeals = totalMeals });
        }

        /// <summary>
        /// Delete a meal for a specific user
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteMeal(int? mealId)
        {
            Meal meal = await _dbContext.Meals.FindAsync(mealId);
            if (meal == null)
            {
                return NotFound("Meal not found");
            }

            _dbContext.Remove(meal);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Update a meal for a specific user
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateMeal([FromBody]Meal meal)
        {
            Meal oldMeal = await _dbContext.Meals.FindAsync(meal.Id);
            if (oldMeal == null)
            {
                return NotFound("Meal not found");
            }

            oldMeal.FoodId = meal.FoodId;
            oldMeal.Calories = meal.Calories;
            oldMeal.UserId = meal.UserId;
            oldMeal.Date = meal.Date;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Create a new meal for a specific user
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMeal([FromBody] Meal meal)
        {
            await _dbContext.Meals.AddAsync(meal);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
