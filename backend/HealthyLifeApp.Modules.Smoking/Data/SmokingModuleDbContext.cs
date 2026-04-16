using HealthyLifeApp.Modules.Smoking.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthyLifeApp.Modules.Smoking.Data;

public class SmokingModuleDbContext : DbContext
{
    public SmokingModuleDbContext(DbContextOptions<SmokingModuleDbContext> options) : base(options) { }

    public DbSet<SmokingHabit> SmokingHabits => Set<SmokingHabit>();
    public DbSet<HealthImprovementMilestone> HealthMilestones => Set<HealthImprovementMilestone>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("smoking");
    }
}
