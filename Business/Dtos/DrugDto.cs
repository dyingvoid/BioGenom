namespace Business.Dtos;

public record DrugDto
{
    public required string SelfLink { get; init; }
    public required string ImageUrl { get; init; }
    public required List<DrugNutrientDto> Nutrients { get; init; }
}