using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.Classes;
using Backend.Models.Responses.Users;
using BackendTest.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace BackendTest.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<ApiDbContext> _dbContext;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _dbContext = new Mock<ApiDbContext>();
            _controller = new UserController(_dbContext.Object);
        }


        [Fact]
        public void AddMeal_Success_OkResponse()
        {
            List<Meal> meals = new List<Meal>();
            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);
            _dbContext.Setup(context => context.Meals.AddAsync(It.IsAny<Meal>(), default)).Callback<Meal, CancellationToken>((s, c) => meals.Add(s));

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.AddMeal(new Meal()).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            OkResult okResult = (OkResult)result;

            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Once);
            _dbContext.Object.Meals.Count().Should().Be(1);
        }

        [Fact]
        public void GetCheatedMeals_Success_ReturnsCheatedMealsWithCheatedFood()
        {
            IList<Food> foods = new List<Food>() { new Food { Id = 1 } };
            IList<UserCheatingFood> userCheatingFoods = new List<UserCheatingFood>() { new UserCheatingFood { FoodId = 1, UserId = 0 } };

            _dbContext.Setup(context => context.Foods).ReturnsDbSet(foods);
            _dbContext.Setup(context => context.UserCheatingFoods).ReturnsDbSet(userCheatingFoods);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetCheatedFoods().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<List<CheatedFood>>();
            ((List<CheatedFood>)okResult.Value)[0].Cheated.Should().Be(true);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Foods, Times.Once);
            _dbContext.Verify(x => x.UserCheatingFoods, Times.Once);
        }

        [Fact]
        public void GetCheatedMeals_Success_ReturnsCheatedMealsWithNoCheatedFood()
        {
            IList<Food> foods = new List<Food>() { new Food { Id = 1 } };
            IList<UserCheatingFood> userCheatingFoods = new List<UserCheatingFood>() { new UserCheatingFood { FoodId = 1, UserId = 1 } };

            _dbContext.Setup(context => context.Foods).ReturnsDbSet(foods);
            _dbContext.Setup(context => context.UserCheatingFoods).ReturnsDbSet(userCheatingFoods);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetCheatedFoods().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<List<CheatedFood>>();
            ((List<CheatedFood>)okResult.Value)[0].Cheated.Should().Be(false);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Foods, Times.Once);
            _dbContext.Verify(x => x.UserCheatingFoods, Times.Once);
        }

        [Fact]
        public void GetUserMeals_Success_ReturnsUserMeals()
        {
            User user = new User { Id = 0, UserCheatingFoods = new List<UserCheatingFood>() };

            IList<Meal> meals = new List<Meal>() { 
                new Meal { Id = 1, Calories = 10, FoodId = 1, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
                new Meal { Id = 2, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetUserMeals(1, DateTime.Today.AddDays(-3), DateTime.Today).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<GetUserMealsResponse>();
            ((GetUserMealsResponse)okResult.Value).Count.Should().Be(1);
            ((GetUserMealsResponse)okResult.Value).CaloriesPerDay[0].Calories.Should().Be(20);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(3));
        }

        [Fact]
        public void GetUserMeals_Success_ReturnsUserMealsEmpty()
        {
            User user = new User { Id = 0, UserCheatingFoods = new List<UserCheatingFood>() };

            IList<Meal> meals = new List<Meal>() {
                new Meal { Id = 1, Calories = 10, FoodId = 1, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
                new Meal { Id = 2, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetUserMeals(1, DateTime.Today.AddDays(-7), DateTime.Today.AddDays(-4)).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<GetUserMealsResponse>();
            ((GetUserMealsResponse)okResult.Value).CaloriesPerDay.Count().Should().Be(0);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(3));
        }


        [Fact]
        public void GetUserMeals_Success_ReturnsUserMealsSecondPage()
        {
            User user = new User { Id = 0, UserCheatingFoods = new List<UserCheatingFood>() };

            IList<Meal> meals = new List<Meal>() {
                new Meal { Id = 1, Calories = 10, FoodId = 1, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
                new Meal { Id = 2, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-2), User = user },
                new Meal { Id = 3, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-3), User = user },
                new Meal { Id = 4, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-4), User = user },
                new Meal { Id = 5, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-5), User = user },
                new Meal { Id = 6, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-6), User = user },
                new Meal { Id = 7, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-7), User = user },
                new Meal { Id = 8, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-8), User = user },
                new Meal { Id = 9, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-9), User = user },
                new Meal { Id = 10, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-10), User = user },
                new Meal { Id = 11, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-11), User = user },
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetUserMeals(2, DateTime.Today.AddDays(-12), DateTime.Today).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<GetUserMealsResponse>();
            ((GetUserMealsResponse)okResult.Value).Count.Should().Be(11);
            ((GetUserMealsResponse)okResult.Value).CaloriesPerDay.Count().Should().Be(1);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(3));
        }


        [Fact]
        public void GetUserMeals_Success_ReturnsUserMealsWithCheatedMeals()
        {
            User user = new User { Id = 0, UserCheatingFoods = new List<UserCheatingFood>() { new UserCheatingFood { FoodId = 1, UserId = 0 } } };

            IList<Meal> meals = new List<Meal>() {
                new Meal { Id = 1, Calories = 10, FoodId = 1, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
                new Meal { Id = 2, Calories = 10, FoodId = 2, UserId = 0, Date = DateTime.Today.AddDays(-1), User = user },
            };

            _dbContext.Setup(context => context.Meals).ReturnsDbSet(meals);

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.GetUserMeals(0, DateTime.Today.AddDays(-3), DateTime.Today).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = (OkObjectResult)result;

            okResult.Value.Should().BeOfType<GetUserMealsResponse>();
            ((GetUserMealsResponse)okResult.Value).Count = 1;
            ((GetUserMealsResponse)okResult.Value).CaloriesPerDay[0].Calories.Should().Be(10);
            okResult.StatusCode.Should().Be(200);

            _dbContext.Verify(x => x.Meals, Times.Exactly(3));
        }


        [Fact]
        public void UpdateCheatedMeals_Success_OkResponseNewValues()
        {
            List<UserCheatingFood> cheatingFoods = new List<UserCheatingFood>
            {
                new UserCheatingFood { FoodId = 1, UserId = 0}
            };

            _dbContext.Setup(context => context.UserCheatingFoods).ReturnsDbSet(cheatingFoods);

            _dbContext.Setup(context => context.UserCheatingFoods.RemoveRange(It.IsAny<IEnumerable<UserCheatingFood>>()))
                .Callback<IEnumerable<UserCheatingFood>>((s) => {
                    cheatingFoods = cheatingFoods.Where(cf => !s.Any(s => s.FoodId == cf.FoodId)).ToList();
                });
            
            _dbContext.Setup(context => context.UserCheatingFoods.AddRangeAsync(It.IsAny<IEnumerable<UserCheatingFood>>(), It.IsAny<CancellationToken>()))
                .Callback<IEnumerable<UserCheatingFood>, CancellationToken>((s, c) => cheatingFoods.AddRange(s));

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.UpdateCheatedFoods(new List<Food> { new Food { Id = 1 }, new Food { Id = 2 } }).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            _dbContext.Verify(x => x.UserCheatingFoods.RemoveRange(It.IsAny<IQueryable<UserCheatingFood>>()), Times.Once);
            _dbContext.Verify(x => x.UserCheatingFoods.AddRangeAsync(It.IsAny<IEnumerable<UserCheatingFood>>(), default), Times.Once);

            cheatingFoods.Count.Should().Be(2);
        }

        [Fact]
        public void UpdateCheatedMeals_Success_OkResponseRemoveAll()
        {
            List<UserCheatingFood> cheatingFoods = new List<UserCheatingFood>()
            {
                new UserCheatingFood { FoodId = 1, UserId = 0}
            };

            _dbContext.Setup(context => context.UserCheatingFoods).ReturnsDbSet(cheatingFoods);

            _dbContext.Setup(context => context.UserCheatingFoods.RemoveRange(It.IsAny<IEnumerable<UserCheatingFood>>()))
                 .Callback<IEnumerable<UserCheatingFood>>((s) => {
                     cheatingFoods = cheatingFoods.Where(cf => !s.Any(s => s.FoodId == cf.FoodId)).ToList();
                 });

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.UpdateCheatedFoods(new List<Food>()).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            _dbContext.Verify(x => x.UserCheatingFoods.RemoveRange(It.IsAny<IQueryable<UserCheatingFood>>()), Times.Once);
            _dbContext.Verify(x => x.UserCheatingFoods.AddRangeAsync(It.IsAny<IEnumerable<UserCheatingFood>>(), default), Times.Never);

            cheatingFoods.Count.Should().Be(0);
        }

        [Fact]
        public void UpdateCheatedMeals_Success_OkResponseAddAll()
        {
            List<UserCheatingFood> cheatingFoods = new List<UserCheatingFood>();

            _dbContext.Setup(context => context.UserCheatingFoods).ReturnsDbSet(cheatingFoods);

            _dbContext.Setup(context => context.UserCheatingFoods.AddRangeAsync(It.IsAny<IEnumerable<UserCheatingFood>>(), It.IsAny<CancellationToken>()))
                .Callback<IEnumerable<UserCheatingFood>, CancellationToken>((s, c) => cheatingFoods.AddRange(s));

            _dbContext.Setup(context => context.UserCheatingFoods.RemoveRange(It.IsAny<IEnumerable<UserCheatingFood>>()))
                 .Callback<IEnumerable<UserCheatingFood>>((s) => {
                     cheatingFoods = cheatingFoods.Where(cf => !s.Any(s => s.FoodId == cf.FoodId)).ToList();
                 });

            _controller.ControllerContext = ControllerContextHelper.GetMockControllerContext();

            IActionResult result = _controller.UpdateCheatedFoods(new List<Food>() { new Food { Id = 1 } }).GetAwaiter().GetResult();

            result.Should().BeOfType<OkResult>();

            _dbContext.Verify(x => x.UserCheatingFoods.RemoveRange(It.IsAny<IQueryable<UserCheatingFood>>()), Times.Once);
            _dbContext.Verify(x => x.UserCheatingFoods.AddRangeAsync(It.IsAny<IEnumerable<UserCheatingFood>>(), default), Times.Once);

            cheatingFoods.Count.Should().Be(1);
        }
    }
}
