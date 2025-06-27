using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.Aplication.Commands.Users;
using AuthUsers.Aplication.Interfaces;
using AuthUsers.Aplication.Services;
using AuthUsers.Aplication.Validator;
using AuthUsers.Aplication.Validator.Employee;
using AuthUsers.Aplication.Validator.Users;
using AuthUsers.domain.Interfaces.Users;
using AuthUsers.infra.DbConfig;
using AuthUsers.infra.Repositories.Employee;
using AuthUsers.infra.Repositories.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AuthUsers.ioc.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ContextDB>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Port=5432;Database=authdb;Username=authuser;Password=Teste123",
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ContextDB).Assembly.FullName);
                });
        });

        return services;
    }

    public static IServiceCollection AddInterfacesValidators(this IServiceCollection services)
    {

        services.AddScoped<IValidator<CreateEmployeeCommands>, CreateEmployeeValidation>();
        services.AddScoped<IValidator<ChangePasswordCommands>, ChangePassWordValidation>();

        services.AddScoped<IValidator<ChangeLoginUserCommands>, ChangeLoginUserValidator>();
        services.AddScoped<IValidator<ChangePhoneNumberCommands>,ChangeNumberPhone>();
        services.AddScoped<IValidator<CreateUsersCommands>, CreateUsersValidator>();
        services.AddScoped<IValidator<LoginUserCommands>, LoginUserValidator>();

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

    public static IServiceCollection AddInterfacesServices(this IServiceCollection services)
    {
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<IPasswordHasher, HashService>();
        services.AddScoped<IValidateCPF, ValidateCPF>();

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(Assembly.Load("AuthUsers.Aplication")));

        return services;
    }

    public static IServiceCollection AddFluentValidate(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.Load("AuthUsers.Aplication"));

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AuthProj",
                Version = "v1"
            });

            var securitySchema = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Entre com o JWT Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new string[] {} }
    });
        });
        return services;
    }

}
