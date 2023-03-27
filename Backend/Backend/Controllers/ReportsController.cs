using Backend.Attributes;
using Backend.Common;
using Backend.Data;
using Backend.Models.Classes;
using Backend.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Role.Admin)]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ReportsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the entries information
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetReportInformation()
        {
            DateTime lastWeekStartDate = DateTime.Today.AddDays(-6);
            DateTime lastWeekEndDate = DateTime.Today.EndOfDay();

            int lastWeekEntries = await _dbContext.Meals.Where(m => lastWeekStartDate <= m.Date && lastWeekEndDate >= m.Date).CountAsync();

            DateTime pastWeekStartDate = DateTime.Today.AddDays(-13);
            DateTime pastWeekEndDate = DateTime.Today.AddDays(-7).EndOfDay();

            int pastWeekEntries = await _dbContext.Meals.Where(m => pastWeekStartDate <= m.Date && pastWeekEndDate >= m.Date).CountAsync();

            return Ok(new ReportInformation
            {
                LastWeekStartDate = lastWeekStartDate,
                LastWeekEndDate = lastWeekEndDate,
                LastWeekEntries = lastWeekEntries,
                PastWeekStartDate = pastWeekStartDate,
                PastWeekEndDate = pastWeekEndDate,
                PastWeekEntries = pastWeekEntries
            });
        }

        /// <summary>
        /// Get the average calories for users in the last 7 days
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAverageCaloriesPerUser()
        {
            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today.EndOfDay();

            var result = await _dbContext.Users
                .Select(u => new CaloriesPerUser
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Calories = (u.Meals == null ? 0 : u.Meals.Where(m => m.Date >= startDate && m.Date <= endDate).Sum(m => m.Calories)) / 7

                }).ToListAsync();

            return Ok(result);
        }
    }
}
