namespace Models;

public record User
{
    public required Guid Id { get; init; }
    public List<Report> Reports { get; set; } = [];
}