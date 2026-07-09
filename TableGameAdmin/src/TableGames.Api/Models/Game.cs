using System.ComponentModel.DataAnnotations;

namespace TableGames.Api.Models;

/// <summary>
/// Un juego de mesa del catálogo que administra el dashboard.
/// </summary>
public class Game
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(60)]
    public string Category { get; set; } = "General";

    public int MinPlayers { get; set; } = 1;

    public int MaxPlayers { get; set; } = 4;

    /// <summary>Precio por hora de alquiler de la mesa.</summary>
    public decimal PricePerHour { get; set; }

    /// <summary>Si el juego está disponible para reservar.</summary>
    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
