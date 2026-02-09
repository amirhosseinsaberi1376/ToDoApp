using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.DTOs.TodoTasks;

namespace ToDoApp.Services.TodoTasks;

public interface ITodoTaskService
{
    Task<List<TodoTaskDto>> GetTodoTasksAsync();
    Task<TodoTaskDto?> GetTodoTaskByIdAsync(string id);
    Task<TodoTaskDto> CreateTodoTaskAsync(CreateTodoTaskDto dto);
    Task<bool> UpdateTodoTaskAsync(string id, UpdateTodoTaskDto dto);
    Task<bool> PatchTodoTaskAsync(string id, JsonPatchDocument<TodoTaskDto> patch);
    Task<bool> DeleteTodoTaskAsync(string id);
}
