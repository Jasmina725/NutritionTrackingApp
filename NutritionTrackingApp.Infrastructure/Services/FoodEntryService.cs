using Microsoft.EntityFrameworkCore;
using NutritionTrackingApp.Core.Entities;
using NutritionTrackingApp.Core.Helpers;
using NutritionTrackingApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Infrastructure.Services
{
    public class FoodEntryService: IEntryService
    {
        private readonly AppDbContext _db;
        private readonly ISpoonacularService _spoonacular;

        public FoodEntryService(AppDbContext db, ISpoonacularService spoonacular)
        {
            _db = db;
            _spoonacular = spoonacular;
        }

        public async Task<FoodEntry> CreateFoodEntryAsync(int userId, string ingredientName, decimal amount, string unit)
        {
            var (spoonId, name) = await _spoonacular.SearchIngredientAsync(ingredientName);

            var nutritionInfo = await _spoonacular.GetNutritionAsync(spoonId, amount, unit);

            var ingredient = await _db.Ingredients
                .FirstOrDefaultAsync(i => i.SpoonacularId == spoonId);

            if (ingredient == null)
            {
                ingredient = new Ingredient
                {
                    SpoonacularId = spoonId,
                    Name = name
                };
                _db.Ingredients.Add(ingredient);
                await _db.SaveChangesAsync();
            }

            var entry = new FoodEntry
            {
                UserId = userId,
                IngredientId = ingredient.Id,
                Amount = amount,
                Unit = unit,
                CreatedAt = DateTime.UtcNow,
                Calories = nutritionInfo.Calories,
                Protein = nutritionInfo.Protein,
                Carbs = nutritionInfo.Carbs,
                Fat = nutritionInfo.Fat,
                Sugar = nutritionInfo.Sugar
            };

            _db.FoodEntries.Add(entry);
            await _db.SaveChangesAsync();

            return entry;
        }

        public async Task<bool> DeleteFoodEntryAsync(int entryId, int userId)
        {
            var entry = await _db.FoodEntries
                .FirstOrDefaultAsync(f => f.Id == entryId && f.UserId == userId);

            if (entry == null)
                return false;
          
            _db.FoodEntries.Remove(entry);
            await _db.SaveChangesAsync();
            return true;          
        }

        public async Task<List<DailyCaloriesDto>> GetFoodEntriesAsync(int userId)
        {
            return await _db.FoodEntries
                .Where(e => e.UserId == userId)
                .GroupBy(e => e.CreatedAt.Date)
                .Select(g => new DailyCaloriesDto
                {
                    Date = g.Key,
                    TotalCalories = g.Sum(x => x.Calories)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<DailyEntryDto>> GetEntriesForDayAsync(int userId, DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _db.FoodEntries
                .Where(e => e.UserId == userId && e.CreatedAt >= start && e.CreatedAt < end)
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new DailyEntryDto(e.Id, e.Ingredient.Name, e.Amount, e.Unit, e.Calories, e.Protein, e.Fat, e.Carbs, e.Sugar))
                .ToListAsync();
        }
    }
}
