using System.Text.Json.Serialization;

namespace Business.Dtos;

public record ReportRequestDto
{
    [JsonPropertyName("report_id")]
    public required Guid ReportId { get; init; }
    
    [JsonPropertyName("user_id")]
    public required Guid UserId { get; init; }

    [JsonPropertyName("nutrient_reports")]
    public required List<NutrientReportRequestDto> NutrientReports { get; init; }
}