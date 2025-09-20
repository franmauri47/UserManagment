using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql");

        //Services
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //Db Context
        services.AddDbContext<MySqlDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        return services;
    }
}
