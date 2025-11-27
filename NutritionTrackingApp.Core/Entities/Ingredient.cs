using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Entities
{
    public class Ingredient
    {
        public int Id { get; set; } 
        public int SpoonacularId { get; set; }  
        public required string Name { get; set; }  
        public List<FoodEntry> FoodEntries { get; set; } = new();
    }
}
