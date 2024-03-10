using InkInsight.API.Mappers;
using InkInsight.API.Persistences;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("InkInsightCs");

builder.Services.AddControllers();
//builder.Services.AddDbContext<InkInsightDbContext>(o => o.UseInMemoryDatabase("InkInsightDB"));
builder.Services.AddDbContext<InkInsightDbContext>(o => o.UseMySQL(connectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ReviewProfile).Assembly);

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
