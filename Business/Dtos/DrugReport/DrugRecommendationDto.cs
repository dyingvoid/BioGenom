namespace Business.Dtos;

public record DrugRecommendationDto
{
    public required List<DrugDto> Drugs { get; init; }
}