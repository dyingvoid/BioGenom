using System.Text.Json.Serialization;

namespace Business.Dtos;

public record RecommendedNutrientIntakeDto
{
    [JsonPropertyName("nutrient_id")]
    public int NutrientId { get; init; }

    [JsonPropertyName("nutrient_name")]
    public string NutrientName { get; init; }

    [JsonPropertyName("current_intake")]
    public float CurrentIntake { get; init; }

    [JsonPropertyName("drug_intake")]
    public float DrugIntake { get; init; }

    [JsonPropertyName("food_intake")]
    public float FoodIntake { get; init; }
    
    public static RecommendedNutrientIntakeDto Create(
        int nutrientId, string nutrientName,
        float currentIntake, float drugIntake, float recommendedIntake)
    {
        return new RecommendedNutrientIntakeDto
        {
            NutrientId = nutrientId,
            NutrientName = nutrientName,
            CurrentIntake = currentIntake,
            DrugIntake = drugIntake,
            FoodIntake = Math.Max(0, recommendedIntake - currentIntake - drugIntake)
        };
    }
}