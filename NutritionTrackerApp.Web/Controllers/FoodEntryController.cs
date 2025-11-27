using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTrackerApp.Web.DTOs.Mappings;
using NutritionTrackingApp.Core.Interfaces;
using NutritionTrackingApp.Web.Models;

namespace NutritionTrackerApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoodEntryController : ControllerBase
    {
        private readonly IEntryService _foodEntryService;
        private readonly IUserService _userService;

        public FoodEntryController(
            IEntryService foodEntryService,
            IUserService userService)
        {
            _foodEntryService = foodEntryService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFoodEntryDto dto)
        {
            var userId = _userService.GetUserIdFromClaims(User);

            var created = await _foodEntryService.CreateFoodEntryAsync(userId, dto.Name, dto.Amount, dto.Unit);

            return Ok(created.ToDto());
        }

        [HttpGet("days")]
        public async Task<IActionResult> GetDaysSummary()
        {
            var userId = _userService.GetUserIdFromClaims(User);

            var result = await _foodEntryService.GetFoodEntriesAsync(userId);

            return Ok(result);
        }

        [HttpGet("day/{date}")]
        public async Task<IActionResult> GetForDay(DateTime date)
        {
            var userId = _userService.GetUserIdFromClaims(User);

            var entries = await _foodEntryService.GetEntriesForDayAsync(userId, date);

            return Ok(entries);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userService.GetUserIdFromClaims(User);

            var success = await _foodEntryService.DeleteFoodEntryAsync(id, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
