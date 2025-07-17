using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Dtos;

public record NutrientReportRequestDto
{
    [JsonPropertyName("nutrient_id")]
    public required int NutrientId { get; init; }

    [JsonPropertyName("current_intake")]
    [Range(0, float.MaxValue)]
    public required float CurrentIntake { get; init; }
}