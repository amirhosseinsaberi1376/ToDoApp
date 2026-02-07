namespace ToDoApp.Entities;

public sealed class UserTodoTask
{
    public string UserId { get; set; }
    public string TodoTaskId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
