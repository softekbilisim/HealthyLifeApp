using HealthyLifeApp.Modules.Nutrition.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyLifeApp.Modules.Nutrition;

public static class NutritionModuleExtensions
{
    public static IServiceCollection AddNutritionModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NutritionModuleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
