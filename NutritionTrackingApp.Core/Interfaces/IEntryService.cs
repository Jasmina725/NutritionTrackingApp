using NutritionTrackingApp.Core.Entities;
using NutritionTrackingApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Interfaces
{
    public interface IEntryService
    {
        Task<FoodEntry> CreateFoodEntryAsync(int userId, string ingredientName, decimal amount, string unit);
        Task<List<DailyCaloriesDto>> GetFoodEntriesAsync(int userId);
        Task<List<DailyEntryDto>> GetEntriesForDayAsync(int userId, DateTime date);
        Task<bool> DeleteFoodEntryAsync(int entryId, int userId);
    }
}
