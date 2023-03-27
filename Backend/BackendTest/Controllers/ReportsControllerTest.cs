using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.Classes;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace BackendTest.Controllers
{
    public class ReportsControllerTest
    {
        private readonly Mock<ApiDbContext> _dbContext;
        private readonly ReportsController _controller;

        public ReportsControllerTest()
        {
            _dbContext = new Mock<ApiDbContext>();
            _controller = new ReportsController(_dbContext.Object);
        }


        [Fact]
        public void GetReportInformation_Success_OkReportInformation()
        {
            List<Meal> meals = new List<Meal> {
                new Meal { Id = 1, Date = DateTime.Today }, 
                new Meal { Id = 2, Date = DateTime.Today.AddDays(-1) }, 
                new Meal { Id = 3, Date = DateTime.Today.AddDays(-3)  }, 
                new Meal { Id = 4, Date = DateTime.Today.AddDays(-5)  }, 
                new Meal { Id = 5, Date = DateTime.Today.AddDays(-10)  }, 
                new Meal { Id = 6, Date = DateTime.Today.AddDays(-12)  }, 
            };
            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            IActionResult result = _controller.GetReportInformation().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(2));

            ((ReportInformation)okResult.Value).LastWeekEntries = 4;
            ((ReportInformation)okResult.Value).PastWeekEntries = 2;
        }


        [Fact]
        public void GetAverageCaloriesPerUser_Success_OkReturnsCaloriesPerUser()
        {
            List<Meal> mealsUser1 = new List<Meal> {
                new Meal { Id = 1, Calories = 10, Date = DateTime.Today},
                new Meal { Id = 2, Calories = 20, Date = DateTime.Today.AddDays(-1)},
                new Meal { Id = 3, Calories = 30, Date = DateTime.Today.AddDays(-3)},
            };

            List<Meal> mealsUser2 = new List<Meal> {
                new Meal { Id = 4, Calories = 5, Date = DateTime.Today.AddDays(-5)},
                new Meal { Id = 5, Calories = 10, Date = DateTime.Today.AddDays(-5)},
                new Meal { Id = 6, Calories = 15, Date = DateTime.Today.AddDays(-5)},
            };

            List<User> users = new List<User> {
                new User { Id = 1, Username = "Diego", Meals = mealsUser1 },
                new User { Id = 2, Username = "Jose" , Meals = mealsUser2 }
            };

            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            IActionResult result = _controller.GetAverageCaloriesPerUser().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Users, Times.Once);

            ((List<CaloriesPerUser>)okResult.Value).Count.Should().Be(2);
            ((List<CaloriesPerUser>)okResult.Value)[0].Calories.Should().Be(8);
            ((List<CaloriesPerUser>)okResult.Value)[1].Calories.Should().Be(4);
        }

        [Fact]
        public void GetAverageCaloriesPerUser_Success_OkReturnsZeroCalories()
        {
            List<User> users = new List<User> { new User { Id = 1, Username = "Diego", Meals = new List<Meal> { } } };
            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            IActionResult result = _controller.GetAverageCaloriesPerUser().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Users, Times.Once);

            ((List<CaloriesPerUser>)okResult.Value).Count.Should().Be(1);
            ((List<CaloriesPerUser>)okResult.Value)[0].Calories.Should().Be(0);
        }

        [Fact]
        public void GetAverageCaloriesPerUser_Success_OkReturnsNullMeals()
        {
            List<User> users = new List<User> { new User { Id = 1, Username = "Diego" } };
            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            IActionResult result = _controller.GetAverageCaloriesPerUser().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Users, Times.Once);

            ((List<CaloriesPerUser>)okResult.Value).Count.Should().Be(1);
            ((List<CaloriesPerUser>)okResult.Value)[0].Calories.Should().Be(0);
        }

    }
}
