using Microsoft.AspNetCore.Mvc;
using NutritionTrackerApp.Web.DTOs.Mappings;
using NutritionTrackingApp.Core.Interfaces;
using NutritionTrackingApp.Web.Models;

namespace NutritionTrackerApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterUserAsync(dto.Username, dto.Password);
            return Ok(user.ToDto());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await _userService.LoginAsync(dto.username, dto.password);
            return Ok(new { Token = token });
        }
    }
}
