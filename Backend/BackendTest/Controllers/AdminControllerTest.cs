using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.Responses.Admin;
using BackendTest.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace BackendTest.Controllers
{
    public class AdminControllerTest
    {
        private readonly Mock<ApiDbContext> _dbContext;
        private readonly AdminController _controller;

        public AdminControllerTest()
        {
            _dbContext = new Mock<ApiDbContext>();
            _controller = new AdminController(_dbContext.Object);
        }

        [Fact]
        public void GetUsers_Success_OkReturnUsers()
        {
            List<User> users= new List<User>()
            {
                new User() { Id = 1}
            };

            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            IActionResult result = _controller.GetUsers().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Users, Times.Once);
            ((List<User>)okResult.Value).Count.Should().Be(1);
        }
        
        [Fact]
        public void GetGeneralInformation_Success_OkReturnsMeals()
        {
            User user = new User { Id = 1 };

            List<Meal> meals = new List<Meal>()
            {
                new Meal { Id = 1, Date = DateTime.Today.AddDays(-1), Calories = 10, User = user, Food = new Food { Id = 1 } },
                new Meal { Id = 2, Date = DateTime.Today.AddDays(-1), Calories = 11, User = user, Food = new Food { Id = 2 } },
                new Meal { Id = 3, Date = DateTime.Today.AddDays(-1), Calories = 12, User = user, Food = new Food { Id = 3 } },
                new Meal { Id = 4, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } }
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            IActionResult result = _controller.GetGeneralInformation(1, DateTime.Today.AddDays(-5), DateTime.Today).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(2));
            ((GetGeneralInformationResponse)okResult.Value).Meals.Count.Should().Be(4);
            ((GetGeneralInformationResponse)okResult.Value).TotalMeals.Should().Be(4);
        }

        [Fact]
        public void GetGeneralInformation_Success_OkEmptyMeals()
        {
            User user = new User { Id = 1 };

            List<Meal> meals = new List<Meal>()
            {
                new Meal { Id = 1, Date = DateTime.Today.AddDays(-1), Calories = 10, User = user, Food = new Food { Id = 1 } },
                new Meal { Id = 2, Date = DateTime.Today.AddDays(-1), Calories = 11, User = user, Food = new Food { Id = 2 } },
                new Meal { Id = 3, Date = DateTime.Today.AddDays(-1), Calories = 12, User = user, Food = new Food { Id = 3 } },
                new Meal { Id = 4, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } }
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            IActionResult result = _controller.GetGeneralInformation(1, DateTime.Today.AddDays(-10), DateTime.Today.AddDays(-8)).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(2));
            ((GetGeneralInformationResponse)okResult.Value).Meals.Count.Should().Be(0);
            ((GetGeneralInformationResponse)okResult.Value).TotalMeals.Should().Be(0);
        }

        [Fact]
        public void GetGeneralInformation_Success_OkSecondPage()
        {
            User user = new User { Id = 1 };

            List<Meal> meals = new List<Meal>()
            {
                new Meal { Id = 1, Date = DateTime.Today.AddDays(-1), Calories = 10, User = user, Food = new Food { Id = 1 } },
                new Meal { Id = 2, Date = DateTime.Today.AddDays(-1), Calories = 11, User = user, Food = new Food { Id = 2 } },
                new Meal { Id = 3, Date = DateTime.Today.AddDays(-1), Calories = 12, User = user, Food = new Food { Id = 3 } },
                new Meal { Id = 4, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 5, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 6, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 7, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 8, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 9, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 10, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },
                new Meal { Id = 11, Date = DateTime.Today.AddDays(-1), Calories = 13, User = user, Food = new Food { Id = 4 } },

            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            IActionResult result = _controller.GetGeneralInformation(2, DateTime.Today.AddDays(-5), DateTime.Today).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(2));
            ((GetGeneralInformationResponse)okResult.Value).Meals.Count.Should().Be(1);
            ((GetGeneralInformationResponse)okResult.Value).TotalMeals.Should().Be(11);
        }


        [Fact]
        public void DeleteMeal_Success_OkResponse()
        {
            Meal meal = new Meal() { Id = 1 };
            List<Meal> meals = new List<Meal>() { meal };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Remove(It.IsAny<Meal>())).Callback<Meal>((s) => meals.Remove(s));
            _dbContext.Setup(context => context.Meals.FindAsync(It.IsAny<int>())).ReturnsAsync(meal);

            IActionResult result = _controller.DeleteMeal(meal.Id).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            OkResult okResult = (OkResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Object.Meals.Count().Should().Be(0);
        }

        [Fact]
        public void DeleteMeal_Error_NotFound()
        {
            Meal meal = new Meal() { Id = 1 };
            List<Meal> meals = new List<Meal>() { meal };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Meals.FindAsync(It.IsAny<int>())).ReturnsAsync((Meal)null);

            IActionResult result = _controller.DeleteMeal(meal.Id).GetAwaiter().GetResult();

            result.Should().BeOfType<NotFoundObjectResult>();

            NotFoundObjectResult notFoundResult = (NotFoundObjectResult)result;

            notFoundResult.StatusCode.Should().Be(404);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Never);

            _dbContext.Object.Meals.Count().Should().Be(1);
        }

        [Fact]
        public void UpdateMeal_Success_OkResponse()
        {
            Meal meal = new Meal() { Id = 1, Calories = 250 };
            List<Meal> meals = new List<Meal>() { meal };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Meals.FindAsync(It.IsAny<int>())).ReturnsAsync(meal);

            IActionResult result = _controller.UpdateMeal(new Meal
            {
                Id = 1,
                Calories = 500
            }).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            OkResult okResult = (OkResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
            _dbContext.Object.Meals.First().Calories.Should().Be(500);
        }

        [Fact]
        public void UpdateMeal_Error_NotFound()
        {
            Meal meal = new Meal() { Id = 1, Calories = 250 };
            List<Meal> meals = new List<Meal>() { meal };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Meals.FindAsync(It.IsAny<int>())).ReturnsAsync((Meal)null);

            IActionResult result = _controller.UpdateMeal(new Meal
            {
                Id = 1,
                Calories = 500
            }).GetAwaiter().GetResult();

            result.Should().BeOfType<NotFoundObjectResult>();

            NotFoundObjectResult notFoundResult = (NotFoundObjectResult)result;

            notFoundResult.StatusCode.Should().Be(404);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Never);

            _dbContext.Object.Meals.First().Calories.Should().Be(250);
        }

        [Fact]
        public void CreateMeal_Success_OkResponse()
        {
            List<Meal> meals = new List<Meal>();
            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Meals.AddAsync(It.IsAny<Meal>(), default)).Callback<Meal, CancellationToken>((s, c) => meals.Add(s));

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.CreateMeal(new Meal()).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            OkResult okResult = (OkResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Object.Meals.Count().Should().Be(1);
        }
    }
}
