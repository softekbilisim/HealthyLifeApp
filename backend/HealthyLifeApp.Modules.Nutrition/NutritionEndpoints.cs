using System.Security.Claims;
using HealthyLifeApp.Modules.Nutrition.Data;
using HealthyLifeApp.Modules.Nutrition.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Nutrition;

public static class NutritionEndpoints
{
    public static void MapNutritionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/nutrition").RequireAuthorization();

        group.MapPost("/profile", async (NutritionProfileRequest request, NutritionModuleDbContext db, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var profile = await db.NutritionProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                profile = new NutritionProfile { UserId = userId };
                db.NutritionProfiles.Add(profile);
            }

            profile.BloodType = request.BloodType;
            profile.Weight = request.Weight;
            profile.Age = request.Age;
            profile.Gender = request.Gender;

            // BMR Calculation (Mifflin-St Jeor)
            double bmr;
            if (request.Gender.ToLower() == "male")
                bmr = (10 * request.Weight) + (6.25 * request.Height) - (5 * request.Age) + 5;
            else
                bmr = (10 * request.Weight) + (6.25 * request.Height) - (5 * request.Age) - 161;

            profile.DailyCalorieNeed = bmr * 1.2; // Sedentary factor
            profile.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(profile);
        });

        group.MapGet("/suggestions", async (string bloodType, NutritionModuleDbContext db) =>
        {
            var suggestions = await db.FoodSuggestions
                .Where(s => s.BloodType == bloodType)
                .ToListAsync();
            return Results.Ok(suggestions);
        });
    }
}

public record NutritionProfileRequest(string BloodType, double Weight, double Height, int Age, string Gender);
