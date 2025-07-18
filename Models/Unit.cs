namespace Models;

public record Unit
{
    public required int Id { get; init; }
    public required string Name { get; set; }
}