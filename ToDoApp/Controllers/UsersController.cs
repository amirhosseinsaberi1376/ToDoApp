using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;
using ToDoApp.Services.Users;

namespace ToDoApp.Controllers;

[ApiController]
[Route("users")]
public sealed class UsersController(IUserService userService): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        return Ok(await userService.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOneUser(string id)
    {
        UserWithTasksDto user = await userService.GetUserByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateHabit(CreateUserDto createUserDto)
    {
        UserDto user = await userService.CreateUserAsync(createUserDto);
        return CreatedAtAction(nameof(GetOneUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
    {
        return await userService.UpdateUserAsync(id, updateUserDto)
            ? NoContent()
            : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchUser(string id, JsonPatchDocument<UserDto> patch)
    {
        return await userService.PatchUserAsync(id, patch)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        return await userService.DeleteUserAsync(id)
             ? NoContent()
             : NotFound();
    }

}
