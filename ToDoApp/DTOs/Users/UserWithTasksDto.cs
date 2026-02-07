namespace ToDoApp.DTOs.Users;

public sealed record UserWithTasksDto
{
    public required string Id { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public required string[] Tasks { get; init; }
}
