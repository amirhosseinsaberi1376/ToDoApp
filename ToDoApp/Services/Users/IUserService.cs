using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.DTOs.Users;

namespace ToDoApp.Services.Users;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserWithTasksDto?> GetUserByIdAsync(string id);
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task<bool> UpdateUserAsync(string id, UpdateUserDto dto);
    Task<bool> PatchUserAsync(string id, JsonPatchDocument<UserDto> patch);
    Task<bool> DeleteUserAsync(string id);
}
