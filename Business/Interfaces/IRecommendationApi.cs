using Business.Dtos;

namespace Business.Interfaces;

public interface IRecommendationApi
{
    Task<DrugRecommendationDto> GetRecommendedDrugs(
        Guid userId,
        ReportResponseDto reportResponseDto,
        CancellationToken ct = default
    );
}