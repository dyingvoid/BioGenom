using System.Text.Json.Serialization;

namespace Business.Dtos;

public class NutrientReportResponseDto
{
    [JsonPropertyName("nutrient_id")]
    public required int NutrientId { get; init; }
    
    [JsonPropertyName("nutrient_name")]
    public required string NutrientName { get; init; }

    [JsonPropertyName("current_intake")]
    public required float CurrentIntake { get; init; }

    [JsonPropertyName("min_intake")]
    public required float MinIntake { get; init; }

    [JsonPropertyName("max_intake")]
    public required float? MaxIntake { get; init; }
}