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
    public DbSet<Show> Shows => Set<Show>();
    public DbSet<ShowSeat> ShowSeats => Set<ShowSeat>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();
    public DbSet<User> Users => Set<User>();

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

        // Show Configuration & Relationships
        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(e => e.ShowId);
            entity.Property(e => e.ShowTime).IsRequired();
            entity.Property(e => e.TicketPrice).IsRequired().HasColumnType("decimal(18,2)");

            entity.HasOne(s => s.Movie)
                  .WithMany(m => m.Shows)
                  .HasForeignKey(s => s.MovieId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Screen)
                  .WithMany()
                  .HasForeignKey(s => s.ScreenId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ShowSeat Configuration
        modelBuilder.Entity<ShowSeat>(entity =>
        {
            entity.HasKey(e => e.ShowSeatId);
            entity.HasOne(ss => ss.Show)
                  .WithMany(s => s.ShowSeats)
                  .HasForeignKey(ss => ss.ShowId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ss => ss.Seat)
                  .WithMany()
                  .HasForeignKey(ss => ss.SeatId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Booking Configuration
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId);
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

            entity.HasOne(b => b.Show)
                  .WithMany(s => s.Bookings)
                  .HasForeignKey(b => b.ShowId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // BookingSeat Configuration
        modelBuilder.Entity<BookingSeat>(entity =>
        {
            entity.HasKey(e => e.BookingSeatId);
            entity.HasOne(bs => bs.Booking)
                  .WithMany(b => b.BookingSeats)
                  .HasForeignKey(bs => bs.BookingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(bs => bs.Seat)
                  .WithMany()
                  .HasForeignKey(bs => bs.SeatId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
        });
    }
}