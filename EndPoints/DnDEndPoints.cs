using DnDAPI.Data;
using DnDAPI.DTOs;
using DnDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DnDAPI.EndPoints;

public static class DnDEndPoints
{
    public static void MapMonstersEndPoints(this WebApplication app)
    {
        var monstersUrl = app.MapGroup("/monsters");

        monstersUrl.MapGet("/", GetAllMonsters);
        monstersUrl.MapGet("/{id}", GetMonster);
        monstersUrl.MapPost("/", CreateMonster);
        monstersUrl.MapPut("/{id}", UpdateMonster);
        monstersUrl.MapDelete("/{id}", DeleteMonster);
    }

    private static async Task<IResult> GetAllMonsters(DndStoreContext db)
    {
        return TypedResults.Ok(await db.Dnds
            .Include(x => x.Type)
            .Select(x => new DndDto(x))
            .ToArrayAsync());
    }

    private static async Task<IResult> GetMonster(int id, DndStoreContext db)
    {
        return await db.Dnds
            .Include(x => x.Type)
            .FirstOrDefaultAsync(x => x.Id == id)
            is Dnd dnd
                ? TypedResults.Ok(new DndDto(dnd))
                : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateMonster(CreateDnd dto, DndStoreContext db)
    {
        // Find existing type by name or create a new one
        var type = await db.Types.SingleOrDefaultAsync(t => t.Monster == dto.Type);

        if (type is null)
        {
            type = new Models.Type
            {
                Monster = dto.Type
            };

            db.Types.Add(type);
        }

        var entity = new Dnd
        {
            Monster = dto.Monster,
            HitPoints = dto.HitPoints,
            Type = type
        };

        db.Dnds.Add(entity);

        await db.SaveChangesAsync();

        var createdDto = new DndDto(entity);
        return TypedResults.Created($"/monsters/{entity.Id}", createdDto);
    }

    private static async Task<IResult> UpdateMonster(int id, UpdateDnd dto, DndStoreContext db)
    {
        var entity = await db.Dnds.FindAsync(id);

        if (entity is null) return TypedResults.NotFound();

        entity.Monster = dto.Monster;
        entity.HitPoints = dto.HitPoints;

        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteMonster(int id, DndStoreContext db)
    {
        if (await db.Dnds.FindAsync(id) is Dnd dnd)
        {
            db.Dnds.Remove(dnd);
            await db.SaveChangesAsync();
        }

        return TypedResults.NoContent();
    }
}
