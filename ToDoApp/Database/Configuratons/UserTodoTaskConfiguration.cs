using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Entities;

namespace ToDoApp.Database.Configuratons;

public sealed class UserTodoTaskConfiguration : IEntityTypeConfiguration<UserTodoTask>
{
    public void Configure(EntityTypeBuilder<UserTodoTask> builder)
    {
        builder.HasKey(ut => new { ut.UserId, ut.TodoTaskId });

        builder.HasOne<User>()
            .WithMany(u => u.UserTodoTasks)
            .HasForeignKey(ut => ut.UserId);

        builder.HasOne<TodoTask>()
            .WithMany()
            .HasForeignKey(ut => ut.TodoTaskId);
    }
}
