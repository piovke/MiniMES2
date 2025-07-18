using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("MiniProduction");
        services.AddDbContext<MiniProductionDbContext>(options =>
            options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}