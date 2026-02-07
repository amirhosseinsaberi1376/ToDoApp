namespace ToDoApp.Entities;

public sealed class TodoTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public DateTime? LastCompletedAtUtc { get; set; }

}
