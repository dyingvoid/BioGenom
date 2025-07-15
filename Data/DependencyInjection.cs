using Business.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IDrugRepository, DrugRepository>();
        services.AddScoped<INutrientRepository, NutrientRepository>();
        return services;
    }
}