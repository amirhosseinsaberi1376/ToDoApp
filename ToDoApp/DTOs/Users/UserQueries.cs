using System.Linq.Expressions;
using ToDoApp.DTOs.TodoTasks;
using ToDoApp.Entities;

namespace ToDoApp.DTOs.Users;

internal static class UserQueries
{
public static Expression<Func<User, UserDto>> ProjectToDto()
    {
        return u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            CreatedAtUtc = u.CreatedAtUtc,
            UpdatedAtUtc = u.UpdatedAtUtc,
            Tasks = u.Tasks
                .Select(t => t.Title)
                .ToArray(),
        };
    }
}
