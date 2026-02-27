using System.ComponentModel.DataAnnotations;

namespace DnDAPI.DTOs;

public record CreateDnd(
   [Required][StringLength(50)] string Monster,
    [Required][StringLength(50)] string Type,
    [Required][Range(1, 999)] int HitPoints
);