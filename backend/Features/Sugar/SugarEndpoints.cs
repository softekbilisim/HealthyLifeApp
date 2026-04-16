using System.Security.Claims;
using HealthyLifeApp.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.API.Features.Sugar;

public static class SugarEndpoints
{
    public static void MapSugarEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sugar").RequireAuthorization();

        group.MapPost("/start", async (DateTime startDate, AppDbContext db, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var progress = await db.SugarProgress.FirstOrDefaultAsync(p => p.UserId == userId);

            if (progress == null)
            {
                progress = new SugarQuittingProgress { UserId = userId };
                db.SugarProgress.Add(progress);
            }

            progress.StartDate = startDate;
            progress.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(progress);
        });

        group.MapGet("/milestones", async (AppDbContext db) =>
        {
            var milestones = await db.SugarMilestones.OrderBy(m => m.DaysAfterQuitting).ToListAsync();
            return Results.Ok(milestones);
        });

        group.MapGet("/recipes", async (bool forCravings, AppDbContext db) =>
        {
            var recipes = await db.Recipes.Where(r => r.IsForSugarCravings == forCravings).ToListAsync();
            return Results.Ok(recipes);
        });
    }
}
