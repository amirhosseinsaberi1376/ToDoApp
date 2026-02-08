using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.UserTodoTasks;
using ToDoApp.Entities;

namespace ToDoApp.Controllers;

[ApiController]
[Route("/users/{userId}/tasks")]
public sealed class UserTodoTasksController(ApplicationDbContext dbContext): ControllerBase
{
    [HttpPut]
    public async Task<ActionResult> UpsertUserTodoTasks(string userId, UpsertUserTodoTasksDto upsertUserTodoTasksDto)
    {
        User? user = await dbContext.Users.Include(u => u.UserTodoTasks)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if(user is null)
        {
            return NotFound();
        }

        var currentTaskIds = user.UserTodoTasks.Select(ut => ut.TodoTaskId).ToHashSet();
        
        if(currentTaskIds.SetEquals(upsertUserTodoTasksDto.TodoTaskIds))
        {
            return NoContent();
        }

        List<string> existingTaskIds = await dbContext.TodoTasks
            .Where(t => upsertUserTodoTasksDto.TodoTaskIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();

        if(existingTaskIds.Count != upsertUserTodoTasksDto.TodoTaskIds.Count)
        {
            return BadRequest("One or more tag ids is invalid.");
        }

        user.UserTodoTasks.RemoveAll(ut => !upsertUserTodoTasksDto.TodoTaskIds.Contains(ut.TodoTaskId));

        string[] todoTaskIdsToAdd = upsertUserTodoTasksDto.TodoTaskIds.Except(currentTaskIds).ToArray();

        user.UserTodoTasks.AddRange(todoTaskIdsToAdd.Select(todoTaskId => new UserTodoTask
        {
            UserId = userId,
            TodoTaskId = todoTaskId,
            CreatedAtUtc = DateTime.UtcNow,
        }));

        await dbContext.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{todoTaskId}")]
    public async Task<ActionResult> DeleteUserTodoTask(string userId, string todoTaskId)
    {
        UserTodoTask? userTodoTask = await dbContext.UserTodoTasks
            .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TodoTaskId == todoTaskId);

        if(userTodoTask == null)
        {
           return NotFound();
        } 
        
        dbContext.UserTodoTasks.Remove(userTodoTask);

        await dbContext.SaveChangesAsync();

        return NoContent();
        

    }

}
