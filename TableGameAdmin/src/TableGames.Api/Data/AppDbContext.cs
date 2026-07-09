using Microsoft.EntityFrameworkCore;
using TableGames.Api.Models;

namespace TableGames.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .Property(g => g.PricePerHour)
            .HasColumnType("decimal(10,2)");

        // Datos de ejemplo para que el dashboard no arranque vacío.
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Name = "Catan", Category = "Estrategia", MinPlayers = 3, MaxPlayers = 4, PricePerHour = 5.00m, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
            new Game { Id = 2, Name = "Carcassonne", Category = "Estrategia", MinPlayers = 2, MaxPlayers = 5, PricePerHour = 4.50m, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
            new Game { Id = 3, Name = "Uno", Category = "Cartas", MinPlayers = 2, MaxPlayers = 10, PricePerHour = 2.00m, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
            new Game { Id = 4, Name = "Ajedrez", Category = "Clásico", MinPlayers = 2, MaxPlayers = 2, PricePerHour = 3.00m, IsAvailable = false, CreatedAt = new DateTime(2024, 1, 1) }
        );
    }
}
