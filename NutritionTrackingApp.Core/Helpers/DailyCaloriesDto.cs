using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Helpers
{
    public class DailyCaloriesDto
    {
        public DateTime Date { get; set; }
        public decimal TotalCalories { get; set; }
    }
}
