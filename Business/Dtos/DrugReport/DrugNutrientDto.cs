namespace Business.Dtos;

public record DrugNutrientDto
{
    public required int NutrientId { get; init; }
    public required float Amount { get; init; }
}