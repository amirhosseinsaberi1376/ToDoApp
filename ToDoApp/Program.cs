using ToDoApp;
using ToDoApp.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddDatabase();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
