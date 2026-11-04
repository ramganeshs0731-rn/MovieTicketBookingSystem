using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class ShowSeat
{
    [Key]
    public int ShowSeatId { get; set; }

    [Required]
    public int ShowId { get; set; }
    [ForeignKey("ShowId")]
    public Show Show { get; set; } = null!;

    [Required]
    public int SeatId { get; set; }
    [ForeignKey("SeatId")]
    public Seat Seat { get; set; } = null!;

    [Required]
    public bool IsBooked { get; set; } = false; // Status tracking
}