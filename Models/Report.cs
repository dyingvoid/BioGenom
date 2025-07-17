namespace Models;

public record Report
{
    public required Guid Id { get; init; }

    public Guid UserId { get; init; }

    public List<NutrientReport> NutrientReports { get; set; } = [];
}