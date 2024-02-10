using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductWebApi.Application.Common.Behaviours;
using ProductWebApi.Application.Infrastructure.Persistence;

namespace ProductWebApi.Application;

public static class DependencyConfig
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddAutoMapper(typeof(Application));
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Application).Assembly);
            config.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });
        services.AddValidatorsFromAssemblyContaining(typeof(Application));

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        services.AddDbContext<ApiDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}