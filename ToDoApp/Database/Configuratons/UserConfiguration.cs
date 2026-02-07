using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Entities;

namespace ToDoApp.Database.Configuratons;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasMaxLength(500);

        builder.Property(u => u.UserName).HasMaxLength(500);

        builder.Property(u => u.Email).HasMaxLength(500);

        builder.HasMany(u => u.Tasks)
            .WithMany()
            .UsingEntity<UserTodoTask>();
    }
}
