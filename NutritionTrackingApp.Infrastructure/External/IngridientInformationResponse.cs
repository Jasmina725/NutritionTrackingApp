using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Infrastructure.External
{
    public class IngredientInformationResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; } 

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = "gram";

        [JsonPropertyName("nutrition")]
        public NutritionData Nutrition { get; set; } = new();
    }
    public class NutritionData
    {
        [JsonPropertyName("nutrients")]
        public List<NutrientInfo> Nutrients { get; set; } = new List<NutrientInfo>();

        [JsonIgnore]
        public decimal Calories => GetNutrient("Calories");

        [JsonIgnore]
        public decimal Protein => GetNutrient("Protein");

        [JsonIgnore]
        public decimal Carbs => GetNutrient("Carbohydrates"); 

        [JsonIgnore]
        public decimal Fat => GetNutrient("Fat");

        [JsonIgnore]
        public decimal Sugar => GetNutrient("Sugar");

        private decimal GetNutrient(string name)
        {
            var n = Nutrients.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            return n?.Amount ?? 0m;
        }
    }

    public class NutrientInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;
    }
    public class NutritionInfo
    {
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fat { get; set; }
        public decimal Sugar { get; set; }
    }
}
