namespace Models;

public record Report
{
    public required Guid Id { get; init; }

    public Guid UserId { get; init; }
    public User User { get; init; } = null!;

    public List<NutrientReport> NutrientReports { get; set; } = [];
}