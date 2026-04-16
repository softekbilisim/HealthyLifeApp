using HealthyLifeApp.Modules.Nutrition.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Nutrition.Data;

public class NutritionModuleDbContext : DbContext
{
    public NutritionModuleDbContext(DbContextOptions<NutritionModuleDbContext> options) : base(options) { }

    public DbSet<NutritionProfile> NutritionProfiles => Set<NutritionProfile>();
    public DbSet<FoodSuggestion> FoodSuggestions => Set<FoodSuggestion>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("nutrition");
    }
}
