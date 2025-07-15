namespace Models;

public record Drug
{
    public required Guid Id { get; init; }
    public required decimal Price { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public List<DrugNutrient> DrugNutrients { get; set; } = [];
    
    public Guid ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
}