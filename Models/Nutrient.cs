namespace Models;

public record Nutrient
{
    public required int Id { get; init; }
    public required string Name { get; set; }

    public required float MinAmount { get; set; }

    public required float? MaxAmount { get; set; }

    public int UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
}