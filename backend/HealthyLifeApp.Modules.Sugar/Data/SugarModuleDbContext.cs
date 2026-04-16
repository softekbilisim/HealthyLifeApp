using HealthyLifeApp.Modules.Sugar.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Sugar.Data;

public class SugarModuleDbContext : DbContext
{
    public SugarModuleDbContext(DbContextOptions<SugarModuleDbContext> options) : base(options) { }

    public DbSet<SugarQuittingProgress> SugarProgress => Set<SugarQuittingProgress>();
    public DbSet<SugarHealthMilestone> SugarMilestones => Set<SugarHealthMilestone>();
    public DbSet<HealthyRecipe> Recipes => Set<HealthyRecipe>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("sugar");
    }
}
