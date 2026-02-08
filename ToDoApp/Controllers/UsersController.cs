using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;

namespace ToDoApp.Controllers;

[ApiController]
[Route("users")]
public sealed class UsersController(ApplicationDbContext dbContext): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        List<UserDto> users = await dbContext.Users
            .Select(UserQueries.ProjectToDto())
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneUser(string id)
    {
        UserWithTasksDto? user = await dbContext.Users
                .Where(u => u.Id == id)
                .Select(UserQueries.ProjectWithTasksToDto())
                .FirstOrDefaultAsync();

        if (user is null)

        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateHabit(CreateUserDto createUserDto)
    {
        User user = createUserDto.ToEntity();

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync();

        UserDto userDto = user.ToDto();

        return CreatedAtAction(nameof(GetOneUser), new { id = userDto.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
            return NotFound();
        }

        user.UpdateFromDto(updateUserDto);

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchUser(string id, JsonPatchDocument<UserDto> patchDocument)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        UserDto userDto = user.ToDto();

        patchDocument.ApplyTo(userDto, ModelState);

        if (!TryValidateModel(userDto))
        {
            return ValidationProblem(ModelState);
        }

        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(h => h.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();

        return NoContent();
    }

}
