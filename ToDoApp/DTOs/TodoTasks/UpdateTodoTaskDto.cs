namespace ToDoApp.DTOs.TodoTasks;

public sealed class UpdateTodoTaskDto
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required bool IsCompleted { get; init; }
}
