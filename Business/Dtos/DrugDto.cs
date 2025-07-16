namespace Business.Dtos;

public record DrugDto
{
    public required string Ref { get; init; }
    public required string PictureRef { get; init; }
    public required List<DrugNutrientDto> Nutrients { get; init; }
}