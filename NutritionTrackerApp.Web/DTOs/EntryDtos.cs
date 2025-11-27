using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Web.Models
{
    public record CreateFoodEntryDto(string Name, decimal Amount, string Unit);
    public record FoodEntryDto(int Id, string IngredientName, decimal Amount, string Unit,
        decimal Calories, decimal? Protein, decimal? Fat, decimal? Carbs, decimal? Sugar, DateTimeOffset CreatedAt);
}
