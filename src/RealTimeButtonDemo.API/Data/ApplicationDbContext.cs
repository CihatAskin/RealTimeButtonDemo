using Microsoft.EntityFrameworkCore;
using RealTimeButtonDemo.Shared.Models;

namespace RealTimeButtonDemo.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ButtonState> ButtonStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ButtonState>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RoomId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(20);
            entity.Property(e => e.LastClickedBy).HasMaxLength(50);
            entity.HasIndex(e => e.RoomId);
        });

        // Seed data
        modelBuilder.Entity<ButtonState>().HasData(
            new ButtonState 
            { 
                Id = 1, 
                RoomId = "demo-room", 
                Color = "#007bff", 
                LastClickedBy = "system",
                LastClickedAt = DateTime.UtcNow,
                IsActive = true 
            }
        );
    }
}