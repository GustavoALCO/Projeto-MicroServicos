using AdsService.Aplication.Settings;
using AdsService.Dommain.Interfaces.Image;
using AdsService.Dommain.Interfaces.Images;
using AdsService.Dommain.Interfaces.Product;
using AdsService.Infra.Repository.Commands.Images;
using AdsService.Infra.Repository.Commands.Product;
using AdsService.Infra.Repository.Queries.Images;
using AdsService.Infra.Repository.Queries.Product;
using AuthUsers.infra.DbConfig;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace AdsSevice.IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionString>(configuration.GetSection("ConnectionStrings"));
        services.AddDbContext<ContextDB>((provider, options) =>
        {
            var config = provider.GetRequiredService<IOptions<ConnectionString>>().Value;

            options.UseNpgsql(
                $"Host=postgres-db;Port={config.Port};Database={config.Database};Username={config.Username};Password={config.Password}",
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ContextDB).Assembly.FullName);
                });
        });

        return services;
    }

    public static IServiceCollection AddInterfacesValidators(this IServiceCollection services)
    {

        services.AddScoped<IValidator<AdsService.Aplication.Commands.Product.CreateProductCommands>, AdsService.Aplication.Validations.Products.CreateProductValidator >();

        services.AddScoped<IValidator<AdsService.Aplication.Commands.Image.PathImagesCommands>, AdsService.Aplication.Validations.Image.PathImagesValidator>();

        return services;
    }

    public static IServiceCollection AddInterfaces(this IServiceCollection services)
    {
        services.AddScoped<IImageRepositoryCommands, ImageRepositoryCommands>();
        services.AddScoped<IImageRepositoryQuery, ImageRepositoryQuery>();


        services.AddScoped<IProductRepositoryQuery, ProductRepositoryQuery>();
        services.AddScoped<IProductRepositoryCommands, ProductRepositoryCommands>();

        return services;
    }

    public static IServiceCollection AddInterfacesServices(this IServiceCollection services)
    {
        services.AddScoped<AdsService.Aplication.Interfaces.IConvertImagesByte, AdsService.Aplication.Services.ConvertImagesByte>();
        services.AddScoped<AdsService.Aplication.Interfaces.IValidateBase64, AdsService.Aplication.Services.ValidateBase64>();

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(Assembly.Load("AdsService.Aplication")));

        return services;
    }

    public static IServiceCollection AddFluentValidate(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.Load("AdsService.Aplication"));

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AdsProj",
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

    public static IServiceCollection Authentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var jwtSettings = jwtSection.Get<JWTSettings>();

        services.Configure<JWTSettings>(jwtSection); // Disponibiliza o IOptions<JWTSettings> para injeção

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudiences = jwtSettings.Audience, // <- aceita múltiplas audiências

                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ValidateIssuerSigningKey = true,

                RoleClaimType = "Role" // usa o claim "Role" no token
            };
        });

        return services;
    }
}