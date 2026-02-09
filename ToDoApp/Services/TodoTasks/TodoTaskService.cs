using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.TodoTasks;
using ToDoApp.Entities;

namespace ToDoApp.Services.TodoTasks;

public sealed class TodoTaskService : ITodoTaskService
{
    private readonly ApplicationDbContext _dbContext;

    public TodoTaskService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TodoTaskDto>> GetTodoTasksAsync()
    {
        return await _dbContext.TodoTasks
            .Select(TodoTaskQueries.ProjectToDto())
            .ToListAsync();
    }

    public async Task<TodoTaskDto?> GetTodoTaskByIdAsync(string id)
    {
        return await _dbContext.TodoTasks
            .Where(t => t.Id == id)
            .Select(TodoTaskQueries.ProjectToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<TodoTaskDto> CreateTodoTaskAsync(CreateTodoTaskDto dto)
    {
        TodoTask task = dto.ToEntity();

        _dbContext.TodoTasks.Add(task);
        await _dbContext.SaveChangesAsync();

        return task.ToDto();
    }

    public async Task<bool> UpdateTodoTaskAsync(string id, UpdateTodoTaskDto dto)
    {
        TodoTask? task = await _dbContext.TodoTasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        task.UpdateFromDto(dto);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> PatchTodoTaskAsync(
        string id,
        JsonPatchDocument<TodoTaskDto> patch)
    {
        TodoTask? task = await _dbContext.TodoTasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        TodoTaskDto dto = task.ToDto();

        patch.ApplyTo(dto);

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsCompleted = dto.IsCompleted;
        task.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTodoTaskAsync(string id)
    {
        TodoTask? task = await _dbContext.TodoTasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return false;
        }

        _dbContext.TodoTasks.Remove(task);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
