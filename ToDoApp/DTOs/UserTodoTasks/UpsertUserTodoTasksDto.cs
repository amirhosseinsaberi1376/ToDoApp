namespace ToDoApp.DTOs.UserTodoTasks;

public sealed record UpsertUserTodoTasksDto
{
    public required List<string> TodoTaskIds { get; init; }
}
