using System;

namespace DnDAPI.Models;

public class Dnd
{
    public int Id { get; set; }

    public required string Monster { get; set; }

    public Type? Type { get; set; }

    public int TypeId { get; set; }

    public int HitPoints { get; set; }
}
