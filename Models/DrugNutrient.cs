namespace Models;

public record DrugNutrient
{
    public Guid Id { get; init; }
    public Guid DrugId { get; set; }
    public Drug? Drug { get; set; }
    public int NutrientId { get; set; }
    public Nutrient? Nutrient { get; set; }
    public required float Amount { get; set; }
}