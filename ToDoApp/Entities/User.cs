namespace ToDoApp.Entities;

public sealed class User
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<UserTodoTask> UserTodoTasks { get; set; }
    public List<Task> Tasks { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
