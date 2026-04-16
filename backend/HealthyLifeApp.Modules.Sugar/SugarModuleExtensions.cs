using HealthyLifeApp.Modules.Sugar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyLifeApp.Modules.Sugar;

public static class SugarModuleExtensions
{
    public static IServiceCollection AddSugarModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SugarModuleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
