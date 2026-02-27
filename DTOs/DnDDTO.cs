using DnDAPI.Models;

namespace DnDAPI.DTOs;

public class DndDto
{

    public int Id { get; set; }

    public string? Monster { get; set; }

    public string? Type { get; set; }

    public int HitPoints { get; set; }

    public DndDto() { }
    public DndDto(Dnd monsterLog) =>
     (Id, Monster, Type, HitPoints) = (monsterLog.Id, monsterLog.Monster, monsterLog.Type?.Monster, monsterLog.HitPoints);
};


