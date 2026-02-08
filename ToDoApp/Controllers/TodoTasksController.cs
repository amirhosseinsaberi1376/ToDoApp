using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.TodoTasks;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;

namespace ToDoApp.Controllers;


[ApiController]
[Route("tasks")]
public sealed class TodoTasksController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetTodoTasks()
    {
        List<TodoTaskDto> todoTasks = await dbContext.TodoTasks
            .Select(TodoTaskQueries.ProjectToDto())
            .ToListAsync();

        return Ok(todoTasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneTodoTask(string id)
    {
        TodoTaskDto? todoTask = await dbContext.TodoTasks
                .Where(t => t.Id == id)
                .Select(TodoTaskQueries.ProjectToDto())
                .FirstOrDefaultAsync();

        if (todoTask is null)

        {
            return NotFound();
        }

        return Ok(todoTask);
    }

    [HttpPost]
    public async Task<ActionResult<TodoTaskDto>> CreateTodoTask(CreateTodoTaskDto createTodoTaskDto)
    {
        TodoTask todoTask = createTodoTaskDto.ToEntity();

        dbContext.TodoTasks.Add(todoTask);

        await dbContext.SaveChangesAsync();

        TodoTaskDto todoTaskDto = todoTask.ToDto();

        return CreatedAtAction(nameof(GetOneTodoTask), new { id = todoTaskDto.Id }, todoTaskDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodoTask(string id, UpdateTodoTaskDto updateTodoTaskDto)
    {
        TodoTask? todoTask = await dbContext.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);

        if (todoTask == null)
        {
            return NotFound();
        }

        todoTask.UpdateFromDto(updateTodoTaskDto);

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchTodoTask(string id, JsonPatchDocument<TodoTaskDto> patchDocument)
    {
        TodoTask? todoTask = await dbContext.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);

        if (todoTask == null)
        {
            return NotFound();
        }

        TodoTaskDto todoTaskDto = todoTask.ToDto();

        patchDocument.ApplyTo(todoTaskDto, ModelState);

        if (!TryValidateModel(todoTaskDto))
        {
            return ValidationProblem(ModelState);
        }

        todoTask.Title = todoTaskDto.Title;
        todoTask.Description = todoTaskDto.Description;
        todoTask.IsCompleted = todoTaskDto.IsCompleted;
        todoTask.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodoTask(string id)
    {
        TodoTask? todoTask = await dbContext.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);

        if (todoTask is null)
        {
            return NotFound();
        }

        dbContext.TodoTasks.Remove(todoTask);

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

}
