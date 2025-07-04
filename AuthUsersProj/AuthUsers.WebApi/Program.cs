using AuthUsers.Aplication.Settings;
using AuthUsers.ioc.Dependency;

var builder = WebApplication.CreateBuilder(args);

//Pega os valores declarados do Jwt no appsettings para a configuração dos Jwt no applications 
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthProj v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
