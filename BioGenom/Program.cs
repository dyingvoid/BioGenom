using Business;
using Data;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BioGenom;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.RegisterRepositories();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "BioGenomReport:";
        });
        builder.Services.RegisterApplicationServices();
        
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapOpenApi();
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}