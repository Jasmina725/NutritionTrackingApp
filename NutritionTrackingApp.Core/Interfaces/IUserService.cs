using NutritionTrackingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string username, string password);
        Task<string> LoginAsync(string username, string password);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        int GetUserIdFromClaims(ClaimsPrincipal user);
    }
}
