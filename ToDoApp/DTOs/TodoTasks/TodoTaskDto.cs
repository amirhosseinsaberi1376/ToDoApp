namespace ToDoApp.DTOs.TodoTasks;

public sealed record TodoTaskDto
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required bool IsCompleted { get; init; }
    public required DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public DateTime? LastCompletedAtUtc { get; init; }

}
