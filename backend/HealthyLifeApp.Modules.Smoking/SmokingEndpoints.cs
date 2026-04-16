using System.Security.Claims;
using HealthyLifeApp.Modules.Smoking.Data;
using HealthyLifeApp.Modules.Smoking.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Smoking;

public static class SmokingEndpoints
{
    public static void MapSmokingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/smoking").RequireAuthorization();

        group.MapPost("/habit", async (SmokingHabitRequest request, SmokingModuleDbContext db, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var habit = await db.SmokingHabits.FirstOrDefaultAsync(h => h.UserId == userId);

            if (habit == null)
            {
                habit = new SmokingHabit { UserId = userId };
                db.SmokingHabits.Add(habit);
            }

            habit.DailyCigaretteCount = request.DailyCigaretteCount;
            habit.MinutesPerCigarette = request.MinutesPerCigarette;
            habit.PackPrice = request.PackPrice;
            habit.CigarettesInPack = request.CigarettesInPack;
            habit.QuitDate = request.QuitDate;
            habit.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(habit);
        });

        group.MapGet("/stats", async (SmokingModuleDbContext db, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var habit = await db.SmokingHabits.FirstOrDefaultAsync(h => h.UserId == userId);

            if (habit == null) return Results.NotFound("Habit not found");

            var timePassed = DateTime.UtcNow - habit.QuitDate;
            var totalMinutes = timePassed.TotalMinutes;

            if (totalMinutes < 0) totalMinutes = 0;

            var cigarettesNotSmoked = (habit.DailyCigaretteCount / 24.0 / 60.0) * totalMinutes;
            var moneySaved = (cigarettesNotSmoked / habit.CigarettesInPack) * (double)habit.PackPrice;
            var timeRegained = cigarettesNotSmoked * habit.MinutesPerCigarette;

            return Results.Ok(new
            {
                CigarettesNotSmoked = Math.Round(cigarettesNotSmoked, 2),
                MoneySaved = Math.Round(moneySaved, 2),
                TimeRegainedMinutes = Math.Round(timeRegained, 2),
                DaysPassed = timePassed.Days,
                HoursPassed = timePassed.Hours
            });
        });

        group.MapGet("/milestones", async (SmokingModuleDbContext db) =>
        {
            var milestones = await db.HealthMilestones.OrderBy(m => m.MinutesAfterQuitting).ToListAsync();
            return Results.Ok(milestones);
        });
    }
}

public record SmokingHabitRequest(int DailyCigaretteCount, int MinutesPerCigarette, decimal PackPrice, int CigarettesInPack, DateTime QuitDate);
