using Microsoft.Extensions.Configuration;
using NutritionTrackingApp.Core.Helpers;
using NutritionTrackingApp.Infrastructure.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Infrastructure.Services
{
    public interface ISpoonacularService
    {
        Task<(int IngredientId, string Name)> SearchIngredientAsync(string name);
        Task<NutritionInfo> GetNutritionAsync(int ingredientId, decimal amount, string unit);
    }

    public class SpoonacularService: ISpoonacularService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public SpoonacularService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Spoonacular:ApiKey"]!;
        }

        public async Task<(int IngredientId, string Name)> SearchIngredientAsync(string name)
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["query"] = name,
                ["number"] = "1"
            };

            var request = HttpRequestHelper.CreateGetRequestWithApiKey(
                "/food/ingredients/search",
                queryParams,
                _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var results = doc.RootElement.GetProperty("results");

            var list = results.EnumerateArray()
                .Select(item => new KeyValuePair<int, string>(
                    item.GetProperty("id").GetInt32(),
                    item.GetProperty("name").GetString() ?? ""
                ))
                .ToList();

            if (!list.Any())
                throw new Exception($"Ingredient '{name}' not found.");

            var first = list.First();

            return (first.Key, first.Value);
        }

        public async Task<NutritionInfo> GetNutritionAsync(int ingredientId, decimal amount, string unit = "gram")
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["amount"] = amount.ToString(),
                ["unit"] = unit
            };

            var request = HttpRequestHelper.CreateGetRequestWithApiKey(
                $"/food/ingredients/{ingredientId}/information",
                queryParams,
                _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<IngredientInformationResponse>();

            if (data == null)
                throw new Exception("Error getting nutrition data.");

            return new NutritionInfo()
            {
                Calories = data.Nutrition.Calories,
                Protein = data.Nutrition.Protein,
                Carbs = data.Nutrition.Carbs,
                Fat = data.Nutrition.Fat,
                Sugar = data.Nutrition.Sugar
            };
        }
    }
}
