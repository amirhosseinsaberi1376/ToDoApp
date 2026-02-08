using ToDoApp.Entities;

namespace ToDoApp.DTOs.TodoTasks;

internal static class TodoTaskMappings
{
    public static TodoTaskDto ToDto(this TodoTask todoTask)
    {
        return new TodoTaskDto
        {
            Id = todoTask.Id,
            Title = todoTask.Title,
            Description = todoTask.Description,
            IsCompleted = todoTask.IsCompleted,
            CreatedAtUtc = todoTask.CreatedAtUtc,
            UpdatedAtUtc = todoTask.UpdatedAtUtc,
        };
    }
    public static TodoTask ToEntity(this CreateTodoTaskDto dto)
    {
        TodoTask todoTask = new()
        {
            Id = $"t_{Guid.CreateVersion7()}",
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            CreatedAtUtc = DateTime.UtcNow
        };

        return todoTask;
    }

    public static void UpdateFromDto(this TodoTask todoTask, UpdateTodoTaskDto dto)
    {
        todoTask.Title = dto.Title;
        todoTask.Description = dto.Description;
        todoTask.IsCompleted = dto.IsCompleted;
    }
}
