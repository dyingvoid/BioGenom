using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Apis;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApis(this IServiceCollection services)
    {
        return services.AddSingleton<IDrugRecommendationApi, DrugRecommendationApi>();
    }
}