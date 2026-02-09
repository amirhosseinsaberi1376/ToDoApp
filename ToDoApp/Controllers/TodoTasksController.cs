using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.TodoTasks;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;
using ToDoApp.Services.TodoTasks;

namespace ToDoApp.Controllers;


[ApiController]
[Route("tasks")]
public sealed class TodoTasksController(ITodoTaskService todoTaskService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetTodoTasks()
    {
        return Ok(await todoTaskService.GetTodoTasksAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneTodoTask(string id)
    {
        var task = await todoTaskService.GetTodoTaskByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskDto>> CreateTodoTask(
        CreateTodoTaskDto dto)
    {
        var task = await todoTaskService.CreateTodoTaskAsync(dto);
        return CreatedAtAction(nameof(GetOneTodoTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodoTask(
        string id,
        UpdateTodoTaskDto dto)
    {
        return await todoTaskService.UpdateTodoTaskAsync(id, dto)
            ? NoContent()
            : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchTodoTask(
        string id,
        JsonPatchDocument<TodoTaskDto> patch)
    {
        return await todoTaskService.PatchTodoTaskAsync(id, patch)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodoTask(string id)
    {
        return await todoTaskService.DeleteTodoTaskAsync(id)
            ? NoContent()
            : NotFound();
    }
}
