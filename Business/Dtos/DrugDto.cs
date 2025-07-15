namespace Business.Dtos;

public record DrugDto
{
    public required decimal Price { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required List<DrugNutrientDto> Nutrients { get; init; }
    public required Guid ManufacturerId { get; init; }
}