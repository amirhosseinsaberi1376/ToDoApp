using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.Users;

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
}
