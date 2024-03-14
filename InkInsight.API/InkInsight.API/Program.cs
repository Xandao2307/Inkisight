using InkInsight.API.Configurations;
using InkInsight.API.Mappers;
using InkInsight.API.Persistences;
using InkInsight.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("InkInsightCs");
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File($"logs/inkinsight.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

Log.Information("Initiate App");

builder.Services.AddControllers();
builder.Services.AddDbContext<InkInsightDbContext>(o => o.UseMySQL(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ReviewProfile).Assembly);
builder.Services.AddSingleton<JwtConfiguration>();
builder.Services.AddTransient<TokenService>();
builder.Host.UseSerilog();

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


static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
           .UseSerilog() // Integração com o Serilog
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<StartupBase>();
           });
}
