using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Web.Models
{
    public record RegisterUserDto(
    string Username,
    string Password
    );

    public record LoginUserDto(
    string username,
    string password
    );
}
