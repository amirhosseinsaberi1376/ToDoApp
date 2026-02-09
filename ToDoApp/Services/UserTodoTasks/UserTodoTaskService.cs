using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.UserTodoTasks;
using ToDoApp.Entities;

namespace ToDoApp.Services.UserTodoTasks;

public sealed class UserTodoTaskService : IUserTodoTaskService
{
    private readonly ApplicationDbContext _dbContext;

    public UserTodoTaskService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> UpsertUserTodoTasksAsync(string userId, UpsertUserTodoTasksDto dto)
    {
        User? user = await _dbContext.Users
            .Include(u => u.UserTodoTasks)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return false;
        }

        var currentTaskIds = user.UserTodoTasks.Select(ut => ut.TodoTaskId).ToHashSet();

        if (currentTaskIds.SetEquals(dto.TodoTaskIds))
        {
            return true; 
        }

        List<string> existingTaskIds = await _dbContext.TodoTasks
            .Where(t => dto.TodoTaskIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();

        if (existingTaskIds.Count != dto.TodoTaskIds.Count)
        {
            throw new InvalidOperationException("One or more task ids is invalid.");
        }

        user.UserTodoTasks.RemoveAll(ut => !dto.TodoTaskIds.Contains(ut.TodoTaskId));

        var todoTaskIdsToAdd = dto.TodoTaskIds.Except(currentTaskIds);
        user.UserTodoTasks.AddRange(todoTaskIdsToAdd.Select(todoTaskId => new UserTodoTask
        {
            UserId = userId,
            TodoTaskId = todoTaskId,
            CreatedAtUtc = DateTime.UtcNow
        }));

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserTodoTaskAsync(string userId, string todoTaskId)
    {
        UserTodoTask? userTodoTask = await _dbContext.UserTodoTasks
            .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TodoTaskId == todoTaskId);

        if (userTodoTask == null)
        {
            return false;
        }

        _dbContext.UserTodoTasks.Remove(userTodoTask);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
