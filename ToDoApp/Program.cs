using ToDoApp;
using ToDoApp.Extensions;
using ToDoApp.Services.TodoTasks;
using ToDoApp.Services.Users;
using ToDoApp.Services.UserTodoTasks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddApiServices()
       .AddDatabase();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
