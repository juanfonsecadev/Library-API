using Library.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Library.API.Services;

Env.Load();  // Carrega as variáveis de ambiente do .env

var builder = WebApplication.CreateBuilder(args);

// Obtém a string de conexão diretamente do arquivo .env
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Verifica se a string de conexão foi carregada corretamente
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'CONNECTION_STRING' não foi encontrada.");
}

// Add services to the container.
builder.Services.AddControllers();

// Configuração Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o DbContext para usar o MySQL com a string de conexão carregada da variável de ambiente
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));
builder.Services.AddScoped<IAuthService, AuthService>();


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
app.UseRouting();
app.Run();
