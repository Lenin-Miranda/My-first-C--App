using System.ComponentModel.DataAnnotations;

namespace TableGames.Api.Dtos;

/// <summary>Datos que envía el cliente para crear o actualizar un juego.</summary>
public record GameInput(
    [Required, MaxLength(120)] string Name,
    [MaxLength(60)] string Category,
    [Range(1, 100)] int MinPlayers,
    [Range(1, 100)] int MaxPlayers,
    [Range(0, 10000)] decimal PricePerHour,
    bool IsAvailable
);
