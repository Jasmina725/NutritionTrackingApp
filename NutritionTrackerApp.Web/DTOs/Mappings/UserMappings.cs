using NutritionTrackingApp.Core.Entities;
using NutritionTrackingApp.Web.Models;

namespace NutritionTrackerApp.Web.DTOs.Mappings
{
    public static class UserMappings
    {
        public static RegisterUserDto ToDto(this User e)
        {
            return new RegisterUserDto(
                e.Username,
                e.PasswordHash
            );
        }
    }
}
