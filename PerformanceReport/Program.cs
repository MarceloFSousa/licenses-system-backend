using Domain.Repositories;
using Domain.Repository;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using PerformanceReport.Repository;
using PerformanceReport.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("http://*:8080", "http://127.0.0.1:8081");

builder.Services.AddControllers();

builder.Services.AddScoped<PerformanceService>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddDbContext<TradeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LicensesDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
