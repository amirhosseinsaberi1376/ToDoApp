using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.DTOs.Users;
using ToDoApp.Entities;

namespace ToDoApp.Services.Users;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _dbContext.Users.Select(UserQueries.ProjectToDto()).ToListAsync();
    }

    public async Task<UserWithTasksDto?> GetUserByIdAsync(string id)
    {
        return await _dbContext.Users
            .Where(u => u.Id == id)
            .Select(UserQueries.ProjectWithTasksToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        User user = dto.ToEntity();

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return user.ToDto();
    }

    public async Task<bool> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
        {
            return false;
        }

        user.UpdateFromDto(dto);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> PatchUserAsync(string id, JsonPatchDocument<UserDto> patch)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
            return false;
        }

        UserDto userDto = user.ToDto();

        patch.ApplyTo(userDto);

        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.UpdatedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if(user == null)
        {
            return false;
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
