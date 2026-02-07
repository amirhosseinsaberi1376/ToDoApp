namespace ToDoApp.DTOs.Users;

public sealed class CreateUserDto
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
}
