using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class BookingSeat
{
    [Key]
    public int BookingSeatId { get; set; }

    [Required]
    public int BookingId { get; set; }
    [ForeignKey("BookingId")]
    public Booking Booking { get; set; } = null!;

    [Required]
    public int SeatId { get; set; }
    [ForeignKey("SeatId")]
    public Seat Seat { get; set; } = null!;
}