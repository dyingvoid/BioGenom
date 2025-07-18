using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<CacheService>();
        services.AddScoped<ReportService>();
        return services;
    }
}