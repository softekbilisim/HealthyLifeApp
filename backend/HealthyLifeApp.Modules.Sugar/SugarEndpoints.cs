using System.Security.Claims;
using HealthyLifeApp.Modules.Sugar.Data;
using HealthyLifeApp.Modules.Sugar.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Sugar;

public static class SugarEndpoints
{
    public static void MapSugarEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sugar").RequireAuthorization();

        group.MapPost("/start", async (DateTime startDate, SugarModuleDbContext db, ClaimsPrincipal user) =>
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

        group.MapGet("/milestones", async (SugarModuleDbContext db) =>
        {
            var milestones = await db.SugarMilestones.OrderBy(m => m.DaysAfterQuitting).ToListAsync();
            return Results.Ok(milestones);
        });

        group.MapGet("/recipes", async (bool forCravings, SugarModuleDbContext db) =>
        {
            var recipes = await db.Recipes.Where(r => r.IsForSugarCravings == forCravings).ToListAsync();
            return Results.Ok(recipes);
        });
    }
}
