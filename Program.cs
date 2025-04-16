using Library.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Library.API.Services;

Env.Load();  

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'CONNECTION_STRING' não foi encontrada.");
}


builder.Services.AddControllers();  
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();  

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();


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
