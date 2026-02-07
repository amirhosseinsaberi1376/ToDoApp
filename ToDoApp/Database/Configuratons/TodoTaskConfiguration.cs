using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Entities;

namespace ToDoApp.Database.Configuratons;

public sealed class TodoTaskConfiguration : IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder) 
    {
       builder.HasKey(t =>  t.Id);

       builder.Property(t => t.Id).HasMaxLength(500);

       builder.Property(t => t.Title).HasMaxLength(100);

       builder.Property(t => t.Description).HasMaxLength(500);

    }
}
