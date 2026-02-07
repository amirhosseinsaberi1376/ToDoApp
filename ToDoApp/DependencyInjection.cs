using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Serialization;
using ToDoApp.Database;

namespace ToDoApp;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver =
     new CamelCasePropertyNamesContractResolver())
     .AddXmlSerializerFormatters();

        builder.Services.AddOpenApi();

        return builder;
    }
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
