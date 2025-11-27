using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Entities
{
    public class FoodEntry
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Amount { get; set; }    
        public string Unit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal Calories { get; set; }  
        public decimal Protein { get; set; }   
        public decimal Carbs { get; set; }  
        public decimal Fat { get; set; }      
        public decimal Sugar { get; set; }     
    }
}
