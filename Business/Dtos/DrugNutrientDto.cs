namespace Business.Dtos;

public record DrugNutrientDto
{
    public required string NutrientName { get; init; }
    public required float Amount { get; init; }
}