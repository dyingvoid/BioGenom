using Business.Dtos;
using Business.Interfaces;

namespace Infrastructure.Apis;

// Mock
public class DrugRecommendationApi : IDrugRecommendationApi
{
    public Task<DrugRecommendationDto> GetRecommendedDrugs(
        Guid userId, IList<NutrientReportResponseDto> nutrientReports, CancellationToken ct = default)
    {
        var result = new List<DrugDto>();
        var rnd = new Random();

        foreach (var report in nutrientReports)
        {
            var deficit = report.MinIntake - report.CurrentIntake;
            var maxToSupple = report.MaxIntake != null
                ? Math.Min(deficit, (float)report.MaxIntake - report.CurrentIntake)
                : deficit;
            if (maxToSupple <= 0)
                continue;

            var minRandom = 0.5f * deficit;
            var amount = (float)(rnd.NextDouble() * (maxToSupple - minRandom) + minRandom);
            var nutrient = new DrugNutrientDto
            {
                NutrientId = report.NutrientId,
                Amount = amount
            };
            result.Add(
                new DrugDto
                {
                    SelfLink = "https://drugs.ru/drug-id",
                    ImageUrl = "https://s3.drugs.ru/image",
                    Nutrients = [nutrient]
                }
            );
        }

        return Task.FromResult(
            new DrugRecommendationDto
            {
                Drugs = result
            }
        );
    }
}