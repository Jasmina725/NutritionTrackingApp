using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Helpers
{
    public record DailyEntryDto(int Id, string IngredientName, decimal Amount, string Unit,
        decimal Calories, decimal? Protein, decimal? Fat, decimal? Carbs, decimal? Sugar);
}
