using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class Seat
{
    [Key]
    public int SeatId { get; set; }

    [Required]
    [MaxLength(10)]
    public string SeatNumber { get; set; } = string.Empty; // e.g., "A1", "B12"

    [Required]
    [MaxLength(50)]
    public string SeatType { get; set; } = "Normal"; // Normal, VIP, Recliner

    [Required]
    public int ScreenId { get; set; } // Foreign Key

    [ForeignKey("ScreenId")]
    public Screen Screen { get; set; } = null!;
}