using Business.Dtos;

namespace Business.Interfaces;

public interface IDrugRecommendationApi
{
    Task<DrugRecommendationDto> GetRecommendedDrugs(
        Guid userId,
        IList<NutrientReportResponseDto> nutrientReports,
        CancellationToken ct = default
    );
}