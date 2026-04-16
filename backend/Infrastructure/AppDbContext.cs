using HealthyLifeApp.API.Features.Identity;
using HealthyLifeApp.API.Features.Smoking;
using HealthyLifeApp.API.Features.Nutrition;
using HealthyLifeApp.API.Features.Sugar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.API.Infrastructure;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SmokingHabit> SmokingHabits => Set<SmokingHabit>();
    public DbSet<HealthImprovementMilestone> HealthMilestones => Set<HealthImprovementMilestone>();
    public DbSet<NutritionProfile> NutritionProfiles => Set<NutritionProfile>();
    public DbSet<FoodSuggestion> FoodSuggestions => Set<FoodSuggestion>();
    public DbSet<SugarQuittingProgress> SugarProgress => Set<SugarQuittingProgress>();
    public DbSet<SugarHealthMilestone> SugarMilestones => Set<SugarHealthMilestone>();
    public DbSet<HealthyRecipe> Recipes => Set<HealthyRecipe>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Identity tables to identity schema
        builder.HasDefaultSchema("dbo");

        // You can also organize schemas by feature if preferred,
        // but user requested single DB context.
    }
}
