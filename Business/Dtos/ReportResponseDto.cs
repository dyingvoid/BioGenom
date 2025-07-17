using System.Text.Json.Serialization;

namespace Business.Dtos;

public record ReportResponseDto
{
    [JsonPropertyName("nutrient_reports")]
    public required List<NutrientReportResponseDto> NutrientReports { get; init; }

    [JsonPropertyName("recommended_nutrient_intake")]
    public required List<RecommendedNutrientIntakeDto> RecommendedNutrientIntake { get; init; }
}