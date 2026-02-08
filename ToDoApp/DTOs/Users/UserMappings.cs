using ToDoApp.Entities;

namespace ToDoApp.DTOs.Users;

internal static class UserMappings
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            CreatedAtUtc = user.CreatedAtUtc,
            UpdatedAtUtc = user.UpdatedAtUtc,
        };
    }
    public static User ToEntity(this CreateUserDto dto)
    {
        User user = new()
        {
            Id = $"u_{Guid.CreateVersion7()}",
            UserName = dto.UserName,
            Email = dto.Email,
            CreatedAtUtc = DateTime.UtcNow
        };

        return user;
    }

    public static void UpdateFromDto(this User user, UpdateUserDto dto)
    {
        user.UserName = dto.UserName;
        user.Email = dto.Email;
    }
}
