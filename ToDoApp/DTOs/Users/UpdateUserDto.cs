namespace ToDoApp.DTOs.Users;

public class UpdateUserDto
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
}
