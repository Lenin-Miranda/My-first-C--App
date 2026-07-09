using System;

namespace TableGames.Desktop.Models;

/// <summary>
/// Representa un juego tal como lo devuelve la REST API.
/// </summary>
public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public int MinPlayers { get; set; } = 1;
    public int MaxPlayers { get; set; } = 4;
    public decimal PricePerHour { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}
