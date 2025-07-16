namespace Business.Common;

public record Deficit
{
    public required int NutrientId { get; init; }
    public required float Amount { get; init; }
}