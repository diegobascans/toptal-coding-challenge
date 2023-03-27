using Backend.Auth;
using Backend.Data;
using Backend.Models;
using Backend.Models.Responses.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private ApiDbContext _dbContext;
        private IJwtAuthenticationService _authService;

        public GeneralController(ApiDbContext dbContext, IJwtAuthenticationService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        /// <summary>
        /// Retrieve all the foods
        /// </summary>
        /// <returns>List<Food></returns>
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetFoods()
        {
            return Ok(await _dbContext.Foods.ToListAsync());
        }

        /// <summary>
        /// Get the JWT Token for a specific user (This method is for testing purposes)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> Authenticate(int userId)
        {
            User user;

            user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            string token = null;

            if (user != null)
            {
                token = _authService.Authenticate(user);
            } 

            return Ok(new AuthenticationResponse { Token = token, User = user });
        }
    }
}
