using EthioClass.Application.Common.Interfaces;
using EthioClass.Infrastructure.Data;
using EthioClass.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EthioClass.Infrastructure.Services;
namespace EthioClass.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<EthioClassDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("EthioClassDatabase")));

        service.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EthioClassDbContext>());
        
        service.AddScoped<IPasswordHasher, BcryptPasswordHasher > ();

        return service;

    }
    
}