using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Theatre> Theatres => Set<Theatre>();
    public DbSet<Screen> Screens => Set<Screen>();
    public DbSet<Seat> Seats => Set<Seat>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MovieTicketBookingDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Movie Configuration
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DurationMinutes).IsRequired();
            entity.Property(e => e.ReleaseDate).IsRequired();
            entity.Property(e => e.Language).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
        });

        // Theatre Configuration
        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(e => e.TheatreId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(50);
        });

        // Screen Configuration & Relationship with Theatre
        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(e => e.ScreenId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.HasOne(s => s.Theatre)
                  .WithMany(t => t.Screens)
                  .HasForeignKey(s => s.TheatreId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seat Configuration & Relationship with Screen
        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId);
            entity.Property(e => e.SeatNumber).IsRequired().HasMaxLength(10);
            entity.Property(e => e.SeatType).IsRequired().HasMaxLength(50);

            entity.HasOne(s => s.Screen)
                  .WithMany(sc => sc.Seats)
                  .HasForeignKey(s => s.ScreenId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}