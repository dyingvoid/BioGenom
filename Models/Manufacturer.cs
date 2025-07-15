namespace Models;

public record Manufacturer
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
}