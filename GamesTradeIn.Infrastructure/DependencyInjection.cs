using GamesTradeIn.Domain.Intefaces;
using GamesTradeIn.Infrastructure.Data.Context;
using GamesTradeIn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamesTradeIn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<GamesTradeInDbContext>(options => options.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
        }));

        services.AddScoped<IUserRepository, UserRepository>();
    
        return services;
    }
}