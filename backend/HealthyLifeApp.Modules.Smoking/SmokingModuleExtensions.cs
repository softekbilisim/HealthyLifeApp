using HealthyLifeApp.Modules.Smoking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyLifeApp.Modules.Smoking;

public static class SmokingModuleExtensions
{
    public static IServiceCollection AddSmokingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SmokingModuleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
