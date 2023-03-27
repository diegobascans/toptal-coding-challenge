using Backend.Auth;
using Backend.Common;
using Backend.Data;
using Backend.Models;
using Backend.Models.Classes;
using Backend.Models.Responses.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public UserController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all user meals between two times
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>List<Meal></returns>
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetUserMeals(int? pageNumber, DateTime start, DateTime end)
        {
            int currPageNumber = pageNumber ?? 1;
            int currPageSize = Constants.Common.PAGE_SIZE;
            int userId = JwtAuthenticationService.GetUserIdFromToken(HttpContext);

            var foodsIds = await _dbContext.Meals
                .Where(m => m.UserId == userId && !m.User.UserCheatingFoods.Any(ucf => ucf.FoodId == m.FoodId))
                .Select(m => m.FoodId).Distinct().ToListAsync();

            var caloriesPerDay = await _dbContext.Meals
                .Where(m =>
                    m.UserId == userId &&
                    m.Date >= start &&
                    m.Date <= end.Date.EndOfDay() &&
                    foodsIds.Any(fid => fid == m.FoodId)
                    )
                .GroupBy(m => new { m.Date.Date, m.User.CaloriesLimit })
                .Select(m => 
                    new DayMeal
                    {
                        Date = m.Key.Date,
                        Calories = m.Sum(s => s.Calories),
                        LimitExceeded = m.Sum(s => s.Calories) >= m.Key.CaloriesLimit
                    })
                .OrderByDescending(o => o.Date)
                .Skip((currPageNumber - 1) * currPageSize)
                .Take(currPageSize).ToListAsync();

            int totalCaloriesPerDay = await _dbContext.Meals
                .Where(m =>
                    m.UserId == userId &&
                    m.Date >= start &&
                    m.Date <= end.Date.EndOfDay() &&
                   foodsIds.Any(fid => fid == m.FoodId))
                .GroupBy(m => new { m.Date.Date, m.User.CaloriesLimit }).CountAsync();

            return Ok(new GetUserMealsResponse { CaloriesPerDay = caloriesPerDay, Count = totalCaloriesPerDay });
        }


        /// <summary>
        /// Get all cheated foods
        /// </summary>
        /// <returns>List<Meal></returns>
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetCheatedFoods()
        {
            int userId = JwtAuthenticationService.GetUserIdFromToken(HttpContext);

            var result = await (from foods in _dbContext.Foods
                                join userCheatingFood in _dbContext.UserCheatingFoods
                                   on new { Key1 = foods.Id, Key2 = userId }
                                   equals new { Key1 = userCheatingFood.FoodId, Key2 = userCheatingFood.UserId } into userCheatingFoodJoinFoods
                                from sub_userCheatingFoodJoinFoods in userCheatingFoodJoinFoods.DefaultIfEmpty()
                                select new CheatedFood
                                {
                                    Food = foods,
                                    Cheated = sub_userCheatingFoodJoinFoods != null
                                }).ToListAsync();


            return Ok(result);
        }


        /// <summary>
        /// Update the cheated list of foods
        /// </summary>
        /// <param name="foods"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdateCheatedFoods(List<Food> foods)
        {
            int userId = JwtAuthenticationService.GetUserIdFromToken(HttpContext);

            IQueryable<UserCheatingFood> foodsToRemove =
                _dbContext.UserCheatingFoods.Where(ucf =>
                ucf.UserId == userId &&
                !foods.Select(f => f.Id).Any(f => f == ucf.FoodId));

            _dbContext.UserCheatingFoods.RemoveRange(foodsToRemove);

            var foodsToAdd =
                foods.Where(f => !_dbContext.UserCheatingFoods.Any(ucf => userId == ucf.UserId && ucf.FoodId == f.Id));

            if (foodsToAdd.Count() > 0)
            {
                await _dbContext.UserCheatingFoods.AddRangeAsync(foodsToAdd.Select(fa => new UserCheatingFood { FoodId = fa.Id, UserId = userId }));
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        /// <summary>
        /// Add a new meal for a specific user
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> AddMeal([FromBody] Meal meal)
        {
            int userId = JwtAuthenticationService.GetUserIdFromToken(HttpContext);
            meal.UserId = userId;
            await _dbContext.Meals.AddAsync(meal);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
