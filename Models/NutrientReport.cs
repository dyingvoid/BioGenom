namespace Models;

public record NutrientReport
{
    public required Guid Id { get; init; }

    public required float CurrentIntake { get; set; }


    public Guid ReportId { get; init; }
    public Report Report { get; init; } = null!;

    public int NutrientId { get; init; }
    public Nutrient Nutrient { get; init; } = null!;
}