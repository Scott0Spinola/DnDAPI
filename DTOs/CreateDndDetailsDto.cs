namespace DnDAPI.DTOs;

public record CreateDndDetailsDto(
    int Id,
    string Monster,
    int TypeId,
    int HitPoints
);