using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.UserTodoTasks;
using ToDoApp.Entities;
using ToDoApp.Services.UserTodoTasks;

namespace ToDoApp.Controllers;

[ApiController]
[Route("/users/{userId}/tasks")]
public sealed class UserTodoTasksController(IUserTodoTaskService userTodoTaskService) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult> UpsertUserTodoTasks(
        string userId, UpsertUserTodoTasksDto dto)
    {
        try
        {
            bool result = await userTodoTaskService.UpsertUserTodoTasksAsync(userId, dto);
            return result ? NoContent() : NotFound();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("One or more task ids is invalid.");
        }
    }

    [HttpDelete("{todoTaskId}")]
    public async Task<ActionResult> DeleteUserTodoTask(string userId, string todoTaskId)
    {
        bool result = await userTodoTaskService.DeleteUserTodoTaskAsync(userId, todoTaskId);
        return result ? NoContent() : NotFound();
    }
}
