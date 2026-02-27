using DnDAPI.Models;
using Microsoft.EntityFrameworkCore;
using MonsterType = DnDAPI.Models.Type;

namespace DnDAPI.Data;

public class DndStoreContext(DbContextOptions<DndStoreContext> options) : DbContext(options)
{
    public DbSet<Dnd> Dnds => Set<Dnd>();

    public DbSet<MonsterType> Types => Set<MonsterType>();
}
