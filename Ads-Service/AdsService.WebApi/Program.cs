using AdsService.Aplication.Settings;
using AdsSevice.IOC;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Pega os valores declarados do Jwt no appsettings para a configuração dos Jwt no applications 
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

//Pega os valores declarados do ConnectionString no appsettings
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediator();

builder.Services.AddSwagger();

builder.Services.AddInterfacesServices();

builder.Services.AddInterfaces();

builder.Services.AddFluentValidate();

builder.Services.AddInfra(builder.Configuration);

builder.Services.Authentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthProj v1");
});


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
