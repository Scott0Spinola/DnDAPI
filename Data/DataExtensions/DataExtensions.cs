
using Microsoft.EntityFrameworkCore;
using MonsterType = DnDAPI.Models.Type;
namespace DnDAPI.Data;

public static class DataExtensions
{
    public static void MigratedDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DndStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddDndStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("DndStrore");
        builder.Services.AddScoped<DndStoreContext>();
        builder.Services.AddSqlite<DndStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<MonsterType>().Any())
                {
                    context.Set<MonsterType>().AddRange(
                        new MonsterType { Monster = "Beast" },
                        new MonsterType { Monster = "Dragon" },
                        new MonsterType { Monster = "Undead" },
                        new MonsterType { Monster = "Fiend" },
                        new MonsterType { Monster = "Construct" }
                    );

                    context.SaveChanges();
                }
            }
            )
            );

    }
}
