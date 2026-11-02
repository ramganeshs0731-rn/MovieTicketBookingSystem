using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class Show
{
    [Key]
    public int ShowId { get; set; }

    [Required]
    public int MovieId { get; set; } // Foreign Key

    [ForeignKey("MovieId")]
    public Movie Movie { get; set; } = null!;

    [Required]
    public int ScreenId { get; set; } // Foreign Key

    [ForeignKey("ScreenId")]
    public Screen Screen { get; set; } = null!;

    [Required]
    public DateTime ShowTime { get; set; } // e.g., 2026-11-05T18:00:00

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TicketPrice { get; set; } // e.g., 150.00

    public ICollection<ShowSeat> ShowSeats { get; set; } = new List<ShowSeat>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}