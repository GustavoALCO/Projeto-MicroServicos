using AuthUsers.Aplication.Settings;

var builder = WebApplication.CreateBuilder(args);

//Pega os valores declarados do Jwt no appsettings para a configuração dos Jwt no applications 
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
