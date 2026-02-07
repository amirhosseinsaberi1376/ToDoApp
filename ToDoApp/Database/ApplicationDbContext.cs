using Microsoft.EntityFrameworkCore;
using ToDoApp.Entities;

namespace ToDoApp.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<UserTodoTask> UserTodoTasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Application);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }   
}
