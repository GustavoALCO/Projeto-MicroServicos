using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthUsers.ioc.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbConfig>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Database=WEBAPI;Username=postgres;Password=your_password",
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(DbConfig).Assembly.FullName);
                });
        });

        return services;
    }
}
