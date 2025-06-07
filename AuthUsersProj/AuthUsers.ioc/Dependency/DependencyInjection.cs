using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Validator.Employee;
using AuthUsers.domain.Interfaces.Users;
using AuthUsers.infra.DbConfig;
using AuthUsers.infra.Repositories.Employee;
using AuthUsers.infra.Repositories.Users;
using FluentValidation;
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

    public static IServiceCollection AddInterfacesValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateEmployeeCommands>, CreateEmployeeValidation>();


        return services;
    }

    public static IServiceCollection AddInterfaces(this IServiceCollection services)
    {
        //Mapeando as Interfaces dos funcionarios
        services.AddScoped<IEmployeeRepositoryCommands, EmployeeRepositoryCommands>();
        services.AddScoped<IEmployeeRepositoryQuery, EmployeeRepositoryQuery>();

        //Mapeando as Interfaces dos Usuarios
        services.AddScoped<IUserRepositoryCommands, UsersRepositoryCommands>();
        services.AddScoped<IUserRepositoryQuery, UsersRepositoryQuery>();

        return services;
    }
}
