using NutritionTrackingApp.Core.Entities;
using NutritionTrackingApp.Web.Models;

namespace NutritionTrackerApp.Web.DTOs.Mappings
{
    public static class FoodEntryMappings
    {
        public static FoodEntryDto ToDto(this FoodEntry e)
        {
            return new FoodEntryDto(
                e.Id,
                e.Ingredient?.Name ?? "",
                e.Amount,
                e.Unit,
                e.Calories,
                e.Protein,
                e.Carbs,
                e.Fat,
                e.Sugar,
                e.CreatedAt
            );
        }
    }
}
