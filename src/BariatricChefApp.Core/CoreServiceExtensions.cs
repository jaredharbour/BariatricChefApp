using BariatricChefApp.Core.Data;
using BariatricChefApp.Core.Data.Repositories;
using BariatricChefApp.Core.Interfaces;
using BariatricChefApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BariatricChefApp.Core;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddBariatricChefCore(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BariatricChefDbContext>(options =>
        {
            var endpoint = configuration["CosmosDb:Endpoint"]
                ?? throw new InvalidOperationException("CosmosDb:Endpoint is required.");
            var key = configuration["CosmosDb:Key"]
                ?? throw new InvalidOperationException("CosmosDb:Key is required.");
            var database = configuration["CosmosDb:DatabaseName"] ?? "BariatricChef";

            options.UseCosmos(endpoint, key, database);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<RecipeService>();
        services.AddScoped<NutritionService>();
        services.AddScoped<UserProfileService>();

        return services;
    }
}
