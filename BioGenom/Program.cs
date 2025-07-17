using BioGenom.Middlewares;
using Business;
using Infrastructure.Apis;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace BioGenom;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration)
        );

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.RegisterRepositories();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "BioGenomReport:";
        });
        builder.Services.RegisterApis();
        builder.Services.RegisterApplicationServices();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("BioGenomReport"))
            .WithTracing(tracing =>
            {
                tracing
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation();

                tracing.AddOtlpExporter();
            });

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.MapOpenApi();
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}