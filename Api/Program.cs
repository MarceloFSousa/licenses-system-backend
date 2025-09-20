using Domain.Repositories;
using Domain.Services;
using Domain.Repositories;
using Domain.Services;
using Api.Service;
using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Domain.Repository;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<ExpertsService>();
builder.Services.AddScoped<LicenseService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<ImageService>();



builder.Services.AddScoped<LocalBucketService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var basePath = config["BucketSettings:BasePath"] 
                   ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");

    return new LocalBucketService(basePath);
});
builder.Services.AddDbContext<LicensesContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LicensesDatabase")));
// Build app
var app = builder.Build();
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadsPath))
    Directory.CreateDirectory(uploadsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
