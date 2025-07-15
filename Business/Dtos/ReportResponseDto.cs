using System.Text.Json.Serialization;

namespace Business.Dtos;

public record ReportResponseDto
{
    [JsonPropertyName("nutrient_reports")]
    public required List<NutrientReportResponseDto> NutrientReports { get; init; }
}