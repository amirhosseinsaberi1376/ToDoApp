using System.Linq.Expressions;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;

namespace ToDoApp.DTOs.TodoTasks;

internal static class TodoTaskQueries
{
    public static Expression<Func<TodoTask, TodoTaskDto>> ProjectToDto()
    {
        return t => new TodoTaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAtUtc = t.CreatedAtUtc,
            UpdatedAtUtc = t.UpdatedAtUtc,
            LastCompletedAtUtc = t.LastCompletedAtUtc
        };
    }
    }
