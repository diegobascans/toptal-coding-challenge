using Backend.Auth;
using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Backend.Models.Responses.General;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace BackendTest.Controllers
{
    public class GeneralControllerTest
    {
        private readonly Mock<ApiDbContext> _dbContext;
        private readonly Mock<IJwtAuthenticationService> _authService;
        private readonly GeneralController _controller;

        public GeneralControllerTest()
        {
            _dbContext = new Mock<ApiDbContext>();
            _authService = new Mock<IJwtAuthenticationService>();
            _controller = new GeneralController(_dbContext.Object, _authService.Object);
        }

        [Fact]
        public void Authenticate_Success_GetUserAndToken()
        {
            IList<User> users = new List<User> { new User { Id = 0 } };
            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            _authService.Setup(auth => auth.Authenticate(It.IsAny<User>())).Returns("dummyToken");

            IActionResult result = _controller.Authenticate(0).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okObjectResult = (OkObjectResult)result;

            okObjectResult.StatusCode.Should().Be(200);

            ((AuthenticationResponse)okObjectResult.Value).Token.Should().NotBeNull();
            ((AuthenticationResponse)okObjectResult.Value).User.Should().NotBeNull();

            _authService.Verify(x => x.Authenticate(It.IsAny<User>()), Times.Once);
            _dbContext.Verify(x => x.Users, Times.Once);
        }

        [Fact]
        public void Authenticate_Error_UserNotFound()
        {
            IList<User> users = new List<User> { };
            _dbContext.Setup(context => context.Users).ReturnsDbSet(users);

            _authService.Setup(auth => auth.Authenticate(It.IsAny<User>())).Returns("dummyToken");

            IActionResult result = _controller.Authenticate(0).GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okObjectResult = (OkObjectResult)result;

            ((AuthenticationResponse)okObjectResult.Value).Token.Should().BeNull();
            ((AuthenticationResponse)okObjectResult.Value).User.Should().BeNull();

            _authService.Verify(x => x.Authenticate(It.IsAny<User>()), Times.Never);
            _dbContext.Verify(x => x.Users, Times.Once);
        }

        [Fact]
        public void GetFoods_Success_GetFoodsList()
        {
            IList<Food> foods = new List<Food> { new Food { Id = 1 } };
            _dbContext.Setup(context => context.Foods).ReturnsDbSet(foods);

            IActionResult result = _controller.GetFoods().GetAwaiter().GetResult();

            result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okObjectResult = (OkObjectResult)result;

            ((List<Food>)okObjectResult.Value).Should().NotBeNull();
            ((List<Food>)okObjectResult.Value).Count.Should().Be(1);

            _dbContext.Verify(x => x.Foods, Times.Once);
        }
    }
}
