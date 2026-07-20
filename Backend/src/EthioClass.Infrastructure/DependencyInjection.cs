using EthioClass.Application.Common.Interfaces;
using EthioClass.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EthioClass.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<EthioClassDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("EthioClassDatabase")));

        service.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EthioClassDbContext>());

        return service;
    }
}