using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ToDoApp.Database;

namespace ToDoApp;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(
             builder.Configuration.GetConnectionString("Database"),
             npgsqlOptions => npgsqlOptions
             .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Application))
         .UseSnakeCaseNamingConvention());

        return builder;
    }
}
