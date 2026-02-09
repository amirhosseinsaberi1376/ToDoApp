using ToDoApp.DTOs.UserTodoTasks;

namespace ToDoApp.Services.UserTodoTasks;

public interface IUserTodoTaskService
{
    Task<bool> UpsertUserTodoTasksAsync(string userId, UpsertUserTodoTasksDto dto);
    Task<bool> DeleteUserTodoTaskAsync(string userId, string todoTaskId);
}
